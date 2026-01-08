-- =============================================
-- Sprint 9 - Product Catalog Seed
-- 80-100 Aktar Ürünleri
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Product Catalog Seed başlatılıyor...';
GO

-- Get category IDs
DECLARE @KatBaharat INT, @KatBitki INT, @KatCay INT, @KatYag INT, @KatMacun INT,
        @KatTohum INT, @KatKozmetik INT, @KatTakviye INT, @KatKuruyemis INT,
        @KatBakliyat INT, @KatBal INT, @KatKrem INT, @KatTatli INT, @KatPekmez INT, @KatSivi INT;

SELECT @KatBaharat = kategori_id FROM urun_kategori WHERE kategori_kod = 'BAHARAT';
SELECT @KatBitki = kategori_id FROM urun_kategori WHERE kategori_kod = 'BITKI';
SELECT @KatCay = kategori_id FROM urun_kategori WHERE kategori_kod = 'CAY';
SELECT @KatYag = kategori_id FROM urun_kategori WHERE kategori_kod = 'YAG';
SELECT @KatMacun = kategori_id FROM urun_kategori WHERE kategori_kod = 'MACUN';
SELECT @KatTohum = kategori_id FROM urun_kategori WHERE kategori_kod = 'TOHUM';
SELECT @KatKozmetik = kategori_id FROM urun_kategori WHERE kategori_kod = 'KOZMETIK';
SELECT @KatTakviye = kategori_id FROM urun_kategori WHERE kategori_kod = 'TAKVIYE';
SELECT @KatKuruyemis = kategori_id FROM urun_kategori WHERE kategori_kod = 'KURUYEMIS';
SELECT @KatBakliyat = kategori_id FROM urun_kategori WHERE kategori_kod = 'BAKLIYAT';
SELECT @KatBal = kategori_id FROM urun_kategori WHERE kategori_kod = 'BAL';
SELECT @KatKrem = kategori_id FROM urun_kategori WHERE kategori_kod = 'KREM';
SELECT @KatTatli = kategori_id FROM urun_kategori WHERE kategori_kod = 'TATLI';
SELECT @KatPekmez = kategori_id FROM urun_kategori WHERE kategori_kod = 'PEKMEZ';
SELECT @KatSivi = kategori_id FROM urun_kategori WHERE kategori_kod = 'SIVI';

-- Get birim (unit) IDs
DECLARE @BirimKG INT, @BirimGR INT, @BirimAdet INT, @BirimML INT, @BirimLT INT, @BirimPKT INT, @BirimKTU INT;

SELECT @BirimKG = birim_id FROM urun_birim WHERE birim_kod = 'KG';
SELECT @BirimGR = birim_id FROM urun_birim WHERE birim_kod = 'GR';
SELECT @BirimAdet = birim_id FROM urun_birim WHERE birim_kod = 'ADET';
SELECT @BirimML = birim_id FROM urun_birim WHERE birim_kod = 'ML';
SELECT @BirimLT = birim_id FROM urun_birim WHERE birim_kod = 'LT';
SELECT @BirimPKT = birim_id FROM urun_birim WHERE birim_kod = 'PKT';
SELECT @BirimKTU = birim_id FROM urun_birim WHERE birim_kod = 'KTU';

-- =============================================
-- BAHARAT (15-20 ürün)
-- =============================================

PRINT 'BAHARAT ürünleri ekleniyor...';

