using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Urun.Interface.Models;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Urun
{
    public partial class UcUrunGorselYonetim : UcBase
    {
        private int _urunId;
        private GalleryControl _galleryControl;
        private GalleryItemGroup _galleryGroup;
        private ContextMenuStrip _contextMenu;
        private PanelControl _pnlTop;
        private SimpleButton _btnResimEkle;
        private SimpleButton _btnYenile;

        // Dual-mode support: Yeni ürün vs düzenleme modu
        private bool _isNewProductMode = false;
        private string _tempFolderPath = null;
        private List<string> _tempImagePaths = null;

        public UcUrunGorselYonetim()
        {
            InitializeComponent();
            InitializeUI();
            InitializeContextMenu();
        }

        public void SetUrunId(int urunId)
        {
            _urunId = urunId;
            LoadGorseller();
        }

        /// <summary>
        /// Yeni ürün modu için kontrol yapılandırması
        /// </summary>
        /// <param name="isNewMode">Yeni ürün modu aktif mi</param>
        /// <param name="tempFolderPath">Geçici klasör yolu</param>
        /// <param name="tempImagePaths">Geçici görsel yolları listesi</param>
        public void SetNewProductMode(bool isNewMode, string tempFolderPath, List<string> tempImagePaths)
        {
            _isNewProductMode = isNewMode;
            _tempFolderPath = tempFolderPath;
            _tempImagePaths = tempImagePaths;

            LoadGorseller();
        }

        /// <summary>
        /// Genel sekmesinden görsel eklendiğinde gallery'yi yenile
        /// </summary>
        public void RefreshTempImages()
        {
            if (_isNewProductMode)
            {
                LoadGorseller();
            }
        }

        private void InitializeUI()
        {
            // Panel Top
            _pnlTop = new PanelControl();
            _pnlTop.Dock = DockStyle.Top;
            _pnlTop.Height = 50;
            _pnlTop.Padding = new Padding(10);
            this.Controls.Add(_pnlTop);

            // Button Ekle
            _btnResimEkle = new SimpleButton();
            _btnResimEkle.Text = "Resim Ekle";
            //_btnResimEkle.ImageOptions.Image = ... (Resource image if available, skipping for now)
            _btnResimEkle.Size = new Size(120, 30);
            _btnResimEkle.Location = new Point(10, 10);
            _btnResimEkle.Click += BtnResimEkle_Click;
            _pnlTop.Controls.Add(_btnResimEkle);

            // Button Yenile
            _btnYenile = new SimpleButton();
            _btnYenile.Text = "Yenile";
            _btnYenile.Size = new Size(80, 30);
            _btnYenile.Location = new Point(140, 10);
            _btnYenile.Click += (s, e) => LoadGorseller();
            _pnlTop.Controls.Add(_btnYenile);


            // Gallery Control
            _galleryControl = new GalleryControl();
            _galleryControl.Dock = DockStyle.Fill;
            _galleryControl.Gallery.ImageSize = new Size(120, 120);
            _galleryControl.Gallery.ShowItemText = true;
            _galleryControl.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
            _galleryControl.Gallery.HoverImageSize = new Size(130, 130);
            _galleryControl.Gallery.ShowGroupCaption = false;
            
            _galleryGroup = new GalleryItemGroup();
            _galleryControl.Gallery.Groups.Add(_galleryGroup);

            // Drag & Drop Events
            _galleryControl.AllowDrop = true;
            _galleryControl.DragEnter += GalleryControl_DragEnter;
            _galleryControl.DragDrop += GalleryControl_DragDrop;
            
            // Click Events
            _galleryControl.Gallery.ItemClick += Gallery_ItemClick;
            _galleryControl.MouseUp += GalleryControl_MouseUp;

            // Add Gallery (After panel so it fills the rest)
            this.Controls.Add(_galleryControl);
            _galleryControl.BringToFront();
        }

        private void BtnResimEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isNewProductMode && _urunId == 0)
                {
                    MessageHelper.ShowWarning("Lütfen önce ürünü kaydedin.");
                    return;
                }

                using (var ofd = new OpenFileDialog())
                {
                    ofd.Multiselect = true;
                    ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        int successCount = 0;

                        if (_isNewProductMode)
                        {
                            // YENİ ÜRÜN MODU: Geçici klasöre kopyala
                            foreach (var file in ofd.FileNames)
                            {
                                string copiedPath = ImageFileHelper.CopyToTempFolder(file, _tempFolderPath);
                                if (!string.IsNullOrEmpty(copiedPath))
                                {
                                    if (_tempImagePaths != null && !_tempImagePaths.Contains(copiedPath))
                                    {
                                        _tempImagePaths.Add(copiedPath);
                                        successCount++;
                                    }
                                }
                            }

                            if (successCount > 0)
                            {
                                LoadGorseller();
                                MessageHelper.ShowSuccess(string.Format("{0} görsel eklendi. Ürünü kaydettiğinizde kalıcı olacak.", successCount));
                            }
                        }
                        else
                        {
                            // DÜZENLEME MODU: Direkt veritabanına kaydet
                            foreach (var file in ofd.FileNames)
                            {
                                bool isFirst = _galleryGroup.Items.Count == 0 && successCount == 0;
                                InterfaceFactory.Urun.GorselEkle(_urunId, file, "Urun", isFirst);
                                successCount++;
                            }

                            if (successCount > 0)
                            {
                                LoadGorseller();
                                MessageHelper.ShowSuccess(string.Format("{0} görsel eklendi.", successCount));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Resim ekleme hatası: " + ex.Message);
            }
        }

        private void InitializeContextMenu()
        {
            _contextMenu = new ContextMenuStrip();
            var itemAnaGorsel = new ToolStripMenuItem("Ana Görsel Yap");
            itemAnaGorsel.Click += (s, e) => SetAnaGorsel();
            
            var itemSil = new ToolStripMenuItem("Sil");
            itemSil.Click += (s, e) => DeleteGorsel();

            _contextMenu.Items.Add(itemAnaGorsel);
            _contextMenu.Items.Add(itemSil);
        }

        private void LoadGorseller()
        {
            try
            {
                _galleryGroup.Items.Clear();

                if (_isNewProductMode)
                {
                    // YENİ ÜRÜN MODU: Geçici klasörden yükle
                    if (_tempImagePaths == null || _tempImagePaths.Count == 0)
                        return;

                    int index = 0;
                    foreach (string tempPath in _tempImagePaths)
                    {
                        if (File.Exists(tempPath))
                        {
                            using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.Read))
                            {
                                var image = Image.FromStream(stream);
                                var item = new GalleryItem(image, index == 0 ? "★ Ana Görsel" : "", "");
                                item.Tag = tempPath; // Temp path'i tag'de sakla
                                _galleryGroup.Items.Add(item);
                            }
                            index++;
                        }
                    }
                }
                else
                {
                    // DÜZENLEME MODU: Veritabanından yükle
                    if (_urunId == 0) return;

                    var gorseller = InterfaceFactory.Urun.GorselListele(_urunId);

                    foreach (var gorsel in gorseller.OrderBy(x => x.Sira))
                    {
                        if (File.Exists(gorsel.GorselPath))
                        {
                            var image = Image.FromFile(gorsel.GorselPath);
                            var item = new GalleryItem(image, gorsel.AnaGorsel ? "★ Ana Görsel" : "", "");
                            item.Tag = gorsel; // DTO'yu tag'de sakla

                            if (gorsel.AnaGorsel)
                            {
                                item.Caption = "★ Ana Görsel";
                            }

                            _galleryGroup.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("Görseller yüklenirken hata: " + ex.Message, "GORSEL_YONETIM");
            }
        }

        #region Drag & Drop Logic

        private void GalleryControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void GalleryControl_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (!_isNewProductMode && _urunId == 0)
                {
                    MessageHelper.ShowWarning("Lütfen önce ürünü kaydedin.");
                    return;
                }

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                int successCount = 0;

                if (_isNewProductMode)
                {
                    // YENİ ÜRÜN MODU: Geçici klasöre kopyala
                    foreach (string file in files)
                    {
                        var ext = Path.GetExtension(file).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp")
                        {
                            string copiedPath = ImageFileHelper.CopyToTempFolder(file, _tempFolderPath);
                            if (!string.IsNullOrEmpty(copiedPath))
                            {
                                if (_tempImagePaths != null && !_tempImagePaths.Contains(copiedPath))
                                {
                                    _tempImagePaths.Add(copiedPath);
                                    successCount++;
                                }
                            }
                        }
                    }

                    if (successCount > 0)
                    {
                        LoadGorseller();
                        MessageHelper.ShowSuccess(string.Format("{0} görsel eklendi. Ürünü kaydettiğinizde kalıcı olacak.", successCount));
                    }
                }
                else
                {
                    // DÜZENLEME MODU: Direkt veritabanına kaydet
                    foreach (string file in files)
                    {
                        var ext = Path.GetExtension(file).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp")
                        {
                            bool isFirst = _galleryGroup.Items.Count == 0 && successCount == 0;
                            InterfaceFactory.Urun.GorselEkle(_urunId, file, "Urun", isFirst);
                            successCount++;
                        }
                    }

                    if (successCount > 0)
                    {
                        LoadGorseller();
                        MessageHelper.ShowSuccess(string.Format("{0} görsel eklendi.", successCount));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Resim yükleme hatası: " + ex.Message);
            }
        }

        #endregion

        #region Context Menu Actions

        private GalleryItem _selectedItem;

        private void GalleryControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RibbonHitInfo hitInfo = _galleryControl.CalcHitInfo(e.Location);
                if (hitInfo.InGalleryItem)
                {
                    _selectedItem = hitInfo.GalleryItem;
                    _contextMenu.Show(_galleryControl, e.Location);
                }
            }
        }

        private void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
             _selectedItem = e.Item;
        }

        private void SetAnaGorsel()
        {
            if (_selectedItem == null) return;

            if (_isNewProductMode)
            {
                // YENİ ÜRÜN MODU: Listede yeniden sırala (ilk = ana)
                string tempPath = _selectedItem.Tag as string;
                if (tempPath != null && _tempImagePaths != null && _tempImagePaths.Contains(tempPath))
                {
                    _tempImagePaths.Remove(tempPath);
                    _tempImagePaths.Insert(0, tempPath);
                    LoadGorseller();
                }
            }
            else
            {
                // DÜZENLEME MODU: Veritabanında güncelle
                var dto = _selectedItem.Tag as UrunGorselDto;
                if (dto == null) return;

                try
                {
                    InterfaceFactory.Urun.AnaGorselAyarla(dto.GorselId);
                    LoadGorseller();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowError("Hata: " + ex.Message);
                }
            }
        }

        private void DeleteGorsel()
        {
            if (_selectedItem == null) return;

            if (!MessageHelper.ShowConfirmation("Bu görseli silmek istediğinize emin misiniz?"))
                return;

            try
            {
                if (_isNewProductMode)
                {
                    // YENİ ÜRÜN MODU: Listeden çıkar ve geçici dosyayı sil
                    string tempPath = _selectedItem.Tag as string;
                    if (tempPath != null && _tempImagePaths != null)
                    {
                        _tempImagePaths.Remove(tempPath);
                        ImageFileHelper.RemoveTempFile(tempPath);
                        LoadGorseller();
                    }
                }
                else
                {
                    // DÜZENLEME MODU: Veritabanından sil
                    var dto = _selectedItem.Tag as UrunGorselDto;
                    if (dto == null) return;

                    InterfaceFactory.Urun.GorselSil(dto.GorselId);
                    LoadGorseller();
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Hata: " + ex.Message);
            }
        }

        #endregion
    }
}
