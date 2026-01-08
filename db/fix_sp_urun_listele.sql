USE AktarOtomasyon;
GO

ALTER PROCEDURE [dbo].[sp_urun_listele]
    @aktif BIT = 1,
    @kategori_id INT = NULL,
    @arama NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.[urun_id], u.[urun_kod], u.[urun_adi],
        u.[kategori_id], k.[kategori_adi],
        u.[birim_id], b.[birim_adi],
        u.[alis_fiyati], u.[satis_fiyati],
        u.[barkod], u.[aktif],
        g.[gorsel_path] as [ana_gorsel_path]
    FROM [dbo].[urun] u
    LEFT JOIN [dbo].[urun_kategori] k ON u.[kategori_id] = k.[kategori_id]
    LEFT JOIN [dbo].[urun_birim] b ON u.[birim_id] = b.[birim_id]
    LEFT JOIN [dbo].[urun_gorsel] g ON u.[urun_id] = g.[urun_id] AND g.[ana_gorsel] = 1
    WHERE (@aktif IS NULL OR u.[aktif] = @aktif)
      AND (@kategori_id IS NULL OR u.[kategori_id] = @kategori_id)
      AND (@arama IS NULL OR u.[urun_adi] LIKE '%' + @arama + '%' OR u.[urun_kod] LIKE '%' + @arama + '%')
    ORDER BY u.[urun_adi];
END
GO
