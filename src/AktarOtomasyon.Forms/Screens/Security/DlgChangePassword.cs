using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Security.Interface.Models.Auth;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Password change dialog - Modal form
    /// Allows users to change their own password
    /// </summary>
    public partial class DlgChangePassword : XtraForm
    {
        public DlgChangePassword()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                // Show current user info
                if (SessionManager.IsAuthenticated)
                {
                    lblKullaniciInfo.Text = string.Format("Kullanıcı: {0} ({1})",
                        SessionManager.AdSoyad,
                        SessionManager.KullaniciAdi);
                }

                // Focus on old password field
                txtEskiParola.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("DlgChangePassword_InitializeForm error: {0}", ex.Message), "SEC");
            }
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                // Prepare change password request
                var request = new ChangePasswordDto
                {
                    KullaniciId = SessionManager.KullaniciId,
                    EskiParola = txtEskiParola.Text,
                    YeniParola = txtYeniParola.Text
                };

                // Call change password service
                var error = InterfaceFactory.Auth.ChangePassword(request);

                if (string.IsNullOrEmpty(error))
                {
                    // Success
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Error
                    DMLManager.ShowError(error);

                    // Clear password fields on error
                    txtEskiParola.Text = string.Empty;
                    txtYeniParola.Text = string.Empty;
                    txtYeniParolaTekrar.Text = string.Empty;
                    txtEskiParola.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnTamam_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Parola değiştirme hatası: {0}", ex.Message));

                // Clear password fields on exception
                txtEskiParola.Text = string.Empty;
                txtYeniParola.Text = string.Empty;
                txtYeniParolaTekrar.Text = string.Empty;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnIptal_Click error: {0}", ex.Message), "SEC");
            }
        }

        private bool ValidateInput()
        {
            // Old password required
            if (string.IsNullOrWhiteSpace(txtEskiParola.Text))
            {
                DMLManager.ShowWarning("Eski parolanızı giriniz.");
                txtEskiParola.Focus();
                return false;
            }

            // New password required
            if (string.IsNullOrWhiteSpace(txtYeniParola.Text))
            {
                DMLManager.ShowWarning("Yeni parolanızı giriniz.");
                txtYeniParola.Focus();
                return false;
            }

            // New password confirmation required
            if (string.IsNullOrWhiteSpace(txtYeniParolaTekrar.Text))
            {
                DMLManager.ShowWarning("Yeni parolanızı tekrar giriniz.");
                txtYeniParolaTekrar.Focus();
                return false;
            }

            // New password minimum length (8 chars)
            if (txtYeniParola.Text.Length < 8)
            {
                DMLManager.ShowWarning("Yeni parola en az 8 karakter olmalıdır.");
                txtYeniParola.Focus();
                return false;
            }

            // New password complexity check (at least one uppercase and one digit)
            bool hasUpper = false;
            bool hasDigit = false;

            foreach (char c in txtYeniParola.Text)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                if (char.IsDigit(c))
                    hasDigit = true;
            }

            if (!hasUpper || !hasDigit)
            {
                DMLManager.ShowWarning("Yeni parola en az bir büyük harf ve bir rakam içermelidir.");
                txtYeniParola.Focus();
                return false;
            }

            // New password confirmation must match
            if (txtYeniParola.Text != txtYeniParolaTekrar.Text)
            {
                DMLManager.ShowWarning("Yeni parola ve tekrarı eşleşmiyor.");
                txtYeniParolaTekrar.Focus();
                return false;
            }

            // New password must be different from old
            if (txtEskiParola.Text == txtYeniParola.Text)
            {
                DMLManager.ShowWarning("Yeni parola eski paroladan farklı olmalıdır.");
                txtYeniParola.Focus();
                return false;
            }

            return true;
        }

        private void DlgChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Clear password fields on close for security
                txtEskiParola.Text = string.Empty;
                txtYeniParola.Text = string.Empty;
                txtYeniParolaTekrar.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("DlgChangePassword_FormClosing error: {0}", ex.Message), "SEC");
            }
        }
    }
}
