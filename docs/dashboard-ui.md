# Dashboard UI Documentation - Sprint 9

## Overview

The dashboard (UcANA_DASH) provides a real-time overview of critical business metrics, replacing placeholder data from Sprint 1 with live data from stored procedures implemented in Sprint 9 Backend.

**File**: `src/AktarOtomasyon.Forms/Screens/Dashboard/UcANA_DASH.cs`
**Form**: `src/AktarOtomasyon.Forms/Screens/Dashboard/FrmANA_DASH.cs`

## Architecture

### UC-Only Pattern
- All business logic in UcANA_DASH (UserControl)
- FrmANA_DASH is just a shell hosting the UC
- No code duplication between Form and UC

### Data Sources
All data loaded from stored procedures (see `docs/dashboard-contract.md` for SP contracts):
- `sp_dash_kritik_stok_ozet` - Critical stock summary
- `sp_dash_bekleyen_siparis_ozet` - Pending order summary
- `sp_dash_son_bildirimler` - Recent notifications
- `sp_dash_son_stok_hareket` - Recent stock movements (TODO: implement widget)
- `sp_dash_top_hareket_urun` - Most active products (TODO: implement widget)

## Current Implementation (Sprint 9)

### LoadData Method

```csharp
public override void LoadData()
{
    try
    {
        // Sprint 9: Load real data from dashboard stored procedures
        LoadKritikStokWidget();
        LoadBekleyenSiparisWidget();
        LoadBildirimWidget();
        // TODO: Add grids for recent movements in Designer first
        // LoadSonHareketlerWidget();
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"Dashboard.LoadData error: {ex.Message}", "DASHBOARD");
        MessageHelper.ShowError("Dashboard yüklenirken hata oluştu.");
    }
}
```

### Widget 1: Critical Stock Summary

**Method**: `LoadKritikStokWidget()`
**SP**: `sp_dash_kritik_stok_ozet`

**Data Loaded**:
```csharp
int kritikAdet = reader["kritik_adet"];     // Count of items at/below min stock
int acilAdet = reader["acil_adet"];         // Count of items at/below safety stock
int toplamUrun = reader["toplam_urun"];     // Total active products
```

**UI Elements**:
- `lblKritikStokCount` - Shows critical count with conditional color:
  - Red (Kritik) if count > 0
  - Green (Normal) if count = 0

**Planned Enhancements** (requires Designer):
- `lblAcilStokCount` - Show emergency stock count
- `lblToplamUrunCount` - Show total products
- Format: "28 / 20 / 100" (kritik / acil / toplam)

**Navigation** (already implemented):
```csharp
private void btnKritikStokDetay_Click(object sender, EventArgs e)
{
    NavigationManager.OpenScreen("STOK_KRITIK", ParentFrm.MdiParent);
}
```

---

### Widget 2: Pending Orders Summary

**Method**: `LoadBekleyenSiparisWidget()`
**SP**: `sp_dash_bekleyen_siparis_ozet`

**Data Loaded**:
```csharp
int taslakAdet = reader["taslak_adet"];         // Draft orders
int gonderildiAdet = reader["gonderildi_adet"]; // Sent orders (not yet received)
int kismiAdet = reader["kismi_teslim_adet"];    // Partially received orders
decimal bekleyenTutar = reader["bekleyen_tutar"]; // Total amount pending
```

**UI Elements**:
- `lblBekleyenSiparisCount` - Currently shows sent order count

**Planned Enhancements** (requires Designer):
- `lblTaslakCount` - Draft order count
- `lblGonderildiCount` - Sent order count (with color: blue/Info)
- `lblKismiCount` - Partial deliveries (with color: orange/Acil, bold)
- `lblBekleyenTutar` - Total pending amount formatted as money
- Format: "3 taslak | 12 gönderildi | 45,250 TL"

**Navigation** (already implemented):
```csharp
private void btnBekleyenSiparisDetay_Click(object sender, EventArgs e)
{
    NavigationManager.OpenScreen("SIPARIS_LISTE", ParentFrm.MdiParent);
}
```

