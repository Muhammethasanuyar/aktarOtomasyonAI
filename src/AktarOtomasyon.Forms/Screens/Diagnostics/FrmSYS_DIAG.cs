using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Diagnostics
{
    public partial class FrmSYS_DIAG : FrmBase
    {
        public FrmSYS_DIAG(string ekranKod) : base()
        {
            InitializeComponent();
            this.EkranKod = ekranKod;
        }
    }
}
