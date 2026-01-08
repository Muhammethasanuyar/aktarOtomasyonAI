-- =============================================
-- Sprint 9 - Orders Seed
-- Siparişler (10-30 orders with 3-12 line items)
-- Status distribution: TASLAK (20%), GONDERILDI (30%), KISMI (25%), TAMAMLANDI (20%), IPTAL (5%)
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Orders Seed başlatılıyor...';
GO

-- =============================================
-- HELPER: Get User and Supplier IDs
-- =============================================

DECLARE @AdminKullaniciId INT;
DECLARE @Ted001Id INT, @Ted002Id INT, @Ted003Id INT, @Ted004Id INT, @Ted005Id INT;
DECLARE @Ted006Id INT, @Ted007Id INT, @Ted008Id INT, @Ted009Id INT, @Ted010Id INT;

SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

SELECT @Ted001Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED001';
SELECT @Ted002Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED002';
SELECT @Ted003Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED003';
SELECT @Ted004Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED004';
SELECT @Ted005Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED005';
SELECT @Ted006Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED006';
SELECT @Ted007Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED007';
SELECT @Ted008Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED008';
SELECT @Ted009Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED009';
SELECT @Ted010Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED010';

PRINT 'Kullanıcı ID: ' + CAST(@AdminKullaniciId AS VARCHAR);
PRINT 'Tedarikçi sayısı hazır: 10';
GO

-- =============================================
-- ORDER 1: TASLAK (1 of 4)
-- =============================================

PRINT 'Sipariş 1: TASLAK (TED001)';

DECLARE @AdminKullaniciId INT, @Ted001Id INT, @Siparis1Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted001Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED001';

EXEC sp_siparis_taslak_olustur @Ted001Id, @AdminKullaniciId, @Siparis1Id OUTPUT;

-- Update dates
UPDATE siparis
SET siparis_tarih = DATEADD(DAY, -2, GETDATE()),
    beklenen_teslim_tarih = DATEADD(DAY, 5, GETDATE()),
    durum = 'TASLAK'
WHERE siparis_id = @Siparis1Id;

-- Add line items (5 items)
EXEC sp_siparis_satir_ekle @Siparis1Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH001'), 10.0, 200.00;
EXEC sp_siparis_satir_ekle @Siparis1Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH002'), 8.0, 180.00;
EXEC sp_siparis_satir_ekle @Siparis1Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK001'), 12.0, 140.00;
EXEC sp_siparis_satir_ekle @Siparis1Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY001'), 15.0, 120.00;
EXEC sp_siparis_satir_ekle @Siparis1Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR001'), 20.0, 350.00;

PRINT '  ✓ Sipariş 1 oluşturuldu (TASLAK, 5 satır)';
GO

-- =============================================
-- ORDER 2: GONDERILDI (1 of 6)
-- =============================================

PRINT 'Sipariş 2: GONDERILDI (TED002)';

DECLARE @AdminKullaniciId INT, @Ted002Id INT, @Siparis2Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted002Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED002';

EXEC sp_siparis_taslak_olustur @Ted002Id, @AdminKullaniciId, @Siparis2Id OUTPUT;

-- Update dates and status
UPDATE siparis
SET siparis_tarih = DATEADD(DAY, -15, GETDATE()),
    gonderilme_tarih = DATEADD(DAY, -14, GETDATE()),
    beklenen_teslim_tarih = DATEADD(DAY, 1, GETDATE()),
    durum = 'GONDERILDI'
WHERE siparis_id = @Siparis2Id;

-- Add line items (8 items)
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH003'), 15.0, 190.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH004'), 20.0, 170.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK002'), 10.0, 130.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY002'), 25.0, 110.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG001'), 5.0, 280.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR002'), 18.0, 320.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK001'), 50.0, 95.00;
EXEC sp_siparis_satir_ekle @Siparis2Id, (SELECT urun_id FROM urun WHERE urun_kod = 'MAC001'), 12.0, 220.00;

PRINT '  ✓ Sipariş 2 oluşturuldu (GONDERILDI, 8 satır)';
GO

