# SqlManager Kullanım Kılavuzu

## İçindekiler
1. [Genel Bakış](#genel-bakış)
2. [Connection String Yönetimi](#connection-string-yönetimi)
3. [Temel Kullanım Patternleri](#temel-kullanım-patternleri)
4. [Transaction Yönetimi](#transaction-yönetimi)
5. [Parametre Tipleri ve Mapping](#parametre-tipleri-ve-mapping)
6. [DataTable'dan Model Mapping](#datatableden-model-mapping)
7. [Hata Yönetimi](#hata-yönetimi)
8. [Best Practices](#best-practices)

---

## Genel Bakış

**SqlManager** sınıfı, SQL Server veritabanı bağlantısını ve işlemlerini yönetir.

### Temel Kurallar

- **ZORUNLU:** `using(sMan)` standardı ile kullanılır (IDisposable pattern)
- **YASAK:** Class-level SqlManager tutmak (stateless pattern)
- **ZORUNLU:** Her metotta try/catch ile hata yönetimi
- **ZORUNLU:** Stored Procedure (SP) kontratına uygun parametre kullanımı

### Namespace

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Util.DataAccess;
```

---

## Connection String Yönetimi

### App.config Yapısı

Connection string **zorunlu olarak** `App.config` dosyasında `Db` adıyla tanımlanmalıdır:

```xml
<configuration>
  <connectionStrings>
    <add name="Db"
         connectionString="Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

### Environment Stratejisi

| Environment | Server | Authentication | Örnek Connection String |
|-------------|--------|----------------|------------------------|
| **DEV** | localhost | Windows Auth | `Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;` |
| **STAGE** | Test sunucusu | SQL Auth | `Server=TEST_SERVER;Database=AktarOtomasyon;User Id=aktar_test;Password=***;TrustServerCertificate=True;` |
| **PROD** | Production sunucusu | SQL Auth | `Server=PROD_SERVER;Database=AktarOtomasyon;User Id=aktar_user;Password=***;TrustServerCertificate=True;` |

### Bağlantı Testi

```csharp
using (var sMan = new SqlManager())
{
    var hata = sMan.TestConnection();
    if (hata != null)
    {
        throw new InvalidOperationException($"Veritabanı bağlantısı başarısız: {hata}");
    }
}
```

---

## Temel Kullanım Patternleri

### 1. ExecuteNonQuery (INSERT/UPDATE/DELETE)

**Kullanım Amacı:** Veri değişikliği yapan işlemler (DML).

**Örnek: Ekran Log Yazma**

```csharp
public string VersiyonLogla(string ekranKod, string versiyon, int? kullaniciId = null)
{
    try
    {
        if (string.IsNullOrWhiteSpace(ekranKod))
            return "Ekran kodu boş olamaz.";

        if (string.IsNullOrWhiteSpace(versiyon))
            return "Versiyon bilgisi boş olamaz.";

        using (var sMan = new SqlManager())
        {
            var cmd = sMan.CreateCommand("sp_kul_ekran_versiyon_logla", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50)
            {
                Value = ekranKod
            });

            cmd.Parameters.Add(new SqlParameter("@versiyon", SqlDbType.NVarChar, 20)
            {
                Value = versiyon
            });

            cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
            {
                Value = (object)kullaniciId ?? DBNull.Value
            });

            sMan.ExecuteNonQuery(cmd);

            return null; // başarı
        }
    }
    catch (SqlException sqlEx)
    {
        return $"Veritabanı hatası: {sqlEx.Number} - {sqlEx.Message}";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

**Dönüş Değeri:** `int` (etkilenen satır sayısı)

---

### 2. ExecuteQuery (SELECT - DataTable)

**Kullanım Amacı:** Veri okuma işlemleri (SELECT).

**Örnek: Ekran Bilgisi Getir**

```csharp
public KulEkranModel EkranGetir(string ekranKod)
{
    try
    {
        if (string.IsNullOrWhiteSpace(ekranKod))
            return null;

        using (var sMan = new SqlManager())
        {
            var cmd = sMan.CreateCommand("sp_kul_ekran_getir", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50)
            {
                Value = ekranKod
            });

            var dt = sMan.ExecuteQuery(cmd);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new KulEkranModel
            {
                EkranId = Convert.ToInt32(row["ekran_id"]),
                EkranKod = row["ekran_kod"]?.ToString(),
                MenudekiAdi = row["menudeki_adi"]?.ToString(),
                FormAdi = row["form_adi"]?.ToString(),
                Modul = row["modul"]?.ToString(),
                Aciklama = row["aciklama"]?.ToString(),
                Aktif = Convert.ToBoolean(row["aktif"])
            };
        }
    }
    catch (SqlException)
    {
        return null;
    }
    catch (Exception)
    {
        return null;
    }
}
```

**Dönüş Değeri:** `DataTable` (sonuç seti)

---

### 3. ExecuteScalar (Tek Değer)

**Kullanım Amacı:** COUNT, MAX, SUM gibi tek değer dönen sorgular.

**Örnek: Ürün Sayısı**

```csharp
public int UrunSayisiGetir(int kategoriId)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            var cmd = sMan.CreateCommand("sp_urun_sayisi_getir", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int)
            {
                Value = kategoriId
            });

            var result = sMan.ExecuteScalar(cmd);

            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
    catch
    {
        return 0;
    }
}
```

**Dönüş Değeri:** `object` (ilk satır, ilk kolon değeri)

---

### 4. OUTPUT Parametre Kullanımı

**Kullanım Amacı:** SP'den değer döndürme (örn: identity ID).

**Örnek: Yeni Ürün Ekle ve ID Al**

```csharp
public int UrunEkle(string urunAdi, decimal fiyat)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            var cmd = sMan.CreateCommand("sp_urun_ekle", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@urun_adi", SqlDbType.NVarChar, 100)
            {
                Value = urunAdi
            });

            cmd.Parameters.Add(new SqlParameter("@fiyat", SqlDbType.Decimal)
            {
                Value = fiyat
            });

            var outputParam = new SqlParameter("@yeni_urun_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            sMan.ExecuteNonQuery(cmd);

            return Convert.ToInt32(outputParam.Value);
        }
    }
    catch
    {
        return 0;
    }
}
```

**SP Tanımı:**

```sql
CREATE PROCEDURE sp_urun_ekle
    @urun_adi NVARCHAR(100),
    @fiyat DECIMAL(18,2),
    @yeni_urun_id INT OUTPUT
AS
BEGIN
    INSERT INTO urun (urun_adi, fiyat, aktif)
    VALUES (@urun_adi, @fiyat, 1);

    SET @yeni_urun_id = SCOPE_IDENTITY();
END
```

---

## Transaction Yönetimi

### Transaction Kuralları

- **Tek SP çağrısı:** Transaction otomatik (SP içinde `BEGIN TRANSACTION/COMMIT`)
- **Çoklu SP çağrısı:** `SqlManager.BeginTransaction()` kullan
- **Dispose pattern:** Commit edilmemiş transaction otomatik rollback olur
- **Nested transaction:** Desteklenmez (hata fırlatır)

### Transaction API

```csharp
// Transaction başlat
sMan.BeginTransaction();

// Transaction commit et (kalıcı hale getir)
sMan.CommitTransaction();

// Transaction geri al (rollback)
sMan.RollbackTransaction();
```

---

### Örnek 1: Tek SP (Transaction Gerekmez)

```csharp
public string UrunKaydet(UrunModel model)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            var cmd = sMan.CreateCommand("sp_urun_kaydet", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
            {
                Value = model.UrunId
            });

            cmd.Parameters.Add(new SqlParameter("@urun_adi", SqlDbType.NVarChar, 100)
            {
                Value = model.UrunAdi
            });

            sMan.ExecuteNonQuery(cmd);

            return null; // başarı
        }
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

---

### Örnek 2: Çoklu SP (Transaction Gerekli)

**Senaryo:** Sipariş oluştur + stok düş + log yaz (hepsi birden başarılı olmalı veya hepsi geri alınmalı).

```csharp
public string SiparisOlustur(SiparisModel siparis, List<SiparisKalemModel> kalemler)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            sMan.BeginTransaction();

            try
            {
                // 1. Sipariş master kayıt
                var cmdSiparis = sMan.CreateCommand("sp_siparis_ekle", CommandType.StoredProcedure);
                cmdSiparis.Parameters.Add(new SqlParameter("@musteri_id", SqlDbType.Int)
                {
                    Value = siparis.MusteriId
                });

                var outputSiparisId = new SqlParameter("@siparis_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmdSiparis.Parameters.Add(outputSiparisId);

                sMan.ExecuteNonQuery(cmdSiparis);

                int siparisId = Convert.ToInt32(outputSiparisId.Value);

                // 2. Sipariş kalemleri ekle + stok düş
                foreach (var kalem in kalemler)
                {
                    var cmdKalem = sMan.CreateCommand("sp_siparis_kalem_ekle", CommandType.StoredProcedure);
                    cmdKalem.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                    {
                        Value = siparisId
                    });
                    cmdKalem.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                    {
                        Value = kalem.UrunId
                    });
                    cmdKalem.Parameters.Add(new SqlParameter("@miktar", SqlDbType.Decimal)
                    {
                        Value = kalem.Miktar
                    });

                    sMan.ExecuteNonQuery(cmdKalem);
                }

                // 3. Log yaz
                var cmdLog = sMan.CreateCommand("sp_siparis_log_yaz", CommandType.StoredProcedure);
                cmdLog.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                {
                    Value = siparisId
                });
                cmdLog.Parameters.Add(new SqlParameter("@islem", SqlDbType.NVarChar, 50)
                {
                    Value = "YENI_SIPARIS"
                });

                sMan.ExecuteNonQuery(cmdLog);

                // Tüm işlemler başarılı - commit et
                sMan.CommitTransaction();

                return null; // başarı
            }
            catch
            {
                // Hata durumunda rollback
                sMan.RollbackTransaction();
                throw;
            }
        }
    }
    catch (SqlException sqlEx)
    {
        return $"Veritabanı hatası: {sqlEx.Number} - {sqlEx.Message}";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

---

### Örnek 3: Dispose ile Otomatik Rollback

```csharp
public string TransactionOrnegi()
{
    try
    {
        using (var sMan = new SqlManager())
        {
            sMan.BeginTransaction();

            var cmd1 = sMan.CreateCommand("sp_islem1", CommandType.StoredProcedure);
            sMan.ExecuteNonQuery(cmd1);

            var cmd2 = sMan.CreateCommand("sp_islem2", CommandType.StoredProcedure);
            sMan.ExecuteNonQuery(cmd2);

            // Eğer burada exception fırlatılırsa, Dispose metodu otomatik rollback yapar
            throw new Exception("Test hatası");

            sMan.CommitTransaction(); // Bu satıra asla ulaşılmaz

            return null;
        }
        // using bloğu bitince Dispose çağrılır ve transaction rollback olur
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

---

## Parametre Tipleri ve Mapping

### SQL Server - .NET Tip Karşılıkları

| SQL Server | .NET Type | SqlDbType | Örnek |
|------------|-----------|-----------|-------|
| INT | `int` | `SqlDbType.Int` | `42` |
| BIGINT | `long` | `SqlDbType.BigInt` | `9223372036854775807L` |
| SMALLINT | `short` | `SqlDbType.SmallInt` | `32767` |
| TINYINT | `byte` | `SqlDbType.TinyInt` | `255` |
| BIT | `bool` | `SqlDbType.Bit` | `true` |
| DECIMAL(18,2) | `decimal` | `SqlDbType.Decimal` | `123.45M` |
| FLOAT | `double` | `SqlDbType.Float` | `123.45` |
| NVARCHAR(50) | `string` | `SqlDbType.NVarChar, 50` | `"Değer"` |
| VARCHAR(50) | `string` | `SqlDbType.VarChar, 50` | `"Value"` |
| DATETIME | `DateTime` | `SqlDbType.DateTime` | `DateTime.Now` |
| DATETIME2 | `DateTime` | `SqlDbType.DateTime2` | `DateTime.Now` |
| UNIQUEIDENTIFIER | `Guid` | `SqlDbType.UniqueIdentifier` | `Guid.NewGuid()` |

---

### Nullable Parametre Kullanımı

**KURAL:** Nullable değerler için `DBNull.Value` kullanılmalıdır.

#### ✓ DOĞRU

```csharp
cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
{
    Value = (object)kullaniciId ?? DBNull.Value
});
```

#### ✗ YANLIŞ

```csharp
// YANLIŞ: null direkt gönderilmemeli
cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
{
    Value = kullaniciId  // nullable int varsa hata fırlatır
});
```

---

### String Parametre Uzunluk Belirtme

**KURAL:** NVARCHAR/VARCHAR parametrelerinde mutlaka uzunluk belirtilmelidir.

#### ✓ DOĞRU

```csharp
cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50)
{
    Value = ekranKod
});
```

#### ✗ YANLIŞ

```csharp
// YANLIŞ: Uzunluk belirtilmemiş (default 1 byte olur!)
cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar)
{
    Value = ekranKod
});
```

---

## DataTable'dan Model Mapping

### Null-Safe Mapping

**KURAL:** DataRow'dan değer okurken her zaman null kontrolü yapılmalıdır.

#### ✓ DOĞRU

```csharp
var row = dt.Rows[0];

return new KulEkranModel
{
    EkranId = Convert.ToInt32(row["ekran_id"]),  // NOT NULL kolon
    EkranKod = row["ekran_kod"]?.ToString(),     // Nullable kolon
    MenudekiAdi = row["menudeki_adi"]?.ToString(),
    FormAdi = row["form_adi"]?.ToString(),
    Modul = row["modul"]?.ToString(),
    Aciklama = row["aciklama"]?.ToString(),
    Aktif = Convert.ToBoolean(row["aktif"])      // NOT NULL kolon
};
```

#### ✗ YANLIŞ

```csharp
// YANLIŞ: Null kontrolü yok, nullable kolonlarda exception fırlatabilir
return new KulEkranModel
{
    EkranKod = row["ekran_kod"].ToString(),  // DBNull ise hata verir!
    MenudekiAdi = row["menudeki_adi"].ToString()
};
```

---

### Liste Mapping

```csharp
public List<UrunModel> UrunListesiGetir(int kategoriId)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            var cmd = sMan.CreateCommand("sp_urun_liste_getir", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int)
            {
                Value = kategoriId
            });

            var dt = sMan.ExecuteQuery(cmd);

            var liste = new List<UrunModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    liste.Add(new UrunModel
                    {
                        UrunId = Convert.ToInt32(row["urun_id"]),
                        UrunAdi = row["urun_adi"]?.ToString(),
                        Fiyat = Convert.ToDecimal(row["fiyat"]),
                        Aktif = Convert.ToBoolean(row["aktif"])
                    });
                }
            }

            return liste;
        }
    }
    catch
    {
        return new List<UrunModel>(); // Boş liste döner (null değil)
    }
}
```

---

## Hata Yönetimi

### Hata Sözleşmesi

#### Service Metot Dönüş Tipleri

- **String dönen metotlar (DML):**
  - `null` = başarı
  - `string` = hata mesajı

- **Model dönen metotlar (Query):**
  - `Model` = bulundu
  - `null` = bulunamadı veya hata

- **Liste dönen metotlar (Query):**
  - `List<Model>` = sonuçlar (boş liste = veri yok)
  - **Asla null dönmeyin!**

---

### SqlException vs Exception

**KURAL:** SqlException ayrı yakalanarak daha detaylı hata mesajı verilebilir.

```csharp
try
{
    using (var sMan = new SqlManager())
    {
        // DB işlemleri
        return null; // başarı
    }
}
catch (SqlException sqlEx)
{
    // SQL Server hataları (connection, constraint, timeout vb.)
    return $"Veritabanı hatası: {sqlEx.Number} - {sqlEx.Message}";
}
catch (Exception ex)
{
    // Diğer hatalar (mapping, null reference vb.)
    return ex.Message;
}
```

---

### Önemli SQL Error Number'lar

| Error Number | Açıklama | Örnek Mesaj |
|--------------|----------|-------------|
| 2627 | PRIMARY KEY ihlali | `Duplicate key violates unique constraint` |
| 547 | FOREIGN KEY ihlali | `The INSERT statement conflicted with the FOREIGN KEY constraint` |
| -2 | Timeout | `Timeout expired. The timeout period elapsed prior to completion of the operation` |
| 1205 | Deadlock | `Transaction was deadlocked on lock resources` |
| 18456 | Login failed | `Login failed for user` |

**Kullanım Örneği:**

```csharp
catch (SqlException sqlEx)
{
    switch (sqlEx.Number)
    {
        case 2627:
            return "Bu kayıt zaten mevcut.";
        case 547:
            return "İlişkili kayıtlar var, silinemez.";
        case -2:
            return "İşlem zaman aşımına uğradı. Lütfen tekrar deneyin.";
        default:
            return $"Veritabanı hatası: {sqlEx.Number} - {sqlEx.Message}";
    }
}
```

---

## Best Practices

### ✓ DOĞRU Kullanımlar

#### 1. using Pattern (Zorunlu)

```csharp
public string IslemYap()
{
    using (var sMan = new SqlManager())
    {
        // DB işlemleri
        return null; // başarı
    }
    // Dispose otomatik çağrılır, connection kapanır
}
```

#### 2. Parameterized Queries (SQL Injection Koruması)

```csharp
cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50)
{
    Value = ekranKod
});
```

#### 3. Null-Safe DataRow Okuma

```csharp
EkranKod = row["ekran_kod"]?.ToString(),
```

#### 4. Transaction İçin try/catch/rollback

```csharp
sMan.BeginTransaction();
try
{
    // İşlemler
    sMan.CommitTransaction();
    return null;
}
catch
{
    sMan.RollbackTransaction();
    throw;
}
```

---

### ✗ YANLIŞ Kullanımlar

#### 1. Class-Level SqlManager (Stateless İhlali)

```csharp
// YANLIŞ: Thread-safe değil, resource leak riski
public class UrunService
{
    private SqlManager _sMan = new SqlManager(); // YASAK!

    public void IslemYap()
    {
        _sMan.ExecuteNonQuery(...);
    }
}
```

#### 2. String Concatenation ile SQL (SQL Injection Riski)

```csharp
// YANLIŞ: SQL Injection açığı!
var cmd = sMan.CreateCommand(
    $"SELECT * FROM urun WHERE urun_adi = '{urunAdi}'",  // YASAK!
    CommandType.Text
);
```

#### 3. Nullable Parametre için null Gönderme

```csharp
// YANLIŞ: Nullable int için DBNull.Value kullanılmalı
cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
{
    Value = kullaniciId  // kullaniciId = null ise hata!
});
```

#### 4. DataRow'dan Null Kontrolsüz Okuma

```csharp
// YANLIŞ: DBNull ise exception fırlatır
EkranKod = row["ekran_kod"].ToString()  // YASAK!
```

#### 5. Liste Metotlarında null Dönme

```csharp
// YANLIŞ: null dönmek yerine boş liste dönülmeli
public List<UrunModel> UrunListesi()
{
    try
    {
        // ...
        return liste;
    }
    catch
    {
        return null; // YANLIŞ! return new List<UrunModel>() olmalı
    }
}
```

#### 6. Transaction Olmadan Çoklu SP

```csharp
// YANLIŞ: Transaction yok, yarıda kalan işlem riski
using (var sMan = new SqlManager())
{
    sMan.ExecuteNonQuery(cmd1); // Başarılı
    sMan.ExecuteNonQuery(cmd2); // HATA! (cmd1 geri alınmaz!)
}
```

---

## Özet Kontrol Listesi

Sprint 1 backend implementasyonu için SqlManager kullanımı kontrol listesi:

- [ ] `using(sMan)` standardı uygulandı mı?
- [ ] Transaction gerekli yerde kullanıldı mı?
- [ ] Parametreler `SqlParameter` ile eklendi mi?
- [ ] String parametrelerinde uzunluk belirtildi mi?
- [ ] Nullable parametreler `DBNull.Value` ile gönderildi mi?
- [ ] DataRow okuma null-safe mi?
- [ ] SqlException ayrı yakalandı mı?
- [ ] Hata sözleşmesi uygulandı mı (null=başarı, string=hata)?
- [ ] Liste metotları boş liste döndürüyor mu (null değil)?
- [ ] Class-level SqlManager kullanılmadı mı? (stateless)

---

## Referanslar

- **Dokümanlar:**
  - `docs/decisions.md` - Teknik kararlar
  - `docs/standards.md` - Kodlama standartları
  - `docs/sp-contract.md` - SP katalog ve kontratlar

- **Kod Örnekleri:**
  - `src/AktarOtomasyon.Common.Service/KulEkranService.cs` - Referans implementasyon

- **MSDN:**
  - [SqlConnection Class](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection)
  - [SqlCommand Class](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand)
  - [SqlParameter Class](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlparameter)
  - [SqlTransaction Class](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqltransaction)

---

**Son Güncelleme:** Sprint 1 Backend - Transaction desteği eklendi
**Yazar:** AktarOtomasyon Backend Ekibi
