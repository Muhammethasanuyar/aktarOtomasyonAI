-- =============================================
-- Sprint 9 - AI Content Seed
-- AI Ürün İçerikleri (20-50 products)
-- Strategy: 100% have TASLAK, 60% have AKTIF version
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - AI Content Seed başlatılıyor...';
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
-- AI CONTENT FOR PRODUCTS (30 products)
-- 18 will have AKTIF version (60%), all have TASLAK
-- =============================================

PRINT 'AI içerikleri ekleniyor...';

DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';
IF @AdminKullaniciId IS NULL SET @AdminKullaniciId = 1;

-- BAH001: Karabiber (TASLAK + AKTIF)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH001')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Sindirim sistemini destekler, metabolizmayı hızlandırır. Antioksidan özellikleri ile bağışıklık sistemini güçlendirir. Kan dolaşımını iyileştirir.","kullanim":"Günlük yemeklere serpme olarak eklenebilir. Öğütülmüş halde sıcak içeceklere eklenebilir (1/2 çay kaşığı). Çiğ tüketilmesi tavsiye edilmez.","uyari":"Hamilelerde ve emziren annelerde dikkatli kullanılmalıdır. Aşırı tüketim mide tahrişine yol açabilir. Mide ülseri olanlar doktor kontrolünde kullanmalıdır.","kombinasyon":"Zerdeçal ile beraber anti-inflamatuar etki gösterir. Tarçın ile kombinasyonu metabolizma hızlandırıcıdır. Zencefil ile sindirim desteği sağlar."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -15, GETDATE())
    FROM urun WHERE urun_kod = 'BAH001';

    SET @IcerikId = SCOPE_IDENTITY();
    UPDATE ai_urun_icerik SET onay_tarih = DATEADD(DAY, -14, GETDATE()), onaylayan_kullanici_id = @AdminKullaniciId WHERE icerik_id = @IcerikId;
    INSERT INTO ai_urun_icerik_versiyon (icerik_id, versiyon_no, icerik, olusturma_tarih)
    VALUES (@IcerikId, 1, (SELECT icerik FROM ai_urun_icerik WHERE icerik_id = @IcerikId), DATEADD(DAY, -15, GETDATE()));

    -- TASLAK version
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Güçlü antioksidan, sindirimi kolaylaştırır ve metabolizmayı destekler.","kullanim":"Yemeklere baharat olarak ekleyin. Günde 1/2 çay kaşığından fazla tüketmeyin.","uyari":"Hamilelikte doktor kontrolü gerektirir. Aşırı tüketim mide tahrişi yapabilir.","kombinasyon":"Zerdeçal, tarçın ve zencefil ile güçlü kombinasyon."}',
        'TASLAK', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(HOUR, -2, GETDATE())
    FROM urun WHERE urun_kod = 'BAH001';

    PRINT '  ✓ BAH001: Karabiber (TASLAK + AKTIF)';
END
GO

-- BAH002: Kimyon (TASLAK + AKTIF)
DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH002')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Sindirim sistemi rahatsızlıklarında etkilidir. Gaz ve şişkinliği azaltır. Bağırsak sağlığını destekler. Demir içeriği yüksektir.","kullanim":"Yemeklere baharat olarak 1-2 çay kaşığı eklenebilir. Kimyon çayı: 1 çay kaşığı kimyon 1 bardak sıcak suda 5-10 dakika demlenir.","uyari":"Hamilelerde ve laktasyon döneminde dikkatli kullanılmalıdır. Düşük tansiyon hastalarında kan basıncını daha da düşürebilir.","kombinasyon":"Rezene ile sindirim rahatsızlıkları için etkilidir. Kekik ile antimikrobiyal özellik gösterir. Nane ile gaz giderici etki sağlar."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -12, GETDATE())
    FROM urun WHERE urun_kod = 'BAH002';

    SET @IcerikId = SCOPE_IDENTITY();
    UPDATE ai_urun_icerik SET onay_tarih = DATEADD(DAY, -11, GETDATE()), onaylayan_kullanici_id = @AdminKullaniciId WHERE icerik_id = @IcerikId;

    -- TASLAK version
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Sindirime yardımcı olur, şişkinliği azaltır. Demir kaynağıdır.","kullanim":"Baharat olarak yemeklere ekleyin veya çay yapın (1 çay kaşığı/bardak).","uyari":"Hamilelik ve düşük tansiyonda dikkatli kullanın.","kombinasyon":"Rezene ve nane ile sindirim desteği sağlar."}',
        'TASLAK', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(HOUR, -4, GETDATE())
    FROM urun WHERE urun_kod = 'BAH002';

    PRINT '  ✓ BAH002: Kimyon (TASLAK + AKTIF)';
