# Release Package Validation Checklist

## Sprint 8 Frontend Release

**Version**: _______
**Release Date**: _______
**Prepared By**: _______
**Target Environment**: [ ] Dev [ ] Staging [ ] Production

---

## 1. Binary Files

- [ ] AktarOtomasyon.Forms.exe present
- [ ] All .dll files present in bin/
- [ ] DevExpress assemblies present (version ____)
- [ ] System.dll, System.Windows.Forms.dll present
- [ ] No .pdb files in production build
- [ ] No .xml documentation files (unless intentional)
- [ ] File versions match release version
- [ ] Assembly version: ____________

### Verification Command
```powershell
Get-ChildItem bin\Debug\*.exe,*.dll | Select-Object Name, Length, LastWriteTime
```

---

## 2. Configuration Files

- [ ] App.config present
- [ ] App.config.Development present (template)
- [ ] App.config.Staging present (template)
- [ ] App.config.Production present (template)
- [ ] .env.example present (template file)
- [ ] .env NOT included (must be created on server)
- [ ] No sensitive data in App.config files
- [ ] Connection string uses placeholders or empty
- [ ] Comments explain required .env variables

### Required .env Variables Documented
- [ ] AI_PROVIDER
- [ ] AI_API_KEY
- [ ] STORAGE_MODE
- [ ] TEMPLATE_PATH
- [ ] MEDIA_PATH
- [ ] LOG_LEVEL
- [ ] DATABASE_CONNECTION_STRING

---

## 3. Database Scripts

### Schema Scripts
- [ ] Schema scripts in db/schema/ numbered correctly
- [ ] Latest migration number: ____
- [ ] All scripts follow naming convention: ###_sprintX_description.sql
- [ ] Each script has corresponding rollback (if applicable)

### Verification Scripts
- [ ] db/verify/verify_db_version.sql present
- [ ] db/verify/verify_tables.sql present
- [ ] db/verify/verify_stored_procs.sql present

### Backup Scripts
- [ ] db/backup/backup_full.sql present
- [ ] db/backup/backup_differential.sql present
- [ ] db/backup/backup_log.sql present
- [ ] db/backup/restore_full.sql present
- [ ] db/backup/backup_schedule.sql present

### Sprint 8 Specific Scripts
- [ ] 009_sprint8_sys_diag_seed.sql (SYS_DIAG screen metadata)
- [ ] System settings table includes ConfigSource/IsReadOnly columns
- [ ] Database version logged correctly

---

## 4. Security

### No Hardcoded Secrets
- [ ] No API keys in config files
- [ ] No passwords in config files
- [ ] No connection strings with credentials
- [ ] No encryption keys in source code
- [ ] No service account credentials

### .gitignore Configuration
- [ ] .env excluded
- [ ] *.user excluded
- [ ] bin/ and obj/ excluded
- [ ] logs/ excluded
- [ ] .vs/ excluded

### Security Fixes Included
- [ ] Product images path traversal fix (UcUrunKart.cs lines 26, 411, 498)
- [ ] GetSecureImagePath method implemented
- [ ] Path validation prevents "../" attacks
- [ ] All file operations use secure path methods

### User Secrets Documentation
- [ ] Deployment guide explains .env setup
- [ ] API key setup instructions clear
- [ ] Database credential management documented

---

## 5. Documentation

### User Documentation
- [ ] README.md up to date
- [ ] Installation instructions current
- [ ] Configuration guide complete

### Developer Documentation
- [ ] CHANGELOG.md includes this release
- [ ] Sprint 8 Frontend features documented
- [ ] Known issues listed

### Deployment Documentation
- [ ] docs/deployment-guide.md complete
- [ ] docs/uat-scenarios.md present (9 scenarios)
- [ ] docs/release-checklist.md present (this file)
- [ ] docs/backup-restore-guide.md present

### Sprint 8 Specific Documentation
- [ ] System diagnostics screen usage documented
- [ ] Configuration source indication explained
- [ ] Environment variable priority documented
- [ ] Security improvements listed

---

## 6. Testing Evidence

### Unit Tests
- [ ] Test project builds successfully
- [ ] All unit tests passing: _____ / _____
- [ ] Code coverage acceptable: _____%

### UAT Scenarios
- [ ] Scenario 1: System Diagnostics - PASSED
- [ ] Scenario 2: Config Source Visibility - PASSED
- [ ] Scenario 3: Template Error Handling - PASSED
- [ ] Scenario 4: Product Image Security - PASSED
- [ ] Scenario 5: Template Upload Retry - PASSED
- [ ] Scenario 6: Database Backup/Restore - PASSED
- [ ] Scenario 7: Post-Deployment Smoke Test - PASSED
- [ ] Scenario 8: Environment Configuration - PASSED
- [ ] Scenario 9: Error Logging & PII - PASSED

### Performance Testing
- [ ] Smoke test passed on staging
- [ ] Performance test results acceptable
- [ ] No memory leaks detected
- [ ] Startup time acceptable: _____ seconds

### Security Testing
- [ ] Security scan completed (if applicable)
- [ ] No critical vulnerabilities
- [ ] Path traversal protection verified
- [ ] PII redaction tested
- [ ] Scan report attached: [ ] Yes [ ] No

---

## 7. Deployment Prerequisites

