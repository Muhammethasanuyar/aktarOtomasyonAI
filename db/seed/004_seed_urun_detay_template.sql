USE [AktarOtomasyon]
GO

-- =============================================
-- URUN_DETAY_V1 AI Şablon Ekle
-- Sprint 9: Ürün Detay Ekranı için AI İçerik Şablonu
-- JSON Format: {fayda, kullanim, uyari, kombinasyon}
-- =============================================

-- Önce mevcut kayıt varsa sil (re-run için)
DELETE FROM [dbo].[ai_sablon] WHERE [sablon_kod] = 'URUN_DETAY_V1'
GO

-- Yeni şablon ekle
INSERT INTO [dbo].[ai_sablon] (
    [sablon_kod],
    [sablon_adi],
    [prompt_sablonu],
    [aciklama],
    [aktif],
    [olusturma_tarih]
)
VALUES (
    'URUN_DETAY_V1',
    'Ürün Detay Ekranı - Yapılandırılmış İçerik',
    'Ürün: {URUN_ADI} ({URUN_KOD})
Kategori: {KATEGORI}
Birim: {BIRIM}
Fiyat: {FIYAT}
Açıklama: {ACIKLAMA}

Lütfen bu ürün için aşağıdaki bilgileri JSON formatında üret:
1. "Fayda": Ürünün faydalarını 2-3 madde halinde açıkla
2. "Kullanim": Kullanım talimatlarını madde madde belirt
3. "Uyari": Varsa uyarıları ve dikkat edilecek noktaları belirt
4. "Kombinasyon": Hangi ürünlerle birlikte kullanılabileceğini öner

Önemli Kurallar:
- Tıbbi tedavi iddiaları kullanma
- Türkçe yaz
- Her alan için en az 2-3 cümle yaz
- Madde işaretleri kullan (•)
- JSON formatında döndür

Format Örneği:
{
  "Fayda": "• Sindirim sistemini destekler\n• Doğal antioksidan kaynağıdır\n• Bağışıklık sistemini güçlendirir",
  "Kullanim": "• Günde 1-2 kez kullanılabilir\n• Yemeklerden önce veya sonra alınabilir\n• Uzun süreli kullanım için uzman tavsiyesi önerilir",
  "Uyari": "• Hamileler ve emziren anneler doktor tavsiyesi almalıdır\n• Alerjik reaksiyon görülürse kullanımı bırakın\n• Önerilen dozajı aşmayın",
  "Kombinasyon": "• Bal ile birlikte tüketilebilir\n• Limon suyu ile kullanılabilir\n• Diğer bitkisel ürünlerle kombinasyon yapmadan önce danışın"
}',
    'Ürün detay ekranı için yapılandırılmış AI içerik (fayda, kullanım, uyarı, kombinasyon). JSON formatında 4 ayrı bölüm.',
    1,
    GETDATE()
);
GO

-- Doğrulama sorgusu
SELECT
    [sablon_id],
    [sablon_kod],
    [sablon_adi],
    [aktif],
    [olusturma_tarih]
FROM [dbo].[ai_sablon]
WHERE [sablon_kod] = 'URUN_DETAY_V1'
GO

PRINT 'URUN_DETAY_V1 şablonu başarıyla eklendi.'
GO