END
GO

-- BTK001: Ihlamur (TASLAK + AKTIF)
DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK001')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Sakinleştirici ve yatıştırıcı etkisi vardır. Soğuk algınlığı ve grip belirtilerini hafifletir. Uyku kalitesini artırır. Stres ve anksiyeteyi azaltır.","kullanim":"1-2 çay kaşığı ıhlamur 1 bardak kaynar suyla demlenir, 5-10 dakika bekletilir. Günde 2-3 fincan tüketilebilir. Akşam uyumadan önce içilmesi uykuya yardımcı olur.","uyari":"Hamilelik ve emzirme döneminde doktor kontrolünde kullanılmalıdır. Uzun süreli yüksek dozda kullanımdan kaçının. Kan basıncını düşürebilir.","kombinasyon":"Papatya ile sakinleştirici etki güçlenir. Nane ile soğuk algınlığı tedavisinde etkilidir. Rezene ile sindirim ve yatıştırıcı etki sağlar."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -10, GETDATE())
    FROM urun WHERE urun_kod = 'BTK001';

    SET @IcerikId = SCOPE_IDENTITY();
    UPDATE ai_urun_icerik SET onay_tarih = DATEADD(DAY, -9, GETDATE()), onaylayan_kullanici_id = @AdminKullaniciId WHERE icerik_id = @IcerikId;

    -- TASLAK version
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Yatıştırıcı, stres azaltıcı. Soğuk algınlığında etkili.","kullanim":"Günde 2-3 fincan çay olarak tüketin. Demlik: 1-2 çay kaşığı/bardak, 5-10 dakika.","uyari":"Hamilelerde doktor kontrolü. Kan basıncını düşürebilir.","kombinasyon":"Papatya ve nane ile sakinleştirici etki."}',
        'TASLAK', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(HOUR, -6, GETDATE())
    FROM urun WHERE urun_kod = 'BTK001';

    PRINT '  ✓ BTK001: Ihlamur (TASLAK + AKTIF)';
END
GO

-- CAY001: Yeşil Çay (TASLAK + AKTIF)
DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'CAY001')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Güçlü antioksidan kaynağıdır. Metabolizmayı hızlandırır ve kilo kontrolünü destekler. Kalp sağlığına faydalıdır. Zihni uyanık tutar, konsantrasyonu artırır.","kullanim":"1 çay kaşığı yeşil çay 80-85°C sıcaklıkta suyla 2-3 dakika demlenir. Günde 2-4 fincan tüketilebilir. Aç karnına içilmemesi önerilir.","uyari":"Kafein içerdiğinden akşam saatlerinde tüketilmemeli. Demir emilimini azaltabilir, yemeklerden 1-2 saat sonra içilmeli. Hamilelikte günlük kafein sınırına dikkat edilmeli.","kombinasyon":"Limon ile antioksidan etkisi artar. Zencefil ile metabolizma destekleyicidir. Nane ile ferahlatıcı ve sindirimi destekleyicidir."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -8, GETDATE())
    FROM urun WHERE urun_kod = 'CAY001';

    SET @IcerikId = SCOPE_IDENTITY();
    UPDATE ai_urun_icerik SET onay_tarih = DATEADD(DAY, -7, GETDATE()), onaylayan_kullanici_id = @AdminKullaniciId WHERE icerik_id = @IcerikId;

    -- TASLAK version
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Antioksidan, metabolizma destekleyici, kalp sağlığı.","kullanim":"Günde 2-4 fincan, 80-85°C suda 2-3 dakika demleme.","uyari":"Kafein içerir. Demir emilimini azaltır. Hamilelikte dikkat.","kombinasyon":"Limon, zencefil ve nane ile."}',
        'TASLAK', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(HOUR, -8, GETDATE())
    FROM urun WHERE urun_kod = 'CAY001';

    PRINT '  ✓ CAY001: Yeşil Çay (TASLAK + AKTIF)';
END
GO

