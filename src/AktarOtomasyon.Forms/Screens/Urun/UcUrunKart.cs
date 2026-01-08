using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using AktarOtomasyon.Util.DataAccess;
using Newtonsoft.Json; // Adding JsonConvert usage

namespace AktarOtomasyon.Forms.Screens.Urun
{
    /// <summary>
    /// Ürün kartı UserControl.
    /// UC-Only Pattern: Tüm iş mantığı bu sınıfta.
    /// 4 Tab: Genel, Stok Ayar, Görseller, AI
    /// </summary>
    public partial class UcUrunKart : UcBase
    {
        private int? _urunId = null;
        private UrunModel _urunModel = null;
        private UrunStokAyarDto _stokAyarDto = null;
        private AiIcerikModel _aiIcerikModel = null;
        private bool _dataChanged = false;

        private class AiContentJsonModel
        {
            public string Fayda { get; set; }
            public string Kullanim { get; set; }
            public string Uyari { get; set; }
            public string Kombinasyon { get; set; }
        }

        // Geçici görsel yönetimi için alanlar
        private string _tempFolderGuid = null;
        private string _tempFolderPath = null;
        private string _mainImageTempPath = null;
        private List<string> _tempImagePaths = new List<string>();

        public UcUrunKart()
        {
            InitializeComponent();
            ApplyStandards();
        }

        private void AttachFormClosingHandler()
        {
            if (ParentFrm != null)
            {
                ParentFrm.FormClosing -= ParentFrm_FormClosing; // Önce eski handler'ı kaldır (çift bağlantı önleme)
                ParentFrm.FormClosing += ParentFrm_FormClosing;
            }
        }

