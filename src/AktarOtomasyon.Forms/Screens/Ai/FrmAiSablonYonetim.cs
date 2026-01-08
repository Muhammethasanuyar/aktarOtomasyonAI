using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Ai
{
    public partial class FrmAiSablonYonetim : FrmBase
    {
        public FrmAiSablonYonetim(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmAiSablonYonetim_Load(object sender, EventArgs e)
        {
            ucAiSablonYonetim.LoadData();
        }
    }
}
