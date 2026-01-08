using System;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using System.Collections.Generic;

namespace AktarOtomasyon.Forms.Screens.Raporlar
{
    public partial class UcRaporlar : UcBase
    {
        public UcRaporlar()
        {
            InitializeComponent();
        }

        public override void LoadData()
        {
            // Default dates: This Month
            dtBaslangic.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtBitis.DateTime = DateTime.Now;

            LoadReport();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                var baslangic = dtBaslangic.DateTime.Date;
                var bitis = dtBitis.DateTime.Date.AddDays(1).AddSeconds(-1); // End of day

                var raporList = InterfaceFactory.Stok.SatisRaporuGetir(baslangic, bitis);
                
                // Encoding fix if necessary
                if(raporList != null)
                {
                    foreach(var item in raporList)
                    {
                        item.UrunAdi = TextHelper.FixEncoding(item.UrunAdi);
                    }
                }

                gcRapor.DataSource = raporList;
                
                // Grid Formatting
                GridHelper.ApplyStandardFormatting(gvRapor);
                GridHelper.FormatQuantityColumn(colToplamMiktar);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("Rapor listeleme hatası: " + ex.Message, "RAPOR");
                MessageHelper.ShowError("Rapor alınırken hata oluştu.");
            }
        }
    }
}
