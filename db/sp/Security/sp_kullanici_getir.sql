/*
    sp_kullanici_getir

    Purpose: Get user by ID for editing
    Does NOT return password hash/salt (use sp_kullanici_getir_login for auth)

    Parameters:
        @kullanici_id - User ID to retrieve

    Returns:
        Single user record (without password hash/salt)
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_getir
GO

CREATE PROCEDURE [dbo].[sp_kullanici_getir]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        kullanici_id,
        kullanici_adi,
        ad_soyad,
        email,
        aktif,
        son_giris_tarih,
        created_at,
        created_by,
        updated_at,
        updated_by
    FROM [dbo].[kullanici]
    WHERE kullanici_id = @kullanici_id
END
GO

PRINT 'Created sp_kullanici_getir'
GO
