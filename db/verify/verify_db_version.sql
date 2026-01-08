/*
    Sprint 8: Verify Database Version System

    PURPOSE:
    Verifies that the database versioning system is properly installed
    and functioning. Checks table structure, stored procedures, and
    version history integrity.

    USAGE:
    sqlcmd -S localhost -E -d AktarOtomasyon -i verify_db_version.sql

    WHAT IT CHECKS:
    1. db_version table exists with correct schema
    2. Required stored procedures exist
    3. Version history is valid
    4. Indexes are in place
    5. Constraints are enforced
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Verifying Database Version System'
PRINT '========================================='
PRINT ''

DECLARE @ErrorCount INT = 0

-- =============================================
-- CHECK 1: db_version Table Exists
-- =============================================

PRINT 'Check 1: Verifying db_version table...'

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'db_version')
BEGIN
    PRINT '  ✓ db_version table exists'

    -- Check required columns
    DECLARE @RequiredColumns TABLE (column_name NVARCHAR(128))
    INSERT INTO @RequiredColumns VALUES
        ('version_id'),
        ('version_number'),
        ('description'),
        ('script_name'),
        ('applied_by'),
        ('applied_date'),
        ('checksum'),
        ('success'),
        ('error_message'),
        ('execution_time_ms'),
        ('rollback_script')

    DECLARE @MissingColumns TABLE (column_name NVARCHAR(128))

    INSERT INTO @MissingColumns
    SELECT r.column_name
    FROM @RequiredColumns r
    WHERE NOT EXISTS (
        SELECT 1
        FROM sys.columns c
        WHERE c.object_id = OBJECT_ID('db_version')
          AND c.name = r.column_name
    )

    IF EXISTS (SELECT 1 FROM @MissingColumns)
    BEGIN
        PRINT '  ✗ ERROR: Missing columns in db_version table:'
        SELECT '    - ' + column_name FROM @MissingColumns
        SET @ErrorCount = @ErrorCount + 1
    END
    ELSE
    BEGIN
        PRINT '  ✓ All required columns present'
    END
END
ELSE
BEGIN
    PRINT '  ✗ ERROR: db_version table NOT FOUND'
    PRINT '    Run: db/schema/009_sprint8_db_versioning.sql'
    SET @ErrorCount = @ErrorCount + 1
END

PRINT ''

-- =============================================
-- CHECK 2: Stored Procedures Exist
-- =============================================

PRINT 'Check 2: Verifying stored procedures...'

DECLARE @RequiredProcs TABLE (proc_name NVARCHAR(128))
INSERT INTO @RequiredProcs VALUES
    ('sp_db_version_register'),
    ('sp_db_version_current'),
    ('sp_db_version_history')

DECLARE @MissingProcs TABLE (proc_name NVARCHAR(128))

INSERT INTO @MissingProcs
SELECT r.proc_name
FROM @RequiredProcs r
WHERE NOT EXISTS (
    SELECT 1
    FROM sys.procedures p
    WHERE p.name = r.proc_name
)

IF EXISTS (SELECT 1 FROM @MissingProcs)
BEGIN
    PRINT '  ✗ ERROR: Missing stored procedures:'
    SELECT '    - ' + proc_name FROM @MissingProcs
    SET @ErrorCount = @ErrorCount + 1
END
ELSE
BEGIN
    PRINT '  ✓ sp_db_version_register exists'
    PRINT '  ✓ sp_db_version_current exists'
    PRINT '  ✓ sp_db_version_history exists'
END

PRINT ''

-- =============================================
-- CHECK 3: Indexes Exist
-- =============================================

PRINT 'Check 3: Verifying indexes...'

IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('db_version')
      AND name = 'IX_db_version_applied_date'
)
    PRINT '  ✓ IX_db_version_applied_date exists'
ELSE
BEGIN
    PRINT '  ⚠ WARNING: IX_db_version_applied_date index missing (performance may be affected)'
END

IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('db_version')
      AND name = 'IX_db_version_success'
)
    PRINT '  ✓ IX_db_version_success exists'
ELSE
BEGIN
    PRINT '  ⚠ WARNING: IX_db_version_success index missing'
END

PRINT ''

-- =============================================
-- CHECK 4: Constraints Exist
-- =============================================

PRINT 'Check 4: Verifying constraints...'

-- Check UNIQUE constraint on version_number
IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('db_version')
      AND name LIKE '%version_number%'
      AND is_unique = 1
)
    PRINT '  ✓ UNIQUE constraint on version_number exists'
ELSE
BEGIN
    PRINT '  ✗ ERROR: Missing UNIQUE constraint on version_number'
    SET @ErrorCount = @ErrorCount + 1
END

-- Check CHECK constraint on version_number format
IF EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE parent_object_id = OBJECT_ID('db_version')
      AND name = 'CK_version_number'
)
    PRINT '  ✓ CHECK constraint on version_number format exists'
ELSE
BEGIN
    PRINT '  ⚠ WARNING: Missing CHECK constraint on version_number format'
END

PRINT ''

-- =============================================
-- CHECK 5: Version History Integrity
-- =============================================

PRINT 'Check 5: Verifying version history integrity...'

-- Check if any versions exist
DECLARE @VersionCount INT

SELECT @VersionCount = COUNT(*)
FROM db_version

IF @VersionCount > 0
BEGIN
    PRINT '  ✓ Version history contains ' + CAST(@VersionCount AS NVARCHAR(10)) + ' entries'

    -- Check for duplicate version numbers
    DECLARE @DuplicateCount INT

    SELECT @DuplicateCount = COUNT(*)
    FROM (
        SELECT version_number, COUNT(*) AS cnt
        FROM db_version
        GROUP BY version_number
        HAVING COUNT(*) > 1
    ) AS duplicates

    IF @DuplicateCount > 0
    BEGIN
        PRINT '  ✗ ERROR: Found ' + CAST(@DuplicateCount AS NVARCHAR(10)) + ' duplicate version numbers!'
        SELECT '    - Duplicate version: ' + version_number
        FROM db_version
        GROUP BY version_number
        HAVING COUNT(*) > 1
        SET @ErrorCount = @ErrorCount + 1
    END
    ELSE
    BEGIN
        PRINT '  ✓ No duplicate version numbers'
    END

    -- Check for invalid version numbers
    DECLARE @InvalidVersions INT

    SELECT @InvalidVersions = COUNT(*)
    FROM db_version
    WHERE version_number NOT LIKE '[0-9]%.[0-9]%.[0-9]%'
      AND version_number NOT LIKE '[0-9]%.[0-9]%.[0-9]%-rollback'

    IF @InvalidVersions > 0
    BEGIN
        PRINT '  ⚠ WARNING: Found ' + CAST(@InvalidVersions AS NVARCHAR(10)) + ' invalid version numbers'
        SELECT '    - Invalid: ' + version_number FROM db_version
        WHERE version_number NOT LIKE '[0-9]%.[0-9]%.[0-9]%'
          AND version_number NOT LIKE '[0-9]%.[0-9]%.[0-9]%-rollback'
    END
    ELSE
    BEGIN
        PRINT '  ✓ All version numbers valid (semantic versioning)'
    END

    -- Check for failed versions
    DECLARE @FailedVersions INT

    SELECT @FailedVersions = COUNT(*)
    FROM db_version
    WHERE success = 0

    IF @FailedVersions > 0
    BEGIN
        PRINT '  ⚠ WARNING: Found ' + CAST(@FailedVersions AS NVARCHAR(10)) + ' failed migrations'
        SELECT '    - Failed: ' + version_number + ' - ' + ISNULL(error_message, 'No error message')
        FROM db_version
        WHERE success = 0
    END
    ELSE
    BEGIN
        PRINT '  ✓ All migrations successful'
    END
END
ELSE
BEGIN
    PRINT '  ⚠ WARNING: No version history found (empty db_version table)'
    PRINT '    Consider registering current database version'
END

PRINT ''

-- =============================================
-- CHECK 6: Test Stored Procedures
-- =============================================

PRINT 'Check 6: Testing stored procedures...'

-- Test sp_db_version_current
BEGIN TRY
    DECLARE @TestTable TABLE (
        CurrentVersion NVARCHAR(20),
        Description NVARCHAR(500),
        LastScript NVARCHAR(200),
        AppliedBy NVARCHAR(128),
        AppliedDate DATETIME,
        ExecutionTimeMs INT
    )

    INSERT INTO @TestTable
    EXEC sp_db_version_current

    PRINT '  ✓ sp_db_version_current executed successfully'
END TRY
BEGIN CATCH
    PRINT '  ✗ ERROR: sp_db_version_current failed'
    PRINT '    ' + ERROR_MESSAGE()
    SET @ErrorCount = @ErrorCount + 1
END CATCH

-- Test sp_db_version_history
BEGIN TRY
    EXEC sp_db_version_history @limit = 5
    PRINT '  ✓ sp_db_version_history executed successfully'
END TRY
BEGIN CATCH
    PRINT '  ✗ ERROR: sp_db_version_history failed'
    PRINT '    ' + ERROR_MESSAGE()
    SET @ErrorCount = @ErrorCount + 1
END CATCH

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
IF @ErrorCount = 0
BEGIN
    PRINT '✓ ALL CHECKS PASSED'
    PRINT '========================================='
    PRINT ''
    PRINT 'Database versioning system is properly configured!'
    PRINT ''
    PRINT 'Current Version:'
    EXEC sp_db_version_current
    PRINT ''
    PRINT 'Recent History:'
    EXEC sp_db_version_history @limit = 5
END
ELSE
BEGIN
    PRINT '✗ VERIFICATION FAILED'
    PRINT '========================================='
    PRINT ''
    PRINT 'Found ' + CAST(@ErrorCount AS NVARCHAR(10)) + ' error(s)'
    PRINT ''
    PRINT 'To fix:'
    PRINT '  1. Review errors above'
    PRINT '  2. Run: db/schema/009_sprint8_db_versioning.sql'
    PRINT '  3. Re-run this verification script'
END

PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
