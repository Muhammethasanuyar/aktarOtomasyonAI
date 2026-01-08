# Sprint 7 Backend - Kurulum ve Test Rehberi

## 🎯 Genel Bakış

Sprint 7 Backend, güvenlik ve kimlik doğrulama sistemini içerir:
- PBKDF2 şifre hashleme
- Kullanıcı, Rol, Yetki yönetimi
- Audit logging (denetim kayıtları)

---

## 📋 Veritabanı Kurulum Adımları

### 1. Schema Oluşturma

SQL Server Management Studio'da sırasıyla şu scriptleri çalıştırın:

```sql
-- Adım 1: Tabloları oluştur
db/schema/008_sprint7_security_schema.sql
```

### 2. Stored Procedure'leri Oluşturma

```sql
-- Auth stored procedures
db/sp/Auth/sp_kullanici_getir_login.sql
db/sp/Auth/sp_kullanici_son_giris_guncelle.sql
db/sp/Auth/sp_kullanici_parola_guncelle.sql
db/sp/Auth/sp_kullanici_parola_sifirla.sql

-- Security stored procedures
db/sp/Security/sp_kullanici_kaydet.sql
db/sp/Security/sp_kullanici_listele.sql
db/sp/Security/sp_kullanici_getir.sql
db/sp/Security/sp_kullanici_pasifle.sql

db/sp/Security/sp_rol_kaydet.sql
db/sp/Security/sp_rol_listele.sql
db/sp/Security/sp_rol_getir.sql
db/sp/Security/sp_rol_pasifle.sql

db/sp/Security/sp_yetki_listele.sql
db/sp/Security/sp_yetki_getir.sql

db/sp/Security/sp_kullanici_rol_ekle.sql
db/sp/Security/sp_kullanici_rol_sil.sql
db/sp/Security/sp_kullanici_rol_listele.sql

db/sp/Security/sp_rol_yetki_ekle.sql
db/sp/Security/sp_rol_yetki_sil.sql
db/sp/Security/sp_rol_yetki_listele.sql

db/sp/Security/sp_ekran_yetki_ekle.sql
db/sp/Security/sp_ekran_yetki_sil.sql
db/sp/Security/sp_ekran_yetki_listele.sql

db/sp/Security/sp_kullanici_yetki_listele.sql
db/sp/Security/sp_kullanici_yetki_kontrol.sql

-- Audit stored procedures
db/sp/Audit/sp_audit_listele.sql
db/sp/Audit/sp_audit_getir.sql
```

### 3. Seed Data (İlk Veriler)

```sql
-- Adım 3: Admin kullanıcı ve rolleri oluştur
db/seed/008_sprint7_security_seed.sql
```

Bu script şunları oluşturur:
- ✅ 4 yetki (TEMPLATE_VIEW, TEMPLATE_MANAGE, TEMPLATE_APPROVE, SETTINGS_MANAGE)
- ✅ 1 rol (ADMIN - tüm yetkilere sahip)
- ✅ 1 kullanıcı (admin/Admin123!)
- ✅ Ekran-yetki eşleştirmeleri

---

## 🔐 Giriş Bilgileri

**Varsayılan Admin Hesabı:**

```
Kullanıcı Adı: admin
Şifre: Admin123!
```

### ⚠️ GÜVENLİK UYARISI

1. **İLK GİRİŞTE MUTLAKA ŞİFRE DEĞİŞTİRİN!**
2. Şifre değiştirmek için "Hesabım > Şifre Değiştir" menüsünü kullanın
3. Yeni şifre en az 8 karakter olmalıdır

---

## 🧪 Manuel Test Adımları

### Test 1: Login Testi

```sql
-- Test: Admin kullanıcısını kontrol et
SELECT
    kullanici_id,
    kullanici_adi,
    ad_soyad,
    email,
    aktif,
    parola_iterasyon
FROM kullanici
WHERE kullanici_adi = 'admin'

-- Beklenen: 1 kayıt, aktif = 1, parola_iterasyon = 10000
```

### Test 2: Rol ve Yetki Kontrolü

```sql
-- Test: Admin rolünün yetkilerini kontrol et
SELECT
    r.rol_adi,
    COUNT(ry.yetki_id) as yetki_sayisi
FROM rol r
LEFT JOIN rol_yetki ry ON r.rol_id = ry.rol_id
WHERE r.rol_adi = 'ADMIN'
GROUP BY r.rol_adi

-- Beklenen: ADMIN rolü, en az 4 yetki
```

### Test 3: Stored Procedure Testi

```sql
-- Test: Login SP'yi çalıştır (sadece veri kontrolü, şifre doğrulamaz)
EXEC sp_kullanici_getir_login @kullanici_adi = 'admin'

-- Beklenen: Admin kullanıcı bilgileri (parola_hash ve parola_salt dolu olmalı)
```

### Test 4: Kullanıcı Yetkilerini Kontrol Et

```sql
-- Test: Admin kullanıcısının yetkilerini listele
DECLARE @admin_id INT
SELECT @admin_id = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin'

EXEC sp_kullanici_yetki_listele @kullanici_id = @admin_id

-- Beklenen: Tüm yetkiler listelenmelidir (TEMPLATE_VIEW, TEMPLATE_MANAGE, vb.)
```

### Test 5: Yetki Kontrolü

```sql
-- Test: Admin kullanıcısının TEMPLATE_VIEW yetkisi var mı?
DECLARE @admin_id INT
SELECT @admin_id = kullanici_id FROM kullanici WHERE kullanici_adi = 'admin'

EXEC sp_kullanici_yetki_kontrol
    @kullanici_id = @admin_id,
    @yetki_kod = 'TEMPLATE_VIEW'

-- Beklenen: 1 (yetki var)
```

