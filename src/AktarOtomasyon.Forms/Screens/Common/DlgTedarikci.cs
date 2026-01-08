using System;
using System.Windows.Forms;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Common
{
    /// <summary>
    /// Tedarikçi oluşturma/düzenleme dialog formu
    /// </summary>
    public partial class DlgTedarikci : DevExpress.XtraEditors.XtraForm
    {
        private TedarikciModel _tedarikci;
        private bool _isNew;

        public TedarikciModel Tedarikci
        {
            get { return _tedarikci; }
        }

        public DlgTedarikci() : this(null)
        {
        }

        public DlgTedarikci(TedarikciModel tedarikci)
        {
            InitializeComponent();
            _tedarikci = tedarikci ?? new TedarikciModel { Aktif = true };
            _isNew = tedarikci == null || tedarikci.TedarikciId == 0;
            
            LoadData();
            ApplyModernStyling();
        }

        private void LoadData()
        {
            if (_isNew)
            {
                this.Text = "Yeni Tedarikçi";
                txtTedarikciKod.Text = "";
                txtTedarikciKod.Enabled = false; // Otomatik oluşturulacak
            }
            else
            {
                this.Text = "Tedarikçi Düzenle";
                txtTedarikciKod.Text = _tedarikci.TedarikciKod ?? "";
                txtTedarikciKod.Enabled = true;
            }

            txtTedarikciAdi.Text = _tedarikci.TedarikciAdi ?? "";
            txtYetkili.Text = _tedarikci.Yetkili ?? "";
            txtTelefon.Text = _tedarikci.Telefon ?? "";
            txtEmail.Text = _tedarikci.Email ?? "";
            txtAdres.Text = _tedarikci.Adres ?? "";
            chkAktif.Checked = _tedarikci.Aktif;
        }

        private void ApplyModernStyling()
        {
            // Modernize buttons
            ButtonHelper.ApplyPrimaryStyle(btnKaydet);
            ButtonHelper.ApplySecondaryStyle(btnIptal);

            // Modernize group
            ModernTheme.ApplyModernGroup(grpBilgiler);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTedarikciAdi.Text))
            {
                MessageHelper.ShowWarning("Tedarikçi adı zorunludur.");
                txtTedarikciAdi.Focus();
                return false;
            }

            return true;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                _tedarikci.TedarikciKod = txtTedarikciKod.Text.Trim();
                _tedarikci.TedarikciAdi = txtTedarikciAdi.Text.Trim();
                _tedarikci.Yetkili = txtYetkili.Text.Trim();
                _tedarikci.Telefon = txtTelefon.Text.Trim();
                _tedarikci.Email = txtEmail.Text.Trim();
                _tedarikci.Adres = txtAdres.Text.Trim();
                _tedarikci.Aktif = chkAktif.Checked;

                var error = InterfaceFactory.Common.TedarikciKaydet(_tedarikci);
                if (error != null)
                {
                    MessageHelper.ShowError("Tedarikçi kaydedilemedi: " + error);
                    return;
                }

                MessageHelper.ShowSuccess("Tedarikçi başarıyla kaydedildi.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Hata: " + ex.Message);
                ErrorManager.LogMessage("DlgTedarikci.btnKaydet error: " + ex.Message, "DLG");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
