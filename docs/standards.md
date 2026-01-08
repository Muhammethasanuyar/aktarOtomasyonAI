# Kodlama Standartları

## Service Katmanı

```csharp
// ✓ DOĞRU - Stateless, try/catch, using, string return
public class UrunService : IUrunInterface
{
    public string Kaydet(UrunModel urun)
    {
        try
        {
            using (var sMan = new SqlManager())
            {
                // SP çağrısı
                return null; // Başarı
            }
        }
        catch (Exception ex)
        {
            return ex.Message; // Hata
        }
    }
}

// ✗ YANLIŞ - Class-level değişken
public class UrunService
{
    private SqlManager _sMan; // YASAK!
}
```

## UI Katmanı

```csharp
// ✓ DOĞRU - DMLManager ile hata kontrolü
var hata = interfaceInstance.Kaydet(model);
if (!DMLManager.KaydetKontrol(hata))
    return; // İşlem kesildi

// ✓ DOĞRU - Form standardı
this.MaximumSize = new Size(770, 700);
this.AutoScroll = true;
```

## Naming Conventions

| Tip | Örnek |
|-----|-------|
| Interface | `IUrunInterface` |
| Service | `UrunService` |
| Model | `UrunModel` |
| Form | `FrmUrunListe` |
| UserControl | `UcUrunKart` |
| SP | `sp_urun_kaydet` |
| Tablo | `urun`, `urun_kategori` |
