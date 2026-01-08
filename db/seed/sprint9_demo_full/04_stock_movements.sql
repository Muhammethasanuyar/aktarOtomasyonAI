-- =============================================
-- Sprint 9 - Stock Movements Seed
-- Stok Hareketleri (30-90 day history)
-- Distribution: 20-30% Critical, 15-20% Emergency, 50-60% Normal
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Stock Movements Seed başlatılıyor...';
GO

-- =============================================
-- CONFIGURATION
-- =============================================

DECLARE @StartDate DATETIME = DATEADD(DAY, -90, GETDATE());
DECLARE @EndDate DATETIME = GETDATE();
DECLARE @KullaniciId INT = 1; -- Admin user

-- Movement type distribution: GIRIS (60%), CIKIS (35%), SAYIM (3%), DUZELTME (2%)

PRINT 'Tarih aralığı: ' + CONVERT(VARCHAR, @StartDate, 120) + ' - ' + CONVERT(VARCHAR, @EndDate, 120);
GO

-- =============================================
-- HELPER: Get User ID
-- =============================================

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

IF @AdminKullaniciId IS NULL
BEGIN
    PRINT '  ⚠ UYARI: Admin kullanıcı bulunamadı! kullanici_id = 1 varsayılıyor.';
    SET @AdminKullaniciId = 1;
END

PRINT 'Kullanıcı ID: ' + CAST(@AdminKullaniciId AS VARCHAR);
GO

-- =============================================
-- CRITICAL PRODUCTS (28 products - 28%)
-- Target: mevcut < kritik after all movements
-- Strategy: Initial GIRIS close to kritik, more CIKIS than GIRIS
-- =============================================

PRINT 'CRITICAL products - stok hareketleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- BAH001: Karabiber (kritik=5.0) -> Target: 3.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 8.0, DATEADD(DAY, -85, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -70, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.0, DATEADD(DAY, -45, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH001';
END

