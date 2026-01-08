/*
    sp_kullanici_kaydet

    Purpose: Create or update user with PBKDF2 password hash/salt
    UPSERT operation based on kullanici_id

    Parameters:
        @kullanici_id     - NULL for INSERT, value for UPDATE (OUTPUT parameter)
        @kullanici_adi    - Username (unique, required)
        @ad_soyad         - Full name (required)
        @email            - Email address (optional, basic validation)
        @parola_hash      - PBKDF2 password hash (base64, required for INSERT, optional for UPDATE)
        @parola_salt      - PBKDF2 salt (base64, required for INSERT, optional for UPDATE)
        @parola_iterasyon - PBKDF2 iterations (default 10000)
        @aktif            - Active status (default 1)
        @created_by       - User ID performing the operation

    Returns:
        @kullanici_id via OUTPUT parameter

    Audit:
        Logs CREATE or UPDATE action to audit_log
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_kullanici_kaydet', 'P') IS NOT NULL
    DROP PROCEDURE sp_kullanici_kaydet
GO

CREATE PROCEDURE [dbo].[sp_kullanici_kaydet]
    @kullanici_id INT = NULL OUTPUT,
    @kullanici_adi NVARCHAR(50),
    @ad_soyad NVARCHAR(100),
    @email NVARCHAR(100) = NULL,
    @parola_hash NVARCHAR(512) = NULL,
    @parola_salt NVARCHAR(256) = NULL,
    @parola_iterasyon INT = 10000,
    @aktif BIT = 1,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validation
    IF @kullanici_adi IS NULL OR LTRIM(RTRIM(@kullanici_adi)) = ''
        RAISERROR('Kullanıcı adı zorunludur.', 16, 1);

    IF @ad_soyad IS NULL OR LTRIM(RTRIM(@ad_soyad)) = ''
        RAISERROR('Ad Soyad zorunludur.', 16, 1);

    -- Unique check for kullanici_adi
    IF EXISTS (SELECT 1 FROM [dbo].[kullanici]
               WHERE kullanici_adi = @kullanici_adi
               AND (@kullanici_id IS NULL OR kullanici_id != @kullanici_id))
        RAISERROR('Bu kullanıcı adı zaten kullanılıyor.', 16, 1);

    -- Email format validation (basic)
    IF @email IS NOT NULL AND @email NOT LIKE '%_@__%.__%'
        RAISERROR('Geçerli bir email adresi giriniz.', 16, 1);

    BEGIN TRANSACTION

    BEGIN TRY
        IF @kullanici_id IS NULL OR @kullanici_id = 0
        BEGIN
            -- INSERT
            -- Password is required for new users
            IF @parola_hash IS NULL OR @parola_salt IS NULL
                RAISERROR('Yeni kullanıcı için parola zorunludur.', 16, 1);

            INSERT INTO [dbo].[kullanici]
                (kullanici_adi, ad_soyad, email, parola_hash, parola_salt, parola_iterasyon, aktif, created_at, created_by)
            VALUES
                (@kullanici_adi, @ad_soyad, @email, @parola_hash, @parola_salt, @parola_iterasyon, @aktif, GETDATE(), @created_by)

            SET @kullanici_id = SCOPE_IDENTITY()

            -- Audit log
            EXEC sp_audit_log_ekle
                @entity = 'Kullanici',
                @entity_id = @kullanici_id,
                @action = 'CREATE',
                @created_by = @created_by
        END
        ELSE
        BEGIN
            -- UPDATE
            UPDATE [dbo].[kullanici]
            SET kullanici_adi = @kullanici_adi,
                ad_soyad = @ad_soyad,
                email = @email,
                -- Only update password if provided
                parola_hash = ISNULL(@parola_hash, parola_hash),
                parola_salt = ISNULL(@parola_salt, parola_salt),
                parola_iterasyon = ISNULL(@parola_iterasyon, parola_iterasyon),
                aktif = @aktif,
                updated_at = GETDATE(),
                updated_by = @created_by
            WHERE kullanici_id = @kullanici_id

            IF @@ROWCOUNT = 0
                RAISERROR('Kullanıcı bulunamadı.', 16, 1);

            -- Audit log
            EXEC sp_audit_log_ekle
                @entity = 'Kullanici',
                @entity_id = @kullanici_id,
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

PRINT 'Created sp_kullanici_kaydet'
GO
