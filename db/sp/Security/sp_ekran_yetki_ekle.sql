/*
    sp_ekran_yetki_ekle

    Purpose: Assign permission to screen
    Idempotent: Silently ignores if already assigned
    No audit logging (configuration changes)

    Parameters:
        @ekran_kod - Screen code (from kul_ekran)
        @yetki_id  - Permission ID
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_ekran_yetki_ekle', 'P') IS NOT NULL
    DROP PROCEDURE sp_ekran_yetki_ekle
GO

CREATE PROCEDURE [dbo].[sp_ekran_yetki_ekle]
    @ekran_kod NVARCHAR(50),
    @yetki_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if already assigned (idempotent)
    IF EXISTS (SELECT 1 FROM ekran_yetki
               WHERE ekran_kod = @ekran_kod AND yetki_id = @yetki_id)
        RETURN; -- Silently ignore duplicate

    INSERT INTO ekran_yetki (ekran_kod, yetki_id, created_at)
    VALUES (@ekran_kod, @yetki_id, GETDATE())
END
GO

PRINT 'Created sp_ekran_yetki_ekle'
GO
