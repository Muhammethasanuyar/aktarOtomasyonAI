-- =============================================
-- Common Stored Procedures
-- =============================================

USE [AktarOtomasyon]
GO

-- sp_kul_ekran_getir
IF OBJECT_ID('sp_kul_ekran_getir', 'P') IS NOT NULL DROP PROCEDURE sp_kul_ekran_getir;
GO

CREATE PROCEDURE [dbo].[sp_kul_ekran_getir]
    @ekran_kod NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [ekran_id],
        [ekran_kod],
        [menudeki_adi],
        [form_adi],
        [modul],
        [aciklama],
        [aktif]
    FROM [dbo].[kul_ekran]
    WHERE [ekran_kod] = @ekran_kod AND [aktif] = 1;
END
GO

-- sp_kul_ekran_versiyon_logla
IF OBJECT_ID('sp_kul_ekran_versiyon_logla', 'P') IS NOT NULL DROP PROCEDURE sp_kul_ekran_versiyon_logla;
GO

CREATE PROCEDURE [dbo].[sp_kul_ekran_versiyon_logla]
    @ekran_kod NVARCHAR(50),
    @versiyon NVARCHAR(20),
    @kullanici_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ekran_id INT;
    SELECT @ekran_id = [ekran_id] FROM [dbo].[kul_ekran] WHERE [ekran_kod] = @ekran_kod;
    
    IF @ekran_id IS NOT NULL
    BEGIN
        INSERT INTO [dbo].[kul_ekran_log] ([ekran_id], [kullanici_id], [versiyon])
        VALUES (@ekran_id, @kullanici_id, @versiyon);
    END
END
GO

PRINT 'Common SP''ler oluşturuldu.'
GO
