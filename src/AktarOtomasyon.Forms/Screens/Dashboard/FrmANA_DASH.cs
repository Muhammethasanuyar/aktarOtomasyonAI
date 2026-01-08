using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Dashboard
{
    /// <summary>
    /// Ana Dashboard ekranı.
    /// STANDART: UC-Only pattern - tüm logic UcANA_DASH içinde.
    /// </summary>
    public partial class FrmANA_DASH : FrmBase
    {
        public FrmANA_DASH(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        private void FrmANA_DASH_Load(object sender, EventArgs e)
        {
            ucDashboard.LoadData();
        }

        protected override bool OnayliKapat()
        {
            return true; // Dashboard has no unsaved changes
        }
    }
}
