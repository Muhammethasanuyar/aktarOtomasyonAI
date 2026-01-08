/*
    sp_rol_yetki_ekle

    Purpose: Assign permission to role
    Idempotent: Silently ignores if already assigned

    Parameters:
        @rol_id     - Role ID
        @yetki_id   - Permission ID
        @created_by - User ID performing the operation

    Audit:
        Logs ASSIGN_PERMISSION action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_yetki_ekle', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_yetki_ekle
GO

CREATE PROCEDURE [dbo].[sp_rol_yetki_ekle]
    @rol_id INT,
    @yetki_id INT,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if already assigned (idempotent)
    IF EXISTS (SELECT 1 FROM rol_yetki
               WHERE rol_id = @rol_id AND yetki_id = @yetki_id)
        RETURN; -- Silently ignore duplicate

    INSERT INTO rol_yetki (rol_id, yetki_id, created_at, created_by)
    VALUES (@rol_id, @yetki_id, GETDATE(), @created_by)

    -- Audit log
    EXEC sp_audit_log_ekle
        @entity = 'RolYetki',
        @entity_id = @rol_id,
        @action = 'ASSIGN_PERMISSION',
        @detail_json = (SELECT @yetki_id AS yetki_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
        @created_by = @created_by
END
GO

PRINT 'Created sp_rol_yetki_ekle'
GO
