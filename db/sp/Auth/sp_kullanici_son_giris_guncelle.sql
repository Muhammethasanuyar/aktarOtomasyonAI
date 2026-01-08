/*
    sp_kullanici_son_giris_guncelle

    Purpose: Update user's last login timestamp
    Called after successful authentication

    Parameters:
        @kullanici_id - User ID to update

    Returns:
        Number of affected rows
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_son_giris_guncelle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_son_giris_guncelle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_son_giris_guncelle]
    @kullanici_id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[kullanici]
    SET son_giris_tarih = GETDATE()
    WHERE kullanici_id = @kullanici_id

    SELECT @@ROWCOUNT AS affected_rows
END
GO

PRINT 'Created sp_kullanici_son_giris_guncelle'
GO
