# Grid Standards Documentation - Sprint 9

## Overview

This document defines the standard configuration and formatting rules for all DevExpress GridView controls in the application, ensuring consistency and optimal user experience.

## Standard Grid Configuration

### Applied via GridHelper.ApplyStandardFormatting()

**Location**: `src/AktarOtomasyon.Forms/Common/GridHelper.cs`

```csharp
public static void ApplyStandardFormatting(GridView gridView)
{
    // View Options
    gridView.OptionsView.ShowAutoFilterRow = true;
    gridView.OptionsView.ShowGroupPanel = false;
    gridView.OptionsView.ColumnAutoWidth = false;
    gridView.OptionsView.EnableAppearanceEvenRow = true;

    // Behavior Options
    gridView.OptionsBehavior.Editable = false;

    // Selection Options
    gridView.OptionsSelection.EnableAppearanceFocusedCell = false;

    // Appearance
    gridView.Appearance.Row.Font = new Font("Segoe UI", 9F);
    gridView.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
    gridView.Appearance.EvenRow.BackColor = Color.FromArgb(250, 250, 250);
}
```

---

## Configuration Breakdown

### ShowAutoFilterRow: true

**Purpose**: Enable filtering on all columns

**Behavior**:
- Filter row appears below column headers
- User can type filter criteria
- Supports wildcards (*, ?)
- Multi-column filtering (AND logic)

**Example**:
```
┌──────────┬────────────┬──────────┐
│ Ürün Adı │ Kategori   │  Fiyat   │ ← Headers
├──────────┼────────────┼──────────┤
│ [filter] │ [filter]   │ [filter] │ ← Auto-filter row
├──────────┼────────────┼──────────┤
│ Data row │ Data row   │ Data row │
└──────────┴────────────┴──────────┘
```

**Best Practice**: Always enable for list screens (product list, order list, etc.)

---

### ShowGroupPanel: false

**Purpose**: Hide group-by panel for cleaner UI

**Rationale**:
- Most users don't use grouping
- Saves vertical space
- Keeps interface simple
- Can be enabled per-screen if needed

**If Grouping Needed**:
```csharp
gridView.OptionsView.ShowGroupPanel = true;
gridView.GroupSummary.Add(...); // Add group summaries if needed
```

---

### ColumnAutoWidth: false

**Purpose**: Manual column sizing for precise control

**Behavior**:
- Columns use specified Width property
- No automatic resizing on data load
- User can resize columns manually
- Widths persist if using layout saving

**Column Width Guidelines**:
| Column Type | Recommended Width |
|-------------|-------------------|
| ID (hidden) | 80px |
| Date/Time | 130px |
| Money | 100px |
| Quantity | 80px |
| Short Text (Code) | 100-120px |
| Medium Text (Name) | 200-250px |
| Long Text (Description) | 300-400px |
| Boolean/Checkbox | 60px |
| Icon | 40px |

---

### EnableAppearanceEvenRow: true

**Purpose**: Zebra striping for better readability

**Appearance**:
- Odd rows: Default background (white)
- Even rows: Light gray RGB(250, 250, 250)

**Visual**:
```
┌────────────────────────┐
│ Row 1 (white)          │
│ Row 2 (light gray)     │ ← Even row
│ Row 3 (white)          │
│ Row 4 (light gray)     │ ← Even row
└────────────────────────┘
```

---

### Editable: false

**Purpose**: Read-only grids (list screens)

**Rationale**:
- List screens are for viewing, not editing
- Editing happens in dedicated edit screens (URUN_KART, etc.)
- Prevents accidental data modification

**Exceptions**:
- Grid-based editors (e.g., order line items in UcSiparisTaslak)
- Set `gridView.OptionsBehavior.Editable = true` for those specific cases

---

### EnableAppearanceFocusedCell: false

**Purpose**: Row selection instead of cell selection

**Behavior**:
- Entire row highlighted when focused
- No separate cell focus indicator
- Cleaner visual appearance

**Visual**:
```
Before (EnableAppearanceFocusedCell = true):
┌────────────┬────────────┐
│ Product A  │ 100.00     │
│ Product B  │ [200.00]   │ ← Cell focus border
│ Product C  │ 150.00     │
└────────────┴────────────┘

After (EnableAppearanceFocusedCell = false):
┌────────────┬────────────┐
│ Product A  │ 100.00     │
│ Product B  │ 200.00     │ ← Entire row highlighted
│ Product C  │ 150.00     │
└────────────┴────────────┘
```

