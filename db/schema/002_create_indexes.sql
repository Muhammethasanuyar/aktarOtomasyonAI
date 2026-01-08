-- =============================================
-- Aktar Otomasyon - İndeksler
-- Versiyon: 1.0
-- =============================================

USE [AktarOtomasyon]
GO

-- Ürün İndeksleri
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_urun_kategori_id')
    CREATE INDEX IX_urun_kategori_id ON [dbo].[urun]([kategori_id]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_urun_barkod')
    CREATE INDEX IX_urun_barkod ON [dbo].[urun]([barkod]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_urun_aktif')
    CREATE INDEX IX_urun_aktif ON [dbo].[urun]([aktif]);

-- Stok Hareket İndeksleri
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_stok_hareket_urun_id')
    CREATE INDEX IX_stok_hareket_urun_id ON [dbo].[stok_hareket]([urun_id]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_stok_hareket_tarih')
    CREATE INDEX IX_stok_hareket_tarih ON [dbo].[stok_hareket]([hareket_tarih]);

-- Sipariş İndeksleri
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_siparis_tedarikci_id')
    CREATE INDEX IX_siparis_tedarikci_id ON [dbo].[siparis]([tedarikci_id]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_siparis_durum')
    CREATE INDEX IX_siparis_durum ON [dbo].[siparis]([durum]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_siparis_satir_siparis_id')
    CREATE INDEX IX_siparis_satir_siparis_id ON [dbo].[siparis_satir]([siparis_id]);

-- Bildirim İndeksleri
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_bildirim_okundu')
    CREATE INDEX IX_bildirim_okundu ON [dbo].[bildirim]([okundu]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_bildirim_kullanici_id')
    CREATE INDEX IX_bildirim_kullanici_id ON [dbo].[bildirim]([kullanici_id]);

-- AI İndeksleri
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ai_urun_icerik_urun_id')
    CREATE INDEX IX_ai_urun_icerik_urun_id ON [dbo].[ai_urun_icerik]([urun_id]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ai_urun_icerik_durum')
    CREATE INDEX IX_ai_urun_icerik_durum ON [dbo].[ai_urun_icerik]([durum]);

-- Ekran Log İndeksleri
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_kul_ekran_log_ekran_id')
    CREATE INDEX IX_kul_ekran_log_ekran_id ON [dbo].[kul_ekran_log]([ekran_id]);

PRINT 'İndeks oluşturma tamamlandı.'
GO
