-- =============================================
-- SPRINT 6: Seed Data for Template & System Settings
-- =============================================
USE [AktarOtomasyon]
GO

-- System Settings - Default Values
IF NOT EXISTS (SELECT 1 FROM [dbo].[system_setting] WHERE [setting_key] = 'TemplatePath')
BEGIN
    INSERT INTO [dbo].[system_setting] ([setting_key], [setting_value], [aciklama])
    VALUES ('TemplatePath', '.\templates', 'Şablon dosyalarının saklandığı klasör yolu')
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[system_setting] WHERE [setting_key] = 'ReportPath')
BEGIN
    INSERT INTO [dbo].[system_setting] ([setting_key], [setting_value], [aciklama])
    VALUES ('ReportPath', '.\reports', 'Rapor çıktılarının saklandığı klasör yolu')
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[system_setting] WHERE [setting_key] = 'TemplateStorageMode')
BEGIN
    INSERT INTO [dbo].[system_setting] ([setting_key], [setting_value], [aciklama])
    VALUES ('TemplateStorageMode', 'FileSystem', 'Şablon saklama modu: FileSystem veya Database')
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[system_setting] WHERE [setting_key] = 'TemplateMaxSizeMB')
BEGIN
    INSERT INTO [dbo].[system_setting] ([setting_key], [setting_value], [aciklama])
    VALUES ('TemplateMaxSizeMB', '10', 'Maksimum şablon dosya boyutu (MB)')
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[system_setting] WHERE [setting_key] = 'TemplateAllowedExtensions')
BEGIN
    INSERT INTO [dbo].[system_setting] ([setting_key], [setting_value], [aciklama])
    VALUES ('TemplateAllowedExtensions', '.repx;.docx;.xlsx;.pdf', 'İzin verilen şablon dosya uzantıları (noktalı virgül ile ayrılmış)')
END

-- Sample Templates
IF NOT EXISTS (SELECT 1 FROM [dbo].[template] WHERE [template_kod] = 'SIPARIS_CIKTI')
BEGIN
    INSERT INTO [dbo].[template] ([template_kod], [template_adi], [modul], [aciklama], [aktif])
    VALUES ('SIPARIS_CIKTI', 'Sipariş Çıktı Raporu', 'Siparis', 'Sipariş detaylarını içeren rapor şablonu', 1)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[template] WHERE [template_kod] = 'URUN_ETIKET')
BEGIN
    INSERT INTO [dbo].[template] ([template_kod], [template_adi], [modul], [aciklama], [aktif])
    VALUES ('URUN_ETIKET', 'Ürün Etiket Şablonu', 'Urun', 'Ürün barkod etiketi şablonu', 1)
END

PRINT 'Sprint 6: Seed data inserted successfully.'
GO