---

## Font Standards

### Row Font
- **Family**: Segoe UI
- **Size**: 9pt
- **Weight**: Regular
- **Usage**: All data rows

### Header Font
- **Family**: Segoe UI
- **Size**: 9pt
- **Weight**: Bold
- **Usage**: Column headers

**Rationale**: Segoe UI is the Windows system font, providing excellent readability and professional appearance.

---

## Column Formatters

### 1. FormatIdColumn

```csharp
public static void FormatIdColumn(GridColumn column, bool visible = false)
{
    column.Visible = visible;
    column.Width = 80;
    column.OptionsColumn.AllowEdit = false;
}
```

**Usage**:
```csharp
if (gvUrunler.Columns["UrunId"] != null)
    GridHelper.FormatIdColumn(gvUrunler.Columns["UrunId"], visible: false);
```

**Standard**: Hide ID columns by default (primary keys not user-relevant).

---

### 2. FormatDateColumn

```csharp
public static void FormatDateColumn(GridColumn column)
{
    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
    column.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
    column.Width = 130;
}
```

**Format**: dd.MM.yyyy HH:mm (Turkish date format)

**Examples**:
- 12.01.2025 14:30
- 01.06.2024 09:15

**Usage**:
```csharp
if (gvUrunler.Columns["OlusturmaTarih"] != null)
    GridHelper.FormatDateColumn(gvUrunler.Columns["OlusturmaTarih"]);
```

**Alternative Formats** (if needed per-screen):
```csharp
column.DisplayFormat.FormatString = "dd.MM.yyyy";       // Date only
column.DisplayFormat.FormatString = "dd MMMM yyyy";     // 12 Ocak 2025
column.DisplayFormat.FormatString = "HH:mm";            // Time only
```

---

### 3. FormatMoneyColumn

```csharp
public static void FormatMoneyColumn(GridColumn column)
{
    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
    column.DisplayFormat.FormatString = "N2";
    column.Width = 100;
}
```

**Format**: N2 (Number with 2 decimals, thousand separator)

**Examples**:
- 1,234.56
- 45,250.00
- 0.50

**Usage**:
```csharp
if (gvUrunler.Columns["AlisFiyat"] != null)
    GridHelper.FormatMoneyColumn(gvUrunler.Columns["AlisFiyat"]);
if (gvUrunler.Columns["SatisFiyat"] != null)
    GridHelper.FormatMoneyColumn(gvUrunler.Columns["SatisFiyat"]);
```

**Alignment**: Right-aligned by default for numeric columns.

---

### 4. FormatQuantityColumn

```csharp
public static void FormatQuantityColumn(GridColumn column)
{
    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
    column.DisplayFormat.FormatString = "N2";
    column.Width = 80;
}
```

**Format**: N2 (Number with 2 decimals, thousand separator)

**Examples**:
- 25.50
- 1,234.00
- 0.25

**Usage**:
```csharp
if (gvStok.Columns["MevcutStok"] != null)
    GridHelper.FormatQuantityColumn(gvStok.Columns["MevcutStok"]);
if (gvStok.Columns["MinStok"] != null)
    GridHelper.FormatQuantityColumn(gvStok.Columns["MinStok"]);
```

**Note**: Same format as money but narrower width (80px vs 100px).

---

## Conditional Formatting

### RowCellStyle Event Pattern

```csharp
private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
{
    var view = sender as GridView;
    if (view == null) return;

    try
    {
        // Get row data
        var rowData = view.GetRow(e.RowHandle) as MyModel;
        if (rowData == null) return;

        // Apply formatting based on conditions
        if (rowData.Status == "CRITICAL")
        {
            e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }
    }
    catch
    {
        // Ignore formatting errors (row may be filtering/grouping row)
    }
}
```

### Stock Level Formatting

**UcStokKritik.cs** example:

