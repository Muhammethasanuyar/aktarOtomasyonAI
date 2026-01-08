using System;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Urun.Interface;
using AktarOtomasyon.Stok.Interface;
using AktarOtomasyon.Siparis.Interface;
using AktarOtomasyon.Forms;

namespace AktarOtomasyon.Forms.Screens.Test
{
    public partial class UcBarkodTest : UcBase
    {
        private UrunModel _bulunanUrun = null;

        public UcBarkodTest()
        {
            InitializeComponent();
        }

        public override void LoadData()
        {
            ClearProductInfo();
            ApplyModernStyling();
            txtBarkod.Focus();
            txtBarkod.KeyDown += TxtBarkod_KeyDown;
        }

        /// <summary>
        /// Applies modern styling to all controls
        /// </summary>
        private void ApplyModernStyling()
        {
            // Modernize buttons
            ButtonHelper.ApplyPrimaryStyle(btnSimulate);
            ButtonHelper.ApplySuccessStyle(btnSiparisTaslaginaGit);
            ButtonHelper.ApplySecondaryStyle(btnTemizle);

            // Modernize panel
            ModernTheme.ApplyModernGroup(pnlUrunBilgi);

            // Modernize text input
            txtBarkod.Properties.Appearance.Font = ModernTheme.Typography.BodyLarge;
            txtBarkod.Properties.Appearance.ForeColor = ModernTheme.Colors.TextPrimary;
            txtBarkod.Properties.Appearance.Options.UseFont = true;
            txtBarkod.Properties.Appearance.Options.UseForeColor = true;
        }

        private void TxtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSimulate_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            var barkod = txtBarkod.Text.Trim();

            // Phase 2.2: Input validation
            if (string.IsNullOrWhiteSpace(barkod))
            {
                MessageHelper.ShowWarning("Lütfen bir barkod numarası girin.");
                txtBarkod.Focus();
                return;
            }

            if (barkod.Length < 8 || barkod.Length > 13)
            {
                MessageHelper.ShowWarning("Barkod numarası 8-13 karakter arasında olmalıdır.");
                txtBarkod.SelectAll();
                txtBarkod.Focus();
                return;
            }

            // Phase 4: Loading state başlat
            SetLoadingState(true);

            try
            {
                // Ürünü barkod ile bul
                ErrorManager.LogMessage($"btnSimulate_Click: Barkod aranıyor: {barkod}", "TEST");
                _bulunanUrun = InterfaceFactory.Urun.GetirBarkod(barkod);

                if (_bulunanUrun == null)
                {
                    SetLoadingState(false);
                    ErrorManager.LogMessage($"btnSimulate_Click: Ürün bulunamadı: {barkod}", "TEST");
                    MessageHelper.ShowWarning("Ürün bulunamadı: " + barkod);
                    ClearProductInfo();
                    txtBarkod.SelectAll();
                    txtBarkod.Focus();
                    return;
                }

                ErrorManager.LogMessage($"btnSimulate_Click: Ürün bulundu! Ürün ID: {_bulunanUrun.UrunId}, Adı: {_bulunanUrun.UrunAdi}", "TEST");

                // Phase 2.3: Önce eski veriyi temizle (UI'yi temizle ama _bulunanUrun'u koru)
                // ClearProductInfo() çağrılmıyor çünkü _bulunanUrun'u null yapıyor
                // Bunun yerine sadece UI'yi temizliyoruz
                if (pnlUrunBilgi != null)
                    pnlUrunBilgi.Visible = false;
                if (btnSiparisTaslaginaGit != null)
                    btnSiparisTaslaginaGit.Enabled = false;

                // Ürün bilgilerini göster
                ShowProductInfo(_bulunanUrun);
                ErrorManager.LogMessage($"btnSimulate_Click: ShowProductInfo çağrıldı. _bulunanUrun hala set: {(_bulunanUrun != null)}", "TEST");

                // Phase 4: Loading state kapat
                SetLoadingState(false);

                // Otomatik yönlendirme: Barkod okunduktan sonra direkt satış ekranına git
                // _bulunanUrun burada hala set edilmiş olmalı (yukarıda set edildi)
                ErrorManager.LogMessage($"btnSimulate_Click: NavigateToSales çağrılmadan önce _bulunanUrun kontrolü. Ürün ID: {(_bulunanUrun?.UrunId ?? -1)}, Null: {(_bulunanUrun == null)}", "TEST");
                
                if (_bulunanUrun != null)
                {
                    ErrorManager.LogMessage($"btnSimulate_Click: NavigateToSales çağrılıyor...", "TEST");
                    try
                    {
                        NavigateToSales();
                    }
                    catch (Exception navEx)
                    {
                        ErrorManager.LogMessage($"btnSimulate_Click: NavigateToSales exception: {navEx.Message}\nStack: {navEx.StackTrace}", "TEST");
                        MessageHelper.ShowError("Yönlendirme hatası: " + navEx.Message);
                    }
                }
                else
                {
                    ErrorManager.LogMessage("btnSimulate_Click: _bulunanUrun null! Ürün bilgisi kayboldu.", "TEST");
                    MessageHelper.ShowWarning("Ürün bilgisi kayboldu. Lütfen tekrar deneyin.");
                }
            }
            catch (Exception ex)
            {
                SetLoadingState(false);
                MessageHelper.ShowError("Hata: " + ex.Message);
                ErrorManager.LogMessage("BarkodTest error: " + ex.Message, "TEST");
            }
        }

