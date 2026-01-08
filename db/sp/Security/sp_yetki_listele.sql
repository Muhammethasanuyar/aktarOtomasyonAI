/*
    sp_yetki_listele

    Purpose: List permissions with optional module filter
    Permissions are read-only (managed via seed scripts only)

    Parameters:
        @modul - Filter by module (NULL for all)

    Returns:
        List of permissions
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_yetki_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_yetki_listele
GO

CREATE PROCEDURE [dbo].[sp_yetki_listele]
    @modul NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        yetki_id,
        yetki_kod,
        yetki_adi,
        aciklama,
        modul,
        created_at
    FROM yetki
    WHERE (@modul IS NULL OR modul = @modul)
    ORDER BY modul, yetki_kod
END
GO

PRINT 'Created sp_yetki_listele'
GO
