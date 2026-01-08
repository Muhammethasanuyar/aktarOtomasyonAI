using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    public partial class FrmSiparisTeslim : FrmBase
    {
        private int _siparisId;

        public FrmSiparisTeslim(string ekranKod, object siparisId) : base(ekranKod)
        {
            _siparisId = Convert.ToInt32(siparisId);
            InitializeComponent();
        }

        private void FrmSiparisTeslim_Load(object sender, EventArgs e)
        {
            ucSiparisTeslim.LoadData(_siparisId);
        }

        protected override bool OnayliKapat()
        {
            return true;
        }
    }
}
