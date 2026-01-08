-- =============================================
-- Demo Data Reset Utility
-- Safely removes demo data and resets identity seeds
-- =============================================

USE [AktarOtomasyon]
GO

PRINT '========================================'
PRINT 'Demo Data Reset - BAŞLANGIC'
PRINT '========================================'
PRINT ''

-- =============================================
-- STEP 1: Disable Foreign Key Constraints
-- =============================================

PRINT 'ADIM 1: Foreign Key constraint''ler devre dışı bırakılıyor...'
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
PRINT '  ✓ Foreign Key constraint''ler devre dışı'
PRINT ''

-- =============================================
-- STEP 2: Delete Demo Data (Reverse FK Order)
-- =============================================

PRINT 'ADIM 2: Demo verileri siliniyor...'

-- AI Content (child tables first)
DELETE FROM [dbo].[ai_urun_icerik_versiyon];
PRINT '  ✓ ai_urun_icerik_versiyon temizlendi'

DELETE FROM [dbo].[ai_urun_icerik];
PRINT '  ✓ ai_urun_icerik temizlendi'

-- Images
DELETE FROM [dbo].[urun_gorsel];
PRINT '  ✓ urun_gorsel temizlendi'

-- Notifications
DELETE FROM [dbo].[bildirim];
PRINT '  ✓ bildirim temizlendi'

-- Orders (child tables first)
DELETE FROM [dbo].[siparis_satir];
PRINT '  ✓ siparis_satir temizlendi'

DELETE FROM [dbo].[siparis];
PRINT '  ✓ siparis temizlendi'

-- Stock Movements
DELETE FROM [dbo].[stok_hareket];
PRINT '  ✓ stok_hareket temizlendi'

-- Products (child tables first)
DELETE FROM [dbo].[urun_stok_ayar];
PRINT '  ✓ urun_stok_ayar temizlendi'

DELETE FROM [dbo].[urun];
PRINT '  ✓ urun temizlendi'

-- Reference Data
DELETE FROM [dbo].[tedarikci];
PRINT '  ✓ tedarikci temizlendi'

DELETE FROM [dbo].[urun_kategori];
PRINT '  ✓ urun_kategori temizlendi'

DELETE FROM [dbo].[urun_birim];
PRINT '  ✓ urun_birim temizlendi'

-- Security (Demo users only - keep admin)
DELETE FROM [dbo].[kullanici] WHERE [kullanici_adi] LIKE 'demo%';
PRINT '  ✓ kullanici (demo users) temizlendi'

DELETE FROM [dbo].[rol_yetki];
PRINT '  ✓ rol_yetki temizlendi'

DELETE FROM [dbo].[rol] WHERE [rol_kod] NOT IN ('ADMIN', 'USER');
PRINT '  ✓ rol (demo roles) temizlendi'

PRINT ''

-- =============================================
-- STEP 3: Reset Identity Seeds
-- =============================================

PRINT 'ADIM 3: Identity seed''ler sıfırlanıyor...'

DBCC CHECKIDENT ('[dbo].[urun_kategori]', RESEED, 0);
PRINT '  ✓ urun_kategori seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[urun_birim]', RESEED, 0);
PRINT '  ✓ urun_birim seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[tedarikci]', RESEED, 0);
PRINT '  ✓ tedarikci seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[urun]', RESEED, 0);
PRINT '  ✓ urun seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[stok_hareket]', RESEED, 0);
PRINT '  ✓ stok_hareket seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[siparis]', RESEED, 0);
PRINT '  ✓ siparis seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[siparis_satir]', RESEED, 0);
PRINT '  ✓ siparis_satir seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[bildirim]', RESEED, 0);
PRINT '  ✓ bildirim seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[ai_urun_icerik]', RESEED, 0);
PRINT '  ✓ ai_urun_icerik seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[urun_gorsel]', RESEED, 0);
PRINT '  ✓ urun_gorsel seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[rol]', RESEED, 0);
PRINT '  ✓ rol seed sıfırlandı'

