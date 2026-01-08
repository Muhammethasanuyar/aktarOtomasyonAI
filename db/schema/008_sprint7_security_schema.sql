/*
    Sprint 7 Security Schema Updates
    - Add PBKDF2 columns to kullanici table
    - Create ekran_yetki table for screen-permission mappings
    - Create performance indexes
*/

USE [AktarOtomasyon]
GO

PRINT '========================================='
PRINT 'Sprint 7: Security Schema Updates'
PRINT '========================================='

-- =============================================
-- STEP 1: Update kullanici table for PBKDF2
-- =============================================

-- Add parola_salt column if not exists
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_salt')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [parola_salt] NVARCHAR(256) NULL
    PRINT '✓ Added parola_salt column to kullanici table'
END

-- Add parola_iterasyon column if not exists
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_iterasyon')
BEGIN
    ALTER TABLE [dbo].[kullanici] ADD [parola_iterasyon] INT NULL DEFAULT 10000
    PRINT '✓ Added parola_iterasyon column to kullanici table'
END

-- Rename sifre_hash to parola_hash for consistency
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('kullanici') AND name = 'sifre_hash')
    AND NOT EXISTS (SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_hash')
BEGIN
    EXEC sp_rename 'kullanici.sifre_hash', 'parola_hash', 'COLUMN'
    PRINT '✓ Renamed sifre_hash to parola_hash'
END

-- Update parola_hash size for PBKDF2 output (base64 encoded)
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_hash')
BEGIN
    -- Check current size
    DECLARE @current_max_length INT
    SELECT @current_max_length = max_length
    FROM sys.columns
    WHERE object_id = OBJECT_ID('kullanici') AND name = 'parola_hash'

    -- If size is less than 512, alter it
    IF @current_max_length < 1024  -- NVARCHAR uses 2 bytes per char, so 512 * 2 = 1024
    BEGIN
        ALTER TABLE [dbo].[kullanici] ALTER COLUMN [parola_hash] NVARCHAR(512) NOT NULL
        PRINT '✓ Updated parola_hash size to NVARCHAR(512)'
    END
END

PRINT ''

-- =============================================
-- STEP 2: Create ekran_yetki table
-- =============================================

IF NOT EXISTS (SELECT 1 FROM sys.objects
               WHERE object_id = OBJECT_ID('ekran_yetki') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[ekran_yetki]
    (
        [ekran_yetki_id] INT IDENTITY(1,1) PRIMARY KEY,
        [ekran_kod] NVARCHAR(50) NOT NULL,
        [yetki_id] INT NOT NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_ekran_yetki_ekran FOREIGN KEY ([ekran_kod])
            REFERENCES [dbo].[kul_ekran]([ekran_kod]),
        CONSTRAINT FK_ekran_yetki_yetki FOREIGN KEY ([yetki_id])
            REFERENCES [dbo].[yetki]([yetki_id]),
        CONSTRAINT UQ_ekran_yetki UNIQUE ([ekran_kod], [yetki_id])
    )

    PRINT '✓ Created ekran_yetki table'
END
ELSE
BEGIN
    PRINT '  ekran_yetki table already exists'
END

PRINT ''

-- =============================================
-- STEP 3: Create Performance Indexes
-- =============================================

PRINT 'Creating performance indexes...'

-- kullanici indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_kullanici_adi' AND object_id = OBJECT_ID('kullanici'))
BEGIN
    CREATE INDEX IX_kullanici_kullanici_adi ON [dbo].[kullanici]([kullanici_adi])
    PRINT '✓ Created index: IX_kullanici_kullanici_adi'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_aktif' AND object_id = OBJECT_ID('kullanici'))
BEGIN
    CREATE INDEX IX_kullanici_aktif ON [dbo].[kullanici]([aktif])
    PRINT '✓ Created index: IX_kullanici_aktif'
END

-- rol indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_aktif' AND object_id = OBJECT_ID('rol'))
BEGIN
    CREATE INDEX IX_rol_aktif ON [dbo].[rol]([aktif])
    PRINT '✓ Created index: IX_rol_aktif'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_rol_adi' AND object_id = OBJECT_ID('rol'))
BEGIN
    CREATE INDEX IX_rol_rol_adi ON [dbo].[rol]([rol_adi])
    PRINT '✓ Created index: IX_rol_rol_adi'
END

-- yetki indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_yetki_yetki_kod' AND object_id = OBJECT_ID('yetki'))
BEGIN
    CREATE INDEX IX_yetki_yetki_kod ON [dbo].[yetki]([yetki_kod])
    PRINT '✓ Created index: IX_yetki_yetki_kod'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_yetki_modul' AND object_id = OBJECT_ID('yetki'))
BEGIN
    CREATE INDEX IX_yetki_modul ON [dbo].[yetki]([modul])
    PRINT '✓ Created index: IX_yetki_modul'
END

-- kullanici_rol indexes (CRITICAL for effective permissions)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_rol_kullanici' AND object_id = OBJECT_ID('kullanici_rol'))
BEGIN
    CREATE INDEX IX_kullanici_rol_kullanici ON [dbo].[kullanici_rol]([kullanici_id])
    PRINT '✓ Created index: IX_kullanici_rol_kullanici'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_kullanici_rol_rol' AND object_id = OBJECT_ID('kullanici_rol'))
BEGIN
    CREATE INDEX IX_kullanici_rol_rol ON [dbo].[kullanici_rol]([rol_id])
    PRINT '✓ Created index: IX_kullanici_rol_rol'
END

-- rol_yetki indexes (CRITICAL for effective permissions)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_yetki_rol' AND object_id = OBJECT_ID('rol_yetki'))
BEGIN
    CREATE INDEX IX_rol_yetki_rol ON [dbo].[rol_yetki]([rol_id])
    PRINT '✓ Created index: IX_rol_yetki_rol'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_rol_yetki_yetki' AND object_id = OBJECT_ID('rol_yetki'))
BEGIN
    CREATE INDEX IX_rol_yetki_yetki ON [dbo].[rol_yetki]([yetki_id])
    PRINT '✓ Created index: IX_rol_yetki_yetki'
END

-- ekran_yetki indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ekran_yetki_ekran' AND object_id = OBJECT_ID('ekran_yetki'))
BEGIN
    CREATE INDEX IX_ekran_yetki_ekran ON [dbo].[ekran_yetki]([ekran_kod])
    PRINT '✓ Created index: IX_ekran_yetki_ekran'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ekran_yetki_yetki' AND object_id = OBJECT_ID('ekran_yetki'))
BEGIN
    CREATE INDEX IX_ekran_yetki_yetki ON [dbo].[ekran_yetki]([yetki_id])
    PRINT '✓ Created index: IX_ekran_yetki_yetki'
END

-- audit_log indexes (for filtering and performance)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_entity_action' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_entity_action ON [dbo].[audit_log]([entity], [action])
    PRINT '✓ Created index: IX_audit_log_entity_action'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_created_at' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_created_at ON [dbo].[audit_log]([created_at] DESC)
    PRINT '✓ Created index: IX_audit_log_created_at'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_created_by' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_created_by ON [dbo].[audit_log]([created_by])
    PRINT '✓ Created index: IX_audit_log_created_by'
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_audit_log_entity_id' AND object_id = OBJECT_ID('audit_log'))
BEGIN
    CREATE INDEX IX_audit_log_entity_id ON [dbo].[audit_log]([entity_id])
    PRINT '✓ Created index: IX_audit_log_entity_id'
END

PRINT ''
PRINT '========================================='
PRINT 'Sprint 7: Schema updates completed'
PRINT '========================================='
PRINT ''
PRINT 'Summary:'
PRINT '- kullanici table updated for PBKDF2 (parola_salt, parola_iterasyon)'
PRINT '- parola_hash resized to NVARCHAR(512)'
PRINT '- ekran_yetki table created for screen-permission mappings'
PRINT '- Performance indexes created on security tables'
PRINT '- Audit log indexes created for filtering'
PRINT ''
GO