---

### Widget 3: Recent Notifications

**Method**: `LoadBildirimWidget()`
**SP**: `sp_dash_son_bildirimler` (limit: 10)

**Data Structure**:
- BildirimId (int)
- BildirimTip (string) - STOK_KRITIK, STOK_ACIL, SIPARIS_*, AI_*
- Baslik (string)
- Icerik (string)
- Okundu (bool)
- OlusturmaTarih (datetime)

**UI Elements**:
- `lblBildirimCount` - Shows unread count (currently shows placeholder "3")

**Planned Enhancements** (requires Designer):
Add `grdBildirimler` with `gvBildirimler` GridView:

**Grid Configuration**:
```csharp
GridHelper.ApplyStandardFormatting(gvBildirimler);
GridHelper.FormatDateColumn(gvBildirimler.Columns["OlusturmaTarih"]);

// Row formatting
gvBildirimler.RowStyle += (s, e) => {
    if (e.RowHandle >= 0)
    {
        var okundu = Convert.ToBoolean(
            gvBildirimler.GetRowCellValue(e.RowHandle, "okundu"));
        if (!okundu)
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
    }
};
```

**Navigation** (already implemented):
```csharp
private void btnBildirimDetay_Click(object sender, EventArgs e)
{
    NavigationManager.OpenScreen("BILDIRIM_MRK", ParentFrm.MdiParent);
}
```

---

### Widget 4: Recent Stock Movements (TODO)

**Method**: `LoadSonHareketlerWidget()` (not yet implemented)
**SP**: `sp_dash_son_stok_hareket` (limit: 10)

**Data Structure** (from SP contract):
- UrunAdi (string)
- HareketTip (string) - GIRIS, CIKIS, SAYIM, DUZELTME
- Miktar (decimal)
- AciklamaTip (string) - MANUEL, SIPARIS, SATIS, FIRE
- Tarih (datetime)

**Planned Implementation**:
```csharp
private void LoadSonHareketlerWidget()
{
    using (var sMan = new SqlManager())
    {
        var dt = sMan.ExecuteDataTable("sp_dash_son_stok_hareket",
            new { limit = 10 });

        grdHareketler.DataSource = dt;
        gvHareketler.BestFitColumns();

        // Color-code movement types
        gvHareketler.RowStyle += (s, e) => {
            if (e.RowHandle >= 0)
            {
                var tip = gvHareketler.GetRowCellValue(e.RowHandle, "hareket_tip")?.ToString();
                if (tip == "GIRIS")
                    e.Appearance.ForeColor = GridHelper.StandardColors.Normal; // Green
                else if (tip == "CIKIS")
                    e.Appearance.ForeColor = GridHelper.StandardColors.Kritik; // Red
            }
        };
    }
}
```

**Requires Designer Changes**:
- Add GridControl `grdHareketler`
- Add GridView `gvHareketler`
- Configure columns: UrunAdi, HareketTip, Miktar, Tarih

---

### Widget 5: Most Active Products (TODO)

**Method**: Not yet implemented
**SP**: `sp_dash_top_hareket_urun` (limit: 10)

**Data Structure** (from SP contract):
- UrunAdi (string)
- HareketSayisi (int) - Number of movements in last 30 days
- ToplamMiktar (decimal) - Total quantity moved
- SonHareketTarih (datetime)

**Planned Implementation**:
```csharp
private void LoadTopHareketWidget()
{
    using (var sMan = new SqlManager())
    {
        var dt = sMan.ExecuteDataTable("sp_dash_top_hareket_urun",
            new { limit = 10 });

        grdTopHareket.DataSource = dt;
        gvTopHareket.BestFitColumns();
    }
}
```

**Requires Designer Changes**:
- Add GridControl `grdTopHareket`
- Add GridView `gvTopHareket`
- Configure columns: UrunAdi, HareketSayisi, ToplamMiktar, SonHareketTarih

---

## Planned Dashboard Layout

