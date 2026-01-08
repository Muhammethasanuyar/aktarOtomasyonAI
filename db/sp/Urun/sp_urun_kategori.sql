-- =============================================
-- Ürün Kategori Yönetimi Stored Procedures
-- Sprint 2: Category CRUD Operations
-- =============================================

-- =============================================
-- sp_urun_kategori_kaydet
-- Kategori kaydetme/güncelleme (UPSERT)
-- BR-KAT-001: Unique kategori_kod
-- BR-KAT-002: No circular parent reference
-- =============================================
IF OBJECT_ID('sp_urun_kategori_kaydet', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_kategori_kaydet;
GO

CREATE PROCEDURE sp_urun_kategori_kaydet
    @kategori_id INT OUTPUT,
    @kategori_kod NVARCHAR(50),
    @kategori_adi NVARCHAR(200),
    @ust_kategori_id INT = NULL,
    @aktif BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Required field validations
    IF @kategori_kod IS NULL OR LTRIM(RTRIM(@kategori_kod)) = ''
        RAISERROR('Kategori kodu zorunludur.', 16, 1);

    IF @kategori_adi IS NULL OR LTRIM(RTRIM(@kategori_adi)) = ''
        RAISERROR('Kategori adı zorunludur.', 16, 1);

    -- BR-KAT-001: Unique kategori_kod check
    IF EXISTS (SELECT 1 FROM urun_kategori
               WHERE kategori_kod = @kategori_kod
               AND (@kategori_id IS NULL OR kategori_id != @kategori_id))
        RAISERROR('Kategori kodu zaten kullanılıyor.', 16, 1);

    -- BR-KAT-002: Circular reference prevention
    IF @ust_kategori_id IS NOT NULL
    BEGIN
        IF @kategori_id IS NOT NULL AND @ust_kategori_id = @kategori_id
            RAISERROR('Kategori kendi üst kategorisi olamaz.', 16, 1);

        -- Check if parent exists and is active
        IF NOT EXISTS (SELECT 1 FROM urun_kategori
                       WHERE kategori_id = @ust_kategori_id AND aktif = 1)
            RAISERROR('Üst kategori bulunamadı veya pasif durumda.', 16, 1);
    END

    -- UPSERT Logic
    IF @kategori_id IS NULL OR @kategori_id = 0
    BEGIN
        -- INSERT
        INSERT INTO urun_kategori (kategori_kod, kategori_adi, ust_kategori_id, aktif, olusturma_tarih)
        VALUES (@kategori_kod, @kategori_adi, @ust_kategori_id, @aktif, GETDATE());

        SET @kategori_id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- UPDATE
        IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_id = @kategori_id)
            RAISERROR('Güncellenecek kategori bulunamadı.', 16, 1);

        UPDATE urun_kategori
        SET kategori_kod = @kategori_kod,
            kategori_adi = @kategori_adi,
            ust_kategori_id = @ust_kategori_id,
            aktif = @aktif,
            guncelleme_tarih = GETDATE()
        WHERE kategori_id = @kategori_id;
    END

    -- Return the saved record
    SELECT @kategori_id AS kategori_id;
END
GO

-- =============================================
-- sp_urun_kategori_getir
-- Tek kategori getirme (parent bilgisi ile)
-- =============================================
IF OBJECT_ID('sp_urun_kategori_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_kategori_getir;
GO

CREATE PROCEDURE sp_urun_kategori_getir
    @kategori_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        k.kategori_id,
        k.kategori_kod,
        k.kategori_adi,
        k.ust_kategori_id,
        uk.kategori_adi AS ust_kategori_adi,
        k.aktif,
        k.olusturma_tarih,
        k.guncelleme_tarih
    FROM urun_kategori k
    LEFT JOIN urun_kategori uk ON k.ust_kategori_id = uk.kategori_id
    WHERE k.kategori_id = @kategori_id;
END
GO

-- =============================================
-- sp_urun_kategori_listele
-- Kategori listesi (hiyerarşik, ürün sayısı ile)
-- =============================================
IF OBJECT_ID('sp_urun_kategori_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_kategori_listele;
GO

CREATE PROCEDURE sp_urun_kategori_listele
    @aktif BIT = NULL,
    @ust_kategori_id INT = NULL,
    @arama NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        k.kategori_id,
        k.kategori_kod,
        k.kategori_adi,
        k.ust_kategori_id,
        uk.kategori_adi AS ust_kategori_adi,
        k.aktif,
        COUNT(u.urun_id) AS urun_sayisi,
        k.olusturma_tarih
    FROM urun_kategori k
    LEFT JOIN urun_kategori uk ON k.ust_kategori_id = uk.kategori_id
    LEFT JOIN urun u ON k.kategori_id = u.kategori_id AND u.aktif = 1
    WHERE
        (@aktif IS NULL OR k.aktif = @aktif)
        AND (@ust_kategori_id IS NULL OR k.ust_kategori_id = @ust_kategori_id)
        AND (@arama IS NULL
             OR k.kategori_kod LIKE '%' + @arama + '%'
             OR k.kategori_adi LIKE '%' + @arama + '%')
    GROUP BY
        k.kategori_id,
        k.kategori_kod,
        k.kategori_adi,
        k.ust_kategori_id,
        uk.kategori_adi,
        k.aktif,
        k.olusturma_tarih
    ORDER BY
        CASE WHEN k.ust_kategori_id IS NULL THEN 0 ELSE 1 END,
        k.kategori_adi;
END
GO

-- =============================================
-- sp_urun_kategori_pasifle
-- Kategori pasif etme (soft delete)
-- BR-KAT-003: Check active products
-- BR-KAT-004: Check active sub-categories
-- =============================================
IF OBJECT_ID('sp_urun_kategori_pasifle', 'P') IS NOT NULL
    DROP PROCEDURE sp_urun_kategori_pasifle;
GO

CREATE PROCEDURE sp_urun_kategori_pasifle
    @kategori_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if category exists
    IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_id = @kategori_id)
        RAISERROR('Kategori bulunamadı.', 16, 1);

    -- BR-KAT-003: Check for active products
    IF EXISTS (SELECT 1 FROM urun
               WHERE kategori_id = @kategori_id AND aktif = 1)
        RAISERROR('Bu kategoriye ait aktif ürünler var. Önce ürünleri pasif hale getiriniz.', 16, 1);

    -- BR-KAT-004: Check for active sub-categories
    IF EXISTS (SELECT 1 FROM urun_kategori
               WHERE ust_kategori_id = @kategori_id AND aktif = 1)
        RAISERROR('Bu kategorinin aktif alt kategorileri var. Önce alt kategorileri pasif hale getiriniz.', 16, 1);

    -- Deactivate category
    UPDATE urun_kategori
    SET aktif = 0,
        guncelleme_tarih = GETDATE()
    WHERE kategori_id = @kategori_id;

    SELECT @@ROWCOUNT AS affected_rows;
END
GO

PRINT 'sp_urun_kategori.sql executed successfully - 4 stored procedures created';
