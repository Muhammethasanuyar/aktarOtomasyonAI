using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Bildirim
{
    public partial class FrmBildirimMrk : FrmBase
    {
        public FrmBildirimMrk(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmBildirimMrk_Load(object sender, EventArgs e)
        {
            ucBildirimMrk.LoadData();
        }

        protected override bool OnayliKapat()
        {
            return true; // Liste ekranı için değişiklik yok
        }
    }
}
