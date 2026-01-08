namespace AktarOtomasyon.Forms.Screens.Test
{
    partial class UcBarkodTest
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlCenter = new DevExpress.XtraEditors.PanelControl();
            this.pnlUrunBilgi = new DevExpress.XtraEditors.GroupControl();
            this.lblKritikStok = new DevExpress.XtraEditors.LabelControl();
            this.lblMevcutStok = new DevExpress.XtraEditors.LabelControl();
            this.lblSatisFiyati = new DevExpress.XtraEditors.LabelControl();
            this.lblAlisFiyati = new DevExpress.XtraEditors.LabelControl();
            this.lblBarkodGoster = new DevExpress.XtraEditors.LabelControl();
            this.lblUrunKod = new DevExpress.XtraEditors.LabelControl();
            this.lblUrunAdi = new DevExpress.XtraEditors.LabelControl();
            this.btnSiparisTaslaginaGit = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.lblBarkod = new DevExpress.XtraEditors.LabelControl();
            this.txtBarkod = new DevExpress.XtraEditors.TextEdit();
            this.btnSimulate = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).BeginInit();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUrunBilgi)).BeginInit();
            this.pnlUrunBilgi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarkod.Properties)).BeginInit();
            this.SuspendLayout();

            // pnlCenter
            this.pnlCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenter.Controls.Add(this.pnlUrunBilgi);
            this.pnlCenter.Controls.Add(this.btnTemizle);
            this.pnlCenter.Controls.Add(this.btnSiparisTaslaginaGit);
            this.pnlCenter.Controls.Add(this.btnSimulate);
            this.pnlCenter.Controls.Add(this.txtBarkod);
            this.pnlCenter.Controls.Add(this.lblBarkod);
            this.pnlCenter.Controls.Add(this.lblInfo);
            this.pnlCenter.Location = new System.Drawing.Point(50, 50);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(700, 500);
            this.pnlCenter.TabIndex = 0;

            // lblInfo
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Appearance.Options.UseForeColor = true;
            this.lblInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblInfo.Location = new System.Drawing.Point(20, 20);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(360, 42);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Barkod Okuyucu Simülasyonu\r\n(Test Modu)";

            // lblBarkod
            this.lblBarkod.Location = new System.Drawing.Point(20, 90);
            this.lblBarkod.Name = "lblBarkod";
            this.lblBarkod.Text = "Barkod Giriniz:";

            // txtBarkod
            this.txtBarkod.Location = new System.Drawing.Point(20, 110);
            this.txtBarkod.Name = "txtBarkod";
            this.txtBarkod.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtBarkod.Properties.Appearance.Options.UseFont = true;
            this.txtBarkod.Size = new System.Drawing.Size(360, 32);
            this.txtBarkod.TabIndex = 1;

            // btnSimulate
            this.btnSimulate.Appearance.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnSimulate.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSimulate.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSimulate.Appearance.Options.UseBackColor = true;
            this.btnSimulate.Appearance.Options.UseFont = true;
            this.btnSimulate.Appearance.Options.UseForeColor = true;
            this.btnSimulate.Location = new System.Drawing.Point(20, 160);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(320, 40);
            this.btnSimulate.TabIndex = 2;
            this.btnSimulate.Text = "BARKOD OKU";
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);

            // btnTemizle
            this.btnTemizle.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnTemizle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTemizle.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnTemizle.Appearance.Options.UseBackColor = true;
            this.btnTemizle.Appearance.Options.UseFont = true;
            this.btnTemizle.Appearance.Options.UseForeColor = true;
            this.btnTemizle.Location = new System.Drawing.Point(360, 160);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(100, 40);
            this.btnTemizle.TabIndex = 3;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);

            // btnSiparisTaslaginaGit
            this.btnSiparisTaslaginaGit.Appearance.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnSiparisTaslaginaGit.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSiparisTaslaginaGit.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSiparisTaslaginaGit.Appearance.Options.UseBackColor = true;
            this.btnSiparisTaslaginaGit.Appearance.Options.UseFont = true;
            this.btnSiparisTaslaginaGit.Appearance.Options.UseForeColor = true;
            this.btnSiparisTaslaginaGit.Enabled = false;
            this.btnSiparisTaslaginaGit.Location = new System.Drawing.Point(20, 220);
            this.btnSiparisTaslaginaGit.Name = "btnSiparisTaslaginaGit";
            this.btnSiparisTaslaginaGit.Size = new System.Drawing.Size(440, 45);
            this.btnSiparisTaslaginaGit.TabIndex = 4;
            this.btnSiparisTaslaginaGit.Text = "Sipariş Taslağına Git";
            this.btnSiparisTaslaginaGit.Click += new System.EventHandler(this.btnSiparisTaslaginaGit_Click);

            // pnlUrunBilgi
            this.pnlUrunBilgi.Controls.Add(this.lblKritikStok);
            this.pnlUrunBilgi.Controls.Add(this.lblMevcutStok);
            this.pnlUrunBilgi.Controls.Add(this.lblSatisFiyati);
            this.pnlUrunBilgi.Controls.Add(this.lblAlisFiyati);
            this.pnlUrunBilgi.Controls.Add(this.lblBarkodGoster);
            this.pnlUrunBilgi.Controls.Add(this.lblUrunKod);
            this.pnlUrunBilgi.Controls.Add(this.lblUrunAdi);
            this.pnlUrunBilgi.Location = new System.Drawing.Point(20, 280);
            this.pnlUrunBilgi.Name = "pnlUrunBilgi";
            this.pnlUrunBilgi.Size = new System.Drawing.Size(440, 200);
            this.pnlUrunBilgi.TabIndex = 5;
            this.pnlUrunBilgi.Text = "Ürün Bilgileri";
            this.pnlUrunBilgi.Visible = false;

            // lblUrunAdi
            this.lblUrunAdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUrunAdi.Appearance.Options.UseFont = true;
            this.lblUrunAdi.Location = new System.Drawing.Point(20, 30);
            this.lblUrunAdi.Name = "lblUrunAdi";
            this.lblUrunAdi.Size = new System.Drawing.Size(10, 21);
            this.lblUrunAdi.TabIndex = 0;
            this.lblUrunAdi.Text = "-";

            // lblUrunKod
            this.lblUrunKod.Location = new System.Drawing.Point(20, 60);
            this.lblUrunKod.Name = "lblUrunKod";
            this.lblUrunKod.Size = new System.Drawing.Size(50, 13);
            this.lblUrunKod.TabIndex = 1;
            this.lblUrunKod.Text = "Kod: -";

            // lblBarkodGoster
            this.lblBarkodGoster.Location = new System.Drawing.Point(20, 80);
            this.lblBarkodGoster.Name = "lblBarkodGoster";
            this.lblBarkodGoster.Size = new System.Drawing.Size(60, 13);
            this.lblBarkodGoster.TabIndex = 2;
            this.lblBarkodGoster.Text = "Barkod: -";

            // lblAlisFiyati
            this.lblAlisFiyati.Location = new System.Drawing.Point(20, 110);
            this.lblAlisFiyati.Name = "lblAlisFiyati";
            this.lblAlisFiyati.Size = new System.Drawing.Size(70, 13);
            this.lblAlisFiyati.TabIndex = 3;
            this.lblAlisFiyati.Text = "Alış Fiyatı: -";

            // lblSatisFiyati
            this.lblSatisFiyati.Location = new System.Drawing.Point(20, 130);
            this.lblSatisFiyati.Name = "lblSatisFiyati";
            this.lblSatisFiyati.Size = new System.Drawing.Size(75, 13);
            this.lblSatisFiyati.TabIndex = 4;
            this.lblSatisFiyati.Text = "Satış Fiyatı: -";

            // lblMevcutStok
            this.lblMevcutStok.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMevcutStok.Appearance.Options.UseFont = true;
            this.lblMevcutStok.Location = new System.Drawing.Point(20, 160);
            this.lblMevcutStok.Name = "lblMevcutStok";
            this.lblMevcutStok.Size = new System.Drawing.Size(80, 15);
            this.lblMevcutStok.TabIndex = 5;
            this.lblMevcutStok.Text = "Mevcut Stok: -";

            // lblKritikStok
            this.lblKritikStok.Location = new System.Drawing.Point(20, 180);
            this.lblKritikStok.Name = "lblKritikStok";
            this.lblKritikStok.Size = new System.Drawing.Size(70, 13);
            this.lblKritikStok.TabIndex = 6;
            this.lblKritikStok.Text = "Kritik Stok: -";

            this.Controls.Add(this.pnlCenter);
            this.Name = "UcBarkodTest";
            this.Size = new System.Drawing.Size(800, 600);

            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).EndInit();
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUrunBilgi)).EndInit();
            this.pnlUrunBilgi.ResumeLayout(false);
            this.pnlUrunBilgi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarkod.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.PanelControl pnlCenter;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.LabelControl lblBarkod;
        private DevExpress.XtraEditors.TextEdit txtBarkod;
        private DevExpress.XtraEditors.SimpleButton btnSimulate;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraEditors.SimpleButton btnSiparisTaslaginaGit;
        private DevExpress.XtraEditors.GroupControl pnlUrunBilgi;
        private DevExpress.XtraEditors.LabelControl lblUrunAdi;
        private DevExpress.XtraEditors.LabelControl lblUrunKod;
        private DevExpress.XtraEditors.LabelControl lblBarkodGoster;
        private DevExpress.XtraEditors.LabelControl lblAlisFiyati;
        private DevExpress.XtraEditors.LabelControl lblSatisFiyati;
        private DevExpress.XtraEditors.LabelControl lblMevcutStok;
        private DevExpress.XtraEditors.LabelControl lblKritikStok;
    }
}
