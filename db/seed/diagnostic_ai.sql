USE [AktarOtomasyon]
GO

PRINT '=========================================='
PRINT 'AI SİSTEM DİAGNOSTİK KONTROLÜ'
PRINT '=========================================='
PRINT ''

-- 1. AI Şablon Kontrolü
PRINT '1. AI ŞABLON KONTROLÜ:'
PRINT '----------------------------------------'
IF EXISTS (SELECT 1 FROM [dbo].[ai_sablon] WHERE [sablon_kod] = 'URUN_DETAY_V1')
BEGIN
    PRINT '✓ URUN_DETAY_V1 şablonu MEVCUT'
    SELECT
        [sablon_kod],
        [sablon_adi],
        [aktif],
        [olusturma_tarih],
        LEN([prompt_sablonu]) AS PromptUzunluk
    FROM [dbo].[ai_sablon]
    WHERE [sablon_kod] = 'URUN_DETAY_V1'
END
ELSE
BEGIN
    PRINT '✗ URUN_DETAY_V1 şablonu BULUNAMADI!'
    PRINT '  >> ÇÖZÜM: 004_seed_urun_detay_template.sql dosyasını çalıştırın'
END
PRINT ''

-- 2. Tüm Şablonlar
PRINT '2. MEVCUT TÜM ŞABLONLAR:'
PRINT '----------------------------------------'
SELECT
    [sablon_kod],
    [sablon_adi],
    [aktif]
FROM [dbo].[ai_sablon]
ORDER BY [olusturma_tarih] DESC
PRINT ''

-- 3. Stored Procedure Kontrolü
PRINT '3. STORED PROCEDURE KONTROLÜ:'
PRINT '----------------------------------------'
IF OBJECT_ID('sp_ai_sablon_getir', 'P') IS NOT NULL
    PRINT '✓ sp_ai_sablon_getir MEVCUT'
ELSE
    PRINT '✗ sp_ai_sablon_getir BULUNAMADI!'

IF OBJECT_ID('sp_ai_icerik_getir', 'P') IS NOT NULL
    PRINT '✓ sp_ai_icerik_getir MEVCUT'
ELSE
    PRINT '✗ sp_ai_icerik_getir BULUNAMADI!'

IF OBJECT_ID('sp_ai_icerik_kaydet', 'P') IS NOT NULL
    PRINT '✓ sp_ai_icerik_kaydet MEVCUT'
ELSE
    PRINT '✗ sp_ai_icerik_kaydet BULUNAMADI!'

IF OBJECT_ID('sp_ai_urun_bilgi_getir', 'P') IS NOT NULL
    PRINT '✓ sp_ai_urun_bilgi_getir MEVCUT'
ELSE
    PRINT '✗ sp_ai_urun_bilgi_getir BULUNAMADI!'
PRINT ''

-- 4. Test Ürün Kontrolü
PRINT '4. TEST ÜRÜN KONTROLÜ (İlk 5 ürün):'
PRINT '----------------------------------------'
SELECT TOP 5
    [urun_id],
    [urun_kod],
    [urun_adi],
    [aktif]
FROM [dbo].[urun]
WHERE [aktif] = 1
ORDER BY [urun_id]
PRINT ''

-- 5. Mevcut AI İçerik Kontrolü
PRINT '5. MEVCUT AI İÇERİKLER:'
PRINT '----------------------------------------'
SELECT
    COUNT(*) AS ToplamIcerik,
    SUM(CASE WHEN [durum] = 'AKTIF' THEN 1 ELSE 0 END) AS AktifIcerik,
    SUM(CASE WHEN [durum] = 'TASLAK' THEN 1 ELSE 0 END) AS TaslakIcerik
FROM [dbo].[ai_icerik]
PRINT ''

PRINT '=========================================='
PRINT 'DİAGNOSTİK TAMAMLANDI'
PRINT '=========================================='
GO
