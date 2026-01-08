-- =============================================
-- Tedarikçi Listele Stored Procedure
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_tedarikci_listele', 'P') IS NOT NULL DROP PROCEDURE sp_tedarikci_listele;
GO

CREATE PROCEDURE [dbo].[sp_tedarikci_listele]
    @aktif BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        tedarikci_id,
        tedarikci_kod,
        tedarikci_adi,
        yetkili,
        telefon,
        email,
        adres,
        aktif,
        olusturma_tarih
    FROM [dbo].[tedarikci]
    WHERE (@aktif IS NULL OR aktif = @aktif)
    ORDER BY tedarikci_adi
END
GO
