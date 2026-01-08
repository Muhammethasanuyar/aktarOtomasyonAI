/*
    Sprint 8: Grant Permissions to Database Users

    This script assigns least-privilege permissions to each user:

    1. aktar_app (Application User):
       - EXECUTE on ALL stored procedures
       - SELECT/INSERT/UPDATE/DELETE on ALL user tables
       - VIEW DEFINITION (for schema introspection)

    2. aktar_readonly (Read-Only User):
       - SELECT on ALL user tables
       - EXECUTE on read-only stored procedures (sp_*_listele, sp_*_getir)
       - VIEW DEFINITION (for reporting tools)

    3. aktar_backup (Backup Operator):
       - BACKUP DATABASE permission
       - BACKUP LOG permission
       - db_backupoperator role membership

    RUN AFTER: 001_create_db_users.sql
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Granting Permissions to Database Users'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Grant Permissions to aktar_app (Application User)
-- =============================================

PRINT 'Step 1: Granting permissions to aktar_app...'

-- Grant EXECUTE on ALL stored procedures
GRANT EXECUTE TO aktar_app
PRINT '  ✓ Granted EXECUTE on all stored procedures'

-- Grant SELECT, INSERT, UPDATE, DELETE on ALL user tables
DECLARE @sql NVARCHAR(MAX) = ''

-- Generate GRANT statements for all user tables
SELECT @sql = @sql +
    'GRANT SELECT, INSERT, UPDATE, DELETE ON [' + s.name + '].[' + t.name + '] TO aktar_app;' + CHAR(13)
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = 'dbo'  -- Only user tables in dbo schema
  AND t.is_ms_shipped = 0  -- Exclude system tables

-- Execute the generated grants
IF LEN(@sql) > 0
BEGIN
    EXEC sp_executesql @sql
    PRINT '  ✓ Granted SELECT/INSERT/UPDATE/DELETE on all user tables'
END

-- Grant VIEW DEFINITION (allows reading schema metadata)
GRANT VIEW DEFINITION TO aktar_app
PRINT '  ✓ Granted VIEW DEFINITION'

PRINT ''

-- =============================================
-- STEP 2: Grant Permissions to aktar_readonly (Read-Only User)
-- =============================================

PRINT 'Step 2: Granting permissions to aktar_readonly...'

-- Grant SELECT on ALL user tables
SET @sql = ''

SELECT @sql = @sql +
    'GRANT SELECT ON [' + s.name + '].[' + t.name + '] TO aktar_readonly;' + CHAR(13)
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = 'dbo'
  AND t.is_ms_shipped = 0

IF LEN(@sql) > 0
BEGIN
    EXEC sp_executesql @sql
    PRINT '  ✓ Granted SELECT on all user tables'
END

-- Grant EXECUTE on read-only stored procedures (sp_*_listele, sp_*_getir)
SET @sql = ''

SELECT @sql = @sql +
    'GRANT EXECUTE ON [' + s.name + '].[' + p.name + '] TO aktar_readonly;' + CHAR(13)
FROM sys.procedures p
INNER JOIN sys.schemas s ON p.schema_id = s.schema_id
WHERE s.name = 'dbo'
  AND (p.name LIKE 'sp_%_listele' OR p.name LIKE 'sp_%_getir')

IF LEN(@sql) > 0
BEGIN
    EXEC sp_executesql @sql
    PRINT '  ✓ Granted EXECUTE on read-only stored procedures (listele/getir)'
END
ELSE
BEGIN
    PRINT '  ⚠ No read-only stored procedures found (sp_*_listele, sp_*_getir)'
END

-- Grant VIEW DEFINITION
GRANT VIEW DEFINITION TO aktar_readonly
PRINT '  ✓ Granted VIEW DEFINITION'

PRINT ''

-- =============================================
-- STEP 3: Grant Permissions to aktar_backup (Backup Operator)
-- =============================================

PRINT 'Step 3: Granting permissions to aktar_backup...'

-- Add to db_backupoperator role (allows backup/restore operations)
ALTER ROLE db_backupoperator ADD MEMBER aktar_backup
PRINT '  ✓ Added to db_backupoperator role'

-- Grant BACKUP DATABASE permission at server level
-- NOTE: This must be run in master database
USE [master]
GO

GRANT BACKUP DATABASE TO aktar_backup
GRANT BACKUP LOG TO aktar_backup
PRINT '  ✓ Granted BACKUP DATABASE and BACKUP LOG permissions'

USE [AktarOtomasyon]
GO

PRINT ''

-- =============================================
-- STEP 4: Deny Dangerous Permissions (Defense in Depth)
-- =============================================

PRINT 'Step 4: Denying dangerous permissions (defense in depth)...'

-- Deny dangerous permissions to aktar_app (just in case)
DENY ALTER ANY DATABASE TO aktar_app
DENY CREATE DATABASE TO aktar_app
DENY DROP ANY DATABASE TO aktar_app
DENY CONTROL TO aktar_app
PRINT '  ✓ Denied dangerous permissions to aktar_app'

-- Deny all write operations to aktar_readonly
DENY INSERT TO aktar_readonly
DENY UPDATE TO aktar_readonly
DENY DELETE TO aktar_readonly
DENY ALTER TO aktar_readonly
DENY CREATE TABLE TO aktar_readonly
DENY DROP TABLE TO aktar_readonly
PRINT '  ✓ Denied write permissions to aktar_readonly'

-- Deny data access to aktar_backup (backup only)
DENY SELECT TO aktar_backup
DENY INSERT TO aktar_backup
DENY UPDATE TO aktar_backup
DENY DELETE TO aktar_backup
DENY EXECUTE TO aktar_backup
PRINT '  ✓ Denied data access to aktar_backup'

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
PRINT 'Permission Grant Complete!'
PRINT '========================================='
PRINT ''
PRINT 'Permissions Summary:'
PRINT ''
PRINT '1. aktar_app (Application User):'
PRINT '   • EXECUTE on all stored procedures'
PRINT '   • SELECT/INSERT/UPDATE/DELETE on all tables'
PRINT '   • VIEW DEFINITION for schema introspection'
PRINT '   • DENIED: ALTER DATABASE, CREATE DATABASE, CONTROL'
PRINT ''
PRINT '2. aktar_readonly (Read-Only User):'
PRINT '   • SELECT on all user tables'
PRINT '   • EXECUTE on sp_*_listele, sp_*_getir procedures'
PRINT '   • VIEW DEFINITION for reporting'
PRINT '   • DENIED: INSERT, UPDATE, DELETE, ALTER, CREATE, DROP'
PRINT ''
PRINT '3. aktar_backup (Backup Operator):'
PRINT '   • BACKUP DATABASE permission'
PRINT '   • BACKUP LOG permission'
PRINT '   • db_backupoperator role membership'
PRINT '   • DENIED: All data access (SELECT, INSERT, UPDATE, DELETE, EXECUTE)'
PRINT ''
PRINT 'Next Steps:'
PRINT '  1. Run 003_verify_permissions.sql to verify all permissions'
PRINT '  2. Test application connection with aktar_app user'
PRINT '  3. Test read-only reporting with aktar_readonly user'
PRINT '  4. Test backup operations with aktar_backup user'
PRINT '  5. Update connection strings in .env file'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
