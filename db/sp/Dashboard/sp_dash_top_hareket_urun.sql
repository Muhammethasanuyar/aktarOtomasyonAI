-- =============================================
-- Dashboard SP: En Çok Hareket Gören Ürünler
-- Returns: Top N products by movement count (optional)
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_dash_top_hareket_urun', 'P') IS NOT NULL
    DROP PROCEDURE sp_dash_top_hareket_urun;
GO

CREATE PROCEDURE [dbo].[sp_dash_top_hareket_urun]
    @limit INT = 10,
    @gun INT = 30  -- Son N gün
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BaslangicTarih DATETIME = DATEADD(DAY, -@gun, GETDATE());

    SELECT TOP (@limit)
        u.urun_id,
        u.urun_adi,
        k.kategori_adi,
        COUNT(h.hareket_id) AS hareket_sayisi,
        SUM(CASE WHEN h.hareket_tip IN ('GIRIS', 'SAYIM') THEN h.miktar ELSE 0 END) AS toplam_giris,
        SUM(CASE WHEN h.hareket_tip IN ('CIKIS', 'DUZELTME') THEN h.miktar ELSE 0 END) AS toplam_cikis
    FROM urun u
    INNER JOIN urun_kategori k ON u.kategori_id = k.kategori_id
    INNER JOIN stok_hareket h ON u.urun_id = h.urun_id
    WHERE h.hareket_tarih >= @BaslangicTarih
      AND u.aktif = 1
    GROUP BY u.urun_id, u.urun_adi, k.kategori_adi
    ORDER BY COUNT(h.hareket_id) DESC;
END
GO

PRINT 'sp_dash_top_hareket_urun oluşturuldu.';
GO
