using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Siparis.Interface;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    public partial class UcSiparisTaslak : UcBase
    {
        private int? _siparisId;
        private SiparisModel _siparisModel;
        private List<SiparisSatirModel> _satirList;
        private bool _dataChanged;

        public UcSiparisTaslak()
        {
            InitializeComponent();
            _satirList = new List<SiparisSatirModel>();
            _dataChanged = false;
            ApplyGridStandards();
        }

        /// <summary>
        /// Sprint 9: Apply grid standards to order lines grid
        /// </summary>
        private void ApplyGridStandards()
        {
            var view = gridControlSatir.MainView as GridView;
            if (view != null)
            {
                GridHelper.ApplyStandardFormatting(view);

                // Format money columns
                if (view.Columns["BirimFiyat"] != null)
                    GridHelper.FormatMoneyColumn(view.Columns["BirimFiyat"]);
                if (view.Columns["Tutar"] != null)
                    GridHelper.FormatMoneyColumn(view.Columns["Tutar"]);

                // Format quantity column
                if (view.Columns["Miktar"] != null)
                    GridHelper.FormatQuantityColumn(view.Columns["Miktar"]);
            }
        }

        public override void LoadData()
        {
            LoadData(null);
        }

        public void LoadData(int? siparisId)
        {
            try
            {
                _siparisId = siparisId;
                LoadTedarikciLookup();
                LoadDurumCombo();

                if (_siparisId.HasValue)
                {
                    LoadSiparisData(_siparisId.Value);
                }
                else
                {
                    InitializeNewOrder();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.LoadData hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void InitializeNewOrder()
        {
            _siparisModel = new SiparisModel();
            txtSiparisNo.Text = "(Otomatik)";
            txtSiparisNo.Enabled = false;
            lkpTedarikci.EditValue = null;
            dateSiparisTarih.DateTime = DateTime.Now;
            dateBeklenenTeslim.EditValue = null;
            cmbDurum.SelectedIndex = 0; // TASLAK
            cmbDurum.Enabled = false;
            memoAciklama.Text = string.Empty;

            // Tab2 disabled until saved
            tabSatirlar.PageEnabled = false;

            _satirList.Clear();
            gridControlSatir.DataSource = null;
            lblToplamTutar.Text = "0,00 ₺";

            _dataChanged = false;
        }

        private void LoadSiparisData(int siparisId)
        {
            try
            {
                var siparisService = InterfaceFactory.Siparis;
                _siparisModel = siparisService.Getir(siparisId);

                if (_siparisModel == null)
                {
                    MessageBox.Show("Sipariş bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtSiparisNo.Text = _siparisModel.SiparisNo;
                txtSiparisNo.Enabled = false;
                lkpTedarikci.EditValue = _siparisModel.TedarikciId;
                dateSiparisTarih.DateTime = _siparisModel.SiparisTarih;
                dateBeklenenTeslim.EditValue = _siparisModel.BeklenenTeslimTarih;
                cmbDurum.SelectedItem = _siparisModel.Durum;
                cmbDurum.Enabled = false; // Durum NavigationManager'dan Durum Güncelle ile değişir
                memoAciklama.Text = _siparisModel.Aciklama ?? string.Empty;

                // Enable Tab2
                tabSatirlar.PageEnabled = true;

                // Load satırlar
                LoadSatirlar(siparisId);

                _dataChanged = false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.LoadSiparisData hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Sipariş yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadTedarikciLookup()
        {
            try
            {
                // Cache'i yenile ve tedarikçi listesini yükle
                var tedarikciler = SiparisLookupProvider.GetTedarikciListe(forceRefresh: true);

                lkpTedarikci.Properties.DataSource = tedarikciler;
                lkpTedarikci.Properties.DisplayMember = "TedarikciAdi";
                lkpTedarikci.Properties.ValueMember = "TedarikciId";
                lkpTedarikci.Properties.NullText = "Seçiniz...";

                lkpTedarikci.Properties.Columns.Clear();
                lkpTedarikci.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TedarikciAdi", "Tedarikçi"));
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.LoadTedarikciLookup hata: " + ex.Message, "SIP_TASLAK");
            }
        }

        private void btnTedarikciEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Tedarikçi oluşturma dialogunu aç
                var dlgType = Type.GetType("AktarOtomasyon.Forms.Screens.Common.DlgTedarikci, AktarOtomasyon.Forms");
                if (dlgType == null)
                {
                    MessageHelper.ShowError("Tedarikçi dialog formu bulunamadı.");
                    return;
                }
                
                var dlg = Activator.CreateInstance(dlgType) as Form;
                if (dlg == null)
                {
                    MessageHelper.ShowError("Tedarikçi dialog formu oluşturulamadı.");
                    return;
                }
                
                try
                {
                    if (dlg.ShowDialog(this.FindForm()) == DialogResult.OK)
                    {
                        // Yeni oluşturulan tedarikçiyi seç
                        LoadTedarikciLookup();
                        
                        // Tedarikci property'sini reflection ile al
                        var tedarikciProp = dlgType.GetProperty("Tedarikci");
                        if (tedarikciProp != null)
                        {
                            var tedarikci = tedarikciProp.GetValue(dlg);
                            if (tedarikci != null)
                            {
                                var tedarikciIdProp = tedarikci.GetType().GetProperty("TedarikciId");
                                if (tedarikciIdProp != null)
                                {
                                    var tedarikciId = (int)tedarikciIdProp.GetValue(tedarikci);
                                    if (tedarikciId > 0)
                                    {
                                        lkpTedarikci.EditValue = tedarikciId;
                                        MessageHelper.ShowSuccess("Tedarikçi oluşturuldu ve seçildi.");
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    dlg.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Tedarikçi oluşturma hatası: " + ex.Message);
                ErrorManager.LogMessage("UcSiparisTaslak.btnTedarikciEkle error: " + ex.Message, "SIP_TASLAK");
            }
        }

        private void LoadDurumCombo()
        {
            try
            {
                cmbDurum.Properties.Items.Clear();

                var durumlar = SiparisLookupProvider.GetSiparisDurumlari();
                foreach (var durum in durumlar)
                {
                    cmbDurum.Properties.Items.Add(durum.Kod);
                }

                cmbDurum.SelectedIndex = 0; // TASLAK
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.LoadDurumCombo hata: " + ex.Message, "SIP_TASLAK");
            }
        }

        private void LoadSatirlar(int siparisId)
        {
            try
            {
                gridControlSatir.BeginUpdate();

                var siparisService = InterfaceFactory.Siparis;
                _satirList = siparisService.SatirListele(siparisId);

                gridControlSatir.DataSource = _satirList;

                UpdateToplamTutar();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.LoadSatirlar hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Satırlar yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                gridControlSatir.EndUpdate();
            }
        }

        private bool ValidateGenel()
        {
            if (lkpTedarikci.EditValue == null)
            {
                MessageBox.Show("Lütfen tedarikçi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTabPage = tabGenel;
                lkpTedarikci.Focus();
                return false;
            }

            if (dateSiparisTarih.EditValue == null)
            {
                MessageBox.Show("Lütfen sipariş tarihi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTabPage = tabGenel;
                dateSiparisTarih.Focus();
                return false;
            }

            return true;
        }

        public override string SaveData()
        {
            try
            {
                if (!ValidateGenel())
                    return "Validation failed";

                var siparisService = InterfaceFactory.Siparis;

                if (!_siparisId.HasValue)
                {
                    // New order - create via TaslakOlusturDetayli
                    var tedarikciId = Convert.ToInt32(lkpTedarikci.EditValue);

                    _siparisModel = siparisService.TaslakOlusturDetayli(tedarikciId);

                    if (_siparisModel == null)
                    {
                        DMLManager.ShowError("Sipariş oluşturulamadı.");
                        return "Sipariş oluşturulamadı.";
                    }

                    _siparisId = _siparisModel.SiparisId;

                    // Now update the header with other fields
                    _siparisModel.SiparisTarih = dateSiparisTarih.DateTime;
                    _siparisModel.BeklenenTeslimTarih = dateBeklenenTeslim.EditValue as DateTime?;
                    _siparisModel.Aciklama = memoAciklama.Text;

                    var error = siparisService.GuncelleHeader(_siparisModel);

                    if (error != null)
                    {
                        DMLManager.ShowError(error);
                        return error;
                    }

                    // Enable Tab2
                    tabSatirlar.PageEnabled = true;

                    // Update UI
                    txtSiparisNo.Text = _siparisModel.SiparisNo;

                    DMLManager.KaydetKontrol(null);
                    _dataChanged = false;

                    MessageBox.Show("Sipariş başarıyla oluşturuldu. Şimdi satır ekleyebilirsiniz.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return null; // Success
                }
                else
                {
                    // Edit mode - update header
                    _siparisModel.TedarikciId = Convert.ToInt32(lkpTedarikci.EditValue);
                    _siparisModel.SiparisTarih = dateSiparisTarih.DateTime;
                    _siparisModel.BeklenenTeslimTarih = dateBeklenenTeslim.EditValue as DateTime?;
                    _siparisModel.Aciklama = memoAciklama.Text;

                    var error = siparisService.GuncelleHeader(_siparisModel);

                    if (error != null)
                    {
                        DMLManager.ShowError(error);
                        return error;
                    }

                    DMLManager.GuncelleKontrol(null);
                    _dataChanged = false;

                    return null; // Success
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.SaveData hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Kayıt sırasında hata oluştu: " + ex.Message);
                return ex.Message;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnKaydetVeKapat_Click(object sender, EventArgs e)
        {
            SaveData();
            if (!_dataChanged)
            {
                var parentForm = this.FindForm();
                if (parentForm != null)
                    parentForm.Close();
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            if (_dataChanged)
            {
                var result = MessageBox.Show("Değişiklikler kaydedilmedi. Çıkmak istediğinizden emin misiniz?",
                    "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var parentForm = this.FindForm();
                    if (parentForm != null)
                        parentForm.Close();
                }
            }
            else
            {
                var parentForm = this.FindForm();
                if (parentForm != null)
                    parentForm.Close();
            }
        }

        private void btnSatirEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_siparisId.HasValue)
                {
                    MessageBox.Show("Lütfen önce sipariş başlığını kaydediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var satir = new SiparisSatirModel
                {
                    SiparisId = _siparisId.Value
                };

                if (ShowSatirDialog(satir))
                {
                    var siparisService = InterfaceFactory.Siparis;
                    var error = siparisService.SatirEkle(satir);

                    if (error != null)
                    {
                        DMLManager.ShowError(error);
                    }
                    else
                    {
                        DMLManager.KaydetKontrol(null);
                        RefreshSatirlar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.btnSatirEkle_Click hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Satır eklenirken hata oluştu: " + ex.Message);
            }
        }

        private void btnSatirDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridControlSatir.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                var satir = view.GetRow(view.FocusedRowHandle) as SiparisSatirModel;
                if (satir == null) return;

                if (ShowSatirDialog(satir))
                {
                    var siparisService = InterfaceFactory.Siparis;
                    var error = siparisService.SatirGuncelle(satir);

                    if (error != null)
                    {
                        DMLManager.ShowError(error);
                    }
                    else
                    {
                        DMLManager.GuncelleKontrol(null);
                        RefreshSatirlar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.btnSatirDuzenle_Click hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Satır güncellenirken hata oluştu: " + ex.Message);
            }
        }

        private void btnSatirSil_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridControlSatir.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                var satir = view.GetRow(view.FocusedRowHandle) as SiparisSatirModel;
                if (satir == null) return;

                if (DMLManager.SilmeOnayAl(string.Format("'{0}' ürününü sipariş satırlarından silmek istediğinizden emin misiniz?",
                    satir.UrunAdi)))
                {
                    var siparisService = InterfaceFactory.Siparis;
                    var error = siparisService.SatirSil(satir.SatirId);

                    if (error != null)
                    {
                        DMLManager.ShowError(error);
                    }
                    else
                    {
                        DMLManager.SilKontrol(null);
                        RefreshSatirlar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTaslak.btnSatirSil_Click hata: " + ex.Message, "SIP_TASLAK");
                DMLManager.ShowError("Satır silinirken hata oluştu: " + ex.Message);
            }
        }

        private bool ShowSatirDialog(SiparisSatirModel satir)
        {
            using (var dlg = new DlgSiparisSatir(satir))
            {
                return dlg.ShowDialog() == DialogResult.OK;
            }
        }

        private void RefreshSatirlar()
        {
            if (_siparisId.HasValue)
            {
                LoadSatirlar(_siparisId.Value);
            }
        }

        private void UpdateToplamTutar()
        {
            decimal toplam = _satirList.Sum(s => s.Tutar);
            lblToplamTutar.Text = toplam.ToString("C2");
        }

        private void gridViewSatir_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null || view.FocusedRowHandle < 0)
            {
                btnSatirDuzenle.Enabled = false;
                btnSatirSil.Enabled = false;
                return;
            }

            btnSatirDuzenle.Enabled = true;
            btnSatirSil.Enabled = true;
        }

        private void gridViewSatir_DoubleClick(object sender, EventArgs e)
        {
            btnSatirDuzenle_Click(sender, e);
        }

        public override bool HasChanges()
        {
            return _dataChanged;
        }

        public override void ClearData()
        {
            _siparisId = null;
            _siparisModel = null;
            _satirList.Clear();
            _dataChanged = false;
        }

        private void lkpTedarikci_EditValueChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }

        private void dateSiparisTarih_EditValueChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }

        private void dateBeklenenTeslim_EditValueChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }

        private void memoAciklama_TextChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }
    }
}
