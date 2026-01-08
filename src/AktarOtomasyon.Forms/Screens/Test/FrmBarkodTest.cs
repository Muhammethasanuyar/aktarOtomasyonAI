using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Test
{
    public partial class FrmBarkodTest : FrmBase
    {
        public FrmBarkodTest(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmBarkodTest_Load(object sender, EventArgs e)
        {
            ucBarkodTest.LoadData();
        }
    }
}
