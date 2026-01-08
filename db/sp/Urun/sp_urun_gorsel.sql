-- =============================================
-- Urun Görsel Stored Procedures
-- =============================================

USE [AktarOtomasyon]
GO

-- sp_urun_gorsel_ekle
IF OBJECT_ID('sp_urun_gorsel_ekle', 'P') IS NOT NULL DROP PROCEDURE sp_urun_gorsel_ekle;
GO

CREATE PROCEDURE [dbo].[sp_urun_gorsel_ekle]
    @urun_id INT,
    @gorsel_path NVARCHAR(500),
    @gorsel_tip NVARCHAR(50) = NULL,
    @ana_gorsel BIT = 0,
    @gorsel_id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Eğer ana görsel ise diğerlerini pasifle
    IF @ana_gorsel = 1
    BEGIN
        UPDATE [dbo].[urun_gorsel] SET [ana_gorsel] = 0 WHERE [urun_id] = @urun_id;
    END
    
    DECLARE @sira INT;
    SELECT @sira = ISNULL(MAX([sira]), 0) + 1 FROM [dbo].[urun_gorsel] WHERE [urun_id] = @urun_id;
    
    INSERT INTO [dbo].[urun_gorsel] ([urun_id], [gorsel_path], [gorsel_tip], [ana_gorsel], [sira])
    VALUES (@urun_id, @gorsel_path, @gorsel_tip, @ana_gorsel, @sira);
    
    SET @gorsel_id = SCOPE_IDENTITY();
END
GO

-- sp_urun_gorsel_sil
IF OBJECT_ID('sp_urun_gorsel_sil', 'P') IS NOT NULL DROP PROCEDURE sp_urun_gorsel_sil;
GO

CREATE PROCEDURE [dbo].[sp_urun_gorsel_sil]
    @gorsel_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM [dbo].[urun_gorsel] WHERE [gorsel_id] = @gorsel_id;
END
GO

-- sp_urun_gorsel_listele
IF OBJECT_ID('sp_urun_gorsel_listele', 'P') IS NOT NULL DROP PROCEDURE sp_urun_gorsel_listele;
GO

CREATE PROCEDURE [dbo].[sp_urun_gorsel_listele]
    @urun_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT [gorsel_id], [urun_id], [gorsel_path], [gorsel_tip], [ana_gorsel], [sira], [olusturma_tarih]
    FROM [dbo].[urun_gorsel]
    WHERE [urun_id] = @urun_id
    ORDER BY [ana_gorsel] DESC, [sira];
END
GO

-- sp_urun_ana_gorsel_set
IF OBJECT_ID('sp_urun_ana_gorsel_set', 'P') IS NOT NULL DROP PROCEDURE sp_urun_ana_gorsel_set;
GO

CREATE PROCEDURE [dbo].[sp_urun_ana_gorsel_set]
    @gorsel_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @urun_id INT;
    SELECT @urun_id = [urun_id] FROM [dbo].[urun_gorsel] WHERE [gorsel_id] = @gorsel_id;
    
    -- Tüm görselleri pasifle
    UPDATE [dbo].[urun_gorsel] SET [ana_gorsel] = 0 WHERE [urun_id] = @urun_id;
    
    -- Seçileni ana yap
    UPDATE [dbo].[urun_gorsel] SET [ana_gorsel] = 1 WHERE [gorsel_id] = @gorsel_id;
END
GO

PRINT 'Urun görsel SP''ler oluşturuldu.'
GO
