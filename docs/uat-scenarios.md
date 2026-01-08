# Sprint 8 Frontend - UAT Test Scenarios

## Overview
This document contains 9 minimum UAT (User Acceptance Testing) scenarios for validating Sprint 8 Frontend features before production deployment.

**Test Date**: _____________
**Tested By**: _____________
**Environment**: [ ] Dev [ ] Staging [ ] Production

---

## Scenario 1: System Diagnostics Dashboard Functionality

**Test ID**: UAT-001
**Title**: System Diagnostics Dashboard Functionality
**Priority**: CRITICAL

### Preconditions
- User has SETTINGS_MANAGE or SECURITY_MANAGE permission
- Fresh deployment environment

### Test Steps
1. Login as admin user
2. Navigate to Sistem > Sistem Durumu (SYS_DIAG)
3. Click "Tüm Kontrolleri Çalıştır"
4. Verify all checks complete
5. Verify database connectivity shows "OK"
6. Verify all path access checks show "OK" or "WARNING"
7. Click "Rapor Oluştur" and verify report generated
8. Click "Kopyala" and verify clipboard contains results

### Expected Results
- All critical checks (Database, Stored Procedures) show "OK"
- Path checks show "OK" (or "WARNING" with explanatory details)
- Report export succeeds
- Clipboard copy succeeds

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 2: Configuration Source Visibility

**Test ID**: UAT-002
**Title**: Config Source Indication in Settings Screen
**Priority**: HIGH

### Preconditions
- Set environment variable: `AI_PROVIDER=GEMINI`
- Set database setting: `STORAGE_MODE=FileSystem`
- Have App.config setting: `TEMPLATE_PATH=templates`

### Test Steps
1. Open Sistem > Sistem Ayarları (SYS_SETTINGS)
2. Locate AI_PROVIDER setting in grid
3. Verify "Kaynak" column shows "Environment"
4. Verify row is greyed out
5. Attempt to edit AI_PROVIDER
6. Verify tooltip appears explaining cannot edit
7. Locate STORAGE_MODE setting
8. Verify "Kaynak" shows "Database"
9. Verify can edit STORAGE_MODE
10. Locate TEMPLATE_PATH setting
11. Verify "Kaynak" shows "App.config"

### Expected Results
- All config sources correctly identified
- Environment/App.config sourced settings are read-only
- Database sourced settings are editable
- Tooltip explanations are clear

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 3: Template Screen Error Handling

**Test ID**: UAT-003
**Title**: Template Market Path Error Handling
**Priority**: HIGH

### Preconditions
- User has template management permission

### Test Cases

**A) Missing TEMPLATE_PATH config**
1. Remove TEMPLATE_PATH from sys_setting and App.config
2. Open Şablonlar > Şablon Marketi (TEMPLATE_MRK)
3. Verify user-friendly error message appears
4. Verify error mentions checking TEMPLATE_PATH

**B) Non-existent directory**
1. Set TEMPLATE_PATH to non-existent path
2. Open Template Market
3. Verify error message shows path and suggests creating directory

**C) Read-only directory**
1. Set TEMPLATE_PATH to read-only directory
2. Open Template Market
3. Verify error message indicates permission issue
4. Verify guidance on checking folder permissions

### Expected Results
- All error scenarios handled gracefully
- Error messages are actionable
- Screen doesn't crash
- User understands what action to take

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 4: Product Image Security

**Test ID**: UAT-004
**Title**: Product Image Path Traversal Protection
**Priority**: CRITICAL (Security)

### Preconditions
- User has product management permission
- STORAGE_MODE=FileSystem

### Test Steps
1. Open product with image
2. Verify image loads correctly
3. Manually modify database: Update urun_gorsel SET gorsel_path = '../../../windows/system32/cmd.exe' WHERE gorsel_id = X
4. Refresh product screen
5. Verify image fails to load with security exception
6. Check error logs for "Path traversal attempt detected"
7. Try with path: "../../../../test.jpg"
8. Verify blocked
9. Try with absolute path: "C:\temp\test.jpg"
10. Verify blocked

### Expected Results
- Normal images load successfully
- Path traversal attempts are blocked
- Security exceptions logged
- No sensitive files accessible
- Application doesn't crash

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 5: Template Upload with Retry

**Test ID**: UAT-005
**Title**: Template Upload Retry Mechanism
**Priority**: MEDIUM

### Preconditions
- User has template management permission
- Have a test template file (.repx or .docx)

### Test Cases

**A) Normal upload**
1. Open Template Market
2. Select a template
3. Click "Versiyon Yükle"
4. Select test file
5. Verify upload succeeds
6. Verify file appears in version list

**B) Simulated filesystem error (manual test)**
1. Lock the template directory externally
2. Attempt to upload
3. Verify retry message appears with countdown
4. Unlock directory during retry
5. Verify upload eventually succeeds

**C) Permission denied**
1. Remove write permissions from template directory
2. Attempt upload
3. Verify clear error message about permissions
4. Verify guidance provided

