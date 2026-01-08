using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Ai
{
    public partial class FrmAiModul : FrmBase
    {
        private int? _urunId;

        public FrmAiModul(string ekranKod) : base(ekranKod)
        {
            _urunId = null;
            InitializeComponent();
        }

        public FrmAiModul(string ekranKod, object urunId) : base(ekranKod)
        {
            _urunId = urunId as int?;
            InitializeComponent();
        }

        private void FrmAiModul_Load(object sender, EventArgs e)
        {
            ucAiModul.LoadData(_urunId);
        }

        protected override bool OnayliKapat()
        {
            if (!ucAiModul.HasChanges())
                return true;

            var result = MessageBox.Show(
                "Kaydedilmemiş değişiklikler var. Çıkmak istediğinizden emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }
    }
}
