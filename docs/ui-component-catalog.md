# UI Component Catalog - Sprint 9

## Overview

This document catalogs all reusable UI components created in Sprint 9, providing usage examples and integration guidelines.

## Helper Classes

### 1. MessageHelper

**Location**: `src/AktarOtomasyon.Forms/Common/MessageHelper.cs`
**Purpose**: Standardized message display across the application

#### Methods

##### ShowSuccess
```csharp
public static void ShowSuccess(string message, string title = "Başarılı")
```

**Usage**:
```csharp
MessageHelper.ShowSuccess("Ürün başarıyla kaydedildi.");
MessageHelper.ShowSuccess("İşlem tamamlandı.", "Kayıt Başarılı");
```

**Display**:
- Icon: Information (blue)
- Default Title: "Başarılı"
- Button: OK

---

##### ShowError
```csharp
public static void ShowError(string message, string title = "Hata")
```

**Usage**:
```csharp
MessageHelper.ShowError("Veritabanı bağlantı hatası.");
MessageHelper.ShowError(error, "Kayıt Hatası");
```

**Display**:
- Icon: Error (red)
- Default Title: "Hata"
- Footer: "Detaylar sistem loguna kaydedildi."
- Button: OK

**Note**: Automatically appends log message to inform users that details are logged.

---

##### ShowWarning
```csharp
public static void ShowWarning(string message, string title = "Uyarı")
```

**Usage**:
```csharp
MessageHelper.ShowWarning("Lütfen tüm alanları doldurun.");
MessageHelper.ShowWarning("Stok seviyesi düşük.", "Dikkat");
```

**Display**:
- Icon: Warning (yellow)
- Default Title: "Uyarı"
- Button: OK

---

##### ShowConfirmation
```csharp
public static bool ShowConfirmation(string message, string title = "Onay")
```

**Usage**:
```csharp
var confirm = MessageHelper.ShowConfirmation(
    "Bu ürünü silmek istediğinizden emin misiniz?",
    "Silme Onayı"
);

if (confirm)
{
    // Proceed with deletion
}
```

**Display**:
- Icon: Question (blue)
- Default Title: "Onay"
- Buttons: Yes/No
- Returns: true (Yes), false (No)

---

##### ShowInfo
```csharp
public static void ShowInfo(string message, string title = "Bilgi")
```

**Usage**:
```csharp
MessageHelper.ShowInfo("Rapor oluşturuldu.");
MessageHelper.ShowInfo("Tüm kontroller tamamlandı.", "Durum");
```

**Display**:
- Icon: Information (blue)
- Default Title: "Bilgi"
- Button: OK

---

### 2. GridHelper

**Location**: `src/AktarOtomasyon.Forms/Common/GridHelper.cs`
**Purpose**: Consistent grid formatting and column configuration

#### StandardColors Class

```csharp
public static class StandardColors
{
    public static Color Kritik = Color.FromArgb(255, 77, 79);      // Red
    public static Color Acil = Color.FromArgb(255, 171, 0);        // Orange
    public static Color Normal = Color.FromArgb(76, 175, 80);      // Green
    public static Color Info = Color.FromArgb(33, 150, 243);       // Blue
    public static Color Pasif = Color.FromArgb(158, 158, 158);     // Gray
}
```

**Usage**:
```csharp
lblKritikCount.ForeColor = GridHelper.StandardColors.Kritik;
e.Appearance.ForeColor = GridHelper.StandardColors.Normal;
```

---

#### ApplyStandardFormatting

```csharp
public static void ApplyStandardFormatting(GridView gridView)
```

**Applies**:
- Auto-filter row: Enabled
- Group panel: Disabled
- Column auto-width: Disabled
- Editable: false (read-only)
- Focused cell appearance: Disabled
- Font: Segoe UI 9pt
- Header font: Segoe UI 9pt Bold
- Alternate row: Enabled with light gray background

**Usage**:
```csharp
public UcUrunListe()
{
    InitializeComponent();
    ApplyGridStandards();
}

private void ApplyGridStandards()
{
    GridHelper.ApplyStandardFormatting(gvUrunler);
}
```