-- =============================================
-- ORDER 3: KISMI (1 of 5)
-- =============================================

PRINT 'Sipariş 3: KISMI (TED003)';

DECLARE @AdminKullaniciId INT, @Ted003Id INT, @Siparis3Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted003Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED003';

EXEC sp_siparis_taslak_olustur @Ted003Id, @AdminKullaniciId, @Siparis3Id OUTPUT;

-- Update dates and status
UPDATE siparis
SET siparis_tarih = DATEADD(DAY, -25, GETDATE()),
    gonderilme_tarih = DATEADD(DAY, -24, GETDATE()),
    teslim_tarih = DATEADD(DAY, -17, GETDATE()),
    beklenen_teslim_tarih = DATEADD(DAY, -17, GETDATE()),
    durum = 'KISMI'
WHERE siparis_id = @Siparis3Id;

-- Add line items (6 items with partial delivery)
EXEC sp_siparis_satir_ekle @Siparis3Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH005'), 12.0, 160.00;
EXEC sp_siparis_satir_ekle @Siparis3Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK003'), 8.0, 125.00;
EXEC sp_siparis_satir_ekle @Siparis3Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY003'), 10.0, 115.00;
EXEC sp_siparis_satir_ekle @Siparis3Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG002'), 3.0, 420.00;
EXEC sp_siparis_satir_ekle @Siparis3Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR003'), 22.0, 280.00;
EXEC sp_siparis_satir_ekle @Siparis3Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK002'), 40.0, 88.00;

-- Update teslim_miktar for partial delivery (50-80% delivered)
UPDATE siparis_satir SET teslim_miktar = 8.0 WHERE siparis_id = @Siparis3Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH005'); -- 66%
UPDATE siparis_satir SET teslim_miktar = 6.0 WHERE siparis_id = @Siparis3Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK003'); -- 75%
UPDATE siparis_satir SET teslim_miktar = 5.0 WHERE siparis_id = @Siparis3Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY003'); -- 50%
UPDATE siparis_satir SET teslim_miktar = 3.0 WHERE siparis_id = @Siparis3Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG002'); -- 100%
UPDATE siparis_satir SET teslim_miktar = 18.0 WHERE siparis_id = @Siparis3Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR003'); -- 81%
UPDATE siparis_satir SET teslim_miktar = 0.0 WHERE siparis_id = @Siparis3Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK002'); -- 0%

PRINT '  ✓ Sipariş 3 oluşturuldu (KISMI, 6 satır)';
GO

-- =============================================
-- ORDER 4: TAMAMLANDI (1 of 4)
-- =============================================

PRINT 'Sipariş 4: TAMAMLANDI (TED004)';

DECLARE @AdminKullaniciId INT, @Ted004Id INT, @Siparis4Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted004Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED004';

EXEC sp_siparis_taslak_olustur @Ted004Id, @AdminKullaniciId, @Siparis4Id OUTPUT;

-- Update dates and status
UPDATE siparis
SET siparis_tarih = DATEADD(DAY, -40, GETDATE()),
    gonderilme_tarih = DATEADD(DAY, -39, GETDATE()),
    teslim_tarih = DATEADD(DAY, -33, GETDATE()),
    beklenen_teslim_tarih = DATEADD(DAY, -33, GETDATE()),
    durum = 'TAMAMLANDI'
WHERE siparis_id = @Siparis4Id;

-- Add line items (10 items, all fully delivered)
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH006'), 10.0, 175.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH007'), 6.0, 210.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK004'), 8.0, 135.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY004'), 12.0, 105.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG003'), 4.0, 310.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR004'), 16.0, 295.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK003'), 45.0, 92.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'MAC002'), 10.0, 240.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAL001'), 15.0, 180.00;
EXEC sp_siparis_satir_ekle @Siparis4Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TOM001'), 9.0, 155.00;

-- Update teslim_miktar for full delivery
UPDATE siparis_satir SET teslim_miktar = miktar WHERE siparis_id = @Siparis4Id;

PRINT '  ✓ Sipariş 4 oluşturuldu (TAMAMLANDI, 10 satır)';
GO

