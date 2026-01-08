# UAT Checklist - Sprint 9 UI

## Overview

User Acceptance Testing checklist for Sprint 9 Frontend UI polish and enhancements.

**Sprint**: Sprint 9 Frontend
**Date**: January 2025
**Tester**: ___________________
**Status**: ⬜ Not Started | ⬜ In Progress | ⬜ Complete | ⬜ Failed

---

## Theme & Consistency

### Visual Standards
- [ ] All screens use "Office 2019 Colorful" theme
- [ ] Font is Segoe UI 9pt for data, 9pt Bold for headers
- [ ] Padding is consistent (8px/16px/24px)
- [ ] Color palette consistent across all screens:
  - [ ] Red (Kritik): RGB(255, 77, 79)
  - [ ] Orange (Acil): RGB(255, 171, 0)
  - [ ] Green (Normal): RGB(76, 175, 80)
  - [ ] Blue (Info): RGB(33, 150, 243)
  - [ ] Gray (Pasif): RGB(158, 158, 158)

### Message Standardization
- [ ] All success messages use MessageHelper.ShowSuccess()
- [ ] All error messages use MessageHelper.ShowError()
- [ ] All warnings use MessageHelper.ShowWarning()
- [ ] All confirmations use MessageHelper.ShowConfirmation()
- [ ] Error messages include "Detaylar sistem loguna kaydedildi" footer

---

## Dashboard (UcANA_DASH)

### Data Loading
- [ ] Critical stock widget shows real count from `sp_dash_kritik_stok_ozet`
- [ ] Count is red if > 0, green if = 0
- [ ] Pending orders widget shows real count from `sp_dash_bekleyen_siparis_ozet`
- [ ] Notifications widget shows real count
- [ ] Dashboard loads in < 2 seconds

### Navigation
- [ ] "Kritik Stok Detay" button opens UcSTOK_KRITIK
- [ ] "Bekleyen Sipariş Detay" button opens UcSIPARIS_LISTE
- [ ] "Bildirim Detay" button opens UcBILDIRIM_MRK

### Future Widgets (TODO - Not in Sprint 9)
- [ ] Recent notifications grid (10 rows)
- [ ] Recent stock movements grid (10 rows)
- [ ] Most active products grid (10 rows)

---

## Grid Standards (All List Screens)

### Configuration
- [ ] Auto-filter row visible on all grids
- [ ] Group panel hidden (no grouping UI)
- [ ] Alternate row coloring (light gray every other row)
- [ ] Grids are read-only (not editable)
- [ ] Row selection (not cell selection)

### Column Formatting
- [ ] ID columns hidden
- [ ] Date columns formatted as "dd.MM.yyyy HH:mm"
- [ ] Money columns formatted with 2 decimals and comma separator (N2)
- [ ] Quantity columns formatted with 2 decimals (N2)

### Performance
- [ ] All grids load in < 2 seconds
- [ ] No UI freezing during data load
- [ ] Scrolling is smooth
- [ ] Filter operations are responsive

---

## Product Screens

### UcUrunListe
- [ ] Grid standards applied
- [ ] Columns formatted correctly (dates, money)
- [ ] MessageHelper used for all messages
- [ ] Double-click opens product card
- [ ] Enter key opens product card
- [ ] F2 creates new product
- [ ] F3 edits selected product
- [ ] F5 refreshes list
- [ ] F8 deactivates selected product
- [ ] Empty state shown when no products match filter

**TODO (Not in Sprint 9)**:
- [ ] Thumbnail column shows product images (48x48)

### UcUrunKart
- [ ] All fields load correctly
- [ ] Validation messages shown via MessageHelper
- [ ] Save success message shown
- [ ] Image gallery functional
- [ ] Primary image can be set
- [ ] Images delete with confirmation

**TODO (Not in Sprint 9)**:
- [ ] AI content tab shows cards (Faydalar, Kullanım, Uyarılar, Kombinasyon)
- [ ] AI status badge displays correctly

