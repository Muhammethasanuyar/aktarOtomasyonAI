-- =============================================
-- Sprint 9 - Notifications Seed
-- Bildirimler (30-100 notifications)
-- Distribution: STOK_KRITIK (40%), STOK_ACIL (20%), SIPARIS_TASLAK (15%), SIPARIS_ONAY (10%), SIPARIS_TESLIM (10%), AI_ONAY_BEKLIYOR (5%)
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Notifications Seed başlatılıyor...';
GO

-- =============================================
-- HELPER: Get Admin User ID
-- =============================================

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

PRINT 'Kullanıcı ID: ' + CAST(@AdminKullaniciId AS VARCHAR);
GO

-- =============================================
-- STOK_KRITIK Notifications (24 of 60 = 40%)
-- =============================================

PRINT 'STOK_KRITIK bildirimleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- BAH001: Karabiber
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Karabiber (Tane)',
    'Karabiber (Tane) ürünü kritik stok seviyesinde. Mevcut: 3.0, Kritik: 5.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -2, GETDATE())
FROM urun WHERE urun_kod = 'BAH001';

-- BAH002: Kimyon
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Kimyon',
    'Kimyon ürünü kritik stok seviyesinde. Mevcut: 2.5, Kritik: 4.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -5, GETDATE())
FROM urun WHERE urun_kod = 'BAH002';

-- BAH003: Zerdeçal
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Zerdeçal (Toz)',
    'Zerdeçal (Toz) ürünü kritik stok seviyesinde. Mevcut: 4.0, Kritik: 6.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -8, GETDATE())
FROM urun WHERE urun_kod = 'BAH003';

-- BAH004: Kırmızı Pul Biber
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Kırmızı Pul Biber',
    'Kırmızı Pul Biber ürünü kritik stok seviyesinde. Mevcut: 5.0, Kritik: 8.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -1, GETDATE())
FROM urun WHERE urun_kod = 'BAH004';

-- BAH005: Tarçın
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Tarçın (Çubuk)',
    'Tarçın (Çubuk) ürünü kritik stok seviyesinde. Mevcut: 3.5, Kritik: 5.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -12, GETDATE())
FROM urun WHERE urun_kod = 'BAH005';

-- BTK001: Ihlamur
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Ihlamur',
    'Ihlamur ürünü kritik stok seviyesinde. Mevcut: 2.0, Kritik: 4.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -15, GETDATE())
FROM urun WHERE urun_kod = 'BTK001';

-- BTK002: Papatya
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Papatya',
    'Papatya ürünü kritik stok seviyesinde. Mevcut: 3.0, Kritik: 5.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -2, GETDATE())
FROM urun WHERE urun_kod = 'BTK002';

-- BTK003: Adaçayı
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Adaçayı',
    'Adaçayı ürünü kritik stok seviyesinde. Mevcut: 1.5, Kritik: 3.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -18, GETDATE())
FROM urun WHERE urun_kod = 'BTK003';

-- CAY001: Yeşil Çay
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Yeşil Çay (Organik)',
    'Yeşil Çay (Organik) ürünü kritik stok seviyesinde. Mevcut: 5.0, Kritik: 8.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -4, GETDATE())
FROM urun WHERE urun_kod = 'CAY001';

-- CAY002: Siyah Çay
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Siyah Çay (Ceylon)',
    'Siyah Çay (Ceylon) ürünü kritik stok seviyesinde. Mevcut: 7.0, Kritik: 10.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -10, GETDATE())
FROM urun WHERE urun_kod = 'CAY002';

-- YAG001: Çörek Otu Yağı
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Çörek Otu Yağı',
    'Çörek Otu Yağı ürünü kritik stok seviyesinde. Mevcut: 1.0, Kritik: 1.5',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -3, GETDATE())
FROM urun WHERE urun_kod = 'YAG001';

-- YAG002: Argan Yağı
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Argan Yağı (Fas)',
    'Argan Yağı (Fas) ürünü kritik stok seviyesinde. Mevcut: 0.5, Kritik: 1.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -6, GETDATE())
