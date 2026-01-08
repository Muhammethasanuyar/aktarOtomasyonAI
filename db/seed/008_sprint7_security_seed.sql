/*
    Sprint 7 Security Seed Data
    - Create initial permissions (Template/Settings modules)
    - Create ADMIN role with all permissions
    - Create admin user (DEFAULT PASSWORD: Admin123! - MUST CHANGE)
    - Map permissions to screens (ekran_yetki)

    CRITICAL SECURITY WARNING:
    The admin user is created with a SAMPLE password hash.
    After first login, IMMEDIATELY change the admin password!
*/

USE [AktarOtomasyon]
GO

PRINT '========================================='
PRINT 'Sprint 7: Security Seed Data'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Insert Permissions
-- =============================================

PRINT 'Step 1: Creating permissions...'

-- Template Module Permissions
IF NOT EXISTS (SELECT 1 FROM yetki WHERE yetki_kod = 'TEMPLATE_VIEW')
BEGIN
    INSERT INTO yetki (yetki_kod, yetki_adi, aciklama, modul, created_at)
    VALUES
        ('TEMPLATE_VIEW', 'Şablon Görüntüleme', 'Şablonları ve versiyonları görüntüleme yetkisi', 'Template', GETDATE())
    PRINT '  ✓ Created permission: TEMPLATE_VIEW'
END

IF NOT EXISTS (SELECT 1 FROM yetki WHERE yetki_kod = 'TEMPLATE_MANAGE')
BEGIN
    INSERT INTO yetki (yetki_kod, yetki_adi, aciklama, modul, created_at)
    VALUES
        ('TEMPLATE_MANAGE', 'Şablon Yönetme', 'Şablon oluşturma, düzenleme, silme ve versiyon yükleme yetkisi', 'Template', GETDATE())
    PRINT '  ✓ Created permission: TEMPLATE_MANAGE'
END

IF NOT EXISTS (SELECT 1 FROM yetki WHERE yetki_kod = 'TEMPLATE_APPROVE')
BEGIN
    INSERT INTO yetki (yetki_kod, yetki_adi, aciklama, modul, created_at)
    VALUES
        ('TEMPLATE_APPROVE', 'Şablon Onaylama', 'Şablon versiyonlarını aktif etme (onaylama) yetkisi', 'Template', GETDATE())
    PRINT '  ✓ Created permission: TEMPLATE_APPROVE'
END

-- Settings Module Permissions
IF NOT EXISTS (SELECT 1 FROM yetki WHERE yetki_kod = 'SETTINGS_MANAGE')
BEGIN
    INSERT INTO yetki (yetki_kod, yetki_adi, aciklama, modul, created_at)
    VALUES
        ('SETTINGS_MANAGE', 'Sistem Ayarları Yönetme', 'Sistem ayarlarını görüntüleme ve düzenleme yetkisi', 'Settings', GETDATE())
    PRINT '  ✓ Created permission: SETTINGS_MANAGE'
END

PRINT ''

-- =============================================
-- STEP 2: Create ADMIN Role
-- =============================================

PRINT 'Step 2: Creating ADMIN role...'

DECLARE @admin_rol_id INT

IF NOT EXISTS (SELECT 1 FROM rol WHERE rol_adi = 'ADMIN')
BEGIN
    INSERT INTO rol (rol_adi, aciklama, aktif, created_at)
    VALUES ('ADMIN', 'Sistem Yöneticisi - Tüm sistem yetkilerine sahip', 1, GETDATE())

    SET @admin_rol_id = SCOPE_IDENTITY()
    PRINT '  ✓ Created ADMIN role'
END
ELSE
BEGIN
    SELECT @admin_rol_id = rol_id FROM rol WHERE rol_adi = 'ADMIN'
    PRINT '  ⚠ ADMIN role already exists'
END

PRINT ''

-- =============================================
-- STEP 3: Assign ALL Permissions to ADMIN Role
-- =============================================

PRINT 'Step 3: Assigning permissions to ADMIN role...'

DECLARE @assigned_count INT = 0

INSERT INTO rol_yetki (rol_id, yetki_id, created_at)
SELECT @admin_rol_id, yetki_id, GETDATE()
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
-- STEP 4: Create Admin User
-- =============================================

PRINT 'Step 4: Creating admin user...'

DECLARE @admin_kullanici_id INT

-- REAL PBKDF2 Hash for "Admin123!"
-- Generated using PasswordHelper with 10,000 iterations
-- Salt and hash are valid and can be used for authentication
DECLARE @sample_salt NVARCHAR(256) = 'ZcBkoj9TcYsnsnwluLFB/nd1nxRpK8fyXCfdqrk1FC4='
DECLARE @sample_hash NVARCHAR(512) = 'mzb2n7nDDEmIm0e9K7A8/8GY9NwK05kPvu2YrfQZixY='

