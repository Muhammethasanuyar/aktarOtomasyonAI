/*
    Sprint 8: Full Database Backup Script

    PURPOSE:
    Creates a full backup of the AktarOtomasyon database with compression
    and checksum validation. This is the foundation for disaster recovery.

    SCHEDULE:
    - Frequency: Daily at 2:00 AM
    - Retention: 7 daily, 4 weekly, 12 monthly backups
    - Compression: Enabled (saves ~50-70% disk space)
    - Checksum: Enabled (detects corruption)

    USAGE:
    Manual: sqlcmd -S localhost -E -i backup_full.sql
    Automated: Create SQL Server Agent Job (see backup_schedule.sql)

    BACKUP LOCATION:
    Default: C:\Backups\AktarOtomasyon\Full\
    Change @BackupPath variable to customize location.

    RETENTION POLICY:
    - Daily backups: Keep last 7 days
    - Weekly backups: Keep last 4 weeks (Sunday backups)
    - Monthly backups: Keep last 12 months (1st of month backups)

    REQUIREMENTS:
    - User must have BACKUP DATABASE permission
    - Backup directory must exist
    - Sufficient disk space (estimate: DB size × 0.3 for compressed backup)
*/

SET NOCOUNT ON

DECLARE @BackupPath NVARCHAR(500)
DECLARE @BackupFile NVARCHAR(500)
DECLARE @DatabaseName NVARCHAR(128) = 'AktarOtomasyon'
DECLARE @BackupType NVARCHAR(20) = 'FULL'
DECLARE @Timestamp NVARCHAR(20)
DECLARE @RetentionDays INT = 7
DECLARE @SQL NVARCHAR(MAX)

-- Generate timestamp for backup filename (yyyyMMdd_HHmmss)
SET @Timestamp = CONVERT(NVARCHAR(20), GETDATE(), 112) + '_' +
                 REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), ':', '')

-- Default backup path (change this to match your environment)
-- Examples:
--   Windows: C:\Backups\AktarOtomasyon\Full\
--   Network: \\backup-server\sql-backups\AktarOtomasyon\Full\
SET @BackupPath = 'C:\Backups\AktarOtomasyon\Full\'

-- Backup filename format: AktarOtomasyon_FULL_20251226_140530.bak
SET @BackupFile = @BackupPath + @DatabaseName + '_' + @BackupType + '_' + @Timestamp + '.bak'

PRINT '========================================='
PRINT 'AktarOtomasyon Full Database Backup'
PRINT '========================================='
PRINT ''
PRINT 'Database: ' + @DatabaseName
PRINT 'Backup Type: ' + @BackupType
PRINT 'Timestamp: ' + @Timestamp
PRINT 'Backup File: ' + @BackupFile
PRINT 'Compression: ON'
PRINT 'Checksum: ON'
PRINT ''

-- =============================================
-- STEP 1: Verify Database Exists
-- =============================================

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = @DatabaseName)
BEGIN
    PRINT '✗ ERROR: Database ' + @DatabaseName + ' not found!'
    PRINT ''
    RAISERROR('Database not found', 16, 1)
    RETURN
END

PRINT '✓ Database verified'

-- =============================================
-- STEP 2: Verify Backup Directory Exists
-- =============================================

-- Check if backup path exists (using xp_fileexist)
DECLARE @FileExists INT
DECLARE @PathExists INT

EXEC master.dbo.xp_fileexist @BackupPath, @FileExists OUTPUT, @PathExists OUTPUT

IF @PathExists = 0
BEGIN
    PRINT '✗ ERROR: Backup directory does not exist: ' + @BackupPath
    PRINT ''
    PRINT 'Create the directory using:'
    PRINT '  mkdir "' + @BackupPath + '"'
    PRINT ''
    RAISERROR('Backup directory not found', 16, 1)
    RETURN
END

PRINT '✓ Backup directory verified'

-- =============================================
-- STEP 3: Check Database Size
-- =============================================

DECLARE @DatabaseSizeMB DECIMAL(18,2)

SELECT @DatabaseSizeMB = SUM(size * 8.0 / 1024)
FROM sys.master_files
WHERE database_id = DB_ID(@DatabaseName)

PRINT '✓ Database size: ' + CAST(@DatabaseSizeMB AS NVARCHAR(20)) + ' MB'
PRINT '  Estimated backup size: ~' + CAST(@DatabaseSizeMB * 0.3 AS NVARCHAR(20)) + ' MB (compressed)'

