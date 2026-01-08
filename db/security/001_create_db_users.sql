/*
    Sprint 8: Create Least-Privilege Database Users

    This script creates three SQL Server logins and database users:
    1. aktar_app - Application user with execute and data access
    2. aktar_readonly - Read-only user for reporting/analytics
    3. aktar_backup - Backup operator user

    SECURITY REQUIREMENTS:
    - Strong passwords (min 16 characters, mixed case, numbers, symbols)
    - CHECK_EXPIRATION=ON, CHECK_POLICY=ON
    - Passwords should be changed quarterly
    - Store actual passwords in environment variables or Azure Key Vault

    BEFORE RUNNING:
    1. Replace placeholder passwords with strong passwords
    2. Save actual passwords securely (Key Vault, password manager)
    3. Run on master database first, then switch to AktarOtomasyon
*/

USE [master]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Creating SQL Server Logins'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Create aktar_app Login (Application User)
-- =============================================

PRINT 'Step 1: Creating aktar_app login...'

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'aktar_app')
BEGIN
    -- IMPORTANT: Replace 'CHANGE_THIS_PASSWORD_16CHARS!' with a strong password
    -- Example strong password: AktarApp2025!Secure#Pass
    CREATE LOGIN aktar_app
    WITH PASSWORD = 'CHANGE_THIS_PASSWORD_16CHARS!',
         DEFAULT_DATABASE = [AktarOtomasyon],
         CHECK_EXPIRATION = ON,
         CHECK_POLICY = ON

    PRINT '  ✓ Created aktar_app login'
    PRINT '  ⚠ IMPORTANT: Change the default password immediately!'
END
ELSE
BEGIN
    PRINT '  ⚠ aktar_app login already exists'
END

PRINT ''

-- =============================================
-- STEP 2: Create aktar_readonly Login (Read-Only User)
-- =============================================

PRINT 'Step 2: Creating aktar_readonly login...'

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'aktar_readonly')
BEGIN
    -- IMPORTANT: Replace 'CHANGE_THIS_PASSWORD_16CHARS!' with a strong password
    -- Example strong password: AktarRead2025!View#Only
    CREATE LOGIN aktar_readonly
    WITH PASSWORD = 'CHANGE_THIS_PASSWORD_16CHARS!',
         DEFAULT_DATABASE = [AktarOtomasyon],
         CHECK_EXPIRATION = ON,
         CHECK_POLICY = ON

    PRINT '  ✓ Created aktar_readonly login'
    PRINT '  ⚠ IMPORTANT: Change the default password immediately!'
END
ELSE
BEGIN
    PRINT '  ⚠ aktar_readonly login already exists'
END

PRINT ''

-- =============================================
-- STEP 3: Create aktar_backup Login (Backup Operator)
-- =============================================

PRINT 'Step 3: Creating aktar_backup login...'

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'aktar_backup')
BEGIN
    -- IMPORTANT: Replace 'CHANGE_THIS_PASSWORD_16CHARS!' with a strong password
    -- Example strong password: AktarBackup2025!Safe#Store
    CREATE LOGIN aktar_backup
    WITH PASSWORD = 'CHANGE_THIS_PASSWORD_16CHARS!',
         DEFAULT_DATABASE = [AktarOtomasyon],
         CHECK_EXPIRATION = ON,
         CHECK_POLICY = ON

    PRINT '  ✓ Created aktar_backup login'
    PRINT '  ⚠ IMPORTANT: Change the default password immediately!'
END
ELSE
BEGIN
    PRINT '  ⚠ aktar_backup login already exists'
END

PRINT ''

-- =============================================
-- STEP 4: Create Database Users
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Step 4: Creating database users in AktarOtomasyon...'

-- Create aktar_app user
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'aktar_app')
BEGIN
    CREATE USER aktar_app FOR LOGIN aktar_app
    PRINT '  ✓ Created aktar_app database user'
END
ELSE
BEGIN
    PRINT '  ⚠ aktar_app database user already exists'
END

-- Create aktar_readonly user
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'aktar_readonly')
BEGIN
    CREATE USER aktar_readonly FOR LOGIN aktar_readonly
    PRINT '  ✓ Created aktar_readonly database user'
END
ELSE
BEGIN
    PRINT '  ⚠ aktar_readonly database user already exists'
END

-- Create aktar_backup user
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'aktar_backup')
BEGIN
    CREATE USER aktar_backup FOR LOGIN aktar_backup
    PRINT '  ✓ Created aktar_backup database user'
END
ELSE
BEGIN
    PRINT '  ⚠ aktar_backup database user already exists'
END

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
PRINT 'User Creation Complete!'
PRINT '========================================='
PRINT ''
PRINT 'Created 3 SQL Server logins:'
PRINT '  1. aktar_app (Application User)'
PRINT '  2. aktar_readonly (Read-Only User)'
PRINT '  3. aktar_backup (Backup Operator)'
PRINT ''
PRINT 'Next Steps:'
PRINT '  1. Run 002_grant_permissions.sql to assign permissions'
PRINT '  2. Run 003_verify_permissions.sql to verify setup'
PRINT '  3. Change all default passwords to strong passwords'
PRINT '  4. Store passwords in Azure Key Vault or secure password manager'
PRINT '  5. Update .env file with DB_USER and DB_PASSWORD'
PRINT ''
PRINT 'Security Reminders:'
PRINT '  - Change passwords quarterly (every 90 days)'
PRINT '  - Never commit passwords to source control'
PRINT '  - Use environment variables or Key Vault for password storage'
PRINT '  - Monitor login failures in SQL Server logs'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