FROM urun WHERE urun_kod = 'YAG002';

-- KUR001: Antep Fıstığı
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Antep Fıstığı (İçi)',
    'Antep Fıstığı (İçi) ürünü kritik stok seviyesinde. Mevcut: 5.0, Kritik: 8.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -20, GETDATE())
FROM urun WHERE urun_kod = 'KUR001';

-- KUR002: Badem
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Badem (İçi)',
    'Badem (İçi) ürünü kritik stok seviyesinde. Mevcut: 7.0, Kritik: 10.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -4, GETDATE())
FROM urun WHERE urun_kod = 'KUR002';

-- KUR003: Ceviz
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Ceviz İçi',
    'Ceviz İçi ürünü kritik stok seviyesinde. Mevcut: 6.0, Kritik: 10.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -14, GETDATE())
FROM urun WHERE urun_kod = 'KUR003';

-- KUR004: Fındık
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Fındık (Levant)',
    'Fındık (Levant) ürünü kritik stok seviyesinde. Mevcut: 5.5, Kritik: 8.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -22, GETDATE())
FROM urun WHERE urun_kod = 'KUR004';

-- BAK001: Kırmızı Mercimek
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Kırmızı Mercimek',
    'Kırmızı Mercimek ürünü kritik stok seviyesinde. Mevcut: 18.0, Kritik: 25.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -5, GETDATE())
FROM urun WHERE urun_kod = 'BAK001';

-- BAK002: Yeşil Mercimek
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Yeşil Mercimek',
    'Yeşil Mercimek ürünü kritik stok seviyesinde. Mevcut: 15.0, Kritik: 20.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -16, GETDATE())
FROM urun WHERE urun_kod = 'BAK002';

-- BAK003: Nohut
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Nohut (9 mm)',
    'Nohut (9 mm) ürünü kritik stok seviyesinde. Mevcut: 16.0, Kritik: 22.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -24, GETDATE())
FROM urun WHERE urun_kod = 'BAK003';

-- MAC001: Bitkisel Macun
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Bitkisel Karışım Macun',
    'Bitkisel Karışım Macun ürünü kritik stok seviyesinde. Mevcut: 3.0, Kritik: 5.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -3, GETDATE())
FROM urun WHERE urun_kod = 'MAC001';

-- MAC002: Epimedium Macun
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Epimedium Macun (1000g)',
    'Epimedium Macun (1000g) ürünü kritik stok seviyesinde. Mevcut: 2.5, Kritik: 4.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -6, GETDATE())
FROM urun WHERE urun_kod = 'MAC002';

-- BAL001: Çiçek Balı
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Çiçek Balı (Doğal)',
    'Çiçek Balı (Doğal) ürünü kritik stok seviyesinde. Mevcut: 4.0, Kritik: 6.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -9, GETDATE())
FROM urun WHERE urun_kod = 'BAL001';

-- TOM001: Chia Tohumu
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: Chia Tohumu (Organik)',
    'Chia Tohumu (Organik) ürünü kritik stok seviyesinde. Mevcut: 2.5, Kritik: 4.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -11, GETDATE())
FROM urun WHERE urun_kod = 'TOM001';

-- KOZ001: Doğal Sabun
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_KRITIK', 'Kritik Stok: El Yapımı Doğal Sabun',
    'El Yapımı Doğal Sabun ürünü kritik stok seviyesinde. Mevcut: 5.0, Kritik: 8.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -7, GETDATE())
FROM urun WHERE urun_kod = 'KOZ001';

PRINT '  ✓ STOK_KRITIK: 24 bildirim eklendi';
GO

-- =============================================
-- STOK_ACIL Notifications (12 of 60 = 20%)
-- =============================================

PRINT 'STOK_ACIL bildirimleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- BAH006: Karanfil
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Karanfil emniyet stok seviyesinde',
    'Karanfil ürünü emniyet stok seviyesinde. Mevcut: 10.0, Emniyet: 15.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -1, GETDATE())
FROM urun WHERE urun_kod = 'BAH006';

