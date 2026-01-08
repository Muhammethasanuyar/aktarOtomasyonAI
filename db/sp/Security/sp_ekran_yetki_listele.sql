/*
    sp_ekran_yetki_listele

    Purpose: Get screen's required permissions

    Parameters:
        @ekran_kod - Screen code

    Returns:
        List of permissions required for the screen
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_ekran_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_ekran_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_ekran_yetki_listele]
    @ekran_kod NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ey.ekran_yetki_id,
        ey.ekran_kod,
        ey.yetki_id,
        y.yetki_kod,
        y.yetki_adi,
        y.aciklama,
        y.modul,
        ey.created_at
    FROM ekran_yetki ey
    INNER JOIN yetki y ON ey.yetki_id = y.yetki_id
    WHERE ey.ekran_kod = @ekran_kod
    ORDER BY y.modul, y.yetki_kod
END
GO

PRINT 'Created sp_ekran_yetki_listele'
GO
