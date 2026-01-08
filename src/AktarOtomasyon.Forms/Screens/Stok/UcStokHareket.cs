using System;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Stok.Interface;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Stok
{
    public partial class UcStokHareket : UcBase
    {
        public UcStokHareket()
        {
            InitializeComponent();
            ApplyGridStandards();
        }

        /// <summary>
        /// Sprint 9: Apply grid standards to stock movement grid
        /// </summary>
        private void ApplyGridStandards()
        {
            var view = gridControl.MainView as GridView;
            if (view != null)
            {
                GridHelper.ApplyStandardFormatting(view);

                // Format date column
                if (view.Columns["HareketTarih"] != null)
                    GridHelper.FormatDateColumn(view.Columns["HareketTarih"]);

                // Format quantity column
                if (view.Columns["Miktar"] != null)
                    GridHelper.FormatQuantityColumn(view.Columns["Miktar"]);
            }
        }

        public override void LoadData()
        {
            try
            {
                LoadHareketTipCombo();
                RefreshList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcStokHareket.LoadData hata: " + ex.Message, "STOK_HAREKET");
                DMLManager.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadHareketTipCombo()
        {
            cmbHareketTip.Properties.Items.Clear();
            cmbHareketTip.Properties.Items.Add("Tümü");
            cmbHareketTip.Properties.Items.Add("GIRIS");
            cmbHareketTip.Properties.Items.Add("CIKIS");
            cmbHareketTip.Properties.Items.Add("SAYIM");
            cmbHareketTip.SelectedIndex = 0; // Tümü
        }

        private void RefreshList()
        {
            try
            {
                gridControl.BeginUpdate();

                var stokService = InterfaceFactory.Stok;
                var hareketler = stokService.HareketListele();

                // Sprint 9: Fix encoding issues
                if (hareketler != null)
                {
                    foreach (var h in hareketler)
                    {
                        h.UrunAdi = TextHelper.FixEncoding(h.UrunAdi);
                        h.Aciklama = TextHelper.FixEncoding(h.Aciklama);
                    }
                }

                gridControl.DataSource = hareketler;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcStokHareket.RefreshList hata: " + ex.Message, "STOK_HAREKET");
                DMLManager.ShowError("Liste yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                gridControl.EndUpdate();
            }
        }

        private void ApplyFilter()
        {
            try
            {
                var view = gridControl.MainView as GridView;
                if (view == null) return;

                view.BeginUpdate();

                // Filtre kaldır
                view.ActiveFilterString = string.Empty;

                string filterString = string.Empty;

                // Hareket tipi filtresi
                if (cmbHareketTip.SelectedIndex > 0) // "Tümü" dışında
                {
                    var hareketTip = cmbHareketTip.SelectedItem.ToString();
                    filterString = string.Format("[HareketTip] = '{0}'", hareketTip);
                }

                // Tarih aralığı filtresi
                if (dateBaslangic.EditValue != null && dateBitis.EditValue != null)
                {
                    var baslangic = dateBaslangic.DateTime.Date;
                    var bitis = dateBitis.DateTime.Date.AddDays(1).AddSeconds(-1);

                    var tarihFiltre = string.Format("[HareketTarih] >= #{0:MM/dd/yyyy}# And [HareketTarih] <= #{1:MM/dd/yyyy}#", baslangic, bitis);

                    if (!string.IsNullOrEmpty(filterString))
                        filterString += " And " + tarihFiltre;
                    else
                        filterString = tarihFiltre;
                }

                // Arama filtresi
                if (!string.IsNullOrWhiteSpace(txtArama.Text))
                {
                    var aramaFiltre = string.Format("Contains([UrunAdi], '{0}')", txtArama.Text);

                    if (!string.IsNullOrEmpty(filterString))
                        filterString += " And " + aramaFiltre;
                    else
                        filterString = aramaFiltre;
                }

                view.ActiveFilterString = filterString;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcStokHareket.ApplyFilter hata: " + ex.Message, "STOK_HAREKET");
            }
            finally
            {
                var view = gridControl.MainView as GridView;
                if (view != null) view.EndUpdate();
            }
        }

        private void ClearFilter()
        {
            cmbHareketTip.SelectedIndex = 0; // Tümü
            dateBaslangic.EditValue = null;
            dateBitis.EditValue = null;
            txtArama.Text = string.Empty;

            var view = gridControl.MainView as GridView;
            if (view != null)
            {
                view.ActiveFilterString = string.Empty;
            }
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            ClearFilter();
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
            btnDuzenle.Enabled = hasRow;
            btnSil.Enabled = hasRow;
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
                ErrorManager.LogMessage("UcStokHareket.OpenUrunKart hata: " + ex.Message, "STOK_HAREKET");
                DMLManager.ShowError("Ürün kartı açılırken hata oluştu: " + ex.Message);
            }
        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            var hareketTipObj = view.GetRowCellValue(e.RowHandle, "HareketTip");
            var hareketTip = hareketTipObj != null ? hareketTipObj.ToString() : null;

            if (hareketTip == "GIRIS")
                e.Appearance.BackColor = Color.LightGreen;
            else if (hareketTip == "CIKIS")
                e.Appearance.BackColor = Color.LightCoral;
            else if (hareketTip == "SAYIM")
                e.Appearance.BackColor = Color.LightBlue;
        }

        private void btnYeniHareket_Click(object sender, EventArgs e)
        {
            DMLManager.ShowInfo("Yeni hareket özelliği henüz aktif değil.");
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            DMLManager.ShowInfo("Düzenleme özelliği henüz aktif değil.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DMLManager.ShowInfo("Silme özelliği henüz aktif değil.");
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
            ClearFilter();
        }
    }
}
