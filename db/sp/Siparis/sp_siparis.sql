-- =============================================
-- Siparis Stored Procedures
-- =============================================

USE [AktarOtomasyon]
GO

-- sp_siparis_taslak_olustur
IF OBJECT_ID('sp_siparis_taslak_olustur', 'P') IS NOT NULL DROP PROCEDURE sp_siparis_taslak_olustur;
GO

CREATE PROCEDURE [dbo].[sp_siparis_taslak_olustur]
    @tedarikci_id INT,
    @kullanici_id INT = NULL,
    @siparis_id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @siparis_no NVARCHAR(20);
    SET @siparis_no = 'SIP' + FORMAT(GETDATE(), 'yyyyMMdd') + RIGHT('0000' + CAST((SELECT ISNULL(MAX(siparis_id), 0) + 1 FROM siparis) AS VARCHAR), 4);
    
    INSERT INTO [dbo].[siparis] ([siparis_no], [tedarikci_id], [kullanici_id])
    VALUES (@siparis_no, @tedarikci_id, @kullanici_id);
    
    SET @siparis_id = SCOPE_IDENTITY();
END
GO

-- sp_siparis_satir_ekle
IF OBJECT_ID('sp_siparis_satir_ekle', 'P') IS NOT NULL DROP PROCEDURE sp_siparis_satir_ekle;
GO

CREATE PROCEDURE [dbo].[sp_siparis_satir_ekle]
    @siparis_id INT,
    @urun_id INT,
    @miktar DECIMAL(18,2),
    @birim_fiyat DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @tutar DECIMAL(18,2) = @miktar * @birim_fiyat;
    
    INSERT INTO [dbo].[siparis_satir] ([siparis_id], [urun_id], [miktar], [birim_fiyat], [tutar])
    VALUES (@siparis_id, @urun_id, @miktar, @birim_fiyat, @tutar);
    
    -- Toplam güncelle
    UPDATE [dbo].[siparis]
    SET [toplam_tutar] = (SELECT SUM([tutar]) FROM [dbo].[siparis_satir] WHERE [siparis_id] = @siparis_id)
    WHERE [siparis_id] = @siparis_id;
END
GO

-- sp_siparis_satir_listele
IF OBJECT_ID('sp_siparis_satir_listele', 'P') IS NOT NULL DROP PROCEDURE sp_siparis_satir_listele;
GO

CREATE PROCEDURE [dbo].[sp_siparis_satir_listele]
    @siparis_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ss.[satir_id], ss.[siparis_id], ss.[urun_id],
        u.[urun_adi], ss.[miktar], ss.[birim_fiyat], ss.[tutar], ss.[teslim_miktar]
    FROM [dbo].[siparis_satir] ss
    INNER JOIN [dbo].[urun] u ON ss.[urun_id] = u.[urun_id]
    WHERE ss.[siparis_id] = @siparis_id
    ORDER BY ss.[satir_id];
END
GO

-- sp_siparis_listele
-- OPTIMIZED: Added pagination support for large datasets
IF OBJECT_ID('sp_siparis_listele', 'P') IS NOT NULL DROP PROCEDURE sp_siparis_listele;
GO

CREATE PROCEDURE [dbo].[sp_siparis_listele]
    @durum NVARCHAR(20) = NULL,
    @page_number INT = 1,
    @page_size INT = 50
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @offset INT = (@page_number - 1) * @page_size;

    SELECT
        s.[siparis_id], s.[siparis_no], s.[tedarikci_id],
        t.[tedarikci_adi], s.[siparis_tarih], s.[beklenen_teslim_tarih],
        s.[durum], s.[toplam_tutar]
    FROM [dbo].[siparis] s
    INNER JOIN [dbo].[tedarikci] t ON s.[tedarikci_id] = t.[tedarikci_id]
    WHERE (@durum IS NULL OR s.[durum] = @durum)
    ORDER BY s.[siparis_tarih] DESC
    OFFSET @offset ROWS
    FETCH NEXT @page_size ROWS ONLY;
END
GO

-- sp_siparis_durum_guncelle
IF OBJECT_ID('sp_siparis_durum_guncelle', 'P') IS NOT NULL DROP PROCEDURE sp_siparis_durum_guncelle;
GO

CREATE PROCEDURE [dbo].[sp_siparis_durum_guncelle]
    @siparis_id INT,
    @durum NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[siparis]
    SET [durum] = @durum
    WHERE [siparis_id] = @siparis_id;
    
    IF @durum = 'TAMAMLANDI'
    BEGIN
        UPDATE [dbo].[siparis] SET [teslim_tarih] = GETDATE() WHERE [siparis_id] = @siparis_id;
    END
END
GO

PRINT 'Siparis SP''ler oluşturuldu.'
GO
