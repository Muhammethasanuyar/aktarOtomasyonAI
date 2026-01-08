using System.Windows.Forms;

namespace AktarOtomasyon.Forms.Base
{
    /// <summary>
    /// Tüm UserControl'ler için temel sınıf.
    /// STANDART: Ekran içeriği Form'da değil UC'de olur.
    /// </summary>
    public class UcBase : UserControl
    {
        /// <summary>
        /// Parent form referansı.
        /// </summary>
        protected Form ParentFrm { get { return this.ParentForm; } }

        /// <summary>
        /// Verileri yükler. Alt sınıflarda override edilir.
        /// </summary>
        public virtual void LoadData()
        {
        }

        /// <summary>
        /// Verileri kaydeder. Alt sınıflarda override edilir.
        /// </summary>
        public virtual string SaveData()
        {
            return null; // Başarı
        }

        /// <summary>
        /// Temizlik yapar. Alt sınıflarda override edilir.
        /// </summary>
        public virtual void ClearData()
        {
        }

        /// <summary>
        /// Değişiklik var mı kontrolü. Alt sınıflarda override edilir.
        /// </summary>
        public virtual bool HasChanges()
        {
            return false;
        }
    }
}
