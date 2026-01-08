-- =============================================
-- Dashboard SP: Son Bildirimler
-- Returns: Top 10 notifications (unread first, then recent)
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_dash_son_bildirimler', 'P') IS NOT NULL
    DROP PROCEDURE sp_dash_son_bildirimler;
GO

CREATE PROCEDURE [dbo].[sp_dash_son_bildirimler]
    @limit INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@limit)
        bildirim_id,
        bildirim_tip,
        baslik,
        icerik,
        referans_tip,
        referans_id,
        okundu,
        olusturma_tarih,
        DATEDIFF(MINUTE, olusturma_tarih, GETDATE()) AS dakika_once
    FROM bildirim
    ORDER BY
        okundu ASC,  -- Unread first
        olusturma_tarih DESC;
END
GO

PRINT 'sp_dash_son_bildirimler oluşturuldu.';
GO
