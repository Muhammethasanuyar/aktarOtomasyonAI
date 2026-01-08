/*
    sp_kullanici_parola_guncelle

    Purpose: Change user password (requires old password verification)
    Used when user changes their own password

    Parameters:
        @kullanici_id        - User ID
        @eski_parola_hash    - Current password hash (for verification)
        @yeni_parola_hash    - New password hash (PBKDF2 base64)
        @yeni_parola_salt    - New password salt (base64)
        @parola_iterasyon    - PBKDF2 iterations (default 10000)

    Returns:
        Number of affected rows

    Audit:
        Logs PASSWORD_CHANGE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_parola_guncelle', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_parola_guncelle
GO

CREATE PROCEDURE [dbo].[sp_kullanici_parola_guncelle]
    @kullanici_id INT,
    @eski_parola_hash NVARCHAR(512),
    @yeni_parola_hash NVARCHAR(512),
    @yeni_parola_salt NVARCHAR(256),
    @parola_iterasyon INT = 10000
AS
BEGIN
    SET NOCOUNT ON;

    -- Verify old password
    IF NOT EXISTS (SELECT 1 FROM kullanici
                   WHERE kullanici_id = @kullanici_id
                   AND parola_hash = @eski_parola_hash)
        RAISERROR('Mevcut parola hatalı.', 16, 1);

    BEGIN TRANSACTION

    BEGIN TRY
        UPDATE [dbo].[kullanici]
        SET parola_hash = @yeni_parola_hash,
            parola_salt = @yeni_parola_salt,
            parola_iterasyon = @parola_iterasyon,
            updated_at = GETDATE()
        WHERE kullanici_id = @kullanici_id

        IF @@ROWCOUNT = 0
            RAISERROR('Kullanıcı bulunamadı.', 16, 1);

        -- Audit log (user changes own password)
        EXEC sp_audit_log_ekle
            @entity = 'Kullanici',
            @entity_id = @kullanici_id,
            @action = 'PASSWORD_CHANGE',
            @created_by = @kullanici_id

        COMMIT TRANSACTION

        SELECT @@ROWCOUNT AS affected_rows
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW;
    END CATCH
END
GO

PRINT 'Created sp_kullanici_parola_guncelle'
GO
