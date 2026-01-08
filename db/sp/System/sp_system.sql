-- =============================================
-- SYSTEM SETTINGS & AUDIT STORED PROCEDURES
-- =============================================
USE [AktarOtomasyon]
GO

-- sp_system_setting_listele
IF OBJECT_ID('sp_system_setting_listele', 'P') IS NOT NULL DROP PROCEDURE sp_system_setting_listele;
GO

CREATE PROCEDURE [dbo].[sp_system_setting_listele]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [setting_key], [setting_value], [aciklama], [updated_by], [updated_at]
    FROM [dbo].[system_setting]
    ORDER BY [setting_key]
END
GO

-- sp_system_setting_getir
IF OBJECT_ID('sp_system_setting_getir', 'P') IS NOT NULL DROP PROCEDURE sp_system_setting_getir;
GO

CREATE PROCEDURE [dbo].[sp_system_setting_getir]
    @setting_key NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [setting_key], [setting_value], [aciklama], [updated_by], [updated_at]
    FROM [dbo].[system_setting]
    WHERE [setting_key] = @setting_key
END
GO

-- sp_system_setting_kaydet
IF OBJECT_ID('sp_system_setting_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_system_setting_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_system_setting_kaydet]
    @setting_key NVARCHAR(100),
    @setting_value NVARCHAR(MAX),
    @aciklama NVARCHAR(500) = NULL,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- BR-SYSTEM_SETTING-001: Key and value required
    IF @setting_key IS NULL OR LTRIM(RTRIM(@setting_key)) = ''
        RAISERROR('Setting key zorunludur.', 16, 1);

    IF @setting_value IS NULL
        RAISERROR('Setting value zorunludur.', 16, 1);

    -- BR-SYSTEM_SETTING-002: Path settings cannot be empty
    IF @setting_key IN ('TemplatePath', 'ReportPath') AND LTRIM(RTRIM(@setting_value)) = ''
        RAISERROR('Path ayarları boş olamaz.', 16, 1);

    -- Upsert
    IF EXISTS (SELECT 1 FROM [dbo].[system_setting] WHERE [setting_key] = @setting_key)
    BEGIN
        UPDATE [dbo].[system_setting]
        SET [setting_value] = @setting_value,
            [aciklama] = @aciklama,
            [updated_by] = @updated_by,
            [updated_at] = GETDATE()
        WHERE [setting_key] = @setting_key;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[system_setting] ([setting_key], [setting_value], [aciklama], [updated_by])
        VALUES (@setting_key, @setting_value, @aciklama, @updated_by);
    END
END
GO

-- sp_audit_log_ekle
IF OBJECT_ID('sp_audit_log_ekle', 'P') IS NOT NULL DROP PROCEDURE sp_audit_log_ekle;
GO

CREATE PROCEDURE [dbo].[sp_audit_log_ekle]
    @entity NVARCHAR(100),
    @entity_id INT,
    @action NVARCHAR(50),
    @detail_json NVARCHAR(MAX) = NULL,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[audit_log] ([entity], [entity_id], [action], [detail_json], [created_by])
    VALUES (@entity, @entity_id, @action, @detail_json, @created_by);
END
GO

PRINT 'System & Audit SPs created successfully.'
GO
