using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Template
{
    /// <summary>
    /// Template Management Center - MDI Shell Form
    /// UC-Only Pattern: Form sadece shell, tüm iş mantığı UcTemplateMrk'de.
    /// </summary>
    public partial class FrmTemplateMrk : FrmBase
    {
        public FrmTemplateMrk(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmTemplateMrk_Load(object sender, EventArgs e)
        {
            ucTemplateMrk.LoadData();
        }

        protected override bool OnayliKapat()
        {
            if (!ucTemplateMrk.HasChanges())
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