-- Note: BAH001-BAH005 already exist from 002_seed_sample_products.sql
-- We'll add BAH006 onwards

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH006', 'Kişniş (Tane)', @KatBaharat, @BirimKG, 100.00, 150.00, 'Yerli kişniş tanesi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH007')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH007', 'Köri (Karışım)', @KatBaharat, @BirimKG, 140.00, 210.00, 'İthal köri baharatı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH008')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH008', 'Yenibahar (Tane)', @KatBaharat, @BirimKG, 130.00, 190.00, 'Taze yenibahar', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH009')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH009', 'Zencefil (Toz)', @KatBaharat, @BirimKG, 160.00, 240.00, 'Öğütülmüş zencefil', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH010')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH010', 'Sumak', @KatBaharat, @BirimKG, 90.00, 135.00, 'Öğütülmüş sumak', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH011')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH011', 'Nane (Kurutulmuş)', @KatBaharat, @BirimKG, 80.00, 120.00, 'Kurutulmuş nane yaprakları', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH012')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH012', 'Fesleğen (Kurutulmuş)', @KatBaharat, @BirimKG, 110.00, 165.00, 'Kurutulmuş fesleğen', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH013')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH013', 'Kekik (Kurutulmuş)', @KatBaharat, @BirimKG, 100.00, 150.00, 'Doğal kekik', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH014')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH014', 'Biberiye', @KatBaharat, @BirimKG, 120.00, 180.00, 'Kurutulmuş biberiye', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH015')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH015', 'Lavanta', @KatBaharat, @BirimKG, 150.00, 225.00, 'Kurutulmuş lavanta çiçeği', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH016')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH016', 'Karanfil (Tane)', @KatBaharat, @BirimKG, 250.00, 375.00, 'İthal karanfil', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH017')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH017', 'Kakule (Tane)', @KatBaharat, @BirimKG, 300.00, 450.00, 'Yeşil kakule', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH018')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH018', 'Safran', @KatBaharat, @BirimGR, 2000.00, 3000.00, 'İran safrası (gram)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH019')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH019', 'Hardal Tohumu', @KatBaharat, @BirimKG, 70.00, 105.00, 'Sarı hardal tohumu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH020')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAH020', 'Yedi Baharat Karışımı', @KatBaharat, @BirimKG, 160.00, 240.00, 'Özel baharat karışımı', 1);

PRINT '  ✓ BAHARAT: 20 ürün (5 mevcut + 15 yeni)';

-- =============================================
-- BITKI (10-15 ürün)
-- =============================================

PRINT 'BITKI ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK001', 'Ihlamur', @KatBitki, @BirimKG, 100.00, 150.00, 'Kurutulmuş ıhlamur çiçeği', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK002', 'Papatya', @KatBitki, @BirimKG, 90.00, 135.00, 'Alman papatyası', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK003', 'Adaçayı', @KatBitki, @BirimKG, 110.00, 165.00, 'Adaçayı yaprağı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK004', 'Melisa', @KatBitki, @BirimKG, 120.00, 180.00, 'Kurutulmuş melisa', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK005', 'Kuşburnu', @KatBitki, @BirimKG, 80.00, 120.00, 'Kuşburnu meyvesi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK006', 'Rezene', @KatBitki, @BirimKG, 95.00, 142.50, 'Rezene tohumu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK007')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK007', 'Çörek Otu', @KatBitki, @BirimKG, 130.00, 195.00, 'Çörek otu tanesi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK008')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK008', 'Isırgan Otu', @KatBitki, @BirimKG, 85.00, 127.50, 'Kurutulmuş ısırgan otu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK009')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK009', 'Altın Çilek', @KatBitki, @BirimKG, 200.00, 300.00, 'Altın çilek meyvesi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK010')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK010', 'Civanperçemi', @KatBitki, @BirimKG, 90.00, 135.00, 'Kurutulmuş civanperçemi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK011')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK011', 'Ebegümeci', @KatBitki, @BirimKG, 75.00, 112.50, 'Ebegümeci çiçeği', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK012')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BTK012', 'Hatmi Çiçeği', @KatBitki, @BirimKG, 110.00, 165.00, 'Hatmi çiçeği ve yaprağı', 1);

PRINT '  ✓ BITKI: 12 ürün';

-- =============================================
-- ÇAY (8-12 ürün)
-- =============================================