IF NOT EXISTS (SELECT 1 FROM kullanici WHERE kullanici_adi = 'admin')
BEGIN
    INSERT INTO kullanici
        (kullanici_adi, ad_soyad, email, parola_hash, parola_salt, parola_iterasyon, aktif, created_at)
    VALUES
        ('admin', 'System Administrator', 'admin@aktar.local', @sample_hash, @sample_salt, 10000, 1, GETDATE())

    SET @admin_kullanici_id = SCOPE_IDENTITY()
    PRINT '  ✓ Created admin user'
    PRINT '  ⚠ DEFAULT PASSWORD: Admin123! (CHANGE IMMEDIATELY AFTER FIRST LOGIN!)'
END
ELSE
BEGIN
    SELECT @admin_kullanici_id = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin'
    PRINT '  ⚠ Admin user already exists'
END

PRINT ''

-- =============================================
-- STEP 5: Assign ADMIN Role to Admin User
-- =============================================

PRINT 'Step 5: Assigning ADMIN role to admin user...'

IF NOT EXISTS (SELECT 1 FROM kullanici_rol
               WHERE kullanici_id = @admin_kullanici_id AND rol_id = @admin_rol_id)
BEGIN
    INSERT INTO kullanici_rol (kullanici_id, rol_id, created_at)
    VALUES (@admin_kullanici_id, @admin_rol_id, GETDATE())

    PRINT '  ✓ Assigned ADMIN role to admin user'
END
ELSE
BEGIN
    PRINT '  ⚠ Admin user already has ADMIN role'
END

PRINT ''

-- =============================================
-- STEP 6: Map Permissions to Screens (ekran_yetki)
-- =============================================

PRINT 'Step 6: Mapping permissions to screens...'

-- TEMPLATE_MRK requires TEMPLATE_VIEW
IF NOT EXISTS (SELECT 1 FROM ekran_yetki e
               INNER JOIN yetki y ON e.yetki_id = y.yetki_id
               WHERE e.ekran_kod = 'TEMPLATE_MRK' AND y.yetki_kod = 'TEMPLATE_VIEW')
    AND EXISTS (SELECT 1 FROM kul_ekran WHERE ekran_kod = 'TEMPLATE_MRK')
BEGIN
    INSERT INTO ekran_yetki (ekran_kod, yetki_id, created_at)
    SELECT 'TEMPLATE_MRK', yetki_id, GETDATE()
    FROM yetki WHERE yetki_kod = 'TEMPLATE_VIEW'

    PRINT '  ✓ Mapped TEMPLATE_VIEW to TEMPLATE_MRK screen'
END

-- SYS_SETTINGS requires SETTINGS_MANAGE
IF NOT EXISTS (SELECT 1 FROM ekran_yetki e
               INNER JOIN yetki y ON e.yetki_id = y.yetki_id
               WHERE e.ekran_kod = 'SYS_SETTINGS' AND y.yetki_kod = 'SETTINGS_MANAGE')
    AND EXISTS (SELECT 1 FROM kul_ekran WHERE ekran_kod = 'SYS_SETTINGS')
BEGIN
    INSERT INTO ekran_yetki (ekran_kod, yetki_id, created_at)
    SELECT 'SYS_SETTINGS', yetki_id, GETDATE()
    FROM yetki WHERE yetki_kod = 'SETTINGS_MANAGE'

    PRINT '  ✓ Mapped SETTINGS_MANAGE to SYS_SETTINGS screen'
END

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================

PRINT '========================================='
PRINT 'Sprint 7: Security seed data completed'
PRINT '========================================='
PRINT ''
PRINT 'Summary:'
PRINT '--------'

-- Count permissions
DECLARE @permission_count INT
SELECT @permission_count = COUNT(*) FROM yetki WHERE modul IN ('Template', 'Settings')
PRINT '  Permissions created: ' + CAST(@permission_count AS VARCHAR)

-- Count roles
PRINT '  Roles created: 1 (ADMIN)'

-- Count users
PRINT '  Users created: 1 (admin)'

-- Count screen mappings
DECLARE @screen_mapping_count INT
SELECT @screen_mapping_count = COUNT(*)
FROM ekran_yetki e
INNER JOIN yetki y ON e.yetki_id = y.yetki_id
WHERE e.ekran_kod IN ('TEMPLATE_MRK', 'SYS_SETTINGS')

PRINT '  Screen permission mappings: ' + CAST(@screen_mapping_count AS VARCHAR)

PRINT ''
PRINT '========================================='
PRINT 'CRITICAL SECURITY WARNINGS:'
PRINT '========================================='
PRINT '1. Admin user created with SAMPLE password hash'
PRINT '2. DEFAULT PASSWORD: Admin123!'
PRINT '3. CHANGE THIS PASSWORD IMMEDIATELY after first login!'
PRINT '4. Use the PasswordHelper class to generate proper PBKDF2 hashes'
PRINT '5. The sample hash provided is for demonstration only'
PRINT '========================================='
PRINT ''

GO
