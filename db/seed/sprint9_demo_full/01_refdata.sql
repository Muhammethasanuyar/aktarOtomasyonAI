-- =============================================
-- Sprint 9 - Reference Data Seed
-- Kategoriler, Birimler, Tedarikçiler
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Reference Data Seed başlatılıyor...';
GO

-- =============================================
-- KATEGORILER (12-25 kategori)
-- =============================================

PRINT 'Kategoriler ekleniyor...';

-- Ana Kategoriler
IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'BITKI')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('BITKI', 'Bitkisel Ürünler', 1);
    PRINT '  + BITKI kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'CAY')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('CAY', 'Çay ve İçecekler', 1);
    PRINT '  + CAY kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'YAG')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('YAG', 'Doğal Yağlar', 1);
    PRINT '  + YAG kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'MACUN')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('MACUN', 'Macun ve Karışımlar', 1);
    PRINT '  + MACUN kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'TOHUM')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('TOHUM', 'Tohum ve Taneler', 1);
    PRINT '  + TOHUM kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'KOZMETIK')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('KOZMETIK', 'Doğal Kozmetik', 1);
    PRINT '  + KOZMETIK kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'TAKVIYE')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('TAKVIYE', 'Takviye Ürünleri', 1);
    PRINT '  + TAKVIYE kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'BAL')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('BAL', 'Bal ve Arı Ürünleri', 1);
    PRINT '  + BAL kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'KREM')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('KREM', 'Krem ve Merhem', 1);
    PRINT '  + KREM kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'TATLI')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('TATLI', 'Helva ve Tatlılar', 1);
    PRINT '  + TATLI kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'PEKMEZ')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('PEKMEZ', 'Pekmez ve Şuruplar', 1);
    PRINT '  + PEKMEZ kategorisi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM urun_kategori WHERE kategori_kod = 'SIVI')
BEGIN
    INSERT INTO urun_kategori (kategori_kod, kategori_adi, aktif)
    VALUES ('SIVI', 'Sıvı Takviyeler', 1);
    PRINT '  + SIVI kategorisi eklendi';
END

-- Birimler zaten seed edilmiş (001_seed_masterdata.sql)
-- KG, GR, ADET, ML, LT, PKT, KTU

PRINT 'Kategoriler tamam.';
GO

-- =============================================
-- TEDARİKÇİLER (10-20 tedarikçi)
-- =============================================

PRINT 'Tedarikçiler ekleniyor...';

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED001')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED001', 'Anadolu Aktarları A.Ş.', 'Mehmet Yılmaz', '0212 555 0101', 'info@anadoluaktar.com', 'Bahçelievler Mah. No:45, İstanbul', 1);
    PRINT '  + TED001 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED002')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED002', 'Doğal Ürünler Ltd.', 'Ayşe Kaya', '0312 555 0202', 'iletisim@dogalurunler.com', 'Çankaya Cad. No:78, Ankara', 1);
    PRINT '  + TED002 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED003')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED003', 'Şifa Baharat San.', 'Fatma Demir', '0232 555 0303', 'satis@sifabaharat.com', 'Konak Mah. No:12, İzmir', 1);
    PRINT '  + TED003 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED004')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED004', 'Yedigün Bitkisel A.Ş.', 'Ali Öztürk', '0224 555 0404', 'bilgi@yedigun.com', 'Osmangazi Bulvarı No:90, Bursa', 1);
    PRINT '  + TED004 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED005')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED005', 'Antalya Aktarları', 'Zeynep Çelik', '0242 555 0505', 'siparis@antalyaaktar.com', 'Muratpaşa No:34, Antalya', 1);
    PRINT '  + TED005 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED006')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED006', 'Karadeniz Bitki Evi', 'Hasan Şahin', '0462 555 0606', 'info@karadenizbitki.com', 'Ortahisar Cad. No:67, Trabzon', 1);
    PRINT '  + TED006 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED007')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED007', 'Ege Baharat Ltd.', 'Elif Arslan', '0236 555 0707', 'satis@egebaharat.com', 'Merkez Mah. No:23, Manisa', 1);
    PRINT '  + TED007 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED008')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED008', 'Marmara Doğal A.Ş.', 'Ahmet Polat', '0262 555 0808', 'bilgi@marmaradogal.com', 'İzmit Bulvarı No:56, Kocaeli', 1);
    PRINT '  + TED008 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED009')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED009', 'GAP Bitkisel Ürünler', 'Murat Yıldız', '0414 555 0909', 'iletisim@gapbitkisel.com', 'Haliliye Cad. No:89, Şanlıurfa', 1);
    PRINT '  + TED009 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED010')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED010', 'Akdeniz Aktarları', 'Selin Aydın', '0322 555 1010', 'info@akdenizaktar.com', 'Seyhan Mah. No:45, Adana', 1);
    PRINT '  + TED010 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED011')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED011', 'Konya Baharat Pazarı', 'Recep Kılıç', '0332 555 1111', 'siparis@konyabaharat.com', 'Meram Cad. No:78, Konya', 1);
    PRINT '  + TED011 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED012')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED012', 'Kayseri Organik Ltd.', 'Gülsüm Erdoğan', '0352 555 1212', 'bilgi@kayseriorganik.com', 'Melikgazi No:34, Kayseri', 1);
    PRINT '  + TED012 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED013')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED013', 'Gaziantep Kuruyemiş A.Ş.', 'Kemal Acar', '0342 555 1313', 'satis@gaziantepkuru.com', 'Şahinbey Mah. No:90, Gaziantep', 1);
    PRINT '  + TED013 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED014')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED014', 'Diyarbakır Bitki Deposu', 'Leyla Çetin', '0412 555 1414', 'info@diyarbakirbitki.com', 'Bağlar Cad. No:123, Diyarbakır', 1);
    PRINT '  + TED014 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED015')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED015', 'Samsun Doğal Gıda', 'Can Yıldırım', '0362 555 1515', 'iletisim@samsundogal.com', 'Atakum No:56, Samsun', 1);
    PRINT '  + TED015 eklendi';
