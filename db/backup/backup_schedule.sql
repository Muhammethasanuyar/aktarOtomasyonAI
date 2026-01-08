/*
    Sprint 8: SQL Server Agent Backup Schedule

    PURPOSE:
    Creates automated SQL Server Agent jobs for database backups.
    Implements the Sprint 8 backup strategy:
    - Full backups: Daily at 2:00 AM
    - Differential backups: Every 4 hours (6 AM, 10 AM, 2 PM, 6 PM)
    - Transaction log backups: Hourly during business hours (9 AM - 6 PM)

    PREREQUISITES:
    - SQL Server Agent service must be running
    - User must have sysadmin or SQLAgentOperatorRole
    - Backup directories must exist
    - Database Backup operator (aktar_backup) created

    USAGE:
    sqlcmd -S localhost -E -i backup_schedule.sql

    MONITORING:
    - View job history: SSMS → SQL Server Agent → Jobs → Right-click → View History
    - Email notifications: Configure Database Mail first
    - Job status query:
        SELECT name, enabled, date_created, date_modified
        FROM msdb.dbo.sysjobs
        WHERE name LIKE 'AktarOtomasyon%'
*/

USE [msdb]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Creating SQL Server Agent Backup Jobs'
PRINT '========================================='
PRINT ''

-- =============================================
-- Configuration Variables
-- =============================================

DECLARE @DatabaseName NVARCHAR(128) = 'AktarOtomasyon'
DECLARE @BackupUser NVARCHAR(128) = 'aktar_backup'  -- SQL user for backups
DECLARE @NotificationEmail NVARCHAR(255) = NULL  -- Set to receive email alerts (requires Database Mail)

-- Backup script paths (update these to match your environment)
DECLARE @FullBackupScript NVARCHAR(500) = 'C:\AktarOtomasyon\db\backup\backup_full.sql'
DECLARE @DiffBackupScript NVARCHAR(500) = 'C:\AktarOtomasyon\db\backup\backup_differential.sql'
DECLARE @LogBackupScript NVARCHAR(500) = 'C:\AktarOtomasyon\db\backup\backup_log.sql'

-- =============================================
-- STEP 1: Verify SQL Server Agent is Running
-- =============================================

IF NOT EXISTS (
    SELECT 1
    FROM sys.dm_server_services
    WHERE servicename LIKE 'SQL Server Agent%'
      AND status_desc = 'Running'
)
BEGIN
    PRINT '✗ ERROR: SQL Server Agent is not running!'
    PRINT '  Start SQL Server Agent service to create scheduled jobs.'
    PRINT ''
    RAISERROR('SQL Server Agent not running', 16, 1)
    RETURN
END

PRINT '✓ SQL Server Agent is running'

-- =============================================
-- JOB 1: Full Backup (Daily at 2:00 AM)
-- =============================================

PRINT ''
PRINT 'Creating job: AktarOtomasyon - Full Backup...'

DECLARE @JobName NVARCHAR(128) = 'AktarOtomasyon - Full Backup'
DECLARE @JobID UNIQUEIDENTIFIER

-- Delete job if exists
IF EXISTS (SELECT 1 FROM msdb.dbo.sysjobs WHERE name = @JobName)
BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = @JobName, @delete_unused_schedule=1
    PRINT '  ⚠ Deleted existing job'
END

-- Create job
EXEC msdb.dbo.sp_add_job
    @job_name = @JobName,
    @enabled = 1,
    @description = 'Daily full backup of AktarOtomasyon database at 2:00 AM',
    @category_name = 'Database Maintenance',
    @job_id = @JobID OUTPUT

