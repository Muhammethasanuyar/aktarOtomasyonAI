using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Urun.Interface;
using AktarOtomasyon.Urun.Interface.Models;
using AktarOtomasyon.Ai.Interface;
using DevExpress.XtraEditors;
using Newtonsoft.Json;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    /// <summary>
    /// Ürün Detay UserControl.
    /// UC-Only Pattern: Tüm iş mantığı bu sınıfta.
    /// Salt okunur görünüm: Ürün bilgileri + AI içerik (faydalar, kullanım, uyarı, kombinasyon)
    /// </summary>
    public partial class UcUrunDetay : UcBase
    {
        private int _urunId;
        private UrunModel _urunModel;
        private List<UrunGorselDto> _gorseller;
        private AiIcerikModel _aiIcerik;
        private int _currentImageIndex = 0;

        // AI içerik JSON model
        private class AiContentJsonModel
        {
            public string Fayda { get; set; }
            public string Kullanim { get; set; }
            public string Uyari { get; set; }
            public string Kombinasyon { get; set; }
        }

        public UcUrunDetay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Verileri yükler. UrunId zorunlu.
        /// </summary>
        public void LoadData(int urunId)
        {
            try
            {
                _urunId = urunId;

                // 1. Ürün bilgilerini yükle
                LoadUrunBilgi();

                // 2. Görselleri yükle
                LoadGorseller();

                // 3. AI içeriği yükle veya üret
                LoadAiIcerik();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("UcUrunDetay.LoadData error: {0}", ex.Message), "URUN");
                MessageHelper.ShowError(string.Format("Veri yükleme hatası: {0}", ex.Message));
            }
        }

        #region Ürün Bilgisi Yükleme

        /// <summary>
        /// Ürün temel bilgilerini yükler ve UI'ye yerleştirir.
        /// </summary>
        private void LoadUrunBilgi()
        {
            _urunModel = InterfaceFactory.Urun.Getir(_urunId);

            if (_urunModel == null)
            {
                MessageHelper.ShowError("Ürün bulunamadı veya silinmiş.");

                // Formu kapat
                if (ParentFrm != null)
                {
                    ParentFrm.Close();
                }
                return;
            }

            // Encoding fix
            _urunModel.UrunAdi = TextHelper.FixEncoding(_urunModel.UrunAdi);

            // UI'ye doldur
            lblUrunAdi.Text = _urunModel.UrunAdi ?? "-";
            lblUrunKod.Text = string.Format("Kod: {0}", _urunModel.UrunKod ?? "-");
            lblBarkod.Text = string.Format("Barkod: {0}", _urunModel.Barkod ?? "-");

            // Kategori ve Birim (Lookup'tan çek)
            var kategori = UrunLookupProvider.GetKategoriler()
                .FirstOrDefault(k => k.KategoriId == _urunModel.KategoriId);
            lblKategori.Text = string.Format("Kategori: {0}",
                kategori != null ? TextHelper.FixEncoding(kategori.KategoriAdi) : "-");

            var birim = UrunLookupProvider.GetBirimler()
                .FirstOrDefault(b => b.BirimId == _urunModel.BirimId);
            lblBirim.Text = string.Format("Birim: {0}",
                birim != null ? TextHelper.FixEncoding(birim.BirimAdi) : "-");

            // Fiyat
            if (_urunModel.SatisFiyati.HasValue)
            {
            lblFiyat.Text = string.Format("Fiyat: {0:C2}", _urunModel.SatisFiyati.Value);
                lblFiyat.Font = new Font(lblFiyat.Font, FontStyle.Bold);
                lblFiyat.ForeColor = Color.DarkGreen;

                // STOK BILGISI (NEW)
                try 
                {
                    var stokBilgi = InterfaceFactory.Stok.StokDurumGetir(_urunId);
                    if (stokBilgi != null)
                    {
                        decimal mevcutStok = stokBilgi.MevcutStok;
                        
                        var birimAdi = birim != null ? TextHelper.FixEncoding(birim.BirimAdi) : "";
                        lblMevcutStok.Text = string.Format("Mevcut Stok: {0:n2} {1}", mevcutStok, birimAdi);
                        
                        // Color Coding
                        if (mevcutStok <= 0)
                            lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.Red;
                        else if (mevcutStok < stokBilgi.KritikStok)
                            lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.OrangeRed;
                        else
                            lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                    else
                    {
                        lblMevcutStok.Text = "Stok: -";
                    }
                }
                catch
                {
                    lblMevcutStok.Text = "Stok: -";
                }
            }
            else
            {
                lblFiyat.Text = "Fiyat: -";
            }

            // Açıklama
            txtAciklama.Text = string.IsNullOrWhiteSpace(_urunModel.Aciklama)
                ? "Açıklama bulunmuyor."
                : TextHelper.FixEncoding(_urunModel.Aciklama);
        }

        #endregion

        #region Görsel Yönetimi

        /// <summary>
        /// Ürün görsellerini yükler ve gallery'yi doldurur.
        /// </summary>
        private void LoadGorseller()
        {
            try
            {
                _gorseller = InterfaceFactory.Urun.GorselListele(_urunId);

                if (_gorseller == null || _gorseller.Count == 0)
                {
                    // Placeholder göster
                    picGorsel.Image = null;
                    lblGorselSayisi.Text = "Görsel yok";
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    return;
                }

                // Ana görseli ilk sıraya getir
                _gorseller = _gorseller.OrderByDescending(g => g.AnaGorsel)
                    .ThenBy(g => g.Sira)
                    .ToList();

                _currentImageIndex = 0;
                ShowCurrentImage();
                UpdateImageNavigation();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadGorseller error: {0}", ex.Message), "URUN");
                picGorsel.Image = null;
                lblGorselSayisi.Text = "Görsel yüklenemedi";
            }
        }

        /// <summary>
        /// Mevcut index'teki görseli gösterir.
        /// </summary>
        private void ShowCurrentImage()
        {
            if (_gorseller == null || _gorseller.Count == 0)
                return;

            var gorsel = _gorseller[_currentImageIndex];

            if (File.Exists(gorsel.GorselPath))
            {
                try
                {
                    // Stream ile yükle (file lock önleme)
                    using (var stream = new FileStream(gorsel.GorselPath, FileMode.Open, FileAccess.Read))
                    {
                        picGorsel.Image = Image.FromStream(stream);
                    }

                    lblGorselSayisi.Text = string.Format("{0} / {1}",
                        _currentImageIndex + 1, _gorseller.Count);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogMessage(string.Format("ShowCurrentImage error: {0}", ex.Message), "URUN");
                    picGorsel.Image = null;
                }
            }
            else
            {
                picGorsel.Image = null;
            }
        }

        /// <summary>
        /// Görsel navigasyon butonlarının durumunu günceller.
        /// </summary>
        private void UpdateImageNavigation()
        {
            if (_gorseller == null || _gorseller.Count <= 1)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                return;
            }

            btnPrev.Enabled = _currentImageIndex > 0;
            btnNext.Enabled = _currentImageIndex < _gorseller.Count - 1;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentImageIndex > 0)
            {
                _currentImageIndex--;
                ShowCurrentImage();
                UpdateImageNavigation();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_gorseller != null && _currentImageIndex < _gorseller.Count - 1)
            {
                _currentImageIndex++;
                ShowCurrentImage();
                UpdateImageNavigation();
            }
        }

        #endregion

        #region AI İçerik Yönetimi

        /// <summary>
        /// AI içeriğini yükler. Yoksa otomatik üretir.
        /// </summary>
        private void LoadAiIcerik()
        {
            try
            {
                // 1. Mevcut içeriği kontrol et (AKTIF durumdaki)
                _aiIcerik = InterfaceFactory.Ai.IcerikGetir(_urunId);

                if (_aiIcerik != null && _aiIcerik.Durum == "AKTIF")
                {
                    // Mevcut içerik var, göster
                    ShowAiContent(_aiIcerik.Icerik);
                    lblAiDurum.Text = string.Format("AI Durum: {0} (Oluşturma: {1:dd.MM.yyyy})",
                        _aiIcerik.Durum, _aiIcerik.OlusturmaTarih);
                    lblAiDurum.ForeColor = Color.Green;
                }
                else
                {
                    // İçerik yok - placeholder göster, otomatik üretme
                    ShowAiPlaceholder();
                    lblAiDurum.Text = "AI Durum: İçerik yok";
                    lblAiDurum.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadAiIcerik error: {0}", ex.Message), "URUN");
                ShowAiError("AI içerik yüklenirken hata oluştu.");
            }
        }

        /// <summary>
        /// AI içeriğini otomatik üretir (senkron).
        /// </summary>
        private void GenerateAiContent()
        {
            try
            {
                // Progress gösterimi
                lblAiDurum.Text = "AI Durum: İçerik üretiliyor...";
                lblAiDurum.ForeColor = Color.Orange;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents(); // UI yenileme

                // AI üretim çağrısı
                var sonuc = InterfaceFactory.Ai.IcerikUret(_urunId, "URUN_DETAY_V1");

                Cursor.Current = Cursors.Default;

                if (sonuc.Basarili)
                {
                    // Başarılı, içeriği göster
                    ShowAiContent(sonuc.UretilenIcerik);
                    lblAiDurum.Text = "AI Durum: Yeni üretildi";
                    lblAiDurum.ForeColor = Color.Blue;
                }
                else
                {
                    // Hata
                    ShowAiError(string.Format("AI üretim hatası: {0}", sonuc.Hata));
                    lblAiDurum.Text = "AI Durum: Üretim başarısız";
                    lblAiDurum.ForeColor = Color.Red;

                    // Retry seçeneği sun
                    var retry = MessageHelper.ShowConfirmation(
                        "AI içerik üretimi başarısız oldu. Tekrar denemek ister misiniz?"
                    );

                    if (retry)
                    {
                        GenerateAiContent(); // Recursive retry
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorManager.LogMessage(string.Format("GenerateAiContent error: {0}", ex.Message), "AI");
                ShowAiError("AI içerik üretimi sırasında beklenmeyen hata.");
                lblAiDurum.Text = "AI Durum: Hata";
                lblAiDurum.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// AI JSON içeriğini parse eder ve UI'ye yerleştirir.
        /// </summary>
        private void ShowAiContent(string jsonContent)
        {
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                ShowAiError("AI içerik boş.");
                return;
            }

            try
            {
                var content = JsonConvert.DeserializeObject<AiContentJsonModel>(jsonContent);

                if (content != null)
                {
                    // Tab kontrol ile göster
                    txtFayda.Text = string.IsNullOrWhiteSpace(content.Fayda)
                        ? "Bilgi bulunmuyor."
                        : TextHelper.FixEncoding(content.Fayda);

                    txtKullanim.Text = string.IsNullOrWhiteSpace(content.Kullanim)
                        ? "Bilgi bulunmuyor."
                        : TextHelper.FixEncoding(content.Kullanim);

                    txtUyari.Text = string.IsNullOrWhiteSpace(content.Uyari)
                        ? "Bilgi bulunmuyor."
                        : TextHelper.FixEncoding(content.Uyari);

                    txtKombinasyon.Text = string.IsNullOrWhiteSpace(content.Kombinasyon)
                        ? "Bilgi bulunmuyor."
                        : TextHelper.FixEncoding(content.Kombinasyon);
                }
                else
                {
                    ShowAiError("AI içerik parse edilemedi.");
                }
            }
            catch (JsonException)
            {
                // JSON değilse ham metin olarak faydaya ata
                txtFayda.Text = TextHelper.FixEncoding(jsonContent);
                txtKullanim.Text = "JSON formatında değil.";
                txtUyari.Text = "-";
                txtKombinasyon.Text = "-";
            }
        }

        /// <summary>
        /// AI içeriği olmadığında placeholder gösterir.
        /// </summary>
        private void ShowAiPlaceholder()
        {
            txtFayda.Text = "AI içeriği bulunmuyor. Ürün Kartı > AI sekmesinden içerik üretebilirsiniz.";
            txtKullanim.Text = "-";
            txtUyari.Text = "-";
            txtKombinasyon.Text = "-";
        }

        /// <summary>
        /// AI hata durumunda boş alanları gösterir.
        /// </summary>
        private void ShowAiError(string message)
        {
            txtFayda.Text = string.Format("HATA: {0}", message);
            txtKullanim.Text = "-";
            txtUyari.Text = "-";
            txtKombinasyon.Text = "-";
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Ürünü düzenleme modunda açar (URUN_KART).
        /// </summary>
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (ParentFrm != null && ParentFrm.MdiParent != null)
                {
                    NavigationManager.OpenScreen("URUN_KART", ParentFrm.MdiParent, _urunId);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnDuzenle_Click error: {0}", ex.Message), "URUN");
                MessageHelper.ShowError("Ürün kartı açılamadı: " + ex.Message);
            }
        }

        /// <summary>
        /// Ekranı kapatır.
        /// </summary>
        private void btnKapat_Click(object sender, EventArgs e)
        {
            if (ParentFrm != null)
            {
                ParentFrm.Close();
            }
        }

        private void btnAiFayda_Click(object sender, EventArgs e)
        {
            GenerateAiContentForField("Fayda", txtFayda);
        }

        private void btnAiKullanim_Click(object sender, EventArgs e)
        {
            GenerateAiContentForField("Kullanim", txtKullanim);
        }

        private void btnAiUyari_Click(object sender, EventArgs e)
        {
            GenerateAiContentForField("Uyari", txtUyari);
        }

        private void btnAiKombinasyon_Click(object sender, EventArgs e)
        {
            GenerateAiContentForField("Kombinasyon", txtKombinasyon);
        }

        /// <summary>
        /// Generates AI content for a specific field
        /// </summary>
        private void GenerateAiContentForField(string fieldName, DevExpress.XtraEditors.MemoEdit targetControl)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Use same template but extract only the requested field
                var result = InterfaceFactory.Ai.IcerikUret(_urunId, "URUN_DETAY_V1");

                Cursor.Current = Cursors.Default;

                if (result.Basarili)
                {
                    try
                    {
                        // Extract JSON from markdown
                        string jsonContent = ExtractJsonFromMarkdown(result.UretilenIcerik);
                        var content = JsonConvert.DeserializeObject<AiContentJsonModel>(jsonContent);

                        if (content != null)
                        {
                            // Set only the requested field
                            string fieldValue = "";
                            switch (fieldName)
                            {
                                case "Fayda":
                                    fieldValue = content.Fayda ?? "";
                                    break;
                                case "Kullanim":
                                    fieldValue = content.Kullanim ?? "";
                                    break;
                                case "Uyari":
                                    fieldValue = content.Uyari ?? "";
                                    break;
                                case "Kombinasyon":
                                    fieldValue = content.Kombinasyon ?? "";
                                    break;
                            }

                            targetControl.Text = TextHelper.FixEncoding(fieldValue);
                            lblAiDurum.Text = string.Format("AI Durum: {0} içeriği üretildi", fieldName);
                            lblAiDurum.ForeColor = Color.Green;
                            MessageHelper.ShowSuccess(string.Format("{0} içeriği başarıyla üretildi.", fieldName));
                        }
                    }
                    catch (Exception parseEx)
                    {
                        ErrorManager.LogMessage("JSON Parse Error: " + parseEx.Message, "AI");
                        targetControl.Text = result.UretilenIcerik;
                        MessageHelper.ShowInfo("İçerik üretildi ancak JSON formatında ayrıştırılamadı.");
                    }
                }
                else
                {
                    MessageHelper.ShowError("AI Üretim Hatası: " + result.Hata);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorManager.LogMessage("AI Gen Error: " + ex.Message, "AI");
                MessageHelper.ShowError("İşlem sırasında hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Extracts JSON from markdown code blocks (```json ... ```)
        /// </summary>
        private string ExtractJsonFromMarkdown(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Remove markdown code block wrappers
            text = text.Trim();

            // Check if wrapped in code blocks
            if (text.StartsWith("```"))
            {
                // Find first line break after ```
                int firstNewLine = text.IndexOf('\n');
                if (firstNewLine > 0)
                {
                    // Remove first line (```json or ```)
                    text = text.Substring(firstNewLine + 1);
                }

                // Remove trailing ```
                if (text.EndsWith("```"))
                {
                    text = text.Substring(0, text.Length - 3);
                }

                text = text.Trim();
            }

            return text;
        }

        #endregion
    }
}
