/*
    sp_kullanici_pasifle

    Purpose: Soft delete user (set aktif=0)
    SECURITY: Cannot delete 'admin' user

    Parameters:
        @kullanici_id - User ID to deactivate
        @updated_by   - User ID performing the operation

    Returns:
        Number of affected rows

    Audit:
        Logs DELETE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_pasifle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_pasifle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_pasifle]
    @kullanici_id INT,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Cannot delete admin user
    DECLARE @kullanici_adi NVARCHAR(50)
    SELECT @kullanici_adi = kullanici_adi
    FROM kullanici
    WHERE kullanici_id = @kullanici_id

    IF @kullanici_adi = 'admin'
        RAISERROR('Admin kullanıcısı pasifleştirilemez.', 16, 1);

    BEGIN TRANSACTION

    BEGIN TRY
        UPDATE [dbo].[kullanici]
        SET aktif = 0,
            updated_at = GETDATE(),
            updated_by = @updated_by
        WHERE kullanici_id = @kullanici_id

        IF @@ROWCOUNT = 0
            RAISERROR('Kullanıcı bulunamadı.', 16, 1);

        -- Audit log
        EXEC sp_audit_log_ekle
            @entity = 'Kullanici',
            @entity_id = @kullanici_id,
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

PRINT 'Created sp_kullanici_pasifle'
GO
