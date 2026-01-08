/*
    Update Admin User Password with REAL PBKDF2 Hash
    Password: Admin123!

    This script updates the existing admin user with proper PBKDF2 credentials
*/

USE [AktarOtomasyon]
GO

PRINT '========================================='
PRINT 'Updating admin user password'
PRINT '========================================='
PRINT ''

-- REAL PBKDF2 Hash for "Admin123!"
-- Generated using PasswordHelper with 10,000 iterations
DECLARE @correct_salt NVARCHAR(256) = 'ZcBkoj9TcYsnsnwluLFB/nd1nxRpK8fyXCfdqrk1FC4='
DECLARE @correct_hash NVARCHAR(512) = 'mzb2n7nDDEmIm0e9K7A8/8GY9NwK05kPvu2YrfQZixY='

-- Check current admin user
PRINT 'Current admin user status:'
SELECT
    kullanici_adi,
    parola_hash,
    parola_salt,
    parola_iterasyon,
    aktif
FROM kullanici
WHERE kullanici_adi = 'admin'

PRINT ''
PRINT 'Updating admin password with PBKDF2 hash...'

-- Update admin user with correct hash and salt
UPDATE kullanici
SET
    parola_hash = @correct_hash,
    parola_salt = @correct_salt,
    parola_iterasyon = 10000,
    aktif = 1,
    updated_at = GETDATE()
WHERE kullanici_adi = 'admin'

PRINT '  ✓ Admin password updated'
PRINT ''
PRINT 'New admin user status:'
SELECT
    kullanici_adi,
    parola_hash,
    parola_salt,
    parola_iterasyon,
    aktif
FROM kullanici
WHERE kullanici_adi = 'admin'

PRINT ''
PRINT '========================================='
PRINT 'Admin password update completed!'
PRINT '========================================='
PRINT ''
PRINT 'Login credentials:'
PRINT '  Username: admin'
PRINT '  Password: Admin123!'
PRINT ''
PRINT 'SECURITY WARNING: Change this password after first login!'
PRINT '========================================='

GO