DBCC CHECKIDENT ('[dbo].[kullanici]', RESEED, 0);
PRINT '  ✓ kullanici seed sıfırlandı'

PRINT ''

-- =============================================
-- STEP 4: Re-enable Foreign Key Constraints
-- =============================================

PRINT 'ADIM 4: Foreign Key constraint''ler aktifleştiriliyor...'
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
PRINT '  ✓ Foreign Key constraint''ler aktif'
PRINT ''

-- =============================================
-- STEP 5: Verify Clean State
-- =============================================

PRINT 'ADIM 5: Temizlik doğrulanıyor...'

DECLARE @UrunCount INT = (SELECT COUNT(*) FROM [dbo].[urun]);
DECLARE @TedarikciCount INT = (SELECT COUNT(*) FROM [dbo].[tedarikci]);
DECLARE @KategoriCount INT = (SELECT COUNT(*) FROM [dbo].[urun_kategori]);
DECLARE @StokCount INT = (SELECT COUNT(*) FROM [dbo].[stok_hareket]);
DECLARE @SiparisCount INT = (SELECT COUNT(*) FROM [dbo].[siparis]);
DECLARE @BildirimCount INT = (SELECT COUNT(*) FROM [dbo].[bildirim]);
DECLARE @AiIcerikCount INT = (SELECT COUNT(*) FROM [dbo].[ai_urun_icerik]);
DECLARE @GorselCount INT = (SELECT COUNT(*) FROM [dbo].[urun_gorsel]);

PRINT '  Ürün sayısı: ' + CAST(@UrunCount AS VARCHAR);
PRINT '  Tedarikçi sayısı: ' + CAST(@TedarikciCount AS VARCHAR);
PRINT '  Kategori sayısı: ' + CAST(@KategoriCount AS VARCHAR);
PRINT '  Stok hareket sayısı: ' + CAST(@StokCount AS VARCHAR);
PRINT '  Sipariş sayısı: ' + CAST(@SiparisCount AS VARCHAR);
PRINT '  Bildirim sayısı: ' + CAST(@BildirimCount AS VARCHAR);
PRINT '  AI içerik sayısı: ' + CAST(@AiIcerikCount AS VARCHAR);
PRINT '  Görsel sayısı: ' + CAST(@GorselCount AS VARCHAR);

IF @UrunCount = 0 AND @TedarikciCount = 0 AND @StokCount = 0 AND @SiparisCount = 0
BEGIN
    PRINT ''
    PRINT '  ✓ Veritabanı başarıyla temizlendi!'
END
ELSE
BEGIN
    PRINT ''
    PRINT '  ⚠ UYARI: Bazı veriler hala mevcut olabilir.'
END

PRINT ''
PRINT '========================================'
PRINT 'Demo Data Reset - TAMAMLANDI'
PRINT '========================================'
PRINT ''
PRINT 'Sonraki adımlar:'
PRINT '  1. Seed scriptlerini çalıştırın (01-08):'
PRINT '     - db/seed/sprint9_demo_full/01_refdata.sql'
PRINT '     - db/seed/sprint9_demo_full/02_products.sql'
PRINT '     - db/seed/sprint9_demo_full/03_stock_settings.sql'
PRINT '     - db/seed/sprint9_demo_full/04_stock_movements.sql'
PRINT '     - db/seed/sprint9_demo_full/05_orders.sql'
PRINT '     - db/seed/sprint9_demo_full/06_notifications.sql'
PRINT '     - db/seed/sprint9_demo_full/07_ai_content.sql'
PRINT '     - db/seed/sprint9_demo_full/08_images.sql'
PRINT '  2. Doğrulama scriptini çalıştırın:'
PRINT '     - db/seed/sprint9_demo_full/99_verify.sql'
PRINT ''
GO