-- BAH007: Kakule
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Kakule (Tane) emniyet stok seviyesinde',
    'Kakule (Tane) ürünü emniyet stok seviyesinde. Mevcut: 6.0, Emniyet: 10.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -1, GETDATE())
FROM urun WHERE urun_kod = 'BAH007';

-- BAH008: Safran
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Safran (İran) emniyet stok seviyesinde',
    'Safran (İran) ürünü emniyet stok seviyesinde. Mevcut: 3.0, Emniyet: 5.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -3, GETDATE())
FROM urun WHERE urun_kod = 'BAH008';

-- BAH009: Kişniş
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Kişniş (Tane) emniyet stok seviyesinde',
    'Kişniş (Tane) ürünü emniyet stok seviyesinde. Mevcut: 8.0, Emniyet: 12.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -6, GETDATE())
FROM urun WHERE urun_kod = 'BAH009';

-- BTK004: Melisa
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Melisa emniyet stok seviyesinde',
    'Melisa ürünü emniyet stok seviyesinde. Mevcut: 5.0, Emniyet: 8.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -9, GETDATE())
FROM urun WHERE urun_kod = 'BTK004';

-- BTK005: Kuşburnu
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Kuşburnu emniyet stok seviyesinde',
    'Kuşburnu ürünü emniyet stok seviyesinde. Mevcut: 8.0, Emniyet: 12.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -2, GETDATE())
FROM urun WHERE urun_kod = 'BTK005';

-- CAY003: Beyaz Çay
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Beyaz Çay (Silver Needle) emniyet stok seviyesinde',
    'Beyaz Çay (Silver Needle) ürünü emniyet stok seviyesinde. Mevcut: 8.0, Emniyet: 12.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -12, GETDATE())
FROM urun WHERE urun_kod = 'CAY003';

-- YAG003: Jojoba Yağı
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Jojoba Yağı (Organik) emniyet stok seviyesinde',
    'Jojoba Yağı (Organik) ürünü emniyet stok seviyesinde. Mevcut: 4.0, Emniyet: 6.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -15, GETDATE())
FROM urun WHERE urun_kod = 'YAG003';

-- KUR005: Kaju
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Kaju (Ham) emniyet stok seviyesinde',
    'Kaju (Ham) ürünü emniyet stok seviyesinde. Mevcut: 10.0, Emniyet: 15.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -18, GETDATE())
FROM urun WHERE urun_kod = 'KUR005';

-- KUR006: Kuru Üzüm
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Kuru Üzüm (Sultani) emniyet stok seviyesinde',
    'Kuru Üzüm (Sultani) ürünü emniyet stok seviyesinde. Mevcut: 12.0, Emniyet: 18.0',
    'URUN', urun_id, NULL, 1, DATEADD(DAY, -3, GETDATE())
FROM urun WHERE urun_kod = 'KUR006';

-- BAK004: Barbunya Fasulye
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Barbunya Fasulye emniyet stok seviyesinde',
    'Barbunya Fasulye ürünü emniyet stok seviyesinde. Mevcut: 35.0, Emniyet: 52.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -21, GETDATE())
FROM urun WHERE urun_kod = 'BAK004';

-- MAC003: Ginseng Macun
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'STOK_ACIL', 'Acil: Ginseng Macun (1000g) emniyet stok seviyesinde',
    'Ginseng Macun (1000g) ürünü emniyet stok seviyesinde. Mevcut: 6.5, Emniyet: 10.0',
    'URUN', urun_id, NULL, 0, DATEADD(HOUR, -4, GETDATE())
FROM urun WHERE urun_kod = 'MAC003';

PRINT '  ✓ STOK_ACIL: 12 bildirim eklendi';
GO

-- =============================================
-- SIPARIS_TASLAK Notifications (9 of 60 = 15%)
-- =============================================

