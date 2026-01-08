using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AktarOtomasyon.Ai.Interface;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Ai
{
    public partial class UcAiSablonYonetim : UcBase
    {
        private List<AiSablonListModel> _sablonList;

        public UcAiSablonYonetim()
        {
            InitializeComponent();
            _sablonList = new List<AiSablonListModel>();
        }

        public override void LoadData()
        {
            try
            {
                LoadSablonlar();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcAiSablonYonetim.LoadData hata: " + ex.Message, "AI_SABLON_YNT");
                DMLManager.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadSablonlar()
        {
            try
            {
                gridSablonlar.BeginUpdate();

                // Load all templates (both active and inactive)
                _sablonList = InterfaceFactory.Ai.SablonListele(aktif: null);

                gridSablonlar.DataSource = _sablonList;

                // Configure grid columns
                ConfigureGrid();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("LoadSablonlar hata: " + ex.Message, "AI_SABLON_YNT");
                DMLManager.ShowError("Şablonlar yüklenirken hata: " + ex.Message);
            }
            finally
            {
                gridSablonlar.EndUpdate();
            }
        }

        private void ConfigureGrid()
        {
            var view = gridSablonlar.MainView as GridView;
            if (view == null) return;

            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsView.ShowAutoFilterRow = true;

            // Set column captions
            if (view.Columns["SablonKod"] != null)
            {
                view.Columns["SablonKod"].Caption = "Kod";
                view.Columns["SablonKod"].Width = 120;
            }
            if (view.Columns["SablonAdi"] != null)
            {
                view.Columns["SablonAdi"].Caption = "Şablon Adı";
                view.Columns["SablonAdi"].Width = 200;
            }
            if (view.Columns["Aciklama"] != null)
            {
                view.Columns["Aciklama"].Caption = "Açıklama";
                view.Columns["Aciklama"].Width = 300;
            }
            if (view.Columns["Aktif"] != null)
            {
                view.Columns["Aktif"].Caption = "Aktif";
                view.Columns["Aktif"].Width = 80;
            }

            // Hide SablonId column
            if (view.Columns["SablonId"] != null)
            {
                view.Columns["SablonId"].Visible = false;
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                var sablon = new AiSablonModel
                {
                    SablonId = 0,
                    Aktif = true
                };

                using (var dlg = new DlgAiSablonDetay(sablon))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        LoadSablonlar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnEkle_Click hata: " + ex.Message, "AI_SABLON_YNT");
                DMLManager.ShowError("Şablon ekleme sırasında hata: " + ex.Message);
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridSablonlar.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0)
                {
                    DMLManager.ShowWarning("Lütfen düzenlenecek şablonu seçiniz.");
                    return;
                }

                var selectedSablon = view.GetRow(view.FocusedRowHandle) as AiSablonListModel;
                if (selectedSablon == null)
                    return;

                // Get full template details
                var sablon = InterfaceFactory.Ai.SablonGetir(selectedSablon.SablonKod);
                if (sablon == null)
                {
                    DMLManager.ShowError("Şablon detayları yüklenemedi.");
                    return;
                }

                using (var dlg = new DlgAiSablonDetay(sablon))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        LoadSablonlar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnDuzenle_Click hata: " + ex.Message, "AI_SABLON_YNT");
                DMLManager.ShowError("Şablon düzenleme sırasında hata: " + ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridSablonlar.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0)
                {
                    DMLManager.ShowWarning("Lütfen silinecek şablonu seçiniz.");
                    return;
                }

                var selectedSablon = view.GetRow(view.FocusedRowHandle) as AiSablonListModel;
                if (selectedSablon == null)
                    return;

                if (DMLManager.SilmeOnayAl(
                    string.Format("'{0}' şablonunu silmek istediğinizden emin misiniz?", selectedSablon.SablonAdi)))
                {
                    var error = InterfaceFactory.Ai.SablonSil(selectedSablon.SablonId);

                    if (DMLManager.SilKontrol(error))
                    {
                        LoadSablonlar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnSil_Click hata: " + ex.Message, "AI_SABLON_YNT");
                DMLManager.ShowError("Şablon silme sırasında hata: " + ex.Message);
            }
        }

        private void btnAktifPasif_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridSablonlar.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0)
                {
                    DMLManager.ShowWarning("Lütfen şablon seçiniz.");
                    return;
                }

                var selectedSablon = view.GetRow(view.FocusedRowHandle) as AiSablonListModel;
                if (selectedSablon == null)
                    return;

                bool newAktifStatus = !selectedSablon.Aktif;

                var error = InterfaceFactory.Ai.SablonAktiflikGuncelle(selectedSablon.SablonId, newAktifStatus);

                if (DMLManager.IslemKontrol(error,
                    string.Format("Şablon {0} duruma getirildi.", newAktifStatus ? "aktif" : "pasif")))
                {
                    LoadSablonlar();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnAktifPasif_Click hata: " + ex.Message, "AI_SABLON_YNT");
                DMLManager.ShowError("Aktiflik değiştirme sırasında hata: " + ex.Message);
            }
        }

        private void gridViewSablonlar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null || view.FocusedRowHandle < 0)
            {
                btnDuzenle.Enabled = false;
                btnSil.Enabled = false;
                btnAktifPasif.Enabled = false;
                return;
            }

            btnDuzenle.Enabled = true;
            btnSil.Enabled = true;
            btnAktifPasif.Enabled = true;
        }

        private void gridViewSablonlar_DoubleClick(object sender, EventArgs e)
        {
            btnDuzenle_Click(sender, e);
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadSablonlar();
        }
    }
}
