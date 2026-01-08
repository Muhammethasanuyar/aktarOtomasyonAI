-- =============================================
-- Aktar Otomasyon - Master Data Seed
-- Versiyon: 1.0
-- =============================================

USE [AktarOtomasyon]
GO

-- =============================================
-- BİRİMLER
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[urun_birim] WHERE [birim_kod] = 'ADET')
BEGIN
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('ADET', 'Adet');
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('KG', 'Kilogram');
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('GR', 'Gram');
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('LT', 'Litre');
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('ML', 'Mililitre');
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('PKT', 'Paket');
    INSERT INTO [dbo].[urun_birim] ([birim_kod], [birim_adi]) VALUES ('KTU', 'Kutu');
    PRINT 'Birimler eklendi.'
END
GO

-- =============================================
-- KATEGORİLER
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[urun_kategori] WHERE [kategori_kod] = 'BAHARAT')
BEGIN
    INSERT INTO [dbo].[urun_kategori] ([kategori_kod], [kategori_adi]) VALUES ('BAHARAT', 'Baharatlar');
    INSERT INTO [dbo].[urun_kategori] ([kategori_kod], [kategori_adi]) VALUES ('KURUYEMIS', 'Kuruyemişler');
    INSERT INTO [dbo].[urun_kategori] ([kategori_kod], [kategori_adi]) VALUES ('BAKLIYAT', 'Bakliyatlar');
    INSERT INTO [dbo].[urun_kategori] ([kategori_kod], [kategori_adi]) VALUES ('SEKER', 'Şekerlemeler');
    INSERT INTO [dbo].[urun_kategori] ([kategori_kod], [kategori_adi]) VALUES ('KAHVE', 'Kahve ve Çay');
    INSERT INTO [dbo].[urun_kategori] ([kategori_kod], [kategori_adi]) VALUES ('DIGER', 'Diğer');
    PRINT 'Kategoriler eklendi.'
END
GO

-- =============================================
-- ROLLER
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[rol] WHERE [rol_kod] = 'ADMIN')
BEGIN
    INSERT INTO [dbo].[rol] ([rol_kod], [rol_adi], [aciklama]) 
    VALUES ('ADMIN', 'Sistem Yöneticisi', 'Tüm yetkilere sahip');
    
    INSERT INTO [dbo].[rol] ([rol_kod], [rol_adi], [aciklama]) 
    VALUES ('KULLANICI', 'Standart Kullanıcı', 'Temel işlem yetkileri');
    
    INSERT INTO [dbo].[rol] ([rol_kod], [rol_adi], [aciklama]) 
    VALUES ('DEPO', 'Depo Sorumlusu', 'Stok ve sipariş yetkileri');
    
    PRINT 'Roller eklendi.'
END
GO

-- =============================================
-- YETKİLER
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[yetki] WHERE [yetki_kod] = 'URUN_GORUNTULE')
BEGIN
    -- Ürün yetkileri
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('URUN_GORUNTULE', 'Ürün Görüntüle', 'URUN');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('URUN_EKLE', 'Ürün Ekle', 'URUN');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('URUN_DUZENLE', 'Ürün Düzenle', 'URUN');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('URUN_SIL', 'Ürün Sil', 'URUN');
    
    -- Stok yetkileri
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('STOK_GORUNTULE', 'Stok Görüntüle', 'STOK');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('STOK_HAREKET', 'Stok Hareket', 'STOK');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('STOK_SAYIM', 'Stok Sayım', 'STOK');
    
    -- Sipariş yetkileri
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('SIPARIS_GORUNTULE', 'Sipariş Görüntüle', 'SIPARIS');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('SIPARIS_OLUSTUR', 'Sipariş Oluştur', 'SIPARIS');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('SIPARIS_ONAYLA', 'Sipariş Onayla', 'SIPARIS');
    
    -- AI yetkileri
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('AI_URET', 'AI İçerik Üret', 'AI');
    INSERT INTO [dbo].[yetki] ([yetki_kod], [yetki_adi], [modul]) VALUES ('AI_ONAYLA', 'AI İçerik Onayla', 'AI');
    
    PRINT 'Yetkiler eklendi.'
END
GO

-- =============================================
-- ADMIN KULLANICISI
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[kullanici] WHERE [kullanici_adi] = 'admin')
BEGIN
    -- Şifre: admin123 (gerçek ortamda hash kullanılmalı)
    INSERT INTO [dbo].[kullanici] ([kullanici_adi], [ad_soyad], [sifre_hash], [email]) 
    VALUES ('admin', 'Sistem Yöneticisi', 'admin123_hash', 'admin@aktar.com');
    
    -- Admin rolü ata
    DECLARE @AdminUserId INT = SCOPE_IDENTITY();
    DECLARE @AdminRolId INT = (SELECT [rol_id] FROM [dbo].[rol] WHERE [rol_kod] = 'ADMIN');
    
    INSERT INTO [dbo].[kullanici_rol] ([kullanici_id], [rol_id]) VALUES (@AdminUserId, @AdminRolId);
    
    PRINT 'Admin kullanıcısı eklendi.'
END
GO

-- =============================================
-- ÖRNEK TEDARİKÇİ
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[tedarikci] WHERE [tedarikci_kod] = 'TED001')
BEGIN
    INSERT INTO [dbo].[tedarikci] ([tedarikci_kod], [tedarikci_adi], [yetkili], [telefon]) 
    VALUES ('TED001', 'Örnek Tedarikçi A.Ş.', 'Ahmet Yılmaz', '0212 555 1234');
    
    PRINT 'Örnek tedarikçi eklendi.'
END
GO

-- =============================================
-- KUL_EKRAN KAYITLARI (Kritik)
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[kul_ekran] WHERE [ekran_kod] = 'ANA_DASH')
BEGIN
    -- Common
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('ANA_DASH', 'Dashboard', 'FrmMain', 'COMMON', 1);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('BILDIRIM_MRK', 'Bildirim Merkezi', 'FrmBildirim', 'COMMON', 2);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('KUL_YETKI', 'Kullanıcı Yönetimi', 'FrmKullanici', 'COMMON', 3);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('SISTEM_AYAR', 'Sistem Ayarları', 'FrmAyarlar', 'COMMON', 4);
    
    -- Ürün
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('URUN_LISTE', 'Ürün Listesi', 'FrmUrunListe', 'URUN', 10);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('URUN_KART', 'Ürün Kartı', 'FrmUrunKart', 'URUN', 11);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('URUN_KATEGORI', 'Kategoriler', 'FrmKategori', 'URUN', 12);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('URUN_BIRIM', 'Birimler', 'FrmBirim', 'URUN', 13);
    
    -- Stok
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('STOK_HAREKET', 'Stok Hareket', 'FrmStokHareket', 'STOK', 20);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('STOK_SAYIM', 'Stok Sayım', 'FrmStokSayim', 'STOK', 21);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('STOK_KRITIK', 'Kritik Stok', 'FrmStokKritik', 'STOK', 22);
    
    -- Sipariş
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('SIP_TASLAK', 'Sipariş Taslak', 'FrmSiparisTaslak', 'SIPARIS', 30);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('SIP_LISTE', 'Sipariş Listesi', 'FrmSiparisListe', 'SIPARIS', 31);
    
    -- AI
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('AI_ONAY', 'AI Onay', 'FrmAiOnay', 'AI', 40);
    
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [sira]) 
    VALUES ('AI_SABLON', 'AI Şablonlar', 'FrmAiSablon', 'AI', 41);
    
    PRINT 'Ekran kayıtları eklendi.'
END
GO

PRINT 'Seed işlemi tamamlandı.'
GO
