/*
    sp_kullanici_yetki_listele

    Purpose: Get user's EFFECTIVE permissions (via roles)
    CRITICAL: This SP is used by UI for permission checks - must be FAST (<100ms)

    Parameters:
        @kullanici_id - User ID

    Returns:
        DISTINCT list of all permissions user has through their roles

    Performance:
        - Uses indexes: IX_kullanici_rol_kullanici, IX_rol_yetki_rol
        - JOINs only active roles
        - Returns DISTINCT to avoid duplicates
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_kullanici_yetki_listele]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Return DISTINCT permissions from all user's active roles
    SELECT DISTINCT
        y.yetki_id,
        y.yetki_kod,
        y.yetki_adi,
        y.aciklama,
        y.modul
    FROM kullanici_rol kr
    INNER JOIN rol r ON kr.rol_id = r.rol_id AND r.aktif = 1
    INNER JOIN rol_yetki ry ON r.rol_id = ry.rol_id
    INNER JOIN yetki y ON ry.yetki_id = y.yetki_id
    WHERE kr.kullanici_id = @kullanici_id
    ORDER BY y.modul, y.yetki_kod
END
GO

PRINT 'Created sp_kullanici_yetki_listele'
GO