END

-- Bitkisel Ürün Tedarikçileri (TED016-TED025)
IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED016')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED016', 'Doğal Bitki Pazarı Ltd.', 'Ayşe Yılmaz', '0532 444 5678', 'info@dogalbitkipazari.com', 'Atatürk Cad. No:45 Kadıköy, İstanbul', 1);
    PRINT '  + TED016 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED017')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED017', 'Şifalı Bitkiler A.Ş.', 'Mehmet Kaya', '0543 555 6789', 'satis@sifalibitkiler.com', 'Cumhuriyet Mah. 123. Sok. Çankaya, Ankara', 1);
    PRINT '  + TED017 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED018')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED018', 'Anadolu Şifalı Otlar', 'Fatma Özkan', '0216 777 8899', 'bilgi@anadoluotlar.com', 'Bağdat Cad. No:234 Maltepe, İstanbul', 1);
    PRINT '  + TED018 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED019')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED019', 'Yeşil Vadi Bitkileri', 'Emre Çelik', '0242 999 1122', 'siparis@yesilvadi.com', 'Lara Mah. Deniz Sok. No:78 Antalya', 1);
    PRINT '  + TED019 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED020')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED020', 'Toros Dağ Bitkileri Ltd.', 'Zeynep Şahin', '0324 444 3344', 'iletisim@torosdagbitkileri.com', 'Toros Bulvarı No:56 Mersin', 1);
    PRINT '  + TED020 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED021')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED021', 'Kapadokya Organik Bitkiler', 'Hakan Demir', '0384 777 5566', 'info@kapadokyaorganik.com', 'Göreme Mah. No:12 Nevşehir', 1);
    PRINT '  + TED021 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED022')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED022', 'Fırat Doğal Bitkiler A.Ş.', 'Aylin Yıldız', '0424 888 7788', 'satis@firatdogal.com', 'Merkez Cad. No:90 Elazığ', 1);
    PRINT '  + TED022 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED023')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED023', 'Amasya Bahçe Bitkileri', 'Selim Arslan', '0358 666 9900', 'bilgi@amasyabahce.com', 'Bahçeler Sok. No:34 Amasya', 1);
    PRINT '  + TED023 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED024')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED024', 'Uludağ Şifalı Otlar Ltd.', 'Deniz Polat', '0224 555 1122', 'info@uludagotlar.com', 'Yıldırım Bulvarı No:67 Bursa', 1);
    PRINT '  + TED024 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED025')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED025', 'Kaz Dağı Doğal Ürünler', 'Canan Aydın', '0266 444 3355', 'siparis@kazdagi.com', 'Edremit Cad. No:123 Balıkesir', 1);
    PRINT '  + TED025 eklendi';
END

