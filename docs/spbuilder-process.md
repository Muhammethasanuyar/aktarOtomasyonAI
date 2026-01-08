# SPBuilder Süreci

## Genel Bakış

SPBuilder, SQL Server stored procedure'larından .NET wrapper DLL'ler üreten bir araçtır.

## Kullanılan Yaklaşım

**Seçenek A (Önerilen)**: DLL'ler build sırasında üretilir. Repoda sadece manifest ve SP scriptleri bulunur.

## Manifest Dosyası

Konum: `/artifacts/spbuilder/manifest/spbuilder-manifest.json`

```json
{
  "assemblies": [
    {
      "name": "Common.Sp",
      "namespace": "AktarOtomasyon.Sp.Common",
      "outputPath": "../dll/Common.Sp.dll",
      "storedProcedures": ["sp_kul_ekran_getir", ...]
    }
  ]
}
```

## DLL Üretim Adımları

1. SQL Server'a bağlan
2. SP imzalarını oku (parametre tipleri)
3. Wrapper class oluştur
4. Compile ve DLL üret
5. `/artifacts/spbuilder/dll/` altına kaydet

## Util.DataAccess Entegrasyonu

Üretilen DLL'ler `Util.DataAccess` projesine referans olarak eklenir:

```xml
<Reference Include="Common.Sp">
  <HintPath>..\..\artifacts\spbuilder\dll\Common.Sp.dll</HintPath>
</Reference>
```

## Versiyon Yönetimi

DLL'ler semantic versioning ile numaralandırılır. SP imzası değiştiğinde yeni versiyon üretilir.
