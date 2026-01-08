/*
    Sprint 8: Transaction Log Backup Script

    PURPOSE:
    Backs up the transaction log to enable point-in-time recovery.
    Essential for minimizing data loss in disaster recovery scenarios.

    SCHEDULE:
    - Frequency: Hourly during business hours (9 AM - 6 PM)
    - Retention: 7 days
    - Compression: Enabled
    - Checksum: Enabled

    USAGE:
    Manual: sqlcmd -S localhost -E -i backup_log.sql
    Automated: Create SQL Server Agent Job (see backup_schedule.sql)

    BACKUP LOCATION:
    Default: C:\Backups\AktarOtomasyon\Log\

    REQUIREMENTS:
    - Database recovery model must be FULL (not SIMPLE)
    - User must have BACKUP LOG permission
    - Full backup must exist first

    RECOVERY MODEL:
    If database is in SIMPLE recovery model, transaction logs are
    automatically truncated and cannot be backed up. Change to FULL:
        ALTER DATABASE AktarOtomasyon SET RECOVERY FULL
*/

SET NOCOUNT ON

DECLARE @BackupPath NVARCHAR(500)
DECLARE @BackupFile NVARCHAR(500)
DECLARE @DatabaseName NVARCHAR(128) = 'AktarOtomasyon'
DECLARE @BackupType NVARCHAR(20) = 'LOG'
DECLARE @Timestamp NVARCHAR(20)
DECLARE @RetentionDays INT = 7

-- Generate timestamp
SET @Timestamp = CONVERT(NVARCHAR(20), GETDATE(), 112) + '_' +
                 REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), ':', '')

-- Default backup path
SET @BackupPath = 'C:\Backups\AktarOtomasyon\Log\'

-- Backup filename: AktarOtomasyon_LOG_20251226_140530.trn
SET @BackupFile = @BackupPath + @DatabaseName + '_' + @BackupType + '_' + @Timestamp + '.trn'

PRINT '========================================='
PRINT 'AktarOtomasyon Transaction Log Backup'
PRINT '========================================='
PRINT ''
PRINT 'Database: ' + @DatabaseName
PRINT 'Backup Type: Transaction Log'
PRINT 'Timestamp: ' + @Timestamp
PRINT 'Backup File: ' + @BackupFile
PRINT 'Retention: ' + CAST(@RetentionDays AS NVARCHAR(10)) + ' days'
PRINT ''

-- =============================================
-- STEP 1: Verify Recovery Model
-- =============================================

DECLARE @RecoveryModel NVARCHAR(60)

SELECT @RecoveryModel = recovery_model_desc
FROM sys.databases
WHERE name = @DatabaseName

IF @RecoveryModel = 'SIMPLE'
BEGIN
    PRINT '✗ ERROR: Database is in SIMPLE recovery model'
    PRINT '  Transaction log backups require FULL or BULK_LOGGED recovery model'
    PRINT ''
    PRINT 'To change recovery model, run:'
    PRINT '  ALTER DATABASE ' + @DatabaseName + ' SET RECOVERY FULL'
    PRINT ''
    RAISERROR('Database in SIMPLE recovery model', 16, 1)
    RETURN
END

PRINT '✓ Recovery model: ' + @RecoveryModel

-- =============================================
-- STEP 2: Verify Full Backup Exists
-- =============================================

DECLARE @LastFullBackup DATETIME

SELECT TOP 1 @LastFullBackup = backup_finish_date
FROM msdb.dbo.backupset
WHERE database_name = @DatabaseName
  AND type = 'D'  -- Full backup
ORDER BY backup_finish_date DESC

IF @LastFullBackup IS NULL
BEGIN
    PRINT '✗ ERROR: No full backup found!'
    PRINT '  Transaction log backups require a full backup first.'
    PRINT '  Run backup_full.sql before running log backups.'
    PRINT ''
    RAISERROR('No full backup found', 16, 1)
    RETURN
END

PRINT '✓ Last full backup: ' + CONVERT(NVARCHAR(50), @LastFullBackup, 120)

-- =============================================
-- STEP 3: Verify Backup Directory
-- =============================================

DECLARE @FileExists INT
DECLARE @PathExists INT

EXEC master.dbo.xp_fileexist @BackupPath, @FileExists OUTPUT, @PathExists OUTPUT

IF @PathExists = 0
BEGIN
    PRINT '✗ ERROR: Backup directory does not exist: ' + @BackupPath
    PRINT '  Create with: mkdir "' + @BackupPath + '"'
    RAISERROR('Backup directory not found', 16, 1)
    RETURN
END

PRINT '✓ Backup directory verified'

-- =============================================
-- STEP 4: Check Transaction Log Size
-- =============================================

DECLARE @LogSizeMB DECIMAL(18,2)
DECLARE @LogUsedPercent DECIMAL(5,2)

SELECT
    @LogSizeMB = CAST(SUM(size * 8.0 / 1024) AS DECIMAL(18,2)),
    @LogUsedPercent = CAST(SUM(FILEPROPERTY(name, 'SpaceUsed') * 100.0 / size) / COUNT(*) AS DECIMAL(5,2))
