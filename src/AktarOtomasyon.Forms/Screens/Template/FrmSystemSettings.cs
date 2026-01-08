using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Template
{
    /// <summary>
    /// System Settings - MDI Shell Form
    /// UC-Only Pattern: Form sadece shell, tüm iş mantığı UcSystemSettings'de.
    /// </summary>
    public partial class FrmSystemSettings : FrmBase
    {
        public FrmSystemSettings(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmSystemSettings_Load(object sender, EventArgs e)
        {
            ucSystemSettings.LoadData();
        }

        protected override bool OnayliKapat()
        {
            if (!ucSystemSettings.HasChanges())
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