        private void ShowProductInfo(UrunModel urun)
        {
            // Null kontrolü
            if (urun == null)
            {
                ClearProductInfo();
                return;
            }

            // Panel görünürlüğünü ayarla
            if (pnlUrunBilgi != null)
            {
                pnlUrunBilgi.Visible = true;
            }

            // Kontrollerin null kontrolü ile güvenli erişim
            if (lblUrunAdi != null)
                lblUrunAdi.Text = TextHelper.FixEncoding(urun.UrunAdi ?? "-");
            
            if (lblUrunKod != null)
                lblUrunKod.Text = "Kod: " + (urun.UrunKod ?? "-");
            
            if (lblBarkodGoster != null)
                lblBarkodGoster.Text = "Barkod: " + (urun.Barkod ?? "-");
            
            // Fiyat bilgileri
            if (lblAlisFiyati != null)
                lblAlisFiyati.Text = "Alış Fiyatı: " + (urun.AlisFiyati.HasValue ? string.Format("{0:C2}", urun.AlisFiyati.Value) : "-");
            
            if (lblSatisFiyati != null)
                lblSatisFiyati.Text = "Satış Fiyatı: " + (urun.SatisFiyati.HasValue ? string.Format("{0:C2}", urun.SatisFiyati.Value) : "-");

            // Stok bilgisi
            try
            {
                var stokDurum = InterfaceFactory.Stok.StokDurumGetir(urun.UrunId);
                if (stokDurum != null)
                {
                    if (lblMevcutStok != null)
                    {
                        lblMevcutStok.Text = "Mevcut Stok: " + stokDurum.MevcutStok.ToString("N2");
                        
                        // Stok durumuna göre renk
                        if (stokDurum.MevcutStok <= stokDurum.KritikStok)
                        {
                            lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                    
                    if (lblKritikStok != null)
                        lblKritikStok.Text = "Kritik Stok: " + stokDurum.KritikStok.ToString("N2");
                }
                else
                {
                    if (lblMevcutStok != null)
                        lblMevcutStok.Text = "Mevcut Stok: 0";
                    if (lblKritikStok != null)
                        lblKritikStok.Text = "Kritik Stok: -";
                }
            }
            catch
            {
                if (lblMevcutStok != null)
                    lblMevcutStok.Text = "Mevcut Stok: -";
                if (lblKritikStok != null)
                    lblKritikStok.Text = "Kritik Stok: -";
            }

            // Panel'i göster
            if (pnlUrunBilgi != null)
                pnlUrunBilgi.Visible = true;
            if (btnSiparisTaslaginaGit != null)
                btnSiparisTaslaginaGit.Enabled = true;
        }

        private void ClearProductInfo()
        {
            if (lblUrunAdi != null)
                lblUrunAdi.Text = "-";
            if (lblUrunKod != null)
                lblUrunKod.Text = "Kod: -";
            if (lblBarkodGoster != null)
                lblBarkodGoster.Text = "Barkod: -";
            if (lblAlisFiyati != null)
                lblAlisFiyati.Text = "Alış Fiyatı: -";
            if (lblSatisFiyati != null)
                lblSatisFiyati.Text = "Satış Fiyatı: -";
            if (lblMevcutStok != null)
                lblMevcutStok.Text = "Mevcut Stok: -";
            if (lblKritikStok != null)
                lblKritikStok.Text = "Kritik Stok: -";
            if (pnlUrunBilgi != null)
                pnlUrunBilgi.Visible = false;
            if (btnSiparisTaslaginaGit != null)
                btnSiparisTaslaginaGit.Enabled = false;
            // NOT: _bulunanUrun burada null yapılmıyor çünkü NavigateToSales() için gerekli
            // _bulunanUrun = null; // KALDIRILDI - Yönlendirme için gerekli
        }

        /// <summary>
        /// Phase 4: Loading durumunu gösterir/gizler
        /// </summary>
        private void SetLoadingState(bool isLoading)
        {
            if (isLoading)
            {
                btnSimulate.Enabled = false;
                btnSiparisTaslaginaGit.Enabled = false;
                btnTemizle.Enabled = false;
                txtBarkod.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                btnSimulate.Enabled = true;
                btnTemizle.Enabled = true;
                txtBarkod.Enabled = true;
                // btnSiparisTaslaginaGit only enabled if product found
                if (_bulunanUrun != null)
                {
                    btnSiparisTaslaginaGit.Enabled = true;
                }
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Barkod okunduktan sonra otomatik olarak satış ekranına yönlendirir
        /// </summary>
        private void NavigateToSales()
        {
            if (_bulunanUrun == null)
            {
                MessageHelper.ShowWarning("Lütfen önce bir ürün bulunuz.");
                ErrorManager.LogMessage("NavigateToSales: _bulunanUrun null", "TEST");
                return;
            }

            ErrorManager.LogMessage($"NavigateToSales başladı. Ürün ID: {_bulunanUrun.UrunId}, Barkod: {_bulunanUrun.Barkod}", "TEST");

            try
            {
                // Phase 4: Loading state başlat
                SetLoadingState(true);

                // SATIS_YAP ekranına yönlendir (barkod parametresi ile)
                // Önce ParentFrm'i kontrol et (UcBase'den gelen property)
                Form mdiParent = null;
                
                if (ParentFrm != null)
                {
                    ErrorManager.LogMessage($"NavigateToSales: ParentFrm bulundu: {ParentFrm.Name}", "TEST");
                    mdiParent = ParentFrm.MdiParent;
                    
                    if (mdiParent == null)
                    {
                        // Alternatif: Application.OpenForms'dan FrmMain'i bul
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form is FrmMain)
                            {
                                mdiParent = form;
                                ErrorManager.LogMessage("NavigateToSales: FrmMain Application.OpenForms'dan bulundu", "TEST");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // ParentFrm null ise Application.OpenForms'dan FrmMain'i bul
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is FrmMain)
                        {
                            mdiParent = form;
                            ErrorManager.LogMessage("NavigateToSales: FrmMain Application.OpenForms'dan bulundu (ParentFrm null)", "TEST");
                            break;
                        }
                    }
                }

                if (mdiParent == null)
                {
                    SetLoadingState(false);
                    MessageHelper.ShowError("Ana form (MDI Parent) bulunamadı. Lütfen ekranı ana pencereden açınız.");
                    ErrorManager.LogMessage("NavigateToSales: mdiParent null - FrmMain bulunamadı", "TEST");
                    return;
                }

                // Barkod numarasını parametre olarak geç
                var barkod = _bulunanUrun.Barkod ?? txtBarkod.Text.Trim();
                ErrorManager.LogMessage($"NavigateToSales: mdiParent bulundu: {mdiParent.Name}. SATIS_YAP açılıyor... Barkod: {barkod}", "TEST");

                // NavigationManager.OpenScreen çağrısı (barkod parametresi ile)
                NavigationManager.OpenScreen("SATIS_YAP", mdiParent, barkod);

                ErrorManager.LogMessage($"NavigateToSales: NavigationManager.OpenScreen çağrıldı", "TEST");

                // Phase 4: Loading state kapat
                SetLoadingState(false);

                // Phase 2.4: Başarılı işlem sonrası temizle ve focus'u geri ver
                txtBarkod.Clear();
                txtBarkod.Focus();
                
                // _bulunanUrun'u temizle (artık yönlendirme yapıldı)
                _bulunanUrun = null;
            }
            catch (Exception ex)
            {
                SetLoadingState(false);
                var errorMsg = $"Satış ekranına gitme hatası: {ex.Message}";
                MessageHelper.ShowError(errorMsg);
                ErrorManager.LogMessage($"BarkodTest NavigateToSales error: {ex.Message}\nStack Trace: {ex.StackTrace}", "TEST");
            }
        }

        private void btnSiparisTaslaginaGit_Click(object sender, EventArgs e)
        {
            // Bu buton artık kullanılmıyor - satış ekranına yönlendirme yapıyoruz
            // Ama eski kod için tutuyoruz
            NavigateToSales();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtBarkod.Text = "";
            ClearProductInfo();
            txtBarkod.Focus();
        }
    }
}