-- =============================================
-- ORDER 5: IPTAL (1 of 1)
-- =============================================

PRINT 'Sipariş 5: IPTAL (TED005)';

DECLARE @AdminKullaniciId INT, @Ted005Id INT, @Siparis5Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted005Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED005';

EXEC sp_siparis_taslak_olustur @Ted005Id, @AdminKullaniciId, @Siparis5Id OUTPUT;

-- Update dates and status
UPDATE siparis
SET siparis_tarih = DATEADD(DAY, -20, GETDATE()),
    iptal_tarih = DATEADD(DAY, -18, GETDATE()),
    beklenen_teslim_tarih = DATEADD(DAY, -13, GETDATE()),
    durum = 'IPTAL'
WHERE siparis_id = @Siparis5Id;

-- Add line items (3 items, cancelled)
EXEC sp_siparis_satir_ekle @Siparis5Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH008'), 2.0, 650.00;
EXEC sp_siparis_satir_ekle @Siparis5Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG004'), 5.0, 290.00;
EXEC sp_siparis_satir_ekle @Siparis5Id, (SELECT urun_id FROM urun WHERE urun_kod = 'MAC003'), 8.0, 230.00;

PRINT '  ✓ Sipariş 5 oluşturuldu (IPTAL, 3 satır)';
GO

-- =============================================
-- ORDERS 6-20: Additional orders for distribution
-- =============================================

PRINT 'Sipariş 6-20: Ek siparişler ekleniyor...';

-- ORDER 6: TASLAK (2 of 4)
DECLARE @AdminKullaniciId INT, @Ted006Id INT, @Siparis6Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted006Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED006';

EXEC sp_siparis_taslak_olustur @Ted006Id, @AdminKullaniciId, @Siparis6Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -1, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 6, GETDATE()), durum = 'TASLAK' WHERE siparis_id = @Siparis6Id;
EXEC sp_siparis_satir_ekle @Siparis6Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK005'), 10.0, 145.00;
EXEC sp_siparis_satir_ekle @Siparis6Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY005'), 12.0, 118.00;
EXEC sp_siparis_satir_ekle @Siparis6Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR005'), 14.0, 305.00;
EXEC sp_siparis_satir_ekle @Siparis6Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK004'), 38.0, 87.00;
GO

-- ORDER 7: GONDERILDI (2 of 6)
DECLARE @AdminKullaniciId INT, @Ted007Id INT, @Siparis7Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted007Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED007';

EXEC sp_siparis_taslak_olustur @Ted007Id, @AdminKullaniciId, @Siparis7Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -12, GETDATE()), gonderilme_tarih = DATEADD(DAY, -11, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 2, GETDATE()), durum = 'GONDERILDI' WHERE siparis_id = @Siparis7Id;
EXEC sp_siparis_satir_ekle @Siparis7Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH009'), 9.0, 165.00;
EXEC sp_siparis_satir_ekle @Siparis7Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK006'), 7.0, 142.00;
EXEC sp_siparis_satir_ekle @Siparis7Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG005'), 10.0, 195.00;
EXEC sp_siparis_satir_ekle @Siparis7Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR006'), 16.0, 125.00;
EXEC sp_siparis_satir_ekle @Siparis7Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ001'), 20.0, 85.00;
GO

-- ORDER 8: GONDERILDI (3 of 6)
DECLARE @AdminKullaniciId INT, @Ted008Id INT, @Siparis8Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted008Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED008';

EXEC sp_siparis_taslak_olustur @Ted008Id, @AdminKullaniciId, @Siparis8Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -10, GETDATE()), gonderilme_tarih = DATEADD(DAY, -9, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 3, GETDATE()), durum = 'GONDERILDI' WHERE siparis_id = @Siparis8Id;
EXEC sp_siparis_satir_ekle @Siparis8Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH010'), 14.0, 155.00;
EXEC sp_siparis_satir_ekle @Siparis8Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY006'), 10.0, 112.00;
EXEC sp_siparis_satir_ekle @Siparis8Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TAK001'), 11.0, 265.00;
GO

-- ORDER 9: KISMI (2 of 5)
DECLARE @AdminKullaniciId INT, @Ted009Id INT, @Siparis9Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted009Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED009';

