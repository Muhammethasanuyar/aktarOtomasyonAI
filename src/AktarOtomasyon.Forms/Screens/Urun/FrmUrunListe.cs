using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    /// <summary>
    /// Ürün listesi ekranı.
    /// UC-Only Pattern: Form sadece shell, tüm iş mantığı UcUrunListe'de.
    /// </summary>
    public partial class FrmUrunListe : FrmBase
    {
        public FrmUrunListe(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmUrunListe_Load(object sender, EventArgs e)
        {
            ucUrunListe.LoadData();
        }

        protected override bool OnayliKapat()
        {
            // Liste ekranlarında kaydedilmemiş değişiklik olmaz
            return true;
        }
    }
}
