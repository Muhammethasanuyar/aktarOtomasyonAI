-- =============================================
-- AI Şablon Seed Data
-- =============================================

USE [AktarOtomasyon]
GO

-- Delete existing templates (if re-running)
DELETE FROM [dbo].[ai_sablon];
GO

-- Template 1: Product Description
INSERT INTO [dbo].[ai_sablon]
    ([sablon_kod], [sablon_adi], [prompt_sablonu], [aciklama], [aktif])
VALUES
    ('URUN_ACIKLAMA',
     'Ürün Açıklaması',
     'Sen bir aktar dükkanı için ürün açıklamaları yazan bir uzmansın. Aşağıdaki ürün için ilgi çekici, bilgilendirici ve SEO uyumlu bir açıklama yaz.

Ürün Adı: {URUN_ADI}
Kategori: {KATEGORI}
Birim: {BIRIM}
Fiyat: {FIYAT} TL

Açıklama:
- 2-3 paragraf uzunluğunda olsun
- Ürünün faydalarından bahset (ANCAK tıbbi iddialar içerme)
- Kullanım önerileri ver
- Doğal ve samimi bir dil kullan
- Türkçe yaz

ÖNEMLİ: "Tedavi eder", "iyileştirir", "hastalık önler" gibi tıbbi iddialar KULLANMA.',
     'Genel ürün açıklaması şablonu',
     1);

-- Template 2: SEO Keywords
INSERT INTO [dbo].[ai_sablon]
    ([sablon_kod], [sablon_adi], [prompt_sablonu], [aciklama], [aktif])
VALUES
    ('URUN_SEO',
     'SEO Anahtar Kelimeler',
     'Aşağıdaki ürün için SEO anahtar kelimeleri öner (virgülle ayrılmış liste).

Ürün: {URUN_ADI}
Kategori: {KATEGORI}

10-15 anahtar kelime öner. Sadece virgülle ayrılmış liste formatında yaz, başka açıklama ekleme.',
     'SEO anahtar kelime şablonu',
     1);

-- Template 3: Usage Instructions
INSERT INTO [dbo].[ai_sablon]
    ([sablon_kod], [sablon_adi], [prompt_sablonu], [aciklama], [aktif])
VALUES
    ('URUN_KULLANIM',
     'Kullanım Talimatları',
     'Aşağıdaki aktar ürünü için kullanım talimatları yaz.

Ürün: {URUN_ADI}
Kategori: {KATEGORI}
Birim: {BIRIM}

Kullanım talimatlarını madde madde liste halinde ver. Pratik ve anlaşılır olsun. Tıbbi iddialar içerme.',
     'Kullanım talimatları şablonu',
     1);

PRINT '3 adet AI şablonu eklendi.'
GO