---

#### FormatIdColumn

```csharp
public static void FormatIdColumn(GridColumn column, bool visible = false)
```

**Usage**:
```csharp
if (gvUrunler.Columns["UrunId"] != null)
    GridHelper.FormatIdColumn(gvUrunler.Columns["UrunId"], visible: false);
```

**Effect**: Hides ID column by default, sets width to 80px if visible.

---

#### FormatDateColumn

```csharp
public static void FormatDateColumn(GridColumn column)
```

**Usage**:
```csharp
if (gvUrunler.Columns["OlusturmaTarih"] != null)
    GridHelper.FormatDateColumn(gvUrunler.Columns["OlusturmaTarih"]);
```

**Effect**:
- Format: dd.MM.yyyy HH:mm
- Width: 130px
- Type: DateTime

---

#### FormatMoneyColumn

```csharp
public static void FormatMoneyColumn(GridColumn column)
```

**Usage**:
```csharp
if (gvUrunler.Columns["AlisFiyat"] != null)
    GridHelper.FormatMoneyColumn(gvUrunler.Columns["AlisFiyat"]);
```

**Effect**:
- Format: N2 (thousand separator, 2 decimals)
- Width: 100px
- Type: Numeric

---

#### FormatQuantityColumn

```csharp
public static void FormatQuantityColumn(GridColumn column)
```

**Usage**:
```csharp
if (gvStok.Columns["MevcutStok"] != null)
    GridHelper.FormatQuantityColumn(gvStok.Columns["MevcutStok"]);
```

**Effect**:
- Format: N2 (thousand separator, 2 decimals)
- Width: 80px
- Type: Numeric

---

### 3. IconHelper

**Location**: `src/AktarOtomasyon.Forms/Common/IconHelper.cs`
**Purpose**: Standardized icon generation with caching

#### GetIcon

```csharp
public static Image GetIcon(string iconName, Size? size = null)
```

**Available Icons**:
- kritik, critical, error
- acil, warning, uyari
- normal, success, basarili
- info, bilgi
- pasif, inactive
- stok, stock
- siparis, order
- bildirim, notification
- urun, product

**Usage**:
```csharp
// Get standard icon (16x16)
var icon = IconHelper.GetIcon("kritik");

// Get larger icon (32x32)
var largeIcon = IconHelper.GetIcon("warning", new Size(32, 32));

// Use in button
btnDelete.Image = IconHelper.GetIcon("error");
```

---

#### GetStockStatusIcon

```csharp
public static Image GetStockStatusIcon(decimal mevcut, decimal kritik,
                                       decimal emniyet, Size? size = null)
```

**Logic**:
- mevcut ≤ kritik → "kritik" (red circle)
- mevcut ≤ emniyet → "acil" (orange triangle)
- mevcut > emniyet → "normal" (green check)

**Usage**:
```csharp
var statusIcon = IconHelper.GetStockStatusIcon(
    mevcutStok: 5,
    kritik: 10,
    emniyet: 20
);
picStatus.Image = statusIcon;
```

---

#### GetOrderStatusIcon

```csharp
public static Image GetOrderStatusIcon(string durum, Size? size = null)
```

**Mapping**:
- TASLAK → info (blue)
- GONDERILDI → acil (orange)
- KISMI → warning (orange)
- TAMAMLANDI → success (green)
- IPTAL → error (red)

**Usage**:
```csharp
var orderIcon = IconHelper.GetOrderStatusIcon("GONDERILDI");
picOrderStatus.Image = orderIcon;
```

---

#### ClearCache

```csharp
public static void ClearCache()
```

**Usage**:
```csharp
// Call when changing theme or reloading icons
IconHelper.ClearCache();
```

---

### 4. EmptyStatePanel

**Location**: `src/AktarOtomasyon.Forms/Common/EmptyStatePanel.cs`
**Purpose**: Reusable empty state component for empty grids/lists

#### Properties

```csharp
public string Message { get; set; }
public string ActionText { get; set; }
public event EventHandler ActionClick;
```

