-- =============================================
-- Sprint 9 - Product Images Seed (Metadata Only)
-- Ürün Görselleri - FileSystem Mode
-- =============================================

USE [AktarOtomasyon]
GO

PRINT 'Sprint 9 - Product Images Seed başlatılıyor...';
PRINT 'NOT: Bu script sadece metadata oluşturur. Gerçek görseller FileSystem''e deploy edilmelidir.';
GO

-- =============================================
-- IMAGE METADATA FOR PRODUCTS
-- FileSystem path: {MEDIA_PATH}\products\{urunId}\main.jpg
-- =============================================

PRINT 'Ürün görseli metadata kayıtları ekleniyor...';

-- Sample products with main images (50 products)
DECLARE @UrunKodlari TABLE (urun_kod VARCHAR(20), gorsel_dosya VARCHAR(100));

INSERT INTO @UrunKodlari (urun_kod, gorsel_dosya) VALUES
('BAH001', 'karabiber.jpg'),
('BAH002', 'kimyon.jpg'),
('BAH003', 'zerdeçal.jpg'),
('BAH004', 'pul_biber.jpg'),
('BAH005', 'tarçin.jpg'),
('BAH006', 'karanfil.jpg'),
('BAH007', 'kakule.jpg'),
('BAH008', 'safran.jpg'),
('BAH009', 'kisnis.jpg'),
('BAH010', 'sumak.jpg'),
('BTK001', 'ihlamur.jpg'),
('BTK002', 'papatya.jpg'),
('BTK003', 'adacayi.jpg'),
('BTK004', 'melisa.jpg'),
('BTK005', 'kusburnu.jpg'),
('BTK006', 'rezene.jpg'),
('BTK007', 'lavanta.jpg'),
('BTK008', 'hatmi.jpg'),
('CAY001', 'yesil_cay.jpg'),
('CAY002', 'siyah_cay.jpg'),
('CAY003', 'beyaz_cay.jpg'),
('CAY004', 'oolong.jpg'),
('CAY005', 'mate.jpg'),
('YAG001', 'corek_otu_yagi.jpg'),
('YAG002', 'argan_yagi.jpg'),
('YAG003', 'jojoba_yagi.jpg'),
('YAG004', 'badem_yagi.jpg'),
('YAG005', 'zeytinyagi.jpg'),
('KUR001', 'antep_fistigi.jpg'),
('KUR002', 'badem.jpg'),
('KUR003', 'ceviz.jpg'),
('KUR004', 'findik.jpg'),
('KUR005', 'kaju.jpg'),
('KUR006', 'kuru_uzum.jpg'),
('KUR007', 'kuru_kayisi.jpg'),
('KUR008', 'kuru_incir.jpg'),
('BAK001', 'kirmizi_mercimek.jpg'),
('BAK002', 'yesil_mercimek.jpg'),
('BAK003', 'nohut.jpg'),
('BAK004', 'barbunya.jpg'),
('BAK005', 'pirinc.jpg'),
('MAC001', 'bitkisel_macun.jpg'),
('MAC002', 'epimedium_macun.jpg'),
('BAL001', 'cicek_bali.jpg'),
('BAL002', 'kestane_bali.jpg'),
('TOM001', 'chia_tohumu.jpg'),
('TOM002', 'keten_tohumu.jpg'),
('KOZ001', 'dogal_sabun.jpg'),
('TAK001', 'spirulina.jpg'),
('KRM001', 'argan_kremi.jpg');

-- Insert main images
DECLARE @UrunKod VARCHAR(20), @GorselDosya VARCHAR(100), @UrunId INT;

DECLARE img_cursor CURSOR FOR SELECT urun_kod, gorsel_dosya FROM @UrunKodlari;
OPEN img_cursor;
FETCH NEXT FROM img_cursor INTO @UrunKod, @GorselDosya;

WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @UrunId = urun_id FROM urun WHERE urun_kod = @UrunKod;

    IF @UrunId IS NOT NULL
    BEGIN
        -- Main image
        INSERT INTO urun_gorsel (urun_id, gorsel_path, gorsel_tip, ana_gorsel, sira, olusturma_tarih)
        VALUES (@UrunId,
                'products\' + CAST(@UrunId AS VARCHAR) + '\' + @GorselDosya,
                'image/jpeg',
                1,
                0,
                DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 30 AS INT), GETDATE()));

        -- Additional images for selected products (every 3rd product)
        IF @UrunId % 3 = 0
        BEGIN
            INSERT INTO urun_gorsel (urun_id, gorsel_path, gorsel_tip, ana_gorsel, sira, olusturma_tarih)
            VALUES (@UrunId,
                    'products\' + CAST(@UrunId AS VARCHAR) + '\detail_01.jpg',
                    'image/jpeg',
                    0,
                    1,
                    DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 25 AS INT), GETDATE()));
        END

        -- Additional images for selected products (every 5th product)
        IF @UrunId % 5 = 0
        BEGIN
            INSERT INTO urun_gorsel (urun_id, gorsel_path, gorsel_tip, ana_gorsel, sira, olusturma_tarih)
            VALUES (@UrunId,
                    'products\' + CAST(@UrunId AS VARCHAR) + '\detail_02.jpg',
                    'image/jpeg',
                    0,
                    2,
                    DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 20 AS INT), GETDATE()));
        END
    END

    FETCH NEXT FROM img_cursor INTO @UrunKod, @GorselDosya;
END

CLOSE img_cursor;
DEALLOCATE img_cursor;

PRINT '  ✓ Ürün görselleri metadata eklendi';
GO

-- =============================================
-- ÖZET
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'Sprint 9 - Product Images Seed TAMAM';
PRINT '========================================';

DECLARE @GorselCount INT, @UrunGorselCount INT, @MainGorselCount INT, @DetailGorselCount INT;

SELECT @GorselCount = COUNT(*) FROM urun_gorsel;
SELECT @UrunGorselCount = COUNT(DISTINCT urun_id) FROM urun_gorsel;
SELECT @MainGorselCount = COUNT(*) FROM urun_gorsel WHERE ana_gorsel = 1;
SELECT @DetailGorselCount = COUNT(*) FROM urun_gorsel WHERE ana_gorsel = 0;

PRINT 'Toplam görsel kayıt sayısı: ' + CAST(@GorselCount AS VARCHAR);
PRINT 'Görseli olan ürün sayısı: ' + CAST(@UrunGorselCount AS VARCHAR);
PRINT '  - Ana görsel: ' + CAST(@MainGorselCount AS VARCHAR);
PRINT '  - Detay görselleri: ' + CAST(@DetailGorselCount AS VARCHAR);
PRINT '';
PRINT 'ÖNEMLI: Gerçek görsel dosyaları {MEDIA_PATH}\products\ klasörüne yerleştirilmelidir.';
PRINT 'Örnek: C:\AktarOtomasyon\media\products\1\karabiber.jpg';
PRINT '';
PRINT '========================================';
GO
