namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class DlgAiSablonDetay
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblSablonKod = new DevExpress.XtraEditors.LabelControl();
            this.txtSablonKod = new DevExpress.XtraEditors.TextEdit();
            this.lblSablonAdi = new DevExpress.XtraEditors.LabelControl();
            this.txtSablonAdi = new DevExpress.XtraEditors.TextEdit();
            this.lblPromptSablonu = new DevExpress.XtraEditors.LabelControl();
            this.memoPromptSablonu = new DevExpress.XtraEditors.MemoEdit();
            this.lblDegiskenler = new DevExpress.XtraEditors.LabelControl();
            this.lblAciklama = new DevExpress.XtraEditors.LabelControl();
            this.memoAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.chkAktif = new DevExpress.XtraEditors.CheckEdit();
            this.btnTamam = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtSablonKod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSablonAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoPromptSablonu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // lblSablonKod
            //
            this.lblSablonKod.Location = new System.Drawing.Point(12, 15);
            this.lblSablonKod.Name = "lblSablonKod";
            this.lblSablonKod.Size = new System.Drawing.Size(62, 13);
            this.lblSablonKod.TabIndex = 0;
            this.lblSablonKod.Text = "Şablon Kodu:";
            //
            // txtSablonKod
            //
            this.txtSablonKod.Location = new System.Drawing.Point(110, 12);
            this.txtSablonKod.Name = "txtSablonKod";
            this.txtSablonKod.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSablonKod.Properties.MaxLength = 50;
            this.txtSablonKod.Size = new System.Drawing.Size(362, 20);
            this.txtSablonKod.TabIndex = 1;
            this.txtSablonKod.EditValueChanged += new System.EventHandler(this.txtSablonKod_EditValueChanged);
            //
            // lblSablonAdi
            //
            this.lblSablonAdi.Location = new System.Drawing.Point(12, 45);
            this.lblSablonAdi.Name = "lblSablonAdi";
            this.lblSablonAdi.Size = new System.Drawing.Size(57, 13);
            this.lblSablonAdi.TabIndex = 2;
            this.lblSablonAdi.Text = "Şablon Adı:";
            //
            // txtSablonAdi
            //
            this.txtSablonAdi.Location = new System.Drawing.Point(110, 42);
            this.txtSablonAdi.Name = "txtSablonAdi";
            this.txtSablonAdi.Properties.MaxLength = 200;
            this.txtSablonAdi.Size = new System.Drawing.Size(362, 20);
            this.txtSablonAdi.TabIndex = 3;
            //
            // lblPromptSablonu
            //
            this.lblPromptSablonu.Location = new System.Drawing.Point(12, 75);
            this.lblPromptSablonu.Name = "lblPromptSablonu";
            this.lblPromptSablonu.Size = new System.Drawing.Size(78, 13);
            this.lblPromptSablonu.TabIndex = 4;
            this.lblPromptSablonu.Text = "Prompt Şablonu:";
            //
            // memoPromptSablonu
            //
            this.memoPromptSablonu.Location = new System.Drawing.Point(12, 94);
            this.memoPromptSablonu.Name = "memoPromptSablonu";
            this.memoPromptSablonu.Size = new System.Drawing.Size(460, 250);
            this.memoPromptSablonu.TabIndex = 5;
            //
            // lblDegiskenler
            //
            this.lblDegiskenler.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblDegiskenler.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblDegiskenler.Appearance.Options.UseFont = true;
            this.lblDegiskenler.Appearance.Options.UseForeColor = true;
            this.lblDegiskenler.Location = new System.Drawing.Point(12, 350);
            this.lblDegiskenler.Name = "lblDegiskenler";
            this.lblDegiskenler.Size = new System.Drawing.Size(374, 13);
            this.lblDegiskenler.TabIndex = 6;
            this.lblDegiskenler.Text = "Kullanılabilir Değişkenler: {URUN_ADI}, {KATEGORI}, {BIRIM}, {FIYAT}, {ACIKLAMA}";
            //
            // lblAciklama
            //
            this.lblAciklama.Location = new System.Drawing.Point(12, 375);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(46, 13);
            this.lblAciklama.TabIndex = 7;
            this.lblAciklama.Text = "Açıklama:";
            //
            // memoAciklama
            //
            this.memoAciklama.Location = new System.Drawing.Point(12, 394);
            this.memoAciklama.Name = "memoAciklama";
            this.memoAciklama.Properties.MaxLength = 500;
            this.memoAciklama.Size = new System.Drawing.Size(460, 80);
            this.memoAciklama.TabIndex = 8;
            //
            // chkAktif
            //
            this.chkAktif.Location = new System.Drawing.Point(12, 485);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Properties.Caption = "Aktif";
            this.chkAktif.Size = new System.Drawing.Size(75, 19);
            this.chkAktif.TabIndex = 9;
            //
            // btnTamam
            //
            this.btnTamam.Location = new System.Drawing.Point(316, 520);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(75, 28);
            this.btnTamam.TabIndex = 10;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            //
            // btnIptal
            //
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.Location = new System.Drawing.Point(397, 520);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(75, 28);
            this.btnIptal.TabIndex = 11;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            //
            // DlgAiSablonDetay
            //
            this.AcceptButton = this.btnTamam;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnTamam);
            this.Controls.Add(this.chkAktif);
            this.Controls.Add(this.memoAciklama);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.lblDegiskenler);
            this.Controls.Add(this.memoPromptSablonu);
            this.Controls.Add(this.lblPromptSablonu);
            this.Controls.Add(this.txtSablonAdi);
            this.Controls.Add(this.lblSablonAdi);
            this.Controls.Add(this.txtSablonKod);
            this.Controls.Add(this.lblSablonKod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgAiSablonDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şablon Detayı";
            this.Load += new System.EventHandler(this.DlgAiSablonDetay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSablonKod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSablonAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoPromptSablonu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblSablonKod;
        private DevExpress.XtraEditors.TextEdit txtSablonKod;
        private DevExpress.XtraEditors.LabelControl lblSablonAdi;
        private DevExpress.XtraEditors.TextEdit txtSablonAdi;
        private DevExpress.XtraEditors.LabelControl lblPromptSablonu;
        private DevExpress.XtraEditors.MemoEdit memoPromptSablonu;
        private DevExpress.XtraEditors.LabelControl lblDegiskenler;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.MemoEdit memoAciklama;
        private DevExpress.XtraEditors.CheckEdit chkAktif;
        private DevExpress.XtraEditors.SimpleButton btnTamam;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
    }
}
