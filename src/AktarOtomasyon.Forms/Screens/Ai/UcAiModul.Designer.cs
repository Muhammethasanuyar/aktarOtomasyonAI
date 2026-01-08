namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class UcAiModul
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpUrunSecimi = new DevExpress.XtraEditors.GroupControl();
            this.lblUrunBilgi = new DevExpress.XtraEditors.LabelControl();
            this.lkpUrun = new DevExpress.XtraEditors.LookUpEdit();
            this.lblUrunSecim = new DevExpress.XtraEditors.LabelControl();
            this.grpIcerikUretimi = new DevExpress.XtraEditors.GroupControl();
            this.progressBarUretim = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnUret = new DevExpress.XtraEditors.SimpleButton();
            this.lkpSablon = new DevExpress.XtraEditors.LookUpEdit();
            this.lblSablon = new DevExpress.XtraEditors.LabelControl();
            this.grpOlusturulanIcerik = new DevExpress.XtraEditors.GroupControl();
            this.lblDurum = new DevExpress.XtraEditors.LabelControl();
            this.lblGuvenlikDurum = new DevExpress.XtraEditors.LabelControl();
            this.memoIcerik = new DevExpress.XtraEditors.MemoEdit();
            this.grpIslemler = new DevExpress.XtraEditors.GroupControl();
            this.btnVazgec = new DevExpress.XtraEditors.SimpleButton();
            this.btnVersiyonGecmisi = new DevExpress.XtraEditors.SimpleButton();
            this.btnOnayla = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpUrunSecimi)).BeginInit();
            this.grpUrunSecimi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUrun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIcerikUretimi)).BeginInit();
            this.grpIcerikUretimi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarUretim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSablon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOlusturulanIcerik)).BeginInit();
            this.grpOlusturulanIcerik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoIcerik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).BeginInit();
            this.grpIslemler.SuspendLayout();
            this.SuspendLayout();
            //
            // grpUrunSecimi
            //
            this.grpUrunSecimi.Controls.Add(this.lblUrunBilgi);
            this.grpUrunSecimi.Controls.Add(this.lkpUrun);
            this.grpUrunSecimi.Controls.Add(this.lblUrunSecim);
            this.grpUrunSecimi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpUrunSecimi.Location = new System.Drawing.Point(0, 0);
            this.grpUrunSecimi.Name = "grpUrunSecimi";
            this.grpUrunSecimi.Size = new System.Drawing.Size(800, 80);
            this.grpUrunSecimi.TabIndex = 0;
            this.grpUrunSecimi.Text = "Ürün Seçimi";
            //
            // lblUrunBilgi
            //
            this.lblUrunBilgi.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblUrunBilgi.Appearance.Options.UseFont = true;
            this.lblUrunBilgi.Location = new System.Drawing.Point(12, 54);
            this.lblUrunBilgi.Name = "lblUrunBilgi";
            this.lblUrunBilgi.Size = new System.Drawing.Size(0, 13);
            this.lblUrunBilgi.TabIndex = 2;
            //
            // lkpUrun
            //
            this.lkpUrun.Location = new System.Drawing.Point(80, 28);
            this.lkpUrun.Name = "lkpUrun";
            this.lkpUrun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpUrun.Size = new System.Drawing.Size(400, 20);
            this.lkpUrun.TabIndex = 1;
            this.lkpUrun.EditValueChanged += new System.EventHandler(this.lkpUrun_EditValueChanged);
            //
            // lblUrunSecim
            //
            this.lblUrunSecim.Location = new System.Drawing.Point(12, 31);
            this.lblUrunSecim.Name = "lblUrunSecim";
            this.lblUrunSecim.Size = new System.Drawing.Size(27, 13);
            this.lblUrunSecim.TabIndex = 0;
            this.lblUrunSecim.Text = "Ürün:";
            //
            // grpIcerikUretimi
            //
            this.grpIcerikUretimi.Controls.Add(this.progressBarUretim);
            this.grpIcerikUretimi.Controls.Add(this.btnUret);
            this.grpIcerikUretimi.Controls.Add(this.lkpSablon);
            this.grpIcerikUretimi.Controls.Add(this.lblSablon);
            this.grpIcerikUretimi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpIcerikUretimi.Location = new System.Drawing.Point(0, 80);
            this.grpIcerikUretimi.Name = "grpIcerikUretimi";
            this.grpIcerikUretimi.Size = new System.Drawing.Size(800, 100);
            this.grpIcerikUretimi.TabIndex = 1;
            this.grpIcerikUretimi.Text = "İçerik Üretimi";
            //
            // progressBarUretim
            //
            this.progressBarUretim.Location = new System.Drawing.Point(12, 68);
            this.progressBarUretim.Name = "progressBarUretim";
            this.progressBarUretim.Properties.ShowTitle = true;
            this.progressBarUretim.Properties.Step = 1;
            this.progressBarUretim.Properties.PercentView = false;
            this.progressBarUretim.Size = new System.Drawing.Size(776, 18);
            this.progressBarUretim.TabIndex = 3;
            this.progressBarUretim.Visible = false;
            //
            // btnUret
            //
            this.btnUret.Location = new System.Drawing.Point(486, 26);
            this.btnUret.Name = "btnUret";
            this.btnUret.Size = new System.Drawing.Size(120, 28);
            this.btnUret.TabIndex = 2;
            this.btnUret.Text = "İçerik Üret";
            this.btnUret.Click += new System.EventHandler(this.btnUret_Click);
            //
            // lkpSablon
            //
            this.lkpSablon.Location = new System.Drawing.Point(80, 30);
            this.lkpSablon.Name = "lkpSablon";
            this.lkpSablon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSablon.Size = new System.Drawing.Size(400, 20);
            this.lkpSablon.TabIndex = 1;
            //
            // lblSablon
            //
            this.lblSablon.Location = new System.Drawing.Point(12, 33);
            this.lblSablon.Name = "lblSablon";
            this.lblSablon.Size = new System.Drawing.Size(38, 13);
            this.lblSablon.TabIndex = 0;
            this.lblSablon.Text = "Şablon:";
            //
            // grpOlusturulanIcerik
            //
            this.grpOlusturulanIcerik.Controls.Add(this.lblDurum);
            this.grpOlusturulanIcerik.Controls.Add(this.lblGuvenlikDurum);
            this.grpOlusturulanIcerik.Controls.Add(this.memoIcerik);
            this.grpOlusturulanIcerik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOlusturulanIcerik.Location = new System.Drawing.Point(0, 180);
            this.grpOlusturulanIcerik.Name = "grpOlusturulanIcerik";
            this.grpOlusturulanIcerik.Size = new System.Drawing.Size(800, 410);
            this.grpOlusturulanIcerik.TabIndex = 2;
            this.grpOlusturulanIcerik.Text = "Oluşturulan İçerik";
            //
            // lblDurum
            //
            this.lblDurum.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDurum.Appearance.Options.UseFont = true;
            this.lblDurum.Location = new System.Drawing.Point(12, 385);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(0, 13);
            this.lblDurum.TabIndex = 2;
            //
            // lblGuvenlikDurum
            //
            this.lblGuvenlikDurum.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGuvenlikDurum.Appearance.Options.UseFont = true;
            this.lblGuvenlikDurum.Location = new System.Drawing.Point(12, 366);
            this.lblGuvenlikDurum.Name = "lblGuvenlikDurum";
            this.lblGuvenlikDurum.Size = new System.Drawing.Size(0, 13);
            this.lblGuvenlikDurum.TabIndex = 1;
            //
            // memoIcerik
            //
            this.memoIcerik.Location = new System.Drawing.Point(12, 28);
            this.memoIcerik.Name = "memoIcerik";
            this.memoIcerik.Size = new System.Drawing.Size(776, 330);
            this.memoIcerik.TabIndex = 0;
            this.memoIcerik.TextChanged += new System.EventHandler(this.memoIcerik_TextChanged);
            //
            // grpIslemler
            //
            this.grpIslemler.Controls.Add(this.btnVazgec);
            this.grpIslemler.Controls.Add(this.btnVersiyonGecmisi);
            this.grpIslemler.Controls.Add(this.btnOnayla);
            this.grpIslemler.Controls.Add(this.btnKaydet);
            this.grpIslemler.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpIslemler.Location = new System.Drawing.Point(0, 590);
            this.grpIslemler.Name = "grpIslemler";
            this.grpIslemler.Size = new System.Drawing.Size(800, 60);
            this.grpIslemler.TabIndex = 3;
            this.grpIslemler.Text = "İşlemler";
            //
            // btnVazgec
            //
            this.btnVazgec.Location = new System.Drawing.Point(438, 26);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(135, 28);
            this.btnVazgec.TabIndex = 3;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
            //
            // btnVersiyonGecmisi
            //
            this.btnVersiyonGecmisi.Location = new System.Drawing.Point(297, 26);
            this.btnVersiyonGecmisi.Name = "btnVersiyonGecmisi";
            this.btnVersiyonGecmisi.Size = new System.Drawing.Size(135, 28);
            this.btnVersiyonGecmisi.TabIndex = 2;
            this.btnVersiyonGecmisi.Text = "Versiyon Geçmişi";
            this.btnVersiyonGecmisi.Click += new System.EventHandler(this.btnVersiyonGecmisi_Click);
            //
            // btnOnayla
            //
            this.btnOnayla.Location = new System.Drawing.Point(156, 26);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(135, 28);
            this.btnOnayla.TabIndex = 1;
            this.btnOnayla.Text = "Onayla ve Aktif Et";
            this.btnOnayla.Click += new System.EventHandler(this.btnOnayla_Click);
            //
            // btnKaydet
            //
            this.btnKaydet.Location = new System.Drawing.Point(12, 26);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(138, 28);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "Taslak Olarak Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // UcAiModul
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpOlusturulanIcerik);
            this.Controls.Add(this.grpIslemler);
            this.Controls.Add(this.grpIcerikUretimi);
            this.Controls.Add(this.grpUrunSecimi);
            this.Name = "UcAiModul";
            this.Size = new System.Drawing.Size(800, 650);
            ((System.ComponentModel.ISupportInitialize)(this.grpUrunSecimi)).EndInit();
            this.grpUrunSecimi.ResumeLayout(false);
            this.grpUrunSecimi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUrun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIcerikUretimi)).EndInit();
            this.grpIcerikUretimi.ResumeLayout(false);
            this.grpIcerikUretimi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarUretim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSablon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOlusturulanIcerik)).EndInit();
            this.grpOlusturulanIcerik.ResumeLayout(false);
            this.grpOlusturulanIcerik.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoIcerik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).EndInit();
            this.grpIslemler.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpUrunSecimi;
        private DevExpress.XtraEditors.LabelControl lblUrunBilgi;
        private DevExpress.XtraEditors.LookUpEdit lkpUrun;
        private DevExpress.XtraEditors.LabelControl lblUrunSecim;
        private DevExpress.XtraEditors.GroupControl grpIcerikUretimi;
        private DevExpress.XtraEditors.ProgressBarControl progressBarUretim;
        private DevExpress.XtraEditors.SimpleButton btnUret;
        private DevExpress.XtraEditors.LookUpEdit lkpSablon;
        private DevExpress.XtraEditors.LabelControl lblSablon;
        private DevExpress.XtraEditors.GroupControl grpOlusturulanIcerik;
        private DevExpress.XtraEditors.LabelControl lblDurum;
        private DevExpress.XtraEditors.LabelControl lblGuvenlikDurum;
        private DevExpress.XtraEditors.MemoEdit memoIcerik;
        private DevExpress.XtraEditors.GroupControl grpIslemler;
        private DevExpress.XtraEditors.SimpleButton btnVazgec;
        private DevExpress.XtraEditors.SimpleButton btnVersiyonGecmisi;
        private DevExpress.XtraEditors.SimpleButton btnOnayla;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
    }
}
