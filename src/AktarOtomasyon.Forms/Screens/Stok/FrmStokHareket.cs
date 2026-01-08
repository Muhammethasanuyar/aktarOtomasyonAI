using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Stok
{
    public partial class FrmStokHareket : FrmBase
    {
        public FrmStokHareket(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmStokHareket_Load(object sender, EventArgs e)
        {
            ucStokHareket.LoadData();
        }

        protected override bool OnayliKapat()
        {
            return true; // Liste ekranlar için değişiklik yok
        }
    }
}