-- Add job step
EXEC msdb.dbo.sp_add_jobstep
    @job_id = @JobID,
    @step_name = 'Run Full Backup Script',
    @subsystem = 'TSQL',
    @command = N'
        -- Full backup with compression and checksum
        BACKUP DATABASE AktarOtomasyon
        TO DISK = N''C:\Backups\AktarOtomasyon\Full\AktarOtomasyon_FULL_'' +
                  CONVERT(NVARCHAR(20), GETDATE(), 112) + ''_'' +
                  REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), '':'', '''') + ''.bak''
        WITH COMPRESSION, CHECKSUM, INIT, STATS = 10
    ',
    @database_name = 'master',
    @on_success_action = 1,  -- Quit with success
    @on_fail_action = 2,     -- Quit with failure
    @retry_attempts = 3,
    @retry_interval = 5

-- Add daily schedule at 2:00 AM
EXEC msdb.dbo.sp_add_jobschedule
    @job_id = @JobID,
    @name = 'Daily at 2:00 AM',
    @enabled = 1,
    @freq_type = 4,          -- Daily
    @freq_interval = 1,      -- Every 1 day
    @active_start_time = 20000  -- 02:00:00 (2:00 AM)

-- Add notification (if email configured)
IF @NotificationEmail IS NOT NULL
BEGIN
    EXEC msdb.dbo.sp_add_jobserver @job_id = @JobID, @server_name = N'(local)'
    EXEC msdb.dbo.sp_update_job @job_id = @JobID,
        @notify_level_email = 2,  -- On failure
        @notify_email_operator_name = @NotificationEmail
END
ELSE
BEGIN
    EXEC msdb.dbo.sp_add_jobserver @job_id = @JobID, @server_name = N'(local)'
END

PRINT '  ✓ Created: ' + @JobName

-- =============================================
-- JOB 2: Differential Backup (Every 4 hours)
-- =============================================

PRINT ''
PRINT 'Creating job: AktarOtomasyon - Differential Backup...'

SET @JobName = 'AktarOtomasyon - Differential Backup'

IF EXISTS (SELECT 1 FROM msdb.dbo.sysjobs WHERE name = @JobName)
BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = @JobName, @delete_unused_schedule=1
    PRINT '  ⚠ Deleted existing job'
END

EXEC msdb.dbo.sp_add_job
    @job_name = @JobName,
    @enabled = 1,
    @description = 'Differential backup every 4 hours (6 AM, 10 AM, 2 PM, 6 PM)',
    @category_name = 'Database Maintenance',
    @job_id = @JobID OUTPUT

EXEC msdb.dbo.sp_add_jobstep
    @job_id = @JobID,
    @step_name = 'Run Differential Backup',
    @subsystem = 'TSQL',
    @command = N'
        -- Differential backup with compression
        BACKUP DATABASE AktarOtomasyon
        TO DISK = N''C:\Backups\AktarOtomasyon\Diff\AktarOtomasyon_DIFF_'' +
                  CONVERT(NVARCHAR(20), GETDATE(), 112) + ''_'' +
                  REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), '':'', '''') + ''.bak''
        WITH DIFFERENTIAL, COMPRESSION, CHECKSUM, INIT, STATS = 10
    ',
    @database_name = 'master',
    @on_success_action = 1,
    @on_fail_action = 2,
    @retry_attempts = 3,
    @retry_interval = 5

-- Schedule: Every 4 hours starting at 6:00 AM
EXEC msdb.dbo.sp_add_jobschedule
    @job_id = @JobID,
    @name = 'Every 4 hours (6 AM, 10 AM, 2 PM, 6 PM)',
    @enabled = 1,
    @freq_type = 4,          -- Daily
    @freq_interval = 1,      -- Every day
    @freq_subday_type = 8,   -- Hours
    @freq_subday_interval = 4,  -- Every 4 hours
    @active_start_time = 60000,  -- 06:00:00 (6:00 AM)
    @active_end_time = 180000    -- 18:00:00 (6:00 PM)

EXEC msdb.dbo.sp_add_jobserver @job_id = @JobID, @server_name = N'(local)'

PRINT '  ✓ Created: ' + @JobName

-- =============================================
-- JOB 3: Transaction Log Backup (Hourly during business hours)
-- =============================================

PRINT ''
PRINT 'Creating job: AktarOtomasyon - Log Backup...'

SET @JobName = 'AktarOtomasyon - Log Backup'

IF EXISTS (SELECT 1 FROM msdb.dbo.sysjobs WHERE name = @JobName)
BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = @JobName, @delete_unused_schedule=1
    PRINT '  ⚠ Deleted existing job'
END

EXEC msdb.dbo.sp_add_job
    @job_name = @JobName,
    @enabled = 1,
    @description = 'Transaction log backup hourly during business hours (9 AM - 6 PM)',
    @category_name = 'Database Maintenance',
    @job_id = @JobID OUTPUT

