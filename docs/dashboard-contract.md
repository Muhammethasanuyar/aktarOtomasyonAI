# Dashboard Stored Procedure Contracts

## Overview

Sprint 9 introduces 5 dashboard stored procedures that provide real-time metrics for the main dashboard screen. These SPs replace the placeholder data from Sprint 8.

## Integration Point

**File**: `src/AktarOtomasyon.Forms/Screens/Dashboard/UcANA_DASH.cs`

**Current State (Sprint 8)**: Lines 21-23 contain placeholder data
```csharp
// PLACEHOLDER DATA (Sprint 8)
lblKritikStokCount.Text = "12";  // Hardcoded
lblBekleyenSiparisCount.Text = "5";  // Hardcoded
```

**Target State (Sprint 10)**: Replace with SP calls
```csharp
// REAL DATA (Sprint 10)
using (var sMan = new SqlManager())
{
    var kritikData = sMan.ExecuteReader("sp_dash_kritik_stok_ozet");
    if (kritikData.Read())
    {
        lblKritikStokCount.Text = kritikData["kritik_adet"].ToString();
        lblAcilStokCount.Text = kritikData["acil_adet"].ToString();
        lblToplamUrunCount.Text = kritikData["toplam_urun"].ToString();
    }
}
```

## Stored Procedures

### 1. sp_dash_kritik_stok_ozet

**Purpose**: Returns critical stock summary for dashboard widget

**Location**: `db/sp/Dashboard/sp_dash_kritik_stok_ozet.sql`

**Parameters**: None

**Returns**: Single row with 3 columns

| Column | Type | Description |
|--------|------|-------------|
| kritik_adet | INT | Number of products at/below critical stock (RED) |
| acil_adet | INT | Number of products between critical and safety stock (YELLOW) |
| toplam_urun | INT | Total active products with stock settings |

**Example Result**:
```
kritik_adet | acil_adet | toplam_urun
------------|-----------|-------------
     28     |    20     |     100
```

**SQL Call**:
```sql
EXEC sp_dash_kritik_stok_ozet;
```

**C# Integration**:
```csharp
public void LoadKritikStokWidget()
{
    using (var sMan = new SqlManager())
    {
        var reader = sMan.ExecuteReader("sp_dash_kritik_stok_ozet");
        if (reader.Read())
        {
            lblKritikStokCount.Text = reader["kritik_adet"].ToString();
            lblAcilStokCount.Text = reader["acil_adet"].ToString();
            lblToplamUrunCount.Text = reader["toplam_urun"].ToString();

            // Set alert colors
            if (Convert.ToInt32(reader["kritik_adet"]) > 0)
                lblKritikStokCount.ForeColor = Color.Red;
        }
    }
}
```

**Performance**: <100ms with index IX_stok_hareket_urun_tip_tarih

---

### 2. sp_dash_bekleyen_siparis_ozet

**Purpose**: Returns pending order summary for dashboard widget

**Location**: `db/sp/Dashboard/sp_dash_bekleyen_siparis_ozet.sql`

**Parameters**: None

**Returns**: Single row with 4 columns

| Column | Type | Description |
|--------|------|-------------|
| taslak_adet | INT | Number of draft orders |
| gonderildi_adet | INT | Number of sent orders (awaiting delivery) |
| kismi_teslim_adet | INT | Number of partially delivered orders |
| bekleyen_tutar | DECIMAL(18,2) | Total amount of pending orders (TL) |

**Example Result**:
```
taslak_adet | gonderildi_adet | kismi_teslim_adet | bekleyen_tutar
------------|-----------------|-------------------|---------------
     4      |        6        |         5         |   45250.00
```

**SQL Call**:
```sql
EXEC sp_dash_bekleyen_siparis_ozet;
```

**C# Integration**:
```csharp
public void LoadBekleyenSiparisWidget()
{
    using (var sMan = new SqlManager())
    {
        var reader = sMan.ExecuteReader("sp_dash_bekleyen_siparis_ozet");
        if (reader.Read())
        {
            lblTaslakCount.Text = reader["taslak_adet"].ToString();
            lblGonderildiCount.Text = reader["gonderildi_adet"].ToString();
            lblKismiCount.Text = reader["kismi_teslim_adet"].ToString();

            var bekleyenTutar = Convert.ToDecimal(reader["bekleyen_tutar"]);
            lblBekleyenTutar.Text = bekleyenTutar.ToString("N2") + " TL";
        }
    }
}
```

**Performance**: <50ms with index IX_siparis_durum_tarih

---

### 3. sp_dash_son_bildirimler

**Purpose**: Returns top 10 recent notifications (unread first)

**Location**: `db/sp/Dashboard/sp_dash_son_bildirimler.sql`

**Parameters**:
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| @limit | INT | 10 | Number of notifications to return |

**Returns**: Multiple rows with 9 columns

