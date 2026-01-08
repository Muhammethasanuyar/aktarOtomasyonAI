using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Security.Interface.Models.User;
using AktarOtomasyon.Security.Interface.Models.Role;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// User management user control - User CRUD operations
    /// </summary>
    public partial class UcKullaniciYonetim : UcBase
    {
        private int? _selectedKullaniciId = null;
        private bool _isEditing = false;

        public UcKullaniciYonetim()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += UcKullaniciYonetim_Load;
        }

        private void UcKullaniciYonetim_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUsers();
                LoadAvailableRoles();
                SetFormState(FormState.View);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("UcKullaniciYonetim_Load error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Yükleme hatası: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Refresh data (called from parent)
        /// </summary>
        public void RefreshData()
        {
            try
            {
                LoadUsers();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("RefreshData error: {0}", ex.Message), "SEC");
            }
        }

        private void LoadUsers()
        {
            try
            {
                var users = InterfaceFactory.Security.KullaniciListele(null);
                grdKullanicilar.DataSource = users;
                gvKullanicilar.BestFitColumns();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadUsers error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Kullanıcılar yüklenemedi: {0}", ex.Message));
            }
        }

        private void LoadAvailableRoles()
        {
            try
            {
                var roles = InterfaceFactory.Security.RolListele(aktif: true);

                chklstRoller.Items.Clear();
                foreach (var rol in roles)
                {
                    chklstRoller.Items.Add(rol, rol.RolAdi);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadAvailableRoles error: {0}", ex.Message), "SEC");
            }
        }

        private void LoadUserRoles(int kullaniciId)
        {
            try
            {
                var userRoles = InterfaceFactory.Security.KullaniciRolListele(kullaniciId);

                // Uncheck all first
                for (int i = 0; i < chklstRoller.ItemCount; i++)
                {
                    chklstRoller.SetItemChecked(i, false);
                }

                // Check assigned roles
                for (int i = 0; i < chklstRoller.ItemCount; i++)
                {
                    var rol = chklstRoller.Items[i].Value as RolListeItemDto;
                    if (rol != null && userRoles.Any(ur => ur.RolId == rol.RolId))
                    {
                        chklstRoller.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadUserRoles error: {0}", ex.Message), "SEC");
            }
        }

        private void gvKullanicilar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvKullanicilar.FocusedRowHandle < 0)
                    return;

                var row = gvKullanicilar.GetFocusedRow() as KullaniciListeItemDto;
                if (row != null)
                {
                    LoadUserDetail(row.KullaniciId);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("gvKullanicilar_FocusedRowChanged error: {0}", ex.Message), "SEC");
            }
        }

        private void LoadUserDetail(int kullaniciId)
        {
            try
            {
                var user = InterfaceFactory.Security.KullaniciGetir(kullaniciId);
                if (user == null)
                {
                    DMLManager.ShowWarning("Kullanıcı bulunamadı.");
                    return;
                }

                _selectedKullaniciId = user.KullaniciId;

                txtKullaniciAdi.Text = user.KullaniciAdi;
                txtAdSoyad.Text = user.AdSoyad;
                txtEmail.Text = user.Email;
                chkAktif.Checked = user.Aktif;
                txtParola.Text = string.Empty; // Never show password

                LoadUserRoles(kullaniciId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadUserDetail error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Detay yüklenemedi: {0}", ex.Message));
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            try
            {
                SetFormState(FormState.New);
                ClearForm();
                txtKullaniciAdi.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnYeni_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Hata: {0}", ex.Message));
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedKullaniciId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen düzenlemek için bir kullanıcı seçin.");
                    return;
                }

                SetFormState(FormState.Edit);
                txtKullaniciAdi.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnDuzenle_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Hata: {0}", ex.Message));
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                var model = new KullaniciModel
                {
                    KullaniciId = _selectedKullaniciId ?? 0,
                    KullaniciAdi = txtKullaniciAdi.Text.Trim(),
                    AdSoyad = txtAdSoyad.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Parola = txtParola.Text, // Only set for new users or when changed
                    Aktif = chkAktif.Checked
                };

                var error = InterfaceFactory.Security.KullaniciKaydet(model);

                if (!DMLManager.KaydetKontrol(error))
                    return;

                // Get the saved user ID (if new)
                if (!_selectedKullaniciId.HasValue)
                {
                    // Find the newly created user
                    var users = InterfaceFactory.Security.KullaniciListele(null);
                    var newUser = users.FirstOrDefault(u => u.KullaniciAdi == model.KullaniciAdi);
                    if (newUser != null)
                    {
                        _selectedKullaniciId = newUser.KullaniciId;
                    }
                }

                // Save role assignments
                if (_selectedKullaniciId.HasValue)
                {
                    SaveUserRoles(_selectedKullaniciId.Value);
                }

                LoadUsers();
                SetFormState(FormState.View);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnKaydet_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Kaydetme hatası: {0}", ex.Message));
            }
        }

        private void SaveUserRoles(int kullaniciId)
        {
            try
            {
                // Get current roles
                var currentRoles = InterfaceFactory.Security.KullaniciRolListele(kullaniciId);

                // Get selected roles from CheckedListBox
                var selectedRoles = new List<int>();
                for (int i = 0; i < chklstRoller.ItemCount; i++)
                {
                    if (chklstRoller.GetItemChecked(i))
                    {
                        var rol = chklstRoller.Items[i].Value as RolListeItemDto;
                        if (rol != null)
                        {
                            selectedRoles.Add(rol.RolId);
                        }
                    }
                }

                // Add new roles
                foreach (var rolId in selectedRoles)
                {
                    if (!currentRoles.Any(cr => cr.RolId == rolId))
                    {
                        InterfaceFactory.Security.KullaniciRolEkle(
                            kullaniciId,
                            rolId,
                            SessionManager.KullaniciId);
                    }
                }

                // Remove unchecked roles
                foreach (var currentRole in currentRoles)
                {
                    if (!selectedRoles.Contains(currentRole.RolId))
                    {
                        InterfaceFactory.Security.KullaniciRolSil(
                            kullaniciId,
                            currentRole.RolId,
                            SessionManager.KullaniciId);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("SaveUserRoles error: {0}", ex.Message), "SEC");
                throw;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            try
            {
                SetFormState(FormState.View);

                if (_selectedKullaniciId.HasValue)
                {
                    LoadUserDetail(_selectedKullaniciId.Value);
                }
                else
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnIptal_Click error: {0}", ex.Message), "SEC");
            }
        }

        private void btnPasifle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedKullaniciId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen pasifleştirmek için bir kullanıcı seçin.");
                    return;
                }

                if (!DMLManager.SilmeOnayAl("Kullanıcıyı pasifleştirmek istediğinizden emin misiniz?"))
                    return;

                var error = InterfaceFactory.Security.KullaniciPasifle(
                    _selectedKullaniciId.Value,
                    SessionManager.KullaniciId);

                if (!DMLManager.SilKontrol(error))
                    return;

                LoadUsers();
                ClearForm();
                _selectedKullaniciId = null;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnPasifle_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Pasifleştirme hatası: {0}", ex.Message));
            }
        }

        private void btnParolaSifirla_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedKullaniciId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen parola sıfırlamak için bir kullanıcı seçin.");
                    return;
                }

                // Prompt for new password
                var newPassword = PromptForPassword();
                if (string.IsNullOrWhiteSpace(newPassword))
                    return;

                var error = InterfaceFactory.Auth.ResetPassword(
                    _selectedKullaniciId.Value,
                    newPassword,
                    SessionManager.KullaniciId);

                if (string.IsNullOrEmpty(error))
                {
                    DMLManager.ShowInfo("Parola başarıyla sıfırlandı.");
                }
                else
                {
                    DMLManager.ShowError(string.Format("Parola sıfırlama hatası: {0}", error));
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnParolaSifirla_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Parola sıfırlama hatası: {0}", ex.Message));
            }
        }

        private string PromptForPassword()
        {
            using (var form = new XtraForm())
            {
                form.Text = "Yeni Parola";
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new System.Drawing.Size(350, 150);
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var lblParola = new LabelControl
                {
                    Text = "Yeni Parola:",
                    Location = new System.Drawing.Point(20, 20),
                    AutoSize = true
                };

                var txtNewPassword = new TextEdit
                {
                    Location = new System.Drawing.Point(20, 40),
                    Size = new System.Drawing.Size(300, 20)
                };
                txtNewPassword.Properties.PasswordChar = '*';
                txtNewPassword.Properties.UseSystemPasswordChar = true;

                var btnOk = new SimpleButton
                {
                    Text = "Tamam",
                    DialogResult = DialogResult.OK,
                    Location = new System.Drawing.Point(120, 75),
                    Size = new System.Drawing.Size(90, 25)
                };

                var btnCancel = new SimpleButton
                {
                    Text = "İptal",
                    DialogResult = DialogResult.Cancel,
                    Location = new System.Drawing.Point(220, 75),
                    Size = new System.Drawing.Size(90, 25)
                };

                form.Controls.AddRange(new Control[] { lblParola, txtNewPassword, btnOk, btnCancel });
                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                return form.ShowDialog() == DialogResult.OK ? txtNewPassword.Text : null;
            }
        }

        private bool ValidateInput()
        {
            // Username required
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                DMLManager.ShowWarning("Kullanıcı adı giriniz.");
                txtKullaniciAdi.Focus();
                return false;
            }

            // Ad Soyad required
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                DMLManager.ShowWarning("Ad soyad giriniz.");
                txtAdSoyad.Focus();
                return false;
            }

            // Email format check (basic)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@"))
            {
                DMLManager.ShowWarning("Geçerli bir email adresi giriniz.");
                txtEmail.Focus();
                return false;
            }

            // Password required for new users
            if (!_selectedKullaniciId.HasValue && string.IsNullOrWhiteSpace(txtParola.Text))
            {
                DMLManager.ShowWarning("Yeni kullanıcı için parola giriniz.");
                txtParola.Focus();
                return false;
            }

            // Password minimum length
            if (!string.IsNullOrWhiteSpace(txtParola.Text) && txtParola.Text.Length < 8)
            {
                DMLManager.ShowWarning("Parola en az 8 karakter olmalıdır.");
                txtParola.Focus();
                return false;
            }

            // At least one role must be selected
            bool hasRole = false;
            for (int i = 0; i < chklstRoller.ItemCount; i++)
            {
                if (chklstRoller.GetItemChecked(i))
                {
                    hasRole = true;
                    break;
                }
            }

            if (!hasRole)
            {
                DMLManager.ShowWarning("En az bir rol seçmelisiniz.");
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtKullaniciAdi.Text = string.Empty;
            txtAdSoyad.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtParola.Text = string.Empty;
            chkAktif.Checked = true;

            for (int i = 0; i < chklstRoller.ItemCount; i++)
            {
                chklstRoller.SetItemChecked(i, false);
            }

            _selectedKullaniciId = null;
        }

        private void SetFormState(FormState state)
        {
            _isEditing = (state == FormState.New || state == FormState.Edit);

            // Enable/disable input controls
            txtKullaniciAdi.Properties.ReadOnly = !_isEditing || (state == FormState.Edit); // Username cannot be changed
            txtAdSoyad.Properties.ReadOnly = !_isEditing;
            txtEmail.Properties.ReadOnly = !_isEditing;
            txtParola.Properties.ReadOnly = !_isEditing;
            chkAktif.Properties.ReadOnly = !_isEditing;
            chklstRoller.Enabled = _isEditing;

            // Button visibility
            btnYeni.Enabled = (state == FormState.View);
            btnDuzenle.Enabled = (state == FormState.View) && _selectedKullaniciId.HasValue;
            btnPasifle.Enabled = (state == FormState.View) && _selectedKullaniciId.HasValue;
            btnParolaSifirla.Enabled = (state == FormState.View) && _selectedKullaniciId.HasValue;
            btnKaydet.Enabled = _isEditing;
            btnIptal.Enabled = _isEditing;

            // Grid interaction
            grdKullanicilar.Enabled = !_isEditing;
        }

        private enum FormState
        {
            View,
            New,
            Edit
        }
    }
}
