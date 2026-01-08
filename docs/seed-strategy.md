# Sprint 9 Seed Data Strategy

## Overview

Sprint 9 introduces comprehensive demo data to eliminate the "empty screen" problem and provide a realistic working environment for testing and demonstration purposes.

## Execution Order

Execute seed scripts in the following order to respect foreign key dependencies:

```
1. 01_refdata.sql          - Reference data (categories, suppliers, units)
2. 02_products.sql         - Product catalog (~100 items)
3. 03_stock_settings.sql   - Stock thresholds and settings
4. 04_stock_movements.sql  - Stock transaction history (30-90 days)
5. 05_orders.sql           - Purchase orders (10-30 orders)
6. 06_notifications.sql    - System notifications (30-100 items)
7. 07_ai_content.sql       - AI-generated product content (20-50 products)
8. 08_images.sql           - Product image metadata (50+ images)
```

## Idempotent Design

All seed scripts use the `IF NOT EXISTS` pattern to allow safe re-execution:

```sql
IF NOT EXISTS (SELECT 1 FROM urun WHERE urun_kod = 'BAH001')
BEGIN
    INSERT INTO urun (...) VALUES (...);
    PRINT '  + BAH001 eklendi';
END
ELSE
    PRINT '  - BAH001 zaten mevcut';
```

## Key Datasets

### Reference Data (01_refdata.sql)

**Categories (12)**:
- BAHARAT (Spices)
- BITKI (Herbs)
- CAY (Tea)
- YAG (Oils)
- MACUN (Pastes)
- TOHUM (Seeds)
- KOZMETIK (Cosmetics)
- TAKVIYE (Supplements)
- KURUYEMIS (Nuts)
- BAKLIYAT (Legumes)
- BAL (Honey)
- KREM (Creams)

**Suppliers (15)**:
- Distributed across Turkish cities (Istanbul, Ankara, Izmir, Bursa, Antalya, Trabzon, Manisa, etc.)
- Complete contact information (phone, email, address)
- Coded as TED001-TED015

**Units (7)**:
- KG (Kilogram)
- GR (Gram)
- LT (Liter)
- ADET (Piece)
- PAKET (Package)
- KUTU (Box)
- ŞIŞE (Bottle)

### Products (02_products.sql)

**Distribution (~100 products)**:
- BAHARAT: 20 (Karabiber, Kimyon, Zerdeçal, Sumak, etc.)
- BITKI: 12 (Ihlamur, Papatya, Adaçayı, Melisa, etc.)
- CAY: 10 (Yeşil, Siyah, Beyaz, Mate, etc.)
- YAG: 10 (Çörek Otu, Argan, Jojoba, Badem, etc.)
- KURUYEMIS: 15 (Antep Fıstığı, Badem, Ceviz, etc.)
- BAKLIYAT: 12 (Kırmızı Mercimek, Nohut, etc.)
- MACUN: 6 (Mesir, Ginseng, etc.)
- BAL: 5 (Çam, Çiçek, Kestane, etc.)
- TOHUM: 5 (Chia, Keten, etc.)
- KOZMETIK: 5 (Sabunlar, Şampuanlar)
- TAKVIYE: 5 (Vitamin, Mineraller)
- KREM: 3 (Cilt bakım ürünleri)

### Stock Settings (03_stock_settings.sql)

**Strategic Distribution**:
- **Critical Products (20-30%)**: mevcut_stok < kritik_stok
- **Emergency Products (15-20%)**: kritik < mevcut_stok < emniyet_stok
- **Normal Products (50-60%)**: mevcut_stok >= emniyet_stok

**Settings per product**:
- min_stok: Minimum order quantity
- kritik_stok: Critical threshold (red alert)
- emniyet_stok: Safety stock (yellow alert)
- hedef_stok: Target inventory level
- siparis_miktari: Default order quantity
- tedarik_sure_gun: Lead time in days (3-14)
- paket_kati: Package multiple

### Stock Movements (04_stock_movements.sql)

**Timeline**: Today - 90 days to Today

**Movement Types**:
- GIRIS (Incoming): 60%
- CIKIS (Outgoing): 35%
- SAYIM (Inventory count): 3%
- DUZELTME (Adjustment): 2%

**Critical Scenarios**:
- 28 products forced to critical state (mevcut < kritik)
- 20 products in emergency state (kritik < mevcut < emniyet)
- ~52 products in normal state

**Implementation**:
- Manual movements for CRITICAL and EMERGENCY products
- Cursor-based batch insert for NORMAL products

### Orders (05_orders.sql)

