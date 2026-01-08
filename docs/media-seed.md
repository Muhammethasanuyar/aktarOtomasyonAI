# Sprint 9 Media Seed Guide

## Overview

Sprint 9 prepares the product image infrastructure using **FileSystem mode**, where image metadata is stored in the database while actual files reside on the file system.

## Image Storage Architecture

### FileSystem Mode

**Database**: Metadata only (urun_gorsel table)
- gorsel_id (PK)
- urun_id (FK)
- gorsel_path (relative path)
- gorsel_tip (MIME type)
- ana_gorsel (main image flag)
- sira (display order)
- olusturma_tarih

**File System**: Actual image files
- Base path: `{MEDIA_PATH}\products\`
- Structure: `{MEDIA_PATH}\products\{urunId}\{filename}.jpg`
- Example: `C:\AktarOtomasyon\Media\products\1\main.jpg`

### Path Convention

**Relative paths in database**:
```
products\1\main.jpg
products\1\detail_01.jpg
products\2\main.jpg
```

**Absolute paths on file system**:
```
C:\AktarOtomasyon\Media\products\1\main.jpg
C:\AktarOtomasyon\Media\products\1\detail_01.jpg
C:\AktarOtomasyon\Media\products\2\main.jpg
```

## Seed Script (08_images.sql)

### Coverage

**50 products** with image metadata:
- All have main image (ana_gorsel = 1, sira = 0)
- Every 3rd product: detail_01.jpg (sira = 1)
- Every 5th product: detail_02.jpg (sira = 2)

### Example Record

```sql
-- Product ID: 1 (Karabiber)
INSERT INTO urun_gorsel (urun_id, gorsel_path, gorsel_tip, ana_gorsel, sira, olusturma_tarih)
VALUES (1, 'products\1\main.jpg', 'image/jpeg', 1, 0, GETDATE());

-- Additional detail image (if applicable)
INSERT INTO urun_gorsel (urun_id, gorsel_path, gorsel_tip, ana_gorsel, sira, olusturma_tarih)
VALUES (1, 'products\1\detail_01.jpg', 'image/jpeg', 0, 1, GETDATE());
```

### Filename Pattern

- **Main image**: `main.jpg`
- **Detail images**: `detail_01.jpg`, `detail_02.jpg`
- **MIME type**: `image/jpeg` (all)

## Demo Image Package

### Source Recommendations

**Free Stock Photo Sites** (CC0 License):
1. **Unsplash.com** - High quality, free to use
   - Search: "spices", "herbs", "tea", "nuts", "honey"

2. **Pexels.com** - Free stock photos
   - Search: "spices", "herbs", "natural products"

3. **Pixabay.com** - Free images
   - Search: "spices", "herbs", "tea leaves"

### Image Requirements

**Format**: JPEG (.jpg)
**Dimensions**: 800x800px recommended (square)
**Size**: <500KB per image
**Quality**: 80-90% JPEG compression
**Naming**: Must match database paths

### Demo Package Structure

```
demo_images/
├── products/
│   ├── 1/
│   │   ├── main.jpg
│   │   └── detail_01.jpg
│   ├── 2/
│   │   ├── main.jpg
│   │   └── detail_01.jpg
│   ├── 3/
│   │   └── main.jpg
│   └── ...
└── image_product_mapping.csv
```

**image_product_mapping.csv**:
```csv
urun_id,urun_kod,urun_adi,main_image,detail_01,detail_02
1,BAH001,Karabiber (Tane),main.jpg,detail_01.jpg,
2,BAH002,Kimyon,main.jpg,detail_01.jpg,
3,BAH003,Zerdeçal,main.jpg,,
```

## Deployment Script

### PowerShell Deployment (deploy_demo_images.ps1)

```powershell
# deploy_demo_images.ps1
# Deploys demo product images from package to MEDIA_PATH

