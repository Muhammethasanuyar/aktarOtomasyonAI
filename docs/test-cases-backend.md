# Backend Test Senaryoları - Sprint 1

## İçindekiler
1. [Test Genel Bakış](#test-genel-bakış)
2. [Test Senaryoları](#test-senaryoları)
3. [Test Checklist](#test-checklist)
4. [Test Raporu Şablonu](#test-raporu-şablonu)
5. [Sorun Giderme](#sorun-giderme)

---

## Test Genel Bakış

### Amaç

Sprint 1 backend implementasyonunun (kul_ekran modülü) doğruluğunu ve kararlılığını doğrulamak.

### Kapsam

- SqlManager bağlantı yönetimi
- KulEkranService.EkranGetir metodu
- KulEkranService.VersiyonLogla metodu
- Hata yönetimi ve validasyon
- Transaction desteği (temel test)

### Kapsam Dışı

- UI testleri (Sprint 1 UI görevlerinde)
- Performans testleri (Sprint 6)
- Entegrasyon testleri (Sprint 7)
- Güvenlik testleri (Sprint 7)

### Test Ortamı

- **OS:** Windows 10/11
- **.NET Framework:** 4.8
- **SQL Server:** 2019 Express (veya üstü)
- **Veritabanı:** AktarOtomasyon
- **Connection String:** `Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;`

### Test Ön Koşulları

1. SQL Server çalışıyor olmalı
2. `AktarOtomasyon` veritabanı oluşturulmuş olmalı
3. `kul_ekran` tablosunda test verisi bulunmalı
4. `kul_ekran_log` tablosu oluşturulmuş olmalı
5. SP'ler deploy edilmiş olmalı:
   - `sp_kul_ekran_getir`
   - `sp_kul_ekran_versiyon_logla`
6. Solution derlenmiş olmalı (no errors)

---

## Test Senaryoları

### TS-001: SqlManager Bağlantı Testi

**Test ID:** TS-001
**Modül:** Util.DataAccess
**Öncelik:** Kritik
**Amaç:** SqlManager'ın veritabanına başarılı şekilde bağlanabildiğini doğrulamak.

#### Ön Koşullar

- SQL Server çalışıyor
- Connection string doğru (`App.config` içinde `Db` key'i var)

#### Test Adımları

1. Visual Studio'da yeni bir Console Application test projesi oluşturun (geçici)
2. `AktarOtomasyon.Util.DataAccess` projesine referans ekleyin
3. Aşağıdaki kodu çalıştırın:

```csharp
using System;
using AktarOtomasyon.Util.DataAccess;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-001: SqlManager Bağlantı Testi Başladı...");

        try
        {
            using (var sMan = new SqlManager())
            {
                var hata = sMan.TestConnection();

                if (hata == null)
                {
                    Console.WriteLine("✓ BAŞARILI: Veritabanı bağlantısı kuruldu.");
                }
                else
                {
                    Console.WriteLine($"✗ BAŞARISIZ: {hata}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ HATA: {ex.Message}");
        }

        Console.WriteLine("\nTesti bitirmek için bir tuşa basın...");
        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

- **PASS:** `✓ BAŞARILI: Veritabanı bağlantısı kuruldu.` mesajı görülür
- Connection açılır ve kapanır (hata yok)

#### Hata Durumları

| Hata Mesajı | Neden | Çözüm |
|-------------|-------|-------|
| `Connection string 'Db' bulunamadı.` | App.config'de connection string yok | App.config'e `Db` key'i ekle |
| `A network-related or instance-specific error` | SQL Server çalışmıyor | SQL Server servisini başlat |
| `Login failed for user` | Authentication hatası | Connection string'de Trusted_Connection=True kullan (dev ortamı için) |
| `Cannot open database` | Veritabanı yok | SSMS'te AktarOtomasyon DB'sini oluştur |

#### Test Sonucu

- [ ] PASS
- [ ] FAIL (Açıklama: _________________)

---

### TS-002: EkranGetir - Başarılı Senaryo

**Test ID:** TS-002
**Modül:** Common.Service.KulEkranService
**Öncelik:** Kritik
**Amaç:** Var olan ekran koduna göre ekran bilgisini başarılı şekilde getirmeyi doğrulamak.

#### Ön Koşullar

- `kul_ekran` tablosunda test verisi var
- `sp_kul_ekran_getir` SP'si deploy edilmiş

**Test verisi kontrolü (SSMS):**

```sql
-- Test için var olan bir ekran kodu seç
SELECT TOP 1 ekran_kod, menudeki_adi, form_adi, modul, aktif
FROM kul_ekran
WHERE aktif = 1;

-- Örnek sonuç: ekran_kod = 'URUN_LISTE'
```

#### Test Adımları

```csharp
using System;
using AktarOtomasyon.Common.Service;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-002: EkranGetir - Başarılı Senaryo");

        var service = new KulEkranService();
        string testEkranKod = "URUN_LISTE"; // Test verisi ekran kodu

        try
        {
            var sonuc = service.EkranGetir(testEkranKod);

            if (sonuc != null)
            {
                Console.WriteLine("✓ BAŞARILI: Ekran bilgisi getirildi.");
                Console.WriteLine($"  Ekran ID: {sonuc.EkranId}");
                Console.WriteLine($"  Ekran Kodu: {sonuc.EkranKod}");
                Console.WriteLine($"  Menüdeki Adı: {sonuc.MenudekiAdi}");
                Console.WriteLine($"  Form Adı: {sonuc.FormAdi}");
                Console.WriteLine($"  Modül: {sonuc.Modul}");
                Console.WriteLine($"  Aktif: {sonuc.Aktif}");
            }
            else
            {
                Console.WriteLine("✗ BAŞARISIZ: Sonuç null döndü.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ HATA: {ex.Message}");
        }

        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

- **PASS:**
  - `sonuc != null`
  - `sonuc.EkranKod == "URUN_LISTE"`
  - `sonuc.MenudekiAdi` dolu
  - `sonuc.FormAdi` dolu
  - `sonuc.Modul` dolu
  - `sonuc.Aktif == true`

#### Hata Durumları

| Durum | Neden | Çözüm |
|-------|-------|-------|
| `sonuc == null` | SP veri döndürmedi | SSMS'te ekran kodu kontrol et |
| Exception: `Invalid column name` | SP kolonları model ile uyumsuz | sp-contract.md kontrol et, model property'lerini doğrula |
| Exception: `Could not find stored procedure` | SP deploy edilmemiş | SSMS'te SP'yi oluştur |

#### Test Sonucu

- [ ] PASS
- [ ] FAIL (Açıklama: _________________)

---

### TS-003: EkranGetir - Null Test (Olmayan Ekran)

**Test ID:** TS-003
**Modül:** Common.Service.KulEkranService
**Öncelik:** Yüksek
**Amaç:** Olmayan ekran kodu için null döndürüldüğünü doğrulamak.

#### Ön Koşullar

- `sp_kul_ekran_getir` SP'si deploy edilmiş

#### Test Adımları

```csharp
using System;
using AktarOtomasyon.Common.Service;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-003: EkranGetir - Null Test");

        var service = new KulEkranService();
        string olmayan_ekran_kod = "OLMAYAN_EKRAN_KODU_12345";

        try
        {
            var sonuc = service.EkranGetir(olmayan_ekran_kod);

            if (sonuc == null)
            {
                Console.WriteLine("✓ BAŞARILI: Olmayan ekran için null döndü.");
            }
            else
            {
                Console.WriteLine("✗ BAŞARISIZ: Olmayan ekran için veri döndü!");
                Console.WriteLine($"  Ekran Kodu: {sonuc.EkranKod}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ HATA: {ex.Message}");
        }

        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

- **PASS:** `sonuc == null`
- Exception fırlatılmamalı (null dönmek yeterli)

#### Test Senaryoları

| Test | Parametre | Beklenen Sonuç |
|------|-----------|----------------|
| 1 | `null` | `sonuc == null` |
| 2 | `""` (boş string) | `sonuc == null` |
| 3 | `"   "` (whitespace) | `sonuc == null` |
| 4 | `"OLMAYAN_KOD"` | `sonuc == null` |

#### Test Sonucu

- [ ] PASS (Tüm senaryolar)
- [ ] FAIL (Açıklama: _________________)

---

### TS-004: VersiyonLogla - Kullanıcısız

**Test ID:** TS-004
**Modül:** Common.Service.KulEkranService
**Öncelik:** Kritik
**Amaç:** Kullanıcı ID olmadan ekran açılış logunun başarılı şekilde yazıldığını doğrulamak.

#### Ön Koşullar

- `kul_ekran_log` tablosu oluşturulmuş
- `sp_kul_ekran_versiyon_logla` SP'si deploy edilmiş

**Tablo kontrolü (SSMS):**

```sql
-- Log tablosu var mı?
SELECT COUNT(*) AS TabloVarMi
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'kul_ekran_log';

-- Var olan log sayısını not et (öncesi)
SELECT COUNT(*) AS OncekiLogSayisi FROM kul_ekran_log;
```

#### Test Adımları

```csharp
using System;
using AktarOtomasyon.Common.Service;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-004: VersiyonLogla - Kullanıcısız");

        var service = new KulEkranService();
        string ekranKod = "URUN_LISTE";
        string versiyon = "1.0.0";

        try
        {
            var hata = service.VersiyonLogla(ekranKod, versiyon, kullaniciId: null);

            if (hata == null)
            {
                Console.WriteLine("✓ BAŞARILI: Log yazıldı.");
                Console.WriteLine("  SSMS'te kontrol et: SELECT TOP 1 * FROM kul_ekran_log ORDER BY log_id DESC");
            }
            else
            {
                Console.WriteLine($"✗ BAŞARISIZ: {hata}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ HATA: {ex.Message}");
        }

        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

- **PASS:**
  - `hata == null`
  - SSMS'te log sayısı 1 arttı

**SSMS Doğrulama:**

```sql
-- Log tablosunda yeni kayıt var mı?
SELECT TOP 1 *
FROM kul_ekran_log
ORDER BY log_id DESC;

-- Beklenen:
-- ekran_kod = 'URUN_LISTE'
-- versiyon = '1.0.0'
-- kullanici_id = NULL
-- olusturma_tarihi = yakın tarih/saat
```

#### Hata Durumları

| Hata Mesajı | Neden | Çözüm |
|-------------|-------|-------|
| `Veritabanı hatası: 547 - ...` | Foreign key ihlali (ekran_kod yok) | kul_ekran tablosunda ekran kodu var mı kontrol et |
| `Could not find stored procedure` | SP yok | SSMS'te SP'yi oluştur |
| `Timeout expired` | SP çok uzun sürdü | SP'yi optimize et veya timeout artır |

#### Test Sonucu

- [ ] PASS
- [ ] FAIL (Açıklama: _________________)

---

### TS-005: VersiyonLogla - Kullanıcılı

**Test ID:** TS-005
**Modül:** Common.Service.KulEkranService
**Öncelik:** Yüksek
**Amaç:** Kullanıcı ID ile birlikte ekran açılış logunun başarılı şekilde yazıldığını doğrulamak.

#### Ön Koşullar

- `kul_ekran_log` tablosu oluşturulmuş
- `sp_kul_ekran_versiyon_logla` SP'si deploy edilmiş
- Test için kullanılacak kullanıcı ID (örn: 1)

#### Test Adımları

```csharp
using System;
using AktarOtomasyon.Common.Service;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-005: VersiyonLogla - Kullanıcılı");

        var service = new KulEkranService();
        string ekranKod = "URUN_LISTE";
        string versiyon = "1.0.0";
        int kullaniciId = 1; // Test kullanıcı ID

        try
        {
            var hata = service.VersiyonLogla(ekranKod, versiyon, kullaniciId);

            if (hata == null)
            {
                Console.WriteLine("✓ BAŞARILI: Log yazıldı (kullanıcılı).");
                Console.WriteLine("  SSMS'te kontrol et: SELECT TOP 1 * FROM kul_ekran_log ORDER BY log_id DESC");
            }
            else
            {
                Console.WriteLine($"✗ BAŞARISIZ: {hata}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ HATA: {ex.Message}");
        }

        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

- **PASS:**
  - `hata == null`
  - SSMS'te log kaydında `kullanici_id = 1` olmalı

**SSMS Doğrulama:**

```sql
-- Kullanıcılı log var mı?
SELECT TOP 1 *
FROM kul_ekran_log
WHERE kullanici_id = 1
ORDER BY log_id DESC;

-- Beklenen:
-- kullanici_id = 1 (NOT NULL)
```

#### Test Sonucu

- [ ] PASS
- [ ] FAIL (Açıklama: _________________)

---

### TS-006: VersiyonLogla - Parametre Validasyonu

**Test ID:** TS-006
**Modül:** Common.Service.KulEkranService
**Öncelik:** Yüksek
**Amaç:** Geçersiz parametrelerle çağrıldığında uygun hata mesajı döndüğünü doğrulamak.

#### Ön Koşullar

- `sp_kul_ekran_versiyon_logla` SP'si deploy edilmiş

#### Test Adımları

```csharp
using System;
using AktarOtomasyon.Common.Service;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-006: VersiyonLogla - Parametre Validasyonu");

        var service = new KulEkranService();

        // Test 1: Boş ekran kodu
        Console.WriteLine("\n--- Test 1: Boş ekran kodu ---");
        var hata1 = service.VersiyonLogla("", "1.0.0", null);
        Console.WriteLine(hata1 == "Ekran kodu boş olamaz."
            ? "✓ PASS: Boş ekran kodu doğru mesaj verdi."
            : $"✗ FAIL: Beklenen mesaj gelmedi. Gelen: {hata1}");

        // Test 2: Null ekran kodu
        Console.WriteLine("\n--- Test 2: Null ekran kodu ---");
        var hata2 = service.VersiyonLogla(null, "1.0.0", null);
        Console.WriteLine(hata2 == "Ekran kodu boş olamaz."
            ? "✓ PASS: Null ekran kodu doğru mesaj verdi."
            : $"✗ FAIL: Beklenen mesaj gelmedi. Gelen: {hata2}");

        // Test 3: Whitespace ekran kodu
        Console.WriteLine("\n--- Test 3: Whitespace ekran kodu ---");
        var hata3 = service.VersiyonLogla("   ", "1.0.0", null);
        Console.WriteLine(hata3 == "Ekran kodu boş olamaz."
            ? "✓ PASS: Whitespace ekran kodu doğru mesaj verdi."
            : $"✗ FAIL: Beklenen mesaj gelmedi. Gelen: {hata3}");

        // Test 4: Boş versiyon
        Console.WriteLine("\n--- Test 4: Boş versiyon ---");
        var hata4 = service.VersiyonLogla("URUN_LISTE", "", null);
        Console.WriteLine(hata4 == "Versiyon bilgisi boş olamaz."
            ? "✓ PASS: Boş versiyon doğru mesaj verdi."
            : $"✗ FAIL: Beklenen mesaj gelmedi. Gelen: {hata4}");

        // Test 5: Null versiyon
        Console.WriteLine("\n--- Test 5: Null versiyon ---");
        var hata5 = service.VersiyonLogla("URUN_LISTE", null, null);
        Console.WriteLine(hata5 == "Versiyon bilgisi boş olamaz."
            ? "✓ PASS: Null versiyon doğru mesaj verdi."
            : $"✗ FAIL: Beklenen mesaj gelmedi. Gelen: {hata5}");

        Console.WriteLine("\nTestler tamamlandı. Bir tuşa basın...");
        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

| Test | Parametre | Beklenen Hata Mesajı |
|------|-----------|----------------------|
| 1 | `ekranKod = ""` | `"Ekran kodu boş olamaz."` |
| 2 | `ekranKod = null` | `"Ekran kodu boş olamaz."` |
| 3 | `ekranKod = "   "` | `"Ekran kodu boş olamaz."` |
| 4 | `versiyon = ""` | `"Versiyon bilgisi boş olamaz."` |
| 5 | `versiyon = null` | `"Versiyon bilgisi boş olamaz."` |

- **PASS:** Tüm testler doğru mesaj veriyor

#### Test Sonucu

- [ ] PASS (Tüm senaryolar)
- [ ] FAIL (Açıklama: _________________)

---

### TS-007: Transaction Test (Temel)

**Test ID:** TS-007
**Modül:** Util.DataAccess.SqlManager
**Öncelik:** Orta
**Amaç:** Transaction desteğinin temel senaryosunu doğrulamak.

**Not:** Bu test Sprint 1'de opsiyoneldir. Gelecek sprint'lerde genişletilebilir.

#### Ön Koşullar

- SqlManager transaction desteği eklendi
- Test için dummy SP'ler (opsiyonel)

#### Test Adımları

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Util.DataAccess;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TS-007: Transaction Test (Temel)");

        // Test 1: BeginTransaction/Commit
        Console.WriteLine("\n--- Test 1: Transaction Commit ---");
        try
        {
            using (var sMan = new SqlManager())
            {
                sMan.BeginTransaction();

                // Basit bir test SP çağrısı (veya dummy INSERT)
                var cmd = sMan.CreateCommand("sp_kul_ekran_versiyon_logla", CommandType.StoredProcedure);
                cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50) { Value = "TEST_TRANSACTION" });
                cmd.Parameters.Add(new SqlParameter("@versiyon", SqlDbType.NVarChar, 20) { Value = "1.0.0" });
                cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = DBNull.Value });

                sMan.ExecuteNonQuery(cmd);

                sMan.CommitTransaction();

                Console.WriteLine("✓ PASS: Transaction commit başarılı.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ FAIL: {ex.Message}");
        }

        // Test 2: BeginTransaction/Rollback
        Console.WriteLine("\n--- Test 2: Transaction Rollback ---");
        try
        {
            using (var sMan = new SqlManager())
            {
                sMan.BeginTransaction();

                var cmd = sMan.CreateCommand("sp_kul_ekran_versiyon_logla", CommandType.StoredProcedure);
                cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50) { Value = "TEST_ROLLBACK" });
                cmd.Parameters.Add(new SqlParameter("@versiyon", SqlDbType.NVarChar, 20) { Value = "1.0.0" });
                cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = DBNull.Value });

                sMan.ExecuteNonQuery(cmd);

                sMan.RollbackTransaction();

                Console.WriteLine("✓ PASS: Transaction rollback başarılı.");
            }

            // SSMS'te kontrol et: TEST_ROLLBACK ekran kodu log tablosunda OLMAMALI
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ FAIL: {ex.Message}");
        }

        // Test 3: Nested Transaction (beklenen: exception)
        Console.WriteLine("\n--- Test 3: Nested Transaction (Beklenen: Hata) ---");
        try
        {
            using (var sMan = new SqlManager())
            {
                sMan.BeginTransaction();
                sMan.BeginTransaction(); // İkinci kez çağrılınca hata vermeli

                Console.WriteLine("✗ FAIL: Nested transaction exception fırlatmadı!");
            }
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message.Contains("Zaten aktif bir transaction var"))
            {
                Console.WriteLine("✓ PASS: Nested transaction engellendi.");
            }
            else
            {
                Console.WriteLine($"✗ FAIL: Beklenmeyen exception: {ex.Message}");
            }
        }

        Console.WriteLine("\nTestler tamamlandı. Bir tuşa basın...");
        Console.ReadKey();
    }
}
```

#### Beklenen Sonuç

- **Test 1:** Transaction commit başarılı, log tablosunda `TEST_TRANSACTION` var
- **Test 2:** Transaction rollback başarılı, log tablosunda `TEST_ROLLBACK` YOK
- **Test 3:** Nested transaction exception fırlattı

**SSMS Doğrulama:**

```sql
-- Test 1: Commit edilen kayıt var mı?
SELECT * FROM kul_ekran_log WHERE ekran_kod = 'TEST_TRANSACTION';
-- Beklenen: 1 kayıt var

