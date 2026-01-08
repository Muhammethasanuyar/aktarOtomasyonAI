/*
    Sprint 7 Security Schema - COMPLETE Migration
    This script adds ALL missing columns and tables for Sprint 7 Backend

    IMPORTANT: Run this BEFORE creating stored procedures!
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Sprint 7: COMPLETE Security Schema Migration'
PRINT '========================================='
PRINT ''

-- =============================================
-- STEP 1: Update kullanici table
-- =============================================
PRINT 'STEP 1: Updating kullanici table...'

-- Add parola_salt
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_salt')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [parola_salt] NVARCHAR(256) NULL
    PRINT '  ✓ Added parola_salt column'
END

-- Add parola_iterasyon
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_iterasyon')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [parola_iterasyon] INT NULL DEFAULT 10000
    PRINT '  ✓ Added parola_iterasyon column'
END

-- Rename sifre_hash to parola_hash if needed
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'sifre_hash')
    AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_hash')
BEGIN
    EXEC sp_rename 'kullanici.sifre_hash', 'parola_hash', 'COLUMN'
    PRINT '  ✓ Renamed sifre_hash to parola_hash'
END

-- Ensure parola_hash has correct size
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_hash')
BEGIN
    DECLARE @current_max_length INT
    SELECT @current_max_length = max_length FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_hash'

    IF @current_max_length < 1024
    BEGIN
        ALTER TABLE [dbo].[kullanici] ALTER COLUMN [parola_hash] NVARCHAR(512) NULL
        PRINT '  ✓ Updated parola_hash size to NVARCHAR(512)'
    END
END

-- Add son_giris_tarih
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'son_giris_tarih')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [son_giris_tarih] DATETIME NULL
    PRINT '  ✓ Added son_giris_tarih column'
END

-- Add created_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'created_at')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    PRINT '  ✓ Added created_at column'
END

-- Add updated_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'updated_at')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [updated_at] DATETIME NULL
    PRINT '  ✓ Added updated_at column'
END

-- Add created_by
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'created_by')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [created_by] INT NULL
    PRINT '  ✓ Added created_by column'
END

-- Add updated_by
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici') AND name = 'updated_by')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [updated_by] INT NULL
    PRINT '  ✓ Added updated_by column'
END

PRINT ''

-- =============================================
-- STEP 2: Update rol table
-- =============================================
PRINT 'STEP 2: Updating rol table...'

-- Add created_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol') AND name = 'created_at')
BEGIN
    ALTER TABLE [dbo].[rol] ADD [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    PRINT '  ✓ Added created_at column'
END

-- Add updated_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol') AND name = 'updated_at')
BEGIN
    ALTER TABLE [dbo].[rol] ADD [updated_at] DATETIME NULL
    PRINT '  ✓ Added updated_at column'
END

-- Add created_by
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol') AND name = 'created_by')
BEGIN
    ALTER TABLE [dbo].[rol] ADD [created_by] INT NULL
    PRINT '  ✓ Added created_by column'
END

-- Add updated_by
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol') AND name = 'updated_by')
BEGIN
    ALTER TABLE [dbo].[rol] ADD [updated_by] INT NULL
    PRINT '  ✓ Added updated_by column'
END

PRINT ''

-- =============================================
-- STEP 3: Update yetki table
-- =============================================
PRINT 'STEP 3: Updating yetki table...'

-- Add created_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('yetki') AND name = 'created_at')
BEGIN
    ALTER TABLE [dbo].[yetki] ADD [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    PRINT '  ✓ Added created_at column'
END

PRINT ''

-- =============================================
-- STEP 4: Update kullanici_rol table
-- =============================================
PRINT 'STEP 4: Updating kullanici_rol table...'

-- Add aktif column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici_rol') AND name = 'aktif')
BEGIN
    ALTER TABLE [dbo].[kullanici_rol] ADD [aktif] BIT NOT NULL DEFAULT 1
    PRINT '  ✓ Added aktif column'
END

-- Add created_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici_rol') AND name = 'created_at')
BEGIN
    ALTER TABLE [dbo].[kullanici_rol] ADD [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    PRINT '  ✓ Added created_at column'
END

-- Add created_by
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('kullanici_rol') AND name = 'created_by')
BEGIN
    ALTER TABLE [dbo].[kullanici_rol] ADD [created_by] INT NULL
    PRINT '  ✓ Added created_by column'
END

PRINT ''

-- =============================================
-- STEP 5: Update rol_yetki table
-- =============================================
PRINT 'STEP 5: Updating rol_yetki table...'

-- Add aktif column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol_yetki') AND name = 'aktif')
BEGIN
    ALTER TABLE [dbo].[rol_yetki] ADD [aktif] BIT NOT NULL DEFAULT 1
    PRINT '  ✓ Added aktif column'
END

-- Add created_at
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol_yetki') AND name = 'created_at')
BEGIN
    ALTER TABLE [dbo].[rol_yetki] ADD [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    PRINT '  ✓ Added created_at column'
END

-- Add created_by
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('rol_yetki') AND name = 'created_by')
BEGIN
    ALTER TABLE [dbo].[rol_yetki] ADD [created_by] INT NULL
    PRINT '  ✓ Added created_by column'
END

PRINT ''

-- =============================================
-- STEP 6: Create ekran_yetki table
-- =============================================
PRINT 'STEP 6: Creating ekran_yetki table...'

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('ekran_yetki') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[ekran_yetki]
    (
        [ekran_yetki_id] INT IDENTITY(1,1) PRIMARY KEY,
        [ekran_kod] NVARCHAR(50) NOT NULL,
        [yetki_id] INT NOT NULL,
        [aktif] BIT NOT NULL DEFAULT 1,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE(),
        [created_by] INT NULL,

        CONSTRAINT FK_ekran_yetki_yetki FOREIGN KEY ([yetki_id])
            REFERENCES [dbo].[yetki]([yetki_id]),
        CONSTRAINT UQ_ekran_yetki UNIQUE ([ekran_kod], [yetki_id])
    )
    PRINT '  ✓ Created ekran_yetki table'
END
ELSE
BEGIN
    PRINT '  ✓ ekran_yetki table already exists'

    -- Add aktif column if missing
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('ekran_yetki') AND name = 'aktif')
    BEGIN
        ALTER TABLE [dbo].[ekran_yetki] ADD [aktif] BIT NOT NULL DEFAULT 1
        PRINT '  ✓ Added aktif column to ekran_yetki'
    END

    -- Add created_by if missing
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('ekran_yetki') AND name = 'created_by')
    BEGIN
        ALTER TABLE [dbo].[ekran_yetki] ADD [created_by] INT NULL
        PRINT '  ✓ Added created_by column to ekran_yetki'
    END
END

PRINT ''

-- =============================================
-- STEP 7: Create audit_log table
-- =============================================
PRINT 'STEP 7: Creating audit_log table...'

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('audit_log') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[audit_log]
    (
        [audit_id] INT IDENTITY(1,1) PRIMARY KEY,
        [entity] NVARCHAR(100) NOT NULL,
        [entity_id] INT NULL,
        [action] NVARCHAR(50) NOT NULL,
        [detail_json] NVARCHAR(MAX) NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE(),
        [created_by] INT NOT NULL,

        CONSTRAINT FK_audit_log_kullanici FOREIGN KEY ([created_by])
            REFERENCES [dbo].[kullanici]([kullanici_id])
    )
    PRINT '  ✓ Created audit_log table'
END
ELSE
BEGIN
    PRINT '  ✓ audit_log table already exists'
END

PRINT ''

-- =============================================
-- STEP 8: Create Performance Indexes
-- =============================================
PRINT 'STEP 8: Creating performance indexes...'

-- kullanici indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_kullanici_adi' AND object_id = OBJECT_ID('kullanici'))
BEGIN
    CREATE INDEX IX_kullanici_kullanici_adi ON [dbo].[kullanici]([kullanici_adi])
    PRINT '  ✓ IX_kullanici_kullanici_adi'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_aktif' AND object_id = OBJECT_ID('kullanici'))
BEGIN
    CREATE INDEX IX_kullanici_aktif ON [dbo].[kullanici]([aktif])
    PRINT '  ✓ IX_kullanici_aktif'
END

-- rol indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_aktif' AND object_id = OBJECT_ID('rol'))
BEGIN
    CREATE INDEX IX_rol_aktif ON [dbo].[rol]([aktif])
    PRINT '  ✓ IX_rol_aktif'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_rol_adi' AND object_id = OBJECT_ID('rol'))
BEGIN
    CREATE INDEX IX_rol_rol_adi ON [dbo].[rol]([rol_adi])
    PRINT '  ✓ IX_rol_rol_adi'
END

-- yetki indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_yetki_yetki_kod' AND object_id = OBJECT_ID('yetki'))
BEGIN
    CREATE INDEX IX_yetki_yetki_kod ON [dbo].[yetki]([yetki_kod])
    PRINT '  ✓ IX_yetki_yetki_kod'
END

-- kullanici_rol indexes (CRITICAL for effective permissions)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_rol_kullanici' AND object_id = OBJECT_ID('kullanici_rol'))
BEGIN
    CREATE INDEX IX_kullanici_rol_kullanici ON [dbo].[kullanici_rol]([kullanici_id])
    PRINT '  ✓ IX_kullanici_rol_kullanici'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_rol_rol' AND object_id = OBJECT_ID('kullanici_rol'))
BEGIN
    CREATE INDEX IX_kullanici_rol_rol ON [dbo].[kullanici_rol]([rol_id])
    PRINT '  ✓ IX_kullanici_rol_rol'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_rol_aktif' AND object_id = OBJECT_ID('kullanici_rol'))
BEGIN
    CREATE INDEX IX_kullanici_rol_aktif ON [dbo].[kullanici_rol]([aktif])
    PRINT '  ✓ IX_kullanici_rol_aktif'
END

-- rol_yetki indexes (CRITICAL for effective permissions)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_yetki_rol' AND object_id = OBJECT_ID('rol_yetki'))
BEGIN
    CREATE INDEX IX_rol_yetki_rol ON [dbo].[rol_yetki]([rol_id])
    PRINT '  ✓ IX_rol_yetki_rol'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_yetki_yetki' AND object_id = OBJECT_ID('rol_yetki'))
BEGIN
    CREATE INDEX IX_rol_yetki_yetki ON [dbo].[rol_yetki]([yetki_id])
    PRINT '  ✓ IX_rol_yetki_yetki'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_yetki_aktif' AND object_id = OBJECT_ID('rol_yetki'))
BEGIN
    CREATE INDEX IX_rol_yetki_aktif ON [dbo].[rol_yetki]([aktif])
    PRINT '  ✓ IX_rol_yetki_aktif'
END

-- ekran_yetki indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ekran_yetki_ekran' AND object_id = OBJECT_ID('ekran_yetki'))
BEGIN
    CREATE INDEX IX_ekran_yetki_ekran ON [dbo].[ekran_yetki]([ekran_kod])
    PRINT '  ✓ IX_ekran_yetki_ekran'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ekran_yetki_yetki' AND object_id = OBJECT_ID('ekran_yetki'))
BEGIN
    CREATE INDEX IX_ekran_yetki_yetki ON [dbo].[ekran_yetki]([yetki_id])
    PRINT '  ✓ IX_ekran_yetki_yetki'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ekran_yetki_aktif' AND object_id = OBJECT_ID('ekran_yetki'))
BEGIN
    CREATE INDEX IX_ekran_yetki_aktif ON [dbo].[ekran_yetki]([aktif])
    PRINT '  ✓ IX_ekran_yetki_aktif'
END

-- audit_log indexes (for filtering and performance)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_entity_action' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_entity_action ON [dbo].[audit_log]([entity], [action])
    PRINT '  ✓ IX_audit_log_entity_action'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_created_at' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_created_at ON [dbo].[audit_log]([created_at] DESC)
    PRINT '  ✓ IX_audit_log_created_at'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_created_by' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_created_by ON [dbo].[audit_log]([created_by])
    PRINT '  ✓ IX_audit_log_created_by'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_entity_id' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_entity_id ON [dbo].[audit_log]([entity_id])
    PRINT '  ✓ IX_audit_log_entity_id'
END

PRINT ''
PRINT '========================================='
PRINT 'Sprint 7: Schema Migration COMPLETED!'
PRINT '========================================='
PRINT ''
PRINT 'Summary of changes:'
PRINT '  - kullanici: Added PBKDF2 columns + audit columns'
PRINT '  - rol: Added audit columns'
PRINT '  - yetki: Added created_at'
PRINT '  - kullanici_rol: Added aktif + audit columns'
PRINT '  - rol_yetki: Added aktif + audit columns'
PRINT '  - ekran_yetki: Created table'
PRINT '  - audit_log: Created table'
PRINT '  - Performance indexes: Created for all tables'
PRINT ''
PRINT 'NEXT STEPS:'
PRINT '  1. Run: db/sp/CREATE_ALL_STORED_PROCEDURES.sql'
PRINT '  2. Run: db/seed/008_sprint7_security_seed.sql'
PRINT '  3. Run: db/verify/verify_sprint7_setup.sql'
PRINT ''
PRINT '========================================='

SET NOCOUNT OFF
GO
