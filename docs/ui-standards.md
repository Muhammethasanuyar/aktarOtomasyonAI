# UI Standards - AktarOtomasyon

**Version**: 1.0
**Date**: 2025-12-22
**Purpose**: Define UI development standards for consistent, maintainable screen development

---

## 1. UC-Only Pattern (CRITICAL)

**RULE**: Forms are shells, UserControls contain ALL business logic.

### Form Responsibilities (Minimal)
- Inherit from `FrmBase`
- Set `EkranKod` in constructor
- Host a single UserControl (Dock=Fill)
- Call `userControl.LoadData()` in Form_Load
- Override `OnayliKapat()` to check `userControl.HasChanges()`

### UserControl Responsibilities (Complete)
- Inherit from `UcBase`
- Implement `LoadData()` - load data from services
- Implement `SaveData()` - save via services, return error string
- Implement `ClearData()` - reset form to initial state
- Implement `HasChanges()` - track if user modified data
- Handle ALL button clicks, validation, business logic

### Example

**FrmUrunDetay.cs** (Shell only):
```csharp
public partial class FrmUrunDetay : FrmBase
{
    private UcUrunDetay ucUrun;

    public FrmUrunDetay(string ekranKod) : base(ekranKod)
    {
        InitializeComponent();
    }

    private void FrmUrunDetay_Load(object sender, EventArgs e)
    {
        ucUrun.LoadData();
    }

    protected override bool OnayliKapat()
    {
        if (ucUrun.HasChanges())
        {
            var result = MessageBox.Show(
                "Kaydedilmemiş değişiklikler var. Çıkmak istediğinizden emin misiniz?",
                "Uyarı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            return result == DialogResult.Yes;
        }
        return true;
    }
}
```

**UcUrunDetay.cs** (All logic):
```csharp
public partial class UcUrunDetay : UcBase
{
    private int? _urunId = null;
    private bool _dataChanged = false;

    public override void LoadData()
    {
        // Load products from service
        var urunler = InterfaceFactory.Urun.UrunListele();
        grdUrun.DataSource = urunler;
        _dataChanged = false;
    }

    public override string SaveData()
    {
        if (!ValidateInputs())
            return "Lütfen tüm alanları doldurunuz.";

        var model = new UrunModel
        {
            UrunId = _urunId,
            UrunAdi = txtUrunAdi.Text,
            Barkod = txtBarkod.Text,
            // ... other fields
        };

        var error = InterfaceFactory.Urun.UrunKaydet(model);
        if (error == null)
        {
            _dataChanged = false;
            LoadData(); // Refresh
        }
        return error;
    }

    public override void ClearData()
    {
        _urunId = null;
        txtUrunAdi.Text = "";
        txtBarkod.Text = "";
        _dataChanged = false;
    }

    public override bool HasChanges()
    {
        return _dataChanged;
    }

    private void txtUrunAdi_TextChanged(object sender, EventArgs e)
    {
        _dataChanged = true;
    }

    private void btnKaydet_Click(object sender, EventArgs e)
    {
        DMLManager.KaydetKontrol(this);
    }
}
```

---

## 2. Form Size Standards

**RULE**: All forms must follow consistent sizing for professional appearance.

### Standard Dimensions
- **Maximum Size**: 770x700 pixels
- **StartPosition**: CenterScreen
- **FormBorderStyle**: FixedSingle (prevents resizing)
- **AutoScroll**: true (for overflow content)
- **MinimizeBox**: true
- **MaximizeBox**: false

### Designer Settings
```csharp
this.ClientSize = new System.Drawing.Size(770, 700);
this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
this.MaximizeBox = false;
this.MinimizeBox = true;
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
this.AutoScroll = true;
```

### Layout Guidelines
- Use **GroupControl** for logical sections (DevExpress)
- **Padding**: 10-20px margins between groups
- **Button Alignment**: Right-align action buttons (Kaydet, İptal, Sil)
- **Label Width**: 120-150px for consistent alignment
- **Control Spacing**: 6px vertical spacing between rows

---

## 3. Naming Conventions

**RULE**: Consistent naming improves readability and maintenance.

### Forms
- **Pattern**: `Frm{ModuleName}{Purpose}`
- **Examples**: `FrmUrunListe`, `FrmStokDetay`, `FrmSiparisOlustur`

### UserControls
- **Pattern**: `Uc{ModuleName}{Purpose}`
- **Examples**: `UcUrunListe`, `UcStokDetay`, `UcSiparisOlustur`

