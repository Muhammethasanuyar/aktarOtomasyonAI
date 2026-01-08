# UI Style Guide - Sprint 9

## Overview

This document defines the visual language and styling standards for the Aktar Otomasyon application, established in Sprint 9 Frontend implementation.

## Theme

**Selected Theme**: Office 2019 Colorful (DevExpress)
**Location**: `src/AktarOtomasyon.Forms/Program.cs` line 73

```csharp
UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
```

### Theme Rationale
- Modern, clean appearance
- Good contrast and readability
- Professional look suitable for business applications
- Native DevExpress theme with full component support

## Color Palette

### Standard Colors

Defined in `GridHelper.StandardColors` class:

| Color Name | RGB Value | Hex | Usage |
|------------|-----------|-----|-------|
| **Kritik** | 255, 77, 79 | #FF4D4F | Critical alerts, out of stock, errors |
| **Acil** | 255, 171, 0 | #FFAB00 | Warnings, low stock, urgent items |
| **Normal** | 76, 175, 80 | #4CAF50 | Success states, adequate stock |
| **Info** | 33, 150, 243 | #2196F3 | Informational items, orders |
| **Pasif** | 158, 158, 158 | #9E9E9E | Inactive/disabled items |

### Color Usage Guidelines

**Kritik (Red)**:
- Out of stock items (stock = 0)
- Failed diagnostic checks
- Error messages
- Cancelled orders (strikethrough)

**Acil (Orange/Yellow)**:
- Below minimum stock
- Warning messages
- Pending/draft states requiring attention

**Normal (Green)**:
- Adequate stock levels
- Successful operations
- Completed orders

**Info (Blue)**:
- Sent orders
- Informational notifications
- General status indicators

**Pasif (Gray)**:
- Inactive products
- Disabled UI elements
- Cancelled items

## Typography

### Font Family
**Primary**: Segoe UI
**Fallback**: System default sans-serif

### Font Sizes

| Element | Size | Weight | Usage |
|---------|------|--------|-------|
| Grid Headers | 9pt | Bold | Column headers in all grids |
| Grid Rows | 9pt | Regular | Data rows in grids |
| Form Labels | 9pt | Regular | Input field labels |
| Button Text | 9pt | Regular | All button text |
| Title/Headers | 11-12pt | Bold | Screen titles, panel headers |
| Error Messages | 9pt | Bold | Error/warning text |

### Font Style Guidelines

**Bold**:
- Grid column headers
- Unread notifications
- Critical items
- Panel titles
- Error/warning messages

**Regular**:
- Normal data display
- Input fields
- Standard buttons

**Strikethrough**:
- Cancelled orders
- Deleted/inactive items (when showing in lists)

## Spacing & Layout

### Standard Spacing Units

| Unit | Pixels | Usage |
|------|--------|-------|
| XS | 4px | Minimal padding, tight spacing |
| S | 8px | Default control padding |
| M | 16px | Panel padding, control margins |
| L | 24px | Section spacing, form margins |
| XL | 32px | Major section separators |

### Layout Principles

1. **Consistent Padding**: Use 8px or 16px for control padding
2. **Grid Margins**: 16px around grid controls
3. **Button Spacing**: 8px between buttons in toolbars
4. **Form Spacing**: 16px between form sections
5. **Panel Padding**: 16px internal padding for panels

## Grid Styling

### Standard Grid Configuration

Applied via `GridHelper.ApplyStandardFormatting()`:

```csharp
- Auto-filter row: Enabled
- Group panel: Disabled
- Column auto-width: Disabled (manual sizing)
- Editable: false (read-only)
- Focused cell appearance: Disabled
- Alternate row coloring: Enabled (250, 250, 250)
```

### Grid Row Heights
- Standard: Default DevExpress height
- With images: Auto-height or fixed (e.g., 60px for thumbnails)

### Column Formatting

| Data Type | Format | Width | Alignment |
|-----------|--------|-------|-----------|
| ID | Hidden | - | - |
| Date/Time | dd.MM.yyyy HH:mm | 130px | Left |
| Money | N2 (thousand separator, 2 decimals) | 100px | Right |
| Quantity | N2 | 80px | Right |
| Text | Plain | Auto/Manual | Left |
| Boolean | Checkbox | 60px | Center |

## Icons

### Icon System

**Implementation**: `IconHelper.cs` with programmatic bitmap generation
**Cache**: Enabled for performance
**Standard Size**: 16x16px (can scale to 32x32 for emphasis)

### Standard Icons

| Icon Name | Shape | Color | Usage |
|-----------|-------|-------|-------|
| kritik | Filled Circle | Red | Critical status |
| acil | Triangle + ! | Orange | Warning status |
| normal | Circle + ✓ | Green | Success status |
| info | Circle + i | Blue | Information |
| pasif | Filled Circle | Gray | Inactive |
| stok | Stacked Boxes | Blue | Stock items |
| siparis | Document | Gray | Orders |
| bildirim | Bell | Amber | Notifications |
| urun | Package/Box | Purple | Products |

### Icon Usage

