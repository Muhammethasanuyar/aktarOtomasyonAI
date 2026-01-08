/*
    sp_kullanici_parola_sifirla

    Purpose: Reset user password (admin only, no old password required)
    Used when admin resets a user's password

    Parameters:
        @kullanici_id        - User ID to reset password for
        @yeni_parola_hash    - New password hash (PBKDF2 base64)
        @yeni_parola_salt    - New password salt (base64)
        @parola_iterasyon    - PBKDF2 iterations (default 10000)
        @reset_by            - Admin user ID performing the reset

    Returns:
        Number of affected rows

    Audit:
        Logs PASSWORD_RESET action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_parola_sifirla', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_parola_sifirla
GO

CREATE PROCEDURE [dbo].[sp_kullanici_parola_sifirla]
    @kullanici_id INT,
    @yeni_parola_hash NVARCHAR(512),
    @yeni_parola_salt NVARCHAR(256),
    @parola_iterasyon INT = 10000,
    @reset_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION

    BEGIN TRY
        UPDATE [dbo].[kullanici]
        SET parola_hash = @yeni_parola_hash,
            parola_salt = @yeni_parola_salt,
            parola_iterasyon = @parola_iterasyon,
            updated_at = GETDATE(),
            updated_by = @reset_by
        WHERE kullanici_id = @kullanici_id

        IF @@ROWCOUNT = 0
            RAISERROR('Kullanıcı bulunamadı.', 16, 1);

        -- Audit log (admin resets password)
        EXEC sp_audit_log_ekle
            @entity = 'Kullanici',
            @entity_id = @kullanici_id,
            @action = 'PASSWORD_RESET',
            @created_by = @reset_by

        COMMIT TRANSACTION

        SELECT @@ROWCOUNT AS affected_rows
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW;
    END CATCH
END
GO

PRINT 'Created sp_kullanici_parola_sifirla'
GO
