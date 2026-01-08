/*
    Sprint 8: Differential Database Backup Script

    PURPOSE:
    Creates a differential backup of changes since the last FULL backup.
    Faster and smaller than full backups, enables point-in-time recovery.

    SCHEDULE:
    - Frequency: Every 4 hours (6 AM, 10 AM, 2 PM, 6 PM)
    - Retention: 48 hours (2 days)
    - Compression: Enabled
    - Checksum: Enabled

    USAGE:
    Manual: sqlcmd -S localhost -E -i backup_differential.sql
    Automated: Create SQL Server Agent Job (see backup_schedule.sql)

    BACKUP LOCATION:
    Default: C:\Backups\AktarOtomasyon\Diff\

    REQUIREMENTS:
    - A full backup must exist first
    - User must have BACKUP DATABASE permission
    - Database recovery model must be FULL or BULK_LOGGED

    RESTORE PROCESS:
    1. Restore last FULL backup
    2. Restore last DIFFERENTIAL backup
    3. Optionally restore transaction log backups
*/

SET NOCOUNT ON

DECLARE @BackupPath NVARCHAR(500)
DECLARE @BackupFile NVARCHAR(500)
DECLARE @DatabaseName NVARCHAR(128) = 'AktarOtomasyon'
DECLARE @BackupType NVARCHAR(20) = 'DIFF'
DECLARE @Timestamp NVARCHAR(20)
DECLARE @RetentionHours INT = 48

-- Generate timestamp for backup filename
SET @Timestamp = CONVERT(NVARCHAR(20), GETDATE(), 112) + '_' +
                 REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 108), ':', '')

-- Default backup path
SET @BackupPath = 'C:\Backups\AktarOtomasyon\Diff\'

-- Backup filename: AktarOtomasyon_DIFF_20251226_140530.bak
SET @BackupFile = @BackupPath + @DatabaseName + '_' + @BackupType + '_' + @Timestamp + '.bak'

PRINT '========================================='
PRINT 'AktarOtomasyon Differential Backup'
PRINT '========================================='
PRINT ''
PRINT 'Database: ' + @DatabaseName
PRINT 'Backup Type: ' + @BackupType
PRINT 'Timestamp: ' + @Timestamp
PRINT 'Backup File: ' + @BackupFile
PRINT 'Retention: ' + CAST(@RetentionHours AS NVARCHAR(10)) + ' hours'
PRINT ''

-- =============================================
-- STEP 1: Verify Full Backup Exists
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
    PRINT '  A differential backup requires a full backup first.'
    PRINT '  Run backup_full.sql before running differential backups.'
    PRINT ''
    RAISERROR('No full backup found', 16, 1)
    RETURN
END

PRINT '✓ Last full backup: ' + CONVERT(NVARCHAR(50), @LastFullBackup, 120)

-- =============================================
-- STEP 2: Verify Backup Directory
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
-- STEP 3: Check Changes Since Last Full Backup
-- =============================================

-- Estimate differential backup size (rough approximation)
DECLARE @ChangedPagesMB DECIMAL(18,2)

SELECT @ChangedPagesMB = SUM(modified_extent_page_count * 8.0 / 1024)
FROM sys.dm_db_file_space_usage

PRINT '✓ Estimated changes: ~' + CAST(ISNULL(@ChangedPagesMB, 0) AS NVARCHAR(20)) + ' MB'

-- =============================================
-- STEP 4: Perform Differential Backup
-- =============================================

PRINT ''
PRINT 'Starting differential backup...'
PRINT ''

DECLARE @StartTime DATETIME = GETDATE()

BEGIN TRY
    BACKUP DATABASE @DatabaseName
    TO DISK = @BackupFile
    WITH
        DIFFERENTIAL,           -- Differential backup
        COMPRESSION,            -- Enable compression
        CHECKSUM,               -- Verify integrity
        INIT,                   -- Overwrite existing file
        FORMAT,
        SKIP,
        STATS = 10,
        NAME = @DatabaseName + ' Differential Backup ' + @Timestamp,
        DESCRIPTION = 'AktarOtomasyon differential backup created on ' + CONVERT(NVARCHAR(50), GETDATE(), 120)

    DECLARE @EndTime DATETIME = GETDATE()
    DECLARE @DurationSeconds INT = DATEDIFF(SECOND, @StartTime, @EndTime)

    PRINT ''
    PRINT '========================================='
    PRINT '✓ DIFFERENTIAL BACKUP SUCCESSFUL'
    PRINT '========================================='
    PRINT ''
    PRINT 'Backup completed at: ' + CONVERT(NVARCHAR(50), @EndTime, 120)
    PRINT 'Duration: ' + CAST(@DurationSeconds AS NVARCHAR(10)) + ' seconds'
    PRINT 'Backup file: ' + @BackupFile
    PRINT ''

    -- Get actual backup size
    DECLARE @BackupSizeMB DECIMAL(18,2)

    SELECT @BackupSizeMB = CAST(backup_size / 1024.0 / 1024.0 AS DECIMAL(18,2))
    FROM msdb.dbo.backupset
    WHERE database_name = @DatabaseName
      AND type = 'I'  -- Differential backup
    ORDER BY backup_finish_date DESC

    PRINT 'Backup size: ' + CAST(@BackupSizeMB AS NVARCHAR(20)) + ' MB'
    PRINT ''

END TRY
BEGIN CATCH
    PRINT ''
    PRINT '✗ DIFFERENTIAL BACKUP FAILED'
    PRINT 'Error: ' + ERROR_MESSAGE()
    PRINT ''
    THROW
END CATCH

-- =============================================
-- STEP 5: Verify Backup
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
-- STEP 6: Cleanup Old Differential Backups
-- =============================================

PRINT ''
PRINT 'Applying retention policy...'

-- Delete differential backup history older than retention period
DELETE FROM msdb.dbo.backupset
WHERE database_name = @DatabaseName
  AND type = 'I'  -- Differential backup
  AND backup_finish_date < DATEADD(HOUR, -@RetentionHours, GETDATE())

DECLARE @DeletedCount INT = @@ROWCOUNT

IF @DeletedCount > 0
    PRINT '✓ Cleaned up ' + CAST(@DeletedCount AS NVARCHAR(10)) + ' old differential backup records'
ELSE
    PRINT '✓ No old backups to clean up'

PRINT ''
PRINT '========================================='
PRINT 'Differential Backup Complete'
PRINT '========================================='
PRINT ''
PRINT 'Recovery Chain:'
PRINT '  1. Last full backup: ' + CONVERT(NVARCHAR(50), @LastFullBackup, 120)
PRINT '  2. This differential backup: ' + CONVERT(NVARCHAR(50), GETDATE(), 120)
PRINT ''
PRINT 'To restore to current point:'
PRINT '  1. Restore full backup with NORECOVERY'
PRINT '  2. Restore this differential backup with RECOVERY'
PRINT ''

SET NOCOUNT OFF
GO
