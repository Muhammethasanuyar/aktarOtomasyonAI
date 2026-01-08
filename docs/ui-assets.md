# UI Assets Documentation - Sprint 9

## Overview

This document describes the asset management strategy for UI elements including icons, images, and media files.

## Icon Management

### Icon Strategy

**Approach**: Programmatic generation with caching
**Location**: `IconHelper.cs`
**Rationale**:
- No external dependencies
- Consistent styling
- Scalable to any size
- Cache for performance

### Available Icons

| Icon Name | Visual | Color | Size | Usage |
|-----------|--------|-------|------|-------|
| kritik | ● | Red | 16x16 | Critical status, errors |
| acil | ▲ ! | Orange | 16x16 | Warnings, urgent items |
| normal | ● ✓ | Green | 16x16 | Success, normal status |
| info | ● i | Blue | 16x16 | Information |
| pasif | ● | Gray | 16x16 | Inactive items |
| stok | ▪▪ | Blue | 16x16 | Stock items |
| siparis | 📄 | Gray | 16x16 | Orders |
| bildirim | 🔔 | Amber | 16x16 | Notifications |
| urun | 📦 | Purple | 16x16 | Products |

### Icon Usage

```csharp
// Get 16x16 icon
var icon = IconHelper.GetIcon("kritik");

// Get custom size
var largeIcon = IconHelper.GetIcon("warning", new Size(32, 32));

// Use in button
btnDelete.Image = IconHelper.GetIcon("error");

// Use in image column (grid)
e.Value = IconHelper.GetStockStatusIcon(mevcut, kritik, emniyet);
```

### Icon Cache

**Automatic**: Icons cached on first generation
**Key Format**: `{iconName}_{width}x{height}`
**Clear Cache**: `IconHelper.ClearCache()` (rarely needed)

**Performance**:
- First call: ~1-2ms (generation)
- Subsequent calls: <0.1ms (cache hit)

---

## Product Images

### Storage Strategy

**Location**: `assets/images/products/{UrunId}/`
**File Naming**: GUID-based to prevent conflicts
**Example**: `assets/images/products/123/a5f3c2d8-4e7b-9a1c-3d5f-8e2b4a6c9d1e.jpg`

### Security (Sandboxing)

**Implemented**: Sprint 8
**Validation**:
- Only allow paths within `assets/images/products/`
- Reject paths with `..`, `~`, absolute paths
- Prevent directory traversal attacks

**Code**:
```csharp
public static bool IsPathSafe(string path, int urunId)
{
    var normalizedPath = Path.GetFullPath(path);
    var expectedPrefix = Path.GetFullPath($"assets/images/products/{urunId}/");
    return normalizedPath.StartsWith(expectedPrefix);
}
```

### Image Types

| Type | Purpose | Naming | Size Limit |
|------|---------|--------|-----------|
| Original | Full-size product image | `{guid}.jpg` | 5 MB |
| Thumbnail | Grid display | `thumb_{guid}.jpg` | 100 KB |
| Preview | Detail view | Same as original | 5 MB |

### Thumbnail Generation (Planned)

```csharp
public static void GenerateThumbnail(string originalPath, string thumbnailPath, int maxWidth = 150, int maxHeight = 150)
{
    using (var original = Image.FromFile(originalPath))
    {
        var ratioX = (double)maxWidth / original.Width;
        var ratioY = (double)maxHeight / original.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(original.Width * ratio);
        var newHeight = (int)(original.Height * ratio);

        using (var thumbnail = new Bitmap(newWidth, newHeight))
        using (var graphics = Graphics.FromImage(thumbnail))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(original, 0, 0, newWidth, newHeight);
            thumbnail.Save(thumbnailPath, ImageFormat.Jpeg);
        }
    }
}
```

### Upload Workflow

1. User selects image file
2. Validate file type (.jpg, .jpeg, .png, .gif)
3. Validate file size (≤ 5 MB)
4. Generate GUID filename
5. Save to `assets/images/products/{UrunId}/{guid}.ext`
6. Generate thumbnail: `assets/images/products/{UrunId}/thumb_{guid}.ext`
7. Save UrunGorsel record to DB
8. Refresh image gallery

---

## Theme Assets

### DevExpress Theme

**Selected**: Office 2019 Colorful
**Location**: `Program.cs` line 73

```csharp
UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
```

**Theme Files**: Embedded in DevExpress DLLs (no external files needed)

### Custom Theme Elements

**Not Used**: Sprint 9 uses standard DevExpress theme without customization

