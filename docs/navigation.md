# Navigasyon Haritası

## Ana Akışlar

```
Dashboard (ANA_DASH)
├── Kritik Stok Uyarıları → STOK_KRITIK
│   └── Sipariş Oluştur → SIP_TASLAK
├── Bildirimler → BILDIRIM_MRK
└── Hızlı Erişim
    ├── Ürün Ara → URUN_LISTE
    └── Sipariş Durumu → SIP_LISTE

Ürün Liste (URUN_LISTE)
├── Yeni Ürün → URUN_KART (yeni)
├── Düzenle (çift tık) → URUN_KART (mevcut)
│   ├── AI İçerik Üret → AI_ONAY
│   └── Stok Ayarları
└── Kategoriler → URUN_KATEGORI

Stok Hareket (STOK_HAREKET)
├── Giriş/Çıkış Kaydet
└── İşlem Sonrası → BILDIRIM_MRK (kritik stok uyarısı)

Sipariş Liste (SIP_LISTE)
├── Yeni Sipariş → SIP_TASLAK
├── Durum Güncelle
└── Teslim Al → STOK_HAREKET (otomatik giriş)

AI Onay (AI_ONAY)
├── Onay Bekleyenler Listesi
├── İçerik Önizleme
└── Onayla/Reddet
```

## Ekran Geçiş Kuralları

1. Form açılışında `kul_ekran` tablosundan başlık alınır
2. Her açılışta versiyon loglanır
3. Alt ekranlar modal olarak açılır
4. Ana navigasyon Dashboard'a döner