PRINT 'SIPARIS_TASLAK bildirimleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- Get siparis IDs with TASLAK status
DECLARE @Sip1Id INT, @Sip6Id INT, @Sip11Id INT, @Sip20Id INT;
SELECT @Sip1Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TASLAK' ORDER BY siparis_id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip6Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TASLAK' ORDER BY siparis_id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip11Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TASLAK' ORDER BY siparis_id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip20Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TASLAK' ORDER BY siparis_id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY;

-- Sipariş 1: TASLAK
IF @Sip1Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TASLAK', 'Yeni Sipariş Taslağı: ' + siparis_no,
    'Yeni sipariş taslağı oluşturuldu. Tedarikçi: ' + t.tedarikci_adi + ', Toplam: ' + CAST(toplam_tutar AS VARCHAR),
    'SIPARIS', @Sip1Id, NULL, 0, DATEADD(HOUR, -2, GETDATE())
FROM siparis s
INNER JOIN tedarikci t ON s.tedarikci_id = t.tedarikci_id
WHERE s.siparis_id = @Sip1Id;

-- Sipariş 6: TASLAK
IF @Sip6Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TASLAK', 'Yeni Sipariş Taslağı: ' + siparis_no,
    'Yeni sipariş taslağı oluşturuldu. Tedarikçi: ' + t.tedarikci_adi + ', Toplam: ' + CAST(toplam_tutar AS VARCHAR),
    'SIPARIS', @Sip6Id, NULL, 1, DATEADD(DAY, -1, GETDATE())
FROM siparis s
INNER JOIN tedarikci t ON s.tedarikci_id = t.tedarikci_id
WHERE s.siparis_id = @Sip6Id;

-- Sipariş 11: TASLAK
IF @Sip11Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TASLAK', 'Yeni Sipariş Taslağı: ' + siparis_no,
    'Yeni sipariş taslağı oluşturuldu. Tedarikçi: ' + t.tedarikci_adi + ', Toplam: ' + CAST(toplam_tutar AS VARCHAR),
    'SIPARIS', @Sip11Id, NULL, 0, DATEADD(DAY, -3, GETDATE())
FROM siparis s
INNER JOIN tedarikci t ON s.tedarikci_id = t.tedarikci_id
WHERE s.siparis_id = @Sip11Id;

-- Sipariş 20: TASLAK
IF @Sip20Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TASLAK', 'Yeni Sipariş Taslağı: ' + siparis_no,
    'Yeni sipariş taslağı oluşturuldu. Tedarikçi: ' + t.tedarikci_adi + ', Toplam: ' + CAST(toplam_tutar AS VARCHAR),
    'SIPARIS', @Sip20Id, NULL, 0, DATEADD(MINUTE, -30, GETDATE())
FROM siparis s
INNER JOIN tedarikci t ON s.tedarikci_id = t.tedarikci_id
WHERE s.siparis_id = @Sip20Id;

-- Additional generic TASLAK notifications (5 more to reach 9 total)
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('SIPARIS_TASLAK', 'Bekleyen Sipariş Taslakları', 'Onay bekleyen 4 sipariş taslağı var. Lütfen kontrol edin.', NULL, NULL, NULL, 0, DATEADD(HOUR, -1, GETDATE()));

INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('SIPARIS_TASLAK', 'Sipariş Taslağı Hatırlatma', 'Tamamlanmamış sipariş taslakları mevcut. İşlem gerekiyor.', NULL, NULL, NULL, 1, DATEADD(DAY, -2, GETDATE()));

INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('SIPARIS_TASLAK', 'Kritik Ürün Siparişi Önerisi', 'Kritik stok seviyesindeki ürünler için sipariş oluşturulması önerilir.', NULL, NULL, NULL, 0, DATEADD(HOUR, -5, GETDATE()));

INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('SIPARIS_TASLAK', 'Aylık Sipariş Planlama', 'Aylık sipariş planlama için taslak hazırlanabilir.', NULL, NULL, NULL, 1, DATEADD(DAY, -4, GETDATE()));

INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('SIPARIS_TASLAK', 'Sezonluk Sipariş Hatırlatma', 'Sezonluk ürünler için sipariş taslağı oluşturulması önerilir.', NULL, NULL, NULL, 0, DATEADD(DAY, -7, GETDATE()));

