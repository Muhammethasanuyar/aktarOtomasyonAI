-- =============================================
-- Aktar Otomasyon - Veritabanı Şeması
-- Versiyon: 1.0
-- Tarih: 2025-12-18
-- =============================================

-- Veritabanı oluşturma (varsa atla)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'AktarOtomasyon')
BEGIN
    CREATE DATABASE [AktarOtomasyon]
END
GO

USE [AktarOtomasyon]
GO

-- =============================================
-- ÜRÜN TABLOLARI
-- =============================================

-- Ürün Kategorileri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urun_kategori]'))
BEGIN
    CREATE TABLE [dbo].[urun_kategori] (
        [kategori_id] INT IDENTITY(1,1) PRIMARY KEY,
        [kategori_kod] NVARCHAR(20) NOT NULL UNIQUE,
        [kategori_adi] NVARCHAR(100) NOT NULL,
        [ust_kategori_id] INT NULL,
        [aktif] BIT NOT NULL DEFAULT 1,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([ust_kategori_id]) REFERENCES [dbo].[urun_kategori]([kategori_id])
    )
END
GO

-- Ürün Birimleri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urun_birim]'))
BEGIN
    CREATE TABLE [dbo].[urun_birim] (
        [birim_id] INT IDENTITY(1,1) PRIMARY KEY,
        [birim_kod] NVARCHAR(10) NOT NULL UNIQUE,
        [birim_adi] NVARCHAR(50) NOT NULL,
        [aktif] BIT NOT NULL DEFAULT 1
    )
END
GO

-- Ürünler
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urun]'))
BEGIN
    CREATE TABLE [dbo].[urun] (
        [urun_id] INT IDENTITY(1,1) PRIMARY KEY,
        [urun_kod] NVARCHAR(50) NOT NULL UNIQUE,
        [urun_adi] NVARCHAR(200) NOT NULL,
        [kategori_id] INT NULL,
        [birim_id] INT NULL,
        [alis_fiyati] DECIMAL(18,2) NULL,
        [satis_fiyati] DECIMAL(18,2) NULL,
        [barkod] NVARCHAR(50) NULL,
        [aciklama] NVARCHAR(MAX) NULL,
        [aktif] BIT NOT NULL DEFAULT 1,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        [guncelleme_tarih] DATETIME NULL,
        FOREIGN KEY ([kategori_id]) REFERENCES [dbo].[urun_kategori]([kategori_id]),
        FOREIGN KEY ([birim_id]) REFERENCES [dbo].[urun_birim]([birim_id])
    )
END
GO

-- Ürün Stok Ayarları
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urun_stok_ayar]'))
BEGIN
    CREATE TABLE [dbo].[urun_stok_ayar] (
        [ayar_id] INT IDENTITY(1,1) PRIMARY KEY,
        [urun_id] INT NOT NULL UNIQUE,
        [min_stok] DECIMAL(18,2) NOT NULL DEFAULT 0,
        [max_stok] DECIMAL(18,2) NULL,
        [kritik_stok] DECIMAL(18,2) NOT NULL DEFAULT 0,
        [siparis_miktari] DECIMAL(18,2) NULL,
        FOREIGN KEY ([urun_id]) REFERENCES [dbo].[urun]([urun_id])
    )
END
GO

-- Ürün Görselleri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urun_gorsel]'))
BEGIN
    CREATE TABLE [dbo].[urun_gorsel] (
        [gorsel_id] INT IDENTITY(1,1) PRIMARY KEY,
        [urun_id] INT NOT NULL,
        [gorsel_path] NVARCHAR(500) NOT NULL,
        [gorsel_tip] NVARCHAR(50) NULL,
        [ana_gorsel] BIT NOT NULL DEFAULT 0,
        [sira] INT NOT NULL DEFAULT 0,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([urun_id]) REFERENCES [dbo].[urun]([urun_id])
    )
END
GO

-- =============================================
-- STOK TABLOLARI
-- =============================================

