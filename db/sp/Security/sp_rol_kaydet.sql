/*
    sp_rol_kaydet

    Purpose: Create or update role
    UPSERT operation based on rol_id

    Parameters:
        @rol_id      - NULL for INSERT, value for UPDATE (OUTPUT parameter)
        @rol_adi     - Role name (unique, required)
        @aciklama    - Description (optional)
        @aktif       - Active status (default 1)
        @created_by  - User ID performing the operation

    Returns:
        @rol_id via OUTPUT parameter

    Audit:
        Logs CREATE or UPDATE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_kaydet', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_kaydet
GO

CREATE PROCEDURE [dbo].[sp_rol_kaydet]
    @rol_id INT = NULL OUTPUT,
    @rol_adi NVARCHAR(100),
    @aciklama NVARCHAR(500) = NULL,
    @aktif BIT = 1,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validation
    IF @rol_adi IS NULL OR LTRIM(RTRIM(@rol_adi)) = ''
        RAISERROR('Rol adı zorunludur.', 16, 1);

    -- Unique check
    IF EXISTS (SELECT 1 FROM rol
               WHERE rol_adi = @rol_adi
               AND (@rol_id IS NULL OR rol_id != @rol_id))
        RAISERROR('Bu rol adı zaten kullanılıyor.', 16, 1);

    BEGIN TRANSACTION

    BEGIN TRY
        IF @rol_id IS NULL OR @rol_id = 0
        BEGIN
            -- INSERT
            INSERT INTO rol (rol_adi, aciklama, aktif, created_at, created_by)
            VALUES (@rol_adi, @aciklama, @aktif, GETDATE(), @created_by)

            SET @rol_id = SCOPE_IDENTITY()

            EXEC sp_audit_log_ekle
                @entity = 'Rol',
                @entity_id = @rol_id,
                @action = 'CREATE',
                @created_by = @created_by
        END
        ELSE
        BEGIN
            -- UPDATE
            UPDATE rol
            SET rol_adi = @rol_adi,
                aciklama = @aciklama,
                aktif = @aktif,
                updated_at = GETDATE(),
                updated_by = @created_by
            WHERE rol_id = @rol_id

            IF @@ROWCOUNT = 0
                RAISERROR('Rol bulunamadı.', 16, 1);

            EXEC sp_audit_log_ekle
                @entity = 'Rol',
                @entity_id = @rol_id,
                @action = 'UPDATE',
                @created_by = @created_by
        END

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW;
    END CATCH
END
GO

PRINT 'Created sp_rol_kaydet'
GO
