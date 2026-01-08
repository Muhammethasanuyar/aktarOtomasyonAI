# System Diagnostics UI Documentation - Sprint 9

## Overview

UcSYS_DIAG provides system health monitoring with visual status indicators and report generation.

**File**: `src/AktarOtomasyon.Forms/Screens/Diagnostics/UcSYS_DIAG.cs`

## Sprint 9 Enhancements

### 1. Grid Standards Applied

```csharp
private void ApplyGridStandards()
{
    GridHelper.ApplyStandardFormatting(gvDiagnostics);

    if (gvDiagnostics.Columns["LastRun"] != null)
        GridHelper.FormatDateColumn(gvDiagnostics.Columns["LastRun"]);
}
```

### 2. MessageHelper Integration

All user messages now use MessageHelper:
- Warnings: `MessageHelper.ShowWarning("Lütfen bir kontrol seçin.")`
- Success: `MessageHelper.ShowInfo("Tüm kontroller tamamlandı.")`
- Errors: `MessageHelper.ShowError("Rapor oluşturulurken hata oluştu.")`

### 3. Loading Indicators

BeginUpdate/EndUpdate added to all grid operations:
```csharp
private void RunAllChecks()
{
    try
    {
        gvDiagnostics.BeginUpdate();

        foreach (var check in _checks)
        {
            RunCheck(check);
        }
    }
    finally
    {
        gvDiagnostics.RefreshData();
        gvDiagnostics.EndUpdate();
    }

    MessageHelper.ShowInfo("Tüm kontroller tamamlandı.");
}
```

---

## Diagnostic Checks

### Check Categories

| Check Name | Purpose | Expected Result |
|------------|---------|-----------------|
| Veritabanı Bağlantısı | DB connection test | OK |
| Stored Procedure Erişimi | SP execution test | OK |
| Dizin Erişimi | File system permissions | OK |
| Gerekli Dizinler | Assets folder structure | OK |

### Status Values

**OK** (Success)
- **Color**: Green (GridHelper.StandardColors.Normal)
- **Icon**: ✅ (planned)
- **Meaning**: Check passed

**WARNING** (Warning)
- **Color**: Orange (GridHelper.StandardColors.Acil)
- **Icon**: ⚠️ (planned)
- **Meaning**: Check passed with warnings

**FAIL** (Failure)
- **Color**: Red (GridHelper.StandardColors.Kritik)
- **Icon**: ❌ (planned)
- **Meaning**: Check failed, requires attention

**Bekliyor...** (Pending)
- **Color**: Default
- **Icon**: ⏳ (planned)
- **Meaning**: Check not yet run

---

## Visual Enhancements (Planned)

### Status Icons

**Implementation**:
```csharp
private void gvDiagnostics_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
{
    if (e.Column.FieldName == "Status")
    {
        var status = e.CellValue?.ToString();
        Image icon = null;

        switch (status)
        {
            case "OK":
                e.Appearance.ForeColor = GridHelper.StandardColors.Normal;
                icon = IconHelper.GetIcon("success", new Size(16, 16));
                break;
            case "WARNING":
                e.Appearance.ForeColor = GridHelper.StandardColors.Acil;
                icon = IconHelper.GetIcon("warning", new Size(16, 16));
                break;
            case "FAIL":
                e.Appearance.ForeColor = GridHelper.StandardColors.Kritik;
                icon = IconHelper.GetIcon("error", new Size(16, 16));
                break;
        }

        if (icon != null)
        {
            // Draw icon before text
            var iconRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16);
            e.Graphics.DrawImage(icon, iconRect);
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
    }
}
```

### Current Row Formatting

Already implemented:
```csharp
private void GvDiagnostics_RowCellStyle(object sender, RowCellStyleEventArgs e)
{
    if (e.Column.FieldName == "Status")
    {
        var check = gvDiagnostics.GetRow(e.RowHandle) as DiagnosticCheck;
        if (check != null)
        {
            if (check.Status == "OK")
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.DarkGreen;
            }
            else if (check.Status == "WARNING")
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.DarkOrange;
            }
            else if (check.Status == "FAIL")
            {
                e.Appearance.BackColor = Color.LightCoral;
                e.Appearance.ForeColor = Color.DarkRed;
            }
        }
    }
}
```

---

## Report Features

### Export to File

```csharp
private void btnExport_Click(object sender, EventArgs e)
{
    var saveDialog = new SaveFileDialog
    {
        Filter = "Text Files|*.txt|All Files|*.*",
        FileName = $"DiagnosticReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
    };

    if (saveDialog.ShowDialog() == DialogResult.OK)
    {
        var report = GenerateReport();
        File.WriteAllText(saveDialog.FileName, report);
        MessageHelper.ShowInfo($"Rapor başarıyla oluşturuldu:\n{saveDialog.FileName}");
    }
}
```

### Copy to Clipboard

