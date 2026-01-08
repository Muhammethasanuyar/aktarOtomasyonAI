/*
    sp_rol_listele

    Purpose: List roles with user and permission counts
    Returns list with counts for grid display

    Parameters:
        @aktif - Filter by active status (NULL for all)

    Returns:
        List of roles with user/permission counts
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_listele
GO

CREATE PROCEDURE [dbo].[sp_rol_listele]
    @aktif BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        r.rol_id,
        r.rol_adi,
        r.aciklama,
        r.aktif,
        r.created_at,
        -- Count of users in this role
        (SELECT COUNT(*) FROM kullanici_rol kr WHERE kr.rol_id = r.rol_id) AS kullanici_sayisi,
        -- Count of permissions
        (SELECT COUNT(*) FROM rol_yetki ry WHERE ry.rol_id = r.rol_id) AS yetki_sayisi
    FROM rol r
    WHERE (@aktif IS NULL OR r.aktif = @aktif)
    ORDER BY r.rol_adi
END
GO

PRINT 'Created sp_rol_listele'
GO
