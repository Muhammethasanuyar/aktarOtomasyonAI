using System;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Stok
{
    /// <summary>
    /// Critical stock screen - Sprint 9 enhanced
    /// Shows products at or below critical stock levels with visual indicators
    /// </summary>
    public partial class UcStokKritik : UcBase
    {
        public UcStokKritik()
        {
            InitializeComponent();
            ApplyGridStandards();
        }

        /// <summary>
        /// Sprint 9: Apply grid standards and conditional formatting
        /// </summary>
        private void ApplyGridStandards()
        {
            var view = gridControl.MainView as GridView;
            if (view != null)
            {
                GridHelper.ApplyStandardFormatting(view);

                // Format columns
                if (view.Columns["MevcutStok"] != null)
                    GridHelper.FormatQuantityColumn(view.Columns["MevcutStok"]);
                if (view.Columns["MinStok"] != null)
                    GridHelper.FormatQuantityColumn(view.Columns["MinStok"]);
                if (view.Columns["EmniyetStok"] != null)
                    GridHelper.FormatQuantityColumn(view.Columns["EmniyetStok"]);
                if (view.Columns["HedefStok"] != null)
                    GridHelper.FormatQuantityColumn(view.Columns["HedefStok"]);
            }
        }

        public override void LoadData()
        {
            try
            {
                RefreshList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcStokKritik.LoadData hata: " + ex.Message, "STOK_KRITIK");
                MessageHelper.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void RefreshList()
        {
            try
            {
                gridControl.BeginUpdate();

                var stokService = InterfaceFactory.Stok;
                var kritikStoklar = stokService.KritikListele();

                // Sprint 9: Fix encoding issues
                if (kritikStoklar != null)
                {
                    foreach (var k in kritikStoklar)
                    {
                        k.UrunAdi = TextHelper.FixEncoding(k.UrunAdi);
                    }
                }

                gridControl.DataSource = kritikStoklar;

                UpdateWarningLabel(kritikStoklar.Count);

                // Sprint 9: Show empty state if no critical items
                if (kritikStoklar.Count == 0)
                {
                    // TODO: Add EmptyStatePanel in Designer
                    // For now, the warning label shows the message
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcStokKritik.RefreshList hata: " + ex.Message, "STOK_KRITIK");
                MessageHelper.ShowError("Liste yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                gridControl.EndUpdate();
            }
        }

        /// <summary>
        /// Sprint 9: Enhanced with standard colors
        /// </summary>
        private void UpdateWarningLabel(int count)
        {
            lblUyari.Text = string.Format("Kritik seviyedeki ürün sayısı: {0}", count);

            if (count > 0)
            {
                lblUyari.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
                lblUyari.Appearance.Font = new Font(lblUyari.Appearance.Font, FontStyle.Bold);
            }
            else
            {
                lblUyari.Appearance.ForeColor = GridHelper.StandardColors.Normal;
                lblUyari.Appearance.Font = new Font(lblUyari.Appearance.Font, FontStyle.Regular);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            bool hasRow = view.FocusedRowHandle >= 0;
            btnStokGor.Enabled = hasRow;
            btnSiparisOner.Enabled = hasRow;
            btnSiparisTaslak.Enabled = hasRow;
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            OpenUrunKart();
        }

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenUrunKart();
                e.Handled = true;
            }
        }

        private void OpenUrunKart()
        {
            var view = gridControl.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            try
            {
                var urunId = view.GetFocusedRowCellValue("UrunId");
                if (urunId == null) return;

                var frm = this.FindForm();
                if (frm != null && frm.MdiParent != null)
                {
                    NavigationManager.OpenScreen("URUN_KART", frm.MdiParent, urunId);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcStokKritik.OpenUrunKart hata: " + ex.Message, "STOK_KRITIK");
                MessageHelper.ShowError("Ürün kartı açılırken hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Sprint 9: Enhanced conditional formatting with standard colors
        /// Red background: Out of stock (0)
        /// Orange background: Critical (less than 50% of min)
        /// Light red background: Below minimum
        /// </summary>
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            try
            {
                var mevcutStok = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MevcutStok"));
                var minStok = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MinStok"));

                if (mevcutStok == 0)
                {
                    // Out of stock - dark red background
                    e.Appearance.BackColor = Color.FromArgb(200, 50, 50);
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (mevcutStok < (minStok * 0.5m))
                {
                    // Very critical - orange/red background
                    e.Appearance.BackColor = Color.FromArgb(255, 100, 80);
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (mevcutStok <= minStok)
                {
                    // Below minimum - light red background
                    e.Appearance.BackColor = Color.FromArgb(255, 230, 230);
                    e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
                }
            }
            catch
            {
                // Ignore formatting errors
            }
        }

        private void btnStokGor_Click(object sender, EventArgs e)
        {
            OpenUrunKart();
        }

        private void btnSiparisOner_Click(object sender, EventArgs e)
        {
            MessageHelper.ShowInfo("Sipariş önerme özelliği henüz aktif değil.");
        }

        private void btnSiparisTaslak_Click(object sender, EventArgs e)
        {
            MessageHelper.ShowInfo("Sipariş taslağı oluşturma özelliği henüz aktif değil.");
        }

        public override bool HasChanges()
        {
            return false; // Liste ekranı için değişiklik yok
        }

        public override string SaveData()
        {
            // Liste ekranı için kaydetme yok
            return null;
        }

        public override void ClearData()
        {
            // Liste ekranı için temizleme yok
        }
    }
}