PRINT '  ✓ SIPARIS_TASLAK: 9 bildirim eklendi';
GO

-- =============================================
-- SIPARIS_ONAY Notifications (6 of 60 = 10%)
-- =============================================

PRINT 'SIPARIS_ONAY bildirimleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- Get siparis IDs with GONDERILDI status
DECLARE @Sip2Id INT, @Sip7Id INT, @Sip8Id INT, @Sip12Id INT, @Sip13Id INT, @Sip17Id INT;
SELECT @Sip2Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'GONDERILDI' ORDER BY siparis_id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip7Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'GONDERILDI' ORDER BY siparis_id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip8Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'GONDERILDI' ORDER BY siparis_id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip12Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'GONDERILDI' ORDER BY siparis_id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip13Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'GONDERILDI' ORDER BY siparis_id OFFSET 4 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip17Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'GONDERILDI' ORDER BY siparis_id OFFSET 5 ROWS FETCH NEXT 1 ROWS ONLY;

-- Sipariş 2: GONDERILDI
IF @Sip2Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_ONAY', 'Sipariş Gönderildi: ' + siparis_no,
    'Sipariş tedarikçiye gönderildi. Beklenen teslim: ' + CONVERT(VARCHAR, beklenen_teslim_tarih, 103),
    'SIPARIS', @Sip2Id, NULL, 1, DATEADD(DAY, -14, GETDATE())
FROM siparis WHERE siparis_id = @Sip2Id;

-- Sipariş 7: GONDERILDI
IF @Sip7Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_ONAY', 'Sipariş Gönderildi: ' + siparis_no,
    'Sipariş tedarikçiye gönderildi. Beklenen teslim: ' + CONVERT(VARCHAR, beklenen_teslim_tarih, 103),
    'SIPARIS', @Sip7Id, NULL, 0, DATEADD(DAY, -11, GETDATE())
FROM siparis WHERE siparis_id = @Sip7Id;

-- Sipariş 8: GONDERILDI
IF @Sip8Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_ONAY', 'Sipariş Gönderildi: ' + siparis_no,
    'Sipariş tedarikçiye gönderildi. Beklenen teslim: ' + CONVERT(VARCHAR, beklenen_teslim_tarih, 103),
    'SIPARIS', @Sip8Id, NULL, 0, DATEADD(DAY, -9, GETDATE())
FROM siparis WHERE siparis_id = @Sip8Id;

-- Sipariş 12: GONDERILDI
IF @Sip12Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_ONAY', 'Sipariş Gönderildi: ' + siparis_no,
    'Sipariş tedarikçiye gönderildi. Beklenen teslim: ' + CONVERT(VARCHAR, beklenen_teslim_tarih, 103),
    'SIPARIS', @Sip12Id, NULL, 1, DATEADD(DAY, -7, GETDATE())
FROM siparis WHERE siparis_id = @Sip12Id;

-- Sipariş 13: GONDERILDI
IF @Sip13Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_ONAY', 'Sipariş Gönderildi: ' + siparis_no,
    'Sipariş tedarikçiye gönderildi. Beklenen teslim: ' + CONVERT(VARCHAR, beklenen_teslim_tarih, 103),
    'SIPARIS', @Sip13Id, NULL, 0, DATEADD(DAY, -5, GETDATE())
FROM siparis WHERE siparis_id = @Sip13Id;

-- Sipariş 17: GONDERILDI
IF @Sip17Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_ONAY', 'Sipariş Gönderildi: ' + siparis_no,
    'Sipariş tedarikçiye gönderildi. Beklenen teslim: ' + CONVERT(VARCHAR, beklenen_teslim_tarih, 103),
    'SIPARIS', @Sip17Id, NULL, 0, DATEADD(DAY, -3, GETDATE())
FROM siparis WHERE siparis_id = @Sip17Id;

PRINT '  ✓ SIPARIS_ONAY: 6 bildirim eklendi';
GO

-- =============================================
-- SIPARIS_TESLIM Notifications (6 of 60 = 10%)
-- =============================================