**Future**: If custom branding needed:
- Skin Editor (DevExpress tool)
- Custom SVG icons
- Brand color palette override

---

## Empty State Assets

### EmptyStatePanel Icon

**Current**: Programmatic generation (folder icon with "empty" indicator)

```csharp
private void SetDefaultIcon()
{
    var bmp = new Bitmap(64, 64);
    using (var g = Graphics.FromImage(bmp))
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(Color.Transparent);

        // Draw folder icon
        var folderRect = new Rectangle(8, 16, 48, 40);
        using (var brush = new SolidBrush(Color.FromArgb(200, 200, 200)))
        {
            g.FillRectangle(brush, folderRect);
        }

        // Draw "empty" indicator (circle slash)
        using (var pen = new Pen(Color.Gray, 2))
        {
            g.DrawEllipse(pen, 16, 24, 32, 32);
            g.DrawLine(pen, 20, 28, 44, 52);
        }
    }
    picIcon.Image = bmp;
}
```

**Alternative**: Could use SVG or PNG files if preferred

---

## Asset Directory Structure

```
aktar_otomasyon/
├── assets/
│   ├── images/
│   │   └── products/
│   │       ├── 1/
│   │       │   ├── a5f3c2d8-....jpg
│   │       │   ├── thumb_a5f3c2d8-....jpg
│   │       │   └── b7e4d9f1-....jpg
│   │       ├── 2/
│   │       │   └── ...
│   │       └── ...
│   └── icons/ (future - if using SVG/PNG icons)
│       ├── kritik.svg
│       ├── acil.svg
│       └── ...
├── bin/
│   └── Debug/
│       └── (DevExpress theme DLLs)
└── src/
    └── AktarOtomasyon.Forms/
        └── Common/
            ├── IconHelper.cs
            └── EmptyStatePanel.cs
```

---

## Performance Considerations

### Icon Caching
- **Memory**: ~5 KB per icon (16x16)
- **Typical Usage**: 10-20 icons cached = ~100 KB
- **Recommendation**: No cache clearing needed

### Image Loading
- **Original Images**: Load on demand (not preloaded)
- **Thumbnails**: Load in grid (async planned)
- **Disposal**: Always dispose Image objects

**Example**:
```csharp
Image thumbnail = null;
try
{
    thumbnail = Image.FromFile(thumbnailPath);
    pictureBox.Image = thumbnail;
}
catch
{
    thumbnail?.Dispose();
}
```

### Thumbnail Pre-generation
- Generate on upload (not on display)
- Store in same directory as original
- Reduces grid load time significantly

---

## File Size Limits

| Asset Type | Max Size | Recommendation |
|------------|----------|----------------|
| Product Image (Original) | 5 MB | 1-2 MB |
| Product Image (Thumbnail) | 100 KB | 50 KB |
| Icons (Programmatic) | N/A | - |
| Empty State Icons | 10 KB | 5 KB |

### Validation

```csharp
private bool ValidateImageFile(string filePath)
{
    // Check file exists
    if (!File.Exists(filePath))
        return false;

    // Check file size
    var fileInfo = new FileInfo(filePath);
    if (fileInfo.Length > 5 * 1024 * 1024) // 5 MB
    {
        MessageHelper.ShowWarning("Dosya boyutu 5 MB'dan büyük olamaz.");
        return false;
    }

    // Check file type
    var extension = Path.GetExtension(filePath).ToLower();
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    if (!allowedExtensions.Contains(extension))
    {
        MessageHelper.ShowWarning("Sadece JPG, PNG ve GIF dosyaları yüklenebilir.");
        return false;
    }

    return true;
}
```

---

## Accessibility

### Icon Alt Text
- Not applicable (icons generated, not img tags)
- For screen readers: Use control tooltips

### Image Alt Text
- Store alt text in DB (UrunGorsel.Aciklama)
- Display in tooltips on hover

### High Contrast
- DevExpress theme handles high contrast automatically
- Standard colors (GridHelper.StandardColors) meet WCAG AA contrast ratios

---

## Future Asset Enhancements

- SVG icon library integration
- Company logo/branding assets
- Custom loading animations
- Product video support
- 3D product models (future)
- Watermarking for product images
- Image optimization pipeline (compression, format conversion)

---

## Related Documentation

- `ui-styleguide.md` - Color and visual standards
- `ui-component-catalog.md` - IconHelper API
- `media-seed.md` - Image seeding for demo data

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Active
