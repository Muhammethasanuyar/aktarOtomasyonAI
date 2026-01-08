-- =============================================
-- Dashboard SP: Son Stok Hareketleri
-- Returns: Top 10 stock movements
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_dash_son_stok_hareket', 'P') IS NOT NULL
    DROP PROCEDURE sp_dash_son_stok_hareket;
GO

CREATE PROCEDURE [dbo].[sp_dash_son_stok_hareket]
    @limit INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@limit)
        h.hareket_id,
        h.urun_id,
        u.urun_adi,
        h.hareket_tip,
        h.miktar,
        h.hareket_tarih,
        h.aciklama,
        DATEDIFF(HOUR, h.hareket_tarih, GETDATE()) AS saat_once
    FROM stok_hareket h
    INNER JOIN urun u ON h.urun_id = u.urun_id
    ORDER BY h.hareket_tarih DESC;
END
GO

PRINT 'sp_dash_son_stok_hareket oluşturuldu.';
GO
