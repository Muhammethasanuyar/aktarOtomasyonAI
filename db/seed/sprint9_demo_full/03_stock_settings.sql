-- =============================================
-- Sprint 9 - Stock Settings Seed
-- Stok Ayarları (min, kritik, emniyet, hedef)
-- Distribution: 20-30% Critical, 15-20% Emergency, 50-60% Normal
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Stock Settings Seed başlatılıyor...';
GO

-- =============================================
-- STOK AYARLARI STRATEGY
-- =============================================
-- Critical Products (20-30%): Will have mevcut < kritik after movements
-- Emergency Products (15-20%): Will have kritik < mevcut < emniyet after movements
-- Normal Products (50-60%): Will have mevcut >= emniyet after movements

-- =============================================
-- HELPER: Get Product IDs by Category
-- =============================================

DECLARE @KatBaharat INT, @KatBitki INT, @KatCay INT, @KatYag INT;
DECLARE @KatMacun INT, @KatTohum INT, @KatKozmetik INT, @KatTakviye INT;
DECLARE @KatKuruyemis INT, @KatBakliyat INT, @KatBal INT, @KatKrem INT;

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

-- =============================================
-- BAHARAT - Spices (20 products)
-- =============================================

PRINT 'BAHARAT stok ayarları ekleniyor...';