#### Usage

**In Designer**:
1. Add EmptyStatePanel to form
2. Dock: Fill or position manually
3. Set Visible: false initially

**In Code**:
```csharp
private void ShowEmptyState(string message, string actionText = null)
{
    emptyStatePanel.Message = message;
    emptyStatePanel.ActionText = actionText;
    emptyStatePanel.Visible = true;
    gridControl.Visible = false;
}

private void HideEmptyState()
{
    emptyStatePanel.Visible = false;
    gridControl.Visible = true;
}

// Apply filter example
private void ApplyFilter()
{
    var products = InterfaceFactory.Urun.Listele(filter);

    if (products.Count == 0)
    {
        ShowEmptyState("Ürün bulunamadı", "Yeni Ürün Ekle");
    }
    else
    {
        HideEmptyState();
        gridControl.DataSource = products;
    }
}

// Optional: Handle action click
private void InitializeEmptyState()
{
    emptyStatePanel.ActionClick += (s, e) => btnYeni.PerformClick();
}
```

**Visual Layout**:
```
┌──────────────────────────┐
│                          │
│      [Icon 64x64]        │
│                          │
│   Ürün bulunamadı        │
│                          │
│  [Yeni Ürün Ekle]        │
│                          │
└──────────────────────────┘
```

---

## Common Patterns

### Standard Grid Setup

```csharp
public UcMyScreen()
{
    InitializeComponent();
    ApplyGridStandards();
}

private void ApplyGridStandards()
{
    GridHelper.ApplyStandardFormatting(gvList);

    // Format columns
    if (gvList.Columns["Id"] != null)
        GridHelper.FormatIdColumn(gvList.Columns["Id"], visible: false);
    if (gvList.Columns["CreatedDate"] != null)
        GridHelper.FormatDateColumn(gvList.Columns["CreatedDate"]);
    if (gvList.Columns["Price"] != null)
        GridHelper.FormatMoneyColumn(gvList.Columns["Price"]);
    if (gvList.Columns["Quantity"] != null)
        GridHelper.FormatQuantityColumn(gvList.Columns["Quantity"]);
}
```

---

### Loading Data with Indicators

```csharp
private void RefreshList()
{
    try
    {
        gridView.BeginUpdate();

        var data = InterfaceFactory.Module.Listele(filter);

        if (data.Count == 0)
        {
            ShowEmptyState("Veri bulunamadı");
        }
        else
        {
            HideEmptyState();
            gridControl.DataSource = data;
        }

        gridView.BestFitColumns();
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"RefreshList error: {ex.Message}", "SCREEN_CODE");
        MessageHelper.ShowError("Liste yüklenirken hata oluştu.");
    }
    finally
    {
        gridView.EndUpdate();
    }
}
```

---

### Conditional Row Formatting

```csharp
private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
{
    var view = sender as GridView;
    if (view == null) return;

    try
    {
        var status = view.GetRowCellValue(e.RowHandle, "Status")?.ToString();

        switch (status)
        {
            case "KRITIK":
                e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                break;
            case "UYARI":
                e.Appearance.ForeColor = GridHelper.StandardColors.Acil;
                break;
            case "NORMAL":
                e.Appearance.ForeColor = GridHelper.StandardColors.Normal;
                break;
        }
    }
    catch
    {
        // Ignore formatting errors
    }
}
```

---

### Delete/Deactivate Confirmation

```csharp
private void btnDelete_Click(object sender, EventArgs e)
{
    var selectedRow = gvList.GetFocusedRow() as MyModel;
    if (selectedRow == null)
    {
        MessageHelper.ShowWarning("Lütfen bir kayıt seçin.");
        return;
    }

    var confirm = MessageHelper.ShowConfirmation(
        $"{selectedRow.Name} kaydını silmek istediğinizden emin misiniz?",
        "Silme Onayı"
    );

    if (!confirm) return;

    try
    {
        var error = InterfaceFactory.Module.Sil(selectedRow.Id);

        if (string.IsNullOrEmpty(error))
        {
            MessageHelper.ShowSuccess($"{selectedRow.Name} başarıyla silindi.");
            RefreshList();
        }
        else
        {
            MessageHelper.ShowError(error);
        }
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"Delete error: {ex.Message}", "SCREEN_CODE");
        MessageHelper.ShowError("Silme işlemi sırasında hata oluştu.");
    }
}
```

