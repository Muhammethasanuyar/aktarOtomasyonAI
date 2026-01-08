using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Util.DataAccess;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Dashboard
{
    /// <summary>
    /// Ana Dashboard UserControl.
    /// Sprint 9: Real-time dashboard with SP integration, Modern Widget Layout
    /// STANDART: Tüm business logic burada - form sadece shell.
    /// </summary>
    public partial class UcANA_DASH : UcBase
    {
        public UcANA_DASH()
        {
            InitializeComponent();
        }

        public override void LoadData()
        {
            try
            {
                // Apply modern card styling to dashboard widgets
                ApplyCardStyling();

                // Format Grids (One-time or refresh)
                GridHelper.ApplyStandardFormatting(gvBildirimler);
                GridHelper.ApplyStandardFormatting(gvHareketler);

                // Specific column formatting
                GridHelper.FormatDateColumn(colTarih);
                GridHelper.FormatDateColumn(colHareketTarih);
                GridHelper.FormatQuantityColumn(colMiktar);
                
                // Load Widget Data
                LoadKritikStokWidget();
                LoadBekleyenSiparisWidget();
                LoadBildirimWidget();
                LoadToplamUrunWidget();
                
                // Load Grid Data
                LoadSonHareketlerWidget();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("Dashboard.LoadData error: {0}", ex.Message), "DASHBOARD");
                MessageHelper.ShowError("Dashboard yüklenirken hata oluştu.");
            }
        }

        /// <summary>
        /// Applies modern card styling with hover effects to dashboard widgets
        /// </summary>
        private void ApplyCardStyling()
        {
            // Apply modern card style to all widget panels
            ModernTheme.ApplyModernCard(widgetKritikStok);
            ModernTheme.ApplyModernCard(widgetBekleyenSiparis);
            ModernTheme.ApplyModernCard(widgetTotalUrun);
            ModernTheme.ApplyModernCard(widgetBildirim);

            // Apply modern group styling
            ModernTheme.ApplyModernGroup(grpBildirimlerGrid);
            ModernTheme.ApplyModernGroup(grpHareketlerGrid);

            // Modernize labels
            ApplyModernLabelStyles();
        }

        /// <summary>
        /// Applies modern typography to dashboard labels
        /// </summary>
        private void ApplyModernLabelStyles()
        {
            // Title labels
            lblKritikStokTitle.Appearance.Font = ModernTheme.Typography.H6;
            lblBekleyenSiparisTitle.Appearance.Font = ModernTheme.Typography.H6;
            lblTotalUrunTitle.Appearance.Font = ModernTheme.Typography.H6;
            lblBildirimTitle.Appearance.Font = ModernTheme.Typography.H6;

            // Count labels (large numbers)
            lblKritikStokCount.Appearance.Font = ModernTheme.Typography.H2;
            lblBekleyenSiparisCount.Appearance.Font = ModernTheme.Typography.H2;
            lblTotalUrunCount.Appearance.Font = ModernTheme.Typography.H2;
            lblBildirimCount.Appearance.Font = ModernTheme.Typography.H2;

            // Description labels
            lblKritikStokDesc.Appearance.Font = ModernTheme.Typography.Caption;
            lblBekleyenSiparisDesc.Appearance.Font = ModernTheme.Typography.Caption;
            lblTotalUrunDesc.Appearance.Font = ModernTheme.Typography.Caption;
            lblBildirimDesc.Appearance.Font = ModernTheme.Typography.Caption;
        }

        /// <summary>
        /// Loads critical stock summary widget
        /// </summary>
        private void LoadKritikStokWidget()
        {
            try
            {
                var stokService = InterfaceFactory.Stok;
                var kritikStoklar = stokService.KritikListele();

                int kritikAdet = kritikStoklar != null ? kritikStoklar.Count : 0;
                lblKritikStokCount.Text = kritikAdet.ToString();

                // Conditional formatting - red if critical items exist
                lblKritikStokCount.ForeColor = kritikAdet > 0
                    ? GridHelper.StandardColors.Kritik
                    : GridHelper.StandardColors.Normal;

                // Make it bold if there are critical items
                if (kritikAdet > 0)
                {
                    lblKritikStokCount.Font = new Font(lblKritikStokCount.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadKritikStokWidget error: {0}", ex.Message), "DASHBOARD");
                lblKritikStokCount.Text = "?";
                lblKritikStokCount.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Loads pending order summary widget
        /// Calls: sp_dash_bekleyen_siparis_ozet
        /// </summary>
        private void LoadBekleyenSiparisWidget()
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_dash_bekleyen_siparis_ozet");
                    var dt = sMan.ExecuteQuery(cmd);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[0];
                        int taslakAdet = Convert.ToInt32(row["taslak_adet"]);
                        int gonderildiAdet = Convert.ToInt32(row["gonderildi_adet"]);
                        int kismiAdet = Convert.ToInt32(row["kismi_teslim_adet"]);
                        decimal bekleyenTutar = Convert.ToDecimal(row["bekleyen_tutar"]);

                        // Total pending count
                        int toplamBekleyen = taslakAdet + gonderildiAdet + kismiAdet;
                        lblBekleyenSiparisCount.Text = toplamBekleyen.ToString();

                        // Conditional formatting
                        lblBekleyenSiparisCount.ForeColor = toplamBekleyen > 0
                            ? GridHelper.StandardColors.Acil
                            : GridHelper.StandardColors.Normal;

                        // TODO: Add labels in Designer for breakdown
                        // lblTaslakCount.Text = taslakAdet.ToString();
                        // lblGonderildiCount.Text = gonderildiAdet.ToString();
                        // lblKismiCount.Text = kismiAdet.ToString();
                        // lblBekleyenTutar.Text = bekleyenTutar.ToString("N2") + " TL";
                    }
                    else
                    {
                        lblBekleyenSiparisCount.Text = "0";
                        lblBekleyenSiparisCount.ForeColor = GridHelper.StandardColors.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadBekleyenSiparisWidget error: {0}", ex.Message), "DASHBOARD");
                lblBekleyenSiparisCount.Text = "?";
                lblBekleyenSiparisCount.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Loads recent notifications count
        /// Calls: sp_dash_son_bildirimler
        /// </summary>
        private void LoadBildirimWidget()
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_dash_son_bildirimler");
                    cmd.Parameters.AddWithValue("@limit", 10);
                    var dt = sMan.ExecuteQuery(cmd);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Count unread notifications
                        int okunmamisSayisi = 0;
                        foreach (System.Data.DataRow row in dt.Rows)
                        {
                            if (!Convert.ToBoolean(row["okundu"]))
                                okunmamisSayisi++;
                        }

                        lblBildirimCount.Text = okunmamisSayisi.ToString();

                        // Conditional formatting
                        lblBildirimCount.ForeColor = okunmamisSayisi > 0
                            ? GridHelper.StandardColors.Info
                            : GridHelper.StandardColors.Normal;

                        // TODO: Bind to grid in Designer
                        grdBildirimler.DataSource = dt;
                        // ApplyBildirimFormatting();
                    }
                    else
                    {
                        lblBildirimCount.Text = "0";
                        lblBildirimCount.ForeColor = GridHelper.StandardColors.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadBildirimWidget error: {0}", ex.Message), "DASHBOARD");
                lblBildirimCount.Text = "?";
                lblBildirimCount.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Loads recent stock movements widget
        /// </summary>
        private void LoadSonHareketlerWidget()
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_dash_son_stok_hareket");
                    cmd.Parameters.AddWithValue("@limit", 20);
                    var dt = sMan.ExecuteQuery(cmd);

                    grdHareketler.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadSonHareketlerWidget error: {0}", ex.Message), "DASHBOARD");
            }
        }

        /// <summary>
        /// Loads total product count (New Widget)
        /// Uses simple scalar query
        /// </summary>
        private void LoadToplamUrunWidget()
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    // Simple query to count active products
                    var cmd = sMan.CreateCommand("SELECT COUNT(*) FROM urun WHERE aktif = 1", CommandType.Text);
                    var result = sMan.ExecuteScalar(cmd);
                    
                    lblTotalUrunCount.Text = result != null ? result.ToString() : "0";
                    lblTotalUrunDesc.Text = "Sistemdeki aktif ürünler";
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadToplamUrunWidget error: {0}", ex.Message), "DASHBOARD");
                lblTotalUrunCount.Text = "-";
            }
        }

        public override string SaveData()
        {
            // Dashboard has no save operation
            return null;
        }

        public override void ClearData()
        {
            // Dashboard has no clear operation
        }

        public override bool HasChanges()
        {
            // Dashboard has no unsaved changes
            return false;
        }

        /// <summary>
        /// Opens critical stock screen
        /// </summary>
        private void btnKritikStokDetay_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Use NavigationManager when available
                // NavigationManager.OpenScreen("STOK_KRITIK", ParentFrm.MdiParent);
                MessageHelper.ShowInfo("Kritik stok ekranı açılacak.");
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnKritikStokDetay_Click error: {0}", ex.Message), "DASHBOARD");
                MessageHelper.ShowError("Kritik stok ekranı açılırken hata oluştu.");
            }
        }

        /// <summary>
        /// Opens pending orders screen
        /// </summary>
        private void btnSiparisDetay_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Use NavigationManager when available
                // NavigationManager.OpenScreen("SIPARIS_LISTE", ParentFrm.MdiParent);
                MessageHelper.ShowInfo("Bekleyen sipariş ekranı açılacak.");
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnSiparisDetay_Click error: {0}", ex.Message), "DASHBOARD");
                MessageHelper.ShowError("Sipariş ekranı açılırken hata oluştu.");
            }
        }

        /// <summary>
        /// Opens notification center
        /// </summary>
        private void btnBildirimDetay_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Use NavigationManager when available
                // NavigationManager.OpenScreen("BILDIRIM_MRK", ParentFrm.MdiParent);
                MessageHelper.ShowInfo("Bildirim merkezi açılacak.");
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnBildirimDetay_Click error: {0}", ex.Message), "DASHBOARD");
                MessageHelper.ShowError("Bildirim merkezi açılırken hata oluştu.");
            }
        }
    }
}
