# Troubleshooting Rehberi

## Sık Karşılaşılan Hatalar

### DevExpress License Hatası
**Belirti**: "DevExpress license not found" hatası
**Çözüm**: 
1. DevExpress kurulumunu kontrol et
2. Lisans dosyasının doğru konumda olduğunu doğrula
3. `licenses.licx` dosyasını kontrol et

### Connection String Hatası
**Belirti**: "Connection string 'Db' bulunamadı"
**Çözüm**:
1. `App.config` içinde `<connectionStrings>` bölümünü kontrol et
2. Key adının tam olarak "Db" olduğunu doğrula

### SQL Server Erişim Hatası
**Belirti**: Login failed / Access denied
**Çözüm**:
1. SQL Server çalışıyor mu kontrol et
2. Windows Auth / SQL Auth ayarını kontrol et
3. Kullanıcı yetkilerini kontrol et

### SP Bulunamadı Hatası
**Belirti**: "Could not find stored procedure"
**Çözüm**:
1. SP'nin veritabanında var olduğunu kontrol et
2. SP adının doğru yazıldığını kontrol et
3. Şema adını kontrol et (dbo.)

### SPBuilder DLL Uyumsuzluğu
**Belirti**: Method not found / Parameter mismatch
**Çözüm**:
1. SP imzası değişmiş olabilir - DLL'i yeniden üret
2. DLL versiyonunu kontrol et
3. Referansı güncelle

### Forms'ta Yasak Referans
**Belirti**: Build uyarısı veya mimari ihlali
**Çözüm**:
1. `.csproj` dosyasından Service/DataAccess referanslarını kaldır
2. Sadece Interface projelerini referans al

### kul_ekran Kaydı Yok
**Belirti**: Form başlığı boş
**Çözüm**:
1. Ekran kodunun seed'de var olduğunu kontrol et
2. `kul_ekran` tablosunda `aktif=1` olduğunu kontrol et
