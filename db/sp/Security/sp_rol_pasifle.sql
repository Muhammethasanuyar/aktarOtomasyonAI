/*
    sp_rol_pasifle

    Purpose: Soft delete role (set aktif=0)
    SECURITY: Cannot delete 'ADMIN' role

    Parameters:
        @rol_id     - Role ID to deactivate
        @updated_by - User ID performing the operation

    Returns:
        Number of affected rows

    Audit:
        Logs DELETE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_pasifle', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_pasifle
GO

CREATE PROCEDURE [dbo].[sp_rol_pasifle]
    @rol_id INT,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Cannot delete ADMIN role
    DECLARE @rol_adi NVARCHAR(100)
    SELECT @rol_adi = rol_adi FROM rol WHERE rol_id = @rol_id

    IF @rol_adi = 'ADMIN'
        RAISERROR('ADMIN rolü pasifleştirilemez.', 16, 1);

    BEGIN TRANSACTION

    BEGIN TRY
        UPDATE rol
        SET aktif = 0,
            updated_at = GETDATE(),
            updated_by = @updated_by
        WHERE rol_id = @rol_id

        IF @@ROWCOUNT = 0
            RAISERROR('Rol bulunamadı.', 16, 1);

        EXEC sp_audit_log_ekle
            @entity = 'Rol',
            @entity_id = @rol_id,
            @action = 'DELETE',
            @created_by = @updated_by

        COMMIT TRANSACTION

        SELECT @@ROWCOUNT AS affected_rows
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW;
    END CATCH
END
GO

PRINT 'Created sp_rol_pasifle'
GO