EXEC sp_siparis_taslak_olustur @Ted009Id, @AdminKullaniciId, @Siparis9Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -28, GETDATE()), gonderilme_tarih = DATEADD(DAY, -27, GETDATE()), teslim_tarih = DATEADD(DAY, -20, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -20, GETDATE()), durum = 'KISMI' WHERE siparis_id = @Siparis9Id;
EXEC sp_siparis_satir_ekle @Siparis9Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK007'), 6.0, 138.00;
EXEC sp_siparis_satir_ekle @Siparis9Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG006'), 8.0, 205.00;
EXEC sp_siparis_satir_ekle @Siparis9Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR007'), 12.0, 135.00;
EXEC sp_siparis_satir_ekle @Siparis9Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK005'), 55.0, 72.00;
UPDATE siparis_satir SET teslim_miktar = 4.0 WHERE siparis_id = @Siparis9Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK007');
UPDATE siparis_satir SET teslim_miktar = 8.0 WHERE siparis_id = @Siparis9Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG006');
UPDATE siparis_satir SET teslim_miktar = 6.0 WHERE siparis_id = @Siparis9Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR007');
UPDATE siparis_satir SET teslim_miktar = 0.0 WHERE siparis_id = @Siparis9Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK005');
GO

-- ORDER 10: TAMAMLANDI (2 of 4)
DECLARE @AdminKullaniciId INT, @Ted010Id INT, @Siparis10Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted010Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED010';

EXEC sp_siparis_taslak_olustur @Ted010Id, @AdminKullaniciId, @Siparis10Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -45, GETDATE()), gonderilme_tarih = DATEADD(DAY, -44, GETDATE()), teslim_tarih = DATEADD(DAY, -38, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -38, GETDATE()), durum = 'TAMAMLANDI' WHERE siparis_id = @Siparis10Id;
EXEC sp_siparis_satir_ekle @Siparis10Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH011'), 11.0, 152.00;
EXEC sp_siparis_satir_ekle @Siparis10Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY007'), 9.0, 108.00;
EXEC sp_siparis_satir_ekle @Siparis10Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR008'), 13.0, 130.00;
EXEC sp_siparis_satir_ekle @Siparis10Id, (SELECT urun_id FROM urun WHERE urun_kod = 'MAC004'), 14.0, 215.00;
EXEC sp_siparis_satir_ekle @Siparis10Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TOM002'), 12.0, 148.00;
UPDATE siparis_satir SET teslim_miktar = miktar WHERE siparis_id = @Siparis10Id;
GO

-- ORDERS 11-20 (continuing distribution pattern)
PRINT 'Sipariş 11-20: Devam...';

-- ORDER 11: TASLAK (3 of 4) - TED001
DECLARE @AdminKullaniciId INT, @Ted001Id INT, @Siparis11Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted001Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED001';

EXEC sp_siparis_taslak_olustur @Ted001Id, @AdminKullaniciId, @Siparis11Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -3, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 4, GETDATE()), durum = 'TASLAK' WHERE siparis_id = @Siparis11Id;
EXEC sp_siparis_satir_ekle @Siparis11Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK008'), 5.0, 132.00;
EXEC sp_siparis_satir_ekle @Siparis11Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG007'), 6.0, 198.00;
EXEC sp_siparis_satir_ekle @Siparis11Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK006'), 32.0, 79.00;
GO

-- ORDER 12: GONDERILDI (4 of 6) - TED002
DECLARE @AdminKullaniciId INT, @Ted002Id INT, @Siparis12Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted002Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED002';

EXEC sp_siparis_taslak_olustur @Ted002Id, @AdminKullaniciId, @Siparis12Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -8, GETDATE()), gonderilme_tarih = DATEADD(DAY, -7, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 4, GETDATE()), durum = 'GONDERILDI' WHERE siparis_id = @Siparis12Id;
EXEC sp_siparis_satir_ekle @Siparis12Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH012'), 10.0, 158.00;
EXEC sp_siparis_satir_ekle @Siparis12Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY008'), 8.0, 125.00;
EXEC sp_siparis_satir_ekle @Siparis12Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR009'), 11.0, 145.00;
EXEC sp_siparis_satir_ekle @Siparis12Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TAK002'), 9.0, 275.00;
GO

