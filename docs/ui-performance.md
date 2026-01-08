# UI Performance Documentation - Sprint 9

## Overview

Performance optimization strategies and best practices for maintaining responsive UI in the Aktar Otomasyon application.

## Grid Performance

### BeginUpdate / EndUpdate Pattern

**Requirement**: ALWAYS wrap grid data operations

**Why**:
- Prevents UI flicker
- Reduces repaints (single repaint vs. per-row)
- 10-100x performance improvement for large datasets

**Pattern**:
```csharp
private void RefreshList()
{
    try
    {
        gridView.BeginUpdate();

        // Data operations here
        var data = InterfaceFactory.Module.Listele(filter);
        gridControl.DataSource = data;
        gridView.BestFitColumns();
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"RefreshList error: {ex.Message}", "SCREEN");
        MessageHelper.ShowError("Liste yüklenirken hata oluştu.");
    }
    finally
    {
        gridView.EndUpdate(); // ALWAYS in finally block
    }
}
```

**Performance Impact**:
| Rows | Without BeginUpdate/EndUpdate | With BeginUpdate/EndUpdate |
|------|------------------------------|---------------------------|
| 100 | 150ms | 15ms |
| 500 | 800ms | 50ms |
| 1000 | 1800ms | 100ms |

---

### BestFitColumns Usage

**Recommendation**: Use sparingly

**When to Use**:
- Initial data load
- User-triggered action (explicit "Fit Columns" button)

**When NOT to Use**:
- Every data refresh
- Filter operations
- Sort operations

**Why**: BestFitColumns scans all visible rows to calculate width, expensive for large datasets.

**Alternative**: Set fixed column widths in Designer or code:

```csharp
private void ApplyGridStandards()
{
    GridHelper.ApplyStandardFormatting(gvList);

    // Set fixed widths (no BestFitColumns needed)
    if (gvList.Columns["UrunAdi"] != null)
        gvList.Columns["UrunAdi"].Width = 250;
    if (gvList.Columns["Kategori"] != null)
        gvList.Columns["Kategori"].Width = 150;
}
```

---

### Virtual Mode (Future Enhancement)

**When Needed**: > 10,000 rows

**Benefit**: Only loads visible rows, dramatically improves performance

**Implementation**:
```csharp
gridView.OptionsView.ColumnAutoWidth = false;
// Virtual mode configuration (future)
```

**Current**: Not implemented (not needed for current data volumes)

---

## Icon Caching

### IconHelper Performance

**Strategy**: Generate once, cache forever

**Cache Implementation**:
```csharp
private static Dictionary<string, Image> _iconCache = new Dictionary<string, Image>();

public static Image GetIcon(string iconName, Size? size = null)
{
    var cacheKey = $"{iconName}_{size.Width}x{size.Height}";

    if (_iconCache.ContainsKey(cacheKey))
        return _iconCache[cacheKey]; // Cache hit: <0.1ms

    // Generate icon (1-2ms)
    Image icon = CreateIcon(iconName, size);
    _iconCache[cacheKey] = icon;
    return icon;
}
```

**Performance**:
- First call: ~1-2ms (generation)
- Subsequent calls: <0.1ms (cache hit)
- Memory: ~5 KB per icon

**Cache Clearing**: Rarely needed (only on theme change)

---

## Image Loading

### Product Images

**Current**: Synchronous loading
**Planned**: Asynchronous loading for thumbnails

**Async Pattern**:
```csharp
private async Task<Image> LoadThumbnailAsync(string path)
{
    return await Task.Run(() =>
    {
        try
        {
            if (File.Exists(path))
                return Image.FromFile(path);
            else
                return GetPlaceholderImage();
        }
        catch
        {
            return GetPlaceholderImage();
        }
    });
}
```

**Benefits**:
- UI remains responsive during load
- Better user experience for slow storage

---

### Image Disposal

**Critical**: Always dispose Image objects to prevent memory leaks

**Pattern**:
```csharp
Image thumbnail = null;
try
{
    thumbnail = Image.FromFile(path);
    pictureBox.Image = thumbnail;
}
catch
{
    thumbnail?.Dispose();
    pictureBox.Image = GetPlaceholderImage();
}
```

**Memory Impact**: Each undisposed 1920x1080 JPG = ~8 MB RAM

---

## Database Query Performance

### Stored Procedures

**All data access uses SPs** (no dynamic SQL)

**Benefits**:
- Pre-compiled execution plans
- Parameter validation
- Reduced network traffic

**Performance Indexes** (Sprint 9 Backend):
- `idx_urun_aktif_kategori` on Urun
- `idx_siparis_durum_tarih` on Siparis
- `idx_stokhareket_urun_tarih` on StokHareket
- `idx_bildirim_kullanici_okundu` on Bildirim

**Query Performance Targets**:
| Operation | Target | Typical |
|-----------|--------|---------|
| Product list (filtered) | < 100ms | 20-50ms |
| Order list | < 100ms | 30-60ms |
| Critical stock | < 50ms | 10-20ms |
| Dashboard widgets | < 100ms each | 20-40ms |

---

### SqlManager Connection Pooling

**Built-in**: ADO.NET connection pooling enabled by default