PRINT 'SIPARIS_TESLIM bildirimleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- Get siparis IDs with TAMAMLANDI status
DECLARE @Sip4Id INT, @Sip10Id INT, @Sip16Id INT, @Sip19Id INT;
SELECT @Sip4Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TAMAMLANDI' ORDER BY siparis_id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip10Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TAMAMLANDI' ORDER BY siparis_id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip16Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TAMAMLANDI' ORDER BY siparis_id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip19Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'TAMAMLANDI' ORDER BY siparis_id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY;

-- Sipariş 4: TAMAMLANDI
IF @Sip4Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TESLIM', 'Sipariş Teslim Alındı: ' + siparis_no,
    'Sipariş tam olarak teslim alındı. Teslim tarihi: ' + CONVERT(VARCHAR, teslim_tarih, 103),
    'SIPARIS', @Sip4Id, NULL, 1, DATEADD(DAY, -33, GETDATE())
FROM siparis WHERE siparis_id = @Sip4Id;

-- Sipariş 10: TAMAMLANDI
IF @Sip10Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TESLIM', 'Sipariş Teslim Alındı: ' + siparis_no,
    'Sipariş tam olarak teslim alındı. Teslim tarihi: ' + CONVERT(VARCHAR, teslim_tarih, 103),
    'SIPARIS', @Sip10Id, NULL, 1, DATEADD(DAY, -38, GETDATE())
FROM siparis WHERE siparis_id = @Sip10Id;

-- Sipariş 16: TAMAMLANDI
IF @Sip16Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TESLIM', 'Sipariş Teslim Alındı: ' + siparis_no,
    'Sipariş tam olarak teslim alındı. Teslim tarihi: ' + CONVERT(VARCHAR, teslim_tarih, 103),
    'SIPARIS', @Sip16Id, NULL, 1, DATEADD(DAY, -43, GETDATE())
FROM siparis WHERE siparis_id = @Sip16Id;

-- Sipariş 19: TAMAMLANDI
IF @Sip19Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TESLIM', 'Sipariş Teslim Alındı: ' + siparis_no,
    'Sipariş tam olarak teslim alındı. Teslim tarihi: ' + CONVERT(VARCHAR, teslim_tarih, 103),
    'SIPARIS', @Sip19Id, NULL, 1, DATEADD(DAY, -27, GETDATE())
FROM siparis WHERE siparis_id = @Sip19Id;

-- KISMI teslim notifications (2)
DECLARE @Sip3Id INT, @Sip9Id INT;
SELECT @Sip3Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'KISMI' ORDER BY siparis_id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Sip9Id = siparis_id FROM siparis WHERE siparis_no LIKE 'SIP%' AND durum = 'KISMI' ORDER BY siparis_id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY;

IF @Sip3Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TESLIM', 'Kısmi Teslimat: ' + siparis_no,
    'Sipariş kısmen teslim alındı. Eksik ürünler için takip devam ediyor.',
    'SIPARIS', @Sip3Id, NULL, 0, DATEADD(DAY, -17, GETDATE())
FROM siparis WHERE siparis_id = @Sip3Id;

IF @Sip9Id IS NOT NULL
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
SELECT 'SIPARIS_TESLIM', 'Kısmi Teslimat: ' + siparis_no,
    'Sipariş kısmen teslim alındı. Eksik ürünler için takip devam ediyor.',
    'SIPARIS', @Sip9Id, NULL, 0, DATEADD(DAY, -20, GETDATE())
FROM siparis WHERE siparis_id = @Sip9Id;

PRINT '  ✓ SIPARIS_TESLIM: 6 bildirim eklendi';
GO

-- =============================================
-- AI_ONAY_BEKLIYOR Notifications (3 of 60 = 5%)
-- =============================================

PRINT 'AI_ONAY_BEKLIYOR bildirimleri ekleniyor...';