-- Test 2: Rollback edilen kayıt yok mu?
SELECT * FROM kul_ekran_log WHERE ekran_kod = 'TEST_ROLLBACK';
-- Beklenen: 0 kayıt
```

#### Test Sonucu

- [ ] PASS (Tüm senaryolar)
- [ ] FAIL (Açıklama: _________________)

---

## Test Checklist

Sprint 1 backend testi tamamlanma kontrol listesi:

### Ön Hazırlık

- [ ] SQL Server çalışıyor
- [ ] AktarOtomasyon veritabanı mevcut
- [ ] kul_ekran tablosunda test verisi var
- [ ] kul_ekran_log tablosu oluşturulmuş
- [ ] SP'ler deploy edilmiş (sp_kul_ekran_getir, sp_kul_ekran_versiyon_logla)
- [ ] Solution derlenmiş (no errors)
- [ ] App.config connection string doğru

### Test Senaryoları

- [ ] TS-001: SqlManager bağlantı testi PASS
- [ ] TS-002: EkranGetir - başarılı senaryo PASS
- [ ] TS-003: EkranGetir - null test PASS
- [ ] TS-004: VersiyonLogla - kullanıcısız PASS
- [ ] TS-005: VersiyonLogla - kullanıcılı PASS
- [ ] TS-006: VersiyonLogla - parametre validasyonu PASS
- [ ] TS-007: Transaction test (opsiyonel) PASS

### Kod Kalitesi

- [ ] Tüm metotlarda try/catch var
- [ ] Hata sözleşmesi uygulanmış (null=başarı, string=hata)
- [ ] using(sMan) standardı uygulanmış
- [ ] Nullable parametreler DBNull.Value ile gönderiliyor
- [ ] DataRow okuma null-safe
- [ ] SqlException ayrı yakalanıyor

---

## Test Raporu Şablonu

### Sprint 1 Backend Test Raporu

**Tarih:** [YYYY-MM-DD]
**Test Eden:** [İsim]
**Ortam:** DEV
**SQL Server Versiyonu:** [örn: 2019 Express]
**.NET Framework Versiyonu:** 4.8

#### Test Özeti

| Test ID | Test Adı | Sonuç | Notlar |
|---------|----------|-------|--------|
| TS-001 | SqlManager Bağlantı Testi | PASS / FAIL | |
| TS-002 | EkranGetir - Başarılı | PASS / FAIL | |
| TS-003 | EkranGetir - Null Test | PASS / FAIL | |
| TS-004 | VersiyonLogla - Kullanıcısız | PASS / FAIL | |
| TS-005 | VersiyonLogla - Kullanıcılı | PASS / FAIL | |
| TS-006 | VersiyonLogla - Validasyon | PASS / FAIL | |
| TS-007 | Transaction Test | PASS / FAIL | (Opsiyonel) |

#### Toplam İstatistikler

- **Toplam Test:** 7
- **PASS:** ___
- **FAIL:** ___
- **Başarı Oranı:** ___%

#### Bulunan Hatalar

| Hata ID | Test ID | Açıklama | Öncelik | Durum |
|---------|---------|----------|---------|-------|
| BUG-001 | TS-XXX | [Açıklama] | Kritik / Yüksek / Orta / Düşük | Açık / Kapalı |

#### Öneriler

- [Öneri 1]
- [Öneri 2]

#### Sonuç

- [ ] Sprint 1 backend testleri BAŞARILI - production'a hazır
- [ ] Kritik hatalar var - production'a hazır DEĞİL

**İmza:** __________________
**Tarih:** __________________

---

## Sorun Giderme

### Sık Karşılaşılan Sorunlar ve Çözümleri

#### 1. Connection String Hatası

**Semptom:**
```
Connection string 'Db' bulunamadı.
```

**Çözüm:**
```xml
<!-- App.config içine ekle -->
<configuration>
  <connectionStrings>
    <add name="Db"
         connectionString="Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

