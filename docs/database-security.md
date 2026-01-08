# Database Security Guide

## Overview

This guide covers the database security setup for AktarOtomasyon, including least-privilege user creation, permission management, and security best practices.

**Sprint**: Sprint 8 Backend
**Last Updated**: 2025-12-26

---

## Table of Contents

1. [Security Architecture](#security-architecture)
2. [Database Users](#database-users)
3. [Setup Instructions](#setup-instructions)
4. [Password Management](#password-management)
5. [Connection String Configuration](#connection-string-configuration)
6. [Testing & Verification](#testing--verification)
7. [Troubleshooting](#troubleshooting)
8. [Security Best Practices](#security-best-practices)

---

## Security Architecture

### Principle of Least Privilege

AktarOtomasyon follows the **principle of least privilege** for database access:

- Each user has only the minimum permissions required for their role
- No user has `sysadmin` or `db_owner` privileges
- Dangerous permissions are explicitly denied (defense in depth)
- Passwords use CHECK_EXPIRATION and CHECK_POLICY

### User Roles

| User | Purpose | Environment |
|------|---------|-------------|
| `aktar_app` | Application runtime user | Production, Staging |
| `aktar_readonly` | Reporting and analytics | Production, Staging, Development |
| `aktar_backup` | Automated backup operations | Production, Staging |

---

## Database Users

### 1. aktar_app (Application User)

**Purpose**: Used by the AktarOtomasyon WinForms application for all runtime operations.

**Permissions**:
- ✅ EXECUTE on all stored procedures
- ✅ SELECT, INSERT, UPDATE, DELETE on all user tables
- ✅ VIEW DEFINITION (for schema introspection)
- ❌ DENIED: ALTER DATABASE, CREATE DATABASE, DROP, CONTROL

**Connection String Example**:
```
Server=localhost;Database=AktarOtomasyon;User Id=aktar_app;Password=YourStrongPassword;TrustServerCertificate=True;
```

**Use Cases**:
- Production application runtime
- Staging environment testing
- Integration testing

---

### 2. aktar_readonly (Read-Only User)

**Purpose**: Used for reporting, analytics, and data exports. Cannot modify data.

**Permissions**:
- ✅ SELECT on all user tables
- ✅ EXECUTE on read-only stored procedures (`sp_*_listele`, `sp_*_getir`)
- ✅ VIEW DEFINITION (for reporting tools)
- ❌ DENIED: INSERT, UPDATE, DELETE, ALTER, CREATE, DROP

**Connection String Example**:
```
Server=localhost;Database=AktarOtomasyon;User Id=aktar_readonly;Password=YourStrongPassword;TrustServerCertificate=True;
```

**Use Cases**:
- Power BI / Excel reporting
- Data analytics and business intelligence
- External read-only API access
- Auditing and compliance reports

---

### 3. aktar_backup (Backup Operator)

**Purpose**: Dedicated user for automated backup operations. Cannot access application data.

**Permissions**:
- ✅ BACKUP DATABASE
- ✅ BACKUP LOG
- ✅ db_backupoperator role membership
- ❌ DENIED: SELECT, INSERT, UPDATE, DELETE, EXECUTE

**Use Cases**:
- Automated SQL Server Agent backup jobs
- PowerShell backup scripts
- Third-party backup software

---

## Setup Instructions

### Prerequisites

- SQL Server 2019+ (or SQL Server 2017 with compatibility level 140+)
- `sysadmin` or `securityadmin` privileges to create logins
- AktarOtomasyon database already created and schema deployed

### Step 1: Create Database Users

Run the user creation script as a SQL Server administrator:

```sql
-- Run this script with sysadmin privileges
sqlcmd -S localhost -E -i db\security\001_create_db_users.sql
```

**What it does**:
- Creates 3 SQL Server logins with strong password policies
- Creates corresponding database users in AktarOtomasyon database
- Sets default database to AktarOtomasyon
- Enables CHECK_EXPIRATION and CHECK_POLICY

**IMPORTANT**: The script uses placeholder passwords. You MUST change them before production deployment.

---

### Step 2: Grant Permissions

Run the permission grant script:

```sql
sqlcmd -S localhost -E -i db\security\002_grant_permissions.sql
```

**What it does**:
- Grants EXECUTE on all stored procedures to `aktar_app`
- Grants table permissions (SELECT/INSERT/UPDATE/DELETE) to `aktar_app`
- Grants SELECT-only permissions to `aktar_readonly`
- Grants EXECUTE on read-only SPs to `aktar_readonly`
- Adds `aktar_backup` to db_backupoperator role
- Explicitly denies dangerous permissions (defense in depth)

---

### Step 3: Verify Permissions

Run the verification script to ensure correct setup:

```sql
sqlcmd -S localhost -E -i db\security\003_verify_permissions.sql
```

**What it checks**:
- All logins exist at server level
- All users exist in AktarOtomasyon database
- aktar_app has EXECUTE and table permissions
- aktar_readonly has SELECT only (no writes)
- aktar_backup has backup permissions only
- Dangerous permissions are denied

**Expected output**:
```
✓ aktar_app login exists
✓ aktar_app user exists
✓ aktar_app has EXECUTE permission
• aktar_app has permissions on 25 tables
✓ aktar_app has VIEW DEFINITION
```

---

## Password Management

### Password Requirements

All database passwords MUST meet these requirements:

- **Minimum length**: 16 characters
- **Complexity**: Mixed case, numbers, and symbols
- **No common patterns**: No dictionary words, sequential characters, or dates
- **Unique**: Different password for each environment (Dev/Staging/Prod)

**Example strong passwords**:
```
AktarApp2025!Secure#Pass
ReadOnly$2025@View#Data
Backup!Store#Safe$2025
```

### Changing Passwords

Use SQL Server Management Studio or T-SQL:

```sql
-- Change aktar_app password
ALTER LOGIN aktar_app WITH PASSWORD = 'NewStrongPassword123!'

-- Change aktar_readonly password
ALTER LOGIN aktar_readonly WITH PASSWORD = 'NewStrongPassword123!'

-- Change aktar_backup password
ALTER LOGIN aktar_backup WITH PASSWORD = 'NewStrongPassword123!'
```

### Password Rotation Policy

- **Frequency**: Change passwords every 90 days (quarterly)
- **Process**:
  1. Generate new strong password
  2. Test new password on staging environment first
  3. Update production password during maintenance window
  4. Update .env file or Key Vault immediately
  5. Restart application to pick up new credentials
  6. Verify connectivity
  7. Document password change in audit log

### Password Storage

**NEVER** store passwords in:
- ❌ Source control (Git, SVN, etc.)
- ❌ App.config files
- ❌ Plain text files
- ❌ Email or chat messages
- ❌ Documentation or wiki pages

**DO** store passwords in:
- ✅ Azure Key Vault (recommended for production)
- ✅ .env file (local development only, gitignored)
- ✅ Enterprise password manager (1Password, LastPass, etc.)
- ✅ Environment variables (production servers)

---

## Connection String Configuration

### Development Environment

Use `.env` file for local development:

```env
# .env file (gitignored)
DB_SERVER=localhost
DB_NAME=AktarOtomasyon
DB_USER=aktar_app
DB_PASSWORD=YourDevPassword123!
DB_TRUSTED_CONNECTION=false
```

### Staging/Production Environment

Use environment variables or Azure Key Vault:

**Option 1: System Environment Variables**
```powershell
# PowerShell (run as Administrator)
[System.Environment]::SetEnvironmentVariable("DB_SERVER", "prod-sql-server.database.windows.net", "Machine")
[System.Environment]::SetEnvironmentVariable("DB_NAME", "AktarOtomasyon", "Machine")
[System.Environment]::SetEnvironmentVariable("DB_USER", "aktar_app", "Machine")
[System.Environment]::SetEnvironmentVariable("DB_PASSWORD", "ProductionPassword123!", "Machine")
[System.Environment]::SetEnvironmentVariable("DB_TRUSTED_CONNECTION", "false", "Machine")
```

**Option 2: Azure Key Vault** (recommended)
```csharp
// Retrieve from Key Vault at runtime
var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
var dbPassword = await secretClient.GetSecretAsync("aktar-app-db-password");
```

### Windows Authentication vs SQL Authentication

**Windows Authentication** (Trusted Connection):
```env
DB_SERVER=localhost
DB_NAME=AktarOtomasyon
DB_TRUSTED_CONNECTION=true
# DB_USER and DB_PASSWORD not needed
```

**SQL Authentication**:
```env
DB_SERVER=localhost
DB_NAME=AktarOtomasyon
DB_USER=aktar_app
DB_PASSWORD=YourPassword123!
DB_TRUSTED_CONNECTION=false
```

---

## Testing & Verification

### Test aktar_app Connection

```csharp
// C# test code
using (var sMan = new SqlManager())
{
    var error = sMan.TestConnection();
    if (error == null)
        Console.WriteLine("✓ Connection successful");
    else
        Console.WriteLine($"✗ Connection failed: {error}");
}
```

### Test aktar_app Permissions

```sql
-- Test EXECUTE permission
EXEC sp_kullanici_listele

-- Test SELECT permission
SELECT TOP 10 * FROM kullanici

-- Test INSERT permission
INSERT INTO audit_log (event_type, description) VALUES ('TEST', 'Permission test')

-- Test that dangerous operations are blocked
DROP TABLE kullanici  -- Should fail with permission denied
```

### Test aktar_readonly Permissions

```sql
-- Should succeed
SELECT * FROM kullanici
EXEC sp_kullanici_listele

-- Should fail (denied)
INSERT INTO kullanici (kullanici_adi) VALUES ('test')
UPDATE kullanici SET aktif = 0
DELETE FROM kullanici
```

### Test aktar_backup Permissions

```sql
-- Should succeed
BACKUP DATABASE AktarOtomasyon TO DISK = 'C:\Backups\test.bak'

-- Should fail (denied)
SELECT * FROM kullanici
EXEC sp_kullanici_listele
```

---

## Troubleshooting

### Error: "Login failed for user 'aktar_app'"

**Possible Causes**:
1. Incorrect password in .env file
2. User not created or disabled
3. Password expired (CHECK_EXPIRATION policy)
4. Account locked due to failed login attempts

**Solutions**:
```sql
-- Check if login exists and is enabled
SELECT name, is_disabled, is_expiration_checked, is_policy_checked
FROM sys.server_principals
WHERE name = 'aktar_app'

-- Reset password if expired
ALTER LOGIN aktar_app WITH PASSWORD = 'NewPassword123!'

-- Enable login if disabled
ALTER LOGIN aktar_app ENABLE
```

---

### Error: "The user does not have permission to perform this action"

**Possible Causes**:
1. Permissions not granted (002_grant_permissions.sql not run)
2. Using wrong user (e.g., aktar_readonly for write operations)
3. New table added after permissions were granted

**Solutions**:
```sql
-- Re-run permission grant script
sqlcmd -S localhost -E -i db\security\002_grant_permissions.sql

-- Verify permissions
sqlcmd -S localhost -E -i db\security\003_verify_permissions.sql

-- Check specific permission
SELECT permission_name, state_desc
FROM sys.database_permissions
WHERE grantee_principal_id = USER_ID('aktar_app')
```

---

### Error: "Cannot open database 'AktarOtomasyon'"

**Possible Causes**:
1. Database doesn't exist
2. User doesn't have access to database
3. Database is offline or in recovery

**Solutions**:
```sql
-- Check database status
SELECT name, state_desc, user_access_desc
FROM sys.databases
WHERE name = 'AktarOtomasyon'

-- Grant database access
USE AktarOtomasyon
CREATE USER aktar_app FOR LOGIN aktar_app
```

---

## Security Best Practices

### 1. Never Use sa or db_owner

- ❌ DO NOT use `sa` account for application connections
- ❌ DO NOT grant `db_owner` or `sysadmin` roles
- ✅ Always use least-privilege users (`aktar_app`, `aktar_readonly`)

### 2. Monitor Failed Login Attempts

```sql
-- Check SQL Server error log for failed logins
EXEC xp_readerrorlog 0, 1, N'Login failed'

-- Enable login auditing
USE master
GO
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE',
     N'Software\Microsoft\MSSQLServer\MSSQLServer',
     N'AuditLevel', REG_DWORD, 3  -- Failed and successful logins
GO
```

### 3. Use Encrypted Connections

Always use `TrustServerCertificate=True` or proper SSL certificates:

```
Server=prod-server;Database=AktarOtomasyon;User Id=aktar_app;Password=***;Encrypt=True;TrustServerCertificate=True;
```

### 4. Implement Connection Pooling

SqlManager already uses ADO.NET connection pooling. Ensure `Pooling=True` (default):

```
Server=localhost;Database=AktarOtomasyon;User Id=aktar_app;Password=***;Pooling=True;Max Pool Size=100;
```

### 5. Regular Security Audits

**Monthly Tasks**:
- Review failed login attempts
- Check for unauthorized permission grants
- Verify user account status (enabled/disabled)

**Quarterly Tasks**:
- Rotate all database passwords
- Review and update permissions
- Audit user access patterns

**Annually**:
- Full security assessment
- Penetration testing
- Update security policies

---

## Related Documentation

- [Configuration Management](./configuration-management.md) - Environment variables and config hierarchy
- [Backup & Restore Guide](./backup-restore-guide.md) - Database backup procedures
- [Deployment Guide](./deployment-guide.md) - Production deployment checklist

---

## Change Log

| Version | Date | Changes |
|---------|------|---------|
| 1.0 | 2025-12-26 | Initial database security setup (Sprint 8) |

---

**Questions or Issues?**

Contact the development team or file an issue in the project repository.