-- ORDER 13: GONDERILDI (5 of 6) - TED003
DECLARE @AdminKullaniciId INT, @Ted003Id INT, @Siparis13Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted003Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED003';

EXEC sp_siparis_taslak_olustur @Ted003Id, @AdminKullaniciId, @Siparis13Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -6, GETDATE()), gonderilme_tarih = DATEADD(DAY, -5, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 6, GETDATE()), durum = 'GONDERILDI' WHERE siparis_id = @Siparis13Id;
EXEC sp_siparis_satir_ekle @Siparis13Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK009'), 7.0, 140.00;
EXEC sp_siparis_satir_ekle @Siparis13Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG008'), 8.0, 188.00;
EXEC sp_siparis_satir_ekle @Siparis13Id, (SELECT urun_id FROM urun WHERE urun_kod = 'MAC005'), 10.0, 225.00;
GO

-- ORDER 14: KISMI (3 of 5) - TED004
DECLARE @AdminKullaniciId INT, @Ted004Id INT, @Siparis14Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted004Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED004';

EXEC sp_siparis_taslak_olustur @Ted004Id, @AdminKullaniciId, @Siparis14Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -22, GETDATE()), gonderilme_tarih = DATEADD(DAY, -21, GETDATE()), teslim_tarih = DATEADD(DAY, -14, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -14, GETDATE()), durum = 'KISMI' WHERE siparis_id = @Siparis14Id;
EXEC sp_siparis_satir_ekle @Siparis14Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY009'), 12.0, 122.00;
EXEC sp_siparis_satir_ekle @Siparis14Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR010'), 14.0, 138.00;
EXEC sp_siparis_satir_ekle @Siparis14Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK007'), 42.0, 85.00;
EXEC sp_siparis_satir_ekle @Siparis14Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAL002'), 10.0, 195.00;
UPDATE siparis_satir SET teslim_miktar = 12.0 WHERE siparis_id = @Siparis14Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'CAY009');
UPDATE siparis_satir SET teslim_miktar = 8.0 WHERE siparis_id = @Siparis14Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR010');
UPDATE siparis_satir SET teslim_miktar = 0.0 WHERE siparis_id = @Siparis14Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAK007');
UPDATE siparis_satir SET teslim_miktar = 10.0 WHERE siparis_id = @Siparis14Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL002');
GO

-- ORDER 15: KISMI (4 of 5) - TED005
DECLARE @AdminKullaniciId INT, @Ted005Id INT, @Siparis15Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted005Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED005';

EXEC sp_siparis_taslak_olustur @Ted005Id, @AdminKullaniciId, @Siparis15Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -18, GETDATE()), gonderilme_tarih = DATEADD(DAY, -17, GETDATE()), teslim_tarih = DATEADD(DAY, -10, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -10, GETDATE()), durum = 'KISMI' WHERE siparis_id = @Siparis15Id;
EXEC sp_siparis_satir_ekle @Siparis15Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK010'), 9.0, 143.00;
EXEC sp_siparis_satir_ekle @Siparis15Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG009'), 5.0, 245.00;
EXEC sp_siparis_satir_ekle @Siparis15Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TOM003'), 8.0, 152.00;
UPDATE siparis_satir SET teslim_miktar = 5.0 WHERE siparis_id = @Siparis15Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BTK010');
UPDATE siparis_satir SET teslim_miktar = 5.0 WHERE siparis_id = @Siparis15Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'YAG009');
UPDATE siparis_satir SET teslim_miktar = 0.0 WHERE siparis_id = @Siparis15Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'TOM003');
GO

-- ORDER 16: TAMAMLANDI (3 of 4) - TED006
DECLARE @AdminKullaniciId INT, @Ted006Id INT, @Siparis16Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted006Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED006';

