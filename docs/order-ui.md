# Order UI Documentation - Sprint 9

## Overview

Order screens provide comprehensive order management with status-based visual indicators and real-time validation.

## UcSiparisListe - Order List Screen

**File**: `src/AktarOtomasyon.Forms/Screens/Siparis/UcSiparisListe.cs`

### Sprint 9 Enhancements

#### 1. Five-Status Badge System

**TASLAK (Draft)**
- **Background**: RGB(255, 255, 224) - Light yellow
- **Foreground**: RGB(184, 134, 11) - Dark goldenrod
- **Font**: Regular
- **Meaning**: Order created but not yet sent to supplier

**GONDERILDI (Sent)**
- **Background**: RGB(224, 240, 255) - Light blue
- **Foreground**: GridHelper.StandardColors.Info (Blue)
- **Font**: Regular
- **Meaning**: Order sent to supplier, awaiting delivery

**KISMI (Partial)**
- **Background**: RGB(255, 240, 230) - Light orange
- **Foreground**: GridHelper.StandardColors.Acil (Orange)
- **Font**: Bold
- **Meaning**: Partially received, requires attention

**TAMAMLANDI (Completed)**
- **Background**: RGB(240, 255, 240) - Light green
- **Foreground**: GridHelper.StandardColors.Normal (Green)
- **Font**: Regular
- **Meaning**: Fully received

**IPTAL (Cancelled)**
- **Background**: RGB(240, 240, 240) - Light gray
- **Foreground**: GridHelper.StandardColors.Pasif (Gray)
- **Font**: Strikeout
- **Meaning**: Order cancelled

**Implementation**:
```csharp
private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
{
    var siparis = view.GetRow(e.RowHandle) as SiparisModel;
    if (siparis == null) return;

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
}
```

#### 2. Grid Standards

```csharp
GridHelper.ApplyStandardFormatting(gvSiparis);
GridHelper.FormatDateColumn(gvSiparis.Columns["SiparisTarih"]);
GridHelper.FormatDateColumn(gvSiparis.Columns["BeklenenTeslimTarih"]);
GridHelper.FormatMoneyColumn(gvSiparis.Columns["ToplamTutar"]);
```

#### 3. MessageHelper Integration

All validations use MessageHelper:
```csharp
if (siparis.Durum != "TASLAK")
{
    MessageHelper.ShowWarning("Sadece TASLAK durumundaki siparişler düzenlenebilir.");
    return;
}
```

---

## UcSiparisTaslak - Order Draft Screen

**File**: `src/AktarOtomasyon.Forms/Screens/Siparis/UcSiparisTaslak.cs`

### Sprint 9 Planned Enhancements (TODO)

#### 1. Summary Panel

**Layout**:
```
┌─────────────────────────────────────────────────────┐
│ Sipariş Özeti                                       │
├─────────────────────────────────────────────────────┤
│  Kalem Sayısı: 15                                   │
│  Toplam Tutar: 12,450.00 TL                         │
│                                                      │
│  [Kaydet] [Gönder] [İptal]                          │
└─────────────────────────────────────────────────────┘
```

**Implementation**:
```csharp
private void UpdateSummary()
{
    int itemCount = _orderLines.Count;
    decimal totalAmount = _orderLines.Sum(x => x.Tutar);

    lblKalemSayisi.Text = $"{itemCount} kalem";
    lblToplamTutar.Text = $"{totalAmount:N2} TL";
    lblToplamTutar.Font = new Font(lblToplamTutar.Font, FontStyle.Bold);

    // Enable/disable buttons based on line count
    btnGonder.Enabled = itemCount > 0;
}
```

#### 2. Line Item Validation Badges