PRINT 'ÇAY ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY001', 'Yeşil Çay', @KatCay, @BirimKG, 120.00, 180.00, 'Çin yeşil çayı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY002', 'Siyah Çay (Earl Grey)', @KatCay, @BirimKG, 140.00, 210.00, 'Bergamot aromalı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY003', 'Beyaz Çay', @KatCay, @BirimKG, 200.00, 300.00, 'Beyaz çay tomurcukları', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY004', 'Mate Çayı', @KatCay, @BirimKG, 150.00, 225.00, 'Arjantin mate çayı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY005', 'Rooibos Çay', @KatCay, @BirimKG, 130.00, 195.00, 'Güney Afrika rooibos', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY006', 'Bitki Çayı Karışımı', @KatCay, @BirimKG, 110.00, 165.00, '9 bitki karışımı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY007')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY007', 'Form Çayı', @KatCay, @BirimKG, 100.00, 150.00, 'Zayıflama çayı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY008')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY008', 'Ödem Atıcı Çay', @KatCay, @BirimKG, 95.00, 142.50, 'Ödem söktürücü karışım', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY009')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY009', 'Nefes Açıcı Çay', @KatCay, @BirimKG, 105.00, 157.50, 'Öksürük ve nefes çayı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY010')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('CAY010', 'Uyku Çayı', @KatCay, @BirimKG, 115.00, 172.50, 'Rahatlatıcı çay karışımı', 1);

PRINT '  ✓ ÇAY: 10 ürün';

-- =============================================
-- YAĞ (8-12 ürün)
-- =============================================

PRINT 'YAĞ ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG001', 'Çörek Otu Yağı', @KatYag, @BirimML, 1.50, 2.25, 'Soğuk sıkım çörek otu yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG002', 'Argan Yağı', @KatYag, @BirimML, 3.00, 4.50, 'Fas argan yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG003', 'Jojoba Yağı', @KatYag, @BirimML, 2.50, 3.75, 'Saf jojoba yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG004', 'Badem Yağı', @KatYag, @BirimML, 1.20, 1.80, 'Tatlı badem yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG005', 'Zeytinyağı (Soğuk Sıkım)', @KatYag, @BirimLT, 180.00, 270.00, 'Natürel sızma zeytinyağı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG006', 'Hindistan Cevizi Yağı', @KatYag, @BirimML, 0.80, 1.20, 'Virgin coconut oil (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG007')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG007', 'Susam Yağı', @KatYag, @BirimML, 0.90, 1.35, 'Soğuk sıkım susam yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG008')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG008', 'Keten Tohumu Yağı', @KatYag, @BirimML, 1.10, 1.65, 'Omega-3 keten yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG009')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG009', 'Avokado Yağı', @KatYag, @BirimML, 2.00, 3.00, 'Soğuk sıkım avokado yağı (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG010')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('YAG010', 'Kayısı Çekirdeği Yağı', @KatYag, @BirimML, 1.80, 2.70, 'Malatya kayısı yağı (ml)', 1);

PRINT '  ✓ YAĞ: 10 ürün';

-- =============================================
-- KURUYEMIŞ (10-15 ürün)
-- =============================================

PRINT 'KURUYEMIŞ ürünleri ekleniyor...';

-- Note: KUR001-KUR005 already exist from 002_seed_sample_products.sql
-- We'll add KUR006 onwards

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR006', 'Kuru Kayısı', @KatKuruyemis, @BirimKG, 120.00, 180.00, 'Malatya kayısısı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR007')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR007', 'Kuru İncir', @KatKuruyemis, @BirimKG, 90.00, 135.00, 'Aydın inciri', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR008')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR008', 'Kuru Erik', @KatKuruyemis, @BirimKG, 70.00, 105.00, 'Kuru erik', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR009')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR009', 'Kaju', @KatKuruyemis, @BirimKG, 450.00, 675.00, 'İthal kaju', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR010')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR010', 'Yer Fıstığı', @KatKuruyemis, @BirimKG, 100.00, 150.00, 'Kavrulmuş yer fıstığı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR011')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR011', 'Leblebi (Beyaz)', @KatKuruyemis, @BirimKG, 60.00, 90.00, 'Çorum leblebisi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR012')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR012', 'Leblebi (Sarı)', @KatKuruyemis, @BirimKG, 55.00, 82.50, 'Sarı leblebi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR013')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR013', 'Kabak Çekirdeği', @KatKuruyemis, @BirimKG, 130.00, 195.00, 'İç kabak çekirdeği', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR014')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR014', 'Ay Çekirdeği', @KatKuruyemis, @BirimKG, 100.00, 150.00, 'İç ay çekirdeği', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR015')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KUR015', 'Goji Berry', @KatKuruyemis, @BirimKG, 400.00, 600.00, 'Tibeti goji meyvesi', 1);

