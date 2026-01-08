-- =============================================
-- Urun Stored Procedures
-- =============================================

USE [AktarOtomasyon]
GO

-- sp_urun_kaydet
IF OBJECT_ID('sp_urun_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_urun_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_urun_kaydet]
    @urun_id INT = NULL OUTPUT,
    @urun_kod NVARCHAR(50),
    @urun_adi NVARCHAR(200),
    @kategori_id INT = NULL,
    @birim_id INT = NULL,
    @alis_fiyati DECIMAL(18,2) = NULL,
    @satis_fiyati DECIMAL(18,2) = NULL,
    @barkod NVARCHAR(50) = NULL,
    @aciklama NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- BR-URUN-003: Required field validation
    IF @urun_kod IS NULL OR LTRIM(RTRIM(@urun_kod)) = ''
        RAISERROR('Ürün kodu zorunludur.', 16, 1);

    IF @urun_adi IS NULL OR LTRIM(RTRIM(@urun_adi)) = ''
        RAISERROR('Ürün adı zorunludur.', 16, 1);

    -- BR-URUN-001: Unique urun_kod check
    IF EXISTS (SELECT 1 FROM [dbo].[urun] WHERE [urun_kod] = @urun_kod
               AND (@urun_id IS NULL OR [urun_id] != @urun_id))
        RAISERROR('Ürün kodu zaten kullanılıyor.', 16, 1);

    -- BR-URUN-002: Unique barcode check (if provided)
    IF @barkod IS NOT NULL AND LTRIM(RTRIM(@barkod)) != ''
    BEGIN
        IF EXISTS (SELECT 1 FROM [dbo].[urun] WHERE [barkod] = @barkod
                   AND (@urun_id IS NULL OR [urun_id] != @urun_id))
            RAISERROR('Barkod zaten başka bir ürüne ait.', 16, 1);
    END

    -- BR-URUN-004: Price validation
    IF @alis_fiyati IS NOT NULL AND @alis_fiyati < 0
        RAISERROR('Alış fiyatı negatif olamaz.', 16, 1);

    IF @satis_fiyati IS NOT NULL AND @satis_fiyati < 0
        RAISERROR('Satış fiyatı negatif olamaz.', 16, 1);

    IF @urun_id IS NULL OR @urun_id = 0
    BEGIN
        INSERT INTO [dbo].[urun] (
            [urun_kod], [urun_adi], [kategori_id], [birim_id],
            [alis_fiyati], [satis_fiyati], [barkod], [aciklama]
        )
        VALUES (
            @urun_kod, @urun_adi, @kategori_id, @birim_id,
            @alis_fiyati, @satis_fiyati, @barkod, @aciklama
        );
        SET @urun_id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE [dbo].[urun]
        SET [urun_kod] = @urun_kod,
            [urun_adi] = @urun_adi,
            [kategori_id] = @kategori_id,
            [birim_id] = @birim_id,
            [alis_fiyati] = @alis_fiyati,
            [satis_fiyati] = @satis_fiyati,
            [barkod] = @barkod,
            [aciklama] = @aciklama,
            [guncelleme_tarih] = GETDATE()
        WHERE [urun_id] = @urun_id;
    END
END
GO

-- sp_urun_getir
IF OBJECT_ID('sp_urun_getir', 'P') IS NOT NULL DROP PROCEDURE sp_urun_getir;
GO

CREATE PROCEDURE [dbo].[sp_urun_getir]
    @urun_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.[urun_id], u.[urun_kod], u.[urun_adi],
        u.[kategori_id], k.[kategori_adi],
        u.[birim_id], b.[birim_adi],
        u.[alis_fiyati], u.[satis_fiyati],
        u.[barkod], u.[aciklama], u.[aktif]
    FROM [dbo].[urun] u
    LEFT JOIN [dbo].[urun_kategori] k ON u.[kategori_id] = k.[kategori_id]
    LEFT JOIN [dbo].[urun_birim] b ON u.[birim_id] = b.[birim_id]
    WHERE u.[urun_id] = @urun_id;
END
GO

-- sp_urun_listele
IF OBJECT_ID('sp_urun_listele', 'P') IS NOT NULL DROP PROCEDURE sp_urun_listele;
GO

CREATE PROCEDURE [dbo].[sp_urun_listele]
    @aktif BIT = 1,
    @kategori_id INT = NULL,
    @arama NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.[urun_id], u.[urun_kod], u.[urun_adi],
        u.[kategori_id], k.[kategori_adi],
        u.[birim_id], b.[birim_adi],
        u.[alis_fiyati], u.[satis_fiyati],
        u.[barkod], u.[aktif],
        ug.[gorsel_path] AS [ana_gorsel_path]
    FROM [dbo].[urun] u
    LEFT JOIN [dbo].[urun_kategori] k ON u.[kategori_id] = k.[kategori_id]
    LEFT JOIN [dbo].[urun_birim] b ON u.[birim_id] = b.[birim_id]
    LEFT JOIN [dbo].[urun_gorsel] ug ON u.[urun_id] = ug.[urun_id] AND ug.[ana_gorsel] = 1
    WHERE (@aktif IS NULL OR u.[aktif] = @aktif)
      AND (@kategori_id IS NULL OR u.[kategori_id] = @kategori_id)
      AND (@arama IS NULL OR u.[urun_adi] LIKE '%' + @arama + '%' OR u.[urun_kod] LIKE '%' + @arama + '%')
    ORDER BY u.[urun_adi];
END
GO

-- sp_urun_pasifle
IF OBJECT_ID('sp_urun_pasifle', 'P') IS NOT NULL DROP PROCEDURE sp_urun_pasifle;
GO

CREATE PROCEDURE [dbo].[sp_urun_pasifle]
    @urun_id INT,
    @cascade_stok_ayar BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if product exists
    IF NOT EXISTS (SELECT 1 FROM [dbo].[urun] WHERE [urun_id] = @urun_id)
        RAISERROR('Ürün bulunamadı.', 16, 1);

    -- BR-URUN-005: Check for pending orders (if siparis tables exist)
    IF OBJECT_ID('dbo.siparis', 'U') IS NOT NULL AND OBJECT_ID('dbo.siparis_satir', 'U') IS NOT NULL
    BEGIN
        IF EXISTS (SELECT 1 FROM [dbo].[siparis_satir] ss
                   INNER JOIN [dbo].[siparis] s ON ss.[siparis_id] = s.[siparis_id]
                   WHERE ss.[urun_id] = @urun_id
                   AND s.[durum] IN ('TASLAK', 'BEKLIYOR', 'TESLIMAT'))
            RAISERROR('Ürünün bekleyen siparişleri var. Önce siparişleri tamamlayınız veya iptal ediniz.', 16, 1);
    END

    -- Optional cascade delete stock settings
    IF @cascade_stok_ayar = 1
        DELETE FROM [dbo].[urun_stok_ayar] WHERE [urun_id] = @urun_id;

    -- Deactivate product
    UPDATE [dbo].[urun]
    SET [aktif] = 0, [guncelleme_tarih] = GETDATE()
    WHERE [urun_id] = @urun_id;

    SELECT @@ROWCOUNT AS affected_rows;
END
GO

-- sp_urun_stok_ayar_kaydet
IF OBJECT_ID('sp_urun_stok_ayar_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_urun_stok_ayar_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_urun_stok_ayar_kaydet]
    @urun_id INT,
    @min_stok DECIMAL(18,2) = 0,
    @max_stok DECIMAL(18,2) = NULL,
    @kritik_stok DECIMAL(18,2) = 0,
    @siparis_miktari DECIMAL(18,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM [dbo].[urun_stok_ayar] WHERE [urun_id] = @urun_id)
    BEGIN
        UPDATE [dbo].[urun_stok_ayar]
        SET [min_stok] = @min_stok,
            [max_stok] = @max_stok,
            [kritik_stok] = @kritik_stok,
            [siparis_miktari] = @siparis_miktari
        WHERE [urun_id] = @urun_id;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[urun_stok_ayar] ([urun_id], [min_stok], [max_stok], [kritik_stok], [siparis_miktari])
        VALUES (@urun_id, @min_stok, @max_stok, @kritik_stok, @siparis_miktari);
    END
END
GO

-- sp_urun_stok_ayar_getir
IF OBJECT_ID('sp_urun_stok_ayar_getir', 'P') IS NOT NULL DROP PROCEDURE sp_urun_stok_ayar_getir;
GO

CREATE PROCEDURE [dbo].[sp_urun_stok_ayar_getir]
    @urun_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT [ayar_id], [urun_id], [min_stok], [max_stok], [kritik_stok], [siparis_miktari]
    FROM [dbo].[urun_stok_ayar]
    WHERE [urun_id] = @urun_id;
END
GO

PRINT 'Urun SP''ler oluşturuldu.'
GO