**Package Multiple Warning**:
```csharp
private void gvLines_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
{
    if (e.Column.FieldName == "Miktar")
    {
        var row = gvLines.GetRow(e.RowHandle) as SiparisSatirDto;
        if (row != null)
        {
            var stokAyar = InterfaceFactory.Urun.StokAyarGetir(row.UrunId);
            if (stokAyar != null && stokAyar.PaketKati > 0)
            {
                if (row.Miktar % stokAyar.PaketKati != 0)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    // Tooltip: "Paket katı: {stokAyar.PaketKati}"
                }
            }
        }
    }
}
```

---

## Order Status Flow

```
TASLAK ──────────────► GONDERILDI ──────────► TAMAMLANDI
  │                         │                       ▲
  │                         │                       │
  │                         └────► KISMI ───────────┘
  │                                  │
  └──────────────────────────────────┴────► IPTAL
```

### Status Transitions

| From | To | Allowed? | Condition |
|------|----|----|-----------|
| TASLAK | GONDERILDI | ✓ | Has line items |
| TASLAK | IPTAL | ✓ | Always |
| GONDERILDI | KISMI | ✓ | Partial receipt |
| GONDERILDI | TAMAMLANDI | ✓ | Full receipt |
| GONDERILDI | IPTAL | ✓ | Before delivery |
| KISMI | TAMAMLANDI | ✓ | Remaining items received |
| KISMI | IPTAL | ✓ | Remaining items cancelled |
| TAMAMLANDI | * | ✗ | Final state |
| IPTAL | * | ✗ | Final state |

---

## Business Rules

### Draft Order Creation
- Must have at least 1 line item
- Each line must have quantity > 0
- Each line must have valid product (not pasif)
- Total amount calculated automatically

### Sending Order
- Validates all line items
- Creates notification for supplier (future)
- Status: TASLAK → GONDERILDI

### Receiving Order
- Can receive partial quantities
- If any line qty < ordered qty: Status = KISMI
- If all lines fully received: Status = TAMAMLANDI
- Updates stock levels (StokHareket entries)

### Package Multiple (PaketKati)
- If product has PaketKati setting:
  - Order quantity should be multiple of PaketKati
  - Warning shown (yellow background) if not multiple
  - Not enforced (user can override)

---

## Visual Examples

### Order List Grid

```
┌────────┬─────────────┬──────────────┬──────────────┬─────────────┐
│ Sipariş│ Tedarikçi   │ Durum        │ Tarih        │ Tutar       │
│ No     │             │              │              │             │
├────────┼─────────────┼──────────────┼──────────────┼─────────────┤
│ 101    │ Tedarikçi A │ TASLAK       │ 10.01.2025   │  2,500.00   │ ← Light yellow bg
│ 102    │ Tedarikçi B │ GONDERILDI   │ 08.01.2025   │  5,800.00   │ ← Light blue bg
│ 103    │ Tedarikçi C │ KISMI        │ 05.01.2025   │  3,200.00   │ ← Light orange bg, bold
│ 104    │ Tedarikçi A │ TAMAMLANDI   │ 03.01.2025   │  4,100.00   │ ← Light green bg
│ 105    │ Tedarikçi D │ IPTAL        │ 01.01.2025   │  1,500.00   │ ← Light gray bg, strikeout
└────────┴─────────────┴──────────────┴──────────────┴─────────────┘
```

---

## Testing Checklist

### Order List
- [ ] All 5 status badges display correctly
- [ ] KISMI orders shown in bold
- [ ] IPTAL orders shown with strikeout
- [ ] Grid standards applied (auto-filter, dates, money)
- [ ] MessageHelper used for all validations
- [ ] Edit only allowed for TASLAK orders

### Order Draft (TODO)
- [ ] Summary panel shows correct counts
- [ ] Total amount calculated correctly
- [ ] Package multiple warnings shown
- [ ] Cannot save empty order
- [ ] Status transitions follow business rules

---

## Related Documentation

- `grid-standards.md` - Grid configuration
- `ui-styleguide.md` - Color palette
- `sp-contract.md` - Order stored procedures

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: List complete, Draft summary panel TODO