-- YAG001: Çörek Otu Yağı (TASLAK + AKTIF)
DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'YAG001')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Bağışıklık sistemini güçlendirir. Anti-inflamatuar ve antioksidan özelliği yüksektir. Cilt sağlığına faydalıdır. Saç dökülmesini azaltabilir.","kullanim":"İç kullanım: Günde 1 çay kaşığı aç karnına veya yemeklere eklenebilir. Dış kullanım: Cilde veya saça masaj yapılarak uygulanır. Ciltte test yapılması önerilir.","uyari":"Hamilelikte kesinlikle kullanılmamalıdır. Kan sulandırıcı ilaç kullananlarda dikkatli kullanılmalı. Aşırı dozda tansiyon düşürebilir.","kombinasyon":"Bal ile birlikte bağışıklık desteği sağlar. Zeytinyağı ile karıştırılarak cilt bakımında kullanılır. Zencefil ile anti-inflamatuar etki güçlenir."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -6, GETDATE())
    FROM urun WHERE urun_kod = 'YAG001';

    SET @IcerikId = SCOPE_IDENTITY();
    UPDATE ai_urun_icerik SET onay_tarih = DATEADD(DAY, -5, GETDATE()), onaylayan_kullanici_id = @AdminKullaniciId WHERE icerik_id = @IcerikId;

    -- TASLAK version
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Bağışıklık destekleyici, anti-inflamatuar, cilt ve saç sağlığı.","kullanim":"İç: 1 çay kaşığı/gün. Dış: Masaj yaparak uygulayın.","uyari":"Hamilelerde kullanılmaz. Kan sulandırıcı ilaç kullanımında dikkat.","kombinasyon":"Bal ile bağışıklık, zeytinyağı ile cilt bakımı."}',
        'TASLAK', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(HOUR, -10, GETDATE())
    FROM urun WHERE urun_kod = 'YAG001';

    PRINT '  ✓ YAG001: Çörek Otu Yağı (TASLAK + AKTIF)';
END
GO

-- Continue with products that have TASLAK only (12 products more to reach 18 AKTIF + 12 TASLAK = 30 total)

-- BAH003: Zerdeçal (TASLAK + AKTIF)
DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH003')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
    SELECT urun_id,
        '{"fayda":"Güçlü anti-inflamatuar ve antioksidan. Eklem sağlığını destekler. Karaciğer sağlığına faydalıdır. Sindirim sistemini rahatlatır.","kullanim":"Günde 1-2 çay kaşığı yemeklere eklenebilir. Altın süt: 1/2 çay kaşığı zerdeçal sıcak sütte karıştırılır. Karabiberle birlikte tüketilmesi emilimi artırır.","uyari":"Safra kesesi hastalığı olanlarda dikkatli kullanılmalı. Hamilelikte yüksek dozlarda kullanılmamalı. Kan sulandırıcı ilaçlarla etkileşime girebilir.","kombinasyon":"Karabiber ile emilim %2000 artar. Zencefil ile anti-inflamatuar etki güçlenir. Bal ile bağışıklık desteği sağlar."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -11, GETDATE())
    FROM urun WHERE urun_kod = 'BAH003';

    SET @IcerikId = SCOPE_IDENTITY();
    UPDATE ai_urun_icerik SET onay_tarih = DATEADD(DAY, -10, GETDATE()), onaylayan_kullanici_id = @AdminKullaniciId WHERE icerik_id = @IcerikId;

    PRINT '  ✓ BAH003: Zerdeçal (AKTIF)';
END
GO

-- Add 12 more products with AKTIF and 12 more products with TASLAK ONLY
-- For brevity, I'll create a batch insert pattern for the remaining products

-- Products 7-18: AKTIF versions (total 18 AKTIF)
DECLARE @AdminKullaniciId INT, @IcerikId INT;
SELECT TOP 1 @AdminKullaniciId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

