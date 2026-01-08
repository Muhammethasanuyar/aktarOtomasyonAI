-- =============================================
-- TEMPLATE VERSION STORED PROCEDURES
-- =============================================
USE [AktarOtomasyon]
GO

-- sp_template_version_ekle
IF OBJECT_ID('sp_template_version_ekle', 'P') IS NOT NULL DROP PROCEDURE sp_template_version_ekle;
GO

CREATE PROCEDURE [dbo].[sp_template_version_ekle]
    @template_version_id INT = NULL OUTPUT,
    @template_id INT,
    @version_no INT OUTPUT,
    @dosya_adi NVARCHAR(255),
    @dosya_yolu NVARCHAR(500),
    @mime_type NVARCHAR(100) = NULL,
    @dosya_boyut BIGINT = NULL,
    @sha256 NVARCHAR(64) = NULL,
    @notlar NVARCHAR(MAX) = NULL,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- BR-TEMPLATE_VERSION-001: Template must exist
    IF NOT EXISTS (SELECT 1 FROM [dbo].[template] WHERE [template_id] = @template_id)
        RAISERROR('Template bulunamadı.', 16, 1);

    -- BR-TEMPLATE_VERSION-002: Auto-increment version_no
    SELECT @version_no = ISNULL(MAX([version_no]), 0) + 1
    FROM [dbo].[template_version]
    WHERE [template_id] = @template_id;

    -- BR-TEMPLATE_VERSION-003: Insert new version
    INSERT INTO [dbo].[template_version] (
        [template_id], [version_no], [durum], [dosya_adi], [dosya_yolu],
        [mime_type], [dosya_boyut], [sha256], [notlar], [created_by]
    )
    VALUES (
        @template_id, @version_no, 'DRAFT', @dosya_adi, @dosya_yolu,
        @mime_type, @dosya_boyut, @sha256, @notlar, @created_by
    );

    SET @template_version_id = SCOPE_IDENTITY();
END
GO

-- sp_template_version_listele
IF OBJECT_ID('sp_template_version_listele', 'P') IS NOT NULL DROP PROCEDURE sp_template_version_listele;
GO

CREATE PROCEDURE [dbo].[sp_template_version_listele]
    @template_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        tv.[template_version_id],
        tv.[template_id],
        tv.[version_no],
        tv.[durum],
        tv.[dosya_adi],
        tv.[dosya_yolu],
        tv.[mime_type],
        tv.[dosya_boyut],
        tv.[sha256],
        tv.[notlar],
        tv.[created_by],
        tv.[created_at],
        tv.[approved_by],
        tv.[approved_at],
        CASE WHEN ta.[template_version_id] = tv.[template_version_id] THEN 1 ELSE 0 END AS [is_active]
    FROM [dbo].[template_version] tv
    LEFT JOIN [dbo].[template_active] ta ON tv.[template_id] = ta.[template_id]
    WHERE tv.[template_id] = @template_id
    ORDER BY tv.[version_no] DESC
END
GO

-- sp_template_version_aktif_et
IF OBJECT_ID('sp_template_version_aktif_et', 'P') IS NOT NULL DROP PROCEDURE sp_template_version_aktif_et;
GO

CREATE PROCEDURE [dbo].[sp_template_version_aktif_et]
    @template_id INT,
    @template_version_id INT,
    @approved_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- BR-TEMPLATE_VERSION-004: Version must exist
        IF NOT EXISTS (SELECT 1 FROM [dbo].[template_version]
                       WHERE [template_version_id] = @template_version_id
                       AND [template_id] = @template_id)
            RAISERROR('Version bulunamadı.', 16, 1);

        -- BR-TEMPLATE_VERSION-005: Archive old active version
        UPDATE tv
        SET tv.[durum] = 'ARCHIVED'
        FROM [dbo].[template_version] tv
        INNER JOIN [dbo].[template_active] ta ON tv.[template_version_id] = ta.[template_version_id]
        WHERE ta.[template_id] = @template_id;

        -- BR-TEMPLATE_VERSION-006: Set new version as APPROVED
        UPDATE [dbo].[template_version]
        SET [durum] = 'APPROVED',
            [approved_by] = @approved_by,
            [approved_at] = GETDATE()
        WHERE [template_version_id] = @template_version_id;

        -- BR-TEMPLATE_VERSION-007: Update or insert active pointer
        IF EXISTS (SELECT 1 FROM [dbo].[template_active] WHERE [template_id] = @template_id)
        BEGIN
            UPDATE [dbo].[template_active]
            SET [template_version_id] = @template_version_id,
                [aktif_at] = GETDATE(),
                [aktif_by] = @approved_by
            WHERE [template_id] = @template_id;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[template_active] ([template_id], [template_version_id], [aktif_by])
            VALUES (@template_id, @template_version_id, @approved_by);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- sp_template_version_arsivle
IF OBJECT_ID('sp_template_version_arsivle', 'P') IS NOT NULL DROP PROCEDURE sp_template_version_arsivle;
GO

CREATE PROCEDURE [dbo].[sp_template_version_arsivle]
    @template_version_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- BR-TEMPLATE_VERSION-008: Cannot archive active version
    IF EXISTS (SELECT 1 FROM [dbo].[template_active] WHERE [template_version_id] = @template_version_id)
        RAISERROR('Aktif versiyon arşivlenemez. Önce başka bir versiyon aktif edilmelidir.', 16, 1);

    UPDATE [dbo].[template_version]
    SET [durum] = 'ARCHIVED'
    WHERE [template_version_id] = @template_version_id;
END
GO

PRINT 'Template Version SPs created successfully.'
GO
