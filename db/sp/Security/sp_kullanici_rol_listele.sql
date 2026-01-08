/*
    sp_kullanici_rol_listele

    Purpose: Get user's roles

    Parameters:
        @kullanici_id - User ID

    Returns:
        List of roles assigned to the user (active roles only)
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_rol_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_rol_listele
GO

CREATE PROCEDURE [dbo].[sp_kullanici_rol_listele]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        kr.kullanici_rol_id,
        kr.kullanici_id,
        kr.rol_id,
        r.rol_adi,
        r.aciklama,
        r.aktif,
        kr.created_at
    FROM kullanici_rol kr
    INNER JOIN rol r ON kr.rol_id = r.rol_id
    WHERE kr.kullanici_id = @kullanici_id
        AND r.aktif = 1
    ORDER BY r.rol_adi
END
GO

PRINT 'Created sp_kullanici_rol_listele'
GO
