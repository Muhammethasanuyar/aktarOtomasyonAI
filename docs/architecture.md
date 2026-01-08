# Aktar Otomasyon - Mimari Döküman

## Genel Bakış

Aktar Mağazası Otomasyonu, **DevExpress WinForms + C#** ile geliştirilmiş, katmanlı mimariye sahip bir masaüstü uygulamasıdır.

## Mimari Diyagram

```
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                        │
│                  AktarOtomasyon.Forms                        │
│         (WinForms + DevExpress, max 770x700)                │
└─────────────────────────┬───────────────────────────────────┘
                          │ Sadece Interface referansları
                          ▼
┌─────────────────────────────────────────────────────────────┐
│                    INTERFACE LAYER                           │
│  *.Interface projeleri (Urun, Stok, Siparis, Ai, Common)    │
│         Interface tanımları + Model sınıfları                │
└─────────────────────────┬───────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────┐
│                    SERVICE LAYER                             │
│  *.Service projeleri (stateless, try/catch, string return)  │
│              İş mantığı + Veri erişim çağrıları             │
└─────────────────────────┬───────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────┐
│                   DATA ACCESS LAYER                          │
│            AktarOtomasyon.Util.DataAccess                   │
│           SqlManager + SPBuilder DLL referansları            │
└─────────────────────────┬───────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────┐
│                      DATABASE                                │
│                SQL Server + Stored Procedures                │
└─────────────────────────────────────────────────────────────┘
```

## Temel Kurallar

| Kural | Açıklama |
|-------|----------|
| UI'da SQL yok | Forms katmanında veritabanı sorgusu olmayacak |
| Forms sadece Interface | Service/DataAccess referansı yasak |
| Service stateless | Class-level değişken tutulmaz |
| Service return string | null=başarı, mesaj=hata |
| using(sMan) | DB bağlantısı IDisposable pattern |
| Form boyutu | max 770x700, AutoScroll=true |
| UserControl only | Form=kabuk, içerik=UC |

## Modüller

- **Common**: Ortak servisler, ekran yönetimi
- **Urun**: Ürün CRUD, kategori, birim, görsel
- **Stok**: Stok hareket, sayım, kritik stok
- **Siparis**: Tedarikçi siparişleri
- **Ai**: AI içerik üretimi ve onay
- **Bildirim**: Sistem bildirimleri
