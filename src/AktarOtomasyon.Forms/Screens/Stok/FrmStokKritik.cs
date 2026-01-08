using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Stok
{
    public partial class FrmStokKritik : FrmBase
    {
        public FrmStokKritik(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmStokKritik_Load(object sender, EventArgs e)
        {
            ucStokKritik.LoadData();
        }

        protected override bool OnayliKapat()
        {
            return true; // Liste ekranı için değişiklik yok
        }
    }
}
