using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    /// <summary>
    /// Ürün kartı ekranı.
    /// UC-Only Pattern: Form sadece shell, tüm iş mantığı UcUrunKart'ta.
    /// Parameter passing: UrunId ile açılır (null = yeni, değer = düzenleme).
    /// </summary>
    public partial class FrmUrunKart : FrmBase
    {
        private int? _urunId;

        /// <summary>
        /// UrunId property - NavigationManager tarafından duplicate kontrolü için kullanılır.
        /// </summary>
        public int? UrunId
        {
            get { return _urunId; }
        }

        /// <summary>
        /// Constructor - Yeni ürün için (menüden açılır)
        /// </summary>
        public FrmUrunKart(string ekranKod) : base(ekranKod)
        {
            _urunId = null;
            InitializeComponent();
        }

        /// <summary>
        /// Constructor - Var olan ürün için (programatik olarak açılır)
        /// </summary>
        public FrmUrunKart(string ekranKod, int? urunId) : base(ekranKod)
        {
            _urunId = urunId;
            InitializeComponent();
        }

        private void FrmUrunKart_Load(object sender, EventArgs e)
        {
            ucUrunKart.LoadData(_urunId);
        }

        protected override bool OnayliKapat()
        {
            if (ucUrunKart.HasChanges())
            {
                var result = MessageBox.Show(
                    "Kaydedilmemiş değişiklikler var. Çıkmak istediğinizden emin misiniz?",
                    "Uyarı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                return result == DialogResult.Yes;
            }
            return true;
        }

        private void FrmUrunKart_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Parent URUN_LISTE'yi yenile
            var parentMdi = this.MdiParent;
            if (parentMdi != null)
            {
                foreach (Form childForm in parentMdi.MdiChildren)
                {
                    var frmListe = childForm as FrmUrunListe;
                    if (frmListe != null)
                    {
                        // UserControl'ü bul ve RefreshList çağır
                        foreach (var control in frmListe.Controls)
                        {
                            var ucListe = control as UcUrunListe;
                            if (ucListe != null)
                            {
                                ucListe.RefreshList();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
