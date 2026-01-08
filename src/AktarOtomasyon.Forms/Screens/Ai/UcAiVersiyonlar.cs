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
    public partial class UcAiVersiyonlar : UcBase
    {
        private int _urunId;
        private List<AiIcerikVersiyonModel> _versiyonList;

        public UcAiVersiyonlar()
        {
            InitializeComponent();
            _versiyonList = new List<AiIcerikVersiyonModel>();
        }

        public void LoadData(int urunId)
        {
            try
            {
                _urunId = urunId;

                // Load product info
                var urun = InterfaceFactory.Urun.Getir(urunId);
                if (urun != null)
                {
                    lblUrunBilgi.Text = string.Format("Ürün: {0} - {1}", urun.UrunKod, urun.UrunAdi);
                }

                // Load versions
                LoadVersions();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("UcAiVersiyonlar.LoadData hata: " + ex.Message, "AI_VERSIYON");
                DMLManager.ShowError("Veri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadVersions()
        {
            try
            {
                gridVersions.BeginUpdate();

                // Load all versions for this product
                _versiyonList = InterfaceFactory.Ai.VersiyonListele(_urunId);

                gridVersions.DataSource = _versiyonList;

                // Configure grid
                ConfigureGrid();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("LoadVersions hata: " + ex.Message, "AI_VERSIYON");
                DMLManager.ShowError("Versiyonlar yüklenirken hata: " + ex.Message);
            }
            finally
            {
                gridVersions.EndUpdate();
            }
        }

        private void ConfigureGrid()
        {
            var view = gridVersions.MainView as GridView;
            if (view == null) return;

            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsView.ShowAutoFilterRow = true;

            // Configure columns
            if (view.Columns["VersiyonNo"] != null)
            {
                view.Columns["VersiyonNo"].Caption = "Versiyon No";
                view.Columns["VersiyonNo"].Width = 100;
            }
            if (view.Columns["OlusturmaTarih"] != null)
            {
                view.Columns["OlusturmaTarih"].Caption = "Oluşturma Tarihi";
                view.Columns["OlusturmaTarih"].Width = 150;
                view.Columns["OlusturmaTarih"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["OlusturmaTarih"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            }
            if (view.Columns["Icerik"] != null)
            {
                view.Columns["Icerik"].Caption = "Önizleme";
                view.Columns["Icerik"].Width = 400;
            }

            // Hide unnecessary columns
            if (view.Columns["VersiyonId"] != null)
            {
                view.Columns["VersiyonId"].Visible = false;
            }
            if (view.Columns["IcerikId"] != null)
            {
                view.Columns["IcerikId"].Visible = false;
            }
        }

        private void gridViewVersions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null || view.FocusedRowHandle < 0)
                {
                    memoVersiyonDetay.Text = string.Empty;
                    btnGeriYukle.Enabled = false;
                    return;
                }

                var selectedVersion = view.GetRow(view.FocusedRowHandle) as AiIcerikVersiyonModel;
                if (selectedVersion != null)
                {
                    memoVersiyonDetay.Text = selectedVersion.Icerik;
                    btnGeriYukle.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("gridViewVersions_FocusedRowChanged hata: " + ex.Message, "AI_VERSIYON");
            }
        }

        private void gridViewVersions_DoubleClick(object sender, EventArgs e)
        {
            btnGeriYukle_Click(sender, e);
        }

        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridVersions.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0)
                {
                    DMLManager.ShowWarning("Lütfen geri yüklenecek versiyonu seçiniz.");
                    return;
                }

                var selectedVersion = view.GetRow(view.FocusedRowHandle) as AiIcerikVersiyonModel;
                if (selectedVersion == null)
                    return;

                var result = MessageBox.Show(
                    string.Format("Versiyon {0} geri yüklenecek ve yeni taslak oluşturulacak. Onaylıyor musunuz?",
                        selectedVersion.VersiyonNo),
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                // Create new content as TASLAK from selected version
                var icerik = new AiIcerikModel
                {
                    UrunId = _urunId,
                    Icerik = selectedVersion.Icerik,
                    Durum = "TASLAK"
                };

                var error = InterfaceFactory.Ai.IcerikKaydet(icerik);

                if (DMLManager.KaydetKontrol(error))
                {
                    DMLManager.ShowInfo("Versiyon başarıyla geri yüklendi. Yeni taslak oluşturuldu.");
                    LoadVersions();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("btnGeriYukle_Click hata: " + ex.Message, "AI_VERSIYON");
                DMLManager.ShowError("Versiyon geri yüklenirken hata: " + ex.Message);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadVersions();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            var parentForm = this.ParentForm;
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }
    }
}
