using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    public partial class FrmSiparisListe : FrmBase
    {
        public FrmSiparisListe(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmSiparisListe_Load(object sender, EventArgs e)
        {
            ucSiparisListe.LoadData();
        }

        protected override bool OnayliKapat()
        {
            return true; // Liste ekranlar için değişiklik yok
        }
    }
}