-- Stok Hareketleri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[stok_hareket]'))
BEGIN
    CREATE TABLE [dbo].[stok_hareket] (
        [hareket_id] INT IDENTITY(1,1) PRIMARY KEY,
        [urun_id] INT NOT NULL,
        [hareket_tip] NVARCHAR(20) NOT NULL, -- GIRIS, CIKIS, SAYIM, DUZELTME
        [miktar] DECIMAL(18,2) NOT NULL,
        [onceki_bakiye] DECIMAL(18,2) NULL,
        [sonraki_bakiye] DECIMAL(18,2) NULL,
        [referans_tip] NVARCHAR(50) NULL, -- SIPARIS, SATIS, MANUEL
        [referans_id] INT NULL,
        [aciklama] NVARCHAR(500) NULL,
        [kullanici_id] INT NULL,
        [hareket_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([urun_id]) REFERENCES [dbo].[urun]([urun_id])
    )
END
GO

-- =============================================
-- SİPARİŞ TABLOLARI
-- =============================================

-- Tedarikçiler
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tedarikci]'))
BEGIN
    CREATE TABLE [dbo].[tedarikci] (
        [tedarikci_id] INT IDENTITY(1,1) PRIMARY KEY,
        [tedarikci_kod] NVARCHAR(20) NOT NULL UNIQUE,
        [tedarikci_adi] NVARCHAR(200) NOT NULL,
        [yetkili] NVARCHAR(100) NULL,
        [telefon] NVARCHAR(20) NULL,
        [email] NVARCHAR(100) NULL,
        [adres] NVARCHAR(500) NULL,
        [aktif] BIT NOT NULL DEFAULT 1,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO

-- Siparişler
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[siparis]'))
BEGIN
    CREATE TABLE [dbo].[siparis] (
        [siparis_id] INT IDENTITY(1,1) PRIMARY KEY,
        [siparis_no] NVARCHAR(20) NOT NULL UNIQUE,
        [tedarikci_id] INT NOT NULL,
        [siparis_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        [beklenen_teslim_tarih] DATETIME NULL,
        [teslim_tarih] DATETIME NULL,
        [durum] NVARCHAR(20) NOT NULL DEFAULT 'TASLAK', -- TASLAK, BEKLIYOR, TESLIMAT, TAMAMLANDI, IPTAL
        [toplam_tutar] DECIMAL(18,2) NOT NULL DEFAULT 0,
        [aciklama] NVARCHAR(500) NULL,
        [kullanici_id] INT NULL,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([tedarikci_id]) REFERENCES [dbo].[tedarikci]([tedarikci_id])
    )
END
GO

-- Sipariş Satırları
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[siparis_satir]'))
BEGIN
    CREATE TABLE [dbo].[siparis_satir] (
        [satir_id] INT IDENTITY(1,1) PRIMARY KEY,
        [siparis_id] INT NOT NULL,
        [urun_id] INT NOT NULL,
        [miktar] DECIMAL(18,2) NOT NULL,
        [birim_fiyat] DECIMAL(18,2) NOT NULL,
        [tutar] DECIMAL(18,2) NOT NULL,
        [teslim_miktar] DECIMAL(18,2) NOT NULL DEFAULT 0,
        FOREIGN KEY ([siparis_id]) REFERENCES [dbo].[siparis]([siparis_id]),
        FOREIGN KEY ([urun_id]) REFERENCES [dbo].[urun]([urun_id])
    )
END
GO

-- =============================================
-- BİLDİRİM TABLOLARI
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bildirim]'))
BEGIN
    CREATE TABLE [dbo].[bildirim] (
        [bildirim_id] INT IDENTITY(1,1) PRIMARY KEY,
        [bildirim_tip] NVARCHAR(50) NOT NULL, -- STOK_KRITIK, SIPARIS_GELEN, AI_ONAY_BEKLIYOR
        [baslik] NVARCHAR(200) NOT NULL,
        [icerik] NVARCHAR(MAX) NULL,
        [referans_tip] NVARCHAR(50) NULL,
        [referans_id] INT NULL,
        [okundu] BIT NOT NULL DEFAULT 0,
        [kullanici_id] INT NULL,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        [okunma_tarih] DATETIME NULL
    )
END
GO

-- =============================================
-- AI TABLOLARI
-- =============================================

-- AI Ürün İçerikleri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ai_urun_icerik]'))
BEGIN
    CREATE TABLE [dbo].[ai_urun_icerik] (
        [icerik_id] INT IDENTITY(1,1) PRIMARY KEY,
        [urun_id] INT NOT NULL,
        [icerik] NVARCHAR(MAX) NOT NULL,
        [durum] NVARCHAR(20) NOT NULL DEFAULT 'TASLAK', -- TASLAK, ONAY_BEKLIYOR, AKTIF
        [kaynaklar] NVARCHAR(MAX) NULL,
        [sablon_kod] NVARCHAR(50) NULL,
        [provider] NVARCHAR(50) NULL,
        [kullanici_id] INT NULL,
        [onaylayan_kullanici_id] INT NULL,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        [onay_tarih] DATETIME NULL,
        FOREIGN KEY ([urun_id]) REFERENCES [dbo].[urun]([urun_id])
    )
END
GO

-- AI İçerik Versiyonları
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ai_urun_icerik_ver]'))
BEGIN
    CREATE TABLE [dbo].[ai_urun_icerik_ver] (
        [versiyon_id] INT IDENTITY(1,1) PRIMARY KEY,
        [icerik_id] INT NOT NULL,
        [versiyon_no] INT NOT NULL,
        [icerik] NVARCHAR(MAX) NOT NULL,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([icerik_id]) REFERENCES [dbo].[ai_urun_icerik]([icerik_id])
    )
END
GO

-- =============================================
-- YETKİ TABLOLARI
-- =============================================

