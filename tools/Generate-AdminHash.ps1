# PowerShell script to generate PBKDF2 hash for admin password
# Uses the same algorithm as PasswordHelper.cs

Write-Host "==========================================="
Write-Host "Admin Password Hash Generator (PBKDF2)"
Write-Host "==========================================="
Write-Host ""

$password = "Admin123!"
$saltSize = 32
$hashSize = 32
$iterations = 10000

Write-Host "Generating hash for password: $password"
Write-Host ""

# Generate random salt
$rng = New-Object System.Security.Cryptography.RNGCryptoServiceProvider
$saltBytes = New-Object byte[] $saltSize
$rng.GetBytes($saltBytes)

# Generate PBKDF2 hash
$pbkdf2 = New-Object System.Security.Cryptography.Rfc2898DeriveBytes($password, $saltBytes, $iterations)
$hashBytes = $pbkdf2.GetBytes($hashSize)

# Convert to Base64
$salt = [Convert]::ToBase64String($saltBytes)
$hash = [Convert]::ToBase64String($hashBytes)

Write-Host "RESULTS:"
Write-Host "--------"
Write-Host ""
Write-Host "Salt (Base64):"
Write-Host $salt
Write-Host ""
Write-Host "Hash (Base64):"
Write-Host $hash
Write-Host ""
Write-Host "==========================================="
Write-Host "UPDATE SEED FILE"
Write-Host "==========================================="
Write-Host ""
Write-Host "Copy these values to: db/seed/008_sprint7_security_seed.sql"
Write-Host ""
Write-Host "DECLARE @sample_salt NVARCHAR(256) = '$salt'"
Write-Host "DECLARE @sample_hash NVARCHAR(512) = '$hash'"
Write-Host ""
Write-Host "==========================================="
