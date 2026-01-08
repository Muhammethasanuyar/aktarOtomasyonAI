using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraTab;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Main security management user control
    /// Contains tabs for User and Role management
    /// </summary>
    public partial class UcSecurityManagement : UcBase
    {
        private UcKullaniciYonetim ucKullaniciYonetim;
        private UcRolYonetim ucRolYonetim;

        public UcSecurityManagement()
        {
            InitializeComponent();
            InitializeUserControls();
        }

        private void InitializeUserControls()
        {
            try
            {
                // Create and add User Management UC
                ucKullaniciYonetim = new UcKullaniciYonetim();
                ucKullaniciYonetim.Dock = DockStyle.Fill;
                tabPageKullanici.Controls.Add(ucKullaniciYonetim);

                // Create and add Role Management UC
                ucRolYonetim = new UcRolYonetim();
                ucRolYonetim.Dock = DockStyle.Fill;
                tabPageRol.Controls.Add(ucRolYonetim);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("UcSecurityManagement InitializeUserControls error: {0}", ex.Message), "SEC");
                DMLManager.ShowError(string.Format("Kontroller yüklenirken hata: {0}", ex.Message));
            }
        }

        private void tabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                // Refresh data when switching tabs
                if (e.Page == tabPageKullanici && ucKullaniciYonetim != null)
                {
                    ucKullaniciYonetim.RefreshData();
                }
                else if (e.Page == tabPageRol && ucRolYonetim != null)
                {
                    ucRolYonetim.RefreshData();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("TabControl_SelectedPageChanged error: {0}", ex.Message), "SEC");
            }
        }
    }
}
