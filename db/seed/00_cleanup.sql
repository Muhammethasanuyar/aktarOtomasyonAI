-- =============================================
-- Veritabanı Temizleme - Tam Reset
-- Sprint 9 Demo Data için hazırlık
-- =============================================

USE [AktarOtomasyon]
GO

PRINT '================================================';
PRINT 'VERİTABANI TEMİZLİĞİ BAŞLATILIYOR...';
PRINT '================================================';
PRINT '';

-- =============================================
-- LOG TABLOLARI
-- =============================================
PRINT 'Log tabloları temizleniyor...';

DELETE FROM [audit_log];
PRINT '  - audit_log temizlendi';

DELETE FROM [kul_ekran_log];
PRINT '  - kul_ekran_log temizlendi';

-- =============================================
-- BİLDİRİMLER
-- =============================================
PRINT 'Bildirimler temizleniyor...';

DELETE FROM [bildirim];
PRINT '  - bildirim temizlendi';

-- =============================================
-- AI İÇERİK
-- =============================================
PRINT 'AI içerik temizleniyor...';

DELETE FROM [ai_urun_icerik_ver];
PRINT '  - ai_urun_icerik_ver temizlendi';

DELETE FROM [ai_urun_icerik];
PRINT '  - ai_urun_icerik temizlendi';

-- =============================================
-- GÖRSEL METADATA
-- =============================================
PRINT 'Ürün görselleri temizleniyor...';

DELETE FROM [urun_gorsel];
PRINT '  - urun_gorsel temizlendi';

-- =============================================
-- SİPARİŞLER
-- =============================================
PRINT 'Siparişler temizleniyor...';

DELETE FROM [siparis_satir];
PRINT '  - siparis_satir temizlendi';

DELETE FROM [siparis];
PRINT '  - siparis temizlendi';

-- =============================================
-- STOK HAREKETLERİ
-- =============================================
PRINT 'Stok hareketleri temizleniyor...';

DELETE FROM [stok_hareket];
PRINT '  - stok_hareket temizlendi';

-- =============================================
-- ÜRÜNLER VE STOK AYARLARI
-- =============================================
PRINT 'Ürünler temizleniyor...';

DELETE FROM [urun_stok_ayar];
PRINT '  - urun_stok_ayar temizlendi';

DELETE FROM [urun];
PRINT '  - urun temizlendi';

-- =============================================
-- TEDARİKÇİLER
-- =============================================
PRINT 'Tedarikçiler temizleniyor...';

DELETE FROM [tedarikci];
PRINT '  - tedarikci temizlendi';

-- =============================================
-- KATEGORİLER (Self-Reference Dikkatli)
-- =============================================
PRINT 'Kategoriler temizleniyor...';

-- Önce alt kategorileri sil (ust_kategori_id IS NOT NULL)
DELETE FROM [urun_kategori] WHERE [ust_kategori_id] IS NOT NULL;
PRINT '  - Alt kategoriler temizlendi';

-- Sonra ana kategorileri sil (ust_kategori_id IS NULL)
DELETE FROM [urun_kategori] WHERE [ust_kategori_id] IS NULL;
PRINT '  - Ana kategoriler temizlendi';

-- =============================================
-- BİRİMLER
-- =============================================
PRINT 'Birimler temizleniyor...';

DELETE FROM [urun_birim];
PRINT '  - urun_birim temizlendi';

-- =============================================
-- KULLANICI-ROL İLİŞKİLERİ
-- =============================================
PRINT 'Kullanıcı-Rol ilişkileri temizleniyor...';

DELETE FROM [kullanici_rol];
PRINT '  - kullanici_rol temizlendi';

DELETE FROM [rol_yetki];
PRINT '  - rol_yetki temizlendi';

-- =============================================
-- KULLANICILAR (Admin Hariç)
-- =============================================
PRINT 'Kullanıcılar temizleniyor (admin hariç)...';

DELETE FROM [kullanici] WHERE [kullanici_kodu] != 'admin';
PRINT '  - kullanici temizlendi (admin korundu)';

-- =============================================
-- ROLLER VE YETKİLER (Opsiyonel - Seed'de Yeniden Oluşturulacak)
-- =============================================
PRINT 'Roller ve yetkiler temizleniyor...';

DELETE FROM [rol];
PRINT '  - rol temizlendi';

DELETE FROM [yetki];
PRINT '  - yetki temizlendi';

-- =============================================
-- EKRAN VE AI ŞABLONLARI (Opsiyonel)
-- =============================================
PRINT 'Ekran ve AI şablonları temizleniyor...';

DELETE FROM [kul_ekran];
PRINT '  - kul_ekran temizlendi';

DELETE FROM [ai_sablon];
PRINT '  - ai_sablon temizlendi';

-- =============================================
-- ÖZET
-- =============================================
PRINT '';
PRINT '================================================';
PRINT 'VERİTABANI TEMİZLİĞİ TAMAMLANDI!';
PRINT '================================================';
PRINT '';
PRINT 'Temizlik Sonrası Kayıt Sayıları:';
PRINT '------------------------------------------------';

SELECT
    'urun' AS tablo,
    COUNT(*) AS kayit_sayisi
FROM [urun]
UNION ALL
SELECT 'urun_kategori', COUNT(*) FROM [urun_kategori]
UNION ALL
SELECT 'urun_birim', COUNT(*) FROM [urun_birim]
UNION ALL
SELECT 'tedarikci', COUNT(*) FROM [tedarikci]
UNION ALL
SELECT 'siparis', COUNT(*) FROM [siparis]
UNION ALL
SELECT 'stok_hareket', COUNT(*) FROM [stok_hareket]
UNION ALL
SELECT 'bildirim', COUNT(*) FROM [bildirim]
UNION ALL
SELECT 'ai_urun_icerik', COUNT(*) FROM [ai_urun_icerik]
UNION ALL
SELECT 'kullanici', COUNT(*) FROM [kullanici]
UNION ALL
SELECT 'rol', COUNT(*) FROM [rol]
UNION ALL
SELECT 'yetki', COUNT(*) FROM [yetki];

PRINT '';
PRINT '✓ Veritabanı seed veriler için hazır';
PRINT '';
GO
