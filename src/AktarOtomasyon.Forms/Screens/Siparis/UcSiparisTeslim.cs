using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Siparis.Interface;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    public partial class UcSiparisTeslim : UcBase
    {
        private int _siparisId;
        private SiparisModel _siparisModel;

        public UcSiparisTeslim()
        {
            InitializeComponent();
        }

        public override void LoadData()
        {
            // Override not used - use LoadData(int) instead
        }

        public void LoadData(int siparisId)
        {
            try
            {
                _siparisId = siparisId;
                LoadSiparisBilgi();
                LoadSatirlar();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTeslim.LoadData hata: " + ex.Message, "SIP_TESLIM");
                DMLManager.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadSiparisBilgi()
        {
            try
            {
                var siparisService = InterfaceFactory.Siparis;
                _siparisModel = siparisService.Getir(_siparisId);

                if (_siparisModel == null)
                {
                    MessageBox.Show("Sipariş bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Durum kontrolü
                if (_siparisModel.Durum != "GONDERILDI" && _siparisModel.Durum != "KISMI")
                {
                    MessageBox.Show("Sadece GONDERILDI veya KISMI durumundaki siparişler teslim alınabilir.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTeslimAl.Enabled = false;
                }

                // Display order info
                txtSiparisNo.Text = _siparisModel.SiparisNo;
                txtTedarikci.Text = _siparisModel.TedarikciAdi;
                dateSiparisTarih.Text = _siparisModel.SiparisTarih.ToString("dd.MM.yyyy");
                txtDurum.Text = _siparisModel.Durum;

                // Teslim tarihi varsayılan olarak bugün
                dateTeslimTarih.DateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTeslim.LoadSiparisBilgi hata: " + ex.Message, "SIP_TESLIM");
                DMLManager.ShowError("Sipariş bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadSatirlar()
        {
            try
            {
                gridControl.BeginUpdate();

                var siparisService = InterfaceFactory.Siparis;
                var satirlar = siparisService.SatirListele(_siparisId);

                gridControl.DataSource = satirlar;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTeslim.LoadSatirlar hata: " + ex.Message, "SIP_TESLIM");
                DMLManager.ShowError("Satırlar yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                gridControl.EndUpdate();
            }
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    string.Format("'{0}' nolu siparişi teslim almak istediğinizden emin misiniz?\n\n" +
                                  "Bu işlem stok hareketleri oluşturacak ve sipariş durumu güncellenecektir.",
                                  _siparisModel.SiparisNo),
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                var siparisService = InterfaceFactory.Siparis;
                var error = siparisService.TeslimAl(_siparisId);

                if (error != null)
                {
                    DMLManager.ShowError(error);
                }
                else
                {
                    MessageBox.Show("Sipariş başarıyla teslim alındı. Stok hareketleri oluşturuldu.",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Close this form
                    var parentForm = this.FindForm();
                    if (parentForm != null)
                        parentForm.Close();

                    // Refresh SIP_LISTE if open
                    if (parentForm != null)
                    {
                        var mdiParent = parentForm.MdiParent;
                        if (mdiParent != null)
                        {
                            foreach (Form childForm in mdiParent.MdiChildren)
                            {
                                var frmListe = childForm as FrmSiparisListe;
                                if (frmListe != null)
                                {
                                    // Trigger refresh
                                    childForm.Activate();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcSiparisTeslim.btnTeslimAl_Click hata: " + ex.Message, "SIP_TESLIM");
                DMLManager.ShowError("Teslim alma sırasında hata oluştu: " + ex.Message);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            var parentForm = this.FindForm();
            if (parentForm != null)
                parentForm.Close();
        }
    }
}