EXEC sp_siparis_taslak_olustur @Ted006Id, @AdminKullaniciId, @Siparis16Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -50, GETDATE()), gonderilme_tarih = DATEADD(DAY, -49, GETDATE()), teslim_tarih = DATEADD(DAY, -43, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -43, GETDATE()), durum = 'TAMAMLANDI' WHERE siparis_id = @Siparis16Id;
EXEC sp_siparis_satir_ekle @Siparis16Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH013'), 8.0, 162.00;
EXEC sp_siparis_satir_ekle @Siparis16Id, (SELECT urun_id FROM urun WHERE urun_kod = 'CAY010'), 10.0, 116.00;
EXEC sp_siparis_satir_ekle @Siparis16Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR011'), 15.0, 142.00;
EXEC sp_siparis_satir_ekle @Siparis16Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK008'), 28.0, 95.00;
EXEC sp_siparis_satir_ekle @Siparis16Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KOZ002'), 18.0, 92.00;
UPDATE siparis_satir SET teslim_miktar = miktar WHERE siparis_id = @Siparis16Id;
GO

-- ORDER 17: GONDERILDI (6 of 6) - TED007
DECLARE @AdminKullaniciId INT, @Ted007Id INT, @Siparis17Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted007Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED007';

EXEC sp_siparis_taslak_olustur @Ted007Id, @AdminKullaniciId, @Siparis17Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -4, GETDATE()), gonderilme_tarih = DATEADD(DAY, -3, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, 8, GETDATE()), durum = 'GONDERILDI' WHERE siparis_id = @Siparis17Id;
EXEC sp_siparis_satir_ekle @Siparis17Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK011'), 6.0, 136.00;
EXEC sp_siparis_satir_ekle @Siparis17Id, (SELECT urun_id FROM urun WHERE urun_kod = 'YAG010'), 7.0, 238.00;
EXEC sp_siparis_satir_ekle @Siparis17Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TAK003'), 12.0, 258.00;
GO

-- ORDER 18: KISMI (5 of 5) - TED008
DECLARE @AdminKullaniciId INT, @Ted008Id INT, @Siparis18Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted008Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED008';

EXEC sp_siparis_taslak_olustur @Ted008Id, @AdminKullaniciId, @Siparis18Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -16, GETDATE()), gonderilme_tarih = DATEADD(DAY, -15, GETDATE()), teslim_tarih = DATEADD(DAY, -8, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -8, GETDATE()), durum = 'KISMI' WHERE siparis_id = @Siparis18Id;
EXEC sp_siparis_satir_ekle @Siparis18Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH014'), 7.0, 168.00;
EXEC sp_siparis_satir_ekle @Siparis18Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR012'), 16.0, 148.00;
EXEC sp_siparis_satir_ekle @Siparis18Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAL003'), 12.0, 188.00;
UPDATE siparis_satir SET teslim_miktar = 7.0 WHERE siparis_id = @Siparis18Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAH014');
UPDATE siparis_satir SET teslim_miktar = 10.0 WHERE siparis_id = @Siparis18Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'KUR012');
UPDATE siparis_satir SET teslim_miktar = 0.0 WHERE siparis_id = @Siparis18Id AND urun_id = (SELECT urun_id FROM urun WHERE urun_kod = 'BAL003');
GO

-- ORDER 19: TAMAMLANDI (4 of 4) - TED009
DECLARE @AdminKullaniciId INT, @Ted009Id INT, @Siparis19Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted009Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED009';

EXEC sp_siparis_taslak_olustur @Ted009Id, @AdminKullaniciId, @Siparis19Id OUTPUT;
UPDATE siparis SET siparis_tarih = DATEADD(DAY, -35, GETDATE()), gonderilme_tarih = DATEADD(DAY, -34, GETDATE()), teslim_tarih = DATEADD(DAY, -27, GETDATE()), beklenen_teslim_tarih = DATEADD(DAY, -27, GETDATE()), durum = 'TAMAMLANDI' WHERE siparis_id = @Siparis19Id;
EXEC sp_siparis_satir_ekle @Siparis19Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH015'), 9.0, 156.00;
EXEC sp_siparis_satir_ekle @Siparis19Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BTK012'), 8.0, 141.00;
EXEC sp_siparis_satir_ekle @Siparis19Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR013'), 10.0, 165.00;
EXEC sp_siparis_satir_ekle @Siparis19Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAK009'), 24.0, 92.00;
EXEC sp_siparis_satir_ekle @Siparis19Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TOM004'), 11.0, 150.00;
UPDATE siparis_satir SET teslim_miktar = miktar WHERE siparis_id = @Siparis19Id;
GO