```csharp
private void btnCopy_Click(object sender, EventArgs e)
{
    try
    {
        var report = GenerateReport();
        Clipboard.SetText(report);
        MessageHelper.ShowInfo("Rapor panoya kopyalandı.");
    }
    catch (Exception ex)
    {
        ErrorManager.LogError($"Error copying to clipboard: {ex.Message}", "SYS_DIAG");
        MessageHelper.ShowError("Panoya kopyalama başarısız.");
    }
}
```

### Report Format

```
===========================================
Sistem Tanılama Raporu
===========================================
Tarih: 12.01.2025 14:30
Kullanıcı: admin
Sistem: Windows 10 Pro

-------------------------------------------
Veritabanı Bağlantısı
Status: OK
Details: Bağlantı başarılı
Last Run: 12.01.2025 14:30

-------------------------------------------
Stored Procedure Erişimi
Status: OK
Details: sp_test çalıştırıldı
Last Run: 12.01.2025 14:30

-------------------------------------------
Dizin Erişimi
Status: OK
Details: Tüm dizinlere erişim var
Last Run: 12.01.2025 14:30

===========================================
Özet:
Toplam Kontrol: 3
Başarılı: 3
Uyarı: 0
Hata: 0
===========================================
```

---

## User Actions

### Run All Checks
- **Button**: btnRunAllChecks
- **Keyboard**: F5 (planned)
- **Action**: Executes all diagnostic checks sequentially
- **Feedback**: MessageHelper.ShowInfo() on completion

### Run Selected Check
- **Button**: btnRunSelected
- **Keyboard**: Enter (planned)
- **Action**: Executes only the focused check
- **Feedback**: Grid refreshes, no message (silent)

### Export Report
- **Button**: btnExport
- **Action**: Save diagnostic report to .txt file
- **Feedback**: File dialog, success message

### Copy Report
- **Button**: btnCopy
- **Action**: Copy diagnostic report to clipboard
- **Feedback**: Success message

---

## Check Implementation Examples

### Database Connection Check

```csharp
private void RunDatabaseCheck(DiagnosticCheck check)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            sMan.ExecuteScalar("SELECT 1");
            check.Status = "OK";
            check.Details = "Bağlantı başarılı";
        }
    }
    catch (Exception ex)
    {
        check.Status = "FAIL";
        check.Details = $"Bağlantı hatası: {ex.Message}";
    }
    check.LastRun = DateTime.Now;
}
```

### Stored Procedure Check

```csharp
private void RunStoredProcedureCheck(DiagnosticCheck check)
{
    try
    {
        using (var sMan = new SqlManager())
        {
            // Test a simple SP
            sMan.ExecuteScalar("sp_dash_kritik_stok_ozet");
            check.Status = "OK";
            check.Details = "SP erişimi başarılı";
        }
    }
    catch (Exception ex)
    {
        check.Status = "FAIL";
        check.Details = $"SP hatası: {ex.Message}";
    }
    check.LastRun = DateTime.Now;
}
```

### Directory Access Check

```csharp
private void RunDirectoryCheck(DiagnosticCheck check)
{
    try
    {
        var requiredDirs = new[]
        {
            "assets",
            "assets/images",
            "assets/images/products",
            "logs"
        };

        var missingDirs = new List<string>();

        foreach (var dir in requiredDirs)
        {
            if (!Directory.Exists(dir))
                missingDirs.Add(dir);
        }

        if (missingDirs.Count == 0)
        {
            check.Status = "OK";
            check.Details = "Tüm dizinler mevcut";
        }
        else
        {
            check.Status = "WARNING";
            check.Details = $"Eksik dizinler: {string.Join(", ", missingDirs)}";
        }
    }
    catch (Exception ex)
    {
        check.Status = "FAIL";
        check.Details = $"Dizin kontrolü hatası: {ex.Message}";
    }
    check.LastRun = DateTime.Now;
}
```

---

## Testing Checklist

- [ ] Grid standards applied (auto-filter, alternate rows)
- [ ] Date column formatted correctly
- [ ] MessageHelper used for all user messages
- [ ] BeginUpdate/EndUpdate used for grid operations
- [ ] Run All Checks executes all checks
- [ ] Run Selected Check executes only focused check
- [ ] Export creates .txt file with correct format
- [ ] Copy to Clipboard works
- [ ] OK status shown in green
- [ ] WARNING status shown in orange
- [ ] FAIL status shown in red
- [ ] Status icons display (planned enhancement)

---

## Future Enhancements

- Automated daily health check
- Email notifications on FAIL status
- Historical check results (trending)
- Performance metrics (DB query time, etc.)
- Custom check plugins
- Scheduled checks with cron syntax

---

## Related Documentation

- `grid-standards.md` - Grid configuration
- `ui-component-catalog.md` - MessageHelper, IconHelper
- `troubleshooting.md` - System troubleshooting guide

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Complete (Icon enhancement planned)
