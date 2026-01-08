-- =============================================
-- Tedarikçi Kaydet Stored Procedure
-- =============================================

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_tedarikci_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_tedarikci_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_tedarikci_kaydet]
    @tedarikci_id INT = NULL OUTPUT,
    @tedarikci_kod NVARCHAR(20),
    @tedarikci_adi NVARCHAR(200),
    @yetkili NVARCHAR(100) = NULL,
    @telefon NVARCHAR(20) = NULL,
    @email NVARCHAR(100) = NULL,
    @adres NVARCHAR(500) = NULL,
    @aktif BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validation
    IF @tedarikci_adi IS NULL OR LTRIM(RTRIM(@tedarikci_adi)) = ''
        RAISERROR('Tedarikçi adı zorunludur.', 16, 1);
    
    -- Generate kod if not provided
    IF @tedarikci_kod IS NULL OR LTRIM(RTRIM(@tedarikci_kod)) = ''
    BEGIN
        DECLARE @maxKod INT;
        SELECT @maxKod = ISNULL(MAX(CAST(SUBSTRING(tedarikci_kod, 4, LEN(tedarikci_kod)) AS INT)), 0)
        FROM tedarikci
        WHERE tedarikci_kod LIKE 'TED%';
        
        SET @tedarikci_kod = 'TED' + RIGHT('0000' + CAST(@maxKod + 1 AS VARCHAR), 4);
    END
    
    -- Unique check for tedarikci_kod
    IF EXISTS (SELECT 1 FROM [dbo].[tedarikci]
               WHERE tedarikci_kod = @tedarikci_kod
               AND (@tedarikci_id IS NULL OR tedarikci_id != @tedarikci_id))
        RAISERROR('Bu tedarikçi kodu zaten kullanılıyor.', 16, 1);
    
    BEGIN TRANSACTION
    
    BEGIN TRY
        IF @tedarikci_id IS NULL OR @tedarikci_id = 0
        BEGIN
            -- INSERT
            INSERT INTO [dbo].[tedarikci]
                (tedarikci_kod, tedarikci_adi, yetkili, telefon, email, adres, aktif, olusturma_tarih)
            VALUES
                (@tedarikci_kod, @tedarikci_adi, @yetkili, @telefon, @email, @adres, @aktif, GETDATE())
            
            SET @tedarikci_id = SCOPE_IDENTITY()
        END
        ELSE
        BEGIN
            -- UPDATE
            UPDATE [dbo].[tedarikci]
            SET tedarikci_kod = @tedarikci_kod,
                tedarikci_adi = @tedarikci_adi,
                yetkili = @yetkili,
                telefon = @telefon,
                email = @email,
                adres = @adres,
                aktif = @aktif
            WHERE tedarikci_id = @tedarikci_id
            
            IF @@ROWCOUNT = 0
                RAISERROR('Tedarikçi bulunamadı.', 16, 1);
        END
        
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        RAISERROR(@ErrorMessage, 16, 1)
    END CATCH
END
GO