```csharp
if (mevcutStok == 0)
{
    // Out of stock: dark red background, white text, bold
    e.Appearance.BackColor = Color.FromArgb(200, 50, 50);
    e.Appearance.ForeColor = Color.White;
    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
}
else if (mevcutStok < (minStok * 0.5m))
{
    // Very critical: orange background, white text, bold
    e.Appearance.BackColor = Color.FromArgb(255, 100, 80);
    e.Appearance.ForeColor = Color.White;
    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
}
else if (mevcutStok <= minStok)
{
    // Below minimum: light red background, red text
    e.Appearance.BackColor = Color.FromArgb(255, 230, 230);
    e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
}
```

### Order Status Formatting

**UcSiparisListe.cs** example:

```csharp
switch (siparis.Durum)
{
    case "TASLAK":
        e.Appearance.BackColor = Color.FromArgb(255, 255, 224);
        e.Appearance.ForeColor = Color.FromArgb(184, 134, 11);
        break;
    case "GONDERILDI":
        e.Appearance.BackColor = Color.FromArgb(224, 240, 255);
        e.Appearance.ForeColor = GridHelper.StandardColors.Info;
        break;
    case "KISMI":
        e.Appearance.BackColor = Color.FromArgb(255, 240, 230);
        e.Appearance.ForeColor = GridHelper.StandardColors.Acil;
        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        break;
    case "TAMAMLANDI":
        e.Appearance.BackColor = Color.FromArgb(240, 255, 240);
        e.Appearance.ForeColor = GridHelper.StandardColors.Normal;
        break;
    case "IPTAL":
        e.Appearance.BackColor = Color.FromArgb(240, 240, 240);
        e.Appearance.ForeColor = GridHelper.StandardColors.Pasif;
        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
        break;
}
```

---

## Keyboard Navigation

### Standard Shortcuts

| Key | Action | Implementation |
|-----|--------|----------------|
| Enter | Open selected row | gridView_KeyDown |
| F2 | New record | btnYeni.PerformClick() |
| F3 | Edit selected | btnDuzenle.PerformClick() |
| F5 | Refresh list | btnYenile.PerformClick() |
| F8 | Delete/Deactivate | btnSil.PerformClick() |
| Ctrl+F | Focus search | txtArama.Focus() |

### Implementation Pattern

```csharp
private void gridView_KeyDown(object sender, KeyEventArgs e)
{
    if (e.KeyCode == Keys.Enter)
    {
        OpenSelectedRow();
        e.Handled = true;
    }
    else if (e.KeyCode == Keys.F2)
    {
        btnYeni.PerformClick();
        e.Handled = true;
    }
    else if (e.KeyCode == Keys.F3)
    {
        btnDuzenle.PerformClick();
        e.Handled = true;
    }
    else if (e.KeyCode == Keys.F5)
    {
        btnYenile.PerformClick();
        e.Handled = true;
    }
    else if (e.KeyCode == Keys.F8)
    {
        btnSil.PerformClick();
        e.Handled = true;
    }
}
```

---

## Double-Click Navigation

```csharp
private void gridView_DoubleClick(object sender, EventArgs e)
{
    OpenSelectedRow();
}

private void OpenSelectedRow()
{
    var view = gridControl.MainView as GridView;
    if (view == null || view.FocusedRowHandle < 0) return;

    var selectedRow = view.GetFocusedRow() as MyModel;
    if (selectedRow == null) return;

    NavigationManager.OpenScreen("MY_SCREEN_EDIT", ParentFrm.MdiParent, selectedRow.Id);
}
```

---

## Loading Performance

### BeginUpdate / EndUpdate Pattern

**Always wrap grid data operations**:

