using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Raporlar
{
    public partial class FrmRaporlar : FrmBase
    {
        public FrmRaporlar(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            ucRaporlar.LoadData();
        }
    }
}