### Expected Results
- Normal uploads work correctly
- Retry mechanism activates on transient errors
- Permission errors clearly explained
- No silent failures

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 6: Database Backup and Restore

**Test ID**: UAT-006
**Title**: Database Backup and Restore Procedures
**Priority**: CRITICAL

### Preconditions
- SQL Server Agent running (or manual execution)
- Backup directories exist
- Test database available

### Test Steps
1. Run `db/backup/backup_full.sql` on test database
2. Verify .bak file created in backup directory
3. Verify file size > 0
4. Run `db/backup/backup_differential.sql`
5. Verify differential .bak file created
6. Insert test data into database
7. Run `db/backup/restore_full.sql`
8. Verify database restored to full backup state
9. Verify test data NOT present (restored to earlier state)
10. Verify database ONLINE and accessible
11. Check db_version table exists
12. Run: `EXEC sp_db_version_current`
13. Verify version info returned

### Expected Results
- Full backup creates valid .bak file
- Differential backup succeeds
- Restore process completes successfully
- Database returns to backup state
- All tables and SPs present
- Version history intact

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 7: Post-Deployment Smoke Test

**Test ID**: UAT-007
**Title**: Post-Deployment Smoke Test
**Priority**: CRITICAL

### Preconditions
- Fresh production deployment
- All config files in place
- Database updated

### Test Steps
1. Launch application
2. Verify splash screen appears
3. Verify login screen appears
4. Login with admin credentials
5. Verify main dashboard loads
6. Check status bar shows correct version
7. Open Sistem > Sistem Durumu (SYS_DIAG)
8. Run all diagnostic checks
9. Verify no FAIL statuses on critical checks
10. Open 5 most critical screens:
    - Ürün Yönetimi (URUN_LISTE)
    - Şablon Marketi (TEMPLATE_MRK)
    - Güvenlik Yönetimi (SEC_YONETIM)
    - Sistem Ayarları (SYS_SETTINGS)
    - Sistem Durumu (SYS_DIAG)
11. Verify each screen loads without errors
12. Perform one basic operation in each (view, search, edit)
13. Check logs directory for CRITICAL or ERROR entries
14. Verify no sensitive data in logs

### Expected Results
- Application launches successfully
- All critical screens accessible
- No CRITICAL/ERROR log entries
- Diagnostic checks pass
- Basic operations work
- No performance issues

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 8: Environment-Specific Configuration

**Test ID**: UAT-008
**Title**: Environment Configuration Validation (Dev/Staging/Prod)
**Priority**: HIGH

### Preconditions
- .env file present for target environment
- App.config.{ENV} file exists

### Test Steps
1. Verify .env file contains correct environment (ENVIRONMENT=Production)
2. Verify sensitive values in .env (AI_API_KEY, DB password)
3. Verify App.config does NOT contain sensitive values
4. Launch application
5. Open Sistem Ayarları (SYS_SETTINGS)
6. Verify environment-specific settings shown:
   - AI_PROVIDER (check matches .env)
   - STORAGE_MODE (check matches environment)
   - Log paths correct for environment
7. Verify "Kaynak" column shows "Environment" for .env values
8. Attempt to edit .env-sourced setting
9. Verify blocked with tooltip
10. Open Sistem Durumu (SYS_DIAG)
11. Verify "Environment Info" panel shows:
    - Correct AI provider
    - Correct database server (masked)
    - Correct paths for environment

### Expected Results
- Environment correctly identified
- Config values correct for target environment
- No sensitive data in App.config
- All paths resolve correctly
- Environment-specific behavior works

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Scenario 9: Error Logging and PII Redaction

**Test ID**: UAT-009
**Title**: Error Logging with PII Protection
**Priority**: HIGH

### Preconditions
- Application running
- Log directory accessible

### Test Steps
1. Trigger various errors (invalid login, missing file, etc.)
2. Open log file in logs directory
3. Verify errors are logged with timestamps
4. Verify log entries contain error context
5. Search logs for sensitive patterns:
   - Email addresses (@)
   - Phone numbers (555-)
   - Credit card patterns (4XXX-XXXX-XXXX-XXXX)
   - API keys (sk_...)
6. Verify all sensitive data is redacted/masked
7. Verify [REDACTED] appears where sensitive data was
8. Trigger a security exception (path traversal)
9. Verify logged with "SECURITY" tag
10. Verify includes sanitized error details

### Expected Results
- All errors logged correctly
- Timestamps accurate
- PII is redacted
- No sensitive data in plain text
- Security events clearly tagged
- Logs are actionable for debugging

**Pass/Fail**: [ ]
**Notes**: _______________________________________________

---

## Test Summary

**Total Scenarios**: 9
**Passed**: _____ / 9
**Failed**: _____ / 9
**Blocked**: _____ / 9

### Critical Issues Found
1. _______________________________________________
2. _______________________________________________
3. _______________________________________________

### Sign-Off

**Tested By**: _______________ Date: ______
**Reviewed By**: _______________ Date: ______
**Approved for Production**: [ ] Yes [ ] No

**Notes**:
_______________________________________________
_______________________________________________
_______________________________________________
