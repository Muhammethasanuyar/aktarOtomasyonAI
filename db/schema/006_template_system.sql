-- =============================================
-- SPRINT 6: Template & System Settings Tables
-- =============================================
USE [AktarOtomasyon]
GO

-- Template Master Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[template]'))
BEGIN
    CREATE TABLE [dbo].[template] (
        [template_id] INT IDENTITY(1,1) PRIMARY KEY,
        [template_kod] NVARCHAR(50) NOT NULL UNIQUE,
        [template_adi] NVARCHAR(200) NOT NULL,
        [aciklama] NVARCHAR(500) NULL,
        [modul] NVARCHAR(50) NOT NULL, -- Siparis, Urun, Common, etc.
        [aktif] BIT NOT NULL DEFAULT 1,
        [created_by] INT NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE(),
        [updated_by] INT NULL,
        [updated_at] DATETIME NULL
    )

    CREATE INDEX IX_template_modul_aktif ON [dbo].[template]([modul], [aktif])
END
GO

-- Template Version Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[template_version]'))
BEGIN
    CREATE TABLE [dbo].[template_version] (
        [template_version_id] INT IDENTITY(1,1) PRIMARY KEY,
        [template_id] INT NOT NULL,
        [version_no] INT NOT NULL,
        [durum] NVARCHAR(20) NOT NULL DEFAULT 'DRAFT', -- DRAFT, APPROVED, ARCHIVED
        [dosya_adi] NVARCHAR(255) NOT NULL,
        [dosya_yolu] NVARCHAR(500) NOT NULL, -- Relative path: templates/{template_kod}/v{version_no}_{timestamp}.ext
        [mime_type] NVARCHAR(100) NULL,
        [dosya_boyut] BIGINT NULL,
        [sha256] NVARCHAR(64) NULL,
        [notlar] NVARCHAR(MAX) NULL,
        [created_by] INT NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE(),
        [approved_by] INT NULL,
        [approved_at] DATETIME NULL,

        FOREIGN KEY ([template_id]) REFERENCES [dbo].[template]([template_id]),
        CONSTRAINT UQ_template_version UNIQUE ([template_id], [version_no])
    )

    CREATE INDEX IX_template_version_durum ON [dbo].[template_version]([template_id], [durum])
END
GO

-- Template Active (tracks current active version per template)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[template_active]'))
BEGIN
    CREATE TABLE [dbo].[template_active] (
        [template_id] INT NOT NULL PRIMARY KEY,
        [template_version_id] INT NOT NULL,
        [aktif_at] DATETIME NOT NULL DEFAULT GETDATE(),
        [aktif_by] INT NULL,

        FOREIGN KEY ([template_id]) REFERENCES [dbo].[template]([template_id]),
        FOREIGN KEY ([template_version_id]) REFERENCES [dbo].[template_version]([template_version_id])
    )
END
GO

-- System Settings Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[system_setting]'))
BEGIN
    CREATE TABLE [dbo].[system_setting] (
        [setting_key] NVARCHAR(100) NOT NULL PRIMARY KEY,
        [setting_value] NVARCHAR(MAX) NOT NULL,
        [aciklama] NVARCHAR(500) NULL,
        [updated_by] INT NULL,
        [updated_at] DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO

-- Enhance Audit Log (if not exists, create; if exists, ensure columns)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[audit_log]'))
BEGIN
    CREATE TABLE [dbo].[audit_log] (
        [audit_id] INT IDENTITY(1,1) PRIMARY KEY,
        [entity] NVARCHAR(100) NOT NULL,        -- TEMPLATE, TEMPLATE_VERSION, SYSTEM_SETTING
        [entity_id] INT NOT NULL,
        [action] NVARCHAR(50) NOT NULL,         -- CREATE, UPDATE, DELETE, ACTIVATE, ARCHIVE, UPLOAD, DOWNLOAD
        [detail_json] NVARCHAR(MAX) NULL,       -- JSON with old/new values
        [created_by] INT NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    )

    CREATE INDEX IX_audit_log_entity ON [dbo].[audit_log]([entity], [entity_id])
    CREATE INDEX IX_audit_log_created_at ON [dbo].[audit_log]([created_at] DESC)
END
GO

PRINT 'Sprint 6: Template & System tables created successfully.'
GO
