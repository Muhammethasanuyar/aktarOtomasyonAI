/*
    Sprint 8: Database Versioning System

    PURPOSE:
    Creates a database versioning system to track schema changes over time.
    Enables version-controlled migrations, rollback support, and audit trail.

    CREATES:
    - db_version table: Stores version history
    - sp_db_version_register: Registers new version
    - sp_db_version_current: Gets current version
    - sp_db_version_history: Lists all versions

    VERSIONING STRATEGY:
    - Semantic versioning: MAJOR.MINOR.PATCH
    - MAJOR: Breaking changes (schema changes requiring app updates)
    - MINOR: New features (backward compatible)
    - PATCH: Bug fixes, small improvements

    USAGE:
    1. Run this script to create versioning system
    2. After running any schema change, register it:
       EXEC sp_db_version_register '1.1.0', 'Added kullanici_yetki table', '001_kullanici_yetki.sql'
    3. Check current version:
       EXEC sp_db_version_current
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Creating Database Versioning System'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Create db_version Table
-- =============================================

PRINT 'Step 1: Creating db_version table...'

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'db_version')
BEGIN
    CREATE TABLE db_version
    (
        version_id INT IDENTITY(1,1) PRIMARY KEY,
        version_number NVARCHAR(20) NOT NULL UNIQUE,  -- Semantic version: 1.0.0
        description NVARCHAR(500) NOT NULL,            -- What changed
        script_name NVARCHAR(200) NULL,                -- Migration script filename
        applied_by NVARCHAR(128) NOT NULL DEFAULT SYSTEM_USER,
        applied_date DATETIME NOT NULL DEFAULT GETDATE(),
        checksum NVARCHAR(64) NULL,                    -- MD5/SHA hash of script
        success BIT NOT NULL DEFAULT 1,                 -- Did migration succeed?
        error_message NVARCHAR(MAX) NULL,              -- Error details if failed
        execution_time_ms INT NULL,                    -- How long did it take?
        rollback_script NVARCHAR(200) NULL             -- Script to undo this version

        CONSTRAINT CK_version_number CHECK (version_number LIKE '[0-9]%.[0-9]%.[0-9]%')
    )

    -- Index for fast version lookups
    CREATE NONCLUSTERED INDEX IX_db_version_applied_date
        ON db_version (applied_date DESC)

    CREATE NONCLUSTERED INDEX IX_db_version_success
        ON db_version (success, applied_date DESC)

    PRINT '  ✓ Created db_version table'
END
ELSE
BEGIN
    PRINT '  ⚠ db_version table already exists'
END

PRINT ''

-- =============================================
-- STEP 2: Create sp_db_version_register Stored Procedure
-- =============================================

PRINT 'Step 2: Creating sp_db_version_register stored procedure...'

IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'sp_db_version_register')
    DROP PROCEDURE sp_db_version_register
GO

CREATE PROCEDURE sp_db_version_register
    @version_number NVARCHAR(20),
    @description NVARCHAR(500),
    @script_name NVARCHAR(200) = NULL,
    @checksum NVARCHAR(64) = NULL,
    @rollback_script NVARCHAR(200) = NULL,
    @execution_time_ms INT = NULL
AS
BEGIN
    SET NOCOUNT ON

    -- Validate version number format (semantic versioning)
    IF @version_number NOT LIKE '[0-9]%.[0-9]%.[0-9]%'
    BEGIN
        RAISERROR('Invalid version number format. Expected: MAJOR.MINOR.PATCH (e.g., 1.0.0)', 16, 1)
        RETURN
    END

    -- Check if version already exists
    IF EXISTS (SELECT 1 FROM db_version WHERE version_number = @version_number)
    BEGIN
        PRINT '⚠ WARNING: Version ' + @version_number + ' already registered'
        PRINT 'Skipping registration to avoid duplicates'
        RETURN
    END

    -- Insert new version
    BEGIN TRY
        INSERT INTO db_version
        (
            version_number,
            description,
            script_name,
            checksum,
            applied_by,
            applied_date,
            success,
            error_message,
            execution_time_ms,
            rollback_script
        )
        VALUES
        (
            @version_number,
            @description,
            @script_name,
            @checksum,
            SYSTEM_USER,
            GETDATE(),
            1,  -- Success
            NULL,
            @execution_time_ms,
            @rollback_script
        )

        PRINT '✓ Registered version: ' + @version_number
        PRINT '  Description: ' + @description
        IF @script_name IS NOT NULL
            PRINT '  Script: ' + @script_name
    END TRY
    BEGIN CATCH
        -- Log failed version
        INSERT INTO db_version
        (
            version_number,
            description,
            script_name,
            applied_by,
            applied_date,
            success,
            error_message
        )
        VALUES
        (
            @version_number,
            @description,
            @script_name,
            SYSTEM_USER,
            GETDATE(),
            0,  -- Failed
            ERROR_MESSAGE()
        )

        PRINT '✗ Failed to register version: ' + @version_number
        PRINT '  Error: ' + ERROR_MESSAGE()

        THROW
    END CATCH
END
GO

PRINT '  ✓ Created sp_db_version_register'
PRINT ''

-- =============================================
-- STEP 3: Create sp_db_version_current Stored Procedure
-- =============================================

PRINT 'Step 3: Creating sp_db_version_current stored procedure...'

IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'sp_db_version_current')
    DROP PROCEDURE sp_db_version_current
GO

CREATE PROCEDURE sp_db_version_current
AS
BEGIN
    SET NOCOUNT ON

    SELECT TOP 1
        version_number AS CurrentVersion,
        description AS Description,
        script_name AS LastScript,
        applied_by AS AppliedBy,
        applied_date AS AppliedDate,
        execution_time_ms AS ExecutionTimeMs
    FROM db_version
    WHERE success = 1  -- Only successful versions
    ORDER BY applied_date DESC, version_id DESC
END
GO

PRINT '  ✓ Created sp_db_version_current'
PRINT ''

-- =============================================
-- STEP 4: Create sp_db_version_history Stored Procedure
-- =============================================

PRINT 'Step 4: Creating sp_db_version_history stored procedure...'

IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'sp_db_version_history')
    DROP PROCEDURE sp_db_version_history
GO

CREATE PROCEDURE sp_db_version_history
    @limit INT = 20
AS
BEGIN
    SET NOCOUNT ON

    SELECT TOP (@limit)
        version_id AS ID,
        version_number AS Version,
        description AS Description,
        script_name AS Script,
        applied_by AS AppliedBy,
        applied_date AS AppliedDate,
        CASE success
            WHEN 1 THEN 'Success'
            ELSE 'Failed'
        END AS Status,
        error_message AS Error,
        execution_time_ms AS ExecutionTimeMs
    FROM db_version
    ORDER BY applied_date DESC, version_id DESC
END
GO

PRINT '  ✓ Created sp_db_version_history'
PRINT ''

-- =============================================
-- STEP 5: Register Historical Versions
-- =============================================

PRINT 'Step 5: Registering historical database versions...'
PRINT ''

-- Register Sprint 1-7 versions (historical backfill)
EXEC sp_db_version_register '1.0.0', 'Initial database schema - Sprint 1-6', 'initial_schema.sql'
EXEC sp_db_version_register '1.1.0', 'Added security tables (rol, yetki, kullanici_rol, rol_yetki) - Sprint 7', 'sprint7_security.sql'
EXEC sp_db_version_register '1.1.1', 'Added audit logging (kullanici_log table) - Sprint 7', 'sprint7_audit.sql'
EXEC sp_db_version_register '1.2.0', 'Added database versioning system - Sprint 8', '009_sprint8_db_versioning.sql'

PRINT ''

-- =============================================
-- STEP 6: Show Current Version
-- =============================================

PRINT '========================================='
PRINT 'Current Database Version'
PRINT '========================================='
PRINT ''

EXEC sp_db_version_current

PRINT ''

-- =============================================
-- STEP 7: Show Version History
-- =============================================

PRINT '========================================='
PRINT 'Version History (Last 10)'
PRINT '========================================='
PRINT ''

EXEC sp_db_version_history @limit = 10

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
PRINT 'Database Versioning System Created!'
PRINT '========================================='
PRINT ''
PRINT 'Stored Procedures:'
PRINT '  • sp_db_version_register - Register new version'
PRINT '  • sp_db_version_current  - Get current version'
PRINT '  • sp_db_version_history  - List version history'
PRINT ''
PRINT 'Usage Examples:'
PRINT ''
PRINT '  -- Register a new version after schema change:'
PRINT '  EXEC sp_db_version_register'
PRINT '      @version_number = ''1.3.0'','
PRINT '      @description = ''Added product_image table'','
PRINT '      @script_name = ''010_product_images.sql'''
PRINT ''
PRINT '  -- Check current version:'
PRINT '  EXEC sp_db_version_current'
PRINT ''
PRINT '  -- View version history:'
PRINT '  EXEC sp_db_version_history @limit = 20'
PRINT ''
PRINT 'Best Practices:'
PRINT '  1. Always register versions after running migration scripts'
PRINT '  2. Use semantic versioning (MAJOR.MINOR.PATCH)'
PRINT '  3. Include descriptive messages in @description'
PRINT '  4. Test migrations on staging environment first'
PRINT '  5. Keep rollback scripts for critical changes'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
