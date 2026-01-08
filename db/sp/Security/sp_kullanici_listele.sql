/*
    sp_kullanici_listele

    Purpose: List users with filters and role names
    Returns list with comma-separated role names for grid display

    Parameters:
        @aktif  - Filter by active status (NULL for all)
        @arama  - Search in kullanici_adi, ad_soyad, email (optional)

    Returns:
        List of users with role names
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_listele
GO

CREATE PROCEDURE [dbo].[sp_kullanici_listele]
    @aktif BIT = NULL,
    @arama NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        k.kullanici_id,
        k.kullanici_adi,
        k.ad_soyad,
        k.email,
        k.aktif,
        k.son_giris_tarih,
        k.created_at,
        -- Role names (comma-separated using STUFF + FOR XML PATH)
        STUFF((
            SELECT ', ' + r.rol_adi
            FROM kullanici_rol kr
            INNER JOIN rol r ON kr.rol_id = r.rol_id
            WHERE kr.kullanici_id = k.kullanici_id
                AND r.aktif = 1
            ORDER BY r.rol_adi
            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''
        ) AS roller
    FROM [dbo].[kullanici] k
    WHERE (@aktif IS NULL OR k.aktif = @aktif)
        AND (
            @arama IS NULL
            OR k.kullanici_adi LIKE '%' + @arama + '%'
            OR k.ad_soyad LIKE '%' + @arama + '%'
            OR k.email LIKE '%' + @arama + '%'
        )
    ORDER BY k.kullanici_adi
END
GO

PRINT 'Created sp_kullanici_listele'
GO
