using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Satis
{
    public partial class FrmSatis : FrmBase
    {
        public FrmSatis(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }

        public FrmSatis(string ekranKod, object parameter) : base(ekranKod)
        {
            InitializeComponent();
            _parameter = parameter;
        }

        private object _parameter;

        private void FrmSatis_Load(object sender, EventArgs e)
        {
            ucSatis.LoadData();
            
            // If parameter passed (Barcode), add it
            if (_parameter != null && _parameter is string)
            {
                ucSatis.AddProductByBarcode((string)_parameter);
            }
        }
    }
}
