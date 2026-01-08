# Database Migrations Guide

## Overview

This directory contains database migration scripts for AktarOtomasyon. Each migration represents a versioned schema change that can be tracked, applied, and (optionally) rolled back.

**Sprint**: Sprint 8
**Versioning System**: Semantic Versioning (MAJOR.MINOR.PATCH)

---

## Table of Contents

1. [Migration Workflow](#migration-workflow)
2. [Naming Conventions](#naming-conventions)
3. [Creating a Migration](#creating-a-migration)
4. [Applying Migrations](#applying-migrations)
5. [Version Numbering](#version-numbering)
6. [Rollback Strategy](#rollback-strategy)
7. [Best Practices](#best-practices)
8. [Examples](#examples)

---

## Migration Workflow

### Step-by-Step Process

1. **Plan the Change**
   - Document what needs to change (add table, modify column, etc.)
   - Determine version number (MAJOR.MINOR.PATCH)
   - Consider backward compatibility

2. **Create Migration Script**
   - Write SQL script with clear comments
   - Include validation checks
   - Add error handling
   - Write rollback script (optional but recommended)

3. **Test on Development**
   - Apply migration to local database
   - Verify schema changes
   - Test application compatibility
   - Validate data integrity

4. **Test on Staging**
   - Apply to staging environment
   - Run full test suite
   - Performance testing
   - User acceptance testing

5. **Apply to Production**
   - Backup database first (CRITICAL!)
   - Apply during maintenance window
   - Register version
   - Monitor for issues
   - Keep rollback script ready

6. **Register Version**
   ```sql
   EXEC sp_db_version_register
       @version_number = '1.3.0',
       @description = 'Added product_image table for media storage',
       @script_name = '010_product_images.sql',
       @rollback_script = '010_rollback_product_images.sql'
   ```

---

## Naming Conventions

### Migration Files

Format: `{number}_{description}.sql`

Examples:
- `010_add_product_images.sql`
- `011_modify_kullanici_email.sql`
- `012_create_report_cache.sql`

### Rollback Files

Format: `{number}_rollback_{description}.sql`

Examples:
- `010_rollback_product_images.sql`
- `011_rollback_kullanici_email.sql`

### Numbering

- Start from `010` (001-009 reserved for historical/system migrations)
- Increment by 1 for each migration
- Zero-pad to 3 digits (010, 011, 012, etc.)
- Never reuse numbers

---

## Creating a Migration

### Migration Template

```sql
/*
    Migration: [Version Number] - [Brief Description]

    Purpose: [Detailed explanation of what this migration does]

    Author: [Your Name]
    Date: [YYYY-MM-DD]
    Ticket: [Issue/Ticket Number if applicable]

    Breaking Changes: [Yes/No - explain if yes]
    Rollback Available: [Yes/No]

    Dependencies:
    - [List any prerequisite migrations or requirements]

    Impact:
    - [Describe impact on application, downtime, etc.]
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON
SET XACT_ABORT ON  -- Rollback entire transaction on error

PRINT '========================================='
PRINT 'Migration [Number]: [Description]'
PRINT '========================================='
PRINT ''

BEGIN TRANSACTION

BEGIN TRY
    -- =============================================
    -- STEP 1: [Description]
    -- =============================================

    PRINT 'Step 1: [Action]...'

    -- Your SQL here

    PRINT '  ✓ Step 1 completed'

    -- =============================================
    -- STEP 2: [Description]
    -- =============================================

    PRINT 'Step 2: [Action]...'

    -- Your SQL here

    PRINT '  ✓ Step 2 completed'

    -- =============================================
    -- Commit Transaction
    -- =============================================

    COMMIT TRANSACTION

    PRINT ''
    PRINT '✓ Migration completed successfully'

    -- Register version (optional - can be done separately)
    -- EXEC sp_db_version_register 'X.Y.Z', 'Description', 'filename.sql'

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION

    PRINT ''
    PRINT '✗ Migration FAILED'
    PRINT 'Error: ' + ERROR_MESSAGE()
    PRINT 'Line: ' + CAST(ERROR_LINE() AS NVARCHAR(10))

    THROW
END CATCH

SET NOCOUNT OFF
GO
```

### Rollback Template

```sql
/*
    Rollback Migration: [Version Number]

    This script undoes changes from [migration_name].sql

    WARNING: This may result in data loss!
    Test thoroughly before running in production.
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON
SET XACT_ABORT ON

PRINT '========================================='
PRINT 'Rolling Back Migration [Number]'
PRINT '========================================='
PRINT ''

BEGIN TRANSACTION

BEGIN TRY
    -- Reverse changes in opposite order from migration

    PRINT 'Step 1: Undoing [action]...'

    -- Rollback SQL here

    PRINT '  ✓ Rollback step 1 completed'

    COMMIT TRANSACTION

    PRINT ''
    PRINT '✓ Rollback completed successfully'

    -- Note: You may want to register a rollback version
    -- EXEC sp_db_version_register 'X.Y.Z-rollback', 'Rolled back: Description', 'rollback_file.sql'

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION

    PRINT '✗ Rollback FAILED: ' + ERROR_MESSAGE()
    THROW
END CATCH

GO
```

---

## Applying Migrations

### Development Environment

```bash
# Apply migration
sqlcmd -S localhost -E -d AktarOtomasyon -i migrations/010_add_product_images.sql

# Register version
sqlcmd -S localhost -E -d AktarOtomasyon -Q "EXEC sp_db_version_register '1.3.0', 'Added product_image table', '010_add_product_images.sql'"

# Check current version
sqlcmd -S localhost -E -d AktarOtomasyon -Q "EXEC sp_db_version_current"
```

### Staging/Production Environment

```bash
# 1. Backup database first!
sqlcmd -S prod-server -E -i backup/backup_full.sql

# 2. Apply migration
sqlcmd -S prod-server -U aktar_app -P [password] -d AktarOtomasyon -i migrations/010_add_product_images.sql

# 3. Register version
sqlcmd -S prod-server -U aktar_app -P [password] -d AktarOtomasyon -Q "EXEC sp_db_version_register '1.3.0', 'Added product_image table', '010_add_product_images.sql'"

# 4. Verify
sqlcmd -S prod-server -U aktar_app -P [password] -d AktarOtomasyon -Q "EXEC sp_db_version_current"
```

---

## Version Numbering

### Semantic Versioning

Version format: **MAJOR.MINOR.PATCH**

#### MAJOR Version (X.0.0)

Breaking changes that require application updates:
- Removing tables or columns
- Changing column data types in incompatible ways
- Renaming database objects used by application
- Changing stored procedure signatures

**Example**: `2.0.0` - Removed deprecated `old_products` table

#### MINOR Version (X.Y.0)

New features that are backward compatible:
- Adding new tables
- Adding new columns with defaults
- Adding new stored procedures
- Creating new indexes

**Example**: `1.3.0` - Added `product_image` table

#### PATCH Version (X.Y.Z)

Bug fixes and small improvements:
- Fixing incorrect default values
- Adding missing indexes for performance
- Updating stored procedure logic (without signature change)
- Data fixes

**Example**: `1.2.1` - Fixed index on `kullanici.email` column

### Current Version History

| Version | Date | Description |
|---------|------|-------------|
| 1.0.0 | 2025-01-15 | Initial database schema (Sprint 1-6) |
| 1.1.0 | 2025-02-01 | Added security tables (Sprint 7) |
| 1.1.1 | 2025-02-05 | Added audit logging (Sprint 7) |
| 1.2.0 | 2025-02-26 | Added database versioning system (Sprint 8) |

---

## Rollback Strategy

### When to Rollback

- Critical bug discovered immediately after deployment
- Data corruption detected
- Application cannot connect after migration
- Performance degradation

### Rollback Process

1. **Stop Application** (prevent new connections)
2. **Run Rollback Script**
   ```bash
   sqlcmd -S server -E -d AktarOtomasyon -i migrations/010_rollback_product_images.sql
   ```
3. **Restore from Backup** (if rollback script unavailable or fails)
   ```bash
   sqlcmd -S server -E -i backup/restore_full.sql
   ```
4. **Verify Database State**
   ```sql
   EXEC sp_db_version_current
   DBCC CHECKDB(AktarOtomasyon)
   ```
5. **Restart Application**
6. **Investigate Root Cause**

### Rollback Limitations

⚠️ Some changes cannot be easily rolled back:
- Data transformations (may lose original data)
- Column deletions (data is lost)
- Large data migrations (time-consuming to reverse)

For these scenarios, **restore from backup** is the safest option.

---

## Best Practices

### 1. Always Test First

- ✅ Test on local development database
- ✅ Test on staging environment
- ✅ Run application integration tests
- ❌ Never apply untested migrations to production

### 2. Use Transactions

```sql
SET XACT_ABORT ON  -- Auto-rollback on error
BEGIN TRANSACTION
    -- Migration steps
COMMIT TRANSACTION
```

### 3. Make Migrations Idempotent

Migrations should be safe to run multiple times:

```sql
-- Good: Check if change already applied
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'email')
BEGIN
    ALTER TABLE kullanici ADD email NVARCHAR(255)
END

-- Bad: Will fail if run twice
ALTER TABLE kullanici ADD email NVARCHAR(255)
```

### 4. Backup Before Migration

```bash
# ALWAYS backup before production migration
sqlcmd -S prod-server -E -i backup/backup_full.sql
```

### 5. Communicate Changes

- Notify team before production migrations
- Document downtime requirements
- Provide rollback plan
- Update release notes

### 6. Monitor After Migration

- Check application logs for errors
- Monitor database performance
- Verify data integrity
- Keep rollback script ready for 24-48 hours

### 7. Version Everything

Register every migration in the version history:

```sql
EXEC sp_db_version_register
    @version_number = '1.3.0',
    @description = 'Clear description of what changed',
    @script_name = 'migration_filename.sql',
    @rollback_script = 'rollback_filename.sql'
```

---

## Examples

### Example 1: Add New Table

**File**: `010_add_product_images.sql`

```sql
/*
    Migration 010: Add Product Images Table
    Version: 1.3.0
    Purpose: Store product images in database for hybrid storage mode
*/

USE [AktarOtomasyon]
GO

SET XACT_ABORT ON

PRINT 'Creating product_image table...'

BEGIN TRANSACTION

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'product_image')
BEGIN
    CREATE TABLE product_image
    (
        image_id INT IDENTITY(1,1) PRIMARY KEY,
        urun_id INT NOT NULL,
        image_data VARBINARY(MAX) NOT NULL,
        file_name NVARCHAR(255) NOT NULL,
        file_size INT NOT NULL,
        mime_type NVARCHAR(100) NOT NULL,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_product_image_urun FOREIGN KEY (urun_id)
            REFERENCES urun(urun_id) ON DELETE CASCADE
    )

    CREATE NONCLUSTERED INDEX IX_product_image_urun
        ON product_image (urun_id)

    PRINT '✓ Created product_image table'
END

COMMIT TRANSACTION

EXEC sp_db_version_register '1.3.0', 'Added product_image table', '010_add_product_images.sql'
GO
```

**Rollback**: `010_rollback_product_images.sql`

```sql
USE [AktarOtomasyon]
GO

PRINT 'Dropping product_image table...'

BEGIN TRANSACTION

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'product_image')
BEGIN
    DROP TABLE product_image
    PRINT '✓ Dropped product_image table'
END

COMMIT TRANSACTION

EXEC sp_db_version_register '1.3.0-rollback', 'Rolled back product_image table', '010_rollback_product_images.sql'
GO
```

---

### Example 2: Modify Existing Column

**File**: `011_modify_kullanici_email.sql`

```sql
/*
    Migration 011: Make Email Unique
    Version: 1.3.1
    Purpose: Enforce email uniqueness for users
*/

USE [AktarOtomasyon]
GO

PRINT 'Making kullanici.email unique...'

BEGIN TRANSACTION

-- Step 1: Find and handle duplicate emails
IF EXISTS (
    SELECT email, COUNT(*)
    FROM kullanici
    WHERE email IS NOT NULL
    GROUP BY email
    HAVING COUNT(*) > 1
)
BEGIN
    PRINT '⚠ WARNING: Duplicate emails found. Cleaning up...'

    -- Keep first user, append ID to duplicates
    UPDATE k2
    SET email = k2.email + '_' + CAST(k2.kullanici_id AS NVARCHAR(10))
    FROM kullanici k2
    WHERE EXISTS (
        SELECT 1
        FROM kullanici k1
        WHERE k1.email = k2.email
          AND k1.kullanici_id < k2.kullanici_id
    )

    PRINT '✓ Cleaned up duplicate emails'
END

-- Step 2: Create unique constraint
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('kullanici')
      AND name = 'UQ_kullanici_email'
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_kullanici_email
        ON kullanici (email)
        WHERE email IS NOT NULL

    PRINT '✓ Created unique constraint on email'
END

COMMIT TRANSACTION

EXEC sp_db_version_register '1.3.1', 'Made kullanici.email unique', '011_modify_kullanici_email.sql'
GO
```

---

## Related Documentation

- [Database Security Guide](../../docs/database-security.md) - User permissions and security
- [Backup & Restore Guide](../../docs/backup-restore-guide.md) - Backup procedures
- [Schema Documentation](../schema/README.md) - Database schema reference

---

## Getting Help

If you encounter issues with migrations:

1. Check version history: `EXEC sp_db_version_history`
2. Review migration logs in `logs/` directory
3. Contact database administrator
4. Consult team documentation

---

**Last Updated**: 2025-12-26 (Sprint 8)
