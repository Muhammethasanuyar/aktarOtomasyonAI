# SQL Server Connection Test Script
Write-Host "=== SQL Server Baglanti Testi ===" -ForegroundColor Cyan
Write-Host ""

# Check SQL Server services
Write-Host "1. SQL Server Servisleri Kontrol Ediliyor..." -ForegroundColor Yellow
$sqlServices = Get-Service -Name "*SQL*" | Where-Object { $_.Status -eq "Running" }
if ($sqlServices) {
    Write-Host "   [OK] SQL Server servisleri calisiyor" -ForegroundColor Green
} else {
    Write-Host "   [HATA] SQL Server servisleri calismiyor!" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Find instance name
Write-Host "2. SQL Server Instance Adi Bulunuyor..." -ForegroundColor Yellow
$instanceName = "MSSQLSERVER"
$expressInstance = Get-Service -Name "MSSQL`$SQLEXPRESS" -ErrorAction SilentlyContinue
if ($expressInstance) {
    $instanceName = "SQLEXPRESS"
    $serverName = "localhost\SQLEXPRESS"
    Write-Host "   [OK] SQL Server Express bulundu: $instanceName" -ForegroundColor Green
} else {
    $serverName = "localhost"
    Write-Host "   [OK] Standart SQL Server bulundu: $instanceName" -ForegroundColor Green
}
Write-Host ""

# Test connection strings
Write-Host "3. Connection String Test Ediliyor..." -ForegroundColor Yellow
$connStr1 = "Server=$serverName;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;"
$connStr2 = "Server=$serverName;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"

$success = $false
$testStrings = @($connStr1, $connStr2)

foreach ($connStr in $testStrings) {
    $shortStr = $connStr.Substring(0, [Math]::Min(60, $connStr.Length))
    Write-Host "   Test ediliyor: $shortStr..." -ForegroundColor Gray
    try {
        $connection = New-Object System.Data.SqlClient.SqlConnection($connStr)
        $connection.Open()
        Write-Host "   [BASARILI] Bu connection string calisiyor!" -ForegroundColor Green
        Write-Host "   Connection String: $connStr" -ForegroundColor Cyan
        $connection.Close()
        $success = $true
        break
    } catch {
        Write-Host "   [BASARISIZ] $($_.Exception.Message)" -ForegroundColor Red
    }
}
Write-Host ""

# Port check
Write-Host "4. Port Kontrolu..." -ForegroundColor Yellow
try {
    $tcpTest = Test-NetConnection -ComputerName localhost -Port 1433 -WarningAction SilentlyContinue
    if ($tcpTest.TcpTestSucceeded) {
        Write-Host "   [OK] Port 1433 acik ve erisilebilir" -ForegroundColor Green
    } else {
        Write-Host "   [UYARI] Port 1433 kapali veya erisilemiyor" -ForegroundColor Yellow
    }
} catch {
    Write-Host "   [UYARI] Port testi yapilamadi" -ForegroundColor Yellow
}
Write-Host ""

# Result
Write-Host "=== SONUC ===" -ForegroundColor Cyan
if ($success) {
    Write-Host "[BASARILI] Baglanti basarili! App.config dosyanizi guncelleyin:" -ForegroundColor Green
    Write-Host ""
    Write-Host '<add name="Db"' -ForegroundColor White
    Write-Host "     connectionString=`"Server=$serverName;Database=AktarOtomasyon;Trusted_Connection=True;TrustServerCertificate=True;`"" -ForegroundColor White
    Write-Host '     providerName="System.Data.SqlClient" />' -ForegroundColor White
} else {
    Write-Host "[HATA] Baglanti basarisiz! Asagidaki adimlari izleyin:" -ForegroundColor Red
    Write-Host ""
    Write-Host "1. SQL Server Configuration Manager'i acin" -ForegroundColor Yellow
    Write-Host "2. SQL Server Network Configuration > Protocols for $instanceName" -ForegroundColor Yellow
    Write-Host "3. Named Pipes ve TCP/IP protokollerini Enabled yapin" -ForegroundColor Yellow
    Write-Host "4. TCP/IP > Properties > IP Addresses > IPAll > TCP Port = 1433" -ForegroundColor Yellow
    Write-Host "5. SQL Server servisini yeniden baslatin" -ForegroundColor Yellow
}
