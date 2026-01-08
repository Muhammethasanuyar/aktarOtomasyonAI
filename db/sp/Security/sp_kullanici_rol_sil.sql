/*
    sp_kullanici_rol_sil

    Purpose: Remove role from user

    Parameters:
        @kullanici_id - User ID
        @rol_id       - Role ID
        @updated_by   - User ID performing the operation

    Returns:
        Number of affected rows

    Audit:
        Logs REVOKE_ROLE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_rol_sil', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_rol_sil
GO

CREATE PROCEDURE [dbo].[sp_kullanici_rol_sil]
    @kullanici_id INT,
    @rol_id INT,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM kullanici_rol
    WHERE kullanici_id = @kullanici_id AND rol_id = @rol_id

    -- Audit log
    EXEC sp_audit_log_ekle
        @entity = 'KullaniciRol',
        @entity_id = @kullanici_id,
        @action = 'REVOKE_ROLE',
        @detail_json = (SELECT @rol_id AS rol_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
        @created_by = @updated_by

    SELECT @@ROWCOUNT AS affected_rows
END
GO

PRINT 'Created sp_kullanici_rol_sil'
GO