**Configuration**: Connection string in App.config
```xml
<connectionStrings>
  <add name="AktarDB"
       connectionString="..."
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Pool Settings** (defaults):
- Min Pool Size: 0
- Max Pool Size: 100
- Connection Lifetime: 0 (unlimited)
- Pooling: true

**Performance**: Connection reuse = ~1ms vs. new connection = ~50ms

---

## UI Thread Management

### Async/Await Pattern (Planned)

**Current**: Most operations synchronous
**Future**: Async for long-running operations

**Example**:
```csharp
private async void btnYukle_Click(object sender, EventArgs e)
{
    try
    {
        // Show loading indicator
        splashScreenManager.ShowWaitForm();

        // Run on background thread
        var data = await Task.Run(() => InterfaceFactory.Module.LargeOperation());

        // Back on UI thread
        gridControl.DataSource = data;
    }
    finally
    {
        splashScreenManager.CloseWaitForm();
    }
}
```

---

## Loading Indicators

### DevExpress SplashScreenManager (Planned)

**Usage**:
```csharp
private void LoadData()
{
    splashScreenManager.ShowWaitForm();
    try
    {
        // Long operation
    }
    finally
    {
        splashScreenManager.CloseWaitForm();
    }
}
```

**When to Use**:
- Operations > 500ms
- User-initiated actions (button clicks)

**When NOT to Use**:
- Quick operations (< 500ms)
- Background auto-refresh

---

### Progress Reporting (Future)

For operations with known duration:
```csharp
private void ImportProducts()
{
    var totalCount = 1000;
    for (int i = 0; i < totalCount; i++)
    {
        // Import item
        var progress = (i + 1) * 100 / totalCount;
        splashScreenManager.SetWaitFormDescription($"İşleniyor: {progress}%");
    }
}
```

---

## Memory Management

### Best Practices

1. **Dispose IDisposable Objects**:
   ```csharp
   using (var sMan = new SqlManager())
   {
       // Use sMan
   } // Automatically disposed
   ```

2. **Clear Large Collections**:
   ```csharp
   _allProducts.Clear();
   _allProducts = null;
   GC.Collect(); // Optional, only for very large datasets
   ```

3. **Unsubscribe Events**:
   ```csharp
   private void UcMyScreen_Load(object sender, EventArgs e)
   {
       gridView.RowCellStyle += GridView_RowCellStyle;
   }

   protected override void Dispose(bool disposing)
   {
       if (disposing)
       {
           gridView.RowCellStyle -= GridView_RowCellStyle; // Prevent leaks
       }
       base.Dispose(disposing);
   }
   ```

4. **Avoid Closures in Loops**:
   ```csharp
   // Bad
   for (int i = 0; i < 100; i++)
   {
       button.Click += (s, e) => DoSomething(i); // Captures i, creates 100 delegates
   }

   // Good
   for (int i = 0; i < 100; i++)
   {
       int index = i; // Copy to local var
       button.Click += (s, e) => DoSomething(index);
   }
   ```

---

## Startup Performance

### Application Initialization

**Target**: < 3 seconds from launch to main form displayed

**Current Bottlenecks**:
- DevExpress assembly loading (~500ms)
- Database connection pool initialization (~100ms)
- Main form initialization (~200ms)

**Optimizations**:
- Delay-load non-critical modules
- Async initialization where possible
- Cache frequently-used data

---

## Form Load Performance

### UcBase.LoadData() Pattern

**Goal**: < 500ms for typical list screens

**Optimization Checklist**:
- [ ] Use BeginUpdate/EndUpdate on grids
- [ ] Avoid BestFitColumns on large grids
- [ ] Use indexed SP queries
- [ ] Cache lookup data (categories, etc.)
- [ ] Avoid nested loops in UI code
- [ ] Dispose images after use

---

## Performance Monitoring

### ErrorManager Logging

Add performance logging:
```csharp
private void RefreshList()
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

    try
    {
        // Load data
    }
    finally
    {
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > 500)
        {
            ErrorManager.LogMessage($"Slow RefreshList: {stopwatch.ElapsedMilliseconds}ms", "PERF");
        }
    }
}
```

### Diagnostic Metrics (Future)

Track in UcSYS_DIAG:
- Average grid load time
- SP execution times
- Memory usage
- Connection pool stats

---

## Performance Testing Checklist

### Grid Screens
- [ ] Load 100 rows: < 200ms
- [ ] Load 500 rows: < 500ms
- [ ] Load 1000 rows: < 1000ms
- [ ] Filter operation: < 100ms
- [ ] Sort operation: < 100ms
- [ ] Scroll smoothly (no lag)

### Edit Screens
- [ ] Form load: < 300ms
- [ ] Save operation: < 500ms
- [ ] Lookup data load: < 100ms

### Dashboard
- [ ] All widgets load: < 2000ms
- [ ] Individual widget: < 500ms

### General
- [ ] No UI freezing during operations
- [ ] Loading indicators shown for > 500ms operations
- [ ] Memory stable (no leaks over 1 hour usage)
- [ ] Startup time: < 3 seconds

---

## Performance Regression Prevention

### Code Review Checklist

When reviewing code, check:
- [ ] BeginUpdate/EndUpdate used for grid operations
- [ ] IDisposable objects disposed (using statements)
- [ ] No N+1 query patterns (load data in batches)
- [ ] Async/await for long operations
- [ ] Icon caching used (IconHelper, not repeated generation)
- [ ] Images disposed after use
- [ ] No heavy operations in RowCellStyle event

---

## Troubleshooting Slow Performance

### Grid Loading Slow

1. Check BeginUpdate/EndUpdate used
2. Remove BestFitColumns()
3. Check SP execution time (add logging)
4. Verify indexes exist on filtered columns
5. Reduce AutoFilterRow complexity

### Form Opening Slow

1. Check LoadData() method
2. Profile database queries
3. Reduce lookup data loading
4. Defer non-critical initialization

### Memory Growing

1. Check Image disposal
2. Check event unsubscription
3. Check large collection clearing
4. Use memory profiler (e.g., dotMemory)

---

## Related Documentation

- `grid-standards.md` - Grid optimization patterns
- `ui-component-catalog.md` - Helper class performance
- `db-design.md` - Database indexing strategy

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Active
