# Stored Procedure Kontratları

## Genel Kurallar

- Tüm SP'ler `dbo` şemasında
- Prefix: `sp_[modul]_[islem]`
- SET NOCOUNT ON her SP'de
- OUTPUT parametreler yeni kayıt ID'leri için

---

## Common

### sp_kul_ekran_getir
```sql
@ekran_kod NVARCHAR(50)
RETURNS: ekran_id, ekran_kod, menudeki_adi, form_adi, modul, aktif
```

### sp_kul_ekran_versiyon_logla
```sql
@ekran_kod NVARCHAR(50)
@versiyon NVARCHAR(20)
@kullanici_id INT = NULL
```

---

## Urun

### sp_urun_kaydet
```sql
@urun_id INT = NULL OUTPUT  -- NULL=yeni, değer=güncelle
@urun_kod NVARCHAR(50)
@urun_adi NVARCHAR(200)
@kategori_id INT = NULL
@birim_id INT = NULL
@alis_fiyati DECIMAL(18,2) = NULL
@satis_fiyati DECIMAL(18,2) = NULL
@barkod NVARCHAR(50) = NULL
@aciklama NVARCHAR(MAX) = NULL
```

### sp_urun_getir
```sql
@urun_id INT
RETURNS: urun detayları + kategori_adi, birim_adi
```

### sp_urun_listele
```sql
@aktif BIT = 1
@kategori_id INT = NULL
@arama NVARCHAR(100) = NULL
RETURNS: ürün listesi
```

### sp_urun_pasifle
```sql
@urun_id INT
```

---

## Stok

### sp_stok_hareket_ekle
```sql
@urun_id INT
@hareket_tip NVARCHAR(20)  -- GIRIS, CIKIS, SAYIM, DUZELTME
@miktar DECIMAL(18,2)
@referans_tip NVARCHAR(50) = NULL
@referans_id INT = NULL
@aciklama NVARCHAR(500) = NULL
@kullanici_id INT = NULL
```

### sp_stok_durum_getir
```sql
@urun_id INT
RETURNS: mevcut_stok DECIMAL(18,2)
```

### sp_stok_kritik_listele
```sql
RETURNS: urun_id, urun_adi, mevcut_stok, min_stok
```

---

## Siparis

### sp_siparis_taslak_olustur
```sql
@tedarikci_id INT
@kullanici_id INT = NULL
@siparis_id INT OUTPUT
```

### sp_siparis_satir_ekle
```sql
@siparis_id INT
@urun_id INT
@miktar DECIMAL(18,2)
@birim_fiyat DECIMAL(18,2)
```

### sp_siparis_listele
```sql
@durum NVARCHAR(20) = NULL
RETURNS: sipariş listesi + tedarikci_adi
```

### sp_siparis_durum_guncelle
```sql
@siparis_id INT
@durum NVARCHAR(20)  -- TASLAK, BEKLIYOR, TESLIMAT, TAMAMLANDI, IPTAL
```

---

## Bildirim

### sp_bildirim_ekle
```sql
@bildirim_tip NVARCHAR(50)  -- STOK_KRITIK, SIPARIS_GELEN, AI_ONAY_BEKLIYOR
@baslik NVARCHAR(200)
@icerik NVARCHAR(MAX) = NULL
@referans_tip NVARCHAR(50) = NULL
@referans_id INT = NULL
@kullanici_id INT = NULL
```

### sp_bildirim_listele
```sql
@kullanici_id INT = NULL
@okundu BIT = NULL
RETURNS: bildirim listesi
```

### sp_bildirim_okundu
```sql
@bildirim_id INT
```

---

## AI

### sp_ai_icerik_getir
```sql
@urun_id INT
RETURNS: aktif içerik
```

### sp_ai_icerik_kaydet
```sql
@urun_id INT
@icerik NVARCHAR(MAX)
@durum NVARCHAR(20) = 'TASLAK'
@kaynaklar NVARCHAR(MAX) = NULL
@sablon_kod NVARCHAR(50) = NULL
@provider NVARCHAR(50) = NULL
@kullanici_id INT = NULL
@icerik_id INT OUTPUT
```

