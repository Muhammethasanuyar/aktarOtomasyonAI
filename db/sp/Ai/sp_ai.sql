-- =============================================
-- AI Stored Procedures
-- =============================================

USE [AktarOtomasyon]
GO

-- sp_ai_icerik_getir
IF OBJECT_ID('sp_ai_icerik_getir', 'P') IS NOT NULL DROP PROCEDURE sp_ai_icerik_getir;
GO

CREATE PROCEDURE [dbo].[sp_ai_icerik_getir]
    @urun_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 1 [icerik_id], [urun_id], [icerik], [durum],
           [kaynaklar], [olusturma_tarih], [onay_tarih]
    FROM [dbo].[ai_urun_icerik]
    WHERE [urun_id] = @urun_id AND [durum] = 'AKTIF'
    ORDER BY [onay_tarih] DESC;
END
GO

-- sp_ai_icerik_kaydet
IF OBJECT_ID('sp_ai_icerik_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_ai_icerik_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_ai_icerik_kaydet]
    @urun_id INT,
    @icerik NVARCHAR(MAX),
    @durum NVARCHAR(20) = 'TASLAK',
    @kaynaklar NVARCHAR(MAX) = NULL,
    @sablon_kod NVARCHAR(50) = NULL,
    @provider NVARCHAR(50) = NULL,
    @kullanici_id INT = NULL,
    @icerik_id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ai_urun_icerik] ([urun_id], [icerik], [durum], [kaynaklar], [sablon_kod], [provider], [kullanici_id])
    VALUES (@urun_id, @icerik, @durum, @kaynaklar, @sablon_kod, @provider, @kullanici_id);
    
    SET @icerik_id = SCOPE_IDENTITY();
    
    -- Versiyon oluştur
    INSERT INTO [dbo].[ai_urun_icerik_ver] ([icerik_id], [versiyon_no], [icerik])
    VALUES (@icerik_id, 1, @icerik);
END
GO

-- sp_ai_icerik_versiyon_listele
IF OBJECT_ID('sp_ai_icerik_versiyon_listele', 'P') IS NOT NULL DROP PROCEDURE sp_ai_icerik_versiyon_listele;
GO

CREATE PROCEDURE [dbo].[sp_ai_icerik_versiyon_listele]
    @urun_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT v.[versiyon_id], v.[icerik_id], v.[versiyon_no], v.[icerik], v.[olusturma_tarih]
    FROM [dbo].[ai_urun_icerik_ver] v
    INNER JOIN [dbo].[ai_urun_icerik] i ON v.[icerik_id] = i.[icerik_id]
    WHERE i.[urun_id] = @urun_id
    ORDER BY v.[versiyon_no] DESC;
END
GO

-- sp_ai_icerik_onayla
IF OBJECT_ID('sp_ai_icerik_onayla', 'P') IS NOT NULL DROP PROCEDURE sp_ai_icerik_onayla;
GO

CREATE PROCEDURE [dbo].[sp_ai_icerik_onayla]
    @icerik_id INT,
    @onaylayan_kullanici_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @urun_id INT;
    SELECT @urun_id = [urun_id] FROM [dbo].[ai_urun_icerik] WHERE [icerik_id] = @icerik_id;
    
    -- Önce mevcut aktif içeriği pasifle
    UPDATE [dbo].[ai_urun_icerik]
    SET [durum] = 'PASIF'
    WHERE [urun_id] = @urun_id AND [durum] = 'AKTIF';
    
    -- Yeni içeriği aktifle
    UPDATE [dbo].[ai_urun_icerik]
    SET [durum] = 'AKTIF',
        [onaylayan_kullanici_id] = @onaylayan_kullanici_id,
        [onay_tarih] = GETDATE()
    WHERE [icerik_id] = @icerik_id;
END
GO

-- sp_ai_sablon_getir
IF OBJECT_ID('sp_ai_sablon_getir', 'P') IS NOT NULL DROP PROCEDURE sp_ai_sablon_getir;
GO

