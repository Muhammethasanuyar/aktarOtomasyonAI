/*
    Sprint 8: Verify Database User Permissions

    This script verifies that all database users have correct permissions.
    Use this to audit security configuration and ensure least-privilege.

    RUN AFTER: 001_create_db_users.sql and 002_grant_permissions.sql
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Verifying Database User Permissions'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Verify Server Logins Exist
-- =============================================

PRINT 'Step 1: Verifying SQL Server logins...'
PRINT ''

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'aktar_app')
    PRINT '  ✓ aktar_app login exists'
ELSE
    PRINT '  ✗ ERROR: aktar_app login NOT FOUND'

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'aktar_readonly')
    PRINT '  ✓ aktar_readonly login exists'
ELSE
    PRINT '  ✗ ERROR: aktar_readonly login NOT FOUND'

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'aktar_backup')
    PRINT '  ✓ aktar_backup login exists'
ELSE
    PRINT '  ✗ ERROR: aktar_backup login NOT FOUND'

PRINT ''

-- =============================================
-- STEP 2: Verify Database Users Exist
-- =============================================

PRINT 'Step 2: Verifying database users in AktarOtomasyon...'
PRINT ''

IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'aktar_app')
    PRINT '  ✓ aktar_app user exists'
ELSE
    PRINT '  ✗ ERROR: aktar_app user NOT FOUND'

IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'aktar_readonly')
    PRINT '  ✓ aktar_readonly user exists'
ELSE
    PRINT '  ✗ ERROR: aktar_readonly user NOT FOUND'

IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'aktar_backup')
    PRINT '  ✓ aktar_backup user exists'
ELSE
    PRINT '  ✗ ERROR: aktar_backup user NOT FOUND'

PRINT ''

-- =============================================
-- STEP 3: Verify aktar_app Permissions
-- =============================================

PRINT 'Step 3: Verifying aktar_app permissions...'
PRINT ''

-- Check database-level permissions
SELECT
    CASE
        WHEN COUNT(*) > 0 THEN '  ✓ aktar_app has EXECUTE permission'
        ELSE '  ✗ ERROR: aktar_app missing EXECUTE permission'
    END AS verification_result
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
WHERE u.name = 'aktar_app'
  AND p.permission_name = 'EXECUTE'
  AND p.state = 'G'  -- GRANT

-- Check table permissions
DECLARE @aktar_app_table_count INT

SELECT @aktar_app_table_count = COUNT(DISTINCT p.major_id)
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
WHERE u.name = 'aktar_app'
  AND p.class = 1  -- Object permissions
  AND p.permission_name IN ('SELECT', 'INSERT', 'UPDATE', 'DELETE')
  AND p.state = 'G'

PRINT '  • aktar_app has permissions on ' + CAST(@aktar_app_table_count AS VARCHAR) + ' tables'

-- Check VIEW DEFINITION
IF EXISTS (
    SELECT 1 FROM sys.database_permissions p
    INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
    WHERE u.name = 'aktar_app'
      AND p.permission_name = 'VIEW DEFINITION'
      AND p.state = 'G'
)
    PRINT '  ✓ aktar_app has VIEW DEFINITION'
ELSE
    PRINT '  ✗ WARNING: aktar_app missing VIEW DEFINITION'

PRINT ''

-- =============================================
-- STEP 4: Verify aktar_readonly Permissions
-- =============================================

PRINT 'Step 4: Verifying aktar_readonly permissions...'
PRINT ''

-- Check table SELECT permissions
DECLARE @readonly_table_count INT

SELECT @readonly_table_count = COUNT(DISTINCT p.major_id)
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
WHERE u.name = 'aktar_readonly'
  AND p.class = 1  -- Object permissions
  AND p.permission_name = 'SELECT'
  AND p.state = 'G'

PRINT '  • aktar_readonly has SELECT on ' + CAST(@readonly_table_count AS VARCHAR) + ' tables'

-- Check stored procedure EXECUTE permissions
DECLARE @readonly_sp_count INT

SELECT @readonly_sp_count = COUNT(DISTINCT p.major_id)
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
INNER JOIN sys.procedures sp ON p.major_id = sp.object_id
WHERE u.name = 'aktar_readonly'
  AND p.permission_name = 'EXECUTE'
  AND p.state = 'G'

PRINT '  • aktar_readonly has EXECUTE on ' + CAST(@readonly_sp_count AS VARCHAR) + ' stored procedures'

-- Verify DENY on write operations
IF EXISTS (
    SELECT 1 FROM sys.database_permissions p
    INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
    WHERE u.name = 'aktar_readonly'
      AND p.permission_name IN ('INSERT', 'UPDATE', 'DELETE')
      AND p.state = 'D'  -- DENY
)
    PRINT '  ✓ aktar_readonly has DENY on write operations'
ELSE
    PRINT '  ✗ WARNING: aktar_readonly missing DENY on write operations'

PRINT ''

-- =============================================
-- STEP 5: Verify aktar_backup Permissions
-- =============================================

PRINT 'Step 5: Verifying aktar_backup permissions...'
PRINT ''

-- Check db_backupoperator role membership
IF EXISTS (
    SELECT 1 FROM sys.database_role_members rm
    INNER JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
    INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
    WHERE u.name = 'aktar_backup'
      AND r.name = 'db_backupoperator'
)
    PRINT '  ✓ aktar_backup is member of db_backupoperator role'
ELSE
    PRINT '  ✗ ERROR: aktar_backup NOT in db_backupoperator role'

-- Verify DENY on data access
IF EXISTS (
    SELECT 1 FROM sys.database_permissions p
    INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
    WHERE u.name = 'aktar_backup'
      AND p.permission_name IN ('SELECT', 'INSERT', 'UPDATE', 'DELETE', 'EXECUTE')
      AND p.state = 'D'  -- DENY
)
    PRINT '  ✓ aktar_backup has DENY on data access'
ELSE
    PRINT '  ✗ WARNING: aktar_backup missing DENY on data access'

PRINT ''

-- =============================================
-- STEP 6: Detailed Permission Report
-- =============================================

PRINT 'Step 6: Detailed permission report...'
PRINT ''
PRINT '========================================='
PRINT 'aktar_app Permissions:'
PRINT '========================================='

SELECT
    CASE p.class
        WHEN 0 THEN 'Database'
        WHEN 1 THEN 'Object'
        WHEN 3 THEN 'Schema'
        ELSE CAST(p.class AS VARCHAR)
    END AS permission_scope,
    p.permission_name,
    CASE p.state
        WHEN 'G' THEN 'GRANT'
        WHEN 'D' THEN 'DENY'
        WHEN 'W' THEN 'GRANT WITH GRANT OPTION'
    END AS permission_state,
    ISNULL(OBJECT_NAME(p.major_id), 'DATABASE') AS object_name
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
WHERE u.name = 'aktar_app'
ORDER BY p.class, p.permission_name

PRINT ''
PRINT '========================================='
PRINT 'aktar_readonly Permissions:'
PRINT '========================================='

SELECT
    CASE p.class
        WHEN 0 THEN 'Database'
        WHEN 1 THEN 'Object'
        WHEN 3 THEN 'Schema'
        ELSE CAST(p.class AS VARCHAR)
    END AS permission_scope,
    p.permission_name,
    CASE p.state
        WHEN 'G' THEN 'GRANT'
        WHEN 'D' THEN 'DENY'
        WHEN 'W' THEN 'GRANT WITH GRANT OPTION'
    END AS permission_state,
    ISNULL(OBJECT_NAME(p.major_id), 'DATABASE') AS object_name
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
WHERE u.name = 'aktar_readonly'
ORDER BY p.class, p.permission_name

PRINT ''
PRINT '========================================='
PRINT 'aktar_backup Permissions:'
PRINT '========================================='

SELECT
    CASE p.class
        WHEN 0 THEN 'Database'
        WHEN 1 THEN 'Object'
        WHEN 3 THEN 'Schema'
        ELSE CAST(p.class AS VARCHAR)
    END AS permission_scope,
    p.permission_name,
    CASE p.state
        WHEN 'G' THEN 'GRANT'
        WHEN 'D' THEN 'DENY'
        WHEN 'W' THEN 'GRANT WITH GRANT OPTION'
    END AS permission_state,
    ISNULL(OBJECT_NAME(p.major_id), 'DATABASE') AS object_name
FROM sys.database_permissions p
INNER JOIN sys.database_principals u ON p.grantee_principal_id = u.principal_id
WHERE u.name = 'aktar_backup'
ORDER BY p.class, p.permission_name

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
PRINT 'Verification Complete!'
PRINT '========================================='
PRINT ''
PRINT 'Review the results above to ensure:'
PRINT '  1. All logins and users exist'
PRINT '  2. aktar_app has EXECUTE and table permissions'
PRINT '  3. aktar_readonly has SELECT only (no writes)'
PRINT '  4. aktar_backup has backup permissions only'
PRINT ''
PRINT 'If any errors found, re-run:'
PRINT '  • 001_create_db_users.sql'
PRINT '  • 002_grant_permissions.sql'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