| Column | Type | Description |
|--------|------|-------------|
| bildirim_id | INT | Notification ID (PK) |
| bildirim_tip | NVARCHAR(30) | Type (STOK_KRITIK, SIPARIS_ONAY, etc.) |
| baslik | NVARCHAR(200) | Notification title |
| icerik | NVARCHAR(MAX) | Notification content/message |
| referans_tip | NVARCHAR(50) | Reference type (URUN, SIPARIS, etc.) |
| referans_id | INT | Reference entity ID |
| okundu | BIT | Read status (0=unread, 1=read) |
| olusturma_tarih | DATETIME | Creation timestamp |
| dakika_once | INT | Minutes ago (calculated) |

**Example Result**:
```
bildirim_id | bildirim_tip  | baslik                     | okundu | dakika_once
------------|---------------|----------------------------|--------|-------------
    58      | STOK_KRITIK   | Kritik Stok: Karabiber     |   0    |     15
    57      | STOK_ACIL     | Acil: Kimyon emniyet...    |   0    |     45
    56      | SIPARIS_ONAY  | Sipariş Onaylandı: SIP...  |   1    |    120
```

**SQL Call**:
```sql
-- Default (top 10)
EXEC sp_dash_son_bildirimler;

-- Custom limit
EXEC sp_dash_son_bildirimler @limit = 5;
```

**C# Integration**:
```csharp
public void LoadBildirimlerWidget()
{
    using (var sMan = new SqlManager())
    {
        var dt = sMan.ExecuteDataTable("sp_dash_son_bildirimler",
            new { limit = 10 });

        gridBildirimler.DataSource = dt;

        // Custom rendering
        gridViewBildirimler.Columns["okundu"].ColumnEdit = repositoryItemCheckEdit;
        gridViewBildirimler.Columns["dakika_once"].DisplayFormat.FormatString = "{0} dk önce";

        // Bold unread notifications
        gridViewBildirimler.RowStyle += (s, e) => {
            if (e.RowHandle >= 0)
            {
                var okundu = Convert.ToBoolean(gridViewBildirimler.GetRowCellValue(e.RowHandle, "okundu"));
                if (!okundu)
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        };
    }
}
```

**Performance**: <80ms with index IX_bildirim_okundu_tarih

---

### 4. sp_dash_son_stok_hareket

**Purpose**: Returns top 10 recent stock movements

**Location**: `db/sp/Dashboard/sp_dash_son_stok_hareket.sql`

**Parameters**:
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| @limit | INT | 10 | Number of movements to return |

**Returns**: Multiple rows with 8 columns

| Column | Type | Description |
|--------|------|-------------|
| hareket_id | INT | Movement ID (PK) |
| urun_id | INT | Product ID |
| urun_adi | NVARCHAR(200) | Product name |
| hareket_tip | NVARCHAR(20) | Type (GIRIS, CIKIS, SAYIM, DUZELTME) |
| miktar | DECIMAL(18,2) | Quantity |
| hareket_tarih | DATETIME | Movement timestamp |
| aciklama | NVARCHAR(500) | Description/notes |
| saat_once | INT | Hours ago (calculated) |

**Example Result**:
```
hareket_id | urun_adi           | hareket_tip | miktar | saat_once
-----------|--------------------|-----------  |--------|----------
   1250    | Karabiber (Tane)   | CIKIS       |  2.5   |     1
   1249    | Kimyon             | GIRIS       | 10.0   |     3
   1248    | Ihlamur            | CIKIS       |  5.0   |     5
```

**SQL Call**:
```sql
-- Default (top 10)
EXEC sp_dash_son_stok_hareket;

-- Custom limit
EXEC sp_dash_son_stok_hareket @limit = 20;
```

**C# Integration**:
```csharp
public void LoadStokHareketWidget()
{
    using (var sMan = new SqlManager())
    {
        var dt = sMan.ExecuteDataTable("sp_dash_son_stok_hareket",
            new { limit = 10 });

        gridStokHareket.DataSource = dt;

        // Color-code movement types
        gridViewHareket.RowStyle += (s, e) => {
            if (e.RowHandle >= 0)
            {
                var tip = gridViewHareket.GetRowCellValue(e.RowHandle, "hareket_tip").ToString();
                if (tip == "GIRIS")
                    e.Appearance.ForeColor = Color.Green;
                else if (tip == "CIKIS")
                    e.Appearance.ForeColor = Color.Red;
            }
        };

        // Format timestamp
        gridViewHareket.Columns["saat_once"].DisplayFormat.FormatString = "{0} saat önce";
    }
}
```

**Performance**: <100ms with index IX_stok_hareket_tarih_desc

---

### 5. sp_dash_top_hareket_urun (Optional)

**Purpose**: Returns most active products by movement count

**Location**: `db/sp/Dashboard/sp_dash_top_hareket_urun.sql`

**Parameters**:
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| @limit | INT | 10 | Number of products to return |
| @gun | INT | 30 | Time window in days |

**Returns**: Multiple rows with 6 columns

| Column | Type | Description |
|--------|------|-------------|
| urun_id | INT | Product ID |
| urun_adi | NVARCHAR(200) | Product name |
| kategori_adi | NVARCHAR(100) | Category name |
| hareket_sayisi | INT | Total movement count |
| toplam_giris | DECIMAL(18,2) | Total incoming quantity |
| toplam_cikis | DECIMAL(18,2) | Total outgoing quantity |