FROM sys.database_files
WHERE type = 1  -- Log files

PRINT '✓ Transaction log size: ' + CAST(@LogSizeMB AS NVARCHAR(20)) + ' MB'
PRINT '  Log used: ' + CAST(@LogUsedPercent AS NVARCHAR(10)) + '%'

IF @LogUsedPercent > 80
    PRINT '  ⚠ WARNING: Transaction log is ' + CAST(@LogUsedPercent AS NVARCHAR(10)) + '% full!'

-- =============================================
-- STEP 5: Perform Transaction Log Backup
-- =============================================

PRINT ''
PRINT 'Starting transaction log backup...'
PRINT ''

DECLARE @StartTime DATETIME = GETDATE()

BEGIN TRY
    BACKUP LOG @DatabaseName
    TO DISK = @BackupFile
    WITH
        COMPRESSION,
        CHECKSUM,
        INIT,
        FORMAT,
        SKIP,
        STATS = 10,
        NAME = @DatabaseName + ' Transaction Log Backup ' + @Timestamp,
        DESCRIPTION = 'AktarOtomasyon transaction log backup created on ' + CONVERT(NVARCHAR(50), GETDATE(), 120)

    DECLARE @EndTime DATETIME = GETDATE()
    DECLARE @DurationSeconds INT = DATEDIFF(SECOND, @StartTime, @EndTime)

    PRINT ''
    PRINT '========================================='
    PRINT '✓ LOG BACKUP SUCCESSFUL'
    PRINT '========================================='
    PRINT ''
    PRINT 'Backup completed at: ' + CONVERT(NVARCHAR(50), @EndTime, 120)
    PRINT 'Duration: ' + CAST(@DurationSeconds AS NVARCHAR(10)) + ' seconds'
    PRINT 'Backup file: ' + @BackupFile
    PRINT ''

    -- Get backup size
    DECLARE @BackupSizeMB DECIMAL(18,2)

    SELECT @BackupSizeMB = CAST(backup_size / 1024.0 / 1024.0 AS DECIMAL(18,2))
    FROM msdb.dbo.backupset
    WHERE database_name = @DatabaseName
      AND type = 'L'  -- Log backup
    ORDER BY backup_finish_date DESC

    PRINT 'Backup size: ' + CAST(@BackupSizeMB AS NVARCHAR(20)) + ' MB'

    -- Check log space freed
    DECLARE @LogUsedAfter DECIMAL(5,2)

    SELECT @LogUsedAfter = CAST(SUM(FILEPROPERTY(name, 'SpaceUsed') * 100.0 / size) / COUNT(*) AS DECIMAL(5,2))
    FROM sys.database_files
    WHERE type = 1

    PRINT 'Log used after backup: ' + CAST(@LogUsedAfter AS NVARCHAR(10)) + '%'
    PRINT '  Log space reclaimed: ' + CAST(@LogUsedPercent - @LogUsedAfter AS NVARCHAR(10)) + '%'
    PRINT ''

END TRY
BEGIN CATCH
    PRINT ''
    PRINT '✗ LOG BACKUP FAILED'
    PRINT 'Error: ' + ERROR_MESSAGE()
    PRINT ''
    THROW
END CATCH

-- =============================================
-- STEP 6: Verify Backup
-- =============================================

PRINT 'Verifying backup integrity...'
PRINT ''

BEGIN TRY
    RESTORE VERIFYONLY
    FROM DISK = @BackupFile
    WITH CHECKSUM

    PRINT '✓ Backup verification successful'
END TRY
BEGIN CATCH
    PRINT '✗ WARNING: Backup verification failed!'
    PRINT '  Error: ' + ERROR_MESSAGE()
END CATCH

-- =============================================
-- STEP 7: Cleanup Old Log Backups
-- =============================================

PRINT ''
PRINT 'Applying retention policy...'

DELETE FROM msdb.dbo.backupset
WHERE database_name = @DatabaseName
  AND type = 'L'  -- Log backup
  AND backup_finish_date < DATEADD(DAY, -@RetentionDays, GETDATE())

DECLARE @DeletedCount INT = @@ROWCOUNT

IF @DeletedCount > 0
    PRINT '✓ Cleaned up ' + CAST(@DeletedCount AS NVARCHAR(10)) + ' old log backup records'
ELSE
    PRINT '✓ No old backups to clean up'

PRINT ''
PRINT '========================================='
PRINT 'Transaction Log Backup Complete'
PRINT '========================================='
PRINT ''
PRINT 'Point-in-Time Recovery Available:'
PRINT '  From: ' + CONVERT(NVARCHAR(50), @LastFullBackup, 120)
PRINT '  To:   ' + CONVERT(NVARCHAR(50), GETDATE(), 120)
PRINT ''
PRINT 'Recovery Process:'
PRINT '  1. Restore full backup with NORECOVERY'
PRINT '  2. Restore differential backup (if any) with NORECOVERY'
PRINT '  3. Restore log backups in sequence with STOPAT parameter'
PRINT '  4. Final restore with RECOVERY'
PRINT ''

SET NOCOUNT OFF
GO
