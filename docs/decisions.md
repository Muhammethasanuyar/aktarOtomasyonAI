# Teknik Kararlar ve Mimari Seçimler

## 1. Framework ve Teknoloji Seçimleri

### 1.1 UI Framework
- **Karar**: DevExpress WinForms v25.1
- **Gerekçe**:
  - Zengin bileşen kütüphanesi (Grid, Chart, Report)
  - Türkçe desteği
  - Offline çalışma yeteneği (masaüstü)
  - Performans (local data binding)
- **Alternatifler**: WPF, UWP, Blazor Hybrid
- **Risk**: Lisans maliyeti, versiyon bağımlılığı

### 1.2 .NET Framework
- **Karar**: .NET Framework 4.8
- **Gerekçe**:
  - Windows masaüstü için en stabil seçenek
  - DevExpress uyumluluğu
  - SQL Server entegrasyonu
- **Alternatifler**: .NET 6/7/8 (cross-platform)
- **Not**: Gelecekte .NET 8 migration değerlendirilebilir

### 1.3 Veri Erişim Teknolojisi
- **Karar**: ADO.NET + Stored Procedure
- **Gerekçe**:
  - Performans (compiled SP'ler)
  - Güvenlik (SQL injection koruması)
  - Transaction yönetimi
  - Veritabanı iş mantığı merkezi yönetim
- **Alternatifler**: Entity Framework, Dapper
- **Not**: ORM overhead'inden kaçınıldı

## 2. Veritabanı Stratejisi

### 2.1 Veritabanı Sunucusu
- **Karar**: SQL Server 2019 Express (dev), SQL Server Standard (prod)
- **Connection Mode**: Windows Authentication (dev), SQL Auth (prod)
- **Encoding**: Turkish_CI_AS collation

### 2.2 Connection String Yönetimi
```xml
<!-- DEV -->
<add name="Db"
     connectionString="Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;"
     providerName="System.Data.SqlClient" />

<!-- PROD -->
<add name="Db"
     connectionString="Server=PROD_SERVER;Database=AktarOtomasyon;User Id=aktar_user;Password=***;TrustServerCertificate=True;"
     providerName="System.Data.SqlClient" />
```

### 2.3 Transaction Politikası
- **Karar**: Service katmanında transaction yönetimi
- **Kural**:
  - Tek SP çağrısı = transaction otomatik (SP içinde)
  - Çoklu SP çağrısı = SqlManager.BeginTransaction() kullan
  - Dispose pattern ile otomatik rollback (hata durumunda)
- **Örnek**:
```csharp
using (var sMan = new SqlManager())
{
    sMan.BeginTransaction();
    try
    {
        // SP1 çağrısı
        // SP2 çağrısı
        sMan.CommitTransaction();
        return null; // başarı
    }
    catch
    {
        sMan.RollbackTransaction();
        throw;
    }
}
```

## 3. Hata Yönetimi ve Loglama

### 3.1 Hata Sözleşmesi (Error Contract)
- **Service Return**: `string` (null=başarı, mesaj=hata)
- **Interface Query**: Model (null=bulunamadı) veya List (boş=veri yok)
- **Exception Handling**: Her service metodu try/catch ile sarmalı

**Standart Pattern:**
```csharp
public string IslemYap()
{
    try
    {
        using (var sMan = new SqlManager())
        {
            // İş mantığı
            return null; // Başarı
        }
    }
    catch (SqlException sqlEx)
    {
        // SQL hataları için özel mesaj
        return $"Veritabanı hatası: {sqlEx.Number} - {sqlEx.Message}";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}
```

### 3.2 Loglama Stratejisi
- **Karar**: Veritabanı tabanlı loglama
- **Tablolar**:
  - `kul_ekran_log` (ekran açılış logları)
  - `audit_log` (veri değişiklik logları)
- **Log Seviyeleri**: INFO, WARNING, ERROR, CRITICAL
- **Saklama**: 90 gün (otomatik arşivleme)

### 3.3 UI Katmanı Hata Gösterimi
- **Karar**: DMLManager utility sınıfı
- **Standart**:
```csharp
var hata = _service.Kaydet(model);
if (!DMLManager.KaydetKontrol(hata))
    return; // Hata mesajı otomatik gösterildi
```

## 4. Configuration Yönetimi

### 4.1 Environment Stratejisi
- **DEV**: localhost, Trusted_Connection, debug enabled
- **STAGE**: Test sunucusu, SQL Auth, minimal logging
- **PROD**: Production sunucusu, SQL Auth, full audit

### 4.2 App.config Yapısı
```xml
<appSettings>
  <add key="Environment" value="DEV" /> <!-- DEV, STAGE, PROD -->
  <add key="AppVersion" value="1.0.0" />
  <add key="AI_PROVIDER" value="GEMINI" />
  <add key="AI_TIMEOUT_SECONDS" value="30" />
  <add key="AI_MAX_RETRY" value="3" />
</appSettings>
```

### 4.3 Deployment Config Override
- **Strateji**: Build configuration ile App.config transform
- **Tool**: SlowCheetah (XML transform)
- **Örnek**: App.Stage.config, App.Production.config

## 5. Service Katmanı Kuralları

### 5.1 Stateless Design
- **YASAK**: Class-level SqlManager veya state değişkenleri
- **ZORUNLU**: using(sMan) pattern her metodda
- **Gerekçe**: Thread-safety, resource leak önleme

### 5.2 Interface Segregation
- **Kural**: Her modül kendi Interface projesine sahip
- **Forms**: SADECE Interface projelerine referans verir
- **Service**: Interface + DataAccess referansları

## 6. Naming Conventions

| Tip | Kural | Örnek |
|-----|-------|-------|
| Stored Procedure | sp_[modul]_[islem] | sp_kul_ekran_getir |
| Tablo | snake_case | kul_ekran, urun_kategori |
| Interface | I[Modül]Interface | IKulEkranInterface |
| Service | [Modül]Service | KulEkranService |
| Model | [Tablo]Model (PascalCase) | KulEkranModel |
| Form | Frm[Modül][Tip] | FrmUrunListe |
| UserControl | Uc[Modül][Tip] | UcUrunKart |

## 7. Güvenlik

### 7.1 SQL Injection Koruması
- **Karar**: Parameterized queries (SqlParameter)
- **YASAK**: String concatenation ile SQL oluşturma

### 7.2 Şifre Yönetimi (Sprint 7 - Güncellenmiş)
- **Karar**: PBKDF2 password hashing (Rfc2898DeriveBytes)
- **Gerekçe**:
  - NIST-onaylı standart (NIST SP 800-132)
  - .NET Framework native desteği (BCrypt için 3rd party DLL gerekmez)
  - FIPS-140 uyumlu
  - Configurable iteration count (future-proofing)
- **Parametreler**:
  - Algorithm: PBKDF2-HMAC-SHA1
  - Salt Size: 32 bytes (256 bits) - rastgele üretilir
  - Hash Size: 32 bytes (256 bits)
  - Iterations: 10,000 (NIST minimum önerisi)
  - Encoding: Base64
- **Tablo Yapısı**:
  - `kullanici.parola_hash` (NVARCHAR(512)) - Base64 encoded hash
  - `kullanici.parola_salt` (NVARCHAR(256)) - Base64 encoded salt
  - `kullanici.parola_iterasyon` (INT) - Default 10000
- **Güvenlik Özellikleri**:
  - Constant-time comparison (timing attack koruması)
  - Per-password random salt (rainbow table koruması)
  - Generic error messages (user enumeration koruması)
- **Alternatifler**: BCrypt, Argon2, scrypt
- **Risk**: SHA1 deprecation (HMAC-SHA256'ya geçiş planlanabilir)

### 7.3 Yetkilendirme (Sprint 7 - RBAC)
- **Karar**: Role-Based Access Control (RBAC)
- **Model**: Users → Roles → Permissions → Screens
- **Tablolar**:
  - `kullanici` - Kullanıcı bilgileri
  - `rol` - Roller (ADMIN, USER, MANAGER, vb.)
  - `yetki` - İzinler (TEMPLATE_VIEW, TEMPLATE_MANAGE, vb.)
  - `kullanici_rol` - User-Role atamaları (N-N)
  - `rol_yetki` - Role-Permission atamaları (N-N)
  - `ekran_yetki` - Screen-Permission gereksinimleri (N-N)
- **Effective Permissions**: Kullanıcının tüm rollerindeki izinlerin birleşimi (UNION)
- **Kontrol Mekanizması**:
  - Her form açılışında `sp_kullanici_yetki_kontrol` çağrısı
  - UI seviyesinde permission caching (session boyunca)
  - Performans hedefi: <50ms per check
- **Permission Modules**: Template, Settings, User, Role, Audit (modüler yapı)
- **Default Role**: ADMIN (tüm izinlere sahip, silinemez)

### 7.4 Authentication Mode (Sprint 7)
- **Karar**: Database-Based Authentication (DbPassword)
- **Gerekçe**:
  - Kullanıcı yönetimi UI'dan yapılabilir
  - LDAP/AD entegrasyonu gerekmez (küçük işletme)
  - Password reset özellikleri kontrol altında
- **Login Flow**:
  1. User enters username + password
  2. `sp_kullanici_getir_login` SP'si hash/salt döner
  3. PBKDF2 verification (PasswordHelper.VerifyPassword)
  4. Başarılı login: son_giris_tarih güncellenir, session açılır
  5. Başarısız login: Generic error message
- **Session Yönetimi**: In-memory (Forms.Common.SessionManager)
- **Alternatifler**: Windows Auth, LDAP, OAuth2
- **Not**: Gelecekte SSO entegrasyonu değerlendirilebilir

### 7.5 Audit Logging (Sprint 7)
- **Karar**: Database-based comprehensive audit logging
- **Tablo**: `audit_log` (entity, entity_id, action, detail_json, created_by, created_at)
- **Logged Actions**:
  - User: CREATE, UPDATE, DELETE, PASSWORD_CHANGE, PASSWORD_RESET
  - Role: CREATE, UPDATE, DELETE
  - Template: CREATE, UPDATE, DELETE, UPLOAD_VERSION, ACTIVATE_VERSION, ARCHIVE_VERSION
  - Settings: UPDATE
  - Assignments: ASSIGN_ROLE, REVOKE_ROLE, ASSIGN_PERMISSION, REVOKE_PERMISSION
- **Detail Storage**: JSON format (esnek schema)
- **Retention**: 365 gün (ayarlanabilir)
- **Viewing**: Read-only UI (AuditService.AuditListele)
- **Indexing**: entity, action, kullanici_id, created_at için composite index

## 8. SPBuilder Entegrasyonu

### 8.1 DLL Üretim Stratejisi
- **Karar**: Build-time generation (not runtime)
- **Manifest**: /artifacts/spbuilder/manifest/spbuilder-manifest.json
- **Output**: /artifacts/spbuilder/dll/[Module].Sp.dll

### 8.2 Versiyon Yönetimi
- **Kural**: SP imzası değişince DLL yeniden üret
- **Semantic Versioning**: MAJOR.MINOR.PATCH

## 9. Sprint 7: Security & Audit Backend Decisions

### 9.1 Authentication Architecture
- **Karar**: PBKDF2 (DbPassword mode)
- **Tarih**: 2024-12 (Sprint 7)
- **Alternatifler Değerlendirmesi**:
  | Seçenek | Avantajlar | Dezavantajlar | Neden Seçilmedi/Seçildi |
  |---------|-----------|---------------|-------------------------|
  | BCrypt | Industry standard, rainbow table koruması | 3rd party DLL, FIPS uyumsuz | Native support yok |
  | **PBKDF2** ✅ | NIST-approved, native .NET, FIPS-140 | SHA1 deprecation riski | .NET Framework uyumu |
  | Argon2 | Modern, memory-hard | 3rd party, .NET Fx desteği zayıf | Framework kısıtı |
  | scrypt | Memory-hard | Standardizasyon eksik | NIST onayı yok |

### 9.2 Screen Permission Mapping Strategy
- **Karar**: ekran_yetki tablosu (N-N mapping)
- **Tarih**: 2024-12 (Sprint 7)
- **Alternatifler Değerlendirmesi**:
  | Seçenek | Avantajlar | Dezavantajlar | Neden Seçilmedi/Seçildi |
  |---------|-----------|---------------|-------------------------|
  | Hardcoded (Code) | Performans | Esneklik yok, deployment gerekli | Yeni ekran için kod değişikliği |
  | kul_ekran tablosuna yetki_kod kolonu | Basit | Tek permission per screen | Multi-permission screens |
  | **ekran_yetki tablosu** ✅ | Esnek, N-N, runtime config | Ekstra tablo | Esneklik öncelikli |
  | Permission code in ekran name | Çok basit | Convention-based, hata riski | Maintainability düşük |

**İmplementasyon Detayları**:
```sql
CREATE TABLE ekran_yetki (
    ekran_yetki_id INT IDENTITY PRIMARY KEY,
    ekran_kod NVARCHAR(50) NOT NULL,  -- FK to kul_ekran
    yetki_id INT NOT NULL,            -- FK to yetki
    UNIQUE (ekran_kod, yetki_id)
)
```

**Kullanım**:
- Template ekranı (TEMPLATE_MRK) → TEMPLATE_VIEW yetkisi gerekir
- Settings ekranı (SYS_SETTINGS) → SETTINGS_MANAGE yetkisi gerekir
- Yeni permission eklemek için sadece INSERT (kod değişikliği yok)

### 9.3 Permission Module Strategy
- **Karar**: Minimum Template/Settings permissions (Sprint 7)
- **Tarih**: 2024-12
- **Gerekçe**:
  - Sprint 7 scope: Security backend + Template/Settings UI
  - Gelecek modüller için extensible catalog
- **İlk Permission Set**:
  | Kod | Modül | Açıklama |
  |-----|-------|----------|
  | TEMPLATE_VIEW | Template | View templates and versions |
  | TEMPLATE_MANAGE | Template | Create, edit, delete templates |
  | TEMPLATE_APPROVE | Template | Activate versions (approval workflow) |
  | SETTINGS_MANAGE | Settings | View and edit system settings |
- **Genişleme Planı**: Her yeni modül için permission'lar seed script ile eklenir

### 9.4 Service Layer Architecture
- **Karar**: 3 ayrı service (Auth, Security, Audit)
- **Tarih**: 2024-12
- **Ayrım Mantığı**:
  - **AuthService**: Login, password operations (stateless, session-free)
  - **SecurityService**: User/Role/Permission CRUD + Assignments
  - **AuditService**: Read-only audit log viewing
- **Interface Segregation**: Her service kendi interface'i (ISP principle)
- **Neden 3 Service?**:
  - SRP (Single Responsibility Principle)
  - Future: AuthService OAuth ile değiştirilebilir
  - Audit service farklı storage'a taşınabilir (MongoDB, etc.)

### 9.5 Password Helper Pattern
- **Karar**: Static helper class (PasswordHelper)
- **Tarih**: 2024-12
- **Gerekçe**:
  - Stateless operations (hash, verify)
  - Testability (mock gerekmez)
  - Reusability (tüm services kullanabilir)
- **Methods**:
  - `HashPassword(password)` → (hash, salt)
  - `VerifyPassword(password, storedHash, storedSalt)` → bool
  - `ConstantTimeCompare` → timing attack koruması
- **Alternatif**: IPasswordHasher interface (overengineering, single impl)

### 9.6 Default Admin Account
- **Karar**: Seed data ile admin/Admin123! hesabı oluştur
- **Tarih**: 2024-12
- **Güvenlik Politikası**:
  - ⚠️ UYARI: İlk deployment sonrası MUTLAKA değiştirilmeli
  - ADMIN rolü silinemez/pasifleştirilemez
  - Admin kullanıcısı pasifleştirilemez
- **Production Checklist**: Default password değişimi zorunlu

### 9.7 Effective Permissions Performance
- **Karar**: Indexed SP with EXISTS clause
- **Tarih**: 2024-12
- **Hedef**: <100ms for yetki_listele, <50ms for yetki_kontrol
- **Optimizasyon**:
  - Composite index: (kullanici_id, rol_id) on kullanici_rol
  - Composite index: (rol_id, yetki_id) on rol_yetki
  - DISTINCT kullanımı (permission duplicates engellenir)
  - EXISTS (first match optimization)
- **UI Caching**: Session boyunca permission list cached (round-trip azaltma)

---

## Karar Sahipleri

| Karar | Sahip | Tarih |
|-------|-------|-------|
| Framework seçimi | Mimari Ekibi | 2024-Q4 |
| Hata sözleşmesi | Backend Lead | 2024-Q4 |
| Transaction stratejisi | DBA + Backend | 2024-Q4 |
| PBKDF2 password hashing | Security Lead | 2024-12 (Sprint 7) |
| RBAC model | Backend + Security | 2024-12 (Sprint 7) |
| ekran_yetki table | Backend Lead | 2024-12 (Sprint 7) |
| Audit logging strategy | DBA + Backend | 2024-12 (Sprint 7) |