### Visual Mockup

```
┌─────────────────────────────────────────────────────────────────┐
│  Ana Sayfa                                                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────────────┐  ┌──────────────────┐  ┌───────────────┐ │
│  │ 🔴 Kritik Stok   │  │ ⏳ Bekleyen      │  │ 🔔 Bildirim   │ │
│  │                  │  │    Siparişler    │  │               │ │
│  │      28          │  │       15         │  │      12       │ │
│  │   (28/20/100)    │  │   45,250 TL      │  │   5 okunmadı  │ │
│  │                  │  │  3T | 12G | 2K   │  │               │ │
│  │  [Detay Git →]   │  │  [Detay Git →]   │  │ [Merkez Git →]│ │
│  └──────────────────┘  └──────────────────┘  └───────────────┘ │
│                                                                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  📋 Son Bildirimler (10)          │  📦 Son Hareketler (10)    │
│  ┌───────────────────────────────┐│  ┌──────────────────────┐ │
│  │ Grid                          ││  │ Grid                 │ │
│  │ - Okunmamış bold              ││  │ - GIRIS green        │ │
│  │ - Tür ikonları                ││  │ - CIKIS red          │ │
│  │ - Tarih formatı               ││  │ - Miktar formatı     │ │
│  └───────────────────────────────┘│  └──────────────────────┘ │
│                                                                  │
│  🔥 En Çok Hareket Eden Ürünler (10)  │                        │
│  ┌───────────────────────────────────┐│                        │
│  │ Grid                              ││                        │
│  │ - Hareket sayısı                  ││                        │
│  │ - Toplam miktar                   ││                        │
│  └───────────────────────────────────┘│                        │
└─────────────────────────────────────────────────────────────────┘
```

### Layout Breakdown

**Top Row** (3 summary cards):
- Each card: 300px width, 120px height
- Spacing: 16px between cards
- Content: Icon, title, main metric, sub-metrics, action button

**Middle Section** (2 grids side-by-side):
- Left: Son Bildirimler grid (50% width)
- Right: Son Hareketler grid (50% width)
- Height: 300px
- Spacing: 16px between panels

**Bottom Section** (1 grid):
- En Çok Hareket Eden Ürünler grid (100% width)
- Height: 250px

---

## Designer Changes Required

### Phase 1: Summary Cards Enhancement

Add to `UcANA_DASH.Designer.cs`:

```csharp
// Critical Stock Card
private DevExpress.XtraEditors.LabelControl lblAcilStokCount;
private DevExpress.XtraEditors.LabelControl lblToplamUrunCount;

// Pending Orders Card
private DevExpress.XtraEditors.LabelControl lblTaslakCount;
private DevExpress.XtraEditors.LabelControl lblGonderildiCount;
private DevExpress.XtraEditors.LabelControl lblKismiCount;
private DevExpress.XtraEditors.LabelControl lblBekleyenTutar;

// Notifications Card
// (Already has lblBildirimCount, but may need unread count label)
```

### Phase 2: Grid Widgets

Add to `UcANA_DASH.Designer.cs`:

```csharp
// Recent Notifications
private DevExpress.XtraGrid.GridControl grdBildirimler;
private DevExpress.XtraGrid.Views.Grid.GridView gvBildirimler;

// Recent Stock Movements
private DevExpress.XtraGrid.GridControl grdHareketler;
private DevExpress.XtraGrid.Views.Grid.GridView gvHareketler;

// Most Active Products
private DevExpress.XtraGrid.GridControl grdTopHareket;
private DevExpress.XtraGrid.Views.Grid.GridView gvTopHareket;
```

### Phase 3: Layout Panels

Use `TableLayoutPanel` or `LayoutControl` to organize:
- Row 1: 3 summary card panels (1/3 width each)
- Row 2: 2 grid panels (1/2 width each)
- Row 3: 1 grid panel (full width)

---

## Color Coding Standards

### Stock Status
- **Kritik (Red)**: Count > 0 critical items
- **Normal (Green)**: Count = 0 critical items
- **Acil (Orange)**: Emergency stock count (if shown separately)