PRINT '  ✓ KURUYEMIŞ: 15 ürün (5 mevcut + 10 yeni)';

-- =============================================
-- BAKLIYAT (10-12 ürün)
-- =============================================

PRINT 'BAKLIYAT ürünleri ekleniyor...';

-- Note: BAK001-BAK005 already exist from 002_seed_sample_products.sql
-- We'll add BAK006 onwards

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK006', 'Barbunya', @KatBakliyat, @BirimKG, 55.00, 82.50, 'Barbunya fasulye', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK007')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK007', 'Pirinç (Osmancık)', @KatBakliyat, @BirimKG, 40.00, 60.00, 'Osmancık pirinci', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK008')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK008', 'Bulgur (Pilavlık)', @KatBakliyat, @BirimKG, 28.00, 42.00, 'Pilavlık bulgur', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK009')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK009', 'Kinoa', @KatBakliyat, @BirimKG, 80.00, 120.00, 'Organik kinoa', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK010')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK010', 'Chia Tohumu', @KatBakliyat, @BirimKG, 150.00, 225.00, 'Chia seeds', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK011')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK011', 'Yulaf Ezmesi', @KatBakliyat, @BirimKG, 35.00, 52.50, 'Tam yulaf ezmesi', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK012')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAK012', 'Kara Pirinç', @KatBakliyat, @BirimKG, 60.00, 90.00, 'Siyah pirinç', 1);

PRINT '  ✓ BAKLIYAT: 12 ürün (5 mevcut + 7 yeni)';

-- =============================================
-- MACUN (5-8 ürün)
-- =============================================

PRINT 'MACUN ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('MAC001', 'Bitkisel Macun', @KatMacun, @BirimKG, 180.00, 270.00, '41 karışım bitkisel macun', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('MAC002', 'Karışık Macun', @KatMacun, @BirimKG, 200.00, 300.00, 'Özel karışım macun', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('MAC003', 'Epimedium Macun', @KatMacun, @BirimKG, 350.00, 525.00, 'Afrodizyak macun', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('MAC004', 'Ginseng Macun', @KatMacun, @BirimKG, 320.00, 480.00, 'Kore ginseng macunu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('MAC005', 'Mesir Macunu', @KatMacun, @BirimKG, 160.00, 240.00, 'Geleneksel mesir macunu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC006')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('MAC006', 'Arı Sütü Macun', @KatMacun, @BirimKG, 400.00, 600.00, 'Arı sütü katkılı macun', 1);

PRINT '  ✓ MACUN: 6 ürün';

-- =============================================
-- BAL (3-5 ürün)
-- =============================================

PRINT 'BAL ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAL001', 'Çiçek Balı', @KatBal, @BirimKG, 200.00, 300.00, 'Süzme çiçek balı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAL002', 'Kestane Balı', @KatBal, @BirimKG, 250.00, 375.00, 'Bursa kestane balı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAL003', 'Çam Balı', @KatBal, @BirimKG, 300.00, 450.00, 'Marmaris çam balı', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAL004', 'Polen', @KatBal, @BirimKG, 350.00, 525.00, 'Arı poleni', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('BAL005', 'Propolis (Damla)', @KatBal, @BirimML, 5.00, 7.50, 'Sıvı propolis (ml)', 1);

PRINT '  ✓ BAL: 5 ürün';

-- =============================================
-- TOHUM (5-8 ürün)
-- =============================================

PRINT 'TOHUM ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TOM001', 'Chia Tohumu', @KatTohum, @BirimKG, 150.00, 225.00, 'Chia seeds - süper gıda', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TOM002', 'Keten Tohumu', @KatTohum, @BirimKG, 40.00, 60.00, 'Altın keten tohumu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TOM003', 'Haşhaş Tohumu', @KatTohum, @BirimKG, 90.00, 135.00, 'Mavi haşhaş tohumu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TOM004', 'Quinoa Tohumu', @KatTohum, @BirimKG, 80.00, 120.00, 'Beyaz kinoa', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TOM005', 'Sarımsak Tozu', @KatTohum, @BirimKG, 110.00, 165.00, 'Öğütülmüş sarımsak', 1);

