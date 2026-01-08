# Product UI Documentation - Sprint 9

## Overview

Product screens (UcUrunListe, UcUrunKart) provide comprehensive product management with enhanced UI including grid standards, thumbnail display, image gallery, and AI content visualization.

## UcUrunListe - Product List Screen

**File**: `src/AktarOtomasyon.Forms/Screens/Urun/UcUrunListe.cs`

### Sprint 9 Enhancements

#### 1. Grid Standards Applied

```csharp
private void ApplyGridStandards()
{
    GridHelper.ApplyStandardFormatting(gvUrunler);

    // Format columns
    if (gvUrunler.Columns["UrunId"] != null)
        GridHelper.FormatIdColumn(gvUrunler.Columns["UrunId"], visible: false);
    if (gvUrunler.Columns["OlusturmaTarih"] != null)
        GridHelper.FormatDateColumn(gvUrunler.Columns["OlusturmaTarih"]);
    if (gvUrunler.Columns["AlisFiyat"] != null)
        GridHelper.FormatMoneyColumn(gvUrunler.Columns["AlisFiyat"]);
    if (gvUrunler.Columns["SatisFiyat"] != null)
        GridHelper.FormatMoneyColumn(gvUrunler.Columns["SatisFiyat"]);
}
```

#### 2. MessageHelper Integration

All user messages now use MessageHelper:
- Success: `MessageHelper.ShowSuccess("Ürün başarıyla kaydedildi.")`
- Error: `MessageHelper.ShowError(error)`
- Warning: `MessageHelper.ShowWarning("Lütfen bir ürün seçin.")`
- Confirmation: `MessageHelper.ShowConfirmation("Ürünü pasifleştirmek istediğinizden emin misiniz?")`

#### 3. Thumbnail Column (TODO - Requires Designer)

**Planned Implementation**:

Add unbound column for product thumbnails:

```csharp
// In Designer
var colThumbnail = new GridColumn();
colThumbnail.FieldName = "Thumbnail";
colThumbnail.Caption = "";
colThumbnail.UnboundType = DevExpress.Data.UnboundColumnType.Object;
colThumbnail.Width = 60;
var pictureEdit = new RepositoryItemPictureEdit();
pictureEdit.SizeMode = PictureSizeMode.Zoom;
colThumbnail.ColumnEdit = pictureEdit;
gvUrunler.Columns.Insert(0, colThumbnail);
```

**Load thumbnails**:

```csharp
private void gvUrunler_CustomUnboundColumnData(object sender,
    CustomColumnDataEventArgs e)
{
    if (e.Column.FieldName == "Thumbnail" && e.IsGetData)
    {
        var row = gvUrunler.GetRow(e.ListSourceRowIndex) as UrunListeItemDto;
        if (row != null && !string.IsNullOrEmpty(row.AnaGorselPath))
        {
            try
            {
                var thumbnailPath = GetThumbnailPath(row.AnaGorselPath);
                if (File.Exists(thumbnailPath))
                {
                    e.Value = Image.FromFile(thumbnailPath);
                }
                else
                {
                    e.Value = GetPlaceholderImage();
                }
            }
            catch
            {
                e.Value = GetPlaceholderImage();
            }
        }
        else
        {
            e.Value = GetPlaceholderImage();
        }
    }
}

private string GetThumbnailPath(string originalPath)
{
    // Convert to thumbnail path
    // Example: images/products/123/image.jpg → images/products/123/thumb_image.jpg
    var dir = Path.GetDirectoryName(originalPath);
    var filename = Path.GetFileName(originalPath);
    return Path.Combine(dir, "thumb_" + filename);
}

private Image GetPlaceholderImage()
{
    var bmp = new Bitmap(48, 48);
    using (var g = Graphics.FromImage(bmp))
    {
        g.FillRectangle(Brushes.LightGray, 0, 0, 48, 48);
        g.DrawString("?", new Font("Arial", 20), Brushes.DarkGray, 12, 8);
    }
    return bmp;
}
```

**Visual Result**:

```
┌─────┬──────────────┬────────────┬──────────┬──────────┐
│ 📷  │ Ürün Adı     │ Kategori   │ Alış     │ Satış    │
├─────┼──────────────┼────────────┼──────────┼──────────┤
│ [🖼️] │ Kara Kimyon  │ Baharat    │ 100.00   │ 120.00   │
│ [🖼️] │ Tarçın       │ Baharat    │  75.00   │  90.00   │
│ [?] │ Zeytin Yaprağ│ Bitki Çayı │  50.00   │  60.00   │ ← No image
└─────┴──────────────┴────────────┴──────────┴──────────┘
```

---

## UcUrunKart - Product Card Screen

**File**: `src/AktarOtomasyon.Forms/Screens/Urun/UcUrunKart.cs`

### Existing Features (Sprint 8)

