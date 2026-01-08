# Demo Data Reset Procedure

## Overview

The demo reset utility (`db/ops/reset_demo.sql`) provides a safe, idempotent way to remove all demo data and reset the database to a clean state for re-seeding.

## When to Use Reset

1. **Fresh Demo Deployment**: Clean slate for new demo environment
2. **Testing Seed Scripts**: Verify seed scripts work on clean database
3. **Development Reset**: Return to clean state during development
4. **QA Testing**: Reset between test cycles
5. **Troubleshooting**: Fix corrupted or inconsistent demo data

## Reset Script Location

**File**: `db/ops/reset_demo.sql`

**Purpose**: Safely removes demo data while preserving schema

## What Gets Reset

### Data Deleted

The following tables are **completely cleared**:

1. **AI Content**:
   - `ai_urun_icerik_versiyon` (all versions)
   - `ai_urun_icerik` (all content)

2. **Media**:
   - `urun_gorsel` (image metadata only, not physical files)

3. **Notifications**:
   - `bildirim` (all notifications)

4. **Orders**:
   - `siparis_satir` (order line items)
   - `siparis` (orders)

5. **Stock**:
   - `stok_hareket` (all movements)
   - `urun_stok_ayar` (stock settings)

6. **Products**:
   - `urun` (all products)

7. **Reference Data**:
   - `tedarikci` (suppliers)
   - `urun_kategori` (categories)
   - `urun_birim` (units)

8. **Security (partial)**:
   - `kullanici` - Demo users only (WHERE kullanici_adi LIKE 'demo%')
   - `rol_yetki` - All role permissions
   - `rol` - Demo roles only (excludes ADMIN, USER base roles)

### Data Preserved

The following are **NOT deleted**:

1. **Schema**:
   - All tables, views, stored procedures
   - All indexes (including Sprint 9 performance indexes)
   - All foreign key constraints

2. **Core Security**:
   - `kullanici` - Admin user (WHERE kullanici_adi NOT LIKE 'demo%')
   - `rol` - Base roles (ADMIN, USER)

3. **System Configuration**:
   - Any application settings
   - Audit logs (if implemented)

## Reset Process

### 5-Step Process

The reset script follows a safe 5-step process:

#### Step 1: Disable Foreign Key Constraints

```sql
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
```

**Purpose**: Allows deletion in any order without FK violations

**Safety**: Constraints are re-enabled in Step 4

#### Step 2: Delete Demo Data (Reverse FK Order)

```sql
-- Child tables first, parent tables last
DELETE FROM [dbo].[ai_urun_icerik_versiyon];
DELETE FROM [dbo].[ai_urun_icerik];
DELETE FROM [dbo].[urun_gorsel];
DELETE FROM [dbo].[bildirim];
DELETE FROM [dbo].[siparis_satir];
DELETE FROM [dbo].[siparis];
DELETE FROM [dbo].[stok_hareket];
DELETE FROM [dbo].[urun_stok_ayar];
DELETE FROM [dbo].[urun];
DELETE FROM [dbo].[tedarikci];
DELETE FROM [dbo].[urun_kategori];
DELETE FROM [dbo].[urun_birim];
DELETE FROM [dbo].[kullanici] WHERE [kullanici_adi] LIKE 'demo%';
DELETE FROM [dbo].[rol_yetki];
DELETE FROM [dbo].[rol] WHERE [rol_kod] NOT IN ('ADMIN', 'USER');
```

**Purpose**: Complete data removal

**Order**: Respects FK dependencies (child → parent)

#### Step 3: Reset Identity Seeds

```sql
DBCC CHECKIDENT ('[dbo].[urun_kategori]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[urun_birim]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[tedarikci]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[urun]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[stok_hareket]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[siparis]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[siparis_satir]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[bildirim]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[ai_urun_icerik]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[urun_gorsel]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[rol]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[kullanici]', RESEED, 0);
```

**Purpose**: Reset auto-increment IDs to start from 1

**Benefit**: Consistent IDs across multiple reset cycles

#### Step 4: Re-enable Foreign Key Constraints

```sql
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
```

**Purpose**: Restore data integrity enforcement

**Validation**: WITH CHECK validates no existing violations

#### Step 5: Verify Clean State

