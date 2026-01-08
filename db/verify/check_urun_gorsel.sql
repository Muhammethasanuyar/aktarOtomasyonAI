/*
==============================================================================
 check_urun_gorsel.sql

 AMAÇ: Ürün görselleri tablosunu kontrol eder ve katalog görünümündeki
       görsel yükleme sorununu teşhis eder.
==============================================================================
*/

USE [AktarOtomasyon]
GO

PRINT '==================================================='
PRINT 'ÜRÜN GÖRSEL KONTROL RAPORU'
PRINT 'Tarih: ' + CONVERT(VARCHAR, GETDATE(), 120)
PRINT '==================================================='
PRINT ''

-- 1. Toplam ürün ve görsel sayısı
DECLARE @toplam_urun INT
DECLARE @toplam_gorsel INT
DECLARE @ana_gorsel_sayisi INT

SELECT @toplam_urun = COUNT(*) FROM [urun] WHERE [aktif] = 1
SELECT @toplam_gorsel = COUNT(*) FROM [urun_gorsel]
SELECT @ana_gorsel_sayisi = COUNT(*) FROM [urun_gorsel] WHERE [ana_gorsel] = 1

PRINT 'Aktif ürün sayısı: ' + CAST(@toplam_urun AS VARCHAR)
PRINT 'Toplam görsel sayısı: ' + CAST(@toplam_gorsel AS VARCHAR)
PRINT 'Ana görsel sayısı: ' + CAST(@ana_gorsel_sayisi AS VARCHAR)
PRINT ''

-- 2. Görseli olmayan ürünler
DECLARE @gorselsiz_urun INT
SELECT @gorselsiz_urun = COUNT(*)
FROM [urun] u
WHERE u.[aktif] = 1
  AND NOT EXISTS (SELECT 1 FROM [urun_gorsel] ug WHERE ug.[urun_id] = u.[urun_id])

PRINT 'Görseli olmayan aktif ürün sayısı: ' + CAST(@gorselsiz_urun AS VARCHAR)

IF @gorselsiz_urun > 0
BEGIN
    PRINT ''
    PRINT 'Görseli olmayan ürünler (ilk 10):'
    PRINT '-------------------------------------------'

    SELECT TOP 10
        u.[urun_id],
        u.[urun_kod],
        u.[urun_adi]
    FROM [urun] u
    WHERE u.[aktif] = 1
      AND NOT EXISTS (SELECT 1 FROM [urun_gorsel] ug WHERE ug.[urun_id] = u.[urun_id])
    ORDER BY u.[urun_adi]
END

PRINT ''
PRINT '==================================================='
PRINT 'ÖRNEK GÖRSEL KAYITLARI (İlk 5):'
PRINT '==================================================='

SELECT TOP 5
    ug.[gorsel_id],
    ug.[urun_id],
    u.[urun_adi],
    ug.[gorsel_path],
    ug.[ana_gorsel],
    ug.[sira],
    CASE
        WHEN LEN(ug.[gorsel_path]) > 0 THEN 'Yol var'
        ELSE 'Yol boş'
    END AS [yol_durumu]
FROM [urun_gorsel] ug
INNER JOIN [urun] u ON ug.[urun_id] = u.[urun_id]
ORDER BY ug.[gorsel_id] DESC

PRINT ''
PRINT '==================================================='
PRINT 'SORUN TESPİT:'
PRINT '==================================================='

IF @toplam_gorsel = 0
BEGIN
    PRINT '❌ SORUN: Veritabanında hiç görsel kaydı yok!'
    PRINT ''
    PRINT 'ÇÖZÜM:'
    PRINT '1. Ürün Kartı ekranından görselleri ekleyin'
    PRINT '2. Veya toplu görsel import işlemi yapın'
END
ELSE IF @gorselsiz_urun > 0
BEGIN
    PRINT '⚠️ UYARI: ' + CAST(@gorselsiz_urun AS VARCHAR) + ' ürünün görseli yok'
    PRINT ''
    PRINT 'Bu ürünler katalogda görsel göstermeyecektir.'
END
ELSE
BEGIN
    PRINT '✓ Tüm aktif ürünlerin görseli var'
    PRINT ''
    PRINT 'Eğer katalogda görsel görünmüyorsa:'
    PRINT '1. gorsel_path dosya yollarını kontrol edin (yukardaki örneklere bakın)'
    PRINT '2. Dosyaların fiziksel olarak diskte olduğundan emin olun'
    PRINT '3. Dosya yolları absolute path olmalı (örn: C:\... veya .\images\...)'
END

PRINT ''
PRINT '==================================================='
PRINT 'SP_URUN_LISTELE TEST:'
PRINT '==================================================='
PRINT 'Katalog ekranının kullandığı stored procedure test ediliyor...'
PRINT ''

-- sp_urun_listele test et
SELECT TOP 5
    u.[urun_id],
    u.[urun_adi],
    ug.[gorsel_path] AS [ana_gorsel_path],
    CASE
        WHEN ug.[gorsel_path] IS NULL THEN '❌ NULL'
        WHEN ug.[gorsel_path] = '' THEN '❌ BOŞ'
        WHEN LEN(ug.[gorsel_path]) > 0 THEN '✓ Dolu (' + CAST(LEN(ug.[gorsel_path]) AS VARCHAR) + ' karakter)'
    END AS [path_durumu]
FROM [dbo].[urun] u
LEFT JOIN [dbo].[urun_gorsel] ug ON u.[urun_id] = ug.[urun_id] AND ug.[ana_gorsel] = 1
WHERE u.[aktif] = 1
ORDER BY u.[urun_adi]

PRINT ''
PRINT '==================================================='
PRINT 'NOT: gorsel_path kolonunun NULL veya BOŞ olmaması gerekir.'
PRINT 'NOT: Dosya yolu fiziksel diskte var olmalı.'
PRINT '==================================================='

GO