-- Check if database is in FULL recovery model
EXEC msdb.dbo.sp_add_jobstep
    @job_id = @JobID,
    @step_name = 'Check Recovery Model',
    @subsystem = 'TSQL',
    @command = N'
        IF (SELECT recovery_model_desc FROM sys.databases WHERE name = ''AktarOtomasyon'') = ''SIMPLE''
        BEGIN
            RAISERROR(''Database in SIMPLE recovery model - cannot backup transaction log'', 16, 1)
        END
    ',
    @database_name = 'master',
    @on_success_action = 3,  -- Go to next step
    @on_fail_action = 2      -- Quit with failure

-- Backup transaction log
EXEC msdb.dbo.sp_add_jobstep
    @job_id = @JobID,
    @step_name = 'Run Log Backup',
    @subsystem = 'TSQL',
    @command = N'
        -- Transaction log backup with compression
        BACKUP LOG AktarOtomasyon
        TO DISK = N''C:\Backups\AktarOtomasyon\Log\AktarOtomasyon_LOG_'' +
                  CONVERT(NVARCHAR(20), GETDATE(), 112) + ''_'' +
                  REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), '':'', '''') + ''.trn''
        WITH COMPRESSION, CHECKSUM, INIT, STATS = 10
    ',
    @database_name = 'master',
    @on_success_action = 1,
    @on_fail_action = 2,
    @retry_attempts = 3,
    @retry_interval = 5

-- Schedule: Hourly from 9 AM to 6 PM
EXEC msdb.dbo.sp_add_jobschedule
    @job_id = @JobID,
    @name = 'Hourly during business hours',
    @enabled = 1,
    @freq_type = 4,          -- Daily
    @freq_interval = 1,      -- Every day
    @freq_subday_type = 8,   -- Hours
    @freq_subday_interval = 1,  -- Every 1 hour
    @active_start_time = 90000,  -- 09:00:00 (9:00 AM)
    @active_end_time = 180000    -- 18:00:00 (6:00 PM)

EXEC msdb.dbo.sp_add_jobserver @job_id = @JobID, @server_name = N'(local)'

PRINT '  ✓ Created: ' + @JobName

-- =============================================
-- STEP 4: Verify Jobs Created
-- =============================================

PRINT ''
PRINT '========================================='
PRINT 'Job Creation Summary'
PRINT '========================================='
PRINT ''

SELECT
    j.name AS JobName,
    CASE j.enabled WHEN 1 THEN 'Yes' ELSE 'No' END AS Enabled,
    s.name AS ScheduleName,
    CASE s.freq_type
        WHEN 4 THEN 'Daily'
        WHEN 8 THEN 'Weekly'
        ELSE 'Other'
    END AS Frequency,
    STUFF(RIGHT('000000' + CAST(s.active_start_time AS VARCHAR(6)), 6), 5, 0, ':') AS StartTime
FROM msdb.dbo.sysjobs j
LEFT JOIN msdb.dbo.sysjobschedules js ON j.job_id = js.job_id
LEFT JOIN msdb.dbo.sysschedules s ON js.schedule_id = s.schedule_id
WHERE j.name LIKE 'AktarOtomasyon%'
ORDER BY j.name

PRINT ''
PRINT '========================================='
PRINT 'Backup Jobs Created Successfully!'
PRINT '========================================='
PRINT ''
PRINT 'Backup Schedule:'
PRINT '  • Full Backup:         Daily at 2:00 AM'
PRINT '  • Differential Backup: Every 4 hours (6 AM - 6 PM)'
PRINT '  • Log Backup:          Hourly (9 AM - 6 PM)'
PRINT ''
PRINT 'Next Steps:'
PRINT '  1. Verify backup directories exist:'
PRINT '     • C:\Backups\AktarOtomasyon\Full\'
PRINT '     • C:\Backups\AktarOtomasyon\Diff\'
PRINT '     • C:\Backups\AktarOtomasyon\Log\'
PRINT ''
PRINT '  2. Set database recovery model to FULL (if not already):'
PRINT '     ALTER DATABASE AktarOtomasyon SET RECOVERY FULL'
PRINT ''
PRINT '  3. Configure Database Mail for email alerts (optional):'
PRINT '     SSMS → Management → Database Mail → Configure'
PRINT ''
PRINT '  4. Test jobs manually:'
PRINT '     EXEC msdb.dbo.sp_start_job @job_name = ''AktarOtomasyon - Full Backup'''
PRINT ''
PRINT '  5. Monitor job history:'
PRINT '     SSMS → SQL Server Agent → Jobs → Right-click → View History'
PRINT ''
PRINT '  6. Set up backup file cleanup (see docs/backup-restore-guide.md)'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
