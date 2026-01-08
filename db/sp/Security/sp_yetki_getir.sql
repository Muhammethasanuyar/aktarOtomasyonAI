/*
    sp_yetki_getir

    Purpose: Get permission by ID

    Parameters:
        @yetki_id - Permission ID to retrieve

    Returns:
        Single permission record
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_yetki_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_yetki_getir
GO

CREATE PROCEDURE [dbo].[sp_yetki_getir]
    @yetki_id INT
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
    WHERE yetki_id = @yetki_id
END
GO

PRINT 'Created sp_yetki_getir'
GO
