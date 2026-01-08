-- =============================================
-- TAM DEMO RESET - TÜM SEED'LERİ ÇALIŞTIR
-- Sprint 9 Demo Data Suite
-- =============================================
--
-- Çalıştırma Komutu:
-- sqlcmd -S localhost -d AktarOtomasyon -E -i "EXECUTE_ALL.sql"
--
-- veya Visual Studio'da SSMS'de doğrudan çalıştırın
-- =============================================

USE [AktarOtomasyon]
GO

PRINT '';
PRINT '=================================================';
PRINT '   TAM DEMO RESET BAŞLATILIYOR';
PRINT '   Sprint 9 Demo Data Suite';
PRINT '=================================================';
PRINT '';
PRINT 'Tarih: ' + CONVERT(VARCHAR, GETDATE(), 120);
PRINT '';

-- =============================================
-- ADIM 1/11: VERİTABANI TEMİZLİĞİ
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 1/11: Veritabanı Temizleniyor...';
PRINT '=================================================';
:r 00_cleanup.sql
GO

-- =============================================
-- ADIM 2/11: MASTER DATA
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 2/11: Master Data Yükleniyor...';
PRINT '=================================================';
:r 001_seed_masterdata.sql
GO

-- =============================================
-- ADIM 3/11: REFERENCE DATA (Kategoriler, Tedarikçiler)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 3/11: Reference Data Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\01_refdata.sql
GO

-- =============================================
-- ADIM 4/11: ÜRÜNLER (~100 ürün)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 4/11: Ürünler Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\02_products.sql
GO

-- =============================================
-- ADIM 5/11: STOK AYARLARI
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 5/11: Stok Ayarları Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\03_stock_settings.sql
GO

-- =============================================
-- ADIM 6/11: STOK HAREKETLERİ (1000+ hareket)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 6/11: Stok Hareketleri Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\04_stock_movements.sql
GO

-- =============================================
-- ADIM 7/11: SİPARİŞLER (10-30 sipariş)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 7/11: Siparişler Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\05_orders.sql
GO

-- =============================================
-- ADIM 8/11: BİLDİRİMLER (30-100 bildirim)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 8/11: Bildirimler Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\06_notifications.sql
GO

-- =============================================
-- ADIM 9/11: AI İÇERİK (20-50 içerik)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 9/11: AI İçerik Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\07_ai_content.sql
GO

-- =============================================
-- ADIM 10/11: GÖRSEL METADATA (50+ görsel)
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 10/11: Görsel Metadata Yükleniyor...';
PRINT '=================================================';
:r sprint9_demo_full\08_images.sql
GO

-- =============================================
-- ADIM 11/11: VERİ DOĞRULAMA
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT 'ADIM 11/11: Veri Doğrulaması Yapılıyor...';
PRINT '=================================================';
:r sprint9_demo_full\99_verify.sql
GO

-- =============================================
-- TAMAMLANDI
-- =============================================
PRINT '';
PRINT '=================================================';
PRINT '   TAM DEMO RESET TAMAMLANDI!';
PRINT '=================================================';
PRINT '';
PRINT 'Özet:';
PRINT '  ✓ Veritabanı temizlendi';
PRINT '  ✓ Master data yüklendi';
PRINT '  ✓ Reference data yüklendi (kategoriler, tedarikçiler)';
PRINT '  ✓ Ürünler yüklendi';
PRINT '  ✓ Stok ayarları ve hareketleri yüklendi';
PRINT '  ✓ Siparişler yüklendi';
PRINT '  ✓ Bildirimler yüklendi';
PRINT '  ✓ AI içerik yüklendi';
PRINT '  ✓ Görsel metadata yüklendi';
PRINT '  ✓ Veri doğrulaması tamamlandı';
PRINT '';
PRINT 'Şimdi uygulamayı başlatabilirsiniz!';
PRINT '';
PRINT 'Bitiş: ' + CONVERT(VARCHAR, GETDATE(), 120);
PRINT '=================================================';
GO