-- Gıda Toptancıları (TED026-TED033)
IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED026')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED026', 'Mega Gıda Toptancısı A.Ş.', 'Burak Kılıç', '0212 333 4455', 'satis@megagida.com', 'Topkapı Sanayi Sitesi No:45 İstanbul', 1);
    PRINT '  + TED026 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED027')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED027', 'Ankara Hububat Pazarı Ltd.', 'Gül Erdoğan', '0312 777 8899', 'bilgi@ankarahububat.com', 'Ticaret Merkezi No:234 Ankara', 1);
    PRINT '  + TED027 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED028')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED028', 'İzmir Kuruyemiş Hali', 'Cem Öztürk', '0232 666 7788', 'info@izmirkuruyemis.com', 'Kemeraltı Çarşısı No:78 İzmir', 1);
    PRINT '  + TED028 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED029')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED029', 'Bakliyat Dünyası Toptancı', 'Seda Yalçın', '0242 555 6677', 'siparis@bakliyatdunyasi.com', 'Hal Kompleksi No:12 Antalya', 1);
    PRINT '  + TED029 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED030')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED030', 'Adana Baharat Hali A.Ş.', 'Oğuz Çetin', '0322 888 9900', 'iletisim@adanabaharat.com', 'Ceyhan Cad. No:90 Adana', 1);
    PRINT '  + TED030 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED031')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED031', 'Konya Gıda Deposu Ltd.', 'Elif Şimşek', '0332 444 5566', 'bilgi@konyagida.com', 'Sanayi Sitesi No:567 Konya', 1);
    PRINT '  + TED031 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED032')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED032', 'Gaziantep Kuruyemiş Toptan', 'Mert Acar', '0342 777 8888', 'satis@gkuruyemis.com', 'Organize Sanayi No:234 Gaziantep', 1);
    PRINT '  + TED032 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED033')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED033', 'Karadeniz Fındık ve Baharat', 'Ayça Kara', '0462 999 1010', 'info@karadenizfindik.com', 'Liman Cad. No:45 Trabzon', 1);
    PRINT '  + TED033 eklendi';
END

-- İthalatçılar (TED034-TED040)
IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED034')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED034', 'Global Spice İthalat A.Ş.', 'Can Yurt', '0212 555 7777', 'import@globalspice.com', 'Atatürk Havalimanı Yakını No:100 İstanbul', 1);
    PRINT '  + TED034 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED035')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED035', 'Asya Baharat İthalat Ltd.', 'Leyla Aksoy', '0216 888 6666', 'bilgi@asyabaharat.com', 'Kartal Limanı No:23 İstanbul', 1);
    PRINT '  + TED035 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED036')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED036', 'Hint Baharatları İthalat', 'Serkan Güneş', '0212 777 5555', 'satis@hintbaharati.com', 'Zeytinburnu Dış Ticaret No:67 İstanbul', 1);
    PRINT '  + TED036 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED037')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED037', 'Uzak Doğu Bitkileri A.Ş.', 'Pınar Yıldırım', '0216 666 4444', 'info@uzakdogubitki.com', 'Tuzla Serbest Bölge No:45 İstanbul', 1);
    PRINT '  + TED037 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED038')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED038', 'Avrupa Organik İthalat Ltd.', 'Kerem Çağlar', '0242 444 2222', 'siparis@avrupaorganik.com', 'Serbest Bölge No:890 Antalya', 1);
    PRINT '  + TED038 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED039')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED039', 'Latin Amerika Baharat', 'Didem Aktaş', '0232 555 3333', 'iletisim@latinbaharat.com', 'Alsancak Liman No:123 İzmir', 1);
    PRINT '  + TED039 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED040')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED040', 'Afrika Baharatları İthalat', 'Yasemin Kocabaş', '0312 444 1111', 'bilgi@afrikabaharat.com', 'Dış Ticaret Merkezi No:456 Ankara', 1);
    PRINT '  + TED040 eklendi';
END

-- Yerel Üreticiler (TED041-TED045)
IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED041')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED041', 'Köy Kooperatifi Ürünleri', 'Hülya Sarı', '0258 777 8888', 'koop@koyurunleri.com', 'Çamlık Köyü No:1 Denizli', 1);
    PRINT '  + TED041 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED042')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED042', 'Organik Çiftlik Ürünleri Ltd.', 'Tarık Başaran', '0274 666 7777', 'info@organikciftlik.com', 'Ekinler Köyü Bilecik', 1);
    PRINT '  + TED042 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED043')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED043', 'Çamlıbel Köy Pazarı', 'Nurcan Taş', '0376 555 6666', 'satis@camlibelpazar.com', 'Çamlıbel Mah. Kastamonu', 1);
    PRINT '  + TED043 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED044')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED044', 'Yaylalar Organik Üretim', 'Orhan Tekin', '0366 444 5555', 'bilgi@yaylalarorganik.com', 'Yaylalar Köyü Tokat', 1);
    PRINT '  + TED044 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED045')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED045', 'Tarım Kooperatifi Burdur', 'Selin Uzun', '0248 888 7777', 'tarimkoop@burdur.com', 'Merkez Köy No:12 Burdur', 1);
    PRINT '  + TED045 eklendi';