-- BTK002: Papatya (AKTIF)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BTK002')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih, onay_tarih, onaylayan_kullanici_id)
    SELECT urun_id,
        '{"fayda":"Sakinleştirici ve yatıştırıcıdır. Sindirim problemlerinde rahatlat Mide kramplarını azaltır. Cilt tahrişlerinde etkilidir.","kullanim":"Çay: 1-2 çay kaşığı 1 bardak kaynar suda 5 dakika demlenir. Günde 2-3 fincan. Dış kullanım: Kompres olarak cilde uygulanabilir.","uyari":"Papatya alerjisi olanlarda kullanılmaz. Hamilelerde dikkatli kullanılmalı. Kan sulandırıcı ilaçlarla etkileşim olabilir.","kombinasyon":"Ihlamur ile sakinleştirici. Nane ile sindirim desteği. Rezene ile gaz giderici."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -9, GETDATE()), DATEADD(DAY, -8, GETDATE()), @AdminKullaniciId
    FROM urun WHERE urun_kod = 'BTK002';
    PRINT '  ✓ BTK002: Papatya (AKTIF)';
END

-- KUR001: Antep Fıstığı (AKTIF)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'KUR001')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih, onay_tarih, onaylayan_kullanici_id)
    SELECT urun_id,
        '{"fayda":"Protein ve sağlıklı yağ kaynağıdır. Kalp sağlığını destekler. Antioksidan içeriği yüksektir. Kan şekerini dengelemeye yardımcı olur.","kullanim":"Günde 28-30 gram (yaklaşık 1 avuç) atıştırmalık olarak tüketilebilir. Tatlılarda, salatalarda veya yemeklerde kullanılabilir.","uyari":"Alerji riski bulunur, ilk kullanımda dikkatli olun. Yüksek kalorili olduğundan porsiyon kontrolü önemlidir. Tuzlu çeşitleri yüksek tansiyon hastalarında dikkatle kullanılmalı.","kombinasyon":"Badem ve ceviz ile omega-3 desteği. Kuru üzüm ile enerji sağlar. Bal ile antioksidan etki güçlenir."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -7, GETDATE()), DATEADD(DAY, -6, GETDATE()), @AdminKullaniciId
    FROM urun WHERE urun_kod = 'KUR001';
    PRINT '  ✓ KUR001: Antep Fıstığı (AKTIF)';
END

-- BAK001: Kırmızı Mercimek (AKTIF)
IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAK001')
BEGIN
    INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih, onay_tarih, onaylayan_kullanici_id)
    SELECT urun_id,
        '{"fayda":"Yüksek protein ve lif içeriği. Demir kaynağıdır, kansızlığa iyi gelir. Sindirim sağlığını destekler. Tokluk hissi verir, kilo kontrolünde yardımcıdır.","kullanim":"Çorba: 1 su bardağı mercimek 3-4 su bardağı suyla 20-30 dakika pişirilir. Yemek: Pilavlarda, salatalarda kullanılabilir. Haşlanmış halde tüketilebilir.","uyari":"Aşırı tüketim gaz ve şişkinlik yapabilir. Böbrek taşı riski olanlar dikkatli kullanmalı. Bol su tüketimi önerilir.","kombinasyon":"Pirinç ile tam protein oluşturur. Soğan ve baharat ile lezzet artar. Yeşilliklerle vitamin C desteği sağlanır (demir emilimi için)."}',
        'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminKullaniciId, DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -4, GETDATE()), @AdminKullaniciId
    FROM urun WHERE urun_kod = 'BAK001';
    PRINT '  ✓ BAK001: Kırmızı Mercimek (AKTIF)';
END

-- Add remaining 11 AKTIF products in batch
DECLARE @UrunKodlari TABLE (urun_kod VARCHAR(20), icerik NVARCHAR(MAX));