---

## 🚀 Uygulama Testi

### Frontend'den Giriş Yapma

1. Uygulamayı çalıştırın
2. Login ekranında:
   - Kullanıcı Adı: `admin`
   - Şifre: `Admin123!`
3. "Giriş Yap" butonuna tıklayın

**Beklenen Sonuç:**
- ✅ Giriş başarılı olmalı
- ✅ Ana ekran açılmalı
- ✅ Kullanıcı adı ve rol bilgisi görünmeli

### Şifre Değiştirme Testi

1. Ana menüden "Hesabım > Şifre Değiştir"
2. Mevcut Şifre: `Admin123!`
3. Yeni Şifre: `YeniSifre123!`
4. Şifre Tekrar: `YeniSifre123!`
5. "Kaydet" butonuna tıklayın

**Beklenen Sonuç:**
- ✅ Şifre başarıyla değiştirilmeli
- ✅ Uygulamadan çıkış yapın
- ✅ Yeni şifre ile giriş yapabilmelisiniz

---

## 🔧 Sorun Giderme

### Hata: "Kullanıcı adı veya parola hatalı"

**Çözüm:**
1. Seed script'inin çalıştırıldığından emin olun
2. Kullanıcı adını kontrol edin (büyük/küçük harf duyarlı değil)
3. Şifreyi kontrol edin: `Admin123!` (büyük/küçük harf duyarlı!)

### Hata: "Stored procedure bulunamadı"

**Çözüm:**
1. Tüm SP dosyalarının çalıştırıldığından emin olun
2. SP'lerin doğru veritabanında olduğunu kontrol edin:

```sql
-- SP'leri kontrol et
SELECT name FROM sys.procedures
WHERE name LIKE 'sp_kullanici%' OR name LIKE 'sp_rol%'
ORDER BY name

-- Beklenen: En az 23 stored procedure
```

### Hata: "Kullanıcı hesabı pasif durumda"

**Çözüm:**
```sql
-- Admin kullanıcısını aktif et
UPDATE kullanici
SET aktif = 1
WHERE kullanici_adi = 'admin'
```

---

## 📊 Veritabanı Yapısı

### Temel Tablolar

| Tablo | Açıklama |
|-------|----------|
| `kullanici` | Kullanıcı bilgileri (PBKDF2 hash ile) |
| `rol` | Roller (ADMIN, MANAGER, vb.) |
| `yetki` | Yetkiler (TEMPLATE_VIEW, vb.) |
| `kullanici_rol` | Kullanıcı-Rol ilişkileri |
| `rol_yetki` | Rol-Yetki ilişkileri |
| `ekran_yetki` | Ekran-Yetki ilişkileri |
| `audit_log` | Denetim kayıtları |

### Güvenlik Özellikleri

1. **PBKDF2 Password Hashing**
   - 10,000 iterasyon
   - 32-byte salt (rastgele)
   - 32-byte hash
   - Base64 encoding

2. **Role-Based Access Control (RBAC)**
   - Kullanıcılar → Roller → Yetkiler → Ekranlar
   - Çoklu rol desteği
   - Dinamik yetki kontrolü

3. **Audit Logging**
   - Tüm CUD işlemleri loglanır
   - Kullanıcı, tarih, değişiklik detayları
   - JSON formatında detay

---

## 🎓 Yeni Kullanıcı Ekleme

### SQL ile Kullanıcı Ekleme

```sql
-- Örnek: Yeni bir kullanıcı ekle
-- NOT: Şifre hash'i elle eklenemez, PasswordHelper kullanın!

-- Adım 1: tools/Generate-AdminHash.ps1 scriptini çalıştırın
-- Şifre: "YeniKullanici123!"
-- Bu size hash ve salt verecektir

-- Adım 2: Kullanıcıyı ekleyin
INSERT INTO kullanici
    (kullanici_adi, ad_soyad, email, parola_hash, parola_salt, parola_iterasyon, aktif, created_at)
VALUES
    ('yenikullanici', 'Yeni Kullanıcı', 'yeni@aktar.local',
     '<BURAYA_HASH>', '<BURAYA_SALT>', 10000, 1, GETDATE())

-- Adım 3: Rol atayın
EXEC sp_kullanici_rol_ekle
    @kullanici_id = <KULLANICI_ID>,
    @rol_id = <ROL_ID>,
    @created_by = 1  -- admin kullanıcısı
```

### Uygulama Üzerinden Kullanıcı Ekleme

1. Admin olarak giriş yapın
2. "Güvenlik > Kullanıcı Yönetimi" menüsü
3. "Yeni Kullanıcı" butonu
4. Formu doldurun
5. Rol(ler) seçin
6. "Kaydet"

---

## ✅ Kontrol Listesi

Kurulum tamamlandıktan sonra şunları doğrulayın:

- [ ] Schema oluşturuldu (tüm tablolar var)
- [ ] Stored procedure'ler oluşturuldu (23+ SP)
- [ ] Seed data çalıştırıldı
- [ ] Admin kullanıcısı var ve aktif
- [ ] Admin rolü var ve 4+ yetkisi var
- [ ] Admin kullanıcısı ADMIN rolüne atanmış
- [ ] Login yapılabiliyor
- [ ] Şifre değiştirme çalışıyor
- [ ] Audit loglar oluşuyor

---

## 📞 Destek

Sorun yaşarsanız:
1. Bu dokümanı baştan sona okuyun
2. Sorun Giderme bölümünü kontrol edin
3. SQL test scriptlerini çalıştırın
4. Hata mesajlarını not edin

---

**Son Güncelleme:** 2025-12-26
**Sprint:** Sprint 7 Backend
**Durum:** ✅ Tamamlandı
