-- =============================================
-- SPRINT 6 FRONTEND: Screen Registrations (kul_ekran)
-- =============================================
USE [AktarOtomasyon]
GO

-- TEMPLATE_MRK (Template Management Center)
IF NOT EXISTS (SELECT 1 FROM [dbo].[kul_ekran] WHERE [ekran_kod] = 'TEMPLATE_MRK')
BEGIN
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [aktif], [created_at])
    VALUES ('TEMPLATE_MRK', 'Şablon Yönetimi', 'FrmTemplateMrk', 'System', 1, GETDATE())

    PRINT 'Sprint 6 Frontend: TEMPLATE_MRK screen registered.'
END
ELSE
BEGIN
    PRINT 'Sprint 6 Frontend: TEMPLATE_MRK screen already exists.'
END
GO

-- SYS_SETTINGS (System Settings)
IF NOT EXISTS (SELECT 1 FROM [dbo].[kul_ekran] WHERE [ekran_kod] = 'SYS_SETTINGS')
BEGIN
    INSERT INTO [dbo].[kul_ekran] ([ekran_kod], [menudeki_adi], [form_adi], [modul], [aktif], [created_at])
    VALUES ('SYS_SETTINGS', 'Sistem Ayarları', 'FrmSystemSettings', 'System', 1, GETDATE())

    PRINT 'Sprint 6 Frontend: SYS_SETTINGS screen registered.'
END
ELSE
BEGIN
    PRINT 'Sprint 6 Frontend: SYS_SETTINGS screen already exists.'
END
GO

PRINT 'Sprint 6 Frontend: Screen registrations completed successfully.'
GO
