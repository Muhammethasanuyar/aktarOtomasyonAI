/*
    sp_ekran_yetki_sil

    Purpose: Remove permission from screen

    Parameters:
        @ekran_kod - Screen code
        @yetki_id  - Permission ID

    Returns:
        Number of affected rows
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_ekran_yetki_sil', 'P') IS NOT NULL
    DROP PROCEDURE sp_ekran_yetki_sil
GO

CREATE PROCEDURE [dbo].[sp_ekran_yetki_sil]
    @ekran_kod NVARCHAR(50),
    @yetki_id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ekran_yetki
    WHERE ekran_kod = @ekran_kod AND yetki_id = @yetki_id

    SELECT @@ROWCOUNT AS affected_rows
END
GO

PRINT 'Created sp_ekran_yetki_sil'
GO
