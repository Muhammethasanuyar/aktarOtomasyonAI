/*
    Sprint 8: Full Database Restore Script

    PURPOSE:
    Restores the AktarOtomasyon database from backup files.
    Used for disaster recovery, migration, or creating test environments.

    RESTORE STRATEGIES:
    1. Full restore only (restore to last full backup)
    2. Full + Differential (restore to last differential backup)
    3. Full + Differential + Logs (point-in-time restore)

    USAGE:
    ⚠ WARNING: This script will OVERWRITE the existing database!
    Test on staging environment first!

    Manual: sqlcmd -S localhost -E -i restore_full.sql

    REQUIREMENTS:
    - User must have CREATE DATABASE or ALTER DATABASE permission
    - Backup files must be accessible
    - Sufficient disk space for database files
    - No active connections to database (will force disconnect)

    BEFORE RUNNING:
    1. Update @BackupPath variables with your backup locations
    2. Update @FullBackupFile with actual backup filename
    3. Review @DataPath and @LogPath for database file locations
    4. Test restore on non-production environment first!
*/

SET NOCOUNT ON

-- =============================================
-- CONFIGURATION VARIABLES
-- =============================================

DECLARE @DatabaseName NVARCHAR(128) = 'AktarOtomasyon'
DECLARE @RestoreMode NVARCHAR(20) = 'FULL_ONLY'  -- Options: FULL_ONLY, FULL_DIFF, FULL_DIFF_LOGS
DECLARE @PointInTime DATETIME = NULL  -- For point-in-time restore, set to specific datetime

-- Backup file paths (UPDATE THESE!)
DECLARE @FullBackupPath NVARCHAR(500) = 'C:\Backups\AktarOtomasyon\Full\'
DECLARE @DiffBackupPath NVARCHAR(500) = 'C:\Backups\AktarOtomasyon\Diff\'
DECLARE @LogBackupPath NVARCHAR(500) = 'C:\Backups\AktarOtomasyon\Log\'

-- Specific backup files to restore (UPDATE THESE!)
-- Examples:
--   @FullBackupFile = 'AktarOtomasyon_FULL_20251226_020000.bak'
--   @DiffBackupFile = 'AktarOtomasyon_DIFF_20251226_140000.bak'
DECLARE @FullBackupFile NVARCHAR(500) = NULL  -- Will auto-detect latest if NULL
DECLARE @DiffBackupFile NVARCHAR(500) = NULL  -- Will auto-detect latest if NULL

-- Database file locations (UPDATE THESE for your environment!)
DECLARE @DataPath NVARCHAR(500) = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\'
DECLARE @LogPath NVARCHAR(500) = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\'

PRINT '========================================='
PRINT 'AktarOtomasyon Database Restore'
PRINT '========================================='
PRINT ''
PRINT 'Database: ' + @DatabaseName
PRINT 'Restore Mode: ' + @RestoreMode
PRINT 'Data Path: ' + @DataPath
PRINT 'Log Path: ' + @LogPath
PRINT ''

-- =============================================
-- STEP 1: WARNING AND CONFIRMATION
-- =============================================

PRINT '⚠⚠⚠ WARNING ⚠⚠⚠'
PRINT 'This script will OVERWRITE the existing database!'
PRINT 'All current data will be LOST and replaced with backup data.'
PRINT ''
PRINT 'Press Ctrl+C NOW to cancel.'
PRINT 'Continuing in 10 seconds...'
PRINT ''

WAITFOR DELAY '00:00:10'

PRINT 'Proceeding with restore...'
PRINT ''

-- =============================================
-- STEP 2: Auto-Detect Latest Backup Files
-- =============================================

IF @FullBackupFile IS NULL
BEGIN
    SELECT TOP 1 @FullBackupFile = physical_device_name
    FROM msdb.dbo.backupset bs
    INNER JOIN msdb.dbo.backupmediafamily bmf ON bs.media_set_id = bmf.media_set_id
    WHERE bs.database_name = @DatabaseName
      AND bs.type = 'D'  -- Full backup
    ORDER BY bs.backup_finish_date DESC

    IF @FullBackupFile IS NULL
    BEGIN
        PRINT '✗ ERROR: No full backup found in backup history'
        PRINT '  Please specify @FullBackupFile manually'
        RAISERROR('No full backup found', 16, 1)
        RETURN
    END

    PRINT '✓ Auto-detected full backup: ' + @FullBackupFile
END

IF @RestoreMode IN ('FULL_DIFF', 'FULL_DIFF_LOGS') AND @DiffBackupFile IS NULL
BEGIN
    SELECT TOP 1 @DiffBackupFile = physical_device_name
    FROM msdb.dbo.backupset bs
    INNER JOIN msdb.dbo.backupmediafamily bmf ON bs.media_set_id = bmf.media_set_id
    WHERE bs.database_name = @DatabaseName
      AND bs.type = 'I'  -- Differential backup
    ORDER BY bs.backup_finish_date DESC

    IF @DiffBackupFile IS NOT NULL
        PRINT '✓ Auto-detected differential backup: ' + @DiffBackupFile
    ELSE
        PRINT '⚠ No differential backup found'
