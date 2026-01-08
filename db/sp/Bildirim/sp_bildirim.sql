-- =============================================
-- Bildirim Stored Procedures
-- =============================================

USE [AktarOtomasyon]
GO

-- sp_bildirim_ekle
IF OBJECT_ID('sp_bildirim_ekle', 'P') IS NOT NULL DROP PROCEDURE sp_bildirim_ekle;
GO

CREATE PROCEDURE [dbo].[sp_bildirim_ekle]
    @bildirim_tip NVARCHAR(50),
    @baslik NVARCHAR(200),
    @icerik NVARCHAR(MAX) = NULL,
    @referans_tip NVARCHAR(50) = NULL,
    @referans_id INT = NULL,
    @kullanici_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[bildirim] ([bildirim_tip], [baslik], [icerik], [referans_tip], [referans_id], [kullanici_id])
    VALUES (@bildirim_tip, @baslik, @icerik, @referans_tip, @referans_id, @kullanici_id);
END
GO

-- sp_bildirim_listele
IF OBJECT_ID('sp_bildirim_listele', 'P') IS NOT NULL DROP PROCEDURE sp_bildirim_listele;
GO

CREATE PROCEDURE [dbo].[sp_bildirim_listele]
    @kullanici_id INT = NULL,
    @okundu BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT [bildirim_id], [bildirim_tip], [baslik], [icerik],
           [referans_tip], [referans_id], [okundu], [olusturma_tarih]
    FROM [dbo].[bildirim]
    WHERE (@kullanici_id IS NULL OR [kullanici_id] = @kullanici_id OR [kullanici_id] IS NULL)
      AND (@okundu IS NULL OR [okundu] = @okundu)
    ORDER BY [olusturma_tarih] DESC;
END
GO

-- sp_bildirim_okundu
IF OBJECT_ID('sp_bildirim_okundu', 'P') IS NOT NULL DROP PROCEDURE sp_bildirim_okundu;
GO

CREATE PROCEDURE [dbo].[sp_bildirim_okundu]
    @bildirim_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[bildirim]
    SET [okundu] = 1, [okunma_tarih] = GETDATE()
    WHERE [bildirim_id] = @bildirim_id;
END
GO

PRINT 'Bildirim SP''ler oluşturuldu.'
GO