        private void ParentFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Sadece yeni ürün modunda ve geçici görseller varsa kontrol et
            if (_urunId == null && !string.IsNullOrEmpty(_tempFolderGuid))
            {
                if (_tempImagePaths != null && _tempImagePaths.Count > 0)
                {
                    var result = MessageBox.Show(
                        "Kaydedilmemiş görseller var. Çıkmak istediğinize emin misiniz?",
                        "Uyarı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                // Kullanıcı çıkmayı onayladı, geçici klasörü temizle
                ImageFileHelper.CleanupTempFolder(_tempFolderGuid);
            }
        }

        private void ApplyStandards()
        {
             // Format Image Grid
             // GridHelper.ApplyStandardFormatting(gvGorseller); // Removed
             
             // Format Lookups
             var katView = lkpKategori.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
             if (katView != null) GridHelper.ApplyStandardFormatting(katView);
             
             var birimView = lkpBirim.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
             if (birimView != null) GridHelper.ApplyStandardFormatting(birimView);
        }

        /// <summary>
        /// Verileri yükler. UrunId null ise yeni kayıt modu, değer varsa düzenleme modu.
        /// </summary>
        public void LoadData(int? urunId)
        {
            try
            {
                _urunId = urunId;
                _dataChanged = false;

                // Lookup verilerini yükle
                LoadKategoriLookup();
                LoadBirimLookup();

                if (_urunId == null)
                {
                    // Yeni ürün modu
                    InitializeNewProduct();
                    DisableTabsExceptGenel();

                    // Görsel yönetimini yeni ürün moduna ayarla
                    ucGorselYonetim.SetNewProductMode(true, _tempFolderPath, _tempImagePaths);

                    // Form closing handler'ı ekle (geçici klasör temizleme için)
                    AttachFormClosingHandler();

                    lblDurum.Text = "Yeni Ürün";
                }
                else
                {
                    // Düzenleme modu
                    LoadUrunData(_urunId.Value);
                    EnableAllTabs();

                    // Görsel yönetimini düzenleme moduna ayarla
                    ucGorselYonetim.SetNewProductMode(false, null, null);
                    ucGorselYonetim.SetUrunId(_urunId.Value);

                    // Ana görseli yükle
                    LoadMainImagePreview(_urunId.Value);

                    // AI verilerini yükle
                    LoadAiData(_urunId.Value);

                    lblDurum.Text = string.Format("Düzenleniyor: {0}", TextHelper.FixEncoding(_urunModel.UrunAdi));
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("UcUrunKart.LoadData error: {0}", ex.Message), "URUN");
                MessageBox.Show(string.Format("Veri yükleme hatası: {0}", ex.Message), "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Data Loading

        private void LoadKategoriLookup()
        {
            var kategoriler = UrunLookupProvider.GetKategoriler();
            lkpKategori.Properties.DataSource = kategoriler;
            lkpKategori.Properties.DisplayMember = "KategoriAdi";
            lkpKategori.Properties.ValueMember = "KategoriId";
            
            // Configure Columns
            var view = lkpKategori.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null) 
            {
               view.PopulateColumns();
               if (view.Columns["KategoriId"] != null) view.Columns["KategoriId"].Visible = false;
               if (view.Columns["UstKategoriId"] != null) view.Columns["UstKategoriId"].Visible = false;
               if (view.Columns["KategoriAdi"] != null) view.Columns["KategoriAdi"].Caption = "Kategori";
               view.BestFitColumns();
               lkpKategori.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
        }

        private void LoadBirimLookup()
        {
            var birimler = UrunLookupProvider.GetBirimler();
            lkpBirim.Properties.DataSource = birimler;
            lkpBirim.Properties.DisplayMember = "BirimAdi";
            lkpBirim.Properties.ValueMember = "BirimId";

            // Configure Columns
            var view = lkpBirim.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null) 
            {
               view.PopulateColumns();
               if (view.Columns["BirimId"] != null) view.Columns["BirimId"].Visible = false;
               if (view.Columns["BirimAdi"] != null) view.Columns["BirimAdi"].Caption = "Birim";
               view.BestFitColumns();
               lkpBirim.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
        }

        private void InitializeNewProduct()
        {
            _urunModel = new UrunModel
            {
                UrunId = 0,
                Aktif = true
            };

            _stokAyarDto = new UrunStokAyarDto
            {
                UrunId = 0,
                MinimumStok = 0,
                HedefStok = 0,
                EmniyetStoku = 0,
                TedarikSuresi = 0
            };

            // UI'yi temizle
            ClearData();
            chkAktif.Checked = true;

            // Geçici klasör oluştur
            var tempResult = ImageFileHelper.CreateTempImageFolder();
            if (tempResult != null)
            {
                _tempFolderGuid = tempResult.Item1;
                _tempFolderPath = tempResult.Item2;
                _tempImagePaths = new List<string>();
                _mainImageTempPath = null;
            }
        }

        private void LoadUrunData(int urunId)
        {
            // Ürün verilerini yükle
            _urunModel = InterfaceFactory.Urun.Getir(urunId);
            if (_urunModel == null)
            {
                MessageBox.Show("Ürün bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // UI'ye doldur
            txtBarkod.Text = _urunModel.Barkod;
            txtUrunKod.Text = _urunModel.UrunKod;
            txtUrunAdi.Text = TextHelper.FixEncoding(_urunModel.UrunAdi);
            lkpKategori.EditValue = _urunModel.KategoriId;
            lkpBirim.EditValue = _urunModel.BirimId;
            txtAciklama.Text = _urunModel.Aciklama;
            chkAktif.Checked = _urunModel.Aktif;

            // Stok ayarlarını yükle
            LoadStokAyar(urunId);
        }

        private void LoadStokAyar(int urunId)
        {
            _stokAyarDto = InterfaceFactory.Urun.StokAyarGetir(urunId);
            if (_stokAyarDto == null)
            {
                // Stok ayarı yoksa yeni oluştur
                _stokAyarDto = new UrunStokAyarDto
                {
                    UrunId = urunId,
                    MinimumStok = 0,
                    HedefStok = 0,
                    EmniyetStoku = 0,
                    TedarikSuresi = 0
                };
            }

            spnMinimumStok.EditValue = _stokAyarDto.MinimumStok;
            spnHedefStok.EditValue = _stokAyarDto.HedefStok;
            spnEmniyetStoku.EditValue = _stokAyarDto.EmniyetStoku;
            spnTedarikSuresi.EditValue = _stokAyarDto.TedarikSuresi;
        }

        private void LoadAiData(int urunId)
        {
            try
            {
                _aiIcerikModel = InterfaceFactory.Ai.IcerikGetir(urunId);
                
                if (_aiIcerikModel == null)
                {
                    _aiIcerikModel = new AiIcerikModel { UrunId = urunId, Durum = "Taslak" };
                    ClearAiFields();
                    lblAiDurum.Text = "Durum: Taslak (Yeni)";
                    lblAiTarih.Text = "Son Güncelleme: -";
                }
                else
                {
                    lblAiDurum.Text = "Durum: " + _aiIcerikModel.Durum;
                    lblAiTarih.Text = "Son Güncelleme: " + _aiIcerikModel.OlusturmaTarih.ToString("g");

                    if (!string.IsNullOrEmpty(_aiIcerikModel.Icerik))
                    {
                        try
                        {
                            var content = JsonConvert.DeserializeObject<AiContentJsonModel>(_aiIcerikModel.Icerik);
                            if (content != null)
                            {
                                txtFayda.Text = TextHelper.FixEncoding(content.Fayda ?? "");
                                txtKullanim.Text = TextHelper.FixEncoding(content.Kullanim ?? "");
                                txtUyari.Text = TextHelper.FixEncoding(content.Uyari ?? "");
                                txtKombinasyon.Text = TextHelper.FixEncoding(content.Kombinasyon ?? "");
                            }
                        }
                        catch
                        {
                            // JSON değilse ham metin olarak Fayda'ya atalım veya loglayalım
                            txtFayda.Text = TextHelper.FixEncoding(_aiIcerikModel.Icerik);
                        }
                    }
                    else
                    {
                        ClearAiFields();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("AI Data Load Error: " + ex.Message, "AI");
            }
        }

        private void ClearAiFields()
        {
            txtFayda.Text = "";
            txtKullanim.Text = "";
            txtUyari.Text = "";
            txtKombinasyon.Text = "";
        }



        #endregion

        #region Tab Management

        private void DisableTabsExceptGenel()
        {
            tabStokAyar.PageEnabled = false;
            // tabGorseller.PageEnabled = false; // Artık yeni ürünlerde de aktif
            tabAI.PageEnabled = false;
        }

        private void EnableAllTabs()
        {
            tabStokAyar.PageEnabled = true;
            tabGorseller.PageEnabled = true;
            tabAI.PageEnabled = true;
        }

        #endregion

        #region Validation

        private bool ValidateGenelTab()
        {
            if (string.IsNullOrWhiteSpace(txtUrunAdi.Text))
            {
                MessageBox.Show("Ürün adı zorunludur.", "Doğrulama Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTabPage = tabGenel;
                txtUrunAdi.Focus();
                return false;
            }

            if (lkpKategori.EditValue == null)
            {
                MessageBox.Show("Kategori seçimi zorunludur.", "Doğrulama Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTabPage = tabGenel;
                lkpKategori.Focus();
                return false;
            }

            if (lkpBirim.EditValue == null)
            {
                MessageBox.Show("Birim seçimi zorunludur.", "Doğrulama Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTabPage = tabGenel;
                lkpBirim.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Save Operations

        public override string SaveData()
        {
            try
            {
                if (!ValidateGenelTab())
                {
                    return "Validation failed";
                }

                // UrunModel'i UI'den doldur
                if (_urunModel == null)
                    _urunModel = new UrunModel();

                _urunModel.Barkod = txtBarkod.Text.Trim();
                _urunModel.UrunKod = txtUrunKod.Text.Trim();
                _urunModel.UrunAdi = txtUrunAdi.Text.Trim();
                _urunModel.KategoriId = (int)lkpKategori.EditValue;
                _urunModel.BirimId = (int)lkpBirim.EditValue;
                _urunModel.Aciklama = txtAciklama.Text.Trim();
                _urunModel.Aktif = chkAktif.Checked;

                // StokAyarDto'yu UI'den doldur
                if (_stokAyarDto == null)
                    _stokAyarDto = new UrunStokAyarDto();

                _stokAyarDto.UrunId = _urunModel.UrunId;
                _stokAyarDto.MinimumStok = (int)spnMinimumStok.Value;
                _stokAyarDto.HedefStok = (int)spnHedefStok.Value;
                _stokAyarDto.EmniyetStoku = (int)spnEmniyetStoku.Value;
                _stokAyarDto.TedarikSuresi = (int)spnTedarikSuresi.Value;

                // Transaction ile kaydet
                var error = InterfaceFactory.Urun.UrunVeStokAyarKaydet(_urunModel, _stokAyarDto);
                DMLManager.KaydetKontrol(error);

                // AI verisini kaydet (Düzenleme modu)
                if (string.IsNullOrEmpty(error) && _urunId != null)
                {
                    SaveAiData();
                }

                if (string.IsNullOrEmpty(error))
                {
                    _dataChanged = false;

                    // Yeni kayıt ise UrunId'yi al ve tab'ları aktif et
                    if (_urunId == null)
                    {
                        // Yeni kaydedilen ürünü tekrar getir (UrunId için)
                        var savedUrun = InterfaceFactory.Urun.Getir(_urunModel.UrunId);
                        if (savedUrun != null)
                        {
                            _urunId = savedUrun.UrunId;
                            _urunModel = savedUrun;
                            EnableAllTabs();

                            // Geçici görselleri kalıcı konuma taşı ve veritabanına kaydet
                            ProcessTempImages(_urunId.Value);

                            // Ana görseli yükle
                            LoadMainImagePreview(_urunId.Value); // ProcessTempImages çağrılmadan önce LoadMainImagePreview çağrılmıştı. Sıralama önemli.
                            // ProcessTempImages zaten LoadMainImagePreview çağırıyor.

                             // Yeni ürün kaydedildikten sonra AI verisini kaydet (eğer girilmişse)
                            if (_aiIcerikModel != null)
                            {
                                _aiIcerikModel.UrunId = _urunId.Value; // Set ID
                                SaveAiData();
                            }

                            lblDurum.Text = string.Format("Düzenleniyor: {0}", TextHelper.FixEncoding(_urunModel.UrunAdi));
                            
                            // UI'yi güncelle (barkod dahil)
                            txtBarkod.Text = savedUrun.Barkod;
                            txtUrunKod.Text = savedUrun.UrunKod;
                            txtUrunAdi.Text = TextHelper.FixEncoding(savedUrun.UrunAdi);
                        }
                    }
                    else
                    {
                        // Düzenleme modu: Kayıt sonrası verileri tekrar yükle (barkod güncellemesi için)
                        var updatedUrun = InterfaceFactory.Urun.Getir(_urunId.Value);
                        if (updatedUrun != null)
                        {
                            _urunModel = updatedUrun;
                            // UI'yi güncelle (barkod dahil)
                            txtBarkod.Text = updatedUrun.Barkod;
                            txtUrunKod.Text = updatedUrun.UrunKod;
                            txtUrunAdi.Text = TextHelper.FixEncoding(updatedUrun.UrunAdi);
                            lblDurum.Text = string.Format("Düzenleniyor: {0}", TextHelper.FixEncoding(updatedUrun.UrunAdi));
                        }
                    }
                }

                return error;
            }
            catch (Exception ex)
            {
                var errorMsg = string.Format("Kayıt hatası: {0}", ex.Message);
                ErrorManager.LogMessage(string.Format("UcUrunKart.SaveData error: {0}", ex.Message), "URUN");
                MessageBox.Show(errorMsg, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return errorMsg;
            }
        }

        #endregion

        #region Image Management

        /// <summary>
        /// Düzenleme modunda ana görseli yükler ve önizlemede gösterir
        /// </summary>
        private void LoadMainImagePreview(int urunId)
        {
            try
            {
                var gorseller = InterfaceFactory.Urun.GorselListele(urunId);
                var anaGorsel = gorseller.FirstOrDefault(x => x.AnaGorsel);

                if (anaGorsel != null && File.Exists(anaGorsel.GorselPath))
                {
                    // Stream kullanarak yükle (file lock önleme)
                    using (var stream = new FileStream(anaGorsel.GorselPath, FileMode.Open, FileAccess.Read))
                    {
                        picMainImage.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    picMainImage.Image = null;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadMainImagePreview error: {0}", ex.Message), "URUN");
                picMainImage.Image = null;
            }
        }

        /// <summary>
        /// Geçici klasördeki görselleri kalıcı konuma taşır ve veritabanına kaydeder
        /// </summary>
        private void ProcessTempImages(int urunId)
        {
            try
            {
                if (string.IsNullOrEmpty(_tempFolderGuid) || _tempImagePaths == null || _tempImagePaths.Count == 0)
                    return;

                // Geçici görselleri kalıcı konuma taşı
                var movedFiles = ImageFileHelper.MoveTempImagesToPermanent(_tempFolderGuid, urunId);

                if (movedFiles != null && movedFiles.Count > 0)
                {
                    // Her taşınan görsel için DB kaydı oluştur
                    for (int i = 0; i < movedFiles.Count; i++)
                    {
                        string tempPath = movedFiles[i].Item1;
                        string relativePath = movedFiles[i].Item2;

                        // İlk görsel veya ana görsel olarak işaretlenen = ana görsel
                        bool isMainImage = (i == 0) || (!string.IsNullOrEmpty(_mainImageTempPath) && tempPath == _mainImageTempPath);

                        SaveImageToDatabase(urunId, relativePath, i + 1, isMainImage);
                    }

                    // Geçici referansları temizle
                    _tempFolderGuid = null;
                    _tempFolderPath = null;
                    _tempImagePaths.Clear();
                    _mainImageTempPath = null;

                    // Gallery'yi yeniden yükle (artık düzenleme modunda)
                    ucGorselYonetim.SetNewProductMode(false, null, null);
                    ucGorselYonetim.SetUrunId(urunId);

                    // Ana görseli yükle
                    LoadMainImagePreview(urunId);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("ProcessTempImages error: {0}", ex.Message), "URUN");
                throw; // SaveData'da yakalanacak
            }
        }

        /// <summary>
        /// Görsel kaydını doğrudan veritabanına ekler (dosya zaten taşınmış)
        /// </summary>
        private void SaveImageToDatabase(int urunId, string gorselPath, int sira, bool anaGorsel)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_gorsel_ekle");

                    cmd.Parameters.AddWithValue("@urun_id", urunId);
                    cmd.Parameters.AddWithValue("@gorsel_path", gorselPath);
                    cmd.Parameters.AddWithValue("@gorsel_tip", "Urun"); // Parametre adı düzeltildi: @gorsel_tipi -> @gorsel_tip
                    cmd.Parameters.AddWithValue("@ana_gorsel", anaGorsel);
                    // @sira kaldırıldı - SP otomatik hesaplıyor

                    // Output parametresi (opsiyonel)
                    var outputParam = new SqlParameter("@gorsel_id", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    sMan.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("SaveImageToDatabase error: {0}", ex.Message), "URUN");
                throw;
            }
        }

        #endregion

        #region Event Handlers - Buttons

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnKaydetVeKapat_Click(object sender, EventArgs e)
        {
            var error = SaveData();
            if (string.IsNullOrEmpty(error))
            {
                if (ParentFrm != null)
                {
                    ParentFrm.Close();
                }
            }
        }

        private void btnGorselSec_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Multiselect = false;
                    ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*";
                    ofd.Title = "Ana Görsel Seç";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        if (_urunId == null)
                        {
                            // YENİ ÜRÜN MODU: Geçici klasöre kopyala
                            string copiedPath = ImageFileHelper.CopyToTempFolder(ofd.FileName, _tempFolderPath);
                            if (!string.IsNullOrEmpty(copiedPath))
                            {
                                _mainImageTempPath = copiedPath;

                                // Temp image listesine ekle (eğer yoksa)
                                if (!_tempImagePaths.Contains(copiedPath))
                                {
                                    _tempImagePaths.Insert(0, copiedPath); // İlk sıraya ekle (ana görsel)
                                }

                                // Önizlemeyi göster
                                using (var stream = new FileStream(copiedPath, FileMode.Open, FileAccess.Read))
                                {
                                    picMainImage.Image = Image.FromStream(stream);
                                }

                                // Gallery'yi yenile
                                ucGorselYonetim.RefreshTempImages();

                                MessageHelper.ShowSuccess("Ana görsel eklendi. Ürünü kaydettiğinizde kalıcı olacak.");
                            }
                        }
                        else
                        {
                            // DÜZENLEME MODU: Direkt DB'ye kaydet
                            bool isFirst = true; // Ana görsel olarak işaretle
                            InterfaceFactory.Urun.GorselEkle(_urunId.Value, ofd.FileName, "Urun", isFirst);

                            // Önizlemeyi yenile
                            LoadMainImagePreview(_urunId.Value);

                            // Gallery'yi yenile
                            ucGorselYonetim.SetUrunId(_urunId.Value);

                            MessageHelper.ShowSuccess("Ana görsel güncellendi.");
                        }

                        _dataChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnGorselSec_Click error: {0}", ex.Message), "URUN");
                MessageHelper.ShowError("Görsel seçme hatası: " + ex.Message);
            }
        }

        private void btnGorselKaldir_Click(object sender, EventArgs e)
        {
            try
            {
                if (picMainImage.Image != null)
                {
                    picMainImage.Image = null;
                    _mainImageTempPath = null;
                    _dataChanged = true;

                    MessageHelper.ShowInfo("Ana görsel önizlemesi kaldırıldı.");
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnGorselKaldir_Click error: {0}", ex.Message), "URUN");
                MessageHelper.ShowError("Hata: " + ex.Message);
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
            if (_urunId == null)
            {
                MessageBox.Show("AI içerik üretmek için önce ürünü kaydetmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Use same template but extract only the requested field
                var result = InterfaceFactory.Ai.IcerikUret(_urunId.Value, "URUN_DETAY_V1");

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
                            MessageHelper.ShowSuccess(string.Format("{0} içeriği başarıyla üretildi.", fieldName));
                            _dataChanged = true;
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
            // Common patterns: ```json\n{...}\n``` or ```\n{...}\n```
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

        private void SaveAiData()
        {
            if (_aiIcerikModel == null) return;
            
            try
            {
                // CRITICAL: Fix encoding before saving to database
                var content = new AiContentJsonModel
                {
                    Fayda = TextHelper.FixEncoding(txtFayda.Text),
                    Kullanim = TextHelper.FixEncoding(txtKullanim.Text),
                    Uyari = TextHelper.FixEncoding(txtUyari.Text),
                    Kombinasyon = TextHelper.FixEncoding(txtKombinasyon.Text)
                };

                _aiIcerikModel.Icerik = JsonConvert.SerializeObject(content);
                // _aiIcerikModel.Durum = "Taslak"; // Keep existing state or auto-update?

                InterfaceFactory.Ai.IcerikKaydet(_aiIcerikModel);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("SaveAiData Error: " + ex.Message, "AI");
            }
        }

        #endregion



        #region Change Tracking

        public override bool HasChanges()
        {
            return _dataChanged;
        }

        public void OnInputChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }

        #endregion

        #region Cleanup

        public override void ClearData()
        {
            txtBarkod.Text = string.Empty;
            txtUrunKod.Text = string.Empty;
            txtUrunAdi.Text = string.Empty;
            lkpKategori.EditValue = null;
            lkpBirim.EditValue = null;
            txtAciklama.Text = string.Empty;
            chkAktif.Checked = true;

            spnMinimumStok.EditValue = 0;
            spnHedefStok.EditValue = 0;
            spnEmniyetStoku.EditValue = 0;
            spnTedarikSuresi.EditValue = 0;

            spnTedarikSuresi.EditValue = 0;

            // grdGorseller.DataSource = null;
            // picPreview.Image = null;
            ucGorselYonetim.SetUrunId(0);

            picMainImage.Image = null;
            _mainImageTempPath = null;

            _dataChanged = false;
        }

        #endregion


    }
}
