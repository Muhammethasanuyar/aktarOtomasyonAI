# SSMS Bağlantı Sorunu Çözümü

## Durum
- ✅ PowerShell test scripti başarılı (connection string çalışıyor)
- ❌ SSMS bağlantı hatası: "Named Pipes Provider, error: 40"

## Çözüm Adımları

### 1. SSMS'de Doğru Server Name Kullanın

SSMS'i açtığınızda **Server name** alanına şunu yazın:

```
localhost
```

veya

```
(local)
```

veya

```
.\
```

**ÖNEMLİ:** Instance adı eklemeyin (SQLEXPRESS yoksa)

---

### 2. SQL Server Configuration Manager'da Protokolleri Kontrol Edin

1. **SQL Server Configuration Manager**'ı açın
   - Başlat menüsünde arayın veya
   - `C:\Windows\SysWOW64\SQLServerManagerXX.msc` (XX = versiyon numarası)

2. **SQL Server Network Configuration** > **Protocols for MSSQLSERVER** bölümüne gidin

3. Şu protokollerin **Enabled** durumunda olduğundan emin olun:
   - ✅ **Named Pipes** → **Enabled**
   - ✅ **TCP/IP** → **Enabled**

4. **TCP/IP**'yi sağ tıklayıp **Properties** > **IP Addresses** sekmesinde:
   - **IPAll** bölümüne gidin
   - **TCP Dynamic Ports** boş bırakın veya silin
   - **TCP Port** = **1433** yazın

5. **Named Pipes**'ı sağ tıklayıp **Properties**:
   - **Enabled** = **Yes** olduğundan emin olun
   - **Pipe Name** = `\\.\pipe\SQLLocal\MSSQLSERVER` (varsayılan)

6. **SQL Server servisini yeniden başlatın:**
   ```powershell
   Restart-Service MSSQLSERVER
   ```

---

### 3. SSMS'de Authentication Seçimi

SSMS bağlantı penceresinde:

1. **Server type:** Database Engine
2. **Server name:** `localhost` (veya `(local)`)
3. **Authentication:** 
   - **Windows Authentication** seçin (önerilen)
   - veya **SQL Server Authentication** (sa/şifre ile)

---

### 4. Alternatif: TCP/IP ile Zorunlu Bağlantı

Eğer Named Pipes sorunlu ise, SSMS'de:

1. **Server name** alanına şunu yazın:
   ```
   tcp:localhost,1433
   ```

Bu, TCP/IP protokolünü zorunlu kullanır.

---

### 5. SQL Server Browser Servisini Kontrol Edin

```powershell
Get-Service SQLBrowser
```

Eğer çalışmıyorsa:
```powershell
Start-Service SQLBrowser
Set-Service SQLBrowser -StartupType Automatic
```

---

### 6. Firewall Kontrolü

Windows Firewall'da SQL Server portlarının açık olduğundan emin olun:

```powershell
# Port 1433 kontrolü
Test-NetConnection -ComputerName localhost -Port 1433
```

---

### 7. SSMS'i Yönetici Olarak Çalıştırın

Bazen SSMS'in yönetici haklarıyla çalıştırılması gerekir:

1. SSMS'e sağ tıklayın
2. **Run as administrator** seçin

---

### 8. SSMS Connection String Formatları

SSMS'de farklı formatlar deneyin:

| Format | Server Name |
|--------|-------------|
| Standart | `localhost` |
| Named Instance | `localhost\SQLEXPRESS` |
| TCP/IP | `tcp:localhost,1433` |
| Named Pipes | `np:\\.\pipe\SQLLocal\MSSQLSERVER` |
| Local | `(local)` |
| Dot | `.\` |

---

### 9. Hızlı Test: PowerShell ile SSMS Benzeri Bağlantı

```powershell
# SSMS benzeri bağlantı testi
$serverName = "localhost"
$connectionString = "Server=$serverName;Integrated Security=True;TrustServerCertificate=True;"

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    Write-Host "Bağlantı başarılı!" -ForegroundColor Green
    Write-Host "Server Version: $($connection.ServerVersion)" -ForegroundColor Cyan
    $connection.Close()
} catch {
    Write-Host "Bağlantı hatası: $($_.Exception.Message)" -ForegroundColor Red
}
```

---

## Yaygın Hatalar ve Çözümleri

| Hata | Çözüm |
|------|-------|
| "Named Pipes Provider error 40" | Named Pipes protokolünü etkinleştirin |
| "Server was not found" | Server name'i kontrol edin (localhost kullanın) |
| "Login failed" | Authentication modunu kontrol edin |
| "Timeout expired" | TCP/IP portunu kontrol edin (1433) |
| "Access denied" | SSMS'i yönetici olarak çalıştırın |

---

## Son Kontrol Listesi

- [ ] SQL Server servisi çalışıyor mu? (`Get-Service MSSQLSERVER`)
- [ ] Named Pipes protokolü Enabled mı?
- [ ] TCP/IP protokolü Enabled mı?
- [ ] TCP Port 1433 ayarlı mı?
- [ ] SQL Server servisi yeniden başlatıldı mı?
- [ ] SSMS yönetici olarak çalıştırıldı mı?
- [ ] Server name doğru mu? (`localhost`)

---

## Hala Çalışmıyorsa

1. SQL Server error log'unu kontrol edin:
   ```
   C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Log\ERRORLOG
   ```

2. Windows Event Viewer'da SQL Server hatalarını kontrol edin:
   - Event Viewer > Windows Logs > Application
   - SQL Server kaynaklı hataları arayın

3. SQL Server'ı yeniden yüklemeyi düşünün (son çare)