-- BAH001-BAH005: CRITICAL (5 products - 25%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 30.0, 20.0, 7, 5 FROM urun WHERE urun_kod = 'BAH001'; -- Karabiber (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 25.0, 15.0, 7, 5 FROM urun WHERE urun_kod = 'BAH002'; -- Kimyon (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 35.0, 20.0, 10, 5 FROM urun WHERE urun_kod = 'BAH003'; -- Zerdeçal (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 15.0, 8.0, 22.0, 40.0, 25.0, 5, 5 FROM urun WHERE urun_kod = 'BAH004'; -- Kırmızı Pul Biber (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 30.0, 20.0, 7, 5 FROM urun WHERE urun_kod = 'BAH005'; -- Tarçın (CRITICAL)

-- BAH006-BAH009: EMERGENCY (4 products - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 30.0, 20.0, 7, 5 FROM urun WHERE urun_kod = 'BAH006'; -- Karanfil (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH007'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 20.0, 12.0, 10, 3 FROM urun WHERE urun_kod = 'BAH007'; -- Kakule (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH008'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 2.0, 1.0, 5.0, 10.0, 5.0, 14, 1 FROM urun WHERE urun_kod = 'BAH008'; -- Safran (EMERGENCY - expensive)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH009'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 25.0, 15.0, 7, 5 FROM urun WHERE urun_kod = 'BAH009'; -- Kişniş (EMERGENCY)

-- BAH010-BAH020: NORMAL (11 products - 55%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH010'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 40.0, 25.0, 7, 5 FROM urun WHERE urun_kod = 'BAH010'; -- Sumak (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH011'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 35.0, 20.0, 7, 5 FROM urun WHERE urun_kod = 'BAH011'; -- Pul Biber (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH012'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 30.0, 18.0, 7, 5 FROM urun WHERE urun_kod = 'BAH012'; -- Kırmızı Toz Biber (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH013'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 25.0, 15.0, 7, 5 FROM urun WHERE urun_kod = 'BAH013'; -- Yenibahar (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH014'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.0, 8.0, 20.0, 12.0, 10, 5 FROM urun WHERE urun_kod = 'BAH014'; -- Zencefil (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH015'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 16.0, 7, 5 FROM urun WHERE urun_kod = 'BAH015'; -- Hardal (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH016'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 32.0, 20.0, 7, 5 FROM urun WHERE urun_kod = 'BAH016'; -- Köri (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH017'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 14.0, 7, 5 FROM urun WHERE urun_kod = 'BAH017'; -- Kekik (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH018'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 26.0, 16.0, 7, 5 FROM urun WHERE urun_kod = 'BAH018'; -- Nane (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH019'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.0, 8.0, 22.0, 12.0, 10, 5 FROM urun WHERE urun_kod = 'BAH019'; -- Fesleğen (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH020'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 14.0, 7, 5 FROM urun WHERE urun_kod = 'BAH020'; -- Biberiye (NORMAL)

-- =============================================
-- BITKI - Herbs (12 products)
-- =============================================

PRINT 'BITKI stok ayarları ekleniyor...';

-- BTK001-BTK003: CRITICAL (3 products - 25%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 25.0, 15.0, 10, 5 FROM urun WHERE urun_kod = 'BTK001'; -- Ihlamur (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 30.0, 20.0, 10, 5 FROM urun WHERE urun_kod = 'BTK002'; -- Papatya (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 22.0, 14.0, 10, 5 FROM urun WHERE urun_kod = 'BTK003'; -- Adaçayı (CRITICAL)

-- BTK004-BTK005: EMERGENCY (2 products - 17%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.0, 8.0, 20.0, 12.0, 14, 5 FROM urun WHERE urun_kod = 'BTK004'; -- Melisa (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 26.0, 16.0, 10, 5 FROM urun WHERE urun_kod = 'BTK005'; -- Kuşburnu (EMERGENCY)

-- BTK006-BTK012: NORMAL (7 products - 58%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 14.0, 10, 5 FROM urun WHERE urun_kod = 'BTK006'; -- Rezene (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK007'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.0, 8.0, 20.0, 12.0, 14, 5 FROM urun WHERE urun_kod = 'BTK007'; -- Lavanta (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK008'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 4.0, 2.0, 7.0, 18.0, 10.0, 14, 5 FROM urun WHERE urun_kod = 'BTK008'; -- Hatmi (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK009'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 22.0, 14.0, 10, 5 FROM urun WHERE urun_kod = 'BTK009'; -- Kınakına (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK010'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.0, 8.0, 20.0, 12.0, 14, 5 FROM urun WHERE urun_kod = 'BTK010'; -- Çörek Otu (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK011'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 4.0, 2.0, 7.0, 18.0, 10.0, 14, 5 FROM urun WHERE urun_kod = 'BTK011'; -- Maydanoz Tohumu (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK012'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 14.0, 10, 5 FROM urun WHERE urun_kod = 'BTK012'; -- Isırgan (NORMAL)

-- =============================================
-- CAY - Teas (10 products)
-- =============================================

PRINT 'CAY stok ayarları ekleniyor...';

-- CAY001-CAY002: CRITICAL (2 products - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 15.0, 8.0, 22.0, 50.0, 30.0, 7, 10 FROM urun WHERE urun_kod = 'CAY001'; -- Yeşil Çay (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 20.0, 10.0, 30.0, 60.0, 40.0, 7, 10 FROM urun WHERE urun_kod = 'CAY002'; -- Siyah Çay (CRITICAL)

-- CAY003-CAY004: EMERGENCY (2 products - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 10, 5 FROM urun WHERE urun_kod = 'CAY003'; -- Beyaz Çay (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 14.0, 14, 5 FROM urun WHERE urun_kod = 'CAY004'; -- Oolong Çay (EMERGENCY)

-- CAY005-CAY010: NORMAL (6 products - 60%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 35.0, 22.0, 10, 10 FROM urun WHERE urun_kod = 'CAY005'; -- Mate Çay (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 30.0, 18.0, 14, 5 FROM urun WHERE urun_kod = 'CAY006'; -- Rooibos (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY007'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 26.0, 16.0, 10, 5 FROM urun WHERE urun_kod = 'CAY007'; -- Earl Grey (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY008'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.0, 8.0, 22.0, 14.0, 14, 5 FROM urun WHERE urun_kod = 'CAY008'; -- Japon Çayı (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY009'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 32.0, 20.0, 10, 10 FROM urun WHERE urun_kod = 'CAY009'; -- Pu-erh (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY010'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 10, 5 FROM urun WHERE urun_kod = 'CAY010'; -- Kombucha Çayı (NORMAL)

-- =============================================
-- YAG - Oils (10 products)
-- =============================================

PRINT 'YAG stok ayarları ekleniyor...';

-- YAG001-YAG002: CRITICAL (2 products - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 3.0, 1.5, 5.0, 12.0, 8.0, 14, 1 FROM urun WHERE urun_kod = 'YAG001'; -- Çörek Otu Yağı (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 2.0, 1.0, 4.0, 10.0, 6.0, 21, 1 FROM urun WHERE urun_kod = 'YAG002'; -- Argan Yağı (CRITICAL - expensive)

-- YAG003-YAG004: EMERGENCY (2 products - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 4.0, 2.0, 6.0, 14.0, 9.0, 14, 1 FROM urun WHERE urun_kod = 'YAG003'; -- Jojoba Yağı (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 18.0, 12.0, 10, 2 FROM urun WHERE urun_kod = 'YAG004'; -- Badem Yağı (EMERGENCY)

-- YAG005-YAG010: NORMAL (6 products - 60%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 7, 2 FROM urun WHERE urun_kod = 'YAG005'; -- Zeytinyağı (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 15.0, 10, 2 FROM urun WHERE urun_kod = 'YAG006'; -- Hint Yağı (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG007'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 4.0, 2.0, 7.0, 16.0, 10.0, 14, 1 FROM urun WHERE urun_kod = 'YAG007'; -- Susam Yağı (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG008'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 20.0, 12.0, 10, 2 FROM urun WHERE urun_kod = 'YAG008'; -- Üzüm Çekirdeği (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG009'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 3.0, 1.5, 5.0, 14.0, 8.0, 14, 1 FROM urun WHERE urun_kod = 'YAG009'; -- Lavanta Yağı (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG010'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 4.0, 2.0, 6.0, 16.0, 10.0, 14, 1 FROM urun WHERE urun_kod = 'YAG010'; -- Chıa Yağı (NORMAL)

-- =============================================
-- KURUYEMIS - Nuts (15 products, 5 existing)
-- =============================================

PRINT 'KURUYEMIS stok ayarları ekleniyor...';

-- KUR001-KUR004: CRITICAL (4 products - 27%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 15.0, 8.0, 22.0, 50.0, 35.0, 7, 5 FROM urun WHERE urun_kod = 'KUR001'; -- Antep Fıstığı (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 18.0, 10.0, 26.0, 60.0, 40.0, 7, 5 FROM urun WHERE urun_kod = 'KUR002'; -- Badem (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 20.0, 10.0, 30.0, 70.0, 45.0, 7, 10 FROM urun WHERE urun_kod = 'KUR003'; -- Ceviz (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 16.0, 8.0, 24.0, 55.0, 38.0, 7, 5 FROM urun WHERE urun_kod = 'KUR004'; -- Fındık (CRITICAL)

-- KUR005-KUR007: EMERGENCY (3 products - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 35.0, 24.0, 10, 5 FROM urun WHERE urun_kod = 'KUR005'; -- Kaju (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 40.0, 28.0, 7, 5 FROM urun WHERE urun_kod = 'KUR006'; -- Kuru Üzüm (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR007'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 30.0, 20.0, 10, 5 FROM urun WHERE urun_kod = 'KUR007'; -- Kuru Kayısı (EMERGENCY)

-- KUR008-KUR015: NORMAL (8 products - 53%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR008'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 36.0, 24.0, 7, 5 FROM urun WHERE urun_kod = 'KUR008'; -- Kuru İncir (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR009'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 25.0, 16.0, 14, 5 FROM urun WHERE urun_kod = 'KUR009'; -- Kuru Hurma (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR010'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 10, 5 FROM urun WHERE urun_kod = 'KUR010'; -- Kuru Erik (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR011'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 34.0, 22.0, 10, 5 FROM urun WHERE urun_kod = 'KUR011'; -- Ayçekirdeği (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR012'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 38.0, 26.0, 7, 5 FROM urun WHERE urun_kod = 'KUR012'; -- Kabak Çekirdeği (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR013'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 22.0, 14.0, 14, 5 FROM urun WHERE urun_kod = 'KUR013'; -- Cranberry (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR014'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 16.0, 10, 5 FROM urun WHERE urun_kod = 'KUR014'; -- Yaban Mersini (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR015'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 30.0, 20.0, 10, 5 FROM urun WHERE urun_kod = 'KUR015'; -- Mulberry (NORMAL)

-- =============================================
-- BAKLIYAT - Legumes/Grains (12 products, 5 existing)
-- =============================================

PRINT 'BAKLIYAT stok ayarları ekleniyor...';

-- BAK001-BAK003: CRITICAL (3 products - 25%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 50.0, 25.0, 75.0, 150.0, 100.0, 7, 25 FROM urun WHERE urun_kod = 'BAK001'; -- Kırmızı Mercimek (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 40.0, 20.0, 60.0, 120.0, 80.0, 7, 25 FROM urun WHERE urun_kod = 'BAK002'; -- Yeşil Mercimek (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 45.0, 22.0, 68.0, 140.0, 90.0, 7, 25 FROM urun WHERE urun_kod = 'BAK003'; -- Nohut (CRITICAL)

-- BAK004-BAK005: EMERGENCY (2 products - 17%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 35.0, 18.0, 52.0, 110.0, 72.0, 7, 25 FROM urun WHERE urun_kod = 'BAK004'; -- Barbunya Fasulye (EMERGENCY)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 60.0, 30.0, 90.0, 180.0, 120.0, 5, 25 FROM urun WHERE urun_kod = 'BAK005'; -- Beyaz Pirinç (EMERGENCY)

-- BAK006-BAK012: NORMAL (7 products - 58%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 30.0, 15.0, 45.0, 100.0, 65.0, 7, 25 FROM urun WHERE urun_kod = 'BAK006'; -- Kuru Fasulye (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK007'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 40.0, 20.0, 60.0, 130.0, 85.0, 7, 25 FROM urun WHERE urun_kod = 'BAK007'; -- Bulgur (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK008'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 25.0, 12.0, 38.0, 90.0, 58.0, 10, 25 FROM urun WHERE urun_kod = 'BAK008'; -- Kinoa (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK009'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 20.0, 10.0, 30.0, 75.0, 48.0, 14, 10 FROM urun WHERE urun_kod = 'BAK009'; -- Yulaf (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK010'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 28.0, 14.0, 42.0, 95.0, 62.0, 10, 25 FROM urun WHERE urun_kod = 'BAK010'; -- Arpa (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK011'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 18.0, 9.0, 27.0, 65.0, 42.0, 14, 10 FROM urun WHERE urun_kod = 'BAK011'; -- Çavdar (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK012'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 22.0, 11.0, 33.0, 78.0, 50.0, 10, 25 FROM urun WHERE urun_kod = 'BAK012'; -- Karabuğday (NORMAL)

-- =============================================
-- MACUN - Pastes (6 products)
-- =============================================

PRINT 'MACUN stok ayarları ekleniyor...';

-- MAC001-MAC002: CRITICAL (2 products - 33%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'MAC001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 30.0, 20.0, 10, 5 FROM urun WHERE urun_kod = 'MAC001'; -- Bitkisel Macun (CRITICAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'MAC002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 25.0, 16.0, 14, 5 FROM urun WHERE urun_kod = 'MAC002'; -- Epimedium Macun (CRITICAL)

-- MAC003: EMERGENCY (1 product - 17%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'MAC003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 22.0, 14.0, 14, 5 FROM urun WHERE urun_kod = 'MAC003'; -- Ginseng Macun (EMERGENCY)

-- MAC004-MAC006: NORMAL (3 products - 50%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'MAC004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 35.0, 24.0, 10, 5 FROM urun WHERE urun_kod = 'MAC004'; -- Mesir Macunu (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'MAC005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 14, 5 FROM urun WHERE urun_kod = 'MAC005'; -- Karışık Macun (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'MAC006'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 20.0, 12.0, 14, 5 FROM urun WHERE urun_kod = 'MAC006'; -- Özel Karışım Macun (NORMAL)

-- =============================================
-- BAL - Honey Products (5 products)
-- =============================================

PRINT 'BAL stok ayarları ekleniyor...';

-- BAL001: CRITICAL (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 40.0, 26.0, 10, 5 FROM urun WHERE urun_kod = 'BAL001'; -- Çiçek Balı (CRITICAL)

-- BAL002: EMERGENCY (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 14, 5 FROM urun WHERE urun_kod = 'BAL002'; -- Kestane Balı (EMERGENCY)

-- BAL003-BAL005: NORMAL (3 products - 60%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 32.0, 21.0, 10, 5 FROM urun WHERE urun_kod = 'BAL003'; -- Çam Balı (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 20.0, 13.0, 14, 5 FROM urun WHERE urun_kod = 'BAL004'; -- Polen (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 4.0, 2.0, 6.0, 16.0, 10.0, 14, 5 FROM urun WHERE urun_kod = 'BAL005'; -- Propolis (NORMAL)

-- =============================================
-- TOHUM - Seeds (5 products)
-- =============================================

PRINT 'TOHUM stok ayarları ekleniyor...';

-- TOM001: CRITICAL (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TOM001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 10, 5 FROM urun WHERE urun_kod = 'TOM001'; -- Chia Tohumu (CRITICAL)

-- TOM002: EMERGENCY (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TOM002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 32.0, 21.0, 10, 5 FROM urun WHERE urun_kod = 'TOM002'; -- Keten Tohumu (EMERGENCY)

-- TOM003-TOM005: NORMAL (3 products - 60%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TOM003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 15.0, 14, 5 FROM urun WHERE urun_kod = 'TOM003'; -- Haşhaş Tohumu (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TOM004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 26.0, 17.0, 10, 5 FROM urun WHERE urun_kod = 'TOM004'; -- Quinoa Tohumu (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TOM005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 20.0, 13.0, 14, 5 FROM urun WHERE urun_kod = 'TOM005'; -- Susam Tohumu (NORMAL)

-- =============================================
-- KOZMETIK - Cosmetics (5 products)
-- =============================================

PRINT 'KOZMETIK stok ayarları ekleniyor...';

-- KOZ001: CRITICAL (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 15.0, 8.0, 22.0, 45.0, 30.0, 10, 10 FROM urun WHERE urun_kod = 'KOZ001'; -- Doğal Sabun (CRITICAL)

-- KOZ002: EMERGENCY (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 12.0, 6.0, 18.0, 38.0, 25.0, 14, 10 FROM urun WHERE urun_kod = 'KOZ002'; -- Defne Sabunu (EMERGENCY)

-- KOZ003-KOZ005: NORMAL (3 products - 60%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 14, 5 FROM urun WHERE urun_kod = 'KOZ003'; -- Kil Maskesi (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 15.0, 14, 5 FROM urun WHERE urun_kod = 'KOZ004'; -- Gül Suyu (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 5.0, 2.5, 8.0, 20.0, 13.0, 14, 5 FROM urun WHERE urun_kod = 'KOZ005'; -- Lavanta Suyu (NORMAL)

-- =============================================
-- TAKVIYE - Supplements (5 products)
-- =============================================

PRINT 'TAKVIYE stok ayarları ekleniyor...';

-- TAK001: CRITICAL (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TAK001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 14, 5 FROM urun WHERE urun_kod = 'TAK001'; -- Spirulina (CRITICAL)

-- TAK002: EMERGENCY (1 product - 20%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TAK002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 15.0, 14, 5 FROM urun WHERE urun_kod = 'TAK002'; -- Omega-3 (EMERGENCY)

-- TAK003-TAK005: NORMAL (3 products - 60%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TAK003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 32.0, 21.0, 10, 10 FROM urun WHERE urun_kod = 'TAK003'; -- Propolis Kapsül (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TAK004'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 26.0, 17.0, 14, 10 FROM urun WHERE urun_kod = 'TAK004'; -- Ginseng Kapsül (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TAK005'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 22.0, 14.0, 14, 10 FROM urun WHERE urun_kod = 'TAK005'; -- Magnezyum (NORMAL)

-- =============================================
-- KREM - Creams (3 products)
-- =============================================

PRINT 'KREM stok ayarları ekleniyor...';

-- KRM001: CRITICAL (1 product - 33%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KRM001'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 10.0, 5.0, 15.0, 32.0, 21.0, 14, 5 FROM urun WHERE urun_kod = 'KRM001'; -- Argan Kremi (CRITICAL)

-- KRM002-KRM003: NORMAL (2 products - 67%)
IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KRM002'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 8.0, 4.0, 12.0, 28.0, 18.0, 14, 5 FROM urun WHERE urun_kod = 'KRM002'; -- Shea Butter Krem (NORMAL)

IF NOT EXISTS (SELECT 1 FROM urun_stok_ayar WHERE urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KRM003'))
    INSERT INTO urun_stok_ayar (urun_id, min_stok, kritik_stok, emniyet_stok, hedef_stok, siparis_miktari, tedarik_sure_gun, paket_kati)
    SELECT urun_id, 6.0, 3.0, 10.0, 24.0, 15.0, 14, 5 FROM urun WHERE urun_kod = 'KRM003'; -- Ekzama Kremi (NORMAL)

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Stock Settings Seed TAMAM';
PRINT '========================================';

DECLARE @StokAyarCount INT, @CriticalCount INT, @EmergencyCount INT, @NormalCount INT;
SELECT @StokAyarCount = COUNT(*) FROM urun_stok_ayar;

-- Calculate distribution (will be accurate after stock movements)
PRINT 'Stok ayar sayısı: ' + CAST(@StokAyarCount AS VARCHAR);
PRINT '';
PRINT 'Distribution Strategy (to be realized after stock movements):';
PRINT '  - Critical Products: 20-30% (mevcut < kritik)';
PRINT '  - Emergency Products: 15-20% (kritik < mevcut < emniyet)';
PRINT '  - Normal Products: 50-60% (mevcut >= emniyet)';
PRINT '';
PRINT '========================================';
GO
