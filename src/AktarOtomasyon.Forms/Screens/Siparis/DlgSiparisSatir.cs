using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Siparis.Interface;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    public partial class DlgSiparisSatir : Form
    {
        private SiparisSatirModel _satir;

        public DlgSiparisSatir(SiparisSatirModel satir)
        {
            InitializeComponent();
            _satir = satir;
        }

        private void DlgSiparisSatir_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUrunLookup();

                // Eğer düzenleme modundaysa verileri yükle
                if (_satir.SatirId > 0)
                {
                    lkpUrun.EditValue = _satir.UrunId;
                    spnMiktar.Value = _satir.Miktar;
                    spnBirimFiyat.Value = _satir.BirimFiyat;
                }
                else
                {
                    spnMiktar.Value = 1;
                    spnBirimFiyat.Value = 0;
                }

                CalculateTutar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUrunLookup()
        {
            try
            {
                var urunler = UrunLookupProvider.GetUrunListe();

                lkpUrun.Properties.DataSource = urunler;
                lkpUrun.Properties.DisplayMember = "UrunAdi";
                lkpUrun.Properties.ValueMember = "UrunId";
                lkpUrun.Properties.NullText = "Seçiniz...";

                lkpUrun.Properties.Columns.Clear();
                lkpUrun.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UrunAdi", "Ürün"));
                lkpUrun.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KategoriAdi", "Kategori", 100));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün listesi yüklenemedi: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTutar()
        {
            decimal tutar = spnMiktar.Value * spnBirimFiyat.Value;
            lblTutar.Text = tutar.ToString("C2");
        }

        private bool ValidateInput()
        {
            if (lkpUrun.EditValue == null)
            {
                MessageBox.Show("Lütfen ürün seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lkpUrun.Focus();
                return false;
            }

            if (spnMiktar.Value <= 0)
            {
                MessageBox.Show("Miktar sıfırdan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spnMiktar.Focus();
                return false;
            }

            if (spnBirimFiyat.Value < 0)
            {
                MessageBox.Show("Birim fiyat negatif olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spnBirimFiyat.Focus();
                return false;
            }

            return true;
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                _satir.UrunId = Convert.ToInt32(lkpUrun.EditValue);
                _satir.Miktar = spnMiktar.Value;
                _satir.BirimFiyat = spnBirimFiyat.Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void spnMiktar_ValueChanged(object sender, EventArgs e)
        {
            CalculateTutar();
        }

        private void spnBirimFiyat_ValueChanged(object sender, EventArgs e)
        {
            CalculateTutar();
        }
    }
}
