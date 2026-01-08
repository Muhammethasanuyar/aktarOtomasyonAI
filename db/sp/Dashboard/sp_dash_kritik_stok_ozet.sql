-- =============================================
-- Dashboard SP: Kritik Stok Özeti
-- Returns: kritik_adet, acil_adet, toplam_urun
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_dash_kritik_stok_ozet', 'P') IS NOT NULL
    DROP PROCEDURE sp_dash_kritik_stok_ozet;
GO

CREATE PROCEDURE [dbo].[sp_dash_kritik_stok_ozet]
AS
BEGIN
    SET NOCOUNT ON;

    -- CTE for stock calculation (optimized)
    WITH StokOzet AS (
        SELECT
            h.urun_id,
            SUM(CASE WHEN h.hareket_tip IN ('GIRIS', 'SAYIM') THEN h.miktar ELSE -h.miktar END) AS mevcut_stok
        FROM stok_hareket h
        GROUP BY h.urun_id
    )
    SELECT
        COUNT(CASE WHEN ISNULL(so.mevcut_stok, 0) <= sa.kritik_stok THEN 1 END) AS kritik_adet,
        COUNT(CASE WHEN ISNULL(so.mevcut_stok, 0) > sa.kritik_stok
                    AND ISNULL(so.mevcut_stok, 0) <= sa.emniyet_stok THEN 1 END) AS acil_adet,
        COUNT(*) AS toplam_urun
    FROM urun u
    INNER JOIN urun_stok_ayar sa ON u.urun_id = sa.urun_id
    LEFT JOIN StokOzet so ON u.urun_id = so.urun_id
    WHERE u.aktif = 1;
END
GO

PRINT 'sp_dash_kritik_stok_ozet oluşturuldu.';
GO
