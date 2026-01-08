-- =============================================
-- Aktar Otomasyon - Örnek Ürün Seed Data
-- Versiyon: 1.0
-- =============================================

USE [AktarOtomasyon]
GO

-- =============================================
-- ÖRNEK ÜRÜNLER
-- =============================================

DECLARE @KategoriIdBaharat INT, @KategoriIdKuruyemis INT, @KategoriIdBakliyat INT;
DECLARE @BirimIdKg INT, @BirimIdGr INT, @BirimIdAdet INT, @BirimIdPkt INT;

-- Kategori ID'leri al
SELECT @KategoriIdBaharat = kategori_id FROM urun_kategori WHERE kategori_kod = 'BAHARAT';
SELECT @KategoriIdKuruyemis = kategori_id FROM urun_kategori WHERE kategori_kod = 'KURUYEMIS';
SELECT @KategoriIdBakliyat = kategori_id FROM urun_kategori WHERE kategori_kod = 'BAKLIYAT';

-- Birim ID'leri al
SELECT @BirimIdKg = birim_id FROM urun_birim WHERE birim_kod = 'KG';
SELECT @BirimIdGr = birim_id FROM urun_birim WHERE birim_kod = 'GR';
SELECT @BirimIdAdet = birim_id FROM urun_birim WHERE birim_kod = 'ADET';
SELECT @BirimIdPkt = birim_id FROM urun_birim WHERE birim_kod = 'PKT';

-- Baharatlar
IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH001')
BEGIN
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAH001', 'Karabiber (Tane)', @KategoriIdBaharat, @BirimIdKg, 150.00, 220.00, 'Taze çekilmiş karabiber');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAH002', 'Kimyon', @KategoriIdBaharat, @BirimIdKg, 120.00, 180.00, 'Öğütülmüş kimyon');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAH003', 'Zerdeçal', @KategoriIdBaharat, @BirimIdKg, 200.00, 300.00, 'Toz zerdeçal');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAH004', 'Kırmızı Pul Biber', @KategoriIdBaharat, @BirimIdKg, 80.00, 120.00, 'Antep pul biberi');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAH005', 'Tarçın (Toz)', @KategoriIdBaharat, @BirimIdKg, 180.00, 260.00, 'İthal tarçın tozu');
    
    PRINT 'Baharat ürünleri eklendi.';
END

-- Kuruyemişler
IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR001')
BEGIN
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('KUR001', 'Antep Fıstığı (İç)', @KategoriIdKuruyemis, @BirimIdKg, 800.00, 1100.00, 'Birinci sınıf iç fıstık');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('KUR002', 'Badem (İç)', @KategoriIdKuruyemis, @BirimIdKg, 350.00, 480.00, 'Yerli badem');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('KUR003', 'Ceviz (İç)', @KategoriIdKuruyemis, @BirimIdKg, 400.00, 550.00, 'Yerli ceviz içi');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('KUR004', 'Fındık (İç)', @KategoriIdKuruyemis, @BirimIdKg, 300.00, 420.00, 'Giresun fındığı');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('KUR005', 'Kuru Üzüm', @KategoriIdKuruyemis, @BirimIdKg, 60.00, 90.00, 'Çekirdeksiz kuru üzüm');
    
    PRINT 'Kuruyemiş ürünleri eklendi.';
END

-- Bakliyatlar
IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK001')
BEGIN
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAK001', 'Kırmızı Mercimek', @KategoriIdBakliyat, @BirimIdKg, 35.00, 50.00, 'Yerli kırmızı mercimek');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAK002', 'Yeşil Mercimek', @KategoriIdBakliyat, @BirimIdKg, 40.00, 55.00, 'Yerli yeşil mercimek');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAK003', 'Nohut', @KategoriIdBakliyat, @BirimIdKg, 45.00, 65.00, 'Konya nohutu');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAK004', 'Kuru Fasulye', @KategoriIdBakliyat, @BirimIdKg, 50.00, 75.00, 'Dermason fasulye');
    
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama)
    VALUES ('BAK005', 'Bulgur (İnce)', @KategoriIdBakliyat, @BirimIdKg, 25.00, 38.00, 'İnce bulgur');
    
    PRINT 'Bakliyat ürünleri eklendi.';
END

-- =============================================
-- STOK AYARLARI (Kritik Stok Seviyeleri)
-- =============================================

INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, siparis_miktari)
SELECT u.urun_id, 5, 2, 10
FROM urun u
WHERE NOT EXISTS (SELECT 1 FROM urun_stok_ayar sa WHERE sa.urun_id = u.urun_id);

PRINT 'Stok ayarları eklendi.';

-- =============================================
-- ÖRNEK STOK HAREKETLERİ (Başlangıç girişi)
-- =============================================

INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, referans_tip, aciklama)
SELECT u.urun_id, 'GIRIS', 10, 'MANUEL', 'Başlangıç stok girişi'
FROM urun u
WHERE NOT EXISTS (SELECT 1 FROM stok_hareket sh WHERE sh.urun_id = u.urun_id);

PRINT 'Başlangıç stok hareketleri eklendi.';

GO
