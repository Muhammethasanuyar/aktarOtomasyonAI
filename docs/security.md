# Security Architecture

## Overview
Sprint 7 implements database-based authentication with PBKDF2 password hashing, role-based access control (RBAC), and comprehensive audit logging for the AktarOtomasyon system.

---

## Authentication

### Password Security (PBKDF2)

**Algorithm**: PBKDF2-HMAC-SHA1 (Rfc2898DeriveBytes)
**Standard**: NIST-approved
**Implementation**: `AktarOtomasyon.Security.Service.Helpers.PasswordHelper`

**Parameters**:
- **Salt Size**: 32 bytes (256 bits)
- **Hash Size**: 32 bytes (256 bits)
- **Iterations**: 10,000
- **Storage**: Base64 encoded

**Database Columns**:
```sql
parola_hash       NVARCHAR(512)  -- Base64 encoded PBKDF2 hash
parola_salt       NVARCHAR(256)  -- Base64 encoded salt
parola_iterasyon  INT            -- Iteration count (default 10000)
```

### Login Flow

1. User enters username + password
2. `AuthService.Login()` calls `sp_kullanici_getir_login`
3. SP returns parola_hash, parola_salt for active user
4. Service verifies password with `PasswordHelper.VerifyPassword()`
5. PBKDF2 hashes input password with stored salt
6. Constant-time comparison prevents timing attacks
7. If valid: Update son_giris_tarih, return LoginResultDto
8. If invalid: Generic error message (prevents user enumeration)

### Password Operations

**Change Password** (User):
- Requires old password verification
- User changes their own password
- Service: `AuthService.ChangePassword()`
- SP: `sp_kullanici_parola_guncelle`
- Audit: PASSWORD_CHANGE

**Reset Password** (Admin):
- No old password required
- Admin resets forgotten password
- Service: `AuthService.ResetPassword()`
- SP: `sp_kullanici_parola_sifirla`
- Audit: PASSWORD_RESET

### Security Best Practices

✅ **Constant-time comparison** for hash verification
✅ **Random salt** generated per password
✅ **Generic error messages** (prevent user enumeration)
✅ **Never log passwords** or hashes
✅ **Configurable iterations** for future-proofing

---

## Authorization (RBAC)

### Model

**Users** → **Roles** → **Permissions** → **Screens**

- Users can have multiple roles (N-N via `kullanici_rol`)
- Roles can have multiple permissions (N-N via `rol_yetki`)
- Screens require specific permissions (N-N via `ekran_yetki`)
- **Effective Permissions**: Union of all permissions from user's active roles

### Tables

```sql
kullanici       -- Users
rol             -- Roles
yetki           -- Permissions (read-only, managed via seed)
kullanici_rol   -- User-Role assignments
rol_yetki       -- Role-Permission assignments
ekran_yetki     -- Screen-Permission mappings
```

### Permission Catalog (Sprint 7)

| Code | Name | Module | Description |
|------|------|--------|-------------|
| `TEMPLATE_VIEW` | Şablon Görüntüleme | Template | View templates and versions |
| `TEMPLATE_MANAGE` | Şablon Yönetme | Template | Create, edit, delete templates + upload versions |
| `TEMPLATE_APPROVE` | Şablon Onaylama | Template | Activate template versions (approval) |
| `SETTINGS_MANAGE` | Sistem Ayarları | Settings | View and edit system settings |

### Screen Mappings

| Screen Code | Required Permissions |
|-------------|---------------------|
| `TEMPLATE_MRK` | `TEMPLATE_VIEW` |
| `SYS_SETTINGS` | `SETTINGS_MANAGE` |

### Permission Checks

**Effective Permissions** (CRITICAL SPs):

```sql
-- Get user's effective permissions (via all roles)
sp_kullanici_yetki_listele @kullanici_id

-- Check if user has specific permission
sp_kullanici_yetki_kontrol @kullanici_id, @yetki_kod
```

**Service Methods**:
```csharp
// Get all permissions for UI caching
List<YetkiDto> permissions = InterfaceFactory.Security.KullaniciYetkiListele(userId);

// Check single permission (fast, indexed)
bool hasPermission = InterfaceFactory.Security.KullaniciYetkiKontrol(userId, "TEMPLATE_MANAGE");
```

**Performance**:
- Indexed joins on `kullanici_rol`, `rol_yetki`
- EXISTS clause stops at first match
- Target: <100ms for yetki_listele, <50ms for yetki_kontrol

