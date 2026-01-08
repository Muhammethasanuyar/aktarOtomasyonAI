# Proje Referans Matrisi

## Referans Kuralları

| Proje | İzin Verilen Referanslar | YASAK |
|-------|-------------------------|-------|
| **Forms** | Common.Interface, Urun.Interface, Stok.Interface, Siparis.Interface, Ai.Interface | *.Service, Util.DataAccess |
| ***.Interface** | Kendi *.Service, Common.Interface | - |
| ***.Service** | Common.Interface, Common.Service, Util.DataAccess, Kendi *.Interface | - |
| **Util.DataAccess** | System.Data.SqlClient, SPBuilder DLL'ler | - |

## Görsel Matris

```
                    Forms    Common.I  Common.S  DataAccess  Urun.I  Urun.S  ...
Forms                 -         ✓         ✗          ✗         ✓       ✗
Common.Interface      ✗         -         ✓          ✗         ✗       ✗
Common.Service        ✗         ✓         -          ✓         ✗       ✗
Util.DataAccess       ✗         ✗         ✗          -         ✗       ✗
Urun.Interface        ✗         ✓         ✗          ✗         -       ✓
Urun.Service          ✗         ✓         ✓          ✓         ✓       -
```

## Doğrulama Komutu

```powershell
# Forms projesinde yasak referans kontrolü
Select-String -Path "src/AktarOtomasyon.Forms/*.csproj" -Pattern "(\.Service|DataAccess)"
# Sonuç boş olmalı!
```