---

## Stock Screens

### UcStokKritik
- [ ] Grid shows only products at/below minimum stock
- [ ] Out of stock (0) rows: Dark red background, white bold text
- [ ] Very critical (< 50% min) rows: Orange background, white bold text
- [ ] Below minimum rows: Light red background, red text
- [ ] Summary label shows correct count
- [ ] Summary label is red when count > 0, green when count = 0
- [ ] Double-click opens product card
- [ ] Enter key opens product card
- [ ] "Stok Gör" button opens product card

---

## Notification Screen

### UcBildirimMrk
- [ ] Unread notifications shown in bold
- [ ] Unread notifications have light cyan background
- [ ] Notification types color-coded:
  - [ ] STOK_KRITIK: Red text
  - [ ] STOK_ACIL: Orange text
  - [ ] SIPARIS: Blue text
  - [ ] AI: Purple text
- [ ] Filter by status works (All, Unread, Read)
- [ ] "Mark as Read" button works
- [ ] "Mark All as Read" button works with confirmation
- [ ] Double-click shows notification detail and marks as read

---

## Order Screens

### UcSiparisListe
- [ ] Order status badges display correctly:
  - [ ] TASLAK: Light yellow background, dark gold text
  - [ ] GONDERILDI: Light blue background, blue text
  - [ ] KISMI: Light orange background, orange bold text
  - [ ] TAMAMLANDI: Light green background, green text
  - [ ] IPTAL: Light gray background, gray strikeout text
- [ ] Grid standards applied
- [ ] Date and money columns formatted
- [ ] Filter by status works
- [ ] Edit only allowed for TASLAK orders

### UcSiparisTaslak (TODO - Not in Sprint 9)
- [ ] Summary panel shows correct item count
- [ ] Summary panel shows correct total amount
- [ ] Package multiple warnings shown (yellow background)
- [ ] Cannot save empty order
- [ ] Status transitions follow business rules

---

## Diagnostics Screen

### UcSYS_DIAG
- [ ] Grid standards applied
- [ ] Date column formatted
- [ ] Run All Checks executes all checks
- [ ] Run Selected Check executes only focused check
- [ ] Status colors correct:
  - [ ] OK: Green background
  - [ ] WARNING: Yellow background
  - [ ] FAIL: Red background
- [ ] Export creates .txt file
- [ ] Copy to Clipboard works
- [ ] MessageHelper used for all messages

**TODO (Not in Sprint 9)**:
- [ ] Status icons display (✅ ⚠️ ❌)

---

## Helper Components

### MessageHelper
- [ ] ShowSuccess displays with Info icon, "Başarılı" title
- [ ] ShowError displays with Error icon, "Hata" title, includes log message
- [ ] ShowWarning displays with Warning icon, "Uyarı" title
- [ ] ShowConfirmation displays with Question icon, Yes/No buttons
- [ ] ShowInfo displays with Info icon, "Bilgi" title

### GridHelper
- [ ] StandardColors accessible and used consistently
- [ ] ApplyStandardFormatting applied to all grids
- [ ] Column formatters work correctly (ID, Date, Money, Quantity)

### IconHelper
- [ ] Icons generate correctly (kritik, acil, normal, info, pasif)
- [ ] Icons cache for performance
- [ ] GetStockStatusIcon returns correct icon based on stock levels
- [ ] GetOrderStatusIcon returns correct icon based on order status

### EmptyStatePanel
- [ ] Shows centered icon and message
- [ ] Action button triggers correct event
- [ ] Displays when grid is empty

---

## Performance

### Grid Operations
- [ ] All grids use BeginUpdate/EndUpdate
- [ ] Grid loading doesn't freeze UI
- [ ] Filter operations are responsive (< 100ms)
- [ ] Sort operations are responsive (< 100ms)

