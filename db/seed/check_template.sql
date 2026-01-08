USE [AktarOtomasyon]
GO

-- URUN_DETAY_V1 şablonunun varlığını kontrol et
SELECT
    [sablon_id],
    [sablon_kod],
    [sablon_adi],
    [aktif],
    [olusturma_tarih],
    LEN([prompt_sablonu]) AS PromptUzunluk
FROM [dbo].[ai_sablon]
WHERE [sablon_kod] = 'URUN_DETAY_V1'

-- Tüm şablonları listele
SELECT
    [sablon_kod],
    [sablon_adi],
    [aktif]
FROM [dbo].[ai_sablon]
ORDER BY [olusturma_tarih] DESC
GO
