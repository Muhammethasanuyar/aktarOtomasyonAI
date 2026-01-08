/*
    Sprint 7 - Create All Stored Procedures

    This script creates all stored procedures needed for Sprint 7 Backend
    Run this script in SQL Server Management Studio

    Order:
    1. Auth procedures (2)
    2. Security procedures (23)
    3. Audit procedures (2)

    Total: 27 stored procedures
*/

USE [AktarOtomasyon]
GO

PRINT '========================================='
PRINT 'Creating Sprint 7 Stored Procedures'
PRINT '========================================='
PRINT ''

-- =============================================
-- AUTH PROCEDURES (2)
-- =============================================
PRINT 'Creating Auth procedures...'
PRINT ''

-- sp_kullanici_getir_login
IF OBJECT_ID('sp_kullanici_getir_login', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_getir_login
GO

CREATE PROCEDURE [dbo].[sp_kullanici_getir_login]
    @kullanici_adi NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        k.kullanici_id,
        k.kullanici_adi,
        k.ad_soyad,
        k.email,
        k.parola_hash,
        k.parola_salt,
        k.parola_iterasyon,
        k.aktif,
        k.son_giris_tarih
    FROM [dbo].[kullanici] k
    WHERE k.kullanici_adi = @kullanici_adi
        AND k.aktif = 1
END
GO
PRINT '  ✓ Created sp_kullanici_getir_login'

-- sp_kullanici_son_giris_guncelle
IF OBJECT_ID('sp_kullanici_son_giris_guncelle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_son_giris_guncelle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_son_giris_guncelle]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[kullanici]
    SET son_giris_tarih = GETDATE()
    WHERE kullanici_id = @kullanici_id
END
GO
PRINT '  ✓ Created sp_kullanici_son_giris_guncelle'
PRINT ''

-- =============================================
-- SECURITY PROCEDURES - User Management (9)
-- =============================================
PRINT 'Creating Security procedures - User Management...'
PRINT ''

-- sp_kullanici_kaydet
IF OBJECT_ID('sp_kullanici_kaydet', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_kaydet
GO

CREATE PROCEDURE [dbo].[sp_kullanici_kaydet]
    @kullanici_id INT OUTPUT,
    @kullanici_adi NVARCHAR(50),
    @ad_soyad NVARCHAR(100),
    @email NVARCHAR(100) = NULL,
    @parola_hash NVARCHAR(512) = NULL,
    @parola_salt NVARCHAR(256) = NULL,
    @parola_iterasyon INT = NULL,
    @aktif BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    IF @kullanici_id IS NULL OR @kullanici_id = 0
    BEGIN
        INSERT INTO [dbo].[kullanici]
            (kullanici_adi, ad_soyad, email, parola_hash, parola_salt, parola_iterasyon, aktif, created_at)
        VALUES
            (@kullanici_adi, @ad_soyad, @email, @parola_hash, @parola_salt, @parola_iterasyon, @aktif, GETDATE())

        SET @kullanici_id = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[kullanici]
        SET
            kullanici_adi = @kullanici_adi,
            ad_soyad = @ad_soyad,
            email = @email,
            aktif = @aktif,
            updated_at = GETDATE()
        WHERE kullanici_id = @kullanici_id
    END
END
GO
PRINT '  ✓ Created sp_kullanici_kaydet'

-- sp_kullanici_listele
IF OBJECT_ID('sp_kullanici_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_listele
GO

CREATE PROCEDURE [dbo].[sp_kullanici_listele]
    @aktif BIT = NULL,
    @kullanici_adi NVARCHAR(50) = NULL,
    @ad_soyad NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        k.kullanici_id,
        k.kullanici_adi,
        k.ad_soyad,
        k.email,
        k.aktif,
        STUFF((
            SELECT ', ' + r.rol_adi
            FROM [dbo].[kullanici_rol] kr
            INNER JOIN [dbo].[rol] r ON kr.rol_id = r.rol_id
            WHERE kr.kullanici_id = k.kullanici_id AND r.aktif = 1
            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') as roller
    FROM [dbo].[kullanici] k
    WHERE (@aktif IS NULL OR k.aktif = @aktif)
        AND (@kullanici_adi IS NULL OR k.kullanici_adi LIKE '%' + @kullanici_adi + '%')
        AND (@ad_soyad IS NULL OR k.ad_soyad LIKE '%' + @ad_soyad + '%')
    ORDER BY k.ad_soyad
END
GO
PRINT '  ✓ Created sp_kullanici_listele'

-- sp_kullanici_getir
IF OBJECT_ID('sp_kullanici_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_getir
GO

CREATE PROCEDURE [dbo].[sp_kullanici_getir]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        kullanici_id,
        kullanici_adi,
        ad_soyad,
        email,
        aktif
    FROM [dbo].[kullanici]
    WHERE kullanici_id = @kullanici_id
END
GO
PRINT '  ✓ Created sp_kullanici_getir'

-- sp_kullanici_pasifle
IF OBJECT_ID('sp_kullanici_pasifle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_pasifle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_pasifle]
    @kullanici_id INT,
    @updated_by INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[kullanici]
    SET
        aktif = 0,
        updated_at = GETDATE(),
        updated_by = @updated_by
    WHERE kullanici_id = @kullanici_id
END
GO
PRINT '  ✓ Created sp_kullanici_pasifle'

-- sp_kullanici_parola_guncelle
IF OBJECT_ID('sp_kullanici_parola_guncelle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_parola_guncelle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_parola_guncelle]
    @kullanici_id INT,
    @yeni_parola_hash NVARCHAR(512),
    @yeni_parola_salt NVARCHAR(256),
    @parola_iterasyon INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[kullanici]
    SET
        parola_hash = @yeni_parola_hash,
        parola_salt = @yeni_parola_salt,
        parola_iterasyon = @parola_iterasyon,
        updated_at = GETDATE()
    WHERE kullanici_id = @kullanici_id
END
GO
PRINT '  ✓ Created sp_kullanici_parola_guncelle'

-- sp_kullanici_parola_sifirla
IF OBJECT_ID('sp_kullanici_parola_sifirla', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_parola_sifirla
GO

CREATE PROCEDURE [dbo].[sp_kullanici_parola_sifirla]
    @kullanici_id INT,
    @yeni_parola_hash NVARCHAR(512),
    @yeni_parola_salt NVARCHAR(256),
    @parola_iterasyon INT,
    @reset_by INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[kullanici]
    SET
        parola_hash = @yeni_parola_hash,
        parola_salt = @yeni_parola_salt,
        parola_iterasyon = @parola_iterasyon,
        updated_at = GETDATE(),
        updated_by = @reset_by
    WHERE kullanici_id = @kullanici_id
END
GO
PRINT '  ✓ Created sp_kullanici_parola_sifirla'

-- sp_kullanici_rol_ekle
IF OBJECT_ID('sp_kullanici_rol_ekle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_rol_ekle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_rol_ekle]
    @kullanici_id INT,
    @rol_id INT,
    @created_by INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[kullanici_rol]
                   WHERE kullanici_id = @kullanici_id AND rol_id = @rol_id)
    BEGIN
        INSERT INTO [dbo].[kullanici_rol]
            (kullanici_id, rol_id, created_at, created_by)
        VALUES
            (@kullanici_id, @rol_id, GETDATE(), @created_by)
    END
END
GO
PRINT '  ✓ Created sp_kullanici_rol_ekle'

-- sp_kullanici_rol_sil
IF OBJECT_ID('sp_kullanici_rol_sil', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_rol_sil
GO

CREATE PROCEDURE [dbo].[sp_kullanici_rol_sil]
    @kullanici_id INT,
    @rol_id INT,
    @updated_by INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[kullanici_rol]
    SET
        aktif = 0,
        updated_at = GETDATE(),
        updated_by = @updated_by
    WHERE kullanici_id = @kullanici_id AND rol_id = @rol_id
END
GO
PRINT '  ✓ Created sp_kullanici_rol_sil'

-- sp_kullanici_rol_listele
IF OBJECT_ID('sp_kullanici_rol_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_rol_listele
GO

CREATE PROCEDURE [dbo].[sp_kullanici_rol_listele]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        kr.kullanici_id,
        kr.rol_id,
        r.rol_adi
    FROM [dbo].[kullanici_rol] kr
    INNER JOIN [dbo].[rol] r ON kr.rol_id = r.rol_id
    WHERE kr.kullanici_id = @kullanici_id
        AND kr.aktif = 1
        AND r.aktif = 1
END
GO
PRINT '  ✓ Created sp_kullanici_rol_listele'

PRINT ''

-- =============================================
-- SECURITY PROCEDURES - Role Management (4)
-- =============================================
PRINT 'Creating Security procedures - Role Management...'
PRINT ''

-- sp_rol_kaydet
IF OBJECT_ID('sp_rol_kaydet', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_kaydet
GO

CREATE PROCEDURE [dbo].[sp_rol_kaydet]
    @rol_id INT OUTPUT,
    @rol_adi NVARCHAR(50),
    @aciklama NVARCHAR(200) = NULL,
    @aktif BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    IF @rol_id IS NULL OR @rol_id = 0
    BEGIN
        INSERT INTO [dbo].[rol]
            (rol_adi, aciklama, aktif, created_at)
        VALUES
            (@rol_adi, @aciklama, @aktif, GETDATE())

        SET @rol_id = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[rol]
        SET
            rol_adi = @rol_adi,
            aciklama = @aciklama,
            aktif = @aktif,
            updated_at = GETDATE()
        WHERE rol_id = @rol_id
    END
END
GO
PRINT '  ✓ Created sp_rol_kaydet'

-- sp_rol_listele
IF OBJECT_ID('sp_rol_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_listele
GO

CREATE PROCEDURE [dbo].[sp_rol_listele]
    @aktif BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        r.rol_id,
        r.rol_adi,
        r.aciklama,
        r.aktif,
        (SELECT COUNT(*) FROM [dbo].[kullanici_rol] kr
         WHERE kr.rol_id = r.rol_id AND kr.aktif = 1) as kullanici_sayisi,
        (SELECT COUNT(*) FROM [dbo].[rol_yetki] ry
         WHERE ry.rol_id = r.rol_id AND ry.aktif = 1) as yetki_sayisi
    FROM [dbo].[rol] r
    WHERE (@aktif IS NULL OR r.aktif = @aktif)
    ORDER BY r.rol_adi
END
GO
PRINT '  ✓ Created sp_rol_listele'

-- sp_rol_getir
IF OBJECT_ID('sp_rol_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_getir
GO

CREATE PROCEDURE [dbo].[sp_rol_getir]
    @rol_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rol_id,
        rol_adi,
        aciklama,
        aktif
    FROM [dbo].[rol]
    WHERE rol_id = @rol_id
END
GO
PRINT '  ✓ Created sp_rol_getir'

-- sp_rol_pasifle
IF OBJECT_ID('sp_rol_pasifle', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_pasifle
GO

CREATE PROCEDURE [dbo].[sp_rol_pasifle]
    @rol_id INT,
    @updated_by INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[rol]
    SET
        aktif = 0,
        updated_at = GETDATE(),
        updated_by = @updated_by
    WHERE rol_id = @rol_id
END
GO
PRINT '  ✓ Created sp_rol_pasifle'

PRINT ''

-- =============================================
-- SECURITY PROCEDURES - Role-Permission (5)
-- =============================================
PRINT 'Creating Security procedures - Role-Permission...'
PRINT ''

-- sp_rol_yetki_ekle
IF OBJECT_ID('sp_rol_yetki_ekle', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_yetki_ekle
GO

CREATE PROCEDURE [dbo].[sp_rol_yetki_ekle]
    @rol_id INT,
    @yetki_id INT,
    @created_by INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[rol_yetki]
                   WHERE rol_id = @rol_id AND yetki_id = @yetki_id)
    BEGIN
        INSERT INTO [dbo].[rol_yetki]
            (rol_id, yetki_id, created_at, created_by)
        VALUES
            (@rol_id, @yetki_id, GETDATE(), @created_by)
    END
END
GO
PRINT '  ✓ Created sp_rol_yetki_ekle'

-- sp_rol_yetki_sil
IF OBJECT_ID('sp_rol_yetki_sil', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_yetki_sil
GO

CREATE PROCEDURE [dbo].[sp_rol_yetki_sil]
    @rol_id INT,
    @yetki_id INT,
    @updated_by INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[rol_yetki]
    SET
        aktif = 0,
        updated_at = GETDATE(),
        updated_by = @updated_by
    WHERE rol_id = @rol_id AND yetki_id = @yetki_id
END
GO
PRINT '  ✓ Created sp_rol_yetki_sil'

-- sp_rol_yetki_listele
IF OBJECT_ID('sp_rol_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_rol_yetki_listele]
    @rol_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ry.rol_id,
        ry.yetki_id,
        y.yetki_kod,
        y.yetki_adi
    FROM [dbo].[rol_yetki] ry
    INNER JOIN [dbo].[yetki] y ON ry.yetki_id = y.yetki_id
    WHERE ry.rol_id = @rol_id
        AND ry.aktif = 1
END
GO
PRINT '  ✓ Created sp_rol_yetki_listele'

-- sp_yetki_listele
IF OBJECT_ID('sp_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_yetki_listele]
    @modul NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        yetki_id,
        yetki_kod,
        yetki_adi,
        modul,
        aciklama
    FROM [dbo].[yetki]
    WHERE (@modul IS NULL OR modul = @modul)
    ORDER BY modul, yetki_adi
END
GO
PRINT '  ✓ Created sp_yetki_listele'

-- sp_yetki_getir
IF OBJECT_ID('sp_yetki_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_yetki_getir
GO

CREATE PROCEDURE [dbo].[sp_yetki_getir]
    @yetki_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        yetki_id,
        yetki_kod,
        yetki_adi,
        modul,
        aciklama
    FROM [dbo].[yetki]
    WHERE yetki_id = @yetki_id
END
GO
PRINT '  ✓ Created sp_yetki_getir'

PRINT ''

-- =============================================
-- SECURITY PROCEDURES - Screen-Permission (3)
-- =============================================
PRINT 'Creating Security procedures - Screen-Permission...'
PRINT ''

-- sp_ekran_yetki_ekle
IF OBJECT_ID('sp_ekran_yetki_ekle', 'P') IS NOT NULL
    DROP PROCEDURE sp_ekran_yetki_ekle
GO

CREATE PROCEDURE [dbo].[sp_ekran_yetki_ekle]
    @ekran_kod NVARCHAR(50),
    @yetki_id INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[ekran_yetki]
                   WHERE ekran_kod = @ekran_kod AND yetki_id = @yetki_id)
    BEGIN
        INSERT INTO [dbo].[ekran_yetki]
            (ekran_kod, yetki_id, created_at)
        VALUES
            (@ekran_kod, @yetki_id, GETDATE())
    END
END
GO
PRINT '  ✓ Created sp_ekran_yetki_ekle'

-- sp_ekran_yetki_sil
IF OBJECT_ID('sp_ekran_yetki_sil', 'P') IS NOT NULL
    DROP PROCEDURE sp_ekran_yetki_sil
GO

CREATE PROCEDURE [dbo].[sp_ekran_yetki_sil]
    @ekran_kod NVARCHAR(50),
    @yetki_id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[ekran_yetki]
    WHERE ekran_kod = @ekran_kod AND yetki_id = @yetki_id
END
GO
PRINT '  ✓ Created sp_ekran_yetki_sil'

-- sp_ekran_yetki_listele
IF OBJECT_ID('sp_ekran_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_ekran_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_ekran_yetki_listele]
    @ekran_kod NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ey.ekran_kod,
        ey.yetki_id,
        y.yetki_kod
    FROM [dbo].[ekran_yetki] ey
    INNER JOIN [dbo].[yetki] y ON ey.yetki_id = y.yetki_id
    WHERE ey.ekran_kod = @ekran_kod
END
GO
PRINT '  ✓ Created sp_ekran_yetki_listele'

PRINT ''

-- =============================================
-- SECURITY PROCEDURES - Effective Permissions (2)
-- =============================================
PRINT 'Creating Security procedures - Effective Permissions (CRITICAL)...'
PRINT ''

-- sp_kullanici_yetki_listele
IF OBJECT_ID('sp_kullanici_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_kullanici_yetki_listele]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        y.yetki_id,
        y.yetki_kod,
        y.yetki_adi,
        y.modul,
        y.aciklama
    FROM [dbo].[kullanici_rol] kr
    INNER JOIN [dbo].[rol] r ON kr.rol_id = r.rol_id
    INNER JOIN [dbo].[rol_yetki] ry ON r.rol_id = ry.rol_id
    INNER JOIN [dbo].[yetki] y ON ry.yetki_id = y.yetki_id
    WHERE kr.kullanici_id = @kullanici_id
        AND kr.aktif = 1
        AND r.aktif = 1
        AND ry.aktif = 1
    ORDER BY y.modul, y.yetki_adi
END
GO
PRINT '  ✓ Created sp_kullanici_yetki_listele'

-- sp_kullanici_yetki_kontrol
IF OBJECT_ID('sp_kullanici_yetki_kontrol', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_yetki_kontrol
GO

CREATE PROCEDURE [dbo].[sp_kullanici_yetki_kontrol]
    @kullanici_id INT,
    @yetki_kod NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM [dbo].[kullanici_rol] kr
        INNER JOIN [dbo].[rol] r ON kr.rol_id = r.rol_id
        INNER JOIN [dbo].[rol_yetki] ry ON r.rol_id = ry.rol_id
        INNER JOIN [dbo].[yetki] y ON ry.yetki_id = y.yetki_id
        WHERE kr.kullanici_id = @kullanici_id
            AND y.yetki_kod = @yetki_kod
            AND kr.aktif = 1
            AND r.aktif = 1
            AND ry.aktif = 1
    )
        SELECT 1 as has_permission
    ELSE
        SELECT 0 as has_permission
END
GO
PRINT '  ✓ Created sp_kullanici_yetki_kontrol'

PRINT ''

-- =============================================
-- AUDIT PROCEDURES (2)
-- =============================================
PRINT 'Creating Audit procedures...'
PRINT ''

-- sp_audit_listele
IF OBJECT_ID('sp_audit_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_audit_listele
GO

CREATE PROCEDURE [dbo].[sp_audit_listele]
    @entity NVARCHAR(100) = NULL,
    @action NVARCHAR(50) = NULL,
    @kullanici_id INT = NULL,
    @baslangic_tarih DATETIME = NULL,
    @bitis_tarih DATETIME = NULL,
    @top INT = 1000
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@top)
        a.audit_id,
        a.entity,
        a.entity_id,
        a.action,
        a.created_by,
        k.kullanici_adi,
        k.ad_soyad,
        a.created_at
    FROM [dbo].[audit_log] a
    INNER JOIN [dbo].[kullanici] k ON a.created_by = k.kullanici_id
    WHERE (@entity IS NULL OR a.entity = @entity)
        AND (@action IS NULL OR a.action = @action)
        AND (@kullanici_id IS NULL OR a.created_by = @kullanici_id)
        AND (@baslangic_tarih IS NULL OR a.created_at >= @baslangic_tarih)
        AND (@bitis_tarih IS NULL OR a.created_at <= @bitis_tarih)
    ORDER BY a.created_at DESC
END
GO
PRINT '  ✓ Created sp_audit_listele'

-- sp_audit_getir
IF OBJECT_ID('sp_audit_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_audit_getir
GO

CREATE PROCEDURE [dbo].[sp_audit_getir]
    @audit_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.audit_id,
        a.entity,
        a.entity_id,
        a.action,
        a.detail_json,
        a.created_by,
        k.kullanici_adi,
        k.ad_soyad,
        a.created_at
    FROM [dbo].[audit_log] a
    INNER JOIN [dbo].[kullanici] k ON a.created_by = k.kullanici_id
    WHERE a.audit_id = @audit_id
END
GO
PRINT '  ✓ Created sp_audit_getir'

PRINT ''

-- =============================================
-- SUMMARY
-- =============================================
PRINT '========================================='
PRINT 'SUMMARY'
PRINT '========================================='
PRINT ''

DECLARE @total_sp_count INT
SELECT @total_sp_count = COUNT(*)
FROM sys.procedures
WHERE name LIKE 'sp_kullanici%'
   OR name LIKE 'sp_rol%'
   OR name LIKE 'sp_yetki%'
   OR name LIKE 'sp_ekran_yetki%'
   OR name LIKE 'sp_audit%'

PRINT 'Total Stored Procedures Created: ' + CAST(@total_sp_count AS VARCHAR)
PRINT ''
PRINT 'Categories:'
PRINT '  - Auth: 2'
PRINT '  - Security (User): 9'
PRINT '  - Security (Role): 4'
PRINT '  - Security (Permission): 5'
PRINT '  - Security (Screen): 3'
PRINT '  - Security (Effective): 2'
PRINT '  - Audit: 2'
PRINT ''
PRINT '✅ All stored procedures created successfully!'
PRINT ''
PRINT 'Next steps:'
PRINT '  1. Run: db/seed/008_sprint7_security_seed.sql'
PRINT '  2. Run: db/verify/verify_sprint7_setup.sql'
PRINT '  3. Login with admin/Admin123!'
PRINT ''
PRINT '========================================='

GO