### sp_ai_icerik_onayla
```sql
@icerik_id INT
@onaylayan_kullanici_id INT = NULL
```

---

## Auth (Sprint 7)

### sp_kullanici_getir_login
```sql
@kullanici_adi NVARCHAR(50)
-- OR --
@kullanici_id INT
RETURNS: kullanici_id, kullanici_adi, ad_soyad, email, parola_hash, parola_salt, parola_iterasyon, aktif, son_giris_tarih
NOTE: Returns hash/salt for PBKDF2 verification, only active users
```

### sp_kullanici_son_giris_guncelle
```sql
@kullanici_id INT
NOTE: Updates son_giris_tarih to GETDATE(), called after successful login
```

---

## Security - User Management (Sprint 7)

### sp_kullanici_kaydet
```sql
@kullanici_id INT = NULL OUTPUT  -- NULL=create, value=update
@kullanici_adi NVARCHAR(50)
@ad_soyad NVARCHAR(100)
@email NVARCHAR(100) = NULL
@parola_hash NVARCHAR(512) = NULL       -- Required for INSERT, optional for UPDATE
@parola_salt NVARCHAR(256) = NULL       -- Required if parola_hash provided
@parola_iterasyon INT = 10000
@aktif BIT = 1
@created_by INT = NULL
@modified_by INT = NULL
NOTE: Validates unique kullanici_adi, email format, writes audit log (CREATE/UPDATE)
```

### sp_kullanici_listele
```sql
@aktif BIT = NULL  -- NULL=all, 1=active, 0=inactive
@arama NVARCHAR(100) = NULL  -- Search in kullanici_adi, ad_soyad, email
RETURNS: kullanici_id, kullanici_adi, ad_soyad, email, aktif, son_giris_tarih, roller (comma-separated)
NOTE: Uses STUFF+FOR XML PATH for roller column
```

### sp_kullanici_getir
```sql
@kullanici_id INT
RETURNS: kullanici_id, kullanici_adi, ad_soyad, email, aktif, son_giris_tarih, created_at, created_by, modified_at, modified_by
NOTE: Does NOT return parola_hash/salt for security
```

### sp_kullanici_pasifle
```sql
@kullanici_id INT
@modified_by INT = NULL
NOTE: Sets aktif=0, BLOCKS deletion of 'admin' user, writes audit log (DELETE)
```

### sp_kullanici_parola_guncelle
```sql
@kullanici_id INT
@eski_parola_hash NVARCHAR(512)     -- For verification
@yeni_parola_hash NVARCHAR(512)
@yeni_parola_salt NVARCHAR(256)
@parola_iterasyon INT = 10000
NOTE: User changes own password, requires old password verification, writes audit log (PASSWORD_CHANGE)
```

### sp_kullanici_parola_sifirla
```sql
@kullanici_id INT
@yeni_parola_hash NVARCHAR(512)
@yeni_parola_salt NVARCHAR(256)
@parola_iterasyon INT = 10000
@reset_by INT  -- Admin user ID
NOTE: Admin resets forgotten password, no old password required, writes audit log (PASSWORD_RESET)
```

---

## Security - Role Management (Sprint 7)

### sp_rol_kaydet
```sql
@rol_id INT = NULL OUTPUT
@rol_kod NVARCHAR(50)
@rol_adi NVARCHAR(100)
@aciklama NVARCHAR(500) = NULL
@aktif BIT = 1
@created_by INT = NULL
@modified_by INT = NULL
NOTE: Validates unique rol_kod, writes audit log (CREATE/UPDATE)
```

### sp_rol_listele
```sql
@aktif BIT = NULL
RETURNS: rol_id, rol_kod, rol_adi, aciklama, aktif, kullanici_sayisi, yetki_sayisi
NOTE: Includes counts via subqueries
```

### sp_rol_getir
```sql
@rol_id INT
RETURNS: rol_id, rol_kod, rol_adi, aciklama, aktif, created_at, created_by, modified_at, modified_by
```

