# UI Test Cases - Frontend Sprint 1

**Version**: 1.0
**Date**: 2025-12-22
**Purpose**: Verify all UI functionality and integration

---

## Database Prerequisites

Before testing, ensure kul_ekran table has the following data:

```sql
-- Main screen entry
INSERT INTO kul_ekran (ekran_kod, menudeki_adi, form_adi, modul, aktif)
VALUES ('ANA_DASH', 'Ana Ekran Dashboard', 'FrmANA_DASH', 'Common', 1);

-- Verify the entry exists
SELECT * FROM kul_ekran WHERE ekran_kod = 'ANA_DASH';
```

---

## Test Environment Setup

1. **Clean Build**: Delete bin/obj folders, rebuild solution in Debug mode
2. **Database**: Ensure SQL Server connection is working (check App.config)
3. **DevExpress License**: Ensure valid DevExpress v25.1 license
4. **Logs Folder**: Will be auto-created at `bin/Debug/logs/`

---

## Test Scenarios

### TS-UI-01: Uygulama Açılış

**Objective**: Verify application starts successfully with correct theme and MDI layout

**Steps**:
1. Launch AktarOtomasyon.Forms.exe
2. Observe application window

**Expected Results**:
- [ ] Application opens without errors
- [ ] Main window appears maximized
- [ ] Office 2019 Colorful theme is applied (modern blue color scheme)
- [ ] AccordionControl menu visible on left side (200px width)
- [ ] Menu items visible: "Ana Ekran", "Ürünler", "Stok", "Siparişler", "Raporlar", "Yönetim"
- [ ] Status bar visible at bottom showing "v1.0.0.0" and "Kullanıcı: Admin"
- [ ] ANA_DASH screen automatically opens as first MDI tab

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-02: Global Exception Handling

**Objective**: Verify global exception handlers catch and log unhandled errors

**Steps**:
1. Temporarily add this code to FrmMain_Load (after line 45):
   ```csharp
   throw new Exception("Test global exception handler");
   ```
2. Launch application
3. Observe error handling

**Expected Results**:
- [ ] MessageBox appears: "Beklenmeyen bir hata oluştu. Lütfen sistem yöneticisine başvurunuz."
- [ ] Error code shown in format: yyyyMMddHHmmss
- [ ] File created: `logs/error_{yyyyMMdd}.txt`
- [ ] Log file contains: timestamp, exception type, message, stack trace
- [ ] Application does NOT crash

**Cleanup**: Remove the throw statement before continuing

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-03: kul_ekran Başlık Yükleme (Başarılı)

**Objective**: Verify form titles are loaded from kul_ekran table

**Steps**:
1. Ensure kul_ekran has entry for 'ANA_DASH' with menudeki_adi = 'Ana Ekran Dashboard'
2. Launch application
3. Observe FrmMain window title
4. Observe ANA_DASH MDI child window title

**Expected Results**:
- [ ] FrmMain title = "Ana Ekran Dashboard" (loaded from DB)
- [ ] ANA_DASH child window title = "Ana Ekran Dashboard" (loaded from DB)
- [ ] NO fallback to "ANA_DASH" text

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-04: Versiyon Loglama

**Objective**: Verify version logging to kul_ekran_log table

**Steps**:
1. Clear kul_ekran_log table:
   ```sql
   DELETE FROM kul_ekran_log WHERE ekran_kod = 'ANA_DASH';
   ```
2. Launch application
3. Check kul_ekran_log table:
   ```sql
   SELECT * FROM kul_ekran_log
   WHERE ekran_kod = 'ANA_DASH'
   ORDER BY log_id DESC;
   ```

**Expected Results**:
- [ ] New row inserted in kul_ekran_log
- [ ] ekran_kod = 'ANA_DASH'
- [ ] versiyon = application version (e.g., "1.0.0.0")
- [ ] acilis_zamani = current timestamp
- [ ] kullanici_id = NULL (not implemented yet)

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-05: kul_ekran Fallback (DB Hatası)

**Objective**: Verify form titles fall back to ekran_kod when DB fails

**Steps**:
1. Temporarily break database connection (change connection string to invalid)
2. Launch application
3. Observe form titles

**Expected Results**:
- [ ] Application does NOT crash
- [ ] FrmMain title = "Aktar Otomasyon" (fallback)
- [ ] ANA_DASH child window title = "ANA_DASH" (fallback to ekran_kod)
- [ ] Error logged to `logs/app_{yyyyMMdd}.txt`

**Cleanup**: Restore correct connection string

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-06: Menü Navigasyonu

**Objective**: Verify menu items open correct screens

**Steps**:
1. Launch application
2. Click "Ana Ekran" menu item

**Expected Results**:
- [ ] ANA_DASH screen opens (or activates if already open)
- [ ] Screen appears as MDI child tab
- [ ] Tab title = "Ana Ekran Dashboard"

**Note**: Other menu items (Ürünler, Stok, etc.) are placeholders and should show warning message "Ekran 'XXX' tanımlı değil."

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-07: MDI Tab Yönetimi (Duplicate Prevention)

**Objective**: Verify same screen cannot be opened twice

**Steps**:
1. Launch application (ANA_DASH opens automatically)
2. Click "Ana Ekran" menu item again
3. Observe tab behavior

**Expected Results**:
- [ ] No duplicate tab is created
- [ ] Existing ANA_DASH tab is activated (brought to front)
- [ ] Only ONE ANA_DASH tab visible

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-08: Dashboard Layout

**Objective**: Verify dashboard visual layout and controls

