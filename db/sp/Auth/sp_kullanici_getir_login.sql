/*
    sp_kullanici_getir_login

    Purpose: Get user credentials for login authentication
    Returns: User record with password hash and salt for PBKDF2 verification

    Parameters:
        @kullanici_adi - Username to authenticate

    Returns:
        Single row with user info and password hash/salt, or empty if not found/inactive
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_getir_login', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_getir_login
GO

CREATE PROCEDURE [dbo].[sp_kullanici_getir_login]
    @kullanici_adi NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Return user with password hash/salt for authentication
    -- Only return active users
    SELECT
        k.kullanici_id,
        k.kullanici_adi,
        k.ad_soyad,
        k.email,
        k.parola_hash,
        k.parola_salt,
        k.parola_iterasyon,
        k.aktif,
        k.son_giris_tarih
    FROM [dbo].[kullanici] k
    WHERE k.kullanici_adi = @kullanici_adi
        AND k.aktif = 1
END
GO

PRINT 'Created sp_kullanici_getir_login'
GO
