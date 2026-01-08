-- Sprint 2 Frontend - Urun Module Screen Registration
-- Execute these INSERT statements in your database to register the new screens
-- This enables title loading from database and version logging

-- URUN_LISTE screen
INSERT INTO kul_ekran (ekran_kod, menudeki_adi, form_adi, modul, aktif)
VALUES ('URUN_LISTE', 'Ürün Listesi', 'FrmUrunListe', 'Urun', 1);

-- URUN_KART screen
INSERT INTO kul_ekran (ekran_kod, menudeki_adi, form_adi, modul, aktif)
VALUES ('URUN_KART', 'Ürün Kartı', 'FrmUrunKart', 'Urun', 1);

-- Verification query to check if screens are registered
SELECT ekran_kod, menudeki_adi, form_adi, modul, aktif
FROM kul_ekran
WHERE modul = 'Urun'
ORDER BY ekran_kod;