CREATE PROCEDURE [dbo].[sp_ai_sablon_getir]
    @sablon_kod NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [sablon_id], [sablon_kod], [sablon_adi],
           [prompt_sablonu], [aciklama], [aktif]
    FROM [dbo].[ai_sablon]
    WHERE [sablon_kod] = @sablon_kod AND [aktif] = 1;
END
GO

-- sp_ai_sablon_listele
IF OBJECT_ID('sp_ai_sablon_listele', 'P') IS NOT NULL DROP PROCEDURE sp_ai_sablon_listele;
GO

CREATE PROCEDURE [dbo].[sp_ai_sablon_listele]
    @aktif BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [sablon_id], [sablon_kod], [sablon_adi],
           [aciklama], [aktif], [olusturma_tarih]
    FROM [dbo].[ai_sablon]
    WHERE (@aktif IS NULL OR [aktif] = @aktif)
    ORDER BY [sablon_adi];
END
GO

-- sp_ai_sablon_kaydet
IF OBJECT_ID('sp_ai_sablon_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_ai_sablon_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_ai_sablon_kaydet]
    @sablon_id INT = NULL,
    @sablon_kod NVARCHAR(50),
    @sablon_adi NVARCHAR(200),
    @prompt_sablonu NVARCHAR(MAX),
    @aciklama NVARCHAR(500) = NULL,
    @aktif BIT = 1,
    @output_sablon_id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF @sablon_id IS NULL OR @sablon_id = 0
    BEGIN
        -- Insert
        INSERT INTO [dbo].[ai_sablon]
            ([sablon_kod], [sablon_adi], [prompt_sablonu], [aciklama], [aktif])
        VALUES
            (@sablon_kod, @sablon_adi, @prompt_sablonu, @aciklama, @aktif);

        SET @output_sablon_id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- Update
        UPDATE [dbo].[ai_sablon]
        SET [sablon_adi] = @sablon_adi,
            [prompt_sablonu] = @prompt_sablonu,
            [aciklama] = @aciklama,
            [aktif] = @aktif,
            [guncelleme_tarih] = GETDATE()
        WHERE [sablon_id] = @sablon_id;

        SET @output_sablon_id = @sablon_id;
    END
END
GO

-- sp_ai_sablon_sil
IF OBJECT_ID('sp_ai_sablon_sil', 'P') IS NOT NULL DROP PROCEDURE sp_ai_sablon_sil;
GO

CREATE PROCEDURE [dbo].[sp_ai_sablon_sil]
    @sablon_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Soft delete (mark as inactive)
    UPDATE [dbo].[ai_sablon]
    SET [aktif] = 0,
        [guncelleme_tarih] = GETDATE()
    WHERE [sablon_id] = @sablon_id;
END
GO

-- sp_ai_sablon_aktiflik_guncelle
IF OBJECT_ID('sp_ai_sablon_aktiflik_guncelle', 'P') IS NOT NULL DROP PROCEDURE sp_ai_sablon_aktiflik_guncelle;
GO

CREATE PROCEDURE [dbo].[sp_ai_sablon_aktiflik_guncelle]
    @sablon_id INT,
    @aktif BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ai_sablon]
    SET [aktif] = @aktif,
        [guncelleme_tarih] = GETDATE()
    WHERE [sablon_id] = @sablon_id;
END
GO

-- sp_ai_urun_bilgi_getir
IF OBJECT_ID('sp_ai_urun_bilgi_getir', 'P') IS NOT NULL DROP PROCEDURE sp_ai_urun_bilgi_getir;
GO

CREATE PROCEDURE [dbo].[sp_ai_urun_bilgi_getir]
    @urun_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.[urun_id],
        u.[urun_kod],
        u.[urun_adi],
        k.[kategori_adi],
        b.[birim_adi],
        u.[satis_fiyati],
        u.[aciklama]
    FROM [dbo].[urun] u
    LEFT JOIN [dbo].[urun_kategori] k ON u.[kategori_id] = k.[kategori_id]
    LEFT JOIN [dbo].[urun_birim] b ON u.[birim_id] = b.[birim_id]
    WHERE u.[urun_id] = @urun_id;
END
GO

PRINT 'AI SP''ler oluşturuldu.'
GO
