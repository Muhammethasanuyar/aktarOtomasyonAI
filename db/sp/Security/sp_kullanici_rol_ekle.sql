/*
    sp_kullanici_rol_ekle

    Purpose: Assign role to user
    Idempotent: Silently ignores if already assigned

    Parameters:
        @kullanici_id - User ID
        @rol_id       - Role ID
        @created_by   - User ID performing the operation

    Audit:
        Logs ASSIGN_ROLE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_rol_ekle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_rol_ekle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_rol_ekle]
    @kullanici_id INT,
    @rol_id INT,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if already assigned (idempotent)
    IF EXISTS (SELECT 1 FROM kullanici_rol
               WHERE kullanici_id = @kullanici_id AND rol_id = @rol_id)
        RETURN; -- Silently ignore duplicate

    INSERT INTO kullanici_rol (kullanici_id, rol_id, created_at, created_by)
    VALUES (@kullanici_id, @rol_id, GETDATE(), @created_by)

    -- Audit log
    EXEC sp_audit_log_ekle
        @entity = 'KullaniciRol',
        @entity_id = @kullanici_id,
        @action = 'ASSIGN_ROLE',
        @detail_json = (SELECT @rol_id AS rol_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
        @created_by = @created_by
END
GO

PRINT 'Created sp_kullanici_rol_ekle'
GO
