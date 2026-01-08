using System;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Common;

namespace AktarOtomasyon.Forms.Base
{
    /// <summary>
    /// Tüm formlar için temel sınıf.
    /// STANDART: max 770x700, AutoScroll = true
    /// </summary>
    public class FrmBase : Form
    {
        public string EkranKod { get; set; }

        public FrmBase()
        {
            InitializeBaseProperties();
        }

        public FrmBase(string ekranKod)
        {
            EkranKod = ekranKod;
            InitializeBaseProperties();
        }

        private void InitializeBaseProperties()
        {
            // Form boyutu standardı
            this.MaximumSize = new Size(770, 700);
            this.AutoScroll = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            // Modern form appearance
            this.BackColor = ModernTheme.Colors.Background;
            
            this.Load += FrmBase_Load;
            this.Shown += FrmBase_Shown;
        }

        private void FrmBase_Shown(object sender, EventArgs e)
        {
            // Fade-in animation effect (web-like)
            this.Opacity = 0;
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Tick += (s, args) =>
            {
                if (this.Opacity < 1.0)
                {
                    this.Opacity += 0.05;
                }
                else
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        private void FrmBase_Load(object sender, EventArgs e)
        {
            LoadEkranBilgisi();
        }

        /// <summary>
        /// kul_ekran tablosundan ekran bilgisini yükler ve versiyon loglar.
        /// </summary>
        protected virtual void LoadEkranBilgisi()
        {
            if (string.IsNullOrEmpty(EkranKod))
                return;

            try
            {
                var ekran = InterfaceFactory.KulEkran.EkranGetir(EkranKod);
                if (ekran != null)
                {
                    this.Text = ekran.MenudekiAdi;

                    // Version logging (non-blocking)
                    var versionError = InterfaceFactory.KulEkran.VersiyonLogla(
                        EkranKod,
                        CommonFunction.GetAppVersion()
                    );

                    if (versionError != null)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Version log failed: {0}", versionError));
                    }
                }
                else
                {
                    this.Text = EkranKod; // Fallback
                }
            }
            catch (Exception ex)
            {
                this.Text = EkranKod; // Fallback
                System.Diagnostics.Debug.WriteLine(string.Format("Ekran bilgisi yüklenemedi: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Form kapatılmadan önce değişiklik kontrolü.
        /// </summary>
        protected virtual bool OnayliKapat()
        {
            return true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!OnayliKapat())
            {
                e.Cancel = true;
                return;
            }

            // Fade-out animation effect (web-like) - only if not already cancelled
            if (!e.Cancel && this.Opacity > 0)
            {
                e.Cancel = true; // Prevent immediate close
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 15;
                int step = 0;
                timer.Tick += (s, args) =>
                {
                    step++;
                    if (step < 10)
                    {
                        this.Opacity = 1.0f - (step * 0.1f);
                    }
                    else
                    {
                        timer.Stop();
                        timer.Dispose();
                        this.Opacity = 0;
                        base.OnFormClosing(new FormClosingEventArgs(CloseReason.UserClosing, false));
                    }
                };
                timer.Start();
                return;
            }

            base.OnFormClosing(e);
        }
    }
}