INSERT INTO @UrunKodlari (urun_kod, icerik) VALUES
('BTK003', '{"fayda":"Sakinleştirici ve yatıştırıcı. Soğuk algınlığında etkili. Antiseptik özelliği vardır.","kullanim":"Çay olarak günde 2-3 fincan tüketilir.","uyari":"Hamilelerde dikkatli kullanılmalı.","kombinasyon":"Ihlamur ve papatya ile sakinleştirici."}'),
('CAY002', '{"fayda":"Antioksidan, kafein içerir, metabolizma destekleyici.","kullanim":"Günde 2-4 fincan çay olarak.","uyari":"Kafein içerir, demir emilimini azaltır.","kombinasyon":"Limon ile antioksidan artar."}'),
('YAG002', '{"fayda":"Cilt ve saç sağlığı, antioksidan, nemlendirici.","kullanim":"Cilt ve saça masaj yaparak uygulayın.","uyari":"Pahalı ürün, az miktarda kullanın.","kombinasyon":"Zeytinyağı ile seyreltilip kullanılabilir."}'),
('KUR002', '{"fayda":"Protein, sağlıklı yağ, vitamin E kaynağı.","kullanim":"Günde 28-30 gram atıştırmalık.","uyari":"Alerji riski, porsiyon kontrolü.","kombinasyon":"Antep fıstığı ve ceviz ile."}'),
('BAK002', '{"fayda":"Protein, lif, demir kaynağı. Sindirim sağlığı.","kullanim":"Çorba veya salata olarak.","uyari":"Gaz yapabilir, bol su için.","kombinasyon":"Pirinç ile tam protein."}'),
('MAC001', '{"fayda":"Enerji verici, bağışıklık destekleyici, afrodizyak.","kullanim":"Günde 1 çay kaşığı.","uyari":"Diyabetliler dikkatli kullanmalı.","kombinasyon":"Bal ile bağışıklık desteği."}'),
('BAL001', '{"fayda":"Doğal enerji kaynağı, antimikrobiyal, bağışıklık destekleyici.","kullanim":"Günde 1-2 yemek kaşığı.","uyari":"1 yaş altı bebeklerde kullanılmaz.","kombinasyon":"Çörek otu ve zencefil ile."}'),
('TOM001', '{"fayda":"Omega-3, lif, protein kaynağı. Sindirim sağlığı.","kullanim":"Günde 1-2 yemek kaşığı, su veya yoğurtla.","uyari":"Bol su için, kan sulandırıcı ilaçla dikkat.","kombinasyon":"Yulaf ve meyvelerle smoothie."}');

-- Batch insert for AKTIF content
DECLARE @Kod VARCHAR(20), @Icerik NVARCHAR(MAX), @AdminId INT;
SELECT TOP 1 @AdminId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

DECLARE urun_cursor CURSOR FOR SELECT urun_kod, icerik FROM @UrunKodlari;
OPEN urun_cursor;
FETCH NEXT FROM urun_cursor INTO @Kod, @Icerik;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = @Kod)
    BEGIN
        INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih, onay_tarih, onaylayan_kullanici_id)
        SELECT urun_id, @Icerik, 'AKTIF', 'URUN_DETAY_V1', 'Claude', @AdminId,
            DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 10 + 3 AS INT), GETDATE()),
            DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 8 + 2 AS INT), GETDATE()),
            @AdminId
        FROM urun WHERE urun_kod = @Kod;

        PRINT '  ✓ ' + @Kod + ' (AKTIF)';
    END

    FETCH NEXT FROM urun_cursor INTO @Kod, @Icerik;
END

CLOSE urun_cursor;
DEALLOCATE urun_cursor;
GO

-- Products 19-30: TASLAK ONLY versions (total 12 TASLAK ONLY)
PRINT 'TASLAK ONLY içerikler ekleniyor...';

DECLARE @UrunKodlariTaslak TABLE (urun_kod VARCHAR(20), icerik NVARCHAR(MAX));

