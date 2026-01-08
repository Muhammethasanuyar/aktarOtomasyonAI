/*
    sp_kullanici_yetki_kontrol

    Purpose: Check if user has specific permission
    CRITICAL: This SP is used by UI for permission checks - must be FAST (<50ms)

    Parameters:
        @kullanici_id - User ID
        @yetki_kod    - Permission code to check

    Returns:
        1 if user has permission (via active roles), 0 if not

    Performance:
        - Uses indexes: IX_kullanici_rol_kullanici, IX_rol_yetki_rol, IX_yetki_yetki_kod
        - EXISTS clause stops at first match (optimal)
        - Only checks active roles
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_yetki_kontrol', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_yetki_kontrol
GO

CREATE PROCEDURE [dbo].[sp_kullanici_yetki_kontrol]
    @kullanici_id INT,
    @yetki_kod NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Return 1 if user has permission, 0 if not
    IF EXISTS (
        SELECT 1
        FROM kullanici_rol kr
        INNER JOIN rol r ON kr.rol_id = r.rol_id AND r.aktif = 1
        INNER JOIN rol_yetki ry ON r.rol_id = ry.rol_id
        INNER JOIN yetki y ON ry.yetki_id = y.yetki_id
        WHERE kr.kullanici_id = @kullanici_id
            AND y.yetki_kod = @yetki_kod
    )
        SELECT 1 AS has_permission
    ELSE
        SELECT 0 AS has_permission
END
GO

PRINT 'Created sp_kullanici_yetki_kontrol'
GO