END

-- E-Ticaret Tedarikçileri (TED046-TED050)
IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED046')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED046', 'E-Aktar Online Ltd.', 'Barış Özdemir', '0850 555 1111', 'destek@e-aktar.com', 'Sanal Ofis Levent İstanbul', 1);
    PRINT '  + TED046 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED047')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED047', 'Dijital Baharat A.Ş.', 'Esra Koç', '0850 666 2222', 'siparis@dijitalbaharat.com', 'E-Ticaret Merkezi Ankara', 1);
    PRINT '  + TED047 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED048')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED048', 'Online Bitkisel Ürünler Ltd.', 'Cem Yavuz', '0850 777 3333', 'info@onlinebitkisel.com', 'Sanal Plaza İzmir', 1);
    PRINT '  + TED048 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED049')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED049', 'Web Market Gıda', 'Tuğba Erdem', '0850 888 4444', 'satis@webmarketgida.com', 'Online Satış Platformu Bursa', 1);
    PRINT '  + TED049 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM tedarikci WHERE tedarikci_kod = 'TED050')
BEGIN
    INSERT INTO tedarikci (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif)
    VALUES ('TED050', 'Sanal Aktar Pazarı A.Ş.', 'İlker Bulut', '0850 999 5555', 'bilgi@sanalaktarpazar.com', 'İnternet Ofis Antalya', 1);
    PRINT '  + TED050 eklendi';
END

PRINT 'Tedarikçiler tamam.';
GO

-- =============================================
-- GÜVENLİK SEED (Sprint 7 seed verisi kontrolü)
-- =============================================

PRINT 'Güvenlik seed verileri kontrol ediliyor...';

-- Roller zaten seed edilmiş (001_seed_masterdata.sql): ADMIN, KULLANICI, DEPO
-- Yetkiler zaten seed edilmiş (001_seed_masterdata.sql): 15 yetki
-- Admin kullanıcı zaten seed edilmiş (001_seed_masterdata.sql): admin / hash

DECLARE @RolCount INT, @YetkiCount INT, @KullaniciCount INT;

SELECT @RolCount = COUNT(*) FROM rol;
SELECT @YetkiCount = COUNT(*) FROM yetki;
SELECT @KullaniciCount = COUNT(*) FROM kullanici WHERE kullanici_adi = 'admin';

IF @RolCount >= 3
    PRINT '  ✓ Roller mevcut: ' + CAST(@RolCount AS VARCHAR);
ELSE
    PRINT '  ⚠ Roller eksik! Sprint 1 seed verilerini kontrol edin.';

IF @YetkiCount >= 15
    PRINT '  ✓ Yetkiler mevcut: ' + CAST(@YetkiCount AS VARCHAR);
ELSE
    PRINT '  ⚠ Yetkiler eksik! Sprint 1 seed verilerini kontrol edin.';

IF @KullaniciCount >= 1
    PRINT '  ✓ Admin kullanıcı mevcut';
ELSE
    PRINT '  ⚠ Admin kullanıcı eksik! Sprint 1 seed verilerini kontrol edin.';

GO

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Reference Data Seed TAMAM';
PRINT '========================================';

DECLARE @KategoriCount INT, @TedarikciCount INT;
SELECT @KategoriCount = COUNT(*) FROM urun_kategori WHERE aktif = 1;
SELECT @TedarikciCount = COUNT(*) FROM tedarikci WHERE aktif = 1;

PRINT 'Kategori sayısı: ' + CAST(@KategoriCount AS VARCHAR);
PRINT 'Tedarikçi sayısı: ' + CAST(@TedarikciCount AS VARCHAR);

IF @KategoriCount >= 12
    PRINT '✓ Kategori hedefi ulaşıldı (>=12)';
ELSE
    PRINT '⚠ Kategori hedefi eksik (beklenen >=12)';

IF @TedarikciCount >= 10
    PRINT '✓ Tedarikçi hedefi ulaşıldı (>=10)';
ELSE
    PRINT '⚠ Tedarikçi hedefi eksik (beklenen >=10)';

PRINT '========================================';
GO