**Example Result**:
```
urun_adi           | kategori_adi      | hareket_sayisi | toplam_giris | toplam_cikis
-------------------|-------------------|----------------|--------------|-------------
Karabiber (Tane)   | Baharat           |      45        |    150.0     |    147.0
Kimyon             | Baharat           |      38        |    120.0     |    110.0
```

**SQL Call**:
```sql
-- Default (top 10, last 30 days)
EXEC sp_dash_top_hareket_urun;

-- Custom parameters
EXEC sp_dash_top_hareket_urun @limit = 20, @gun = 60;
```

**C# Integration**:
```csharp
public void LoadTopHareketWidget()
{
    using (var sMan = new SqlManager())
    {
        var dt = sMan.ExecuteDataTable("sp_dash_top_hareket_urun",
            new { limit = 10, gun = 30 });

        chartTopHareket.DataSource = dt;
        chartTopHareket.SeriesDataMember = "urun_adi";
        chartTopHareket.SeriesTemplate.ArgumentDataMember = "urun_adi";
        chartTopHareket.SeriesTemplate.ValueDataMembers.AddRange("hareket_sayisi");
    }
}
```

**Performance**: <150ms with index IX_stok_hareket_urun_tip_tarih

---

## Dashboard Load Sequence

Recommended loading order in `UcANA_DASH.cs`:

```csharp
public override void LoadData()
{
    try
    {
        // 1. Summary widgets (fast, single row)
        LoadKritikStokWidget();          // sp_dash_kritik_stok_ozet (~50ms)
        LoadBekleyenSiparisWidget();     // sp_dash_bekleyen_siparis_ozet (~30ms)

        // 2. List widgets (medium, top N rows)
        LoadBildirimlerWidget();         // sp_dash_son_bildirimler (~80ms)
        LoadStokHareketWidget();         // sp_dash_son_stok_hareket (~100ms)

        // 3. Analytics widgets (slower, optional)
        LoadTopHareketWidget();          // sp_dash_top_hareket_urun (~150ms)

        // Total load time: ~410ms (acceptable for dashboard)
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Dashboard yüklenirken hata: {ex.Message}",
            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

## Error Handling

**Recommended Pattern**:
```csharp
public void LoadKritikStokWidget()
{
    try
    {
        using (var sMan = new SqlManager())
        {
            var reader = sMan.ExecuteReader("sp_dash_kritik_stok_ozet");
            if (reader.Read())
            {
                lblKritikStokCount.Text = reader["kritik_adet"].ToString();
            }
            else
            {
                // No data - show zero
                lblKritikStokCount.Text = "0";
            }
        }
    }
    catch (SqlException ex)
    {
        // Database error
        lblKritikStokCount.Text = "?";
        LogError($"Kritik stok widget hatası: {ex.Message}");
    }
    catch (Exception ex)
    {
        // General error
        lblKritikStokCount.Text = "?";
        LogError($"Beklenmeyen hata: {ex.Message}");
    }
}
```

## Refresh Strategy

**Auto-refresh** (optional):
```csharp
private System.Windows.Forms.Timer dashboardTimer;

public UcANA_DASH()
{
    InitializeComponent();

    // Refresh dashboard every 5 minutes
    dashboardTimer = new System.Windows.Forms.Timer();
    dashboardTimer.Interval = 300000; // 5 minutes in ms
    dashboardTimer.Tick += (s, e) => LoadData();
    dashboardTimer.Start();
}
```

**Manual refresh**:
```csharp
private void btnRefresh_Click(object sender, EventArgs e)
{
    LoadData();
}
```

## Testing Checklist

- [ ] All 5 SPs execute without errors
- [ ] sp_dash_kritik_stok_ozet returns valid counts
- [ ] sp_dash_bekleyen_siparis_ozet returns valid amounts
- [ ] sp_dash_son_bildirimler returns 10 notifications
- [ ] sp_dash_son_stok_hareket returns 10 movements
- [ ] sp_dash_top_hareket_urun returns top products
- [ ] Dashboard loads in <500ms
- [ ] Widgets display correctly with real data
- [ ] Error handling works for missing data
- [ ] Auto-refresh works (if enabled)

## Performance Benchmarks

Expected execution times (with indexes):

| Stored Procedure | Execution Time | Row Count |
|------------------|----------------|-----------|
| sp_dash_kritik_stok_ozet | <100ms | 1 |
| sp_dash_bekleyen_siparis_ozet | <50ms | 1 |
| sp_dash_son_bildirimler | <80ms | 10 |
| sp_dash_son_stok_hareket | <100ms | 10 |
| sp_dash_top_hareket_urun | <150ms | 10 |
| **Total Dashboard Load** | **<500ms** | **32 rows** |

## Related Documentation

- `seed-strategy.md` - Demo data that powers dashboard
- `performance.md` - Index optimization for dashboard queries
- `demo-reset.md` - Reset procedure for testing
