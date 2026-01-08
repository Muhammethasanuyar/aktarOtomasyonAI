# Sprint 7 Backend - Hızlı Başlangıç

## 🚀 3 Adımda Başlayın

### 1️⃣ Veritabanını Kurun

SQL Server Management Studio'da şu scriptleri sırasıyla çalıştırın:

```sql
-- Schema
db/schema/008_sprint7_security_schema.sql

-- Stored Procedures (klasördeki tüm .sql dosyaları)
db/sp/Auth/*.sql
db/sp/Security/*.sql
db/sp/Audit/*.sql

-- İlk Veri
db/seed/008_sprint7_security_seed.sql
```

### 2️⃣ Doğrulayın

```sql
-- Kurulumu doğrula
db/verify/verify_sprint7_setup.sql

-- Tüm kontroller ✓ PASS olmalı
```

### 3️⃣ Giriş Yapın

```
Kullanıcı Adı: admin
Şifre: Admin123!
```

---

## 📚 Detaylı Dokümantasyon

**Tam kurulum rehberi:** `docs/SPRINT7_SETUP.md`

---

## 🔧 Sorun mu Yaşıyorsunuz?

### "Stored procedure bulunamadı" hatası

```sql
-- SP sayısını kontrol edin (23+ olmalı)
SELECT COUNT(*) FROM sys.procedures
WHERE name LIKE 'sp_%'
```

### "Kullanıcı adı veya parola hatalı"

- Şifre: `Admin123!` (büyük A, büyük/küçük harf duyarlı!)
- Seed script çalıştırıldı mı?

### "Tablo bulunamadı" hatası

```sql
-- Tabloları kontrol edin
SELECT name FROM sys.tables
WHERE name IN ('kullanici', 'rol', 'yetki', 'audit_log')
```

---

## 🛠️ Araçlar

### Hash Generator (Yeni şifre için)

```powershell
# PowerShell ile çalıştırın
tools/Generate-AdminHash.ps1
```

Farklı bir şifre kullanmak isterseniz scripti düzenleyip çalıştırın.

---

## ✅ Tamamlanan İşlemler

Sprint 7 Backend implementasyonu tamamlandı:

- ✅ PasswordHelper (PBKDF2) - 10,000 iterasyon
- ✅ AuthService (4 metod) - Login, ChangePassword, ResetPassword, UpdateLastLogin
- ✅ SecurityService (23 metod) - User/Role/Permission yönetimi
- ✅ AuditService (2 metod) - Audit log yönetimi
- ✅ Seed dosyası gerçek hash ile güncellendi
- ✅ Kurulum dokümantasyonu oluşturuldu
- ✅ Doğrulama scripti hazır
- ✅ Build başarılı (0 hata, 2 uyarı)

---

## 🔐 Güvenlik Kontrol Listesi

- [ ] İlk girişte admin şifresini değiştirin
- [ ] Güçlü şifre kullanın (en az 8 karakter)
- [ ] Gereksiz kullanıcıları pasifleştirin
- [ ] Roller ve yetkileri kontrol edin
- [ ] Audit logları periyodik inceleyin

---

**Son Güncelleme:** 2025-12-26
**Durum:** ✅ Production Ready