### Controls
| Control Type | Prefix | Example |
|--------------|--------|---------|
| GridControl | grd | `grdUrunler` |
| GridView | gv | `gvUrunler` |
| SimpleButton | btn | `btnKaydet`, `btnSil` |
| TextEdit | txt | `txtUrunAdi`, `txtBarkod` |
| ComboBoxEdit | cmb | `cmbKategori` |
| LookUpEdit | lkp | `lkpTedarikci` |
| DateEdit | dte | `dteSiparisTarihi` |
| SpinEdit | spn | `spnMiktar` |
| CheckEdit | chk | `chkAktif` |
| GroupControl | grp | `grpUrunBilgileri` |
| LabelControl | lbl | `lblUrunAdi` |

### Variables
- **Service Results**: Descriptive nouns (`urunler`, `stokHareket`, `siparis`)
- **Error Strings**: Always `error` or `{operation}Error` (`kaydetError`)
- **IDs**: `{entity}Id` (`urunId`, `stokId`)
- **Models**: `{entity}Model` or just `model` if context is clear

---

## 4. Message Display with DMLManager

**RULE**: Use DMLManager for consistent user feedback.

### DMLManager Methods

#### IslemKontrol (Generic Operation)
```csharp
private void btnIsle_Click(object sender, EventArgs e)
{
    DMLManager.IslemKontrol(
        ucMyControl,
        "İşlem başarıyla tamamlandı.",
        "İşlem Başarılı"
    );
}
```

#### KaydetKontrol (Save Operation)
```csharp
private void btnKaydet_Click(object sender, EventArgs e)
{
    DMLManager.KaydetKontrol(ucMyControl);
    // Shows "Kayıt başarıyla tamamlandı." on success
}
```

#### GuncelleKontrol (Update Operation)
```csharp
private void btnGuncelle_Click(object sender, EventArgs e)
{
    DMLManager.GuncelleKontrol(ucMyControl);
    // Shows "Güncelleme başarıyla tamamlandı." on success
}
```

#### SilKontrol (Delete with Confirmation)
```csharp
private void btnSil_Click(object sender, EventArgs e)
{
    if (DMLManager.SilmeOnayAl("Bu ürünü silmek istediğinizden emin misiniz?"))
    {
        DMLManager.SilKontrol(ucMyControl);
        // Shows "Silme işlemi başarıyla tamamlandı." on success
    }
}
```

### Error Handling Pattern
- DMLManager automatically calls `uc.SaveData()`
- If `SaveData()` returns **null**: Success message + optional callback
- If `SaveData()` returns **string**: Error MessageBox with red X icon
- **Never** show MessageBox directly - always use DMLManager

---

## 5. kul_ekran Integration

**RULE**: All screens MUST have kul_ekran entry for title and version tracking.

### Database Entry Required
```sql
INSERT INTO kul_ekran (ekran_kod, menudeki_adi, form_adi, modul, aktif)
VALUES ('URUN_LISTE', 'Ürün Listesi', 'FrmUrunListe', 'Urun', 1);
```

### Automatic Behavior (FrmBase)
1. **Constructor**: Set `EkranKod` property
   ```csharp
   public FrmUrunListe(string ekranKod) : base(ekranKod) { }
   ```

2. **OnLoad**: FrmBase automatically:
   - Calls `InterfaceFactory.KulEkran.EkranGetir(ekranKod)`
   - Sets `this.Text = ekran.MenudekiAdi` (form title from DB)
   - Logs version via `InterfaceFactory.KulEkran.VersiyonLogla(ekranKod, version)`

3. **Fallback**: If DB fails, uses `EkranKod` as form title

### Version Logging
- Tracks which user opened which screen at what time
- Uses `CommonFunction.GetAppVersion()` for version
- Non-blocking: version log failure doesn't prevent form opening

---

## 6. Navigation with NavigationManager

**RULE**: All screen navigation goes through NavigationManager.

### Opening Screens
```csharp
// From menu click:
private void accordionControlElement_UrunListe_Click(object sender, EventArgs e)
{
    NavigationManager.OpenScreen("URUN_LISTE", this);
}

// From button click:
private void btnUrunDetay_Click(object sender, EventArgs e)
{
    NavigationManager.OpenScreen("URUN_DETAY", ParentFrm);
}
```

### Automatic Behavior
- **Duplicate Prevention**: If screen already open, activates existing tab
- **MDI Management**: Forms opened as MDI children with tabs
- **Error Handling**: Shows message if ekran_kod not registered

### Registering New Screens
```csharp
// In NavigationManager.Initialize():
RegisterScreen("URUN_LISTE", typeof(Screens.Urun.FrmUrunListe));
RegisterScreen("URUN_DETAY", typeof(Screens.Urun.FrmUrunDetay));
```

**IMPORTANT**: Every new screen must be registered in NavigationManager.Initialize()

---

## 7. DevExpress Theme

**RULE**: Office 2019 Colorful theme applied globally.

### Application Startup (Program.cs)
```csharp
UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
```

### Available Themes
- Office 2019 Colorful (Default)
- Office 2019 Dark
- Visual Studio 2013 Blue
- The Bezier