```sql
DECLARE @UrunCount INT = (SELECT COUNT(*) FROM [dbo].[urun]);
DECLARE @TedarikciCount INT = (SELECT COUNT(*) FROM [dbo].[tedarikci]);
DECLARE @StokCount INT = (SELECT COUNT(*) FROM [dbo].[stok_hareket]);
-- ... etc

IF @UrunCount = 0 AND @TedarikciCount = 0 AND @StokCount = 0
    PRINT 'Database successfully reset!';
ELSE
    PRINT 'WARNING: Some data still exists.';
```

**Purpose**: Confirm successful reset

**Output**: Row counts for all major tables

## Execution

### From SQL Server Management Studio

```sql
-- Execute reset script
:r C:\Users\Muhammet\Desktop\aktar_otomasyon\db\ops\reset_demo.sql
GO
```

### From Command Line (sqlcmd)

```cmd
sqlcmd -S localhost -d AktarOtomasyon -E -i "C:\Users\Muhammet\Desktop\aktar_otomasyon\db\ops\reset_demo.sql"
```

### From PowerShell

```powershell
Invoke-Sqlcmd -ServerInstance "localhost" `
              -Database "AktarOtomasyon" `
              -InputFile "C:\Users\Muhammet\Desktop\aktar_otomasyon\db\ops\reset_demo.sql"
```

## Complete Reset Cycle

### Full Reset → Re-seed → Verify Workflow

```sql
-- Step 1: Reset demo data
:r db/ops/reset_demo.sql
GO

-- Step 2: Re-seed (in order)
:r db/seed/sprint9_demo_full/01_refdata.sql
GO
:r db/seed/sprint9_demo_full/02_products.sql
GO
:r db/seed/sprint9_demo_full/03_stock_settings.sql
GO
:r db/seed/sprint9_demo_full/04_stock_movements.sql
GO
:r db/seed/sprint9_demo_full/05_orders.sql
GO
:r db/seed/sprint9_demo_full/06_notifications.sql
GO
:r db/seed/sprint9_demo_full/07_ai_content.sql
GO
:r db/seed/sprint9_demo_full/08_images.sql
GO

-- Step 3: Verify seed data
:r db/seed/sprint9_demo_full/99_verify.sql
GO
```

**Expected Duration**: 2-5 minutes total

**Success Criteria**: 13/13 tests passed in verification

## Automated Reset Script

### PowerShell Automation (reset_and_reseed.ps1)

```powershell
# reset_and_reseed.ps1
# Automated reset and re-seed for demo environment

param(
    [string]$ServerInstance = "localhost",
    [string]$Database = "AktarOtomasyon",
    [string]$BasePath = "C:\Users\Muhammet\Desktop\aktar_otomasyon"
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Demo Reset and Re-seed" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

try {
    # Step 1: Reset
    Write-Host "STEP 1: Resetting demo data..." -ForegroundColor Yellow
    Invoke-Sqlcmd -ServerInstance $ServerInstance -Database $Database `
                  -InputFile "$BasePath\db\ops\reset_demo.sql" -Verbose
    Write-Host "  Done!" -ForegroundColor Green
    Write-Host ""

    # Step 2: Re-seed
    Write-Host "STEP 2: Re-seeding data..." -ForegroundColor Yellow

    $seedFiles = @(
        "01_refdata.sql",
        "02_products.sql",
        "03_stock_settings.sql",
        "04_stock_movements.sql",
        "05_orders.sql",
        "06_notifications.sql",
        "07_ai_content.sql",
        "08_images.sql"
    )

    foreach ($file in $seedFiles) {
        Write-Host "  Executing $file..." -ForegroundColor Gray
        Invoke-Sqlcmd -ServerInstance $ServerInstance -Database $Database `
                      -InputFile "$BasePath\db\seed\sprint9_demo_full\$file"
    }
    Write-Host "  Done!" -ForegroundColor Green
    Write-Host ""

    # Step 3: Verify
    Write-Host "STEP 3: Verifying data..." -ForegroundColor Yellow
    Invoke-Sqlcmd -ServerInstance $ServerInstance -Database $Database `
                  -InputFile "$BasePath\db\seed\sprint9_demo_full\99_verify.sql" -Verbose
    Write-Host "  Done!" -ForegroundColor Green
    Write-Host ""

    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "SUCCESS: Demo data reset and re-seeded!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
}
catch {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "ERROR: Reset failed!" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}
```

### Usage

```powershell
# Default (localhost)
.\reset_and_reseed.ps1