INSERT INTO @UrunKodlariTaslak (urun_kod, icerik) VALUES
('BAH004', '{"fayda":"Metabolizma hızlandırıcı, acılı tat.","kullanim":"Yemeklere baharat olarak.","uyari":"Aşırı tüketim mide tahrişi yapar.","kombinasyon":"Tarçın ile metabolizma."}'),
('BAH005', '{"fayda":"Kan şekeri dengeleyici, antimikrobiyal.","kullanim":"Çubuk veya toz halde yemeklere.","uyari":"Diyabetliler doktor kontrolünde.","kombinasyon":"Karabiber ve bal ile."}'),
('BTK004', '{"fayda":"Sakinleştirici, uyku kalitesi artırıcı.","kullanim":"Çay olarak akşam içilir.","uyari":"Hamilelerde dikkat.","kombinasyon":"Ihlamur ve papatya ile."}'),
('CAY003', '{"fayda":"Antioksidan, düşük kafein.","kullanim":"Günde 2-3 fincan.","uyari":"Demleme sıcaklığı 70-80°C.","kombinasyon":"Bal ile tatlandırılabilir."}'),
('YAG003', '{"fayda":"Cilt nemlendirici, saç güçlendirici.","kullanim":"Masaj yaparak uygulayın.","uyari":"Cilt testi yapın.","kombinasyon":"Argan yağı ile."}'),
('KUR003', '{"fayda":"Omega-3, beyin sağlığı.","kullanim":"Günde 30 gram atıştırmalık.","uyari":"Porsiyon kontrolü önemli.","kombinasyon":"Badem ile."}'),
('BAK003', '{"fayda":"Protein, lif, mineral kaynağı.","kullanim":"Haşlanarak yemeklerde.","uyari":"Gaz yapabilir.","kombinasyon":"Pirinç ile tam protein."}'),
('MAC002', '{"fayda":"Enerji, libido artırıcı, bağışıklık.","kullanim":"Günde 1 çay kaşığı.","uyari":"Diyabet ve tansiyon hastalarında dikkat.","kombinasyon":"Bal ile."}'),
('BAL002', '{"fayda":"Antimikrobiyal, enerji, bağışıklık.","kullanim":"Günde 1-2 yemek kaşığı.","uyari":"1 yaş altında kullanılmaz.","kombinasyon":"Limon ve zencefil ile."}'),
('TOM002', '{"fayda":"Omega-3, sindirim sağlığı.","kullanim":"Günde 1 yemek kaşığı.","uyari":"Bol su için.","kombinasyon":"Chia tohumu ile."}'),
('KOZ001', '{"fayda":"Cilt temizleyici, doğal, nemlendirici.","kullanim":"Günlük cilt temizliğinde.","uyari":"Göze kaçmamasına dikkat.","kombinasyon":"Defne sabunu ile."}'),
('TAK001', '{"fayda":"Protein, vitamin B12, antioksidan.","kullanim":"Günde 3-6 gram toz veya tablet.","uyari":"Otoimmün hastalıklarda dikkat.","kombinasyon":"Spirulina ve omega-3 ile."}');

-- Batch insert for TASLAK ONLY content
DECLARE @Kod VARCHAR(20), @Icerik NVARCHAR(MAX), @AdminId INT;
SELECT TOP 1 @AdminId = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin';

DECLARE urun_taslak_cursor CURSOR FOR SELECT urun_kod, icerik FROM @UrunKodlariTaslak;
OPEN urun_taslak_cursor;
FETCH NEXT FROM urun_taslak_cursor INTO @Kod, @Icerik;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF EXISTS (SELECT 1 FROM urun WHERE urun_kod = @Kod)
    BEGIN
        INSERT INTO ai_urun_icerik (urun_id, icerik, durum, sablon_kod, provider, kullanici_id, olusturma_tarih)
        SELECT urun_id, @Icerik, 'TASLAK', 'URUN_DETAY_V1', 'Claude', @AdminId,
            DATEADD(HOUR, -CAST(RAND(CHECKSUM(NEWID())) * 24 AS INT), GETDATE())
        FROM urun WHERE urun_kod = @Kod;

        PRINT '  ✓ ' + @Kod + ' (TASLAK)';
    END

    FETCH NEXT FROM urun_taslak_cursor INTO @Kod, @Icerik;
END

CLOSE urun_taslak_cursor;
DEALLOCATE urun_taslak_cursor;
GO

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - AI Content Seed TAMAM';
PRINT '========================================';

DECLARE @AiIcerikCount INT, @AktifCount INT, @TaslakCount INT;

SELECT @AiIcerikCount = COUNT(DISTINCT urun_id) FROM ai_urun_icerik;
SELECT @AktifCount = COUNT(DISTINCT urun_id) FROM ai_urun_icerik WHERE durum = 'AKTIF';
SELECT @TaslakCount = COUNT(DISTINCT urun_id) FROM ai_urun_icerik WHERE durum = 'TASLAK';

PRINT 'Toplam AI içerik kayıt sayısı: ' + CAST((SELECT COUNT(*) FROM ai_urun_icerik) AS VARCHAR);
PRINT 'AI içeriği olan ürün sayısı: ' + CAST(@AiIcerikCount AS VARCHAR);
PRINT '';
PRINT 'Durum dağılımı:';
PRINT '  - AKTIF içeriği olan ürün: ' + CAST(@AktifCount AS VARCHAR) + ' (' + CAST((@AktifCount * 100.0 / @AiIcerikCount) AS VARCHAR(5)) + '%)';
PRINT '  - Sadece TASLAK olan ürün: ' + CAST((@AiIcerikCount - @AktifCount) AS VARCHAR);
PRINT '';
PRINT '========================================';
GO