### Changing Theme
Only change in `Program.cs` - affects entire application.

---

## 8. Error Handling

**RULE**: Combine global and local error handling for robustness.

### Global Exception Handling (Program.cs)
```csharp
Application.ThreadException += Application_ThreadException;
AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
{
    ErrorManager.HandleGlobalException(e.Exception, true);
}

private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    if (e.ExceptionObject is Exception ex)
        ErrorManager.HandleGlobalException(ex, false);
}
```

### Local Error Handling (Services)
```csharp
public override void LoadData()
{
    try
    {
        var urunler = InterfaceFactory.Urun.UrunListele();
        grdUrun.DataSource = urunler;
    }
    catch (Exception ex)
    {
        ErrorManager.LogMessage($"LoadData failed: {ex.Message}", "UcUrunListe");
        MessageBox.Show(
            "Veriler yüklenirken hata oluştu. Lütfen tekrar deneyiniz.",
            "Hata",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
}
```

### Error Logging
- **Global errors**: Logged to `logs/error_{yyyyMMdd}.txt`
- **Application messages**: Logged to `logs/app_{yyyyMMdd}.txt`
- **Format**: Timestamp, source, message, stack trace

---

## 9. Data Binding Best Practices

### GridControl Data Binding
```csharp
// DO:
grdUrun.DataSource = urunler;
gvUrun.BestFitColumns();

// DON'T:
// Manual row addition - use DataSource
```

### LookUpEdit Binding
```csharp
lkpKategori.Properties.DataSource = kategoriler;
lkpKategori.Properties.DisplayMember = "KategoriAdi";
lkpKategori.Properties.ValueMember = "KategoriId";
lkpKategori.Properties.NullText = "Seçiniz...";
```

### ComboBoxEdit Binding
```csharp
cmbDurum.Properties.Items.AddRange(new object[] {
    "Aktif",
    "Pasif",
    "Beklemede"
});
cmbDurum.SelectedIndex = 0;
```

---

## 10. Testing Checklist

Before committing a new screen, verify:

- [ ] Form inherits from FrmBase, UC inherits from UcBase
- [ ] EkranKod set in form constructor
- [ ] kul_ekran table entry exists
- [ ] Form size follows standards (770x700 max)
- [ ] UC implements LoadData/SaveData/ClearData/HasChanges
- [ ] DMLManager used for save/update/delete operations
- [ ] Naming conventions followed (Frm*, Uc*, grd*, btn*, etc.)
- [ ] Screen registered in NavigationManager
- [ ] No MessageBox.Show() - all via DMLManager
- [ ] Local try/catch for critical operations
- [ ] Version logged on form open (automatic via FrmBase)
- [ ] OnayliKapat() checks HasChanges()

---

## 11. Quick Reference: 5-Minute New Screen

### Step 1: Database (1 min)
```sql
INSERT INTO kul_ekran (ekran_kod, menudeki_adi, form_adi, modul, aktif)
VALUES ('MY_SCREEN', 'My Screen Title', 'FrmMyScreen', 'MyModule', 1);
```

### Step 2: Create Form (1 min)
```csharp
public partial class FrmMyScreen : FrmBase
{
    private UcMyScreen ucMyScreen;

    public FrmMyScreen(string ekranKod) : base(ekranKod)
    {
        InitializeComponent();
    }

    private void FrmMyScreen_Load(object sender, EventArgs e)
    {
        ucMyScreen.LoadData();
    }

    protected override bool OnayliKapat()
    {
        return !ucMyScreen.HasChanges() || ConfirmClose();
    }
}
```

### Step 3: Create UserControl (2 min)
```csharp
public partial class UcMyScreen : UcBase
{
    public override void LoadData() { /* Load data */ }
    public override string SaveData() { /* Save data */ }
    public override void ClearData() { /* Reset form */ }
    public override bool HasChanges() { /* Track changes */ }
}
```

### Step 4: Register (30 sec)
```csharp
// NavigationManager.Initialize():
RegisterScreen("MY_SCREEN", typeof(Screens.MyModule.FrmMyScreen));
```

### Step 5: Add Menu (30 sec)
```csharp
// FrmMain.Designer.cs: Add AccordionControlElement
// FrmMain.cs:
private void accordionControlElement_MyScreen_Click(object sender, EventArgs e)
{
    NavigationManager.OpenScreen("MY_SCREEN", this);
}
```

**Total: 5 minutes** ✅

---

## Summary

Following these standards ensures:
- **Consistency**: All screens behave the same way
- **Maintainability**: Clear separation of concerns (Form = shell, UC = logic)
- **Speed**: New screen development in ~5 minutes
- **Quality**: Built-in error handling, version tracking, user feedback
- **Professionalism**: Consistent sizing, naming, and user experience

**Questions?** See `docs/dataaccess.md` for backend patterns, `docs/decisions.md` for architecture decisions.