PRINT '  ✓ TOHUM: 5 ürün';

-- =============================================
-- KOZMETİK (5-8 ürün)
-- =============================================

PRINT 'KOZMETİK ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KOZ001', 'Doğal Sabun (Zeytinyağı)', @KatKozmetik, @BirimAdet, 8.00, 12.00, 'El yapımı sabun', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KOZ002', 'Defne Sabunu', @KatKozmetik, @BirimAdet, 12.00, 18.00, 'Hatay defne sabunu', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KOZ003', 'Kil Maskesi', @KatKozmetik, @BirimGR, 0.15, 0.225, 'Yeşil kil maskesi (gr)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KOZ004', 'Gül Suyu', @KatKozmetik, @BirimML, 0.80, 1.20, 'Isparta gül suyu (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KOZ005', 'Lavanta Yağı (Uçucu)', @KatKozmetik, @BirimML, 4.00, 6.00, 'Saf lavanta yağı (ml)', 1);

PRINT '  ✓ KOZMETİK: 5 ürün';

-- =============================================
-- TAKVİYE (5-8 ürün)
-- =============================================

PRINT 'TAKVİYE ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TAK001', 'Spirulina (Tablet)', @KatTakviye, @BirimAdet, 0.50, 0.75, 'Spirulina tableti (adet)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TAK002', 'Omega-3 Balık Yağı', @KatTakviye, @BirimAdet, 1.20, 1.80, 'Balık yağı kapsülü (adet)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TAK003', 'Propolis Kapsül', @KatTakviye, @BirimAdet, 1.00, 1.50, 'Propolis kapsül (adet)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK004')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TAK004', 'Ginseng Kapsül', @KatTakviye, @BirimAdet, 1.50, 2.25, 'Kore ginsengi (adet)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK005')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('TAK005', 'Magnezyum Takviyesi', @KatTakviye, @BirimAdet, 0.80, 1.20, 'Magnezyum kapsül (adet)', 1);

PRINT '  ✓ TAKVİYE: 5 ürün';

-- =============================================
-- KREM (3-5 ürün)
-- =============================================

PRINT 'KREM ürünleri ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KRM001')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KRM001', 'Argan Kremi', @KatKrem, @BirimML, 1.50, 2.25, 'Argan yağı kremi (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KRM002')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KRM002', 'Shea Butter Kremi', @KatKrem, @BirimML, 1.20, 1.80, 'Shea butter nemlendirici (ml)', 1);

IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KRM003')
    INSERT INTO urun (urun_kod, urun_adi, kategori_id, birim_id, alis_fiyati, satis_fiyati, aciklama, aktif)
    VALUES ('KRM003', 'Ekzama Kremi', @KatKrem, @BirimML, 2.00, 3.00, 'Bitkisel ekzama kremi (ml)', 1);

PRINT '  ✓ KREM: 3 ürün';

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Product Catalog Seed TAMAM';
PRINT '========================================';

DECLARE @TotalProducts INT;
SELECT @TotalProducts = COUNT(*) FROM urun WHERE aktif = 1;

PRINT 'Toplam Ürün Sayısı: ' + CAST(@TotalProducts AS VARCHAR);

IF @TotalProducts >= 50 AND @TotalProducts <= 150
    PRINT '✓ Ürün hedefi ulaşıldı (50-150 aralığında)';
ELSE IF @TotalProducts < 50
    PRINT '⚠ Ürün hedefi eksik (beklenen >=50)';
ELSE
    PRINT '⚠ Ürün hedefi aşıldı (beklenen <=150)';

-- Kategori dağılımı
PRINT '';
PRINT 'Kategori Dağılımı:';
SELECT k.kategori_adi, COUNT(*) AS urun_sayisi
FROM urun u
INNER JOIN urun_kategori k ON u.kategori_id = k.kategori_id
WHERE u.aktif = 1
GROUP BY k.kategori_adi
ORDER BY urun_sayisi DESC;

PRINT '========================================';
GO