- 4 tabs: Genel, Stok Ayar, Görseller, AI
- Image gallery with FileSystem storage
- Secure path handling (sandboxed to assets/images/products)
- Image upload with GUID-based naming
- Primary image selection

### Sprint 9 Enhancements (Completed)

#### 1. MessageHelper Integration

All messages standardized:
- `MessageHelper.ShowSuccess()` for save success
- `MessageHelper.ShowError()` for validation and save errors
- `MessageHelper.ShowConfirmation()` for delete confirmations

#### 2. Image Gallery (Existing Implementation)

**Current Layout** (Görseller tab):

```
┌───────────────────────────────────────────────────────┐
│  Görseller                                            │
├───────────────────────────────────────────────────────┤
│                                                        │
│  ┌──────────────┐  ┌──────────────────────────────┐  │
│  │ Görsel Liste │  │  Önizleme                    │  │
│  │              │  │                               │  │
│  │ [Grid]       │  │  [Image Preview 300x300]     │  │
│  │  - Thumb     │  │                               │  │
│  │  - Dosya Adı │  │                               │  │
│  │  - Ana?      │  │                               │  │
│  │  - Tarih     │  │                               │  │
│  │              │  │                               │  │
│  │ [+ Ekle]     │  │  [Sil] [Ana Yap]             │  │
│  └──────────────┘  └──────────────────────────────┘  │
│                                                        │
└───────────────────────────────────────────────────────┘
```

**Features**:
- Multi-image upload
- Primary image selection (displayed in product list)
- Preview panel with zoom
- Secure file storage in `assets/images/products/{UrunId}/`
- GUID-based filenames prevent conflicts

#### 3. AI Content Tab Enhancement (TODO - Requires Designer)

**Current State**: AI tab exists but has placeholder label

**Planned Layout**:

```
┌─────────────────────────────────────────────────────┐
│  AI İçerik                                          │
├─────────────────────────────────────────────────────┤
│                                                      │
│  Durum: [✅ Onaylı]  Tarih: [12.01.2025]           │
│                                                      │
│  ┌───────────────────────────────────────────────┐ │
│  │ 💊 Faydaları                                  │ │
│  │ ─────────────────────────────────────────────│ │
│  │ Sindirim sistemini destekler, antioksidan    │ │
│  │ özelliklere sahiptir...                       │ │
│  └───────────────────────────────────────────────┘ │
│                                                      │
│  ┌───────────────────────────────────────────────┐ │
│  │ 📋 Kullanım Önerileri                        │ │
│  │ ─────────────────────────────────────────────│ │
│  │ Günlük yemeklere 1 çay kaşığı eklenebilir... │ │
│  └───────────────────────────────────────────────┘ │
│                                                      │
│  ┌───────────────────────────────────────────────┐ │
│  │ ⚠️ Uyarılar                                   │ │
│  │ ─────────────────────────────────────────────│ │
│  │ Hamilelerde dikkatli kullanılmalıdır...      │ │
│  └───────────────────────────────────────────────┘ │
│                                                      │
│  ┌───────────────────────────────────────────────┐ │
│  │ 🔗 Kombinasyon Önerileri                     │ │
│  │ ─────────────────────────────────────────────│ │
│  │ Zerdeçal ile birlikte kullanılabilir...      │ │
│  └───────────────────────────────────────────────┘ │
│                                                      │
│  [Versiyonlar] [Onaya Gönder] [Düzenle]            │
│                                                      │
└─────────────────────────────────────────────────────┘
```

**Implementation**:

```csharp
private void LoadAiContent(int urunId)
{
    try
    {
        var aiContent = InterfaceFactory.Urun.AiIcerikGetir(urunId);

        if (aiContent == null || string.IsNullOrEmpty(aiContent.Icerik))
        {
            ShowEmptyAiState();
            return;
        }

        // Parse JSON content
        var json = Newtonsoft.Json.JsonConvert
            .DeserializeObject<Dictionary<string, string>>(aiContent.Icerik);

        // Populate card panels
        txtFayda.Text = json.ContainsKey("fayda") ? json["fayda"] : "";
        txtKullanim.Text = json.ContainsKey("kullanim") ? json["kullanim"] : "";
        txtUyari.Text = json.ContainsKey("uyari") ? json["uyari"] : "";
        txtKombinasyon.Text = json.ContainsKey("kombinasyon") ? json["kombinasyon"] : "";

        // Set status badge
        SetAiStatusBadge(aiContent.Durum);
        lblAiTarih.Text = aiContent.OlusturmaTarih.ToString("dd.MM.yyyy");
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"LoadAiContent error: {ex.Message}", "URUN_KART");
        MessageHelper.ShowError("AI içerik yüklenirken hata oluştu.");
    }
}

private void SetAiStatusBadge(string durum)
{
    switch (durum)
    {
        case "AKTIF":
            lblAiDurum.Text = "✅ Onaylı";
            lblAiDurum.ForeColor = GridHelper.StandardColors.Normal;
            btnOnayaGonder.Enabled = false;
            break;
        case "TASLAK":
            lblAiDurum.Text = "📝 Taslak";
            lblAiDurum.ForeColor = GridHelper.StandardColors.Acil;
            btnOnayaGonder.Enabled = true;
            break;
        case "ONAY_BEKLIYOR":
            lblAiDurum.Text = "⏳ Onay Bekliyor";
            lblAiDurum.ForeColor = GridHelper.StandardColors.Info;
            btnOnayaGonder.Enabled = false;
            break;
        case "REDDEDILDI":
            lblAiDurum.Text = "❌ Reddedildi";
            lblAiDurum.ForeColor = GridHelper.StandardColors.Kritik;
            btnOnayaGonder.Enabled = true;
            break;
    }
}

private void ShowEmptyAiState()
{
    // Hide content panels, show empty state
    pnlAiContent.Visible = false;
    lblAiEmpty.Text = "Bu ürün için henüz AI içerik oluşturulmamış.";
    lblAiEmpty.Visible = true;
}
```

