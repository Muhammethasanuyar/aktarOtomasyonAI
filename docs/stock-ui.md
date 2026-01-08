# Stock UI Documentation - Sprint 9

## Overview

Stock screens provide critical stock monitoring with enhanced conditional formatting and visual indicators.

## UcStokKritik - Critical Stock Screen

**File**: `src/AktarOtomasyon.Forms/Screens/Stok/UcStokKritik.cs`

### Purpose
Displays products at or below critical/minimum stock levels with visual severity indicators.

### Sprint 9 Enhancements

#### 1. Grid Standards Applied
```csharp
private void ApplyGridStandards()
{
    GridHelper.ApplyStandardFormatting(gridView);
    GridHelper.FormatQuantityColumn(gridView.Columns["MevcutStok"]);
    GridHelper.FormatQuantityColumn(gridView.Columns["MinStok"]);
    GridHelper.FormatQuantityColumn(gridView.Columns["EmniyetStok"]);
    GridHelper.FormatQuantityColumn(gridView.Columns["HedefStok"]);
}
```

#### 2. Three-Level Conditional Formatting

**Level 1: Out of Stock (Most Critical)**
- **Condition**: MevcutStok = 0
- **Background**: RGB(200, 50, 50) - Dark red
- **Foreground**: White
- **Font**: Bold

**Level 2: Very Critical**
- **Condition**: MevcutStok < (MinStok * 0.5)
- **Background**: RGB(255, 100, 80) - Orange/Red
- **Foreground**: White
- **Font**: Bold

**Level 3: Below Minimum**
- **Condition**: MevcutStok ≤ MinStok
- **Background**: RGB(255, 230, 230) - Light red
- **Foreground**: GridHelper.StandardColors.Kritik (Red)
- **Font**: Regular

**Implementation**:
```csharp
private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
{
    var view = sender as GridView;
    if (view == null) return;

    try
    {
        var mevcutStok = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MevcutStok"));
        var minStok = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MinStok"));

        if (mevcutStok == 0)
        {
            e.Appearance.BackColor = Color.FromArgb(200, 50, 50);
            e.Appearance.ForeColor = Color.White;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }
        else if (mevcutStok < (minStok * 0.5m))
        {
            e.Appearance.BackColor = Color.FromArgb(255, 100, 80);
            e.Appearance.ForeColor = Color.White;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }
        else if (mevcutStok <= minStok)
        {
            e.Appearance.BackColor = Color.FromArgb(255, 230, 230);
            e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
        }
    }
    catch
    {
        // Ignore formatting errors
    }
}
```

#### 3. Summary Label with Conditional Color

```csharp
private void UpdateWarningLabel(int count)
{
    lblUyari.Text = string.Format("Kritik seviyedeki ürün sayısı: {0}", count);

    if (count > 0)
    {
        lblUyari.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
        lblUyari.Appearance.Font = new Font(lblUyari.Appearance.Font, FontStyle.Bold);
    }
    else
    {
        lblUyari.Appearance.ForeColor = GridHelper.StandardColors.Normal;
        lblUyari.Appearance.Font = new Font(lblUyari.Appearance.Font, FontStyle.Regular);
    }
}
```

#### 4. Navigation

**To Product Card**:
```csharp
private void OpenUrunKart()
{
    var urunId = view.GetFocusedRowCellValue("UrunId");
    NavigationManager.OpenScreen("URUN_KART", ParentFrm.MdiParent, urunId);
}
```

**Triggers**:
- Double-click row
- Enter key
- "Stok Gör" button

---

## Visual Examples

### Grid Display

```
┌──────────────┬───────────┬─────────┬─────────┬───────────┐
│ Ürün Adı     │ Mevcut    │ Min     │ Emniyet │ Hedef     │
├──────────────┼───────────┼─────────┼─────────┼───────────┤
│ Kara Kimyon  │    0.00   │  10.00  │  20.00  │  50.00    │ ← Dark red, white text, bold
│ Tarçın       │    3.00   │  15.00  │  30.00  │  75.00    │ ← Orange, white text, bold
│ Karanfil     │    8.00   │  10.00  │  20.00  │  40.00    │ ← Light red, red text
│ Zeytin Yaprağ│   25.00   │  20.00  │  40.00  │ 100.00    │ ← Normal (above min)
└──────────────┴───────────┴─────────┴─────────┴───────────┘
```

---

## Business Rules

### Critical Stock Determination
Product appears in list if:
- `MevcutStok ≤ MinStok` OR
- `MinStok IS NULL AND MevcutStok ≤ EmniyetStok`

**Source**: `sp_stok_kritik_listele` (from AktarOtomasyon.Stok.Service)

### Recommended Actions
- **Out of Stock (0)**: Immediate order required
- **Very Critical (< 50% min)**: Urgent order required
- **Below Minimum**: Order recommended

### Future Features (TODO)
- `btnSiparisOner` - Suggest order quantity based on HedefStok
- `btnSiparisTaslak` - Create draft order for selected product
- Context menu: "Ürüne Git", "Sipariş Taslak Oluştur"

---

## Testing Checklist

- [ ] Grid shows only products ≤ MinStok
- [ ] Out of stock (0) shown in dark red with bold white text
- [ ] Very critical (< 50% min) shown in orange with bold white text
- [ ] Below minimum shown in light red with red text
- [ ] Summary label count matches grid row count
- [ ] Summary label red when count > 0, green when count = 0
- [ ] Double-click opens product card
- [ ] Enter key opens product card

---

## Related Screens

- **UcStokHareket** - Stock movement history
- **UcUrunKart** - Product details
- **UcSiparisTaslak** - Create orders for critical items

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Complete