-- ORDER 20: TASLAK (4 of 4) - TED010
DECLARE @AdminKullaniciId INT, @Ted010Id INT, @Siparis20Id INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;
SELECT @Ted010Id = tedarikci_id FROM tedarikci WHERE tedarikci_kod = 'TED010';

EXEC sp_siparis_taslak_olustur @Ted010Id, @AdminKullaniciId, @Siparis20Id OUTPUT;
UPDATE siparis SET siparis_tarih = GETDATE(), beklenen_teslim_tarih = DATEADD(DAY, 7, GETDATE()), durum = 'TASLAK' WHERE siparis_id = @Siparis20Id;
EXEC sp_siparis_satir_ekle @Siparis20Id, (SELECT urun_id FROM urun WHERE urun_kod = 'BAH016'), 12.0, 160.00;
EXEC sp_siparis_satir_ekle @Siparis20Id, (SELECT urun_id FROM urun WHERE urun_kod = 'KUR014'), 9.0, 155.00;
EXEC sp_siparis_satir_ekle @Siparis20Id, (SELECT urun_id FROM urun WHERE urun_kod = 'MAC006'), 7.0, 235.00;
EXEC sp_siparis_satir_ekle @Siparis20Id, (SELECT urun_id FROM urun WHERE urun_kod = 'TAK004'), 10.0, 268.00;
GO

PRINT '  ✓ Sipariş 6-20 oluşturuldu';
GO

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Orders Seed TAMAM';
PRINT '========================================';

DECLARE @SiparisCount INT, @SatirCount INT;
DECLARE @TaslakCount INT, @GonderildiCount INT, @KismiCount INT, @TamamlandiCount INT, @IptalCount INT;

SELECT @SiparisCount = COUNT(*) FROM siparis;
SELECT @SatirCount = COUNT(*) FROM siparis_satir;

SELECT @TaslakCount = COUNT(*) FROM siparis WHERE durum = 'TASLAK';
SELECT @GonderildiCount = COUNT(*) FROM siparis WHERE durum = 'GONDERILDI';
SELECT @KismiCount = COUNT(*) FROM siparis WHERE durum = 'KISMI';
SELECT @TamamlandiCount = COUNT(*) FROM siparis WHERE durum = 'TAMAMLANDI';
SELECT @IptalCount = COUNT(*) FROM siparis WHERE durum = 'IPTAL';

PRINT 'Toplam sipariş sayısı: ' + CAST(@SiparisCount AS VARCHAR);
PRINT 'Toplam sipariş satır sayısı: ' + CAST(@SatirCount AS VARCHAR);
PRINT '';
PRINT 'Durum dağılımı:';
PRINT '  - TASLAK: ' + CAST(@TaslakCount AS VARCHAR) + ' (' + CAST((@TaslakCount * 100.0 / @SiparisCount) AS VARCHAR(5)) + '%)';
PRINT '  - GONDERILDI: ' + CAST(@GonderildiCount AS VARCHAR) + ' (' + CAST((@GonderildiCount * 100.0 / @SiparisCount) AS VARCHAR(5)) + '%)';
PRINT '  - KISMI: ' + CAST(@KismiCount AS VARCHAR) + ' (' + CAST((@KismiCount * 100.0 / @SiparisCount) AS VARCHAR(5)) + '%)';
PRINT '  - TAMAMLANDI: ' + CAST(@TamamlandiCount AS VARCHAR) + ' (' + CAST((@TamamlandiCount * 100.0 / @SiparisCount) AS VARCHAR(5)) + '%)';
PRINT '  - IPTAL: ' + CAST(@IptalCount AS VARCHAR) + ' (' + CAST((@IptalCount * 100.0 / @SiparisCount) AS VARCHAR(5)) + '%)';
PRINT '';
PRINT '========================================';
GO
