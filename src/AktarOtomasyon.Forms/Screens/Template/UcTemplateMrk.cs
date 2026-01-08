using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Template.Interface.Models;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Template
{
    /// <summary>
    /// Template Management Center - Main UserControl
    /// UC-only pattern: All business logic here
    /// </summary>
    public partial class UcTemplateMrk : UcBase
    {
        private BindingList<TemplateListeItemDto> _templateList;
        private BindingList<TemplateVersionListeItemDto> _versionList;
        private TemplateModel _currentTemplate;
        private bool _isNewTemplate;
        private bool _hasUnsavedChanges;

        public UcTemplateMrk()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            // Filter change events
            cmbModul.EditValueChanged += (s, e) => LoadTemplateList();
            chkAktif.CheckedChanged += (s, e) => LoadTemplateList();
            txtArama.EditValueChanged += (s, e) => LoadTemplateList();

            // Grid events
            gvTemplateList.DoubleClick += (s, e) => LoadTemplateDetay();
            gvTemplateList.FocusedRowChanged += gvTemplateList_FocusedRowChanged;
            gvVersionList.FocusedRowChanged += gvVersionList_FocusedRowChanged;

            // Button events
            btnYenile.Click += (s, e) => LoadData();
            btnYeni.Click += btnYeni_Click;
            btnKaydet.Click += btnKaydet_Click;
            btnPasifle.Click += btnPasifle_Click;
            btnUploadVersion.Click += btnUploadVersion_Click;
            btnAktifEt.Click += btnAktifEt_Click;
            btnArsivle.Click += btnArsivle_Click;
            btnDownloadVersion.Click += btnDownloadVersion_Click;
            btnOpenFile.Click += btnOpenFile_Click;

            // Detail field change tracking
            txtTemplateKod.EditValueChanged += FieldChanged;
            txtTemplateAdi.EditValueChanged += FieldChanged;
            cmbDetayModul.EditValueChanged += FieldChanged;
            memoAciklama.EditValueChanged += FieldChanged;
            chkDetayAktif.CheckedChanged += FieldChanged;
        }

        private void FieldChanged(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
        }

        #region UcBase Overrides

        public override void LoadData()
        {
            try
            {
                // Sprint 8: Validate template path before loading
                string validationError;
                if (!ValidateTemplatePath(out validationError))
                {
                    ShowPathErrorGuidance(validationError);
                    btnUploadVersion.Enabled = false; // Disable upload if path invalid
                    return;
                }

                // Load module lookup
                cmbModul.Properties.Items.Clear();
                cmbModul.Properties.Items.Add("(Tümü)");
                cmbModul.Properties.Items.AddRange(TemplateLookupProvider.GetModulListesi().ToArray());
                cmbModul.SelectedIndex = 0;

                cmbDetayModul.Properties.Items.Clear();
                cmbDetayModul.Properties.Items.AddRange(TemplateLookupProvider.GetModulListesi().ToArray());

                // Default filters
                chkAktif.Checked = true;
                txtArama.Text = "";

                LoadTemplateList();
                ClearDetay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override bool HasChanges()
        {
            return _hasUnsavedChanges;
        }

        public override void ClearData()
        {
            ClearDetay();
            if (_templateList != null) _templateList.Clear();
            if (_versionList != null) _versionList.Clear();
        }

        #endregion

        #region Template List Operations

        private void LoadTemplateList()
        {
            try
            {
                var filtre = new TemplateFiltreDto
                {
                    Modul = cmbModul.SelectedIndex > 0 ? cmbModul.Text : null,
                    Aktif = chkAktif.Checked ? true : (bool?)null,
                    Arama = string.IsNullOrWhiteSpace(txtArama.Text) ? null : txtArama.Text.Trim()
                };

                var liste = InterfaceFactory.Template.TemplateListele(filtre);

                gvTemplateList.BeginUpdate();
                _templateList = new BindingList<TemplateListeItemDto>(liste);
                gcTemplateList.DataSource = _templateList;
                gvTemplateList.EndUpdate();

                lblKayitSayisi.Text = string.Format("Toplam {0} kayıt", liste.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Liste yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvTemplateList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                var selectedItem = gvTemplateList.GetRow(e.FocusedRowHandle) as TemplateListeItemDto;
                if (selectedItem != null)
                {
                    LoadTemplateDetay(selectedItem.TemplateId);
                    LoadVersionList(selectedItem.TemplateId);
                }
            }
        }

        #endregion

        #region Template Detail Operations

        private void LoadTemplateDetay(int? templateId = null)
        {
            try
            {
                if (!templateId.HasValue)
                {
                    var selectedItem = gvTemplateList.GetFocusedRow() as TemplateListeItemDto;
                    if (selectedItem == null)
                        return;
                    templateId = selectedItem.TemplateId;
                }

                _currentTemplate = InterfaceFactory.Template.TemplateGetirById(templateId.Value);
                if (_currentTemplate == null)
                {
                    MessageBox.Show("Template bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _isNewTemplate = false;
                BindTemplateToUI();
                _hasUnsavedChanges = false;

                // Enable/disable buttons based on permission (TODO: Replace with actual permission check)
                btnKaydet.Enabled = true; // TEMPLATE_MANAGE
                btnPasifle.Enabled = _currentTemplate.Aktif; // TEMPLATE_MANAGE
            }
            catch (Exception ex)
            {
                MessageBox.Show("Detay yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindTemplateToUI()
        {
            if (_currentTemplate == null)
                return;

            txtTemplateKod.Text = _currentTemplate.TemplateKod;
            txtTemplateKod.Enabled = _isNewTemplate; // Only editable on create
            txtTemplateAdi.Text = _currentTemplate.TemplateAdi;
            cmbDetayModul.EditValue = _currentTemplate.Modul;
            memoAciklama.Text = _currentTemplate.Aciklama;
            chkDetayAktif.Checked = _currentTemplate.Aktif;

            if (_currentTemplate.AktifVersionNo.HasValue)
            {
                lblAktifVersion.Text = string.Format("Aktif Versiyon: v{0}", _currentTemplate.AktifVersionNo);
            }
            else
            {
                lblAktifVersion.Text = "Aktif Versiyon: Yok";
            }
        }

        private void ClearDetay()
        {
            _currentTemplate = null;
            _isNewTemplate = false;
            _hasUnsavedChanges = false;

            txtTemplateKod.Text = "";
            txtTemplateAdi.Text = "";
            cmbDetayModul.EditValue = null;
            memoAciklama.Text = "";
            chkDetayAktif.Checked = true;
            lblAktifVersion.Text = "Aktif Versiyon: -";

            if (_versionList != null) _versionList.Clear();

            btnKaydet.Enabled = false;
            btnPasifle.Enabled = false;
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            if (HasChanges())
            {
                var result = MessageBox.Show(
                    "Kaydedilmemiş değişiklikler var. Devam etmek istediğinizden emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            _currentTemplate = new TemplateModel
            {
                TemplateId = 0,
                Aktif = true
            };
            _isNewTemplate = true;
            BindTemplateToUI();
            _hasUnsavedChanges = false;

            txtTemplateKod.Focus();
            btnKaydet.Enabled = true; // TEMPLATE_MANAGE
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtTemplateKod.Text))
                {
                    MessageBox.Show("Template kodu zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTemplateKod.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTemplateAdi.Text))
                {
                    MessageBox.Show("Template adı zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTemplateAdi.Focus();
                    return;
                }

                if (cmbDetayModul.EditValue == null)
                {
                    MessageBox.Show("Modül seçimi zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbDetayModul.Focus();
                    return;
                }

                // Update model from UI
                _currentTemplate.TemplateKod = txtTemplateKod.Text.Trim();
                _currentTemplate.TemplateAdi = txtTemplateAdi.Text.Trim();
                _currentTemplate.Modul = cmbDetayModul.EditValue.ToString();
                _currentTemplate.Aciklama = memoAciklama.Text.Trim();
                _currentTemplate.Aktif = chkDetayAktif.Checked;

                // Save
                var error = InterfaceFactory.Template.TemplateKaydet(_currentTemplate);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(_isNewTemplate ? "Template başarıyla oluşturuldu." : "Template başarıyla güncellendi.",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _hasUnsavedChanges = false;
                    _isNewTemplate = false;

                    LoadTemplateList();
                    LoadTemplateDetay(_currentTemplate.TemplateId);
                }
                else
                {
                    MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPasifle_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentTemplate == null || _currentTemplate.TemplateId == 0)
                    return;

                var result = MessageBox.Show(
                    string.Format("'{0}' template'ini pasif hale getirmek istediğinizden emin misiniz?", _currentTemplate.TemplateAdi),
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                var error = InterfaceFactory.Template.TemplatePasifle(_currentTemplate.TemplateId);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Template başarıyla pasif hale getirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTemplateList();
                    LoadTemplateDetay(_currentTemplate.TemplateId);
                }
                else
                {
                    MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pasifleştirme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Version Management

        private void LoadVersionList(int templateId)
        {
            try
            {
                var liste = InterfaceFactory.Template.VersionListele(templateId);

                gvVersionList.BeginUpdate();
                _versionList = new BindingList<TemplateVersionListeItemDto>(liste);
                gcVersionList.DataSource = _versionList;
                gvVersionList.EndUpdate();

                lblVersionCount.Text = string.Format("Toplam {0} versiyon", liste.Count);

                UpdateVersionButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Versiyon listesi yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvVersionList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateVersionButtons();
        }

        private void UpdateVersionButtons()
        {
            var selectedVersion = gvVersionList.GetFocusedRow() as TemplateVersionListeItemDto;

            if (selectedVersion == null)
            {
                btnAktifEt.Enabled = false;
                btnArsivle.Enabled = false;
                btnDownloadVersion.Enabled = false;
                btnOpenFile.Enabled = false;
                return;
            }

            // Enable/disable based on status and permissions
            // TODO: Replace with actual permission checks
            bool canManage = true; // TEMPLATE_MANAGE
            bool canApprove = true; // TEMPLATE_APPROVE

            btnAktifEt.Enabled = canApprove && !selectedVersion.IsActive &&
                (selectedVersion.Durum == "DRAFT" || selectedVersion.Durum == "APPROVED");
            btnArsivle.Enabled = canManage && !selectedVersion.IsActive;
            btnDownloadVersion.Enabled = true;
            btnOpenFile.Enabled = true;
        }

        private void btnUploadVersion_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentTemplate == null || _currentTemplate.TemplateId == 0)
                {
                    MessageBox.Show("Lütfen önce bir template seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var openFileDialog = new OpenFileDialog())
                {
                    // Get allowed extensions from settings
                    var allowedExts = CommonFunction.GetConfigValue("TemplateAllowedExtensions", ".repx;.docx;.xlsx;.pdf");
                    var exts = allowedExts.Split(';').Select(ext => ext.Trim()).ToArray();

                    openFileDialog.Filter = string.Format("Template Files ({0})|{1}|All Files (*.*)|*.*", allowedExts, string.Join(";", exts.Select(ext => "*" + ext)));
                    openFileDialog.Title = "Şablon Dosyası Seçin";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    var selectedFile = openFileDialog.FileName;
                    var fileExt = Path.GetExtension(selectedFile).ToLower();

                    // Validate extension
                    if (!exts.Contains(fileExt))
                    {
                        MessageBox.Show(string.Format("İzin verilen uzantılar: {0}", allowedExts), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Validate size
                    var fileInfo = new FileInfo(selectedFile);
                    var maxSizeMB = int.Parse(CommonFunction.GetConfigValue("TemplateMaxSizeMB", "10"));
                    if (fileInfo.Length > maxSizeMB * 1024 * 1024)
                    {
                        MessageBox.Show(string.Format("Dosya boyutu {0}MB sınırını aşıyor.", maxSizeMB), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Calculate SHA256
                    string sha256;
                    using (var sha = SHA256.Create())
                    {
                        using (var stream = File.OpenRead(selectedFile))
                        {
                            var hash = sha.ComputeHash(stream);
                            sha256 = BitConverter.ToString(hash).Replace("-", "").ToLower();
                        }
                    }

                    // Build relative path: {template_kod}/v{version_no}_{timestamp}{ext}
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var versionNo = (_versionList != null ? _versionList.Count : 0) + 1; // Estimated, SP will determine actual
                    var relativePath = string.Format("{0}/v{1}_{2}{3}", _currentTemplate.TemplateKod, versionNo, timestamp, fileExt);

                    // Copy file to template directory with retry mechanism
                    var templateBasePath = CommonFunction.GetTemplateDirectoryPath();
                    var templateFolderPath = Path.Combine(templateBasePath, _currentTemplate.TemplateKod);

                    // Create template-specific folder with error handling
                    try
                    {
                        if (!Directory.Exists(templateFolderPath))
                            Directory.CreateDirectory(templateFolderPath);
                    }
                    catch (UnauthorizedAccessException uaEx)
                    {
                        MessageBox.Show(
                            string.Format("Klasör oluşturma izni yok: {0}\n\nLütfen klasör izinlerini kontrol edin.", templateFolderPath),
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorManager.LogMessage(string.Format("Directory creation failed: {0}", uaEx.Message), "TEMPLATE");
                        return;
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show(
                            string.Format("Klasör oluşturma hatası: {0}\n\nDetay: {1}", templateFolderPath, ioEx.Message),
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorManager.LogMessage(string.Format("Directory creation failed: {0}", ioEx.Message), "TEMPLATE");
                        return;
                    }

                    var fullDestPath = Path.Combine(templateBasePath, relativePath);

                    // Use retry mechanism for file copy (3 retries)
                    string copyError;
                    if (!CopyFileWithRetry(selectedFile, fullDestPath, 3, out copyError))
                    {
                        MessageBox.Show(
                            string.Format("Dosya kopyalama başarısız:\n\n{0}", copyError),
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorManager.LogMessage(string.Format("File copy failed: {0}", copyError), "TEMPLATE");
                        return;
                    }

                    // Save version metadata
                    var error = InterfaceFactory.Template.VersionEkle(
                        _currentTemplate.TemplateId,
                        Path.GetFileName(selectedFile),
                        relativePath,
                        GetMimeType(fileExt),
                        fileInfo.Length,
                        sha256,
                        null // notlar
                    );

                    if (string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("Versiyon başarıyla yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadVersionList(_currentTemplate.TemplateId);
                    }
                    else
                    {
                        // Rollback file copy on error
                        if (File.Exists(fullDestPath))
                            File.Delete(fullDestPath);

                        MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Versiyon yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAktifEt_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedVersion = gvVersionList.GetFocusedRow() as TemplateVersionListeItemDto;
                if (selectedVersion == null)
                    return;

                if (selectedVersion.IsActive)
                {
                    MessageBox.Show("Bu versiyon zaten aktif.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    string.Format("Versiyon {0} aktif olacak. Önceki aktif versiyon arşivlenecek. Devam etmek istiyor musunuz?", selectedVersion.VersionNo),
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                var error = InterfaceFactory.Template.VersionAktifEt(
                    selectedVersion.TemplateId,
                    selectedVersion.TemplateVersionId);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Versiyon başarıyla aktif hale getirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTemplateList(); // Refresh to update aktif_version_no
                    LoadVersionList(selectedVersion.TemplateId);
                    LoadTemplateDetay(selectedVersion.TemplateId);
                }
                else
                {
                    MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Aktivasyon hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnArsivle_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedVersion = gvVersionList.GetFocusedRow() as TemplateVersionListeItemDto;
                if (selectedVersion == null)
                    return;

                if (selectedVersion.IsActive)
                {
                    MessageBox.Show("Aktif versiyon arşivlenemez. Önce başka bir versiyon aktif edilmelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show(
                    string.Format("Versiyon {0} arşivlenecek. Devam etmek istiyor musunuz?", selectedVersion.VersionNo),
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                var error = InterfaceFactory.Template.VersionArsivle(selectedVersion.TemplateVersionId);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Versiyon başarıyla arşivlendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVersionList(selectedVersion.TemplateId);
                }
                else
                {
                    MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arşivleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownloadVersion_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedVersion = gvVersionList.GetFocusedRow() as TemplateVersionListeItemDto;
                if (selectedVersion == null)
                    return;

                // Get file info from service (returns path, NOT content - FileSystem mode)
                var dosyaResult = InterfaceFactory.Template.DosyaGetir(selectedVersion.TemplateVersionId);
                if (dosyaResult == null || string.IsNullOrEmpty(dosyaResult.DosyaYolu))
                {
                    MessageBox.Show("Dosya bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Build full path from template directory + relative path
                var templatePath = CommonFunction.GetTemplateDirectoryPath();
                var fullPath = Path.Combine(templatePath, dosyaResult.DosyaYolu);

                if (!File.Exists(fullPath))
                {
                    MessageBox.Show(string.Format("Dosya sistemde bulunamadı: {0}", fullPath), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = dosyaResult.DosyaAdi;
                    saveFileDialog.Filter = string.Format("Template File|*{0}|All Files (*.*)|*.*", Path.GetExtension(dosyaResult.DosyaAdi));

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Use retry mechanism for download (3 retries)
                        string copyError;
                        if (CopyFileWithRetry(fullPath, saveFileDialog.FileName, 3, out copyError))
                        {
                            MessageBox.Show("Dosya başarıyla indirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                                string.Format("Dosya indirme başarısız:\n\n{0}", copyError),
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ErrorManager.LogMessage(string.Format("File download failed: {0}", copyError), "TEMPLATE");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İndirme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedVersion = gvVersionList.GetFocusedRow() as TemplateVersionListeItemDto;
                if (selectedVersion == null)
                    return;

                // Get file info from service (returns path, NOT content - FileSystem mode)
                var dosyaResult = InterfaceFactory.Template.DosyaGetir(selectedVersion.TemplateVersionId);
                if (dosyaResult == null || string.IsNullOrEmpty(dosyaResult.DosyaYolu))
                {
                    MessageBox.Show("Dosya bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Build full path from template directory + relative path
                var templatePath = CommonFunction.GetTemplateDirectoryPath();
                var fullPath = Path.Combine(templatePath, dosyaResult.DosyaYolu);

                if (!File.Exists(fullPath))
                {
                    MessageBox.Show(string.Format("Dosya sistemde bulunamadı: {0}", fullPath), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Open with default application
                System.Diagnostics.Process.Start(fullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya açma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helper Methods

        private string GetMimeType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".repx":
                    return "application/xml";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".pdf":
                    return "application/pdf";
                default:
                    return "application/octet-stream";
            }
        }

        #endregion

        #region Sprint 8: Path Validation and Error Handling

        /// <summary>
        /// Validates that the template directory exists and is accessible
        /// </summary>
        private bool ValidateTemplatePath(out string error)
        {
            error = null;

            try
            {
                var templatePath = CommonFunction.GetTemplateDirectoryPath();

                if (string.IsNullOrEmpty(templatePath))
                {
                    error = "Şablon yolu tanımlı değil. Lütfen sistem ayarlarından TEMPLATE_PATH değerini kontrol edin.";
                    return false;
                }

                if (!Path.IsPathRooted(templatePath))
                {
                    templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);
                }

                if (!Directory.Exists(templatePath))
                {
                    error = string.Format("Şablon dizini bulunamadı: {0}\n\nLütfen dizinin var olduğunu ve erişim izniniz olduğunu kontrol edin.", templatePath);
                    return false;
                }

                // Test read/write access
                var testFile = Path.Combine(templatePath, string.Format("test_{0}.tmp", Guid.NewGuid()));
                try
                {
                    File.WriteAllText(testFile, "test");
                    File.Delete(testFile);
                }
                catch (UnauthorizedAccessException)
                {
                    error = string.Format("Şablon dizinine yazma izni yok: {0}\n\nLütfen klasör izinlerini kontrol edin.", templatePath);
                    return false;
                }
                catch (IOException ioEx)
                {
                    error = string.Format("Şablon dizini erişim hatası: {0}\n\nDetay: {1}", templatePath, ioEx.Message);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = string.Format("Şablon yolu doğrulama hatası: {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Shows user-friendly error guidance for path errors
        /// </summary>
        private void ShowPathErrorGuidance(string error)
        {
            var message = new StringBuilder();
            message.AppendLine(error);
            message.AppendLine();
            message.AppendLine("Çözüm önerileri:");
            message.AppendLine("1. Sistem Ayarları ekranından TEMPLATE_PATH değerini kontrol edin");
            message.AppendLine("2. Belirtilen dizinin var olduğundan emin olun");
            message.AppendLine("3. Windows kullanıcınızın dizine yazma izni olduğunu kontrol edin");
            message.AppendLine("4. Antivirüs yazılımınızın dizini engellemediğinden emin olun");

            MessageBox.Show(message.ToString(), "Şablon Dizini Hatası",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            ErrorManager.LogMessage(string.Format("Template path validation failed: {0}", error), "TEMPLATE");
        }

        /// <summary>
        /// Copies a file with retry mechanism for transient errors
        /// </summary>
        private bool CopyFileWithRetry(string sourceFile, string destFile, int maxRetries, out string error)
        {
            error = null;
            int retryDelayMs = 500;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    File.Copy(sourceFile, destFile, false);
                    return true;
                }
                catch (IOException ioEx)
                {
                    if (attempt < maxRetries)
                    {
                        var message = string.Format(
                            "Dosya kopyalama başarısız (Deneme {0}/{1}). {2} saniye sonra tekrar denenecek...\n\nDetay: {3}",
                            attempt, maxRetries, retryDelayMs / 1000.0, ioEx.Message);

                        MessageBox.Show(message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        Thread.Sleep(retryDelayMs);
                        retryDelayMs *= 2; // Exponential backoff
                    }
                    else
                    {
                        error = string.Format("Dosya {0} denemeden sonra kopyalanamadı. Son hata: {1}",
                            maxRetries, ioEx.Message);
                        return false;
                    }
                }
                catch (UnauthorizedAccessException uaEx)
                {
                    error = string.Format("Erişim izni yok: {0}", uaEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    error = string.Format("Beklenmeyen hata: {0}", ex.Message);
                    return false;
                }
            }

            error = "Maksimum deneme sayısına ulaşıldı";
            return false;
        }

        #endregion
    }
}