---

## Audit Logging

### Logged Events

| Entity | Actions |
|--------|---------|
| Kullanici | CREATE, UPDATE, DELETE, PASSWORD_CHANGE, PASSWORD_RESET |
| Rol | CREATE, UPDATE, DELETE |
| Template | CREATE, UPDATE, DELETE, UPLOAD_VERSION, ACTIVATE_VERSION, ARCHIVE_VERSION |
| Settings | UPDATE |
| Assignments | ASSIGN_ROLE, REVOKE_ROLE, ASSIGN_PERMISSION, REVOKE_PERMISSION |

### Audit Log Schema

```sql
CREATE TABLE audit_log (
    audit_id INT IDENTITY PRIMARY KEY,
    entity NVARCHAR(100) NOT NULL,      -- Table name
    entity_id INT NOT NULL,             -- Record ID
    action NVARCHAR(50) NOT NULL,       -- CREATE, UPDATE, DELETE, etc.
    detail_json NVARCHAR(MAX) NULL,     -- Optional JSON payload
    created_by INT NULL,                -- User ID
    created_at DATETIME NOT NULL        -- Timestamp
)
```

### Viewing Audit Logs

```csharp
// List with filters
var filter = new AuditListeFilterDto
{
    Entity = "Kullanici",
    Action = "PASSWORD_RESET",
    BaslangicTarih = DateTime.Now.AddDays(-30),
    Top = 100
};
var logs = InterfaceFactory.Audit.AuditListele(filter);

// Get detail with JSON
var detail = InterfaceFactory.Audit.AuditGetir(auditId);
```

---

## Service Layer

### Patterns

**Error Handling**:
- Write methods return `string` (null = success, message = error)
- Read methods return DTOs or lists
- Silent fail on read errors (return empty list/null)
- Never expose sensitive data in error messages

**Validation**:
- Required fields validated in SPs (RAISERROR)
- Service layer adds business logic validation
- Email format, password length, etc.

**Database Access**:
- Stateless services (no instance state)
- `using (var sMan = new SqlManager())` pattern
- All data access via stored procedures
- Try/catch with SqlException handling

---

## Default Admin Account

**CRITICAL SECURITY WARNING**

```
Username: admin
Password: Admin123!  (SAMPLE HASH - CHANGE IMMEDIATELY)
Role: ADMIN (all permissions)
```

**First Steps After Deployment**:
1. Login as admin with default password
2. **IMMEDIATELY** change password via UI or `AuthService.ChangePassword()`
3. Create personal admin accounts
4. Consider disabling or deleting default admin account

---

## Security Checklist

### Production Deployment

- [ ] Change default admin password
- [ ] Review and audit all users, roles, permissions
- [ ] Verify PBKDF2 parameters (10,000 iterations minimum)
- [ ] Enable audit log review procedures
- [ ] Configure role-permission assignments for all modules
- [ ] Test permission checks on all screens
- [ ] Review InterfaceFactory security service access
- [ ] Document custom permission codes
- [ ] Train administrators on user/role management
- [ ] Establish password policy (length, complexity, expiry)

### Ongoing Security

- [ ] Regular audit log reviews
- [ ] Periodic permission audits
- [ ] User access reviews (quarterly)
- [ ] Monitor failed login attempts
- [ ] Update password iteration count as needed
- [ ] Review and revoke inactive user access

---

## Sprint 7 Deliverables

**Database** (Complete):
- ✅ Schema updates (PBKDF2 columns, ekran_yetki table, indexes)
- ✅ 40+ stored procedures (Auth, User, Role, Permission, Assignment, Audit)
- ✅ Seed data (ADMIN role, 4 permissions, admin user, screen mappings)

**Backend** (Complete):
- ✅ Security.Interface project (3 interfaces, 13 DTOs)
- ✅ Security.Service project (PasswordHelper, 3 services)
- ✅ InterfaceFactory integration

**Next Phase**: Sprint 7 Frontend (Security UI screens)
- User management UI
- Role management UI
- Permission assignment UI
- Audit log viewer UI
- Login screen integration

---

## References

- **PBKDF2**: NIST SP 800-132 (Password-Based Key Derivation)
- **RBAC**: NIST RBAC Model
- **Pattern**: Microsoft .NET Security Guidelines
- **Audit**: OWASP Logging Cheat Sheet
