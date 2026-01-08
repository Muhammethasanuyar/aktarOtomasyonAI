using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    public partial class FrmSiparisTaslak : FrmBase
    {
        private int? _siparisId;

        public FrmSiparisTaslak(string ekranKod) : base(ekranKod)
        {
            _siparisId = null;
            InitializeComponent();
        }

        public FrmSiparisTaslak(string ekranKod, object siparisId) : base(ekranKod)
        {
            _siparisId = siparisId as int?;
            InitializeComponent();
        }

        public int? SiparisId
        {
            get { return _siparisId; }
        }

        private void FrmSiparisTaslak_Load(object sender, EventArgs e)
        {
            ucSiparisTaslak.LoadData(_siparisId);
        }

        protected override bool OnayliKapat()
        {
            if (ucSiparisTaslak.HasChanges())
            {
                return MessageBox.Show("Değişiklikler kaydedilmedi. Çıkmak istediğinizden emin misiniz?",
                    "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
            return true;
        }
    }
}
