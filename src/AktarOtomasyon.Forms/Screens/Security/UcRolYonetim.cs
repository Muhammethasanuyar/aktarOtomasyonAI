using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Security.Interface.Models.Role;
using AktarOtomasyon.Security.Interface.Models.Permission;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Role management user control - Role CRUD operations
    /// </summary>
    public partial class UcRolYonetim : UcBase
    {
        private int? _selectedRolId = null;
        private bool _isEditing = false;

        public UcRolYonetim()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += UcRolYonetim_Load;
        }

        private void UcRolYonetim_Load(object sender, EventArgs e)
        {
            try
            {
                LoadRoles();
                LoadAvailablePermissions();
                SetFormState(FormState.View);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("UcRolYonetim_Load error: {0}", ex.Message), "SEC");
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
                LoadRoles();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("RefreshData error: {0}", ex.Message), "SEC");
            }
        }

        private void LoadRoles()
        {
            try
            {
                var roles = InterfaceFactory.Security.RolListele(aktif: null); // Load all (active and inactive)
                grdRoller.DataSource = roles;
                gvRoller.BestFitColumns();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadRoles error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Roller yüklenemedi: {0}", ex.Message));
            }
        }

        private void LoadAvailablePermissions()
        {
            try
            {
                var permissions = InterfaceFactory.Security.YetkiListele(modul: null); // Load all permissions

                chklstYetkiler.Items.Clear();

                // Group permissions by Modul
                var groupedPermissions = permissions
                    .GroupBy(p => p.Modul ?? "Genel")
                    .OrderBy(g => g.Key);

                foreach (var group in groupedPermissions)
                {
                    // Add group header (non-checkable)
                    chklstYetkiler.Items.Add(string.Format("[ {0} ]", group.Key), CheckState.Unchecked, false);

                    // Add permissions in this group
                    foreach (var perm in group.OrderBy(p => p.YetkiAdi))
                    {
                        chklstYetkiler.Items.Add(perm, string.Format("  {0}", perm.YetkiAdi));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadAvailablePermissions error: {0}", ex.Message), "SEC");
            }
        }

        private void LoadRolePermissions(int rolId)
        {
            try
            {
                var rolePermissions = InterfaceFactory.Security.RolYetkiListele(rolId);

                // Uncheck all first
                for (int i = 0; i < chklstYetkiler.ItemCount; i++)
                {
                    chklstYetkiler.SetItemChecked(i, false);
                }

                // Check assigned permissions
                for (int i = 0; i < chklstYetkiler.ItemCount; i++)
                {
                    var yetki = chklstYetkiler.Items[i].Value as YetkiDto;
                    if (yetki != null && rolePermissions.Any(rp => rp.YetkiId == yetki.YetkiId))
                    {
                        chklstYetkiler.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadRolePermissions error: {0}", ex.Message), "SEC");
            }
        }

        private void gvRoller_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvRoller.FocusedRowHandle < 0)
                    return;

                var row = gvRoller.GetFocusedRow() as RolListeItemDto;
                if (row != null)
                {
                    LoadRoleDetail(row.RolId);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("gvRoller_FocusedRowChanged error: {0}", ex.Message), "SEC");
            }
        }

        private void LoadRoleDetail(int rolId)
        {
            try
            {
                var role = InterfaceFactory.Security.RolGetir(rolId);
                if (role == null)
                {
                    DMLManager.ShowWarning("Rol bulunamadı.");
                    return;
                }

                _selectedRolId = role.RolId;

                txtRolAdi.Text = role.RolAdi;
                txtAciklama.Text = role.Aciklama;
                chkAktif.Checked = role.Aktif;

                LoadRolePermissions(rolId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadRoleDetail error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Detay yüklenemedi: {0}", ex.Message));
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            try
            {
                SetFormState(FormState.New);
                ClearForm();
                txtRolAdi.Focus();
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
                if (!_selectedRolId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen düzenlemek için bir rol seçin.");
                    return;
                }

                SetFormState(FormState.Edit);
                txtRolAdi.Focus();
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

                var model = new RolDto
                {
                    RolId = _selectedRolId ?? 0,
                    RolAdi = txtRolAdi.Text.Trim(),
                    Aciklama = txtAciklama.Text.Trim(),
                    Aktif = chkAktif.Checked
                };

                var error = InterfaceFactory.Security.RolKaydet(model);

                if (!DMLManager.KaydetKontrol(error))
                    return;

                // Get the saved role ID (if new)
                if (!_selectedRolId.HasValue)
                {
                    // Find the newly created role
                    var roles = InterfaceFactory.Security.RolListele(null);
                    var newRole = roles.FirstOrDefault(r => r.RolAdi == model.RolAdi);
                    if (newRole != null)
                    {
                        _selectedRolId = newRole.RolId;
                    }
                }

                // Save permission assignments
                if (_selectedRolId.HasValue)
                {
                    SaveRolePermissions(_selectedRolId.Value);
                }

                LoadRoles();
                SetFormState(FormState.View);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnKaydet_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Kaydetme hatası: {0}", ex.Message));
            }
        }

        private void SaveRolePermissions(int rolId)
        {
            try
            {
                // Get current permissions
                var currentPermissions = InterfaceFactory.Security.RolYetkiListele(rolId);

                // Get selected permissions from CheckedListBox
                var selectedPermissions = new List<int>();
                for (int i = 0; i < chklstYetkiler.ItemCount; i++)
                {
                    if (chklstYetkiler.GetItemChecked(i))
                    {
                        var yetki = chklstYetkiler.Items[i].Value as YetkiDto;
                        if (yetki != null)
                        {
                            selectedPermissions.Add(yetki.YetkiId);
                        }
                    }
                }

                // Add new permissions
                foreach (var yetkiId in selectedPermissions)
                {
                    if (!currentPermissions.Any(cp => cp.YetkiId == yetkiId))
                    {
                        InterfaceFactory.Security.RolYetkiEkle(
                            rolId,
                            yetkiId,
                            SessionManager.KullaniciId);
                    }
                }

                // Remove unchecked permissions
                foreach (var currentPermission in currentPermissions)
                {
                    if (!selectedPermissions.Contains(currentPermission.YetkiId))
                    {
                        InterfaceFactory.Security.RolYetkiSil(
                            rolId,
                            currentPermission.YetkiId,
                            SessionManager.KullaniciId);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("SaveRolePermissions error: {0}", ex.Message), "SEC");
                throw;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            try
            {
                SetFormState(FormState.View);

                if (_selectedRolId.HasValue)
                {
                    LoadRoleDetail(_selectedRolId.Value);
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
                if (!_selectedRolId.HasValue)
                {
                    DMLManager.ShowWarning("Lütfen pasifleştirmek için bir rol seçin.");
                    return;
                }

                if (!DMLManager.SilmeOnayAl("Rolü pasifleştirmek istediğinizden emin misiniz?"))
                    return;

                var error = InterfaceFactory.Security.RolPasifle(
                    _selectedRolId.Value,
                    SessionManager.KullaniciId);

                if (!DMLManager.SilKontrol(error))
                    return;

                LoadRoles();
                ClearForm();
                _selectedRolId = null;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnPasifle_Click error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Pasifleştirme hatası: {0}", ex.Message));
            }
        }

        private bool ValidateInput()
        {
            // Role name required
            if (string.IsNullOrWhiteSpace(txtRolAdi.Text))
            {
                DMLManager.ShowWarning("Rol adı giriniz.");
                txtRolAdi.Focus();
                return false;
            }

            // At least one permission must be selected
            bool hasPermission = false;
            for (int i = 0; i < chklstYetkiler.ItemCount; i++)
            {
                if (chklstYetkiler.GetItemChecked(i))
                {
                    var yetki = chklstYetkiler.Items[i].Value as YetkiDto;
                    if (yetki != null)
                    {
                        hasPermission = true;
                        break;
                    }
                }
            }

            if (!hasPermission)
            {
                DMLManager.ShowWarning("En az bir yetki seçmelisiniz.");
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtRolAdi.Text = string.Empty;
            txtAciklama.Text = string.Empty;
            chkAktif.Checked = true;

            for (int i = 0; i < chklstYetkiler.ItemCount; i++)
            {
                chklstYetkiler.SetItemChecked(i, false);
            }

            _selectedRolId = null;
        }

        private void SetFormState(FormState state)
        {
            _isEditing = (state == FormState.New || state == FormState.Edit);

            // Enable/disable input controls
            txtRolAdi.Properties.ReadOnly = !_isEditing || (state == FormState.Edit); // Role name cannot be changed
            txtAciklama.Properties.ReadOnly = !_isEditing;
            chkAktif.Properties.ReadOnly = !_isEditing;
            chklstYetkiler.Enabled = _isEditing;

            // Button visibility
            btnYeni.Enabled = (state == FormState.View);
            btnDuzenle.Enabled = (state == FormState.View) && _selectedRolId.HasValue;
            btnPasifle.Enabled = (state == FormState.View) && _selectedRolId.HasValue;
            btnKaydet.Enabled = _isEditing;
            btnIptal.Enabled = _isEditing;

            // Grid interaction
            grdRoller.Enabled = !_isEditing;
        }

        private enum FormState
        {
            View,
            New,
            Edit
        }
    }
}