### Data Operations
- [ ] Product list loads in < 500ms
- [ ] Order list loads in < 500ms
- [ ] Critical stock loads in < 300ms
- [ ] Dashboard widgets load in < 500ms each

### Memory
- [ ] No memory leaks after 30 minutes of use
- [ ] Memory stable when opening/closing screens repeatedly

---

## Keyboard Navigation

### List Screens
- [ ] F2 creates new record
- [ ] F3 edits selected record
- [ ] F5 refreshes list
- [ ] F8 deletes/deactivates selected record
- [ ] Enter opens selected record
- [ ] Ctrl+F focuses search box (if applicable)

### Edit Screens
- [ ] Tab order logical (top to bottom, left to right)
- [ ] Enter saves form (if applicable)
- [ ] Escape closes form (if applicable)

---

## Error Handling

### User Errors
- [ ] Validation messages clear and helpful
- [ ] Required field validation works
- [ ] Invalid data format validation works
- [ ] Foreign key validation works

### System Errors
- [ ] Database connection errors handled gracefully
- [ ] SP execution errors logged and message shown
- [ ] File system errors handled (image upload)
- [ ] Network errors handled

---

## Regression Testing

### Sprint 1-8 Features
- [ ] User login works
- [ ] Security permissions enforced
- [ ] Product CRUD operations work
- [ ] Stock movement tracking works
- [ ] Order management works
- [ ] Notification system works
- [ ] Image upload/management works

---

## Browser/Environment Testing

### Operating Systems
- [ ] Windows 10
- [ ] Windows 11
- [ ] Windows Server 2019 (if applicable)

### Screen Resolutions
- [ ] 1920x1080 (Full HD)
- [ ] 1366x768 (HD)
- [ ] 2560x1440 (QHD)

### DPI Scaling
- [ ] 100% (default)
- [ ] 125%
- [ ] 150%

---

## Deployment Testing

### Installation
- [ ] Fresh install works
- [ ] Upgrade from previous version works
- [ ] Database migration scripts run successfully
- [ ] Configuration files correct

### Demo Data
- [ ] Seed script populates correct data
- [ ] Product images load from seed
- [ ] Dashboard shows seeded data

---

## Documentation Verification

### User Documentation
- [ ] `ui-styleguide.md` accurate
- [ ] `ui-component-catalog.md` examples work
- [ ] `dashboard-ui.md` matches implementation
- [ ] `grid-standards.md` matches implementation
- [ ] `product-ui.md` matches implementation
- [ ] `stock-ui.md` matches implementation
- [ ] `order-ui.md` matches implementation
- [ ] `sysdiag-ui.md` matches implementation
- [ ] `ui-assets.md` accurate
- [ ] `ui-performance.md` accurate
- [ ] `uat-sprint9-ui.md` complete

### Code Comments
- [ ] All new classes have XML comments
- [ ] All public methods documented
- [ ] Complex logic has inline comments

---

## Known Issues / TODO Items

**Sprint 9 Incomplete Items** (Require Designer):
1. UcANA_DASH.Designer.cs - Dashboard widget layout
2. UcUrunListe - Thumbnail column
3. UcUrunKart - AI content tab card layout
4. UcSiparisTaslak - Summary panel

**Future Enhancements**:
- Auto-refresh dashboard
- Async image loading
- Virtual mode for large grids (> 10K rows)
- SplashScreenManager for long operations
- Status icons in UcSYS_DIAG

---

## Sign-Off

### Tester
**Name**: ___________________
**Date**: ___________________
**Result**: ⬜ Pass | ⬜ Pass with Issues | ⬜ Fail

**Issues Found**: ___________________

### Developer
**Name**: ___________________
**Date**: ___________________
**Notes**: ___________________

### Product Owner
**Name**: ___________________
**Date**: ___________________
**Approval**: ⬜ Approved | ⬜ Rejected

---

**Version**: Sprint 9
**Last Updated**: 2025-01-12
**Status**: Ready for Testing
