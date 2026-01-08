using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    public partial class FrmUrunKatalog : FrmBase
    {
        public FrmUrunKatalog()
        {
            InitializeComponent();
        }

        public FrmUrunKatalog(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmUrunKatalog_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            ucUrunKatalog.LoadData();
        }
    }
}
