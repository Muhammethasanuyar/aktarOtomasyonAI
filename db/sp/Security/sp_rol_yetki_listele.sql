/*
    sp_rol_yetki_listele

    Purpose: Get role's permissions

    Parameters:
        @rol_id - Role ID

    Returns:
        List of permissions assigned to the role
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_rol_yetki_listele]
    @rol_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ry.rol_yetki_id,
        ry.rol_id,
        ry.yetki_id,
        y.yetki_kod,
        y.yetki_adi,
        y.aciklama,
        y.modul,
        ry.created_at
    FROM rol_yetki ry
    INNER JOIN yetki y ON ry.yetki_id = y.yetki_id
    WHERE ry.rol_id = @rol_id
    ORDER BY y.modul, y.yetki_kod
END
GO

PRINT 'Created sp_rol_yetki_listele'
GO
