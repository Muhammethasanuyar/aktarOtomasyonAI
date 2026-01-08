using System;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Siparis.Interface;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    /// <summary>
    /// Order List Screen - Sprint 9 enhanced
    /// Shows orders with status badges, date range filter, and conditional formatting
    /// </summary>
    public partial class UcSiparisListe : UcBase
    {
        public UcSiparisListe()
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
                if (view.Columns["SiparisTarih"] != null)
                    GridHelper.FormatDateColumn(view.Columns["SiparisTarih"]);
                if (view.Columns["TeslimTarih"] != null)
                    GridHelper.FormatDateColumn(view.Columns["TeslimTarih"]);

                // Format money columns
                if (view.Columns["ToplamTutar"] != null)
                    GridHelper.FormatMoneyColumn(view.Columns["ToplamTutar"]);
            }
        }

        public override void LoadData()
        {
            try
            {
                LoadDurumCombo();
                LoadTedarikciLookup();
                RefreshList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.LoadData hata: " + ex.Message, "SIP_LISTE");
                MessageHelper.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadDurumCombo()
        {
            try
            {
                cmbDurum.Properties.Items.Clear();
                cmbDurum.Properties.Items.Add("Tümü");

                var durumlar = SiparisLookupProvider.GetSiparisDurumlari();
                foreach (var durum in durumlar)
                {
                    cmbDurum.Properties.Items.Add(durum.Tanim);
                }

                cmbDurum.SelectedIndex = 0; // Tümü
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.LoadDurumCombo hata: " + ex.Message, "SIP_LISTE");
            }
        }

        private void LoadTedarikciLookup()
        {
            try
            {
                var tedarikciler = SiparisLookupProvider.GetTedarikciListe();

                lkpTedarikci.Properties.DataSource = tedarikciler;
                lkpTedarikci.Properties.DisplayMember = "TedarikciAdi";
                lkpTedarikci.Properties.ValueMember = "TedarikciId";
                lkpTedarikci.Properties.NullText = "Tümü";
                lkpTedarikci.EditValue = null;

                lkpTedarikci.Properties.Columns.Clear();
                lkpTedarikci.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TedarikciAdi", "Tedarikçi"));
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.LoadTedarikciLookup hata: " + ex.Message, "SIP_LISTE");
            }
        }

        private void RefreshList()
        {
            try
            {
                gridControl.BeginUpdate();

                var siparisService = InterfaceFactory.Siparis;
                var siparisler = siparisService.Listele();

                gridControl.DataSource = siparisler;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.RefreshList hata: " + ex.Message, "SIP_LISTE");
                MessageHelper.ShowError("Liste yüklenirken hata oluştu: " + ex.Message);
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

                // Durum filtresi
                if (cmbDurum.SelectedIndex > 0) // "Tümü" dışında
                {
                    var durumTanim = cmbDurum.SelectedItem.ToString();
                    var durumlar = SiparisLookupProvider.GetSiparisDurumlari();
                    var selectedDurum = durumlar.Find(d => d.Tanim == durumTanim);

                    if (selectedDurum != null)
                    {
                        filterString = string.Format("[Durum] = '{0}'", selectedDurum.Kod);
                    }
                }

                // Tedarikçi filtresi
                if (lkpTedarikci.EditValue != null)
                {
                    var tedarikciId = Convert.ToInt32(lkpTedarikci.EditValue);
                    var tedarikciFiltre = string.Format("[TedarikciId] = {0}", tedarikciId);

                    if (!string.IsNullOrEmpty(filterString))
                        filterString += " And " + tedarikciFiltre;
                    else
                        filterString = tedarikciFiltre;
                }

                // Tarih aralığı filtresi
                if (dateBaslangic.EditValue != null && dateBitis.EditValue != null)
                {
                    var baslangic = dateBaslangic.DateTime.Date;
                    var bitis = dateBitis.DateTime.Date.AddDays(1).AddSeconds(-1);

                    var tarihFiltre = string.Format("[SiparisTarih] >= #{0:MM/dd/yyyy}# And [SiparisTarih] <= #{1:MM/dd/yyyy}#",
                        baslangic, bitis);

                    if (!string.IsNullOrEmpty(filterString))
                        filterString += " And " + tarihFiltre;
                    else
                        filterString = tarihFiltre;
                }

                view.ActiveFilterString = filterString;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.ApplyFilter hata: " + ex.Message, "SIP_LISTE");
            }
            finally
            {
                var view = gridControl.MainView as GridView;
                if (view != null) view.EndUpdate();
            }
        }

        private void ClearFilter()
        {
            cmbDurum.SelectedIndex = 0; // Tümü
            lkpTedarikci.EditValue = null;
            dateBaslangic.EditValue = null;
            dateBitis.EditValue = null;

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

        private void btnYeniSiparis_Click(object sender, EventArgs e)
        {
            try
            {
                var parentForm = this.FindForm();
                if (parentForm == null) return;
                var mdiParent = parentForm.MdiParent;
                if (mdiParent == null)
                {
                    MessageHelper.ShowWarning("MDI Parent bulunamadı.");
                    return;
                }

                NavigationManager.OpenScreen("SIP_TASLAK", mdiParent, null);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.btnYeniSiparis_Click hata: " + ex.Message, "SIP_LISTE");
                MessageHelper.ShowError("Ekran açılırken hata oluştu: " + ex.Message);
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridControl.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                var siparis = view.GetRow(view.FocusedRowHandle) as SiparisModel;
                if (siparis == null) return;

                // Sadece TASLAK durumundaki siparişler düzenlenebilir
                if (siparis.Durum != "TASLAK")
                {
                    MessageHelper.ShowWarning("Sadece TASLAK durumundaki siparişler düzenlenebilir.");
                    return;
                }

                var parentForm = this.FindForm();
                if (parentForm == null) return;
                var mdiParent = parentForm.MdiParent;
                if (mdiParent == null)
                {
                    MessageHelper.ShowWarning("MDI Parent bulunamadı.");
                    return;
                }

                NavigationManager.OpenScreen("SIP_TASLAK", mdiParent, siparis.SiparisId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.btnDuzenle_Click hata: " + ex.Message, "SIP_LISTE");
                MessageHelper.ShowError("Ekran açılırken hata oluştu: " + ex.Message);
            }
        }

        private void btnDurumGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridControl.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                var siparis = view.GetRow(view.FocusedRowHandle) as SiparisModel;
                if (siparis == null) return;

                // İptal edilmiş siparişlerin durumu değiştirilemez
                if (siparis.Durum == "IPTAL")
                {
                    MessageHelper.ShowWarning("İptal edilmiş siparişin durumu değiştirilemez.");
                    return;
                }

                // Durum seçim dialogu göster
                using (var dlg = new Form())
                {
                    dlg.Text = "Durum Güncelle";
                    dlg.Size = new Size(300, 150);
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                    dlg.MaximizeBox = false;
                    dlg.MinimizeBox = false;

                    var lbl = new Label { Text = "Yeni Durum:", Left = 10, Top = 10, Width = 80 };
                    var cmb = new DevExpress.XtraEditors.ComboBoxEdit { Left = 100, Top = 10, Width = 180 };

                    var durumlar = SiparisLookupProvider.GetSiparisDurumlari();
                    foreach (var durum in durumlar)
                    {
                        cmb.Properties.Items.Add(durum.Tanim);
                    }

                    // Mevcut durumu seç
                    var mevcutDurum = durumlar.Find(d => d.Kod == siparis.Durum);
                    if (mevcutDurum != null)
                    {
                        cmb.SelectedItem = mevcutDurum.Tanim;
                    }

                    var btnTamam = new DevExpress.XtraEditors.SimpleButton { Text = "Tamam", Left = 100, Top = 50, DialogResult = DialogResult.OK };
                    var btnIptal = new DevExpress.XtraEditors.SimpleButton { Text = "İptal", Left = 200, Top = 50, DialogResult = DialogResult.Cancel };

                    dlg.Controls.Add(lbl);
                    dlg.Controls.Add(cmb);
                    dlg.Controls.Add(btnTamam);
                    dlg.Controls.Add(btnIptal);
                    dlg.AcceptButton = btnTamam;
                    dlg.CancelButton = btnIptal;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (cmb.SelectedItem == null)
                        {
                            MessageHelper.ShowWarning("Lütfen bir durum seçiniz.");
                            return;
                        }

                        var yeniDurumTanim = cmb.SelectedItem.ToString();
                        var yeniDurum = durumlar.Find(d => d.Tanim == yeniDurumTanim);

                        if (yeniDurum == null) return;

                        var siparisService = InterfaceFactory.Siparis;
                        var error = siparisService.DurumGuncelle(siparis.SiparisId, yeniDurum.Kod);

                        if (error != null)
                        {
                            MessageHelper.ShowError(error);
                        }
                        else
                        {
                            MessageHelper.ShowSuccess("Sipariş durumu başarıyla güncellendi.");
                            RefreshList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.btnDurumGuncelle_Click hata: " + ex.Message, "SIP_LISTE");
                DMLManager.ShowError("Durum güncellenirken hata oluştu: " + ex.Message);
            }
        }

        private void btnKritikStoktan_Click(object sender, EventArgs e)
        {
            try
            {
                // Tedarikçi seçim dialogu göster
                using (var dlg = new Form())
                {
                    dlg.Text = "Kritik Stoktan Sipariş Oluştur";
                    dlg.Size = new Size(350, 150);
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                    dlg.MaximizeBox = false;
                    dlg.MinimizeBox = false;

                    var lbl = new Label { Text = "Tedarikçi:", Left = 10, Top = 10, Width = 80 };
                    var lkp = new DevExpress.XtraEditors.LookUpEdit { Left = 100, Top = 10, Width = 230 };

                    var tedarikciler = SiparisLookupProvider.GetTedarikciListe();
                    lkp.Properties.DataSource = tedarikciler;
                    lkp.Properties.DisplayMember = "TedarikciAdi";
                    lkp.Properties.ValueMember = "TedarikciId";
                    lkp.Properties.NullText = "Seçiniz...";
                    lkp.Properties.Columns.Clear();
                    lkp.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TedarikciAdi", "Tedarikçi"));

                    var btnTamam = new DevExpress.XtraEditors.SimpleButton { Text = "Tamam", Left = 130, Top = 50, DialogResult = DialogResult.OK };
                    var btnIptal = new DevExpress.XtraEditors.SimpleButton { Text = "İptal", Left = 230, Top = 50, DialogResult = DialogResult.Cancel };

                    dlg.Controls.Add(lbl);
                    dlg.Controls.Add(lkp);
                    dlg.Controls.Add(btnTamam);
                    dlg.Controls.Add(btnIptal);
                    dlg.AcceptButton = btnTamam;
                    dlg.CancelButton = btnIptal;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (lkp.EditValue == null)
                        {
                            MessageHelper.ShowWarning("Lütfen bir tedarikçi seçiniz.");
                            return;
                        }

                        var tedarikciId = Convert.ToInt32(lkp.EditValue);

                        var siparisService = InterfaceFactory.Siparis;
                        var error = siparisService.TaslakOlusturKritikStoktan(tedarikciId);

                        if (error != null)
                        {
                            MessageHelper.ShowError(error);
                        }
                        else
                        {
                            MessageHelper.ShowSuccess("Kritik stoktan sipariş başarıyla oluşturuldu.");
                            RefreshList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.btnKritikStoktan_Click hata: " + ex.Message, "SIP_LISTE");
                DMLManager.ShowError("Sipariş oluşturulurken hata oluştu: " + ex.Message);
            }
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridControl.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                var siparis = view.GetRow(view.FocusedRowHandle) as SiparisModel;
                if (siparis == null) return;

                // Sadece GONDERILDI veya KISMI durumundaki siparişler teslim alınabilir
                if (siparis.Durum != "GONDERILDI" && siparis.Durum != "KISMI")
                {
                    MessageHelper.ShowWarning("Sadece GONDERILDI veya KISMI durumundaki siparişler teslim alınabilir.");
                    return;
                }

                var parentForm = this.FindForm();
                if (parentForm == null) return;
                var mdiParent = parentForm.MdiParent;
                if (mdiParent == null)
                {
                    MessageHelper.ShowWarning("MDI Parent bulunamadı.");
                    return;
                }

                NavigationManager.OpenScreen("SIP_TESLIM", mdiParent, siparis.SiparisId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.btnTeslimAl_Click hata: " + ex.Message, "SIP_LISTE");
                MessageHelper.ShowError("Ekran açılırken hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Sprint 9: Enhanced order status badges with standard colors
        /// TASLAK: Light yellow - draft orders
        /// GONDERILDI: Light blue - sent orders
        /// KISMI: Light orange - partial delivery
        /// TAMAMLANDI: Light green - completed
        /// IPTAL: Gray with strikeout - cancelled
        /// </summary>
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            try
            {
                var siparis = view.GetRow(e.RowHandle) as SiparisModel;
                if (siparis == null) return;

                switch (siparis.Durum)
                {
                    case "TASLAK":
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 224); // Light yellow
                        e.Appearance.ForeColor = Color.FromArgb(184, 134, 11); // Dark goldenrod
                        break;
                    case "GONDERILDI":
                        e.Appearance.BackColor = Color.FromArgb(224, 240, 255); // Light blue
                        e.Appearance.ForeColor = GridHelper.StandardColors.Info;
                        break;
                    case "KISMI":
                        e.Appearance.BackColor = Color.FromArgb(255, 240, 230); // Light orange
                        e.Appearance.ForeColor = GridHelper.StandardColors.Acil;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                    case "TAMAMLANDI":
                        e.Appearance.BackColor = Color.FromArgb(240, 255, 240); // Light green
                        e.Appearance.ForeColor = GridHelper.StandardColors.Normal;
                        break;
                    case "IPTAL":
                        e.Appearance.BackColor = Color.FromArgb(240, 240, 240); // Light gray
                        e.Appearance.ForeColor = GridHelper.StandardColors.Pasif;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                        break;
                }
            }
            catch
            {
                // Ignore formatting errors
            }
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null || view.FocusedRowHandle < 0)
                {
                    btnDuzenle.Enabled = false;
                    btnDurumGuncelle.Enabled = false;
                    btnTeslimAl.Enabled = false;
                    return;
                }

                var siparis = view.GetRow(view.FocusedRowHandle) as SiparisModel;
                if (siparis == null)
                {
                    btnDuzenle.Enabled = false;
                    btnDurumGuncelle.Enabled = false;
                    btnTeslimAl.Enabled = false;
                    return;
                }

                // Düzenle: Sadece TASLAK
                btnDuzenle.Enabled = (siparis.Durum == "TASLAK");

                // Durum Güncelle: İPTAL hariç
                btnDurumGuncelle.Enabled = (siparis.Durum != "IPTAL");

                // Teslim Al: Sadece GONDERILDI veya KISMI
                btnTeslimAl.Enabled = (siparis.Durum == "GONDERILDI" || siparis.Durum == "KISMI");
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisListe.gridView_FocusedRowChanged hata: " + ex.Message, "SIP_LISTE");
            }
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            btnDuzenle_Click(sender, e);
        }
    }
}