---

#### 2. Stored Procedure Bulunamadı

**Semptom:**
```
Could not find stored procedure 'sp_kul_ekran_getir'.
```

**Çözüm:**

SSMS'te kontrol et:

```sql
-- SP var mı?
SELECT name
FROM sys.procedures
WHERE name = 'sp_kul_ekran_getir';

-- Yoksa oluştur (db/sp/ klasöründen script çalıştır)
```

---

#### 3. Foreign Key İhlali

**Semptom:**
```
Veritabanı hatası: 547 - The INSERT statement conflicted with the FOREIGN KEY constraint
```

**Çözüm:**

```sql
-- Ekran kodu kul_ekran tablosunda var mı?
SELECT * FROM kul_ekran WHERE ekran_kod = 'URUN_LISTE';

-- Yoksa ekle
INSERT INTO kul_ekran (ekran_kod, menudeki_adi, aktif)
VALUES ('URUN_LISTE', 'Ürün Listesi', 1);
```

---

#### 4. Invalid Column Name

**Semptom:**
```
Invalid column name 'form_adi'.
```

**Çözüm:**

- SP kontratını kontrol et (`docs/sp-contract.md`)
- Model property'lerini kontrol et (`KulEkranModel.cs`)
- DataRow mapping'i kontrol et (case-sensitive!)