**Order Distribution (20 total)**:
- TASLAK (Draft): 4 (20%)
- GONDERILDI (Sent): 6 (30%)
- KISMI (Partial): 5 (25%)
- TAMAMLANDI (Completed): 4 (20%)
- IPTAL (Cancelled): 1 (5%)

**Order Characteristics**:
- 3-12 line items per order
- Realistic pricing (alis_fiyati from urun table)
- KISMI orders have partial deliveries (teslim_miktar updates)
- Date distribution across last 60 days

### Notifications (06_notifications.sql)

**Notification Types (60 total)**:
- STOK_KRITIK: 24 (40%) - Critical stock alerts
- STOK_ACIL: 12 (20%) - Emergency stock alerts
- SIPARIS_TASLAK: 9 (15%) - Draft order reminders
- SIPARIS_ONAY: 6 (10%) - Order approval notifications
- SIPARIS_TESLIM: 6 (10%) - Delivery notifications
- AI_ONAY_BEKLIYOR: 3 (5%) - AI content pending approval

**Read/Unread Mix**: ~50% unread for realistic dashboard

### AI Content (07_ai_content.sql)

**Coverage (30 products)**:
- 18 products with AKTIF (approved) content (60%)
- All 30 have TASLAK (draft) versions (100%)

**Content Structure (JSON)**:
```json
{
  "fayda": "Product benefits and health properties...",
  "kullanim": "Usage instructions and recommendations...",
  "uyari": "Warnings and contraindications...",
  "kombinasyon": "Suggested combinations with other products..."
}
```

**Templates**:
- URUN_DETAY_V1: Detailed product information
- Provider: Claude

### Images (08_images.sql)

**Coverage (50 products)**:
- All products have main image (ana_gorsel = 1)
- Every 3rd product: detail_01.jpg
- Every 5th product: detail_02.jpg

**Path Pattern**: `products\{urunId}\{filename}.jpg`

**FileSystem Mode**:
- Actual images NOT in repository
- Metadata in database (urun_gorsel table)
- Physical files deployed separately via PowerShell script

## Verification

After executing all seed scripts, run:

```sql
-- Verify seed data integrity
:r db/seed/sprint9_demo_full/99_verify.sql
```

Expected output:
- 13/13 tests passed
- Product count: 50-150
- Categories: >=12
- Suppliers: >=10
- Critical stock products: >0
- Orders: 10-30
- Notifications: >=30
- AI content: >=20 products
- Images: >=50

## Reset and Re-seed

To reset demo data and start fresh:

```sql
-- Step 1: Reset demo data
:r db/ops/reset_demo.sql

-- Step 2: Re-run seed scripts (01-08)
:r db/seed/sprint9_demo_full/01_refdata.sql
:r db/seed/sprint9_demo_full/02_products.sql
-- ... continue with 03-08

-- Step 3: Verify
:r db/seed/sprint9_demo_full/99_verify.sql
```

## Best Practices

1. **Always verify** after seeding with 99_verify.sql
2. **Use reset script** for clean re-deployment
3. **Maintain idempotency** - scripts can be re-run safely
4. **Respect dependencies** - execute in correct order
5. **Check foreign keys** - ensure constraints are enabled after seeding

## Performance Considerations

- Stock movements use bulk operations where possible
- Indexes created separately (003_sprint9_indexes.sql)
- Large datasets (~100 products × 30-90 days movements) optimized with batching
- Verification script uses efficient CTEs for stock calculations

## Integration with Frontend

Seed data enables:
- Dashboard widgets show real metrics (db/sp/Dashboard/*.sql)
- Product grids have realistic data
- Stock alerts visible immediately
- Order management screens fully functional
- AI content integration testable

## Maintenance

**Adding Products**:
1. Add category if needed (01_refdata.sql)
2. Add product (02_products.sql)
3. Add stock settings (03_stock_settings.sql)
4. Add movements (04_stock_movements.sql)
5. Verify with 99_verify.sql

**Changing Distribution**:
- Adjust CRITICAL/EMERGENCY ratios in 03_stock_settings.sql
- Recalculate movements in 04_stock_movements.sql
- Maintain total ~100 products for consistency

## Troubleshooting

**Problem**: Verification fails with "Product count = 0"
- **Solution**: Check execution order, run 02_products.sql

**Problem**: Critical stock count = 0
- **Solution**: Re-run 04_stock_movements.sql with forced critical scenarios

**Problem**: Foreign key violations
- **Solution**: Verify execution order (01→02→03→04→05→06→07→08)

**Problem**: Duplicate key errors
- **Solution**: Run reset_demo.sql first, then re-seed

## Related Documentation

- `media-seed.md` - Product image deployment
- `dashboard-contract.md` - Dashboard SP contracts
- `performance.md` - Index strategy and optimization
- `demo-reset.md` - Reset procedure details
