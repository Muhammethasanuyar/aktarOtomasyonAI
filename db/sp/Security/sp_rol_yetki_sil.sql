/*
    sp_rol_yetki_sil

    Purpose: Remove permission from role

    Parameters:
        @rol_id     - Role ID
        @yetki_id   - Permission ID
        @updated_by - User ID performing the operation

    Returns:
        Number of affected rows

    Audit:
        Logs REVOKE_PERMISSION action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_yetki_sil', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_yetki_sil
GO

CREATE PROCEDURE [dbo].[sp_rol_yetki_sil]
    @rol_id INT,
    @yetki_id INT,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM rol_yetki
    WHERE rol_id = @rol_id AND yetki_id = @yetki_id

    -- Audit log
    EXEC sp_audit_log_ekle
        @entity = 'RolYetki',
        @entity_id = @rol_id,
        @action = 'REVOKE_PERMISSION',
        @detail_json = (SELECT @yetki_id AS yetki_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
        @created_by = @updated_by

    SELECT @@ROWCOUNT AS affected_rows
END
GO

PRINT 'Created sp_rol_yetki_sil'
GO
