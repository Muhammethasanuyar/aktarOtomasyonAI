using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Common.Interface.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace AktarOtomasyon.Forms.Screens.Template
{
    /// <summary>
    /// System Settings Editor - Main UserControl
    /// </summary>
    public partial class UcSystemSettings : UcBase
    {
        private BindingList<SystemSettingDto> _settingsList;
        private bool _hasUnsavedChanges;

        public UcSystemSettings()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            btnYenile.Click += (s, e) => LoadData();
            btnKaydet.Click += btnKaydet_Click;
            btnBrowseTemplatePath.Click += (s, e) => BrowsePath("TemplatePath");
            btnBrowseReportPath.Click += (s, e) => BrowsePath("ReportPath");

            gvSettings.CellValueChanged += (s, e) =>
            {
                _hasUnsavedChanges = true;
            };

            gvSettings.CustomRowCellEdit += gvSettings_CustomRowCellEdit;
            gvSettings.ShowingEditor += gvSettings_ShowingEditor;
            gvSettings.RowCellStyle += gvSettings_RowCellStyle;
        }

        #region UcBase Overrides

        public override void LoadData()
        {
            try
            {
                var liste = InterfaceFactory.SystemSetting.SettingListele();

                // Sprint 8: Determine config source for each setting
                foreach (var setting in liste)
                {
                    DetermineConfigSource(setting);
                }

                gvSettings.BeginUpdate();
                _settingsList = new BindingList<SystemSettingDto>(liste);
                gcSettings.DataSource = _settingsList;
                gvSettings.EndUpdate();

                _hasUnsavedChanges = false;
                lblKayitSayisi.Text = string.Format("Toplam {0} ayar", liste.Count);

                // Enable/disable buttons based on permission
                // TODO: Replace with actual permission check
                btnKaydet.Enabled = true; // SETTINGS_MANAGE
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
            if (_settingsList != null) _settingsList.Clear();
            _hasUnsavedChanges = false;
        }

        #endregion

        #region Custom Cell Editors

        private void gvSettings_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "SettingValue")
                return;

            var setting = gvSettings.GetRow(e.RowHandle) as SystemSettingDto;
            if (setting == null)
                return;

            // Custom editor for TemplateStorageMode
            if (setting.SettingKey == "TemplateStorageMode")
            {
                var repo = new RepositoryItemComboBox();
                repo.Items.AddRange(TemplateLookupProvider.GetStorageModeListesi());
                repo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                e.RepositoryItem = repo;
            }
        }

        #endregion

        #region Path Browsing

        private void BrowsePath(string settingKey)
        {
            try
            {
                var setting = _settingsList.FirstOrDefault(s => s.SettingKey == settingKey);
                if (setting == null)
                    return;

                using (var folderBrowser = new FolderBrowserDialog())
                {
                    folderBrowser.Description = string.Format("{0} dizinini seçin", settingKey);
                    folderBrowser.SelectedPath = setting.SettingValue;

                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        setting.SettingValue = folderBrowser.SelectedPath;
                        _hasUnsavedChanges = true;
                        gvSettings.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dizin seçimi hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Sprint 8: Configuration Source Management

        /// <summary>
        /// Determines the config source for a setting (Environment, Database, or App.config)
        /// </summary>
        private void DetermineConfigSource(SystemSettingDto setting)
        {
            // Priority 1: Environment Variables
            var envValue = Environment.GetEnvironmentVariable(setting.SettingKey);
            if (!string.IsNullOrEmpty(envValue))
            {
                setting.ConfigSource = "Environment";
                setting.IsReadOnly = true;
                return;
            }

            // Priority 2: Database
            if (!string.IsNullOrEmpty(setting.SettingValue))
            {
                setting.ConfigSource = "Database";
                setting.IsReadOnly = false;
                return;
            }

            // Priority 3: App.config
            var appConfigValue = ConfigurationManager.AppSettings[setting.SettingKey];
            if (!string.IsNullOrEmpty(appConfigValue))
            {
                setting.ConfigSource = "App.config";
                setting.IsReadOnly = true;
                return;
            }

            // No source found
            setting.ConfigSource = "-";
            setting.IsReadOnly = false;
        }

        /// <summary>
        /// Prevents editing of read-only settings (from Environment or App.config)
        /// </summary>
        private void gvSettings_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            var setting = view.GetFocusedRow() as SystemSettingDto;
            if (setting == null) return;

            if (setting.IsReadOnly)
            {
                e.Cancel = true; // Prevent editing

                // Show tooltip explaining why
                var message = string.Format(
                    "Bu ayar '{0}' kaynağından geldiği için değiştirilemez.\n\n" +
                    "Bu ayarı değiştirmek için:\n" +
                    "- Environment kaynağı: Sistem ortam değişkenlerini düzenleyin\n" +
                    "- App.config kaynağı: App.config dosyasını düzenleyin",
                    setting.ConfigSource);

                MessageBox.Show(message, "Salt Okunur Ayar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Visual styling for read-only settings
        /// </summary>
        private void gvSettings_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var setting = gvSettings.GetRow(e.RowHandle) as SystemSettingDto;
            if (setting != null && setting.IsReadOnly)
            {
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.ForeColor = Color.DarkGray;
            }
        }

        #endregion

        #region Save Operation

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!HasChanges())
                {
                    MessageBox.Show("Değişiklik yapılmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Validate StorageMode change
                var storageModeChanged = false;
                var storageSetting = _settingsList.FirstOrDefault(s => s.SettingKey == "TemplateStorageMode");
                if (storageSetting != null)
                {
                    var currentValue = CommonFunction.GetConfigValue("TemplateStorageMode", "FileSystem");
                    storageModeChanged = storageSetting.SettingValue != currentValue;
                }

                if (storageModeChanged)
                {
                    var result = MessageBox.Show(
                        "TemplateStorageMode değişikliği mevcut verilerin migrasyonunu gerektirebilir. " +
                        "Devam etmek istediğinizden emin misiniz?",
                        "Uyarı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return;
                }

                // Save all settings
                var errors = new List<string>();
                foreach (var setting in _settingsList)
                {
                    var error = InterfaceFactory.SystemSetting.SettingKaydet(setting);
                    if (!string.IsNullOrEmpty(error))
                    {
                        errors.Add(string.Format("{0}: {1}", setting.SettingKey, error));
                    }
                }

                if (errors.Count > 0)
                {
                    MessageBox.Show("Bazı ayarlar kaydedilemedi:\n" + string.Join("\n", errors),
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Tüm ayarlar başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _hasUnsavedChanges = false;
                    LoadData(); // Refresh to get updated timestamps
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