DECLARE @AdminKullaniciId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- Sample AI content approval notifications (will be linked to AI content in next seed file)
INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('AI_ONAY_BEKLIYOR', 'AI İçerik Onay Bekliyor', '5 ürün için AI oluşturulmuş içerik onay bekliyor. Lütfen kontrol edin.', NULL, NULL, NULL, 0, DATEADD(HOUR, -1, GETDATE()));

INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('AI_ONAY_BEKLIYOR', 'Yeni AI İçerik Önerileri', 'Popüler ürünler için AI içerik önerileri hazır. Onayınız bekleniyor.', NULL, NULL, NULL, 0, DATEADD(DAY, -1, GETDATE()));

INSERT INTO bildirim (bildirim_tip, baslik, icerik, referans_tip, referans_id, kullanici_id, okundu, olusturma_tarih)
VALUES ('AI_ONAY_BEKLIYOR', 'AI İçerik Hatırlatma', 'Onay bekleyen AI içerikler mevcut. Müşterilere sunmak için onaylayın.', NULL, NULL, NULL, 1, DATEADD(DAY, -2, GETDATE()));

PRINT '  ✓ AI_ONAY_BEKLIYOR: 3 bildirim eklendi';
GO

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Notifications Seed TAMAM';
PRINT '========================================';

DECLARE @BildirimCount INT, @OkunmayanCount INT, @OkunanCount INT;
DECLARE @StokKritikCount INT, @StokAcilCount INT, @SiparisTaslakCount INT;
DECLARE @SiparisOnayCount INT, @SiparisTeslimCount INT, @AIOnayCount INT;

SELECT @BildirimCount = COUNT(*) FROM bildirim;
SELECT @OkunmayanCount = COUNT(*) FROM bildirim WHERE okundu = 0;
SELECT @OkunanCount = COUNT(*) FROM bildirim WHERE okundu = 1;

SELECT @StokKritikCount = COUNT(*) FROM bildirim WHERE bildirim_tip = 'STOK_KRITIK';
SELECT @StokAcilCount = COUNT(*) FROM bildirim WHERE bildirim_tip = 'STOK_ACIL';
SELECT @SiparisTaslakCount = COUNT(*) FROM bildirim WHERE bildirim_tip = 'SIPARIS_TASLAK';
SELECT @SiparisOnayCount = COUNT(*) FROM bildirim WHERE bildirim_tip = 'SIPARIS_ONAY';
SELECT @SiparisTeslimCount = COUNT(*) FROM bildirim WHERE bildirim_tip = 'SIPARIS_TESLIM';
SELECT @AIOnayCount = COUNT(*) FROM bildirim WHERE bildirim_tip = 'AI_ONAY_BEKLIYOR';

PRINT 'Toplam bildirim sayısı: ' + CAST(@BildirimCount AS VARCHAR);
PRINT '  - Okunmayan: ' + CAST(@OkunmayanCount AS VARCHAR) + ' (' + CAST((@OkunmayanCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '  - Okunan: ' + CAST(@OkunanCount AS VARCHAR) + ' (' + CAST((@OkunanCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '';
PRINT 'Tip dağılımı:';
PRINT '  - STOK_KRITIK: ' + CAST(@StokKritikCount AS VARCHAR) + ' (' + CAST((@StokKritikCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '  - STOK_ACIL: ' + CAST(@StokAcilCount AS VARCHAR) + ' (' + CAST((@StokAcilCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '  - SIPARIS_TASLAK: ' + CAST(@SiparisTaslakCount AS VARCHAR) + ' (' + CAST((@SiparisTaslakCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '  - SIPARIS_ONAY: ' + CAST(@SiparisOnayCount AS VARCHAR) + ' (' + CAST((@SiparisOnayCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '  - SIPARIS_TESLIM: ' + CAST(@SiparisTeslimCount AS VARCHAR) + ' (' + CAST((@SiparisTeslimCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '  - AI_ONAY_BEKLIYOR: ' + CAST(@AIOnayCount AS VARCHAR) + ' (' + CAST((@AIOnayCount * 100.0 / @BildirimCount) AS VARCHAR(5)) + '%)';
PRINT '';
PRINT '========================================';
GO