param(
    [string]$SourcePath = ".\demo_images\products",
    [string]$MediaPath = "C:\AktarOtomasyon\Media\products"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Demo Image Deployment" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verify source path exists
if (-not (Test-Path $SourcePath)) {
    Write-Host "ERROR: Source path not found: $SourcePath" -ForegroundColor Red
    exit 1
}

# Create media path if not exists
if (-not (Test-Path $MediaPath)) {
    Write-Host "Creating media directory: $MediaPath" -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $MediaPath -Force | Out-Null
}

# Copy images
Write-Host "Copying images from $SourcePath to $MediaPath..." -ForegroundColor Green
$copiedCount = 0
$errorCount = 0

Get-ChildItem -Path $SourcePath -Recurse -File | ForEach-Object {
    $relativePath = $_.FullName.Substring($SourcePath.Length).TrimStart('\')
    $destPath = Join-Path $MediaPath $relativePath
    $destDir = Split-Path $destPath -Parent

    # Create destination directory if not exists
    if (-not (Test-Path $destDir)) {
        New-Item -ItemType Directory -Path $destDir -Force | Out-Null
    }

    try {
        Copy-Item -Path $_.FullName -Destination $destPath -Force
        Write-Host "  + Copied: $relativePath" -ForegroundColor Gray
        $copiedCount++
    }
    catch {
        Write-Host "  - ERROR: Failed to copy $relativePath" -ForegroundColor Red
        $errorCount++
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Deployment Summary" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Total copied: $copiedCount images" -ForegroundColor Green
Write-Host "Errors: $errorCount" -ForegroundColor $(if ($errorCount -eq 0) { "Green" } else { "Red" })
Write-Host ""

if ($errorCount -eq 0) {
    Write-Host "SUCCESS: All images deployed successfully!" -ForegroundColor Green
} else {
    Write-Host "WARNING: Some images failed to deploy." -ForegroundColor Yellow
}
```

### Usage

```powershell
# Default paths
.\deploy_demo_images.ps1

# Custom paths
.\deploy_demo_images.ps1 -SourcePath "D:\demo_images\products" -MediaPath "C:\AktarOtomasyon\Media\products"
```

## Verification

### SQL Verification

```sql
-- Check image metadata count
SELECT COUNT(*) AS toplam_gorsel FROM urun_gorsel;
-- Expected: >=50

-- Check main images
SELECT COUNT(*) AS ana_gorsel_sayisi FROM urun_gorsel WHERE ana_gorsel = 1;
-- Expected: >=50

-- Products without images
SELECT u.urun_id, u.urun_adi
FROM urun u
LEFT JOIN urun_gorsel ug ON u.urun_id = ug.urun_id
WHERE ug.gorsel_id IS NULL AND u.aktif = 1;
-- Expected: Empty or few results
```

### File System Verification

```powershell
# Count deployed images
Get-ChildItem -Path "C:\AktarOtomasyon\Media\products" -Recurse -File | Measure-Object
# Expected: >=50 files

# Check for missing directories
# Compare database paths with file system
```

### Frontend Verification

1. Open product list screen
2. Verify main images display correctly
3. Open product detail screen
4. Verify image gallery shows all images
5. Check fallback icon for products without images

## Placeholder/Fallback Strategy

### When Images Missing

**Frontend Implementation** (from Sprint 8):
```csharp
// UcURUN_LIST.cs - Image loading with fallback
private void LoadProductImage(int urunId, PictureEdit pictureEdit)
{
    var imagePath = MediaService.GetProductMainImage(urunId);

    if (File.Exists(imagePath))
    {
        pictureEdit.Image = Image.FromFile(imagePath);
    }
    else
    {
        // Fallback to placeholder icon
        pictureEdit.Image = Properties.Resources.product_placeholder;
    }
}
```

**Placeholder Icons**:
- `Resources\product_placeholder.png` (default)
- `Resources\category_baharat.png` (category-specific)
- Size: 800x800px, transparent PNG

## Integration with Application

### MediaService Configuration

```csharp
// appsettings.json or App.config
{
  "MediaSettings": {
    "Mode": "FileSystem",
    "BasePath": "C:\\AktarOtomasyon\\Media",
    "ProductImagePath": "products",
    "MaxFileSize": 5242880,  // 5MB
    "AllowedExtensions": [".jpg", ".jpeg", ".png"]
  }
}
```

### Path Resolution

```csharp
// MediaService.cs
public string GetProductMainImage(int urunId)
{
    using (var sMan = new SqlManager())
    {
        var gorselPath = sMan.ExecuteScalar(
            "SELECT TOP 1 gorsel_path FROM urun_gorsel WHERE urun_id = @urun_id AND ana_gorsel = 1",
            new { urun_id = urunId }
        )?.ToString();

        if (string.IsNullOrEmpty(gorselPath))
            return null;

        // Convert relative path to absolute
        var basePath = ConfigurationManager.AppSettings["MediaBasePath"];
        return Path.Combine(basePath, gorselPath);
    }
}
```

## Best Practices

1. **Always use relative paths** in database
2. **Store absolute base path** in configuration
3. **Implement fallback** for missing images
4. **Compress images** before deployment (<500KB)
5. **Use consistent dimensions** (800x800px)
6. **Organize by product ID** (products\{urunId}\)
7. **Version control**: Exclude actual images from git
8. **Document sources**: Track image licenses

## Performance Considerations

- Image loading is lazy (on-demand)
- Thumbnail generation for grid views (future enhancement)
- Caching strategy for frequently accessed images
- Index on urun_gorsel (urun_id, ana_gorsel) - created in 003_sprint9_indexes.sql

## Troubleshooting

**Problem**: Images not displaying in UI
- **Check**: MediaBasePath configuration
- **Check**: File permissions on Media directory
- **Check**: Database paths match file system structure

**Problem**: Slow image loading
- **Solution**: Implement thumbnail generation
- **Solution**: Add caching layer
- **Solution**: Verify index on urun_gorsel exists

**Problem**: Missing images for some products
- **Solution**: Run 99_verify.sql to identify
- **Solution**: Add placeholder images
- **Solution**: Ensure fallback icon configured

## Future Enhancements

1. **Thumbnail Generation**: Auto-generate 200x200px thumbnails
2. **Image Upload**: Admin screen for image management
3. **Bulk Import**: Excel-based image mapping import
4. **Cloud Storage**: Azure Blob or S3 integration
5. **WebP Format**: Modern format for better compression
6. **Lazy Loading**: Implement virtual scrolling for grids
7. **CDN Integration**: Serve images from CDN for performance

## Related Documentation

- `seed-strategy.md` - Overall seed data strategy
- `dashboard-contract.md` - Dashboard integration
- `performance.md` - Performance optimization including image indexes
