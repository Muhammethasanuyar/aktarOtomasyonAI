/*
==============================================================================
 003_verify_fix.sql

 AMAÇ: 002_fix_mojibake_ai_content.sql çalıştırıldıktan sonra
       düzeltmenin başarılı olduğunu doğrular.

 ÇALIŞTIRMA: SQL Server Management Studio'da doğrudan Execute edin.
 GEREKLİ HAK: SELECT yetkisi (sadece okuma)
==============================================================================
*/

USE [AktarOtomasyon]
GO

PRINT '==================================================='
PRINT 'MOJİBAKE DOĞRULAMA RAPORU'
PRINT 'Tarih: ' + CONVERT(VARCHAR, GETDATE(), 120)
PRINT '==================================================='
PRINT ''

-- 1. Toplam kayıt sayısı
DECLARE @toplam_kayit INT
SELECT @toplam_kayit = COUNT(*) FROM [ai_urun_icerik]

PRINT 'Toplam kayıt sayısı: ' + CAST(@toplam_kayit AS VARCHAR)
PRINT ''

-- 2. Hala mojibake içeren kayıtları say
DECLARE @kalan_bozuk INT
SELECT @kalan_bozuk = COUNT(DISTINCT [icerik_id])
FROM [ai_urun_icerik]
WHERE [icerik] LIKE '%Ä%'
   OR [icerik] LIKE '%Ã%'
   OR [icerik] LIKE '%Å%'
   OR [icerik] LIKE '%Ğ%'
   OR [icerik] LIKE '%İ%'

IF @kalan_bozuk = 0
BEGIN
    PRINT '=========================================='
    PRINT '✓✓✓ BAŞARILI! ✓✓✓'
    PRINT '=========================================='
    PRINT ''
    PRINT 'Tüm kayıtlar düzeltildi!'
    PRINT 'Mojibake tespit edilmedi.'
    PRINT ''
    PRINT 'SONRAKI ADIMLAR:'
    PRINT '1. Uygulamayı test edin:'
    PRINT '   - Ürün Kartı > AI sekmesi açın'
    PRINT '   - Mevcut AI içeriği olan ürünleri görüntüleyin'
    PRINT '   - Türkçe karakterler düzgün görünüyor mu?'
    PRINT ''
    PRINT '2. Yeni AI içerik üretin:'
    PRINT '   - Fayda/Kullanım/Uyarı/Kombinasyon butonlarını test edin'
    PRINT '   - Türkçe karakterler bozulmadan kaydediliyor mu?'
    PRINT ''
    PRINT '3. Birim dropdown kontrol edin:'
    PRINT '   - Ürün Kartı > Temel Bilgiler'
    PRINT '   - Birim seçenekleri doğru görünüyor mu?'
    PRINT '=========================================='
END
ELSE
BEGIN
    PRINT '=========================================='
    PRINT '⚠️ UYARI: MOJIBAKE KALDI!'
    PRINT '=========================================='
    PRINT ''
    PRINT 'Düzeltilemeyen kayıt sayısı: ' + CAST(@kalan_bozuk AS VARCHAR)
    PRINT ''
    PRINT 'Olası sebepler:'
    PRINT '1. 002_fix_mojibake_ai_content.sql çalıştırılmadı'
    PRINT '2. Farklı encoding sorunu var (Latin1 değil)'
    PRINT '3. Script başarısız oldu ve rollback edildi'
    PRINT ''
    PRINT 'Kalan bozuk kayıtlar (ilk 10):'
    PRINT '=========================================='

    SELECT TOP 10
        [icerik_id],
        [urun_id],
        [durum],
        [olusturma_tarih],
        CASE
            WHEN LEN([icerik]) > 100
            THEN LEFT([icerik], 100) + '...'
            ELSE [icerik]
        END AS [icerik_onizleme]
    FROM [ai_urun_icerik]
    WHERE [icerik] LIKE '%Ä%'
       OR [icerik] LIKE '%Ã%'
       OR [icerik] LIKE '%Å%'
       OR [icerik] LIKE '%Ğ%'
       OR [icerik] LIKE '%İ%'
    ORDER BY [icerik_id] DESC

    PRINT ''
    PRINT 'ÖNERĐ: 002_fix_mojibake_ai_content.sql tekrar çalıştırın.'
    PRINT '=========================================='
END

PRINT ''
PRINT '==================================================='
PRINT 'ÖZET:'
PRINT 'Toplam kayıt: ' + CAST(@toplam_kayit AS VARCHAR)
PRINT 'Temiz kayıt: ' + CAST((@toplam_kayit - @kalan_bozuk) AS VARCHAR)
PRINT 'Bozuk kayıt: ' + CAST(@kalan_bozuk AS VARCHAR)
PRINT '==================================================='

GO
