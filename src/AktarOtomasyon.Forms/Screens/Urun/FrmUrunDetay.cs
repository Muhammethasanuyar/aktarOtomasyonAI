using System;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    /// <summary>
    /// Ürün Detay ekranı - AI bilgileri ile birlikte salt okunur görünüm.
    /// UC-Only Pattern: Form sadece shell, tüm iş mantığı UcUrunDetay'da.
    /// Parameter passing: UrunId ile açılır (zorunlu).
    /// </summary>
    public partial class FrmUrunDetay : FrmBase
    {
        private int _urunId;

        /// <summary>
        /// UrunId property - NavigationManager tarafından duplicate kontrolü için kullanılır.
        /// </summary>
        public int UrunId
        {
            get { return _urunId; }
        }

        /// <summary>
        /// Constructor - UrunId zorunlu (detay görünümü için)
        /// </summary>
        public FrmUrunDetay(string ekranKod, int urunId) : base(ekranKod)
        {
            _urunId = urunId;
            InitializeComponent();
        }

        private void FrmUrunDetay_Load(object sender, EventArgs e)
        {
            // UserControl'e veri yükleme delegesi
            ucUrunDetay.LoadData(_urunId);
        }

        protected override bool OnayliKapat()
        {
            // Salt okunur ekran olduğu için değişiklik kontrolü yok
            return true;
        }
    }
}
