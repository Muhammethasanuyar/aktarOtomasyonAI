/*
    Sprint 8: System Diagnostics Screen Metadata

    This script inserts the kul_ekran metadata for the SYS_DIAG screen.

    USAGE:
    sqlcmd -S localhost -E -d AktarOtomasyon -i 009_sprint8_sys_diag_seed.sql
*/

USE [AktarOtomasyon]
GO

SET NOCOUNT ON

PRINT '=========================================';
PRINT 'Sprint 8: Inserting SYS_DIAG Screen';
PRINT '=========================================';
PRINT '';

-- Check if screen already exists
IF EXISTS (SELECT 1 FROM kul_ekran WHERE ekran_kod = 'SYS_DIAG')
BEGIN
    PRINT 'Screen SYS_DIAG already exists. Skipping insert.';
    PRINT '';
END
ELSE
BEGIN
    -- Insert SYS_DIAG screen metadata
    INSERT INTO kul_ekran (ekran_kod, menudeki_adi, form_adi, modul, aktif)
    VALUES ('SYS_DIAG', 'Sistem Durumu', 'FrmSYS_DIAG', 'System', 1);

    PRINT 'Screen SYS_DIAG inserted successfully.';
    PRINT '  Ekran Kod: SYS_DIAG';
    PRINT '  Menüdeki Adı: Sistem Durumu';
    PRINT '  Form Adı: FrmSYS_DIAG';
    PRINT '  Modül: System';
    PRINT '';
END

-- Show the inserted screen
PRINT 'Verification:';
SELECT ekran_id, ekran_kod, menudeki_adi, form_adi, modul, aktif, kayit_tarihi
FROM kul_ekran
WHERE ekran_kod = 'SYS_DIAG';

PRINT '';
PRINT '=========================================';
PRINT 'Sprint 8 Screen Metadata Insert Complete';
PRINT '=========================================';

SET NOCOUNT OFF
GO