**Steps**:
1. Launch application
2. Observe ANA_DASH screen layout

**Expected Results**:
- [ ] Three GroupControls visible vertically stacked
- [ ] Group 1: "Kritik Stok Ürünler" at position (20, 20), size 730x150
- [ ] Group 2: "Bekleyen Siparişler" at position (20, 190), size 730x150
- [ ] Group 3: "Bildirimler" at position (20, 360), size 730x150
- [ ] Each group has large bold number label (36pt font)
- [ ] Each group has "Detay Gör" button on the right
- [ ] Layout is clean and professional

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-09: Dashboard Placeholder Data

**Objective**: Verify placeholder data loads correctly

**Steps**:
1. Launch application
2. Observe dashboard counters

**Expected Results**:
- [ ] "Kritik Stok Ürünler" shows: **5**
- [ ] "Bekleyen Siparişler" shows: **12**
- [ ] "Bildirimler" shows: **3**
- [ ] All numbers are in large, bold font (36pt)

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-10: Dashboard Detay Butonları

**Objective**: Verify detail buttons show correct Sprint 2 messages

**Steps**:
1. Click "Detay Gör" button in "Kritik Stok Ürünler" group
2. Observe message
3. Click OK
4. Click "Detay Gör" button in "Bekleyen Siparişler" group
5. Observe message
6. Click OK
7. Click "Detay Gör" button in "Bildirimler" group
8. Observe message

**Expected Results**:
- [ ] Button 1 shows: "Kritik stok detay ekranı Sprint 2'de eklenecek."
- [ ] Button 2 shows: "Bekleyen sipariş detay ekranı Sprint 2'de eklenecek."
- [ ] Button 3 shows: "Bildirim detay ekranı Sprint 2'de eklenecek."
- [ ] All messages are XtraMessageBox with Information icon
- [ ] Title = "Bilgi"

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-11: DevExpress Tema

**Objective**: Verify Office 2019 Colorful theme is correctly applied

**Steps**:
1. Launch application
2. Observe visual appearance of all controls

**Expected Results**:
- [ ] Modern blue color scheme (Office 2019 style)
- [ ] AccordionControl has modern appearance
- [ ] GroupControls have modern borders
- [ ] Buttons have modern flat design
- [ ] Status bar has modern appearance
- [ ] NO classic Windows XP style controls

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

### TS-UI-12: Form Size Standardı

**Objective**: Verify forms follow size standards

**Steps**:
1. Launch application
2. Check FrmMain properties:
   - WindowState should be Maximized
   - IsMdiContainer should be true
3. Close FrmMain
4. Manually open FrmANA_DASH (if possible) or check Designer code

**Expected Results**:
- [ ] FrmMain is maximized and MDI container
- [ ] FrmANA_DASH has ClientSize = 770x700 (check Designer.cs)
- [ ] FrmANA_DASH has FormBorderStyle = FixedSingle (cannot resize)
- [ ] FrmANA_DASH has AutoScroll = true (inherited from FrmBase)

**Status**: ⬜ Pass / ⬜ Fail

**Notes**: _______________________________

---

## Additional Integration Tests

### INT-01: Form Close Behavior

**Steps**:
1. Open ANA_DASH screen
2. Try to close it (click X on tab)

**Expected**:
- [ ] Form closes without confirmation (no unsaved changes)
- [ ] Tab disappears
- [ ] Can reopen from menu

**Status**: ⬜ Pass / ⬜ Fail

---

### INT-02: Multiple Screens (Placeholder Test)

**Steps**:
1. Click "Ürünler" menu group
2. Observe result

**Expected**:
- [ ] No crash
- [ ] Either opens submenu (empty) or shows message "Ekran 'XXX' tanımlı değil."

**Status**: ⬜ Pass / ⬜ Fail

---

### INT-03: Error Logging

**Steps**:
1. After running all tests, check logs folder
2. Navigate to `bin/Debug/logs/`

**Expected**:
- [ ] Folder exists
- [ ] `error_{date}.txt` exists (if TS-UI-02 was run)
- [ ] `app_{date}.txt` exists (if any warnings logged)
- [ ] Log files are readable and well-formatted

**Status**: ⬜ Pass / ⬜ Fail

---

## Success Criteria

Sprint 1 UI is considered COMPLETE when:

- [ ] All 12 primary test scenarios (TS-UI-01 through TS-UI-12) **PASS**
- [ ] All 3 integration tests (INT-01 through INT-03) **PASS**
- [ ] NO critical bugs found
- [ ] Application runs without crashes
- [ ] All deliverables committed to source control

---

## Defect Tracking

| Test ID | Defect Description | Severity | Status |
|---------|-------------------|----------|--------|
| TS-UI-XX | _Describe defect here_ | High/Med/Low | Open/Fixed |
|  |  |  |  |
|  |  |  |  |

---

## Test Execution Summary

**Tester**: _______________________
**Date**: _______________________
**Total Tests**: 15 (12 primary + 3 integration)
**Passed**: ___ / 15
**Failed**: ___ / 15
**Pass Rate**: ___ %

**Overall Status**: ⬜ PASS / ⬜ FAIL

**Notes**:
_________________________________________________________
_________________________________________________________
_________________________________________________________

---

## Next Steps After Testing

If all tests pass:
1. Commit all code to source control
2. Tag release as "Frontend-Sprint-1-Complete"
3. Update project documentation
4. Begin Sprint 2 planning

If tests fail:
1. Document all defects above
2. Prioritize by severity
3. Fix critical and high-severity defects
4. Re-run failed tests
5. Update this document with re-test results
