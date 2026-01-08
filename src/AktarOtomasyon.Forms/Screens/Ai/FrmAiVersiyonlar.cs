using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Ai
{
    public partial class FrmAiVersiyonlar : FrmBase
    {
        private int _urunId;

        public FrmAiVersiyonlar(string ekranKod, object urunId) : base(ekranKod)
        {
            if (urunId == null)
                throw new ArgumentException("UrunId parametresi zorunludur.", "urunId");

            _urunId = Convert.ToInt32(urunId);
            InitializeComponent();
        }

        private void FrmAiVersiyonlar_Load(object sender, EventArgs e)
        {
            ucAiVersiyonlar.LoadData(_urunId);
        }
    }
}