### Order Status
- **Info (Blue)**: Sent orders (GONDERILDI)
- **Acil (Orange, Bold)**: Partial deliveries (KISMI)
- **Default**: Draft orders (TASLAK)

### Notification Types
- **Kritik (Red)**: STOK_KRITIK
- **Acil (Orange)**: STOK_ACIL
- **Info (Blue)**: SIPARIS_*
- **Purple**: AI_*
- **Bold**: Unread notifications

### Stock Movements
- **Normal (Green)**: GIRIS (stock in)
- **Kritik (Red)**: CIKIS (stock out)
- **Default**: SAYIM, DUZELTME

---

## Performance Considerations

### Data Refresh
- Dashboard loads on screen open (LoadData)
- No auto-refresh currently implemented
- User can refresh by closing and reopening screen
- Future: Add refresh button with timer option

### SP Performance
- All dashboard SPs use indexed queries
- Limit parameters (10 rows) prevent large data loads
- Fast execution (< 100ms per SP)

### UI Performance
- Use BeginUpdate/EndUpdate on grids
- Load widgets sequentially (current) vs. parallel (future enhancement)

---

## Navigation Flow

### From Dashboard
- **Kritik Stok Detay** → UcSTOK_KRITIK (Critical Stock Screen)
- **Bekleyen Sipariş Detay** → UcSIPARIS_LISTE (Order List Screen)
- **Bildirim Detay** → UcBILDIRIM_MRK (Notification Center)

### To Dashboard
- Main menu: "Ana Sayfa" button
- Breadcrumb navigation
- Keyboard shortcut: Ctrl+Home (if implemented)

---

## Error Handling

### SP Execution Errors
```csharp
try
{
    using (var sMan = new SqlManager())
    {
        var reader = sMan.ExecuteReader("sp_dash_kritik_stok_ozet");
        // ... process data
    }
}
catch (Exception ex)
{
    ErrorManager.LogMessage($"Widget error: {ex.Message}", "DASHBOARD");
    // Show default values or hide widget
    lblKritikStokCount.Text = "-";
}
```

### Missing Data
- Show "0" for counts
- Show "Veri yok" for empty grids
- Use EmptyStatePanel for grid sections (optional)

### Network/DB Errors
- Log with ErrorManager
- Show error message via MessageHelper
- Dashboard displays partial data (loaded widgets show, failed widgets show defaults)

---

## Testing Checklist

### Data Accuracy
- [ ] Critical stock count matches STOK_KRITIK screen
- [ ] Pending order count matches SIPARIS_LISTE filtered by status
- [ ] Notification count matches BILDIRIM_MRK unread count
- [ ] Stock movement grid shows last 10 movements
- [ ] Top products grid shows correct totals

### UI/UX
- [ ] All counts formatted correctly
- [ ] Money values show 2 decimals with thousand separator
- [ ] Dates formatted as dd.MM.yyyy HH:mm
- [ ] Colors match StandardColors palette
- [ ] Bold formatting applied to unread/critical items
- [ ] Navigation buttons work correctly

### Performance
- [ ] Dashboard loads in < 2 seconds
- [ ] No UI freezing during load
- [ ] Grid scrolling smooth

### Error Scenarios
- [ ] Dashboard handles SP execution errors gracefully
- [ ] Missing tables/columns don't crash screen
- [ ] Network interruption shows error message

---

## Future Enhancements

### Sprint 10+
- Auto-refresh with configurable interval (30s, 1min, 5min)
- Expand/collapse widget sections
- User-configurable widget visibility
- Drill-down: Click grid row → open detail screen
- Export dashboard to PDF/Excel
- Mobile-responsive layout
- Real-time notifications (SignalR)

---

## Related Documentation

- `dashboard-contract.md` - Stored procedure contracts
- `ui-component-catalog.md` - Widget components
- `navigation.md` - Screen navigation flow

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Partial Implementation (Cards done, Grids TODO)