```csharp
private void RefreshList()
{
    try
    {
        gridView.BeginUpdate();

        var data = InterfaceFactory.Module.Listele(filter);
        gridControl.DataSource = data;

        gridView.BestFitColumns(); // Optional
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

**Why**:
- Prevents UI flicker during data load
- Improves performance (single repaint instead of per-row)
- Required for grids with > 100 rows

---

## BestFitColumns Usage

```csharp
gridView.BestFitColumns(); // Fit all columns
gridView.BestFitColumns(3); // Fit first 3 columns only
gridView.Columns["UrunAdi"].BestFit(); // Fit single column
```

**Recommendations**:
- Use BestFitColumns() after initial data load
- Don't use on every refresh (performance hit)
- Alternative: Set fixed column widths in Designer
- Consider MaxWidth to prevent extremely wide columns:
  ```csharp
  gridView.Columns["UrunAdi"].MaxWidth = 300;
  ```

---

## Grid Button Enablement

**Pattern**: Enable/disable action buttons based on row selection

```csharp
private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
{
    var view = sender as GridView;
    if (view == null) return;

    bool hasRow = view.FocusedRowHandle >= 0;

    btnDuzenle.Enabled = hasRow;
    btnSil.Enabled = hasRow;
    btnDetay.Enabled = hasRow;
    // Keep btnYeni always enabled
}
```

---

## Empty Grid State

### Using EmptyStatePanel

```csharp
private void RefreshList()
{
    var data = InterfaceFactory.Module.Listele(filter);

    if (data.Count == 0)
    {
        // Show empty state
        emptyStatePanel.Message = "Kayıt bulunamadı";
        emptyStatePanel.ActionText = "Yeni Ekle";
        emptyStatePanel.Visible = true;
        gridControl.Visible = false;
    }
    else
    {
        // Show grid
        emptyStatePanel.Visible = false;
        gridControl.Visible = true;
        gridControl.DataSource = data;
    }
}
```

### Alternative: Built-in DevExpress Empty Text

```csharp
gridView.OptionsView.ShowEmptyDataText = true;
gridView.EmptyDataText = "Kayıt bulunamadı";
```

---

## Common Grid Patterns

### 1. List Screen Pattern

```csharp
public partial class UcMyList : UcBase
{
    public UcMyList()
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
        // ... other columns
    }

    public override void LoadData()
    {
        try
        {
            RefreshList();
        }
        catch (Exception ex)
        {
            ErrorManager.LogMessage($"LoadData error: {ex.Message}", "MY_LIST");
            MessageHelper.ShowError("Veri yüklenirken hata oluştu.");
        }
    }

    private void RefreshList()
    {
        try
        {
            gvList.BeginUpdate();

            var data = InterfaceFactory.Module.Listele(filter);
            gridControl.DataSource = data;
        }
        finally
        {
            gvList.EndUpdate();
        }
    }
}
```

---

## Testing Checklist

### Visual
- [ ] Auto-filter row visible
- [ ] Group panel hidden
- [ ] Alternate row coloring applied (light gray)
- [ ] Font: Segoe UI 9pt for rows
- [ ] Font: Segoe UI 9pt Bold for headers

### Functional
- [ ] Grid is read-only (no accidental edits)
- [ ] Filter row works on all columns
- [ ] Enter key opens selected row
- [ ] Double-click opens selected row
- [ ] F5 refreshes list
- [ ] Action buttons enable/disable based on selection

### Data Formatting
- [ ] ID columns hidden
- [ ] Dates formatted as dd.MM.yyyy HH:mm
- [ ] Money formatted with 2 decimals and thousand separator
- [ ] Quantities formatted with 2 decimals
- [ ] Conditional formatting applied correctly

### Performance
- [ ] Grid loads in < 2 seconds
- [ ] No flicker during data load (BeginUpdate/EndUpdate used)
- [ ] Scrolling is smooth
- [ ] BestFitColumns doesn't cause lag

---

## Troubleshooting

### Filter row not showing
- Check `gridView.OptionsView.ShowAutoFilterRow = true`
- Ensure called after InitializeComponent()

### Alternate rows not colored
- Check `gridView.OptionsView.EnableAppearanceEvenRow = true`
- Check `gridView.Appearance.EvenRow.BackColor` is set

### Column widths not applying
- Check `gridView.OptionsView.ColumnAutoWidth = false`
- Set column widths after ApplyStandardFormatting()

### Date/Money not formatting
- Check column name spelling (case-sensitive)
- Verify column exists before calling formatter
- Check data type in grid (must be DateTime or decimal)

### Performance issues
- Ensure BeginUpdate/EndUpdate wraps data operations
- Check for RowCellStyle event doing heavy operations
- Consider virtual mode for > 10,000 rows

---

## Related Documentation

- `ui-styleguide.md` - Visual design standards
- `ui-component-catalog.md` - GridHelper API reference
- `ui-performance.md` - Performance optimization

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Active
