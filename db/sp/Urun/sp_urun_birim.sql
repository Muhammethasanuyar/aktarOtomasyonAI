-- =============================================
-- Ürün Birim Yönetimi Stored Procedures
-- Sprint 2: Unit CRUD Operations
-- =============================================

-- =============================================
-- sp_urun_birim_kaydet
-- Birim kaydetme/güncelleme (UPSERT)
-- BR-BIRIM-001: Unique birim_kod
-- =============================================
IF OBJECT_ID('sp_urun_birim_kaydet', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_birim_kaydet;
GO

CREATE PROCEDURE sp_urun_birim_kaydet
    @birim_id INT OUTPUT,
    @birim_kod NVARCHAR(20),
    @birim_adi NVARCHAR(100),
    @aktif BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Required field validations
    IF @birim_kod IS NULL OR LTRIM(RTRIM(@birim_kod)) = ''
        RAISERROR('Birim kodu zorunludur.', 16, 1);

    IF @birim_adi IS NULL OR LTRIM(RTRIM(@birim_adi)) = ''
        RAISERROR('Birim adı zorunludur.', 16, 1);

    -- BR-BIRIM-001: Unique birim_kod check
    IF EXISTS (SELECT 1 FROM urun_birim
               WHERE birim_kod = @birim_kod
               AND (@birim_id IS NULL OR birim_id != @birim_id))
        RAISERROR('Birim kodu zaten kullanılıyor.', 16, 1);

    -- UPSERT Logic
    IF @birim_id IS NULL OR @birim_id = 0
    BEGIN
        -- INSERT
        INSERT INTO urun_birim (birim_kod, birim_adi, aktif, olusturma_tarih)
        VALUES (@birim_kod, @birim_adi, @aktif, GETDATE());

        SET @birim_id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- UPDATE
        IF NOT EXISTS (SELECT 1 FROM urun_birim WHERE birim_id = @birim_id)
            RAISERROR('Güncellenecek birim bulunamadı.', 16, 1);

        UPDATE urun_birim
        SET birim_kod = @birim_kod,
            birim_adi = @birim_adi,
            aktif = @aktif,
            guncelleme_tarih = GETDATE()
        WHERE birim_id = @birim_id;
    END

    -- Return the saved record
    SELECT @birim_id AS birim_id;
END
GO

-- =============================================
-- sp_urun_birim_getir
-- Tek birim getirme
-- =============================================
IF OBJECT_ID('sp_urun_birim_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_birim_getir;
GO

CREATE PROCEDURE sp_urun_birim_getir
    @birim_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        birim_id,
        birim_kod,
        birim_adi,
        aktif,
        olusturma_tarih,
        guncelleme_tarih
    FROM urun_birim
    WHERE birim_id = @birim_id;
END
GO

-- =============================================
-- sp_urun_birim_listele
-- Birim listesi (ürün sayısı ile)
-- =============================================
IF OBJECT_ID('sp_urun_birim_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_birim_listele;
GO

CREATE PROCEDURE sp_urun_birim_listele
    @aktif BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        b.birim_id,
        b.birim_kod,
        b.birim_adi,
        b.aktif,
        COUNT(u.urun_id) AS urun_sayisi,
        b.olusturma_tarih
    FROM urun_birim b
    LEFT JOIN urun u ON b.birim_id = u.birim_id AND u.aktif = 1
    WHERE
        (@aktif IS NULL OR b.aktif = @aktif)
    GROUP BY
        b.birim_id,
        b.birim_kod,
        b.birim_adi,
        b.aktif,
        b.olusturma_tarih
    ORDER BY
        b.birim_adi;
END
GO

-- =============================================
-- sp_urun_birim_pasifle
-- Birim pasif etme (soft delete)
-- BR-BIRIM-002: Check active products
-- =============================================
IF OBJECT_ID('sp_urun_birim_pasifle', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_birim_pasifle;
GO

CREATE PROCEDURE sp_urun_birim_pasifle
    @birim_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if unit exists
    IF NOT EXISTS (SELECT 1 FROM urun_birim WHERE birim_id = @birim_id)
        RAISERROR('Birim bulunamadı.', 16, 1);

    -- BR-BIRIM-002: Check for active products
    IF EXISTS (SELECT 1 FROM urun
               WHERE birim_id = @birim_id AND aktif = 1)
        RAISERROR('Bu birime ait aktif ürünler var. Önce ürünleri pasif hale getiriniz.', 16, 1);

    -- Deactivate unit
    UPDATE urun_birim
    SET aktif = 0,
        guncelleme_tarih = GETDATE()
    WHERE birim_id = @birim_id;

    SELECT @@ROWCOUNT AS affected_rows;
END
GO

PRINT 'sp_urun_birim.sql executed successfully - 4 stored procedures created';