```csharp
// Get standard icon
var icon = IconHelper.GetIcon("kritik", new Size(16, 16));

// Get stock status icon
var stockIcon = IconHelper.GetStockStatusIcon(mevcutStok, kritikStok, emniyetStok);

// Get order status icon
var orderIcon = IconHelper.GetOrderStatusIcon("GONDERILDI");
```

## Conditional Formatting

### Stock Levels

| Condition | Background | Foreground | Font |
|-----------|-----------|------------|------|
| Stock = 0 | RGB(200, 50, 50) | White | Bold |
| Stock < 50% min | RGB(255, 100, 80) | White | Bold |
| Stock ≤ min | RGB(255, 230, 230) | Red | Regular |
| Stock ≤ safety | RGB(255, 250, 230) | Dark Orange | Regular |
| Stock > safety | Default | Default | Regular |

### Order Status

| Status | Background | Foreground | Font |
|--------|-----------|------------|------|
| TASLAK | RGB(255, 255, 224) | RGB(184, 134, 11) | Regular |
| GONDERILDI | RGB(224, 240, 255) | Blue | Regular |
| KISMI | RGB(255, 240, 230) | Orange | Bold |
| TAMAMLANDI | RGB(240, 255, 240) | Green | Regular |
| IPTAL | RGB(240, 240, 240) | Gray | Strikethrough |

### Notifications

| Condition | Background | Foreground | Font |
|-----------|-----------|------------|------|
| Unread | RGB(240, 248, 255) | Type-based | Bold |
| Read | Default | Type-based | Regular |

**Type-based colors**:
- STOK_KRITIK: Red
- STOK_ACIL: Orange
- SIPARIS: Blue
- AI: Purple (RGB(156, 39, 176))

## Message Styling

### Message Types

**Success** (MessageHelper.ShowSuccess):
- Icon: Information (blue i)
- Title: "Başarılı"
- Color: Default info styling

**Error** (MessageHelper.ShowError):
- Icon: Error (red X)
- Title: "Hata"
- Footer: "Detaylar sistem loguna kaydedildi."
- Color: Default error styling

**Warning** (MessageHelper.ShowWarning):
- Icon: Warning (yellow !)
- Title: "Uyarı"
- Color: Default warning styling

**Confirmation** (MessageHelper.ShowConfirmation):
- Icon: Question (blue ?)
- Title: "Onay"
- Buttons: Yes/No

**Info** (MessageHelper.ShowInfo):
- Icon: Information (blue i)
- Title: "Bilgi"
- Color: Default info styling

## Empty States

### EmptyStatePanel Component

**Layout**:
1. Icon (64x64px) - centered
2. Message text (14pt) - centered, gray
3. Action button (optional) - centered below message

**Usage**:
```csharp
emptyStatePanel.Message = "Ürün bulunamadı";
emptyStatePanel.ActionText = "Yeni Ürün Ekle";
emptyStatePanel.ActionClick += (s, e) => btnYeni.PerformClick();
```

**Apply to**:
- Empty product lists
- No critical stock items
- No notifications
- No orders matching filter

## Loading States

### Loading Indicators

**Grid Loading**:
```csharp
try
{
    gridView.BeginUpdate();
    // Load data
}
finally
{
    gridView.EndUpdate();
}
```

**Long Operations**:
- Use try/finally with BeginUpdate/EndUpdate
- Consider SplashScreenManager for operations > 2 seconds
- Show progress for operations with known duration

## Accessibility

### Contrast
- Minimum contrast ratio: 4.5:1 for normal text
- All status colors meet WCAG AA standards

### Focus Indicators
- Use DevExpress default focus styling
- Ensure keyboard navigation works on all grids

### Text Size
- Minimum 9pt font size
- Scalable with Windows DPI settings

## Best Practices

### DO
✓ Use GridHelper.StandardColors for all status coloring
✓ Apply GridHelper.ApplyStandardFormatting() to all grids
✓ Use MessageHelper for all user messages
✓ Show EmptyStatePanel when lists are empty
✓ Use IconHelper for consistent iconography
✓ Wrap grid updates in BeginUpdate/EndUpdate
✓ Format columns using GridHelper formatters

### DON'T
✗ Hardcode Color.Red, Color.Green, etc.
✗ Use MessageBox.Show() directly
✗ Skip BeginUpdate/EndUpdate on grids
✗ Use inconsistent spacing (random margins)
✗ Override theme colors without reason
✗ Create custom icons when standard ones exist

## Migration Notes

### Replacing Old Code

**Before**:
```csharp
MessageBox.Show("Hata oluştu", "Hata", MessageBoxButtons.OK);
e.Appearance.ForeColor = Color.Red;
gridView.OptionsView.ShowAutoFilterRow = true; // per-screen setup
```

**After**:
```csharp
MessageHelper.ShowError("Hata oluştu");
e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
GridHelper.ApplyStandardFormatting(gridView);
```

## Related Documentation

- `grid-standards.md` - Detailed grid configuration
- `ui-component-catalog.md` - Reusable component reference
- `ui-performance.md` - Performance optimization guidelines

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Active
