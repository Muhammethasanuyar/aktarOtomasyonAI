/*
    Fix audit_log table - Add missing Sprint 7 columns
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '========================================='
PRINT 'Fixing audit_log table'
PRINT '========================================='
PRINT ''

-- Check current columns
PRINT 'Current audit_log columns:'
SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') ORDER BY column_id

PRINT ''
PRINT 'Adding missing columns...'

-- Add entity column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') AND name = 'entity')
BEGIN
    ALTER TABLE [dbo].[audit_log] ADD [entity] NVARCHAR(100) NOT NULL DEFAULT ''
    PRINT '  ✓ Added entity column'
END

-- Add entity_id column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') AND name = 'entity_id')
BEGIN
    ALTER TABLE [dbo].[audit_log] ADD [entity_id] INT NULL
    PRINT '  ✓ Added entity_id column'
END

-- Add action column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') AND name = 'action')
BEGIN
    ALTER TABLE [dbo].[audit_log] ADD [action] NVARCHAR(50) NOT NULL DEFAULT ''
    PRINT '  ✓ Added action column'
END

-- Add detail_json column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') AND name = 'detail_json')
BEGIN
    ALTER TABLE [dbo].[audit_log] ADD [detail_json] NVARCHAR(MAX) NULL
    PRINT '  ✓ Added detail_json column'
END

-- Add created_at column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') AND name = 'created_at')
BEGIN
    ALTER TABLE [dbo].[audit_log] ADD [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    PRINT '  ✓ Added created_at column'
END

-- Add created_by column
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('audit_log') AND name = 'created_by')
BEGIN
    ALTER TABLE [dbo].[audit_log] ADD [created_by] INT NOT NULL DEFAULT 1
    PRINT '  ✓ Added created_by column'
END

PRINT ''
PRINT 'Creating indexes...'

-- Create indexes
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
PRINT 'audit_log table fixed!'
PRINT '========================================='

SET NOCOUNT OFF
GO
