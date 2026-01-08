-- =============================================
-- TEMPLATE STORED PROCEDURES
-- =============================================
USE [AktarOtomasyon]
GO

-- sp_template_listele
IF OBJECT_ID('sp_template_listele', 'P') IS NOT NULL DROP PROCEDURE sp_template_listele;
GO

CREATE PROCEDURE [dbo].[sp_template_listele]
    @modul NVARCHAR(50) = NULL,
    @aktif BIT = NULL,
    @arama NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t.[template_id],
        t.[template_kod],
        t.[template_adi],
        t.[modul],
        t.[aciklama],
        t.[aktif],
        t.[created_at],
        ta.[template_version_id] AS [aktif_version_id],
        tv.[version_no] AS [aktif_version_no]
    FROM [dbo].[template] t
    LEFT JOIN [dbo].[template_active] ta ON t.[template_id] = ta.[template_id]
    LEFT JOIN [dbo].[template_version] tv ON ta.[template_version_id] = tv.[template_version_id]
    WHERE (@modul IS NULL OR t.[modul] = @modul)
      AND (@aktif IS NULL OR t.[aktif] = @aktif)
      AND (@arama IS NULL OR t.[template_kod] LIKE '%' + @arama + '%' OR t.[template_adi] LIKE '%' + @arama + '%')
    ORDER BY t.[modul], t.[template_adi]
END
GO

-- sp_template_getir
IF OBJECT_ID('sp_template_getir', 'P') IS NOT NULL DROP PROCEDURE sp_template_getir;
GO

CREATE PROCEDURE [dbo].[sp_template_getir]
    @template_id INT = NULL,
    @template_kod NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- BR-TEMPLATE-001: Either template_id or template_kod required
    IF @template_id IS NULL AND @template_kod IS NULL
        RAISERROR('template_id veya template_kod zorunludur.', 16, 1);

    SELECT
        t.[template_id],
        t.[template_kod],
        t.[template_adi],
        t.[modul],
        t.[aciklama],
        t.[aktif],
        t.[created_by],
        t.[created_at],
        t.[updated_by],
        t.[updated_at],
        ta.[template_version_id] AS [aktif_version_id],
        tv.[version_no] AS [aktif_version_no]
    FROM [dbo].[template] t
    LEFT JOIN [dbo].[template_active] ta ON t.[template_id] = ta.[template_id]
    LEFT JOIN [dbo].[template_version] tv ON ta.[template_version_id] = tv.[template_version_id]
    WHERE (@template_id IS NOT NULL AND t.[template_id] = @template_id)
       OR (@template_kod IS NOT NULL AND t.[template_kod] = @template_kod)
END
GO

-- sp_template_kaydet
IF OBJECT_ID('sp_template_kaydet', 'P') IS NOT NULL DROP PROCEDURE sp_template_kaydet;
GO

CREATE PROCEDURE [dbo].[sp_template_kaydet]
    @template_id INT = NULL OUTPUT,
    @template_kod NVARCHAR(50),
    @template_adi NVARCHAR(200),
    @modul NVARCHAR(50),
    @aciklama NVARCHAR(500) = NULL,
    @aktif BIT = 1,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- BR-TEMPLATE-002: Required fields
    IF @template_kod IS NULL OR LTRIM(RTRIM(@template_kod)) = ''
        RAISERROR('Template kodu zorunludur.', 16, 1);

    IF @template_adi IS NULL OR LTRIM(RTRIM(@template_adi)) = ''
        RAISERROR('Template adı zorunludur.', 16, 1);

    IF @modul IS NULL OR LTRIM(RTRIM(@modul)) = ''
        RAISERROR('Modül zorunludur.', 16, 1);

    -- BR-TEMPLATE-003: Normalize template_kod
    SET @template_kod = UPPER(LTRIM(RTRIM(@template_kod)));

    -- BR-TEMPLATE-004: Unique template_kod check
    IF EXISTS (SELECT 1 FROM [dbo].[template]
               WHERE [template_kod] = @template_kod
               AND (@template_id IS NULL OR [template_id] != @template_id))
        RAISERROR('Template kodu zaten kullanılıyor.', 16, 1);

    -- Insert or Update
    IF @template_id IS NULL OR @template_id = 0
    BEGIN
        INSERT INTO [dbo].[template] (
            [template_kod], [template_adi], [modul], [aciklama], [aktif], [created_by]
        )
        VALUES (
            @template_kod, @template_adi, @modul, @aciklama, @aktif, @updated_by
        );
        SET @template_id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE [dbo].[template]
        SET [template_adi] = @template_adi,
            [modul] = @modul,
            [aciklama] = @aciklama,
            [aktif] = @aktif,
            [updated_by] = @updated_by,
            [updated_at] = GETDATE()
        WHERE [template_id] = @template_id;
    END
END
GO

-- sp_template_pasifle
IF OBJECT_ID('sp_template_pasifle', 'P') IS NOT NULL DROP PROCEDURE sp_template_pasifle;
GO

CREATE PROCEDURE [dbo].[sp_template_pasifle]
    @template_id INT,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[template]
    SET [aktif] = 0,
        [updated_by] = @updated_by,
        [updated_at] = GETDATE()
    WHERE [template_id] = @template_id;
END
GO

PRINT 'Template SPs created successfully.'
GO
