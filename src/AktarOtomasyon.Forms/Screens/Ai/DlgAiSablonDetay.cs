using System;
using System.Windows.Forms;
using AktarOtomasyon.Ai.Interface;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Ai
{
    public partial class DlgAiSablonDetay : XtraForm
    {
        private AiSablonModel _sablon;
        private bool _isEditMode;

        public DlgAiSablonDetay(AiSablonModel sablon)
        {
            InitializeComponent();

            _sablon = sablon ?? new AiSablonModel();
            _isEditMode = (_sablon.SablonId > 0);

            this.Text = _isEditMode ? "Şablon Düzenle" : "Yeni Şablon";
        }

        private void DlgAiSablonDetay_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("DlgAiSablonDetay.Load hata: " + ex.Message, "AI_SABLON_DETAY");
                DMLManager.ShowError("Form yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadData()
        {
            if (_isEditMode)
            {
                txtSablonKod.Text = _sablon.SablonKod;
                txtSablonKod.Enabled = false; // Cannot change code in edit mode
            }
            else
            {
                txtSablonKod.Text = string.Empty;
                txtSablonKod.Enabled = true;
            }

            txtSablonAdi.Text = _sablon.SablonAdi ?? string.Empty;
            memoPromptSablonu.Text = _sablon.PromptSablonu ?? string.Empty;
            memoAciklama.Text = _sablon.Aciklama ?? string.Empty;
            chkAktif.Checked = _sablon.Aktif;

            // Set focus to first input
            if (_isEditMode)
                txtSablonAdi.Focus();
            else
                txtSablonKod.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtSablonKod.Text))
            {
                DMLManager.ShowWarning("Şablon kodu zorunludur.");
                txtSablonKod.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSablonAdi.Text))
            {
                DMLManager.ShowWarning("Şablon adı zorunludur.");
                txtSablonAdi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(memoPromptSablonu.Text))
            {
                DMLManager.ShowWarning("Prompt şablonu zorunludur.");
                memoPromptSablonu.Focus();
                return false;
            }

            return true;
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                // Update model
                _sablon.SablonKod = txtSablonKod.Text.Trim().ToUpper();
                _sablon.SablonAdi = txtSablonAdi.Text.Trim();
                _sablon.PromptSablonu = memoPromptSablonu.Text.Trim();
                _sablon.Aciklama = string.IsNullOrWhiteSpace(memoAciklama.Text)
                    ? null
                    : memoAciklama.Text.Trim();
                _sablon.Aktif = chkAktif.Checked;

                // Save via service
                var error = InterfaceFactory.Ai.SablonKaydet(_sablon);

                if (DMLManager.KaydetKontrol(error))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnTamam_Click hata: " + ex.Message, "AI_SABLON_DETAY");
                DMLManager.ShowError("Şablon kaydedilirken hata: " + ex.Message);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSablonKod_EditValueChanged(object sender, EventArgs e)
        {
            // Auto-uppercase
            if (!_isEditMode)
            {
                int selectionStart = txtSablonKod.SelectionStart;
                txtSablonKod.Text = txtSablonKod.Text.ToUpper();
                txtSablonKod.SelectionStart = selectionStart;
            }
        }
    }
}