---

### Save Operation

```csharp
public override string SaveData()
{
    try
    {
        // Validate
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageHelper.ShowWarning("Lütfen ad alanını doldurun.");
            txtName.Focus();
            return "Validation failed";
        }

        // Prepare model
        var model = new MyModel
        {
            Id = _entityId,
            Name = txtName.Text.Trim(),
            // ... other fields
        };

        // Save
        string error;
        if (_entityId == 0)
        {
            error = InterfaceFactory.Module.Ekle(model);
        }
        else
        {
            error = InterfaceFactory.Module.Guncelle(model);
        }

        if (string.IsNullOrEmpty(error))
        {
            MessageHelper.ShowSuccess("Kayıt başarıyla tamamlandı.");
            return null; // Success
        }
        else
        {
            MessageHelper.ShowError(error);
            return error;
        }
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"SaveData error: {ex.Message}", "SCREEN_CODE");
        MessageHelper.ShowError("Kayıt sırasında hata oluştu.");
        return ex.Message;
    }
}
```

---

## Component Integration Checklist

### New List Screen
- [ ] Inherit from UcBase
- [ ] Call ApplyGridStandards() in constructor
- [ ] Format columns using GridHelper
- [ ] Wrap data load in BeginUpdate/EndUpdate
- [ ] Show EmptyStatePanel when no data
- [ ] Use MessageHelper for all messages
- [ ] Apply conditional formatting for status columns
- [ ] Add keyboard shortcuts (F2=New, F3=Edit, F5=Refresh, etc.)

### New Edit Screen
- [ ] Inherit from UcBase
- [ ] Override LoadData(), SaveData(), ClearData(), HasChanges()
- [ ] Use MessageHelper.ShowWarning() for validation
- [ ] Use MessageHelper.ShowSuccess() on save success
- [ ] Use MessageHelper.ShowError() on save error
- [ ] Implement proper try/catch with ErrorManager logging

### Existing Screen Migration
- [ ] Replace all MessageBox.Show() → MessageHelper
- [ ] Replace all DMLManager calls → MessageHelper
- [ ] Replace hardcoded colors → GridHelper.StandardColors
- [ ] Add GridHelper.ApplyStandardFormatting() call
- [ ] Add BeginUpdate/EndUpdate to grid operations
- [ ] Add EmptyStatePanel for empty lists
- [ ] Add icons using IconHelper

---

## Performance Considerations

**Icon Caching**: IconHelper caches all generated icons. Call `IconHelper.ClearCache()` only when necessary (e.g., theme change).

**Grid Updates**: Always wrap grid data binding in BeginUpdate/EndUpdate:
```csharp
try
{
    gridView.BeginUpdate();
    // ... data operations
}
finally
{
    gridView.EndUpdate();
}
```

**Empty State**: Hide EmptyStatePanel when showing grid to avoid double rendering.

---

## Troubleshooting

### EmptyStatePanel not showing
- Check Visible property
- Ensure it's in front (BringToFront)
- Check Z-order in Designer

### Icons not appearing
- Verify icon name spelling
- Check IconHelper.GetIcon() return value is not null
- Ensure image is assigned to control's Image property

### Grid not formatting
- Ensure ApplyStandardFormatting() is called after InitializeComponent()
- Check column names match exactly
- Verify gridView reference is correct (MainView cast)

### Messages not showing
- Ensure MessageHelper methods are static
- Check using statement includes AktarOtomasyon.Forms.Common
- Verify no try/catch is swallowing exceptions

---

## Related Documentation

- `ui-styleguide.md` - Visual design standards
- `grid-standards.md` - Grid configuration details
- `ui-performance.md` - Performance optimization

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Active
