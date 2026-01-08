-- =============================================
-- Dashboard SP: Bekleyen Sipariş Özeti
-- Returns: taslak_adet, gonderildi_adet, kismi_teslim_adet, bekleyen_tutar
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_dash_bekleyen_siparis_ozet', 'P') IS NOT NULL
    DROP PROCEDURE sp_dash_bekleyen_siparis_ozet;
GO

CREATE PROCEDURE [dbo].[sp_dash_bekleyen_siparis_ozet]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        COUNT(CASE WHEN durum = 'TASLAK' THEN 1 END) AS taslak_adet,
        COUNT(CASE WHEN durum = 'GONDERILDI' THEN 1 END) AS gonderildi_adet,
        COUNT(CASE WHEN durum = 'KISMI' THEN 1 END) AS kismi_teslim_adet,
        ISNULL(SUM(CASE WHEN durum IN ('TASLAK', 'GONDERILDI', 'KISMI') THEN toplam_tutar ELSE 0 END), 0) AS bekleyen_tutar
    FROM siparis
    WHERE durum IN ('TASLAK', 'GONDERILDI', 'KISMI');
END
GO

PRINT 'sp_dash_bekleyen_siparis_ozet oluşturuldu.';
GO