### sp_rol_pasifle
```sql
@rol_id INT
@modified_by INT = NULL
NOTE: Sets aktif=0, BLOCKS deletion of 'ADMIN' role, writes audit log (DELETE)
```

---

## Security - Permission Management (Sprint 7)

### sp_yetki_listele
```sql
@modul NVARCHAR(50) = NULL  -- Filter by module (Template, Settings, etc.)
RETURNS: yetki_id, yetki_kod, yetki_adi, aciklama, modul
NOTE: Read-only, permissions managed via seed scripts
```

### sp_yetki_getir
```sql
@yetki_id INT
RETURNS: yetki_id, yetki_kod, yetki_adi, aciklama, modul
```

---

## Security - Assignments (Sprint 7)

### sp_kullanici_rol_ekle
```sql
@kullanici_id INT
@rol_id INT
@created_by INT = NULL
NOTE: Assigns role to user, idempotent (ignores duplicates), writes audit log (ASSIGN_ROLE)
```

### sp_kullanici_rol_sil
```sql
@kullanici_id INT
@rol_id INT
@modified_by INT = NULL
NOTE: Removes role from user, writes audit log (REVOKE_ROLE)
```

### sp_kullanici_rol_listele
```sql
@kullanici_id INT
RETURNS: rol_id, rol_kod, rol_adi, aktif (JOIN to rol table for names)
```

### sp_rol_yetki_ekle
```sql
@rol_id INT
@yetki_id INT
@created_by INT = NULL
NOTE: Assigns permission to role, idempotent, writes audit log (ASSIGN_PERMISSION)
```

### sp_rol_yetki_sil
```sql
@rol_id INT
@yetki_id INT
@modified_by INT = NULL
NOTE: Removes permission from role, writes audit log (REVOKE_PERMISSION)
```

### sp_rol_yetki_listele
```sql
@rol_id INT
RETURNS: yetki_id, yetki_kod, yetki_adi, modul (JOIN to yetki table)
```

### sp_ekran_yetki_ekle
```sql
@ekran_kod NVARCHAR(50)
@yetki_id INT
NOTE: Maps permission to screen, idempotent (used in seed scripts)
```

### sp_ekran_yetki_sil
```sql
@ekran_kod NVARCHAR(50)
@yetki_id INT
NOTE: Removes permission from screen mapping
```

### sp_ekran_yetki_listele
```sql
@ekran_kod NVARCHAR(50)
RETURNS: yetki_id, yetki_kod, yetki_adi (permissions required for this screen)
```

---

## Security - Effective Permissions (Sprint 7) **CRITICAL**

### sp_kullanici_yetki_listele
```sql
@kullanici_id INT
RETURNS: DISTINCT yetki_id, yetki_kod, yetki_adi, aciklama, modul
NOTE: Returns union of all permissions from user's active roles
      PERFORMANCE TARGET: <100ms
      Used by UI for permission caching at session start
```

### sp_kullanici_yetki_kontrol
```sql
@kullanici_id INT
@yetki_kod NVARCHAR(50)
RETURNS: Scalar 1 (has permission) or 0 (no permission)
NOTE: PERFORMANCE TARGET: <50ms
      Uses EXISTS for first-match optimization
      Called before every screen open
```

---

## Audit (Sprint 7)

### sp_audit_listele
```sql
@entity NVARCHAR(100) = NULL      -- Filter by table name
@action NVARCHAR(50) = NULL       -- Filter by action (CREATE, UPDATE, DELETE, etc.)
@kullanici_id INT = NULL          -- Filter by user
@baslangic_tarih DATETIME = NULL  -- Date range start
@bitis_tarih DATETIME = NULL      -- Date range end
@top INT = 1000                   -- Limit results (default 1000)
RETURNS: audit_id, entity, entity_id, action, created_at, created_by, kullanici_adi, ad_soyad
NOTE: LEFT JOIN to kullanici for user info, ORDER BY created_at DESC
```

### sp_audit_getir
```sql
@audit_id INT
RETURNS: audit_id, entity, entity_id, action, detail_json, created_at, created_by, kullanici_adi, ad_soyad
NOTE: Includes detail_json for full audit detail display
```