-- Kullanıcılar
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[kullanici]'))
BEGIN
    CREATE TABLE [dbo].[kullanici] (
        [kullanici_id] INT IDENTITY(1,1) PRIMARY KEY,
        [kullanici_adi] NVARCHAR(50) NOT NULL UNIQUE,
        [ad_soyad] NVARCHAR(100) NOT NULL,
        [sifre_hash] NVARCHAR(256) NOT NULL,
        [email] NVARCHAR(100) NULL,
        [aktif] BIT NOT NULL DEFAULT 1,
        [son_giris_tarih] DATETIME NULL,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO

-- Roller
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rol]'))
BEGIN
    CREATE TABLE [dbo].[rol] (
        [rol_id] INT IDENTITY(1,1) PRIMARY KEY,
        [rol_kod] NVARCHAR(20) NOT NULL UNIQUE,
        [rol_adi] NVARCHAR(100) NOT NULL,
        [aciklama] NVARCHAR(500) NULL,
        [aktif] BIT NOT NULL DEFAULT 1
    )
END
GO

-- Kullanıcı Rolleri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[kullanici_rol]'))
BEGIN
    CREATE TABLE [dbo].[kullanici_rol] (
        [id] INT IDENTITY(1,1) PRIMARY KEY,
        [kullanici_id] INT NOT NULL,
        [rol_id] INT NOT NULL,
        FOREIGN KEY ([kullanici_id]) REFERENCES [dbo].[kullanici]([kullanici_id]),
        FOREIGN KEY ([rol_id]) REFERENCES [dbo].[rol]([rol_id]),
        UNIQUE ([kullanici_id], [rol_id])
    )
END
GO

-- Yetkiler
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[yetki]'))
BEGIN
    CREATE TABLE [dbo].[yetki] (
        [yetki_id] INT IDENTITY(1,1) PRIMARY KEY,
        [yetki_kod] NVARCHAR(50) NOT NULL UNIQUE,
        [yetki_adi] NVARCHAR(100) NOT NULL,
        [modul] NVARCHAR(50) NULL,
        [aktif] BIT NOT NULL DEFAULT 1
    )
END
GO

-- Rol Yetkileri
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rol_yetki]'))
BEGIN
    CREATE TABLE [dbo].[rol_yetki] (
        [id] INT IDENTITY(1,1) PRIMARY KEY,
        [rol_id] INT NOT NULL,
        [yetki_id] INT NOT NULL,
        FOREIGN KEY ([rol_id]) REFERENCES [dbo].[rol]([rol_id]),
        FOREIGN KEY ([yetki_id]) REFERENCES [dbo].[yetki]([yetki_id]),
        UNIQUE ([rol_id], [yetki_id])
    )
END
GO

-- =============================================
-- AUDIT TABLOLARI
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[audit_log]'))
BEGIN
    CREATE TABLE [dbo].[audit_log] (
        [log_id] INT IDENTITY(1,1) PRIMARY KEY,
        [tablo_adi] NVARCHAR(100) NOT NULL,
        [kayit_id] INT NOT NULL,
        [islem_tip] NVARCHAR(20) NOT NULL, -- INSERT, UPDATE, DELETE
        [eski_deger] NVARCHAR(MAX) NULL,
        [yeni_deger] NVARCHAR(MAX) NULL,
        [kullanici_id] INT NULL,
        [islem_tarih] DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO

-- =============================================
-- EKRAN YÖNETİMİ TABLOLARI
-- =============================================

-- kul_ekran - Ekran tanımları
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[kul_ekran]'))
BEGIN
    CREATE TABLE [dbo].[kul_ekran] (
        [ekran_id] INT IDENTITY(1,1) PRIMARY KEY,
        [ekran_kod] NVARCHAR(50) NOT NULL UNIQUE,
        [menudeki_adi] NVARCHAR(100) NOT NULL,
        [form_adi] NVARCHAR(100) NULL,
        [modul] NVARCHAR(50) NULL,
        [aciklama] NVARCHAR(500) NULL,
        [sira] INT NOT NULL DEFAULT 0,
        [aktif] BIT NOT NULL DEFAULT 1,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO

-- kul_ekran_log - Ekran açılış logları
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[kul_ekran_log]'))
BEGIN
    CREATE TABLE [dbo].[kul_ekran_log] (
        [log_id] INT IDENTITY(1,1) PRIMARY KEY,
        [ekran_id] INT NOT NULL,
        [kullanici_id] INT NULL,
        [versiyon] NVARCHAR(20) NULL,
        [acilis_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([ekran_id]) REFERENCES [dbo].[kul_ekran]([ekran_id])
    )
END
GO

-- =============================================
-- AI MODÜLÜ TABLOLARI
-- =============================================

-- ai_sablon - AI Prompt Şablonları
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ai_sablon]'))
BEGIN
    CREATE TABLE [dbo].[ai_sablon] (
        [sablon_id] INT IDENTITY(1,1) PRIMARY KEY,
        [sablon_kod] NVARCHAR(50) NOT NULL UNIQUE,
        [sablon_adi] NVARCHAR(200) NOT NULL,
        [prompt_sablonu] NVARCHAR(MAX) NOT NULL,
        [aciklama] NVARCHAR(500) NULL,
        [aktif] BIT NOT NULL DEFAULT 1,
        [olusturma_tarih] DATETIME NOT NULL DEFAULT GETDATE(),
        [guncelleme_tarih] DATETIME NULL
    )
END
GO

PRINT 'Tablo oluşturma tamamlandı.'
GO
