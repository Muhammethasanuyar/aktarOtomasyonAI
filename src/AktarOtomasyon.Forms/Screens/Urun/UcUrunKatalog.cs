using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Urun.Interface.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    public partial class UcUrunKatalog : UcBase
    {
        private GridControl _gridControl;
        private LayoutView _layoutView;
        private PanelControl _pnlTop;
        private TextEdit _txtArama;
        private SimpleButton _btnAra;
        private SimpleButton _btnYenile;

        public UcUrunKatalog()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Top Panel
            _pnlTop = new PanelControl();
            _pnlTop.Dock = DockStyle.Top;
            _pnlTop.Height = 50;
            _pnlTop.Padding = new Padding(10);
            this.Controls.Add(_pnlTop);

            // Search Bar
            var lblAra = new LabelControl();
            lblAra.Text = "Ara:";
            lblAra.Location = new Point(15, 18);
            _pnlTop.Controls.Add(lblAra);

            _txtArama = new TextEdit();
            _txtArama.Location = new Point(50, 15);
            _txtArama.Size = new Size(200, 25);
            _txtArama.Properties.NullValuePrompt = "Ürün adı veya kodu...";
            _txtArama.KeyDown += (s, e) => { if(e.KeyCode == Keys.Enter) LoadData(); };
            _pnlTop.Controls.Add(_txtArama);

            _btnAra = new SimpleButton();
            _btnAra.Text = "Ara";
            _btnAra.Location = new Point(260, 13);
            _btnAra.Click += (s, e) => LoadData();
            _pnlTop.Controls.Add(_btnAra);

            _btnYenile = new SimpleButton();
            _btnYenile.Text = "Yenile";
            _btnYenile.Location = new Point(350, 13);
            _btnYenile.Click += (s, e) => LoadData();
            _pnlTop.Controls.Add(_btnYenile);

            // Grid Layout View Setup
            _gridControl = new GridControl();
            _gridControl.Dock = DockStyle.Fill;
            _layoutView = new LayoutView(_gridControl);
            _gridControl.MainView = _layoutView;
            
            // Configure LayoutView - Modern Styling with Animations
            _layoutView.OptionsView.ViewMode = LayoutViewMode.MultiRow;
            _layoutView.OptionsView.ShowCardCaption = false;
            _layoutView.OptionsView.ShowHeaderPanel = false;
            _layoutView.CardMinSize = new Size(220, 320);
            _layoutView.OptionsView.ShowCardBorderIfCaptionHidden = true;
            _layoutView.Appearance.FocusedCardCaption.BackColor = Color.FromArgb(33, 150, 243); // Primary blue
            _layoutView.Appearance.FocusedCardCaption.ForeColor = Color.White;
            _layoutView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            // Card appearance
            _layoutView.Appearance.Card.BackColor = Color.White;
            _layoutView.Appearance.Card.Options.UseBackColor = true;

            // Add Columns
            var colId = _layoutView.Columns.AddVisible("UrunId");
            colId.Visible = false;

            // Image Column
            var colImage = _layoutView.Columns.AddVisible("UnboundImage");
            colImage.Caption = "Görsel";
            // Important: We need to use Unbound column logic to load Image from Path, 
            // OR use CustomUnboundColumnData event.
            // Simplified: We will use CustomUnboundColumnData to load image
            colImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colImage.OptionsColumn.ShowCaption = false;
            _layoutView.CustomUnboundColumnData += LayoutView_CustomUnboundColumnData;
            _layoutView.CardClick += LayoutView_CardClick;
            
            var colName = _layoutView.Columns.AddVisible("UrunAdi");
            colName.Caption = "Ürün Adı";
            colName.AppearanceCell.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            colName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            var colPrice = _layoutView.Columns.AddVisible("SatisFiyati");
            colPrice.Caption = "Fiyat";
            colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPrice.DisplayFormat.FormatString = "c2";
            colPrice.AppearanceCell.ForeColor = Color.DarkGreen;
            colPrice.AppearanceCell.Font = new Font("Segoe UI", 9, FontStyle.Bold);
             colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            var colKategori = _layoutView.Columns.AddVisible("KategoriAdi");
            colKategori.Caption = "Kategori";
             colKategori.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            // Card Layout Customization
            // Card Layout Customization
            // Note: AddVisible automatically adds fields to the card.
            // We just need to configure them.
            if (colImage.LayoutViewField != null)
            {
                colImage.LayoutViewField.TextVisible = false;
                // Set width to match card width to ensure it spans the whole card
                colImage.LayoutViewField.Size = new Size(220, 180);
                colImage.LayoutViewField.MaxSize = new Size(0, 180); 
                colImage.LayoutViewField.MinSize = new Size(100, 180); // Allow it to be smaller but stretch 
                colImage.LayoutViewField.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                colImage.LayoutViewField.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                colImage.LayoutViewField.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            }
            if (colName.LayoutViewField != null)
            {
                colName.LayoutViewField.TextVisible = false;
                // colName.LayoutViewField.Location = new Point(10, 200);
            }
            if (colPrice.LayoutViewField != null) 
            {
                colPrice.LayoutViewField.TextVisible = false;
               // colPrice.LayoutViewField.Location = new Point(10, 240);
            }
            if (colKategori.LayoutViewField != null)
            {
                colKategori.LayoutViewField.TextVisible = false;
               // colKategori.LayoutViewField.Location = new Point(10, 275);
            }
            
            // Define a Template Card Layout Programmatically to ensure stacked centered layout
            // This is safer than relying on default AddVisible behavior
            _layoutView.TemplateCard.GroupBordersVisible = false;
            
            // Arrange fields in a vertical stack
            if (colImage.LayoutViewField != null && colName.LayoutViewField != null && colPrice.LayoutViewField != null && colKategori.LayoutViewField != null)
            {
                 // colImage.LayoutViewField.Move(null, DevExpress.XtraLayout.Utils.InsertType.Top); // Removed to prevent NRE
                 colName.LayoutViewField.Move(colImage.LayoutViewField, DevExpress.XtraLayout.Utils.InsertType.Bottom);
                 colPrice.LayoutViewField.Move(colName.LayoutViewField, DevExpress.XtraLayout.Utils.InsertType.Bottom);
                 colKategori.LayoutViewField.Move(colPrice.LayoutViewField, DevExpress.XtraLayout.Utils.InsertType.Bottom);
                 
                 // Alignment - Critical for centering
                 colImage.LayoutViewField.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                 colImage.LayoutViewField.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
                 colImage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                 colImage.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                 
                 colName.LayoutViewField.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                 colPrice.LayoutViewField.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                 colKategori.LayoutViewField.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            // Repository Item for Image
            var repoImage = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            repoImage.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            repoImage.ReadOnly = true;
            repoImage.ShowMenu = false;
            repoImage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            repoImage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            repoImage.Appearance.Options.UseTextOptions = true;
            repoImage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            repoImage.PictureAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            _gridControl.RepositoryItems.Add(repoImage);
            colImage.ColumnEdit = repoImage;


            this.Controls.Add(_gridControl);
            _gridControl.BringToFront(); // Ensure it's not covered by panel? No, panel is docked Top.
        }

        private void LayoutView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "UnboundImage" && e.IsGetData)
            {
                var row = e.Row as UrunListeItemDto;
                if (row != null && !string.IsNullOrEmpty(row.AnaGorselPath))
                {
                    if (File.Exists(row.AnaGorselPath))
                    {
                        try
                        {
                            // FileStream kullanarak yükle (file lock sorununu önler)
                            using (var stream = new FileStream(row.AnaGorselPath, FileMode.Open, FileAccess.Read))
                            {
                                e.Value = Image.FromStream(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.LogMessage(string.Format("Görsel yükleme hatası - Ürün: {0}, Path: {1}, Hata: {2}",
                                row.UrunAdi, row.AnaGorselPath, ex.Message), "KATALOG");
                            e.Value = CreatePlaceholderImage();
                        }
                    }
                    else
                    {
                        ErrorManager.LogMessage(string.Format("Görsel dosyası bulunamadı - Ürün: {0}, Path: {1}",
                            row.UrunAdi, row.AnaGorselPath), "KATALOG");
                        e.Value = CreatePlaceholderImage();
                    }
                }
                else
                {
                    // Görsel path yok, placeholder göster
                    e.Value = CreatePlaceholderImage();
                }
            }
        }

        /// <summary>
        /// Görsel yoksa gösterilecek placeholder oluşturur
        /// </summary>
        private Image CreatePlaceholderImage()
        {
            try
            {
                var bmp = new Bitmap(180, 150);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightGray);
                    using (var font = new Font("Segoe UI", 10, FontStyle.Regular))
                    using (var brush = new SolidBrush(Color.Gray))
                    {
                        var text = "Görsel Yok";
                        var size = g.MeasureString(text, font);
                        var x = (bmp.Width - size.Width) / 2;
                        var y = (bmp.Height - size.Height) / 2;
                        g.DrawString(text, font, brush, x, y);
                    }
                }
                return bmp;
            }
            catch
            {
                return null;
            }
        }

        private void LayoutView_CardClick(object sender, DevExpress.XtraGrid.Views.Layout.Events.CardClickEventArgs e)
        {
            try
            {
                var view = sender as DevExpress.XtraGrid.Views.Layout.LayoutView;
                if (view != null) 
                {
                   var row = view.GetRow(e.RowHandle) as UrunListeItemDto;
                   if (row != null)
                   {
                        // Open UrunDetay (read-only detail view)
                        if (ParentFrm != null)
                        {
                            NavigationManager.OpenScreen("URUN_DETAY", ParentFrm.MdiParent, row.UrunId);
                        }
                   }
                }
            }
            catch (Exception ex)
            {
                 ErrorManager.LogMessage("Katalog navigasyon hatası: " + ex.Message, "URUN");
            }
        }

        public override void LoadData()
        {
            try
            {
                var filtre = new UrunFiltreDto();
                if(!string.IsNullOrWhiteSpace(_txtArama.Text))
                    filtre.Arama = _txtArama.Text;
                
                // Show only active products usually, but maybe catalog shows all? Default active.
                filtre.Aktif = true;

                var urunler = InterfaceFactory.Urun.Listele(filtre);

                // Fix encoding as in UcUrunListe
                if (urunler != null)
                {
                    foreach(var item in urunler)
                    {
                         item.UrunAdi = TextHelper.FixEncoding(item.UrunAdi);
                         item.KategoriAdi = TextHelper.FixEncoding(item.KategoriAdi);
                    }
                }

                _gridControl.DataSource = urunler;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Veriler yüklenirken hata: " + ex.Message);
            }
        }
    }
}