# Custom server
.\reset_and_reseed.ps1 -ServerInstance "PROD-SERVER" -Database "AktarOtomasyon"
```

## Safety Features

### 1. FK Constraint Management

Constraints are disabled during deletion to prevent FK violations, then **re-enabled with validation**.

### 2. Selective Security Deletion

Only demo users deleted (WHERE kullanici_adi LIKE 'demo%'), admin preserved.

### 3. Verification Step

Built-in verification confirms clean state before proceeding.

### 4. Idempotent

Safe to run multiple times - no errors if already clean.

### 5. No Schema Changes

Only data deleted, schema preserved (tables, indexes, SPs intact).

## Physical File Cleanup

### Image Files

**Important**: Reset script only clears database metadata (`urun_gorsel` table), not physical image files.

**Manual Cleanup Required**:

```powershell
# Delete physical product images
Remove-Item -Path "C:\AktarOtomasyon\Media\products\*" -Recurse -Force

# Or keep for re-use (database references them by ID)
```

**Recommendation**: Keep images if re-seeding immediately (seed script references same paths).

## Troubleshooting

### Problem: Foreign Key Violation During Reset

**Error**: "DELETE statement conflicted with REFERENCE constraint..."

**Cause**: FK constraints not disabled

**Solution**:
```sql
-- Manually disable constraints
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

-- Re-run reset script
:r db/ops/reset_demo.sql
```

### Problem: "Object in use" Error

**Error**: "Cannot truncate table because it is being referenced..."

**Cause**: Active connections or transactions

**Solution**:
```sql
-- Check active connections
SELECT * FROM sys.dm_exec_sessions WHERE database_id = DB_ID('AktarOtomasyon');

-- Kill blocking session (if safe)
KILL 53; -- Replace with actual session_id

-- Re-run reset script
```

### Problem: Identity Seed Not Reset

**Error**: New IDs start from previous max instead of 1

**Cause**: DBCC CHECKIDENT not executed

**Solution**:
```sql
-- Manually reset identity
DBCC CHECKIDENT ('[dbo].[urun]', RESEED, 0);

-- Verify
SELECT IDENT_CURRENT('[dbo].[urun]') AS current_identity;
-- Should be 0 or 1
```

### Problem: Verification Shows Non-Zero Counts

**Error**: "WARNING: Some data still exists."

**Cause**: Deletion failed for some tables

**Solution**:
```sql
-- Check which tables have data
SELECT 'urun' AS tablo, COUNT(*) AS kayit_sayisi FROM urun
UNION ALL
SELECT 'tedarikci', COUNT(*) FROM tedarikci
UNION ALL
SELECT 'siparis', COUNT(*) FROM siparis;

-- Manually delete remaining data
DELETE FROM [problematic_table];

-- Re-run reset script
```

## Best Practices

1. **Backup Before Reset**: Always backup production data before reset

2. **Verify Clean State**: Check verification output before re-seeding

3. **Complete Cycle**: Reset → Re-seed → Verify in one session

4. **Document Customizations**: If you modify reset script, document changes

5. **Test in Dev First**: Test reset cycle in development before production

6. **Schedule Maintenance**: Run reset during maintenance windows

7. **Automate**: Use PowerShell script for consistent execution

## Performance

**Reset Duration**: ~10-30 seconds

**Factors**:
- Database size (100 products: ~15 seconds)
- FK constraint count
- Server performance

**Optimization**:
- Disable FK constraints (already done in script)
- Run during low-usage periods
- Ensure adequate database log space

## Related Documentation

- `seed-strategy.md` - Seed data execution order
- `performance.md` - Performance indexes (preserved after reset)
- `dashboard-contract.md` - Dashboard SP contracts
- `media-seed.md` - Image file management

## Maintenance Schedule

**Recommended Reset Frequency**:

- **Development**: Daily or as needed
- **QA/Test**: After each test cycle
- **Demo Environment**: Weekly or before demos
- **Production**: NEVER (use selective data cleanup instead)

## Support

For issues with reset procedure:

1. Check troubleshooting section above
2. Verify FK constraints are enabled after reset
3. Review SQL Server error log
4. Contact database administrator if persistent issues
