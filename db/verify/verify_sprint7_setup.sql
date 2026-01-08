/*
    Sprint 7 Backend - Verification Script
    Run this script to verify that Sprint 7 Backend is correctly set up

    Expected Results:
    - All tables exist
    - All stored procedures exist
    - Admin user exists with correct password hash
    - Admin role exists with permissions
    - All checks should show "PASS"
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Sprint 7 Backend - Verification Script'
PRINT '========================================='
PRINT ''

DECLARE @pass_count INT = 0
DECLARE @fail_count INT = 0

-- =============================================
-- CHECK 1: Tables Exist
-- =============================================
PRINT 'CHECK 1: Verifying tables exist...'

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'kullanici')
BEGIN
    PRINT '  ✓ PASS: Table [kullanici] exists'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Table [kullanici] missing'
    SET @fail_count = @fail_count + 1
END

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'rol')
BEGIN
    PRINT '  ✓ PASS: Table [rol] exists'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Table [rol] missing'
    SET @fail_count = @fail_count + 1
END

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'yetki')
BEGIN
    PRINT '  ✓ PASS: Table [yetki] exists'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Table [yetki] missing'
    SET @fail_count = @fail_count + 1
END

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'audit_log')
BEGIN
    PRINT '  ✓ PASS: Table [audit_log] exists'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Table [audit_log] missing'
    SET @fail_count = @fail_count + 1
END

PRINT ''

-- =============================================
-- CHECK 2: Stored Procedures Exist
-- =============================================
PRINT 'CHECK 2: Verifying stored procedures exist...'

DECLARE @sp_count INT
SELECT @sp_count = COUNT(*)
FROM sys.procedures
WHERE name LIKE 'sp_kullanici%'
   OR name LIKE 'sp_rol%'
   OR name LIKE 'sp_yetki%'
   OR name LIKE 'sp_ekran_yetki%'
   OR name LIKE 'sp_audit%'

IF @sp_count >= 23
BEGIN
    PRINT '  ✓ PASS: ' + CAST(@sp_count AS VARCHAR) + ' stored procedures found (expected: 23+)'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Only ' + CAST(@sp_count AS VARCHAR) + ' stored procedures found (expected: 23+)'
    SET @fail_count = @fail_count + 1

    PRINT '  Missing procedures:'
    PRINT '    Expected: sp_kullanici_getir_login, sp_kullanici_kaydet, sp_kullanici_listele, etc.'
END

PRINT ''

-- =============================================
-- CHECK 3: Admin User Exists
-- =============================================
PRINT 'CHECK 3: Verifying admin user exists...'

IF EXISTS (SELECT 1 FROM kullanici WHERE kullanici_adi = 'admin')
BEGIN
    DECLARE @admin_aktif BIT
    DECLARE @admin_hash NVARCHAR(512)
    DECLARE @admin_salt NVARCHAR(256)

    SELECT
        @admin_aktif = aktif,
        @admin_hash = parola_hash,
        @admin_salt = parola_salt
    FROM kullanici
    WHERE kullanici_adi = 'admin'

    IF @admin_aktif = 1
    BEGIN
        PRINT '  ✓ PASS: Admin user exists and is active'
        SET @pass_count = @pass_count + 1
    END
    ELSE
    BEGIN
        PRINT '  ✗ FAIL: Admin user exists but is not active'
        SET @fail_count = @fail_count + 1
    END

    IF @admin_hash IS NOT NULL AND LEN(@admin_hash) > 0
    BEGIN
        PRINT '  ✓ PASS: Admin user has password hash'
        SET @pass_count = @pass_count + 1
    END
    ELSE
    BEGIN
        PRINT '  ✗ FAIL: Admin user has no password hash'
        SET @fail_count = @fail_count + 1
    END

    IF @admin_salt IS NOT NULL AND LEN(@admin_salt) > 0
    BEGIN
        PRINT '  ✓ PASS: Admin user has password salt'
        SET @pass_count = @pass_count + 1
    END
    ELSE
    BEGIN
        PRINT '  ✗ FAIL: Admin user has no password salt'
        SET @fail_count = @fail_count + 1
    END
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Admin user does not exist'
    PRINT '    Run: db/seed/008_sprint7_security_seed.sql'
    SET @fail_count = @fail_count + 3
END

PRINT ''

-- =============================================
-- CHECK 4: Admin Role Exists
-- =============================================
PRINT 'CHECK 4: Verifying ADMIN role exists...'

IF EXISTS (SELECT 1 FROM rol WHERE rol_adi = 'ADMIN')
BEGIN
    DECLARE @admin_rol_aktif BIT
    SELECT @admin_rol_aktif = aktif FROM rol WHERE rol_adi = 'ADMIN'

    IF @admin_rol_aktif = 1
    BEGIN
        PRINT '  ✓ PASS: ADMIN role exists and is active'
        SET @pass_count = @pass_count + 1
    END
    ELSE
    BEGIN
        PRINT '  ✗ FAIL: ADMIN role exists but is not active'
        SET @fail_count = @fail_count + 1
    END
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: ADMIN role does not exist'
    PRINT '    Run: db/seed/008_sprint7_security_seed.sql'
    SET @fail_count = @fail_count + 1
END

PRINT ''

-- =============================================
-- CHECK 5: Admin Role Has Permissions
-- =============================================
PRINT 'CHECK 5: Verifying ADMIN role has permissions...'

DECLARE @admin_permission_count INT
SELECT @admin_permission_count = COUNT(*)
FROM rol r
INNER JOIN rol_yetki ry ON r.rol_id = ry.rol_id
WHERE r.rol_adi = 'ADMIN'

IF @admin_permission_count >= 4
BEGIN
    PRINT '  ✓ PASS: ADMIN role has ' + CAST(@admin_permission_count AS VARCHAR) + ' permissions (expected: 4+)'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: ADMIN role has only ' + CAST(@admin_permission_count AS VARCHAR) + ' permissions (expected: 4+)'
    PRINT '    Run: db/seed/008_sprint7_security_seed.sql'
    SET @fail_count = @fail_count + 1
END

PRINT ''

-- =============================================
-- CHECK 6: Admin User Has Admin Role
-- =============================================
PRINT 'CHECK 6: Verifying admin user has ADMIN role...'

IF EXISTS (
    SELECT 1
    FROM kullanici k
    INNER JOIN kullanici_rol kr ON k.kullanici_id = kr.kullanici_id
    INNER JOIN rol r ON kr.rol_id = r.rol_id
    WHERE k.kullanici_adi = 'admin' AND r.rol_adi = 'ADMIN'
)
BEGIN
    PRINT '  ✓ PASS: Admin user is assigned to ADMIN role'
    SET @pass_count = @pass_count + 1
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Admin user is not assigned to ADMIN role'
    PRINT '    Run: db/seed/008_sprint7_security_seed.sql'
    SET @fail_count = @fail_count + 1
END

PRINT ''

-- =============================================
-- CHECK 7: Permissions Exist
-- =============================================
PRINT 'CHECK 7: Verifying permissions exist...'

DECLARE @permission_count INT
SELECT @permission_count = COUNT(*) FROM yetki

IF @permission_count >= 4
BEGIN
    PRINT '  ✓ PASS: ' + CAST(@permission_count AS VARCHAR) + ' permissions found (expected: 4+)'
    SET @pass_count = @pass_count + 1

    -- List permissions
    PRINT '  Permissions:'
    DECLARE @yetki_kod NVARCHAR(50), @yetki_adi NVARCHAR(100)
    DECLARE permission_cursor CURSOR FOR
        SELECT yetki_kod, yetki_adi FROM yetki ORDER BY yetki_kod

    OPEN permission_cursor
    FETCH NEXT FROM permission_cursor INTO @yetki_kod, @yetki_adi

    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT '    - ' + @yetki_kod + ': ' + @yetki_adi
        FETCH NEXT FROM permission_cursor INTO @yetki_kod, @yetki_adi
    END

    CLOSE permission_cursor
    DEALLOCATE permission_cursor
END
ELSE
BEGIN
    PRINT '  ✗ FAIL: Only ' + CAST(@permission_count AS VARCHAR) + ' permissions found (expected: 4+)'
    PRINT '    Run: db/seed/008_sprint7_security_seed.sql'
    SET @fail_count = @fail_count + 1
END

PRINT ''

-- =============================================
-- CHECK 8: Test Login Stored Procedure
-- =============================================
PRINT 'CHECK 8: Testing login stored procedure...'

BEGIN TRY
    DECLARE @test_table TABLE (
        kullanici_id INT,
        kullanici_adi NVARCHAR(50),
        ad_soyad NVARCHAR(100),
        email NVARCHAR(100),
        parola_hash NVARCHAR(512),
        parola_salt NVARCHAR(256),
        aktif BIT,
        son_giris_tarih DATETIME
    )

    INSERT INTO @test_table
    EXEC sp_kullanici_getir_login @kullanici_adi = 'admin'

    IF EXISTS (SELECT 1 FROM @test_table)
    BEGIN
        PRINT '  ✓ PASS: sp_kullanici_getir_login works correctly'
        SET @pass_count = @pass_count + 1
    END
    ELSE
    BEGIN
        PRINT '  ✗ FAIL: sp_kullanici_getir_login returned no data'
        SET @fail_count = @fail_count + 1
    END
END TRY
BEGIN CATCH
    PRINT '  ✗ FAIL: sp_kullanici_getir_login threw an error'
    PRINT '    Error: ' + ERROR_MESSAGE()
    SET @fail_count = @fail_count + 1
END CATCH

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================
PRINT '========================================='
PRINT 'VERIFICATION SUMMARY'
PRINT '========================================='
PRINT ''
PRINT '  Total Checks: ' + CAST((@pass_count + @fail_count) AS VARCHAR)
PRINT '  Passed: ' + CAST(@pass_count AS VARCHAR)
PRINT '  Failed: ' + CAST(@fail_count AS VARCHAR)
PRINT ''

IF @fail_count = 0
BEGIN
    PRINT '✅ ALL CHECKS PASSED!'
    PRINT ''
    PRINT 'Sprint 7 Backend is correctly configured.'
    PRINT ''
    PRINT 'You can now login with:'
    PRINT '  Username: admin'
    PRINT '  Password: Admin123!'
    PRINT ''
    PRINT '⚠️  IMPORTANT: Change the admin password after first login!'
END
ELSE
BEGIN
    PRINT '❌ SOME CHECKS FAILED'
    PRINT ''
    PRINT 'Please review the failed checks above and:'
    PRINT '  1. Make sure all schema scripts are run'
    PRINT '  2. Make sure all stored procedures are created'
    PRINT '  3. Make sure seed data script is run'
    PRINT ''
    PRINT 'Refer to: docs/SPRINT7_SETUP.md for detailed instructions'
END

PRINT '========================================='
PRINT ''

SET NOCOUNT OFF
