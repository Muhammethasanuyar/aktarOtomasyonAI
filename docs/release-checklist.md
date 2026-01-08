# Release Checklist

## Pre-Release

- [ ] DB migration scriptleri hazır
- [ ] SP'ler deploy edildi
- [ ] SPBuilder DLL'ler güncel
- [ ] Uygulama versiyonu yükseltildi (AssemblyInfo)
- [ ] `kul_ekran` seed güncel
- [ ] Reports/Templates paketlendi

## Build Kontrolü

- [ ] Solution build başarılı
- [ ] Referans matrisi doğrulandı (Forms'ta yasak referans yok)
- [ ] Unit test'ler geçti (varsa)

## Deploy

- [ ] Veritabanı yedeği alındı
- [ ] Schema değişiklikleri uygulandı
- [ ] SP güncellemeleri uygulandı
- [ ] Seed data eklendi/güncellendi
- [ ] Uygulama kurulum paketi oluşturuldu

## Post-Deploy

- [ ] Smoke test tamamlandı
- [ ] DB bağlantı testi başarılı
- [ ] Kritik ekranlar açıldı
- [ ] Release notes güncellendi
