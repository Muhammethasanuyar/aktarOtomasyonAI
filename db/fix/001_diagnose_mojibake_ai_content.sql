/*
==============================================================================
 001_diagnose_mojibake_ai_content.sql

 AMAÇ: ai_urun_icerik tablosundaki mojibake (bozuk Türkçe karakter) kayıtları tespit eder.

 ÇALIŞTIRMA: SQL Server Management Studio'da doğrudan Execute edin.
 GEREKLİ HAK: SELECT yetkisi (sadece okuma, veri değiştirme yok)
==============================================================================
*/

USE [AktarOtomasyon]
GO

PRINT '==================================================='
PRINT 'MOJİBAKE TANI RAPORU - ai_urun_icerik tablosu'
PRINT 'Tarih: ' + CONVERT(VARCHAR, GETDATE(), 120)
PRINT '==================================================='
PRINT ''

-- 1. Toplam kayıt sayısı
DECLARE @toplam_kayit INT
SELECT @toplam_kayit = COUNT(*) FROM [ai_urun_icerik]

PRINT 'Toplam kayıt sayısı: ' + CAST(@toplam_kayit AS VARCHAR)
PRINT ''

-- 2. Mojibake içeren kayıtları say
-- Mojibake karakterleri: Ä (Latin1: 196), à (Latin1: 195), Å (Latin1: 197)
DECLARE @bozuk_kayit INT
SELECT @bozuk_kayit = COUNT(DISTINCT [icerik_id])
FROM [ai_urun_icerik]
WHERE [icerik] LIKE '%Ä%'    -- ğ → ÄŸ
   OR [icerik] LIKE '%Ã%'    -- ç → Ã§, ı → ±, ö → ö, ş → ÅŸ, ü → ü
   OR [icerik] LIKE '%Å%'    -- ş → ÅŸ
   OR [icerik] LIKE '%Ğ%'    -- Ğ → Ä° (büyük)
   OR [icerik] LIKE '%İ%'    -- İ → Ä° (büyük I noktalı)

PRINT 'Mojibake içeren kayıt sayısı: ' + CAST(@bozuk_kayit AS VARCHAR)
PRINT 'Düzeltme gerekmeyen kayıt: ' + CAST((@toplam_kayit - @bozuk_kayit) AS VARCHAR)
PRINT ''

-- 3. Mojibake örnekleri (ilk 10 kayıt)
IF @bozuk_kayit > 0
BEGIN
    PRINT '==================================================='
    PRINT 'MOJIBAKE İÇEREN KAYITLAR (İlk 10 örnek):'
    PRINT '==================================================='
    PRINT ''

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
    PRINT 'NOT: İçerik 100 karakterden uzunsa "..." ile kesilmiştir.'
END
ELSE
BEGIN
    PRINT '✓ Tüm kayıtlar temiz! Mojibake tespit edilmedi.'
END

PRINT ''
PRINT '==================================================='
PRINT 'SONRAKİ ADIM:'
IF @bozuk_kayit > 0
BEGIN
    PRINT '1. Veritabanını yedekle (ZORUNLU!):'
    PRINT '   BACKUP DATABASE [AktarOtomasyon]'
    PRINT '   TO DISK = ''C:\Backup\AktarOtomasyon_PreFix.bak'''
    PRINT '   WITH FORMAT, INIT, COMPRESSION'
    PRINT ''
    PRINT '2. Düzeltme scripti çalıştır:'
    PRINT '   002_fix_mojibake_ai_content.sql'
END
ELSE
BEGIN
    PRINT 'Düzeltme gerekmez.'
END
PRINT '==================================================='

GO