### Environment Verification
- [ ] Target environment identified: [ ] Dev [ ] Staging [ ] Prod
- [ ] Environment-specific config files prepared
- [ ] .env file template reviewed

### Software Requirements
- [ ] .NET Framework 4.8 runtime available
- [ ] SQL Server version compatible (2016+)
- [ ] SQL Server version: ____________
- [ ] DevExpress license valid
- [ ] License expiry date: ____________

### Database Prerequisites
- [ ] Database version compatibility verified
- [ ] Current database version: ____________
- [ ] Target database version: ____________
- [ ] Migration path tested
- [ ] Rollback plan documented

### Backup Requirements
- [ ] Backup taken of current production database
- [ ] Backup file verified: ____________
- [ ] Backup file size: ____________ MB
- [ ] Backup restore tested on non-prod environment

---

## 8. Version Information

### Application Version
- [ ] Assembly version updated: ____
- [ ] File version updated: ____
- [ ] Product version updated: ____
- [ ] Version displayed in UI: [ ] Yes [ ] No

### Database Version
- [ ] Database version registered in db_version table
- [ ] Latest version number: ____
- [ ] Migration script tested

### Release Notes
- [ ] CHANGELOG entry for version ____
- [ ] Features listed
- [ ] Bug fixes listed
- [ ] Breaking changes noted
- [ ] Upgrade instructions included

### Version Control
- [ ] Git tag created: v____
- [ ] Tag pushed to remote
- [ ] Release branch created
- [ ] Merge to main/master completed

---

## 9. Sprint 8 Specific Checks

### Frontend Features
- [ ] SYS_DIAG screen registered in NavigationManager
- [ ] SYS_DIAG screen in kul_ekran table
- [ ] SYS_DIAG menu item added to FrmMain
- [ ] Config source indication working in SYS_SETTINGS
- [ ] ConfigSource column visible
- [ ] Read-only enforcement working

### Environment Management
- [ ] .env support implemented
- [ ] ConfigurationValidator in place and tested
- [ ] 3-tier config priority working (Env → DB → App.config)
- [ ] CommonFunction.GetConfigValue uses correct priority

### Error Handling
- [ ] ErrorManager PII redaction tested
- [ ] Template path validation working
- [ ] Template retry mechanism tested
- [ ] Product image security validated

### Database Security
- [ ] Database users created (aktar_app, aktar_readonly, aktar_backup)
- [ ] User permissions verified
- [ ] Least-privilege principle applied

### Logging
- [ ] Log rotation configured
- [ ] Log directory writable
- [ ] PII redaction working
- [ ] Security events logged

---

## 10. Pre-Deployment Checklist

### Final Verification
- [ ] All build warnings reviewed and acceptable
- [ ] No breaking changes for existing users
- [ ] Backward compatibility verified
- [ ] Migration path tested end-to-end

### Communication
- [ ] Stakeholders notified of deployment
- [ ] Deployment window scheduled
- [ ] Rollback plan communicated
- [ ] Support team briefed

### Deployment Package
- [ ] All files archived
- [ ] Archive checksum calculated: ____________
- [ ] Archive uploaded to deployment server
- [ ] Deployment script tested

---

## Sign-Off

### Prepared By
**Name**: _______________
**Date**: ______
**Signature**: _______________

### Reviewed By
**Name**: _______________
**Date**: ______
**Signature**: _______________

### Approved By
**Name**: _______________
**Date**: ______
**Signature**: _______________

---

## Deployment Notes

**Deployment Date/Time**: _______________
**Deployed By**: _______________
**Deployment Duration**: _____ minutes
**Issues Encountered**:
___________________________________________
___________________________________________

**Resolution**:
___________________________________________
___________________________________________

**Post-Deployment Verification**: [ ] PASS [ ] FAIL

---

## Appendix: Critical File Changes

### Sprint 8 Frontend Changes

**New Files Created**:
1. `src/AktarOtomasyon.Forms/Screens/Diagnostics/FrmSYS_DIAG.cs`
2. `src/AktarOtomasyon.Forms/Screens/Diagnostics/UcSYS_DIAG.cs`
3. `src/AktarOtomasyon.Forms/Helpers/ConfigurationValidator.cs`
4. `db/seed/009_sprint8_sys_diag_seed.sql`
5. `docs/uat-scenarios.md`
6. `docs/release-package-checklist.md`

**Modified Files** (Security Critical):
1. `src/AktarOtomasyon.Forms/Screens/Urun/UcUrunKart.cs`
   - Fixed hard-coded media path (line 26)
   - Fixed path traversal vulnerability (lines 411, 498)
   - Added GetSecureImagePath method

2. `src/AktarOtomasyon.Forms/Screens/Template/UcTemplateMrk.cs`
   - Added path validation
   - Added retry mechanism
   - Added error guidance

3. `src/AktarOtomasyon.Forms/Screens/Template/UcSystemSettings.cs`
   - Added config source indication
   - Added read-only enforcement

**Modified Files** (Infrastructure):
1. `src/AktarOtomasyon.Forms/Managers/NavigationManager.cs`
2. `src/AktarOtomasyon.Forms/FrmMain.cs`
3. `src/AktarOtomasyon.Forms/Program.cs`
4. `src/AktarOtomasyon.Util.DataAccess/SqlManager.cs`
5. `src/AktarOtomasyon.Common.Interface/Models/SystemSettingDto.cs`

---

**End of Checklist**