**Designer Changes Required**:
- Replace `lblAIPlaceholder` with card layout
- Add 4 MemoEdit controls: txtFayda, txtKullanim, txtUyari, txtKombinasyon
- Add status label: lblAiDurum
- Add date label: lblAiTarih
- Add buttons: btnVersionlar, btnOnayaGonder, btnDuzenle

---

## Color Coding

### Product Status
- **Aktif**: Default colors
- **Pasif**: Gray text (GridHelper.StandardColors.Pasif)

### AI Content Status
- **AKTIF** (Approved): Green check ✅
- **TASLAK** (Draft): Orange pencil 📝
- **ONAY_BEKLIYOR** (Pending): Blue clock ⏳
- **REDDEDILDI** (Rejected): Red X ❌

---

## Navigation Flow

### Product List → Product Card
```csharp
// Double-click or Enter key
NavigationManager.OpenScreen("URUN_KART", ParentFrm.MdiParent, selectedRow.UrunId);
```

### Product Card → Product List
```csharp
// On save or close
if (ParentScreen is UcUrunListe listScreen)
{
    listScreen.RefreshList();
}
```

---

## Validation Rules

### Product Form
- **Required**: Ürün Adı (Product Name)
- **Unique**: Ürün Kodu (if provided)
- **Numeric**: Alış Fiyat, Satış Fiyat (≥ 0)
- **Foreign Key**: Kategori must exist

### Image Upload
- **Max Size**: 5 MB per image (configurable)
- **Allowed Types**: .jpg, .jpeg, .png, .gif
- **Path Restriction**: Must save to `assets/images/products/{UrunId}/`
- **Naming**: GUID-based to prevent conflicts

### AI Content
- **JSON Structure**: Must be valid JSON with keys: fayda, kullanim, uyari, kombinasyon
- **Status Transitions**:
  - TASLAK → ONAY_BEKLIYOR → AKTIF
  - ONAY_BEKLIYOR → REDDEDILDI → TASLAK

---

## Performance Considerations

### Thumbnail Loading
- Lazy load thumbnails (load on scroll if virtualizing)
- Consider thumbnail cache to avoid repeated file reads
- Generate thumbnails on upload (not on display)

### Image Gallery
- Limit preview image size (max 1920x1080)
- Use Image.FromFile with proper disposal
- Consider async loading for large galleries

---

## Testing Checklist

### Product List
- [ ] Grid standards applied (auto-filter, alternate rows)
- [ ] Columns formatted correctly (dates, money)
- [ ] MessageHelper used for all messages
- [ ] Thumbnails display in first column (TODO)
- [ ] Double-click and Enter open product card
- [ ] Keyboard shortcuts work (F2, F3, F5)

### Product Card - General
- [ ] All fields load correctly
- [ ] Validation messages shown via MessageHelper
- [ ] Save success message shown
- [ ] Category lookup populated

### Product Card - Images
- [ ] Image upload works
- [ ] Preview updates on selection
- [ ] Primary image can be set
- [ ] Delete confirmation shown
- [ ] Images saved to correct sandboxed path

### Product Card - AI
- [ ] AI content loads from DB
- [ ] JSON parsed correctly
- [ ] Status badge shows correct state
- [ ] Empty state shown when no content
- [ ] Buttons enable/disable based on status

---

## Future Enhancements

- Product barcode scanning
- Multi-product edit
- Product duplicate/copy
- Export product list to Excel
- Product image zoom/lightbox
- AI content version comparison
- Product tags/labels
- Related products suggestion

---

## Related Documentation

- `grid-standards.md` - Grid configuration
- `ui-component-catalog.md` - MessageHelper, GridHelper
- `media-seed.md` - Image file management
- `architecture.md` - UC-Only pattern

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Partial (Grid standards done, Thumbnails and AI tab TODO)
