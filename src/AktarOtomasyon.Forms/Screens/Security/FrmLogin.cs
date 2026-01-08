using System;
using System.IO;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Security.Interface.Models.Auth;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Login form - shown before main application
    /// Standalone form (NOT MDI child)
    /// </summary>
    public partial class FrmLogin : XtraForm
    {
        private const string CONFIG_FILE = "login.cfg";

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // Set version
                lblVersion.Text = string.Format("Versiyon: {0}", CommonFunction.GetAppVersion());

                // Load saved username if exists
                LoadLoginConfig();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("FrmLogin_Load error: {0}", ex.Message), "LOGIN");
                DMLManager.ShowError(string.Format("Form yüklenirken hata: {0}", ex.Message));
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (!ValidateInputs())
                    return;

                // Prepare login request
                var request = new LoginRequestDto
                {
                    KullaniciAdi = txtKullaniciAdi.Text.Trim(),
                    Parola = txtParola.Text
                };

                // Call login service
                var result = InterfaceFactory.Auth.Login(request);

                if (result == null)
                {
                    DMLManager.ShowError("Login servisi yanıt vermedi.");
                    return;
                }

                if (!result.Success)
                {
                    // Generic error message (prevents user enumeration)
                    DMLManager.ShowError(result.ErrorMessage ?? "Giriş başarısız.");

                    // Clear password and focus username
                    txtParola.Text = string.Empty;
                    txtKullaniciAdi.Focus();
                    txtKullaniciAdi.SelectAll();

                    return;
                }

                // Login successful
                SessionManager.Login(result);

                ErrorManager.LogMessage(string.Format("Login successful: {0}", result.KullaniciAdi), "LOGIN");

                // Save or clear remembered username
                SaveLoginConfig();

                // Close login form with success result
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnGiris_Click error: {0}", ex.Message), "LOGIN");
                DMLManager.ShowError(string.Format("Giriş sırasında hata oluştu: {0}", ex.Message));

                // Clear password on error
                txtParola.Text = string.Empty;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            try
            {
                // Close with cancel result
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnIptal_Click error: {0}", ex.Message), "LOGIN");
            }
        }

        private bool ValidateInputs()
        {
            // Username required
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                DMLManager.ShowWarning("Kullanıcı adı giriniz.");
                txtKullaniciAdi.Focus();
                return false;
            }

            // Password required
            if (string.IsNullOrWhiteSpace(txtParola.Text))
            {
                DMLManager.ShowWarning("Parola giriniz.");
                txtParola.Focus();
                return false;
            }

            return true;
        }

        private void txtKullaniciAdi_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter key moves to password
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtParola.Focus();
            }
        }

        private void txtParola_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter key submits login
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGiris.PerformClick();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Clear password field on close for security
                txtParola.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("FrmLogin_FormClosing error: {0}", ex.Message), "LOGIN");
            }
        }

        #region Config Management

        private void LoadLoginConfig()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    string username = File.ReadAllText(CONFIG_FILE).Trim();
                    if (!string.IsNullOrEmpty(username))
                    {
                        txtKullaniciAdi.Text = username;
                        chkBeniHatirla.Checked = true;
                        
                        // If username is loaded, focus password
                        this.ActiveControl = txtParola;
                    }
                    else
                    {
                        txtKullaniciAdi.Focus();
                    }
                }
                else
                {
                    txtKullaniciAdi.Focus();
                }
            }
            catch (Exception)
            {
                // Ignore config read errors
                txtKullaniciAdi.Focus();
            }
        }

        private void SaveLoginConfig()
        {
            try
            {
                if (chkBeniHatirla.Checked)
                {
                    // Save username
                    File.WriteAllText(CONFIG_FILE, txtKullaniciAdi.Text.Trim());
                }
                else
                {
                    // Update: Delete file instead of clearing content
                    if (File.Exists(CONFIG_FILE))
                    {
                        File.Delete(CONFIG_FILE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("Login config save error: " + ex.Message, "LOGIN");
            }
        }

        #endregion
    }
}