-- =============================================
-- STEP 4: Perform Full Backup
-- =============================================

PRINT ''
PRINT 'Starting full backup...'
PRINT ''

DECLARE @StartTime DATETIME = GETDATE()

BEGIN TRY
    BACKUP DATABASE @DatabaseName
    TO DISK = @BackupFile
    WITH
        COMPRESSION,            -- Enable compression (reduces backup size by 50-70%)
        CHECKSUM,               -- Verify backup integrity
        INIT,                   -- Overwrite existing file if name conflicts
        FORMAT,                 -- Initialize backup media
        SKIP,                   -- Skip checking backup expiration
        STATS = 10,             -- Show progress every 10%
        NAME = @DatabaseName + ' Full Backup ' + @Timestamp,
        DESCRIPTION = 'AktarOtomasyon full database backup created on ' + CONVERT(NVARCHAR(50), GETDATE(), 120)

    DECLARE @EndTime DATETIME = GETDATE()
    DECLARE @DurationSeconds INT = DATEDIFF(SECOND, @StartTime, @EndTime)

    PRINT ''
    PRINT '========================================='
    PRINT '✓ BACKUP SUCCESSFUL'
    PRINT '========================================='
    PRINT ''
    PRINT 'Backup completed at: ' + CONVERT(NVARCHAR(50), @EndTime, 120)
    PRINT 'Duration: ' + CAST(@DurationSeconds AS NVARCHAR(10)) + ' seconds'
    PRINT 'Backup file: ' + @BackupFile
    PRINT ''

    -- Get actual backup file size
    DECLARE @BackupSizeMB DECIMAL(18,2)

    SELECT @BackupSizeMB = CAST(backup_size / 1024.0 / 1024.0 AS DECIMAL(18,2))
    FROM msdb.dbo.backupset
    WHERE database_name = @DatabaseName
      AND type = 'D'  -- Full backup
    ORDER BY backup_finish_date DESC

    PRINT 'Actual backup size: ' + CAST(@BackupSizeMB AS NVARCHAR(20)) + ' MB'
    PRINT 'Compression ratio: ' + CAST((@BackupSizeMB / @DatabaseSizeMB) * 100 AS NVARCHAR(10)) + '%'
    PRINT ''

END TRY
BEGIN CATCH
    PRINT ''
    PRINT '========================================='
    PRINT '✗ BACKUP FAILED'
    PRINT '========================================='
    PRINT ''
    PRINT 'Error Number: ' + CAST(ERROR_NUMBER() AS NVARCHAR(10))
    PRINT 'Error Message: ' + ERROR_MESSAGE()
    PRINT ''

    -- Re-throw error
    THROW
END CATCH

-- =============================================
-- STEP 5: Cleanup Old Backups (Retention Policy)
-- =============================================

PRINT 'Applying retention policy (keep last ' + CAST(@RetentionDays AS NVARCHAR(10)) + ' days)...'
PRINT ''

-- Delete backup history older than retention period
EXEC msdb.dbo.sp_delete_backuphistory @oldest_date = DATEADD(DAY, -@RetentionDays, GETDATE())

PRINT '✓ Old backup history cleaned up'

-- Note: File system cleanup requires xp_cmdshell or PowerShell script
-- See backup-restore-guide.md for cleanup automation

-- =============================================
-- STEP 6: Verify Backup
-- =============================================

PRINT ''
PRINT 'Verifying backup integrity...'
PRINT ''

BEGIN TRY
    RESTORE VERIFYONLY
    FROM DISK = @BackupFile
    WITH CHECKSUM

    PRINT '✓ Backup verification successful'
    PRINT '  Backup is valid and restorable'
END TRY
BEGIN CATCH
    PRINT '✗ WARNING: Backup verification failed!'
    PRINT '  Error: ' + ERROR_MESSAGE()
    PRINT '  The backup file may be corrupted'
END CATCH

PRINT ''
PRINT '========================================='
PRINT 'Backup Process Complete'
PRINT '========================================='
PRINT ''
PRINT 'Next Steps:'
PRINT '  1. Verify backup file exists: ' + @BackupFile
PRINT '  2. Test restore on staging environment'
PRINT '  3. Copy backup to offsite location'
PRINT '  4. Update backup log/documentation'
PRINT ''
PRINT 'For automated backups, run: backup_schedule.sql'
PRINT ''

SET NOCOUNT OFF
GO