```csharp
// DOĞRU
FormAdi = row["form_adi"]?.ToString(),

// YANLIŞ
FormAdi = row["FormAdi"]?.ToString(),  // SQL kolonları snake_case!
```

---

#### 5. Timeout Hatası

**Semptom:**
```
Timeout expired. The timeout period elapsed prior to completion of the operation.
```

**Çözüm:**

1. SP'nin performansını kontrol et (SSMS'te execution plan)
2. Connection timeout artır (geçici çözüm):

```xml
<add name="Db"
     connectionString="Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;Connection Timeout=60;"
     providerName="System.Data.SqlClient" />
```

---

#### 6. DBNull Cast Hatası

**Semptom:**
```
Unable to cast object of type 'System.DBNull' to type 'System.String'.
```

**Çözüm:**

Null-safe okuma kullan:

```csharp
// DOĞRU
Aciklama = row["aciklama"]?.ToString(),

// YANLIŞ
Aciklama = row["aciklama"].ToString(),  // DBNull ise hata!
```

---

### Loglama ve Debugging

#### SSMS'te SP Debug

```sql
-- sp_kul_ekran_getir test
EXEC sp_kul_ekran_getir @ekran_kod = 'URUN_LISTE';

-- sp_kul_ekran_versiyon_logla test
EXEC sp_kul_ekran_versiyon_logla
    @ekran_kod = 'URUN_LISTE',
    @versiyon = '1.0.0',
    @kullanici_id = NULL;

-- Log kontrolü
SELECT TOP 10 * FROM kul_ekran_log ORDER BY log_id DESC;
```

#### Visual Studio Debug

1. Breakpoint koy (service metodu içinde)
2. F5 ile debug başlat
3. Watch window'da değişkenleri izle:
   - `cmd.Parameters`
   - `dt` (DataTable)
   - `row` (DataRow)

---

## Referanslar

- **Dokümanlar:**
  - `docs/decisions.md` - Teknik kararlar
  - `docs/standards.md` - Kodlama standartları
  - `docs/sp-contract.md` - SP katalog ve kontratlar
  - `docs/dataaccess.md` - SqlManager kullanım kılavuzu

- **Kod:**
  - `src/AktarOtomasyon.Common.Service/KulEkranService.cs` - Test edilen servis
  - `src/AktarOtomasyon.Util.DataAccess/SqlManager.cs` - DB bağlantı yöneticisi

- **Veritabanı:**
  - `db/sp/sp_kul_ekran_getir.sql` - Ekran getir SP
  - `db/sp/sp_kul_ekran_versiyon_logla.sql` - Versiyon log SP

---

**Son Güncelleme:** Sprint 1 Backend Test Senaryoları
**Yazar:** AktarOtomasyon Test Ekibi
