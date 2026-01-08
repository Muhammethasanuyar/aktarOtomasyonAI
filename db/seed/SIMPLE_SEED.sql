/*
    Simple Sprint 7 Seed - Create ADMIN role and assign to admin user
    Uses only existing columns
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Creating ADMIN role and permissions'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Create ADMIN Role
-- =============================================

PRINT 'Step 1: Creating ADMIN role...'

DECLARE @admin_rol_id INT

IF NOT EXISTS (SELECT 1 FROM rol WHERE rol_adi = 'ADMIN')
BEGIN
    INSERT INTO rol (rol_adi, aktif, created_at)
    VALUES ('ADMIN', 1, GETDATE())

    SET @admin_rol_id = SCOPE_IDENTITY()
    PRINT '  ✓ Created ADMIN role'
END
ELSE
BEGIN
    SELECT @admin_rol_id = rol_id FROM rol WHERE rol_adi = 'ADMIN'
    PRINT '  ⚠ ADMIN role already exists (ID: ' + CAST(@admin_rol_id AS VARCHAR) + ')'
END

PRINT ''

-- =============================================
-- STEP 2: Assign ALL Permissions to ADMIN Role
-- =============================================

PRINT 'Step 2: Assigning permissions to ADMIN role...'

DECLARE @assigned_count INT = 0

-- Assign all existing permissions to ADMIN role
INSERT INTO rol_yetki (rol_id, yetki_id, aktif, created_at)
SELECT @admin_rol_id, yetki_id, 1, GETDATE()
FROM yetki
WHERE NOT EXISTS (
    SELECT 1 FROM rol_yetki
    WHERE rol_id = @admin_rol_id AND yetki_id = yetki.yetki_id
)

SET @assigned_count = @@ROWCOUNT

IF @assigned_count > 0
    PRINT '  ✓ Assigned ' + CAST(@assigned_count AS VARCHAR) + ' permissions to ADMIN role'
ELSE
    PRINT '  ⚠ All permissions already assigned to ADMIN role'

PRINT ''

-- =============================================
-- STEP 3: Assign ADMIN Role to Admin User
-- =============================================

PRINT 'Step 3: Assigning ADMIN role to admin user...'

DECLARE @admin_kullanici_id INT

SELECT @admin_kullanici_id = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin'

IF @admin_kullanici_id IS NULL
BEGIN
    PRINT '  ✗ ERROR: Admin user not found!'
    PRINT '  Run UPDATE_ADMIN_PASSWORD.sql first to create/update admin user'
END
ELSE
BEGIN
    IF NOT EXISTS (SELECT 1 FROM kullanici_rol
                   WHERE kullanici_id = @admin_kullanici_id AND rol_id = @admin_rol_id)
    BEGIN
        INSERT INTO kullanici_rol (kullanici_id, rol_id, aktif, created_at)
        VALUES (@admin_kullanici_id, @admin_rol_id, 1, GETDATE())

        PRINT '  ✓ Assigned ADMIN role to admin user (ID: ' + CAST(@admin_kullanici_id AS VARCHAR) + ')'
    END
    ELSE
    BEGIN
        PRINT '  ⚠ Admin user already has ADMIN role'
    END
END

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
PRINT 'Seed completed!'
PRINT '========================================='
PRINT ''
PRINT 'Summary:'
PRINT '--------'

-- Show admin user permissions
DECLARE @total_permissions INT
SELECT @total_permissions = COUNT(DISTINCT ry.yetki_id)
FROM kullanici k
INNER JOIN kullanici_rol kr ON k.kullanici_id = kr.kullanici_id
INNER JOIN rol_yetki ry ON kr.rol_id = ry.rol_id
WHERE k.kullanici_adi = 'admin'
  AND kr.aktif = 1
  AND ry.aktif = 1

PRINT '  Admin user: ' + ISNULL((SELECT kullanici_adi FROM kullanici WHERE kullanici_adi = 'admin'), 'NOT FOUND')
PRINT '  Admin role: ADMIN (ID: ' + CAST(@admin_rol_id AS VARCHAR) + ')'
PRINT '  Total permissions: ' + CAST(@total_permissions AS VARCHAR)

PRINT ''
PRINT 'Login credentials:'
PRINT '  Username: admin'
PRINT '  Password: Admin123!'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
