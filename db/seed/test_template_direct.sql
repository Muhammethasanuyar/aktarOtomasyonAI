USE [AktarOtomasyon]
GO

PRINT '=========================================='
PRINT 'DİREKT ŞABLON KONTROLÜ'
PRINT '=========================================='
PRINT ''

-- 1. ai_sablon tablosunda URUN_DETAY_V1 var mı?
PRINT '1. AI_SABLON TABLOSU KONTROLÜ:'
PRINT '----------------------------------------'

IF EXISTS (SELECT 1 FROM [dbo].[ai_sablon] WHERE [sablon_kod] = 'URUN_DETAY_V1')
BEGIN
    PRINT '✓ URUN_DETAY_V1 tablodan direkt okunabilir:'
    SELECT
        [sablon_id],
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
    PRINT '✗ URUN_DETAY_V1 TABLODAN OKUNAMIYOR!'
    PRINT ''
    PRINT 'Tüm mevcut şablonlar:'
    SELECT [sablon_kod], [sablon_adi], [aktif] FROM [dbo].[ai_sablon]
END
PRINT ''

-- 2. Stored Procedure ile okuma
PRINT '2. STORED PROCEDURE İLE OKUMA:'
PRINT '----------------------------------------'

IF OBJECT_ID('sp_ai_sablon_getir', 'P') IS NOT NULL
BEGIN
    PRINT '✓ sp_ai_sablon_getir mevcut, çalıştırılıyor...'
    EXEC sp_ai_sablon_getir @sablon_kod = 'URUN_DETAY_V1'
END
ELSE
BEGIN
    PRINT '✗ sp_ai_sablon_getir STORED PROCEDURE BULUNAMADI!'
    PRINT ''
    PRINT 'ÇÖZÜM: Stored procedure''ü oluşturmanız gerekiyor.'
END
PRINT ''

-- 3. Aktif olmayan şablonlar var mı?
PRINT '3. AKTİF OLMAYAN ŞABLONLAR:'
PRINT '----------------------------------------'
SELECT
    [sablon_kod],
    [sablon_adi],
    [aktif]
FROM [dbo].[ai_sablon]
WHERE [aktif] = 0
PRINT ''

PRINT '=========================================='
PRINT 'TEST TAMAMLANDI'
PRINT '=========================================='
GO
