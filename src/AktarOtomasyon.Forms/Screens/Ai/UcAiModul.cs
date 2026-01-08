using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Ai.Interface;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Urun.Interface;

namespace AktarOtomasyon.Forms.Screens.Ai
{
    public partial class UcAiModul : UcBase
    {
        private int? _urunId;
        private AiIcerikModel _currentIcerik;
        private string _currentSablonKod;
        private bool _dataChanged;

        public UcAiModul()
        {
            InitializeComponent();
            _currentIcerik = new AiIcerikModel();
            _dataChanged = false;
        }

        public void LoadData(int? urunId)
        {
            try
            {
                _urunId = urunId;

                LoadUrunLookup();
                LoadSablonLookup();

                if (_urunId.HasValue)
                {
                    lkpUrun.EditValue = _urunId.Value;
                    lkpUrun.Enabled = false;
                    LoadExistingContent(_urunId.Value);
                }
                else
                {
                    lkpUrun.Enabled = true;
                }

                progressBarUretim.Visible = false;
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcAiModul.LoadData hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadUrunLookup()
        {
            try
            {
                var urunList = InterfaceFactory.Urun.Listele();

                lkpUrun.Properties.DataSource = urunList;
                lkpUrun.Properties.DisplayMember = "UrunAdi";
                lkpUrun.Properties.ValueMember = "UrunId";
                lkpUrun.Properties.NullText = "Ürün seçiniz...";

                // Configure search
                lkpUrun.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                lkpUrun.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("LoadUrunLookup hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("Ürün listesi yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadSablonLookup()
        {
            try
            {
                var sablonList = InterfaceFactory.Ai.SablonListele(aktif: true);

                lkpSablon.Properties.DataSource = sablonList;
                lkpSablon.Properties.DisplayMember = "SablonAdi";
                lkpSablon.Properties.ValueMember = "SablonKod";
                lkpSablon.Properties.NullText = "Şablon seçiniz...";

                // Configure search
                lkpSablon.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                lkpSablon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("LoadSablonLookup hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("Şablon listesi yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadExistingContent(int urunId)
        {
            try
            {
                // Get existing active content
                var existingIcerik = InterfaceFactory.Ai.IcerikGetir(urunId);

                if (existingIcerik != null)
                {
                    _currentIcerik = existingIcerik;
                    memoIcerik.Text = existingIcerik.Icerik;
                    UpdateGuvenlikDurum(existingIcerik.Icerik);
                    lblDurum.Text = string.Format("Durum: {0}", existingIcerik.Durum);
                    _dataChanged = false;
                }
                else
                {
                    _currentIcerik = new AiIcerikModel { UrunId = urunId };
                    memoIcerik.Text = string.Empty;
                    lblGuvenlikDurum.Text = string.Empty;
                    lblDurum.Text = "Durum: İçerik henüz oluşturulmamış";
                    _dataChanged = false;
                }

                // Update product info label
                var urun = InterfaceFactory.Urun.Getir(urunId);
                if (urun != null)
                {
                    lblUrunBilgi.Text = string.Format("Seçili Ürün: {0} - {1}", urun.UrunKod, urun.UrunAdi);
                }

                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("LoadExistingContent hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("İçerik yüklenirken hata: " + ex.Message);
            }
        }

        private void btnUret_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (!_urunId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen ürün seçiniz.");
                    lkpUrun.Focus();
                    return;
                }

                if (lkpSablon.EditValue == null)
                {
                    DMLManager.ShowWarning("Lütfen şablon seçiniz.");
                    lkpSablon.Focus();
                    return;
                }

                _currentSablonKod = lkpSablon.EditValue.ToString();

                // Show progress
                progressBarUretim.Visible = true;
                btnUret.Enabled = false;
                Application.DoEvents();

                // Generate content
                var sonuc = InterfaceFactory.Ai.IcerikUret(_urunId.Value, _currentSablonKod);

                // Hide progress
                progressBarUretim.Visible = false;
                btnUret.Enabled = true;

                if (sonuc.Basarili)
                {
                    memoIcerik.Text = sonuc.UretilenIcerik;
                    UpdateGuvenlikDurum(sonuc.UretilenIcerik);
                    lblDurum.Text = "Durum: TASLAK (Otomatik Kaydedildi)";

                    _currentIcerik.UrunId = _urunId.Value;
                    _currentIcerik.Icerik = sonuc.UretilenIcerik;
                    _currentIcerik.Durum = "TASLAK";
                    _currentIcerik.SablonKod = _currentSablonKod;

                    // Auto-save as draft
                    var error = InterfaceFactory.Ai.IcerikKaydet(_currentIcerik);
                    if (error == null)
                    {
                        DMLManager.ShowInfo("İçerik başarıyla oluşturuldu ve taslak olarak kaydedildi.");
                        LoadExistingContent(_urunId.Value);
                    }
                    else
                    {
                        DMLManager.ShowWarning("İçerik oluşturuldu ancak kayıt sırasında hata: " + error);
                    }
                }
                else
                {
                    DMLManager.ShowError("İçerik üretimi başarısız: " + sonuc.Hata);
                }
            }
            catch (Exception ex)
            {
                progressBarUretim.Visible = false;
                btnUret.Enabled = true;
                ErrorManager.LogMessage("btnUret_Click hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("İçerik üretilirken hata: " + ex.Message);
            }
        }

        private void UpdateGuvenlikDurum(string icerik)
        {
            if (string.IsNullOrWhiteSpace(icerik))
            {
                lblGuvenlikDurum.Text = string.Empty;
                return;
            }

            var forbiddenKeywords = new[] {
                "tedavi eder", "iyileştirir", "kanser", "diyabet",
                "hastalık", "tedavi", "ilaç", "şifa"
            };

            var icerikLower = icerik.ToLowerInvariant();
            bool hasForbidden = forbiddenKeywords.Any(k => icerikLower.Contains(k));

            if (hasForbidden)
            {
                lblGuvenlikDurum.Text = "⚠ Uyarı: İçerik yasak ifadeler içerebilir!";
                lblGuvenlikDurum.ForeColor = Color.Red;
            }
            else
            {
                lblGuvenlikDurum.Text = "✓ Güvenlik kontrolü: OK";
                lblGuvenlikDurum.ForeColor = Color.Green;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (!_urunId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen ürün seçiniz.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(memoIcerik.Text))
                {
                    DMLManager.ShowWarning("İçerik boş olamaz.");
                    return;
                }

                _currentIcerik.UrunId = _urunId.Value;
                _currentIcerik.Icerik = memoIcerik.Text;
                _currentIcerik.Durum = "TASLAK";

                var error = InterfaceFactory.Ai.IcerikKaydet(_currentIcerik);

                if (DMLManager.KaydetKontrol(error))
                {
                    _dataChanged = false;
                    LoadExistingContent(_urunId.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnKaydet_Click hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("Kayıt sırasında hata: " + ex.Message);
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentIcerik == null || _currentIcerik.IcerikId == 0)
                {
                    DMLManager.ShowWarning("Onaylanacak içerik bulunamadı. Lütfen önce içerik oluşturun.");
                    return;
                }

                var result = MessageBox.Show(
                    "İçeriği onaylamak istediğinizden emin misiniz? Bu işlem içeriği aktif hale getirecektir.",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                var error = InterfaceFactory.Ai.IcerikOnayla(_currentIcerik.IcerikId);

                if (DMLManager.IslemKontrol(error, "İçerik başarıyla onaylandı ve aktif hale getirildi."))
                {
                    LoadExistingContent(_urunId.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnOnayla_Click hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("Onaylama sırasında hata: " + ex.Message);
            }
        }

        private void btnVersiyonGecmisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_urunId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen ürün seçiniz.");
                    return;
                }

                NavigationManager.OpenScreen("AI_VERSIYON", this.ParentForm.MdiParent, _urunId.Value);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnVersiyonGecmisi_Click hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("Versiyon geçmişi açılırken hata: " + ex.Message);
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dataChanged)
                {
                    var result = MessageBox.Show(
                        "Kaydedilmemiş değişiklikler var. Vazgeçmek istediğinizden emin misiniz?",
                        "Onay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                        return;
                }

                if (_urunId.HasValue)
                {
                    LoadExistingContent(_urunId.Value);
                }
                else
                {
                    memoIcerik.Text = string.Empty;
                    lblGuvenlikDurum.Text = string.Empty;
                    lblDurum.Text = string.Empty;
                    _currentIcerik = new AiIcerikModel();
                    _dataChanged = false;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnVazgec_Click hata: " + ex.Message, "AI_MODUL");
                DMLManager.ShowError("İşlem sırasında hata: " + ex.Message);
            }
        }

        private void lkpUrun_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpUrun.EditValue != null)
            {
                _urunId = Convert.ToInt32(lkpUrun.EditValue);
                LoadExistingContent(_urunId.Value);
            }
        }

        private void memoIcerik_TextChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
            UpdateGuvenlikDurum(memoIcerik.Text);
        }

        private void UpdateButtonStates()
        {
            bool hasUrun = _urunId.HasValue;
            bool hasContent = !string.IsNullOrWhiteSpace(memoIcerik.Text);
            bool hasIcerikId = _currentIcerik != null && _currentIcerik.IcerikId > 0;

            btnUret.Enabled = hasUrun;
            btnKaydet.Enabled = hasUrun && hasContent;
            btnOnayla.Enabled = hasIcerikId && _currentIcerik.Durum == "TASLAK";
            btnVersiyonGecmisi.Enabled = hasUrun;
        }

        public override bool HasChanges()
        {
            return _dataChanged;
        }
    }
}
