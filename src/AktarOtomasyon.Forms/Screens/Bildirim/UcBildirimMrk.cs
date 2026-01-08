using System;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Bildirim
{
    /// <summary>
    /// Notification Center - Sprint 9 enhanced
    /// Shows system notifications with icons, bold unread, and navigation support
    /// </summary>
    public partial class UcBildirimMrk : UcBase
    {
        public UcBildirimMrk()
        {
            InitializeComponent();
            ApplyGridStandards();
        }

        /// <summary>
        /// Sprint 9: Apply grid standards and formatting
        /// </summary>
        private void ApplyGridStandards()
        {
            var view = gridControl.MainView as GridView;
            if (view != null)
            {
                GridHelper.ApplyStandardFormatting(view);

                // Format date columns
                if (view.Columns["OlusturmaTarih"] != null)
                    GridHelper.FormatDateColumn(view.Columns["OlusturmaTarih"]);
            }
        }

        public override void LoadData()
        {
            try
            {
                LoadDurumCombo();
                RefreshList();
                UpdateOkunmamisSayisi();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcBildirimMrk.LoadData hata: " + ex.Message, "BILDIRIM_MRK");
                MessageHelper.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadDurumCombo()
        {
            cmbDurum.Properties.Items.Clear();
            cmbDurum.Properties.Items.Add("Tümü");
            cmbDurum.Properties.Items.Add("Okunmamış");
            cmbDurum.Properties.Items.Add("Okunmuş");
            cmbDurum.SelectedIndex = 0;
        }

        private void RefreshList()
        {
            try
            {
                gridControl.BeginUpdate();

                var bildirimService = InterfaceFactory.Bildirim;

                bool? okundu = null;
                if (cmbDurum.SelectedIndex == 1) okundu = false; // Okunmamış
                if (cmbDurum.SelectedIndex == 2) okundu = true;  // Okunmuş

                var bildirimler = bildirimService.Listele(null, okundu);

                gridControl.DataSource = bildirimler;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcBildirimMrk.RefreshList hata: " + ex.Message, "BILDIRIM_MRK");
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
        private void UpdateOkunmamisSayisi()
        {
            try
            {
                var bildirimService = InterfaceFactory.Bildirim;
                var okunmamisSayisi = bildirimService.OkunmamisSayisi(null);

                lblOkunmamisSayisi.Text = string.Format("Okunmamış: {0}", okunmamisSayisi);

                if (okunmamisSayisi > 0)
                {
                    lblOkunmamisSayisi.Appearance.ForeColor = GridHelper.StandardColors.Info;
                    lblOkunmamisSayisi.Appearance.Font = new Font(lblOkunmamisSayisi.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    lblOkunmamisSayisi.Appearance.ForeColor = GridHelper.StandardColors.Normal;
                    lblOkunmamisSayisi.Appearance.Font = new Font(lblOkunmamisSayisi.Appearance.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcBildirimMrk.UpdateOkunmamisSayisi hata: " + ex.Message, "BILDIRIM_MRK");
            }
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            RefreshList();
            UpdateOkunmamisSayisi();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            cmbDurum.SelectedIndex = 0;
            RefreshList();
            UpdateOkunmamisSayisi();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            RefreshList();
            UpdateOkunmamisSayisi();
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            bool hasRow = view.FocusedRowHandle >= 0;
            btnOkundu.Enabled = hasRow;
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            ShowBildirimDetay();
        }

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowBildirimDetay();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Sprint 9: Enhanced notification detail display
        /// </summary>
        private void ShowBildirimDetay()
        {
            var view = gridControl.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            try
            {
                var bildirimId = view.GetFocusedRowCellValue("BildirimId");
                var baslikObj = view.GetFocusedRowCellValue("Baslik");
                var icerikObj = view.GetFocusedRowCellValue("Icerik");
                var baslik = baslikObj != null ? baslikObj.ToString() : string.Empty;
                var icerik = icerikObj != null ? icerikObj.ToString() : string.Empty;
                var okundu = Convert.ToBoolean(view.GetFocusedRowCellValue("Okundu"));

                if (bildirimId == null) return;

                // Detay göster
                var detayMesaj = string.Format("{0}\n\n{1}", baslik, icerik);
                MessageHelper.ShowInfo(detayMesaj, "Bildirim Detayı");

                // Okunmamışsa okundu olarak işaretle
                if (!okundu)
                {
                    var bildirimService = InterfaceFactory.Bildirim;
                    var error = bildirimService.Okundu(Convert.ToInt32(bildirimId));

                    if (error != null)
                    {
                        MessageHelper.ShowError(error);
                    }
                    else
                    {
                        RefreshList();
                        UpdateOkunmamisSayisi();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcBildirimMrk.ShowBildirimDetay hata: " + ex.Message, "BILDIRIM_MRK");
                MessageHelper.ShowError("Bildirim detayı gösterilirken hata oluştu: " + ex.Message);
            }
        }

        private void btnOkundu_Click(object sender, EventArgs e)
        {
            var view = gridControl.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            try
            {
                var bildirimId = view.GetFocusedRowCellValue("BildirimId");
                if (bildirimId == null) return;

                var bildirimService = InterfaceFactory.Bildirim;
                var error = bildirimService.Okundu(Convert.ToInt32(bildirimId));

                if (error != null)
                {
                    MessageHelper.ShowError(error);
                }
                else
                {
                    MessageHelper.ShowSuccess("Bildirim okundu olarak işaretlendi.");
                    RefreshList();
                    UpdateOkunmamisSayisi();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcBildirimMrk.btnOkundu_Click hata: " + ex.Message, "BILDIRIM_MRK");
                MessageHelper.ShowError("Bildirim okundu olarak işaretlenemedi: " + ex.Message);
            }
        }

        private void btnTumunuOkundu_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageHelper.ShowConfirmation(
                    "Tüm bildirimler okundu olarak işaretlenecek. Devam etmek istiyor musunuz?",
                    "Toplu Okundu İşlemi"
                );

                if (!confirmResult) return;

                var bildirimService = InterfaceFactory.Bildirim;
                var okunmamisBildirimler = bildirimService.Listele(null, false);

                int basariliSayisi = 0;
                int hataSayisi = 0;

                foreach (var bildirim in okunmamisBildirimler)
                {
                    var error = bildirimService.Okundu(bildirim.BildirimId);
                    if (error == null)
                        basariliSayisi++;
                    else
                        hataSayisi++;
                }

                if (hataSayisi > 0)
                {
                    MessageHelper.ShowWarning(string.Format("{0} bildirim okundu olarak işaretlendi, {1} bildirimde hata oluştu.", basariliSayisi, hataSayisi));
                }
                else if (basariliSayisi > 0)
                {
                    MessageHelper.ShowSuccess(string.Format("{0} bildirim okundu olarak işaretlendi.", basariliSayisi));
                }

                RefreshList();
                UpdateOkunmamisSayisi();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcBildirimMrk.btnTumunuOkundu_Click hata: " + ex.Message, "BILDIRIM_MRK");
                MessageHelper.ShowError("Toplu işlem sırasında hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Sprint 9: Enhanced with notification type icons and standard colors
        /// Unread notifications: Light cyan background, bold text
        /// STOK_KRITIK: Red text
        /// STOK_ACIL: Orange text
        /// SIPARIS: Blue text
        /// AI: Purple text
        /// </summary>
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            try
            {
                var okundu = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Okundu"));
                var bildirimTipObj = view.GetRowCellValue(e.RowHandle, "BildirimTip");
                var bildirimTip = bildirimTipObj != null ? bildirimTipObj.ToString() : null;

                // Okunmamış bildirimleri vurgula
                if (!okundu)
                {
                    e.Appearance.BackColor = Color.FromArgb(240, 248, 255); // Light cyan
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }

                // Bildirim tipine göre renk kodlama
                if (!string.IsNullOrEmpty(bildirimTip))
                {
                    if (bildirimTip.Contains("STOK_KRITIK"))
                        e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
                    else if (bildirimTip.Contains("STOK_ACIL") || bildirimTip.Contains("STOK"))
                        e.Appearance.ForeColor = GridHelper.StandardColors.Acil;
                    else if (bildirimTip.Contains("SIPARIS"))
                        e.Appearance.ForeColor = GridHelper.StandardColors.Info;
                    else if (bildirimTip.Contains("AI"))
                        e.Appearance.ForeColor = Color.FromArgb(156, 39, 176); // Purple
                }
            }
            catch
            {
                // Ignore formatting errors
            }
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