END

-- =============================================
-- STEP 3: Verify Backup Files Exist
-- =============================================

DECLARE @FileExists INT

EXEC master.dbo.xp_fileexist @FullBackupFile, @FileExists OUTPUT

IF @FileExists = 0
BEGIN
    PRINT '✗ ERROR: Full backup file not found: ' + @FullBackupFile
    RAISERROR('Backup file not found', 16, 1)
    RETURN
END

PRINT '✓ Full backup file verified'

-- =============================================
-- STEP 4: Disconnect All Users
-- =============================================

PRINT ''
PRINT 'Disconnecting active users...'

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = @DatabaseName)
BEGIN
    DECLARE @KillSQL NVARCHAR(MAX) = ''

    SELECT @KillSQL = @KillSQL + 'KILL ' + CAST(session_id AS NVARCHAR(10)) + '; '
    FROM sys.dm_exec_sessions
    WHERE database_id = DB_ID(@DatabaseName)
      AND session_id <> @@SPID

    IF LEN(@KillSQL) > 0
    BEGIN
        EXEC sp_executesql @KillSQL
        PRINT '✓ Disconnected active users'
    END
    ELSE
    BEGIN
        PRINT '✓ No active connections'
    END
END

-- =============================================
-- STEP 5: Read Backup File List
-- =============================================

PRINT ''
PRINT 'Reading backup file contents...'

CREATE TABLE #FileList
(
    LogicalName NVARCHAR(128),
    PhysicalName NVARCHAR(260),
    Type CHAR(1),
    FileGroupName NVARCHAR(128),
    Size NUMERIC(20,0),
    MaxSize NUMERIC(20,0),
    FileID BIGINT,
    CreateLSN NUMERIC(25,0),
    DropLSN NUMERIC(25,0),
    UniqueID UNIQUEIDENTIFIER,
    ReadOnlyLSN NUMERIC(25,0),
    ReadWriteLSN NUMERIC(25,0),
    BackupSizeInBytes BIGINT,
    SourceBlockSize INT,
    FileGroupID INT,
    LogGroupGUID UNIQUEIDENTIFIER,
    DifferentialBaseLSN NUMERIC(25,0),
    DifferentialBaseGUID UNIQUEIDENTIFIER,
    IsReadOnly BIT,
    IsPresent BIT,
    TDEThumbprint VARBINARY(32),
    SnapshotURL NVARCHAR(360)
)