-- BAH002: Kimyon (kritik=4.0) -> Target: 2.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 7.0, DATEADD(DAY, -80, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -60, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.0, DATEADD(DAY, -30, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH002';
END

-- BAH003: Zerdeçal (kritik=6.0) -> Target: 4.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 10.0, DATEADD(DAY, -75, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.5, DATEADD(DAY, -55, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -25, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH003';
END

-- BAH004: Kırmızı Pul Biber (kritik=8.0) -> Target: 5.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH004')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 12.0, DATEADD(DAY, -82, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.0, DATEADD(DAY, -65, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -40, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH004';
END

-- BAH005: Tarçın (kritik=5.0) -> Target: 3.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH005')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 9.0, DATEADD(DAY, -78, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -58, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -35, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH005';
END

-- BTK001: Ihlamur (kritik=4.0) -> Target: 2.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 8.0, DATEADD(DAY, -84, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BTK001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.5, DATEADD(DAY, -62, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -38, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK001';
END

-- BTK002: Papatya (kritik=5.0) -> Target: 3.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 10.0, DATEADD(DAY, -81, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BTK002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.0, DATEADD(DAY, -59, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -33, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK002';
END

-- BTK003: Adaçayı (kritik=3.0) -> Target: 1.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 6.0, DATEADD(DAY, -76, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BTK003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -54, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.0, DATEADD(DAY, -28, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK003';
END

-- CAY001: Yeşil Çay (kritik=8.0) -> Target: 5.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 15.0, DATEADD(DAY, -87, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'CAY001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.0, DATEADD(DAY, -67, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.0, DATEADD(DAY, -42, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY001';
END

-- CAY002: Siyah Çay (kritik=10.0) -> Target: 7.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 20.0, DATEADD(DAY, -89, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'CAY002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 7.0, DATEADD(DAY, -72, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 6.0, DATEADD(DAY, -47, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY002';
END

-- YAG001: Çörek Otu Yağı (kritik=1.5) -> Target: 1.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 3.0, DATEADD(DAY, -83, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'YAG001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -61, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.8, DATEADD(DAY, -36, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG001';
END

-- YAG002: Argan Yağı (kritik=1.0) -> Target: 0.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 2.0, DATEADD(DAY, -77, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'YAG002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.8, DATEADD(DAY, -56, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.7, DATEADD(DAY, -31, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG002';
END

-- KUR001: Antep Fıstığı (kritik=8.0) -> Target: 5.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 15.0, DATEADD(DAY, -86, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.5, DATEADD(DAY, -66, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.5, DATEADD(DAY, -41, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR001';
END

-- KUR002: Badem (kritik=10.0) -> Target: 7.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 18.0, DATEADD(DAY, -88, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 6.0, DATEADD(DAY, -69, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.0, DATEADD(DAY, -44, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR002';
END

-- KUR003: Ceviz (kritik=10.0) -> Target: 6.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 20.0, DATEADD(DAY, -85, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 7.5, DATEADD(DAY, -64, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 6.5, DATEADD(DAY, -39, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR003';
END

-- KUR004: Fındık (kritik=8.0) -> Target: 5.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR004')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 16.0, DATEADD(DAY, -82, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.5, DATEADD(DAY, -60, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.0, DATEADD(DAY, -34, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR004';
END

-- BAK001: Kırmızı Mercimek (kritik=25.0) -> Target: 18.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 50.0, DATEADD(DAY, -90, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAK001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 18.0, DATEADD(DAY, -73, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 14.0, DATEADD(DAY, -48, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK001';
END

-- BAK002: Yeşil Mercimek (kritik=20.0) -> Target: 15.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 40.0, DATEADD(DAY, -87, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAK002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 14.0, DATEADD(DAY, -68, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 11.0, DATEADD(DAY, -43, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK002';
END

-- BAK003: Nohut (kritik=22.0) -> Target: 16.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 45.0, DATEADD(DAY, -84, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAK003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 16.0, DATEADD(DAY, -63, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 13.0, DATEADD(DAY, -37, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK003';
END

-- MAC001: Bitkisel Macun (kritik=5.0) -> Target: 3.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 10.0, DATEADD(DAY, -81, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'MAC001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.0, DATEADD(DAY, -57, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'MAC001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -32, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'MAC001';
END

-- MAC002: Epimedium Macun (kritik=4.0) -> Target: 2.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 8.0, DATEADD(DAY, -79, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'MAC002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -55, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'MAC002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -29, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'MAC002';
END

-- BAL001: Çiçek Balı (kritik=6.0) -> Target: 4.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 12.0, DATEADD(DAY, -83, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAL001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.5, DATEADD(DAY, -61, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAL001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.5, DATEADD(DAY, -36, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAL001';
END

-- TOM001: Chia Tohumu (kritik=4.0) -> Target: 2.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 8.0, DATEADD(DAY, -78, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'TOM001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -53, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TOM001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -27, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TOM001';
END

-- KOZ001: Doğal Sabun (kritik=8.0) -> Target: 5.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 15.0, DATEADD(DAY, -86, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KOZ001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 5.5, DATEADD(DAY, -65, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KOZ001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.5, DATEADD(DAY, -40, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KOZ001';
END

-- TAK001: Spirulina (kritik=4.0) -> Target: 2.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 8.0, DATEADD(DAY, -80, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'TAK001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -58, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TAK001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -33, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TAK001';
END

-- KRM001: Argan Kremi (kritik=5.0) -> Target: 3.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KRM001')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 10.0, DATEADD(DAY, -77, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KRM001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.0, DATEADD(DAY, -52, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KRM001';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -26, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KRM001';
END

PRINT '  ✓ CRITICAL products: 28 ürün için hareketler eklendi';
GO

-- =============================================
-- EMERGENCY PRODUCTS (20 products - 20%)
-- Target: kritik < mevcut < emniyet after all movements
-- Strategy: Initial GIRIS close to emniyet, moderate CIKIS
-- =============================================

PRINT 'EMERGENCY products - stok hareketleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- BAH006: Karanfil (kritik=5.0, emniyet=15.0) -> Target: 10.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH006')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 14.0, DATEADD(DAY, -85, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH006';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.5, DATEADD(DAY, -58, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH006';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.5, DATEADD(DAY, -31, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH006';
END

-- BAH007: Kakule (kritik=3.0, emniyet=10.0) -> Target: 6.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH007')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 9.0, DATEADD(DAY, -82, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH007';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.8, DATEADD(DAY, -55, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH007';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -28, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH007';
END

-- BAH008: Safran (kritik=1.0, emniyet=5.0) -> Target: 3.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH008')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 4.0, DATEADD(DAY, -79, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH008';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.6, DATEADD(DAY, -52, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH008';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.4, DATEADD(DAY, -25, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH008';
END

-- BAH009: Kişniş (kritik=4.0, emniyet=12.0) -> Target: 8.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH009')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 11.0, DATEADD(DAY, -76, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH009';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.8, DATEADD(DAY, -49, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH009';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -22, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH009';
END

-- BTK004: Melisa (kritik=2.0, emniyet=8.0) -> Target: 5.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK004')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 7.0, DATEADD(DAY, -84, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BTK004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -57, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.8, DATEADD(DAY, -30, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK004';
END

-- BTK005: Kuşburnu (kritik=4.0, emniyet=12.0) -> Target: 8.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK005')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 11.0, DATEADD(DAY, -81, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BTK005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.8, DATEADD(DAY, -54, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -27, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BTK005';
END

-- CAY003: Beyaz Çay (kritik=4.0, emniyet=12.0) -> Target: 8.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 11.0, DATEADD(DAY, -78, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'CAY003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.8, DATEADD(DAY, -51, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -24, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY003';
END

-- CAY004: Oolong Çay (kritik=3.0, emniyet=10.0) -> Target: 6.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY004')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 9.0, DATEADD(DAY, -75, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'CAY004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.5, DATEADD(DAY, -48, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.0, DATEADD(DAY, -21, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'CAY004';
END

-- YAG003: Jojoba Yağı (kritik=2.0, emniyet=6.0) -> Target: 4.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 5.5, DATEADD(DAY, -83, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'YAG003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.9, DATEADD(DAY, -56, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.6, DATEADD(DAY, -29, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG003';
END

-- YAG004: Badem Yağı (kritik=2.5, emniyet=8.0) -> Target: 5.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG004')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 7.5, DATEADD(DAY, -80, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'YAG004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -53, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 0.8, DATEADD(DAY, -26, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'YAG004';
END

-- KUR005: Kaju (kritik=5.0, emniyet=15.0) -> Target: 10.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR005')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 14.0, DATEADD(DAY, -87, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.4, DATEADD(DAY, -60, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.6, DATEADD(DAY, -33, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR005';
END

-- KUR006: Kuru Üzüm (kritik=6.0, emniyet=18.0) -> Target: 12.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR006')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 17.0, DATEADD(DAY, -84, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR006';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -57, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR006';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.0, DATEADD(DAY, -30, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR006';
END

-- KUR007: Kuru Kayısı (kritik=4.0, emniyet=12.0) -> Target: 8.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR007')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 11.0, DATEADD(DAY, -81, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KUR007';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.8, DATEADD(DAY, -54, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR007';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -27, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KUR007';
END

-- BAK004: Barbunya Fasulye (kritik=18.0, emniyet=52.0) -> Target: 35.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK004')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 50.0, DATEADD(DAY, -88, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAK004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 9.0, DATEADD(DAY, -61, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK004';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 6.0, DATEADD(DAY, -34, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK004';
END

-- BAK005: Beyaz Pirinç (kritik=30.0, emniyet=90.0) -> Target: 60.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK005')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 85.0, DATEADD(DAY, -85, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAK005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 15.0, DATEADD(DAY, -58, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK005';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 10.0, DATEADD(DAY, -31, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAK005';
END

-- MAC003: Ginseng Macun (kritik=3.0, emniyet=10.0) -> Target: 6.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'MAC003')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 9.0, DATEADD(DAY, -82, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'MAC003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.5, DATEADD(DAY, -55, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'MAC003';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.0, DATEADD(DAY, -28, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'MAC003';
END

-- BAL002: Kestane Balı (kritik=4.0, emniyet=12.0) -> Target: 8.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAL002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 11.0, DATEADD(DAY, -79, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAL002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.8, DATEADD(DAY, -52, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAL002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.2, DATEADD(DAY, -25, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAL002';
END

-- TOM002: Keten Tohumu (kritik=5.0, emniyet=15.0) -> Target: 10.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TOM002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 14.0, DATEADD(DAY, -76, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'TOM002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.4, DATEADD(DAY, -49, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TOM002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.6, DATEADD(DAY, -22, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TOM002';
END

-- KOZ002: Defne Sabunu (kritik=6.0, emniyet=18.0) -> Target: 12.0
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KOZ002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 17.0, DATEADD(DAY, -83, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'KOZ002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 3.0, DATEADD(DAY, -56, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KOZ002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 2.0, DATEADD(DAY, -29, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'KOZ002';
END

-- TAK002: Omega-3 (kritik=3.0, emniyet=10.0) -> Target: 6.5
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'TAK002')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 9.0, DATEADD(DAY, -80, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'TAK002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.5, DATEADD(DAY, -53, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TAK002';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 1.0, DATEADD(DAY, -26, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'TAK002';
END

PRINT '  ✓ EMERGENCY products: 20 ürün için hareketler eklendi';
GO

-- =============================================
-- NORMAL PRODUCTS (Remaining ~52 products - 52%)
-- Target: mevcut >= emniyet after all movements
-- Strategy: Initial GIRIS at emniyet or above, balanced GIRIS/CIKIS
-- =============================================

PRINT 'NORMAL products - stok hareketleri ekleniyor (örnekler)...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- Sample NORMAL products from each category (rest follow similar pattern)

-- BAHARAT NORMAL: BAH010-BAH020 (11 products)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH010')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 25.0, DATEADD(DAY, -86, GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = 'BAH010';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', 4.0, DATEADD(DAY, -59, GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = 'BAH010';

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', 3.0, DATEADD(DAY, -32, GETDATE()), @AdminKullaniciId, 'Yeni giriş'
    FROM urun WHERE urun_kod = 'BAH010';
END

-- Continue for all remaining NORMAL products...
-- (Due to length constraints, showing pattern. In real file, all ~52 products would have similar entries)

-- Batch insert for remaining NORMAL products using CURSOR
DECLARE @UrunKod VARCHAR(20), @KritikStok DECIMAL(18, 2), @EmniyetStok DECIMAL(18, 2);
DECLARE @InitialStock DECIMAL(18, 2), @Cikis1 DECIMAL(18, 2), @Giris2 DECIMAL(18, 2);

DECLARE normal_cursor CURSOR FOR
SELECT u.urun_kod, sa.kritik_stok, sa.emniyet_stok
FROM urun u
INNER JOIN urun_stok_ayar sa ON u.urun_id = sa.urun_id
WHERE u.urun_kod NOT IN (
    -- CRITICAL products
    'BAH001', 'BAH002', 'BAH003', 'BAH004', 'BAH005',
    'BTK001', 'BTK002', 'BTK003',
    'CAY001', 'CAY002',
    'YAG001', 'YAG002',
    'KUR001', 'KUR002', 'KUR003', 'KUR004',
    'BAK001', 'BAK002', 'BAK003',
    'MAC001', 'MAC002',
    'BAL001', 'TOM001', 'KOZ001', 'TAK001', 'KRM001',
    -- EMERGENCY products
    'BAH006', 'BAH007', 'BAH008', 'BAH009',
    'BTK004', 'BTK005',
    'CAY003', 'CAY004',
    'YAG003', 'YAG004',
    'KUR005', 'KUR006', 'KUR007',
    'BAK004', 'BAK005',
    'MAC003', 'BAL002', 'TOM002', 'KOZ002', 'TAK002'
)
AND u.aktif = 1;

OPEN normal_cursor;
FETCH NEXT FROM normal_cursor INTO @UrunKod, @KritikStok, @EmniyetStok;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Calculate movements to keep stock >= emniyet
    SET @InitialStock = @EmniyetStok * 1.5; -- 50% above emniyet
    SET @Cikis1 = @EmniyetStok * 0.3; -- 30% of emniyet
    SET @Giris2 = @EmniyetStok * 0.2; -- 20% of emniyet

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', @InitialStock, DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 10 + 80 AS INT), GETDATE()), @AdminKullaniciId, 'İlk stok girişi'
    FROM urun WHERE urun_kod = @UrunKod;

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'CIKIS', @Cikis1, DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 15 + 45 AS INT), GETDATE()), @AdminKullaniciId, 'Satış'
    FROM urun WHERE urun_kod = @UrunKod;

    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'GIRIS', @Giris2, DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 10 + 20 AS INT), GETDATE()), @AdminKullaniciId, 'Yeni giriş'
    FROM urun WHERE urun_kod = @UrunKod;

    FETCH NEXT FROM normal_cursor INTO @UrunKod, @KritikStok, @EmniyetStok;
END

CLOSE normal_cursor;
DEALLOCATE normal_cursor;

PRINT '  ✓ NORMAL products: ~52 ürün için hareketler eklendi';
GO

-- =============================================
-- ADDITIONAL MOVEMENT SAMPLES (SAYIM, DUZELTME)
-- =============================================

PRINT 'Ek hareketler (SAYIM, DUZELTME) ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- SAYIM examples (3% of movements ~ 5-10 sayım)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH010')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'SAYIM', 20.0, DATEADD(DAY, -15, GETDATE()), @AdminKullaniciId, 'Aylık stok sayımı'
    FROM urun WHERE urun_kod = 'BAH010';
END

IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR008')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'SAYIM', 15.0, DATEADD(DAY, -15, GETDATE()), @AdminKullaniciId, 'Aylık stok sayımı'
    FROM urun WHERE urun_kod = 'KUR008';
END

-- DUZELTME examples (2% of movements ~ 3-5 düzeltme)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY005')
BEGIN
    INSERT INTO stok_hareket (urun_id, hareket_tip, miktar, hareket_tarih, kullanici_id, aciklama)
    SELECT urun_id, 'DUZELTME', -0.5, DATEADD(DAY, -10, GETDATE()), @AdminKullaniciId, 'Fire düzeltmesi'
    FROM urun WHERE urun_kod = 'CAY005';
END

PRINT '  ✓ SAYIM ve DUZELTME hareketleri eklendi';
GO

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Stock Movements Seed TAMAM';
PRINT '========================================';

DECLARE @HareketCount INT, @UrunCount INT;
DECLARE @CriticalUrunCount INT, @EmergencyUrunCount INT, @NormalUrunCount INT;

SELECT @HareketCount = COUNT(*) FROM stok_hareket;
SELECT @UrunCount = COUNT(DISTINCT urun_id) FROM stok_hareket;

-- Verify distribution
;WITH StokOzet AS (
    SELECT
        u.urun_id,
        sa.kritik_stok,
        sa.emniyet_stok,
        ISNULL(SUM(CASE WHEN h.hareket_tip IN ('GIRIS', 'SAYIM') THEN h.miktar ELSE -h.miktar END), 0) AS mevcut_stok
    FROM urun u
    INNER JOIN urun_stok_ayar sa ON u.urun_id = sa.urun_id
    LEFT JOIN stok_hareket h ON u.urun_id = h.urun_id
    WHERE u.aktif = 1
    GROUP BY u.urun_id, sa.kritik_stok, sa.emniyet_stok
)
SELECT
    @CriticalUrunCount = SUM(CASE WHEN mevcut_stok <= kritik_stok THEN 1 ELSE 0 END),
    @EmergencyUrunCount = SUM(CASE WHEN mevcut_stok > kritik_stok AND mevcut_stok < emniyet_stok THEN 1 ELSE 0 END),
    @NormalUrunCount = SUM(CASE WHEN mevcut_stok >= emniyet_stok THEN 1 ELSE 0 END)
FROM StokOzet;

PRINT 'Toplam hareket sayısı: ' + CAST(@HareketCount AS VARCHAR);
PRINT 'Hareketi olan ürün sayısı: ' + CAST(@UrunCount AS VARCHAR);
PRINT '';
PRINT 'Distribution (gerçekleşen):';
PRINT '  - Critical Products (mevcut <= kritik): ' + CAST(@CriticalUrunCount AS VARCHAR) + ' (' + CAST((@CriticalUrunCount * 100.0 / @UrunCount) AS VARCHAR(5)) + '%)';
PRINT '  - Emergency Products (kritik < mevcut < emniyet): ' + CAST(@EmergencyUrunCount AS VARCHAR) + ' (' + CAST((@EmergencyUrunCount * 100.0 / @UrunCount) AS VARCHAR(5)) + '%)';
PRINT '  - Normal Products (mevcut >= emniyet): ' + CAST(@NormalUrunCount AS VARCHAR) + ' (' + CAST((@NormalUrunCount * 100.0 / @UrunCount) AS VARCHAR(5)) + '%)';
PRINT '';
PRINT '========================================';
GO
