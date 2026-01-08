# SQL Server Bağlantı Sorunları Giderme Kılavuzu

## Hata: "Named Pipes Provider, error: 40 - Could not open a connection to SQL Server"

Bu hata genellikle SQL Server'a bağlanılamadığında görülür. Aşağıdaki adımları sırayla kontrol edin.

---

## 1. SQL Server Servisinin Çalışıp Çalışmadığını Kontrol Edin

### PowerShell ile Kontrol:
```powershell
Get-Service -Name "*SQL*" | Select-Object Name, Status, DisplayName
```

### Servisleri Başlatma:
```powershell
# SQL Server servisini başlat
Start-Service MSSQLSERVER

# Veya SQL Server Express için:
Start-Service MSSQL$SQLEXPRESS
```

### Windows Servisleri ile Kontrol:
1. `Win + R` tuşlarına basın
2. `services.msc` yazın ve Enter'a basın
3. Şu servisleri arayın ve **Çalışıyor** durumunda olduklarından emin olun:
   - **SQL Server (MSSQLSERVER)** veya **SQL Server (SQLEXPRESS)**
   - **SQL Server Browser** (isteğe bağlı ama önerilir)

---

## 2. SQL Server Instance Adını Kontrol Edin

Connection string'de doğru instance adını kullanın:

### Standart SQL Server için:
```
Server=localhost;Database=AktarOtomasyon;...
```

### SQL Server Express için:
```
Server=localhost\SQLEXPRESS;Database=AktarOtomasyon;...
```

### Instance adını bulma:
```powershell
Get-Service -Name "*SQL*" | Select-Object Name
```

---

## 3. SQL Server Configuration Manager ile Protokolleri Kontrol Edin

1. **SQL Server Configuration Manager**'ı açın:
   - Başlat menüsünde "SQL Server Configuration Manager" arayın
   - Veya: `C:\Windows\SysWOW64\SQLServerManagerXX.msc` (XX = versiyon numarası)

2. **SQL Server Network Configuration** > **Protocols for MSSQLSERVER** (veya SQLEXPRESS) bölümüne gidin

3. Şu protokollerin **Enabled** durumunda olduğundan emin olun:
   - ✅ **Named Pipes** (Enabled)
   - ✅ **TCP/IP** (Enabled)

4. **TCP/IP**'yi sağ tıklayıp **Properties** > **IP Addresses** sekmesinde:
   - **IPAll** bölümünde **TCP Dynamic Ports** boş olmalı veya bir port numarası olmalı (varsayılan: 1433)
   - **TCP Port** = 1433 (veya başka bir port)

5. Değişikliklerden sonra **SQL Server servisini yeniden başlatın**

---

## 4. Connection String'i Güncelleyin

### App.config Dosyasını Düzenleyin:

**Standart SQL Server için:**
```xml
<add name="Db" 
     connectionString="Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;" 
     providerName="System.Data.SqlClient" />
```

**SQL Server Express için:**
```xml
<add name="Db" 
     connectionString="Server=localhost\SQLEXPRESS;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;" 
     providerName="System.Data.SqlClient" />
```

**Belirli bir port kullanıyorsanız:**
```xml
<add name="Db" 
     connectionString="Server=localhost,1433;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;" 
     providerName="System.Data.SqlClient" />
```

---

## 5. Windows Firewall Kontrolü

SQL Server portunun firewall'da açık olduğundan emin olun:

```powershell
# Port 1433'ü kontrol et
Test-NetConnection -ComputerName localhost -Port 1433
```

Firewall'da port açmak için:
1. Windows Defender Firewall > Advanced Settings
2. Inbound Rules > New Rule
3. Port > TCP > Specific local ports: 1433
4. Allow the connection
5. Finish

---

## 6. SQL Server Authentication Modunu Kontrol Edin

1. SQL Server Management Studio (SSMS) ile bağlanmayı deneyin
2. Server Properties > Security sekmesinde:
   - **SQL Server and Windows Authentication mode** seçili olmalı

---

## 7. Alternatif Connection String Formatları

### TCP/IP ile zorunlu bağlantı:
```
Server=tcp:localhost,1433;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;
```

### Named Pipes ile zorunlu bağlantı:
```
Server=np:\\.\pipe\SQLLocal\SQLEXPRESS;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;
```

### SQL Authentication kullanıyorsanız:
```
Server=localhost;Database=AktarOtomasyon;User Id=sa;Password=YourPassword;TrustServerCertificate=True;
```

---

## 8. Hızlı Test Komutları

### PowerShell ile bağlantı testi:
```powershell
$connectionString = "Server=localhost;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;"
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
try {
    $connection.Open()
    Write-Host "Bağlantı başarılı!" -ForegroundColor Green
    $connection.Close()
} catch {
    Write-Host "Bağlantı hatası: $($_.Exception.Message)" -ForegroundColor Red
}
```

### SQL Server Browser servisini başlatma:
```powershell
Start-Service SQLBrowser
```

---

## 9. Yaygın Sorunlar ve Çözümleri

| Sorun | Çözüm |
|-------|-------|
| "Server was not found" | Instance adını kontrol edin (SQLEXPRESS ekleyin) |
| "Named Pipes Provider error" | Named Pipes protokolünü etkinleştirin |
| "TCP/IP Provider error" | TCP/IP protokolünü etkinleştirin ve portu kontrol edin |
| "Login failed" | Authentication modunu kontrol edin |
| "Timeout expired" | Firewall veya network sorununu kontrol edin |

---

## 10. Son Çare: SQL Server'ı Yeniden Yükleyin

Eğer hiçbir çözüm işe yaramazsa:
1. SQL Server'ı kaldırın
2. SQL Server Configuration Manager'dan tüm instance'ları temizleyin
3. SQL Server'ı yeniden yükleyin
4. Yukarıdaki adımları tekrar uygulayın

---

## Yardımcı Kaynaklar

- [Microsoft SQL Server Connection Troubleshooting](https://docs.microsoft.com/sql/relational-databases/errors-events/troubleshoot-connecting-to-the-sql-server-database-engine)
- [SQL Server Configuration Manager](https://docs.microsoft.com/sql/tools/configuration-manager/sql-server-configuration-manager)