INSERT INTO #FileList
EXEC ('RESTORE FILELISTONLY FROM DISK = ''' + @FullBackupFile + '''')

-- Display file list
SELECT
    LogicalName,
    Type,
    CAST(Size / 1024.0 / 1024.0 AS DECIMAL(18,2)) AS SizeMB
FROM #FileList

PRINT '✓ Backup file contains ' + CAST((SELECT COUNT(*) FROM #FileList) AS NVARCHAR(10)) + ' files'

-- =============================================
-- STEP 6: Restore Full Backup
-- =============================================

PRINT ''
PRINT '========================================='
PRINT 'RESTORING FULL BACKUP'
PRINT '========================================='
PRINT ''

DECLARE @StartTime DATETIME = GETDATE()
DECLARE @RestoreSQL NVARCHAR(MAX)

-- Build RESTORE command with file relocations
SET @RestoreSQL = 'RESTORE DATABASE [' + @DatabaseName + '] FROM DISK = ''' + @FullBackupFile + ''' WITH '

-- Add MOVE clauses for each file
SELECT @RestoreSQL = @RestoreSQL +
    'MOVE ''' + LogicalName + ''' TO ''' +
    CASE Type
        WHEN 'D' THEN @DataPath + LogicalName + '.mdf'
        WHEN 'L' THEN @LogPath + LogicalName + '.ldf'
    END + ''', '
FROM #FileList

-- Add restore options
IF @RestoreMode IN ('FULL_DIFF', 'FULL_DIFF_LOGS')
    SET @RestoreSQL = @RestoreSQL + 'NORECOVERY, '  -- More restores to follow
ELSE
    SET @RestoreSQL = @RestoreSQL + 'RECOVERY, '  -- Final restore

SET @RestoreSQL = @RestoreSQL +
    'REPLACE, ' +  -- Overwrite existing database
    'STATS = 10'

PRINT 'Executing:'
PRINT @RestoreSQL
PRINT ''

BEGIN TRY
    EXEC sp_executesql @RestoreSQL

    DECLARE @EndTime DATETIME = GETDATE()
    DECLARE @Duration INT = DATEDIFF(SECOND, @StartTime, @EndTime)

    PRINT ''
    PRINT '✓ Full backup restored successfully'
    PRINT '  Duration: ' + CAST(@Duration AS NVARCHAR(10)) + ' seconds'
    PRINT ''
END TRY
BEGIN CATCH
    PRINT ''
    PRINT '✗ Full backup restore FAILED'
    PRINT '  Error: ' + ERROR_MESSAGE()
    PRINT ''

    DROP TABLE #FileList
    THROW
END CATCH

-- =============================================
-- STEP 7: Restore Differential Backup (if requested)
-- =============================================

IF @RestoreMode IN ('FULL_DIFF', 'FULL_DIFF_LOGS') AND @DiffBackupFile IS NOT NULL
BEGIN
    PRINT '========================================='
    PRINT 'RESTORING DIFFERENTIAL BACKUP'
    PRINT '========================================='
    PRINT ''

    SET @StartTime = GETDATE()

    SET @RestoreSQL = 'RESTORE DATABASE [' + @DatabaseName + '] FROM DISK = ''' + @DiffBackupFile + ''' WITH '

    IF @RestoreMode = 'FULL_DIFF_LOGS'
        SET @RestoreSQL = @RestoreSQL + 'NORECOVERY, STATS = 10'
    ELSE
        SET @RestoreSQL = @RestoreSQL + 'RECOVERY, STATS = 10'

    BEGIN TRY
        EXEC sp_executesql @RestoreSQL

        SET @EndTime = GETDATE()
        SET @Duration = DATEDIFF(SECOND, @StartTime, @EndTime)

        PRINT ''
        PRINT '✓ Differential backup restored successfully'
        PRINT '  Duration: ' + CAST(@Duration AS NVARCHAR(10)) + ' seconds'
        PRINT ''
    END TRY
    BEGIN CATCH
        PRINT ''
        PRINT '✗ Differential backup restore FAILED'
        PRINT '  Error: ' + ERROR_MESSAGE()
        PRINT ''

        DROP TABLE #FileList
        THROW
    END CATCH
END

-- =============================================
-- STEP 8: Restore Transaction Logs (if requested)
-- =============================================

-- Note: Transaction log restore is complex and requires careful ordering
-- For production use, consider using SQL Server's native restore wizard
-- or third-party backup tools

IF @RestoreMode = 'FULL_DIFF_LOGS'
BEGIN
    PRINT '========================================='
    PRINT 'TRANSACTION LOG RESTORE'
    PRINT '========================================='
    PRINT ''
    PRINT '⚠ Transaction log restore not fully automated in this script'
    PRINT '  Requires manual specification of log backup files in chronological order'
    PRINT ''
    PRINT 'To restore logs manually:'
    PRINT '  1. List all log backups after differential/full backup'
    PRINT '  2. Restore each log file in sequence with NORECOVERY'
    PRINT '  3. Final log restore with RECOVERY or STOPAT parameter'
    PRINT ''
    PRINT 'Example:'
    PRINT '  RESTORE LOG AktarOtomasyon FROM DISK = ''..._LOG_1.trn'' WITH NORECOVERY'
    PRINT '  RESTORE LOG AktarOtomasyon FROM DISK = ''..._LOG_2.trn'' WITH NORECOVERY'
    PRINT '  RESTORE DATABASE AktarOtomasyon WITH RECOVERY'
    PRINT ''
END

-- =============================================
-- CLEANUP
-- =============================================

DROP TABLE #FileList

-- =============================================
-- STEP 9: Verify Restore
-- =============================================

PRINT '========================================='
PRINT 'VERIFYING RESTORE'
PRINT '========================================='
PRINT ''

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = @DatabaseName AND state_desc = 'ONLINE')
BEGIN
    PRINT '✓ Database is ONLINE'

    -- Check table count
    DECLARE @TableCount INT
    SELECT @TableCount = COUNT(*)
    FROM sys.tables
    WHERE is_ms_shipped = 0

    PRINT '✓ Found ' + CAST(@TableCount AS NVARCHAR(10)) + ' user tables'

    -- Check row counts for key tables
    PRINT ''
    PRINT 'Sample table row counts:'
    EXEC sp_executesql N'
        SELECT
            t.name AS TableName,
            SUM(p.rows) AS RowCount
        FROM sys.tables t
        INNER JOIN sys.partitions p ON t.object_id = p.object_id
        WHERE t.is_ms_shipped = 0
          AND p.index_id IN (0,1)
        GROUP BY t.name
        ORDER BY SUM(p.rows) DESC
    '

    PRINT ''
    PRINT '✓ Restore verification complete'
END
ELSE
BEGIN
    PRINT '✗ ERROR: Database is not online!'
    PRINT '  Current state: ' + (SELECT state_desc FROM sys.databases WHERE name = @DatabaseName)
END

PRINT ''
PRINT '========================================='
PRINT 'RESTORE COMPLETE'
PRINT '========================================='
PRINT ''
PRINT 'Next Steps:'
PRINT '  1. Run database integrity check: DBCC CHECKDB(AktarOtomasyon)'
PRINT '  2. Verify application connectivity'
PRINT '  3. Test critical functionality'
PRINT '  4. Review audit logs for restore event'
PRINT ''
PRINT 'If this is a production restore:'
PRINT '  - Notify users that system is back online'
PRINT '  - Document restore in incident log'
PRINT '  - Review root cause of data loss'
PRINT ''

SET NOCOUNT OFF
GO
