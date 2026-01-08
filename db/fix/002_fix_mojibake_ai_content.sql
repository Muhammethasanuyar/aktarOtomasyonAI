/*
==============================================================================
 002_fix_mojibake_ai_content.sql

 AMAÇ: ai_urun_icerik tablosundaki mojibake (bozuk Türkçe karakter) kayıtlarını düzeltir.

 ⚠️ UYARI: Bu script veritabanını değiştirir! Mutlaka önceden backup alın!

 ÖNCESİNDE ÇALIŞTIR:
   BACKUP DATABASE [AktarOtomasyon]
   TO DISK = 'C:\Backup\AktarOtomasyon_PreFix.bak'
   WITH FORMAT, INIT, COMPRESSION

 GEREKLİ HAK: UPDATE yetkisi
 ÇALIŞTIRMA: SQL Server Management Studio'da doğrudan Execute edin.
==============================================================================
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON
GO

PRINT '==================================================='
PRINT 'MOJİBAKE DÜZELTME - ai_urun_icerik tablosu'
PRINT 'Başlangıç: ' + CONVERT(VARCHAR, GETDATE(), 120)
PRINT '==================================================='
PRINT ''

-- Transaction başlat (hata olursa rollback yapabilmek için)
BEGIN TRANSACTION

BEGIN TRY
    -- Düzeltilecek kayıt sayısını say
    DECLARE @etkilenecek_kayit INT
    SELECT @etkilenecek_kayit = COUNT(DISTINCT [icerik_id])
    FROM [ai_urun_icerik]
    WHERE [icerik] LIKE '%Ä%'
       OR [icerik] LIKE '%Ã%'
       OR [icerik] LIKE '%Å%'
       OR [icerik] LIKE '%Ğ%'
       OR [icerik] LIKE '%İ%'

    PRINT 'Düzeltilecek kayıt sayısı: ' + CAST(@etkilenecek_kayit AS VARCHAR)
    PRINT ''

    IF @etkilenecek_kayit = 0
    BEGIN
        PRINT '✓ Düzeltilecek kayıt yok!'
        ROLLBACK TRANSACTION
        RETURN
    END

    PRINT 'Düzeltme başlatılıyor...'
    PRINT ''

    -- Mojibake karakterlerini düzelt
    -- UTF-8 → Latin1 → UTF-8 dönüşümü sırasında oluşan bozuk karakterleri düzelt
    UPDATE [ai_urun_icerik]
    SET [icerik] =
        -- Küçük harfler
        REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
        -- Büyük harfler (önce büyükler, sonra küçükler - çakışma olmaması için)
        REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
            [icerik]
            -- Büyük harfler (6 karakter)
        , 'Ä°', 'İ')     -- İ (I noktalı büyük)
        , 'Äž', 'Ğ')     -- Ğ
        , 'Ã‡', 'Ç')     -- Ç
        , 'Ã–', 'Ö')     -- Ö
        , 'ÅŸ', 'Ş')     -- Ş (iki baytlı)
        , 'Ãœ', 'Ü')     -- Ü
            -- Küçük harfler (7 karakter - ı ekstra)
        , 'Ä±', 'ı')     -- ı (i noktasız küçük)
        , 'ÄŸ', 'ğ')     -- ğ
        , 'Ã§', 'ç')     -- ç
        , 'Ã¶', 'ö')     -- ö
        , 'ÅŸ', 'ş')     -- ş (iki baytlı)
        , 'ü', 'ü')     -- ü
    WHERE [icerik] LIKE '%Ä%'
       OR [icerik] LIKE '%Ã%'
       OR [icerik] LIKE '%Å%'
       OR [icerik] LIKE '%Ğ%'
       OR [icerik] LIKE '%İ%'

    DECLARE @duzeltilen_kayit INT = @@ROWCOUNT

    PRINT 'Düzeltilen kayıt sayısı: ' + CAST(@duzeltilen_kayit AS VARCHAR)
    PRINT ''

    -- Kontrol: Hala mojibake var mı?
    DECLARE @kalan_bozuk INT
    SELECT @kalan_bozuk = COUNT(DISTINCT [icerik_id])
    FROM [ai_urun_icerik]
    WHERE [icerik] LIKE '%Ä%'
       OR [icerik] LIKE '%Ã%'
       OR [icerik] LIKE '%Å%'
       OR [icerik] LIKE '%Ğ%'
       OR [icerik] LIKE '%İ%'

    IF @kalan_bozuk > 0
    BEGIN
        PRINT '⚠️ UYARI: ' + CAST(@kalan_bozuk AS VARCHAR) + ' kayıtta hala mojibake var!'
        PRINT 'Bu kayıtlar farklı bir encoding sorununa sahip olabilir.'
        PRINT ''
        PRINT 'Örnek kayıtlar:'

        SELECT TOP 5
            [icerik_id],
            [urun_id],
            LEFT([icerik], 100) AS [icerik_onizleme]
        FROM [ai_urun_icerik]
        WHERE [icerik] LIKE '%Ä%'
           OR [icerik] LIKE '%Ã%'
           OR [icerik] LIKE '%Å%'
           OR [icerik] LIKE '%Ğ%'
           OR [icerik] LIKE '%İ%'
    END
    ELSE
    BEGIN
        PRINT '✓ Tüm mojibake kayıtları başarıyla düzeltildi!'
    END

    PRINT ''
    PRINT '==================================================='
    PRINT 'Transaction commit ediliyor...'

    COMMIT TRANSACTION

    PRINT 'Düzeltme başarıyla tamamlandı!'
    PRINT 'Bitiş: ' + CONVERT(VARCHAR, GETDATE(), 120)
    PRINT '==================================================='
    PRINT ''
    PRINT 'SONRAKİ ADIM:'
    PRINT '1. Doğrulama scripti çalıştır: 003_verify_fix.sql'
    PRINT '2. Uygulamada test et: Ürün Kartı > AI sekmesi'

END TRY
BEGIN CATCH
    -- Hata olursa rollback
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

    PRINT ''
    PRINT '==================================================='
    PRINT '❌ HATA OLUŞTU! Transaction rollback edildi.'
    PRINT '==================================================='
    PRINT 'Hata Mesajı: ' + ERROR_MESSAGE()
    PRINT 'Hata Numarası: ' + CAST(ERROR_NUMBER() AS VARCHAR)
    PRINT 'Hata Satırı: ' + CAST(ERROR_LINE() AS VARCHAR)
    PRINT ''
    PRINT 'Veritabanı değiştirilmedi. Güvenli.'
    PRINT '==================================================='

    -- Hatayı tekrar fırlat (opsiyonel)
    -- THROW;
END CATCH

GO

SET NOCOUNT OFF
GO
