namespace AktarOtomasyon.Forms.Screens.Common
{
    partial class DlgTedarikci
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.GroupControl grpBilgiler;
        private DevExpress.XtraEditors.LabelControl lblTedarikciKod;
        private DevExpress.XtraEditors.TextEdit txtTedarikciKod;
        private DevExpress.XtraEditors.LabelControl lblTedarikciAdi;
        private DevExpress.XtraEditors.TextEdit txtTedarikciAdi;
        private DevExpress.XtraEditors.LabelControl lblYetkili;
        private DevExpress.XtraEditors.TextEdit txtYetkili;
        private DevExpress.XtraEditors.LabelControl lblTelefon;
        private DevExpress.XtraEditors.TextEdit txtTelefon;
        private DevExpress.XtraEditors.LabelControl lblEmail;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl lblAdres;
        private DevExpress.XtraEditors.MemoEdit txtAdres;
        private DevExpress.XtraEditors.CheckEdit chkAktif;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnIptal;

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
            this.grpBilgiler = new DevExpress.XtraEditors.GroupControl();
            this.chkAktif = new DevExpress.XtraEditors.CheckEdit();
            this.txtAdres = new DevExpress.XtraEditors.MemoEdit();
            this.lblAdres = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtTelefon = new DevExpress.XtraEditors.TextEdit();
            this.lblTelefon = new DevExpress.XtraEditors.LabelControl();
            this.txtYetkili = new DevExpress.XtraEditors.TextEdit();
            this.lblYetkili = new DevExpress.XtraEditors.LabelControl();
            this.txtTedarikciAdi = new DevExpress.XtraEditors.TextEdit();
            this.lblTedarikciAdi = new DevExpress.XtraEditors.LabelControl();
            this.txtTedarikciKod = new DevExpress.XtraEditors.TextEdit();
            this.lblTedarikciKod = new DevExpress.XtraEditors.LabelControl();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpBilgiler)).BeginInit();
            this.grpBilgiler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdres.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYetkili.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTedarikciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTedarikciKod.Properties)).BeginInit();
            this.SuspendLayout();

            // grpBilgiler
            this.grpBilgiler.Controls.Add(this.chkAktif);
            this.grpBilgiler.Controls.Add(this.txtAdres);
            this.grpBilgiler.Controls.Add(this.lblAdres);
            this.grpBilgiler.Controls.Add(this.txtEmail);
            this.grpBilgiler.Controls.Add(this.lblEmail);
            this.grpBilgiler.Controls.Add(this.txtTelefon);
            this.grpBilgiler.Controls.Add(this.lblTelefon);
            this.grpBilgiler.Controls.Add(this.txtYetkili);
            this.grpBilgiler.Controls.Add(this.lblYetkili);
            this.grpBilgiler.Controls.Add(this.txtTedarikciAdi);
            this.grpBilgiler.Controls.Add(this.lblTedarikciAdi);
            this.grpBilgiler.Controls.Add(this.txtTedarikciKod);
            this.grpBilgiler.Controls.Add(this.lblTedarikciKod);
            this.grpBilgiler.Location = new System.Drawing.Point(12, 12);
            this.grpBilgiler.Name = "grpBilgiler";
            this.grpBilgiler.Size = new System.Drawing.Size(460, 320);
            this.grpBilgiler.TabIndex = 0;
            this.grpBilgiler.Text = "Tedarikçi Bilgileri";

            // lblTedarikciKod
            this.lblTedarikciKod.Location = new System.Drawing.Point(20, 35);
            this.lblTedarikciKod.Name = "lblTedarikciKod";
            this.lblTedarikciKod.Size = new System.Drawing.Size(70, 13);
            this.lblTedarikciKod.TabIndex = 0;
            this.lblTedarikciKod.Text = "Tedarikçi Kod:";

            // txtTedarikciKod
            this.txtTedarikciKod.Location = new System.Drawing.Point(120, 32);
            this.txtTedarikciKod.Name = "txtTedarikciKod";
            this.txtTedarikciKod.Size = new System.Drawing.Size(320, 20);
            this.txtTedarikciKod.TabIndex = 1;

            // lblTedarikciAdi
            this.lblTedarikciAdi.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTedarikciAdi.Appearance.Options.UseForeColor = true;
            this.lblTedarikciAdi.Location = new System.Drawing.Point(20, 65);
            this.lblTedarikciAdi.Name = "lblTedarikciAdi";
            this.lblTedarikciAdi.Size = new System.Drawing.Size(70, 13);
            this.lblTedarikciAdi.TabIndex = 2;
            this.lblTedarikciAdi.Text = "Tedarikçi Adı: *";

            // txtTedarikciAdi
            this.txtTedarikciAdi.Location = new System.Drawing.Point(120, 62);
            this.txtTedarikciAdi.Name = "txtTedarikciAdi";
            this.txtTedarikciAdi.Size = new System.Drawing.Size(320, 20);
            this.txtTedarikciAdi.TabIndex = 3;

            // lblYetkili
            this.lblYetkili.Location = new System.Drawing.Point(20, 95);
            this.lblYetkili.Name = "lblYetkili";
            this.lblYetkili.Size = new System.Drawing.Size(35, 13);
            this.lblYetkili.TabIndex = 4;
            this.lblYetkili.Text = "Yetkili:";

            // txtYetkili
            this.txtYetkili.Location = new System.Drawing.Point(120, 92);
            this.txtYetkili.Name = "txtYetkili";
            this.txtYetkili.Size = new System.Drawing.Size(320, 20);
            this.txtYetkili.TabIndex = 5;

            // lblTelefon
            this.lblTelefon.Location = new System.Drawing.Point(20, 125);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(40, 13);
            this.lblTelefon.TabIndex = 6;
            this.lblTelefon.Text = "Telefon:";

            // txtTelefon
            this.txtTelefon.Location = new System.Drawing.Point(120, 122);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(320, 20);
            this.txtTelefon.TabIndex = 7;

            // lblEmail
            this.lblEmail.Location = new System.Drawing.Point(20, 155);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(120, 152);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(320, 20);
            this.txtEmail.TabIndex = 9;

            // lblAdres
            this.lblAdres.Location = new System.Drawing.Point(20, 185);
            this.lblAdres.Name = "lblAdres";
            this.lblAdres.Size = new System.Drawing.Size(35, 13);
            this.lblAdres.TabIndex = 10;
            this.lblAdres.Text = "Adres:";

            // txtAdres
            this.txtAdres.Location = new System.Drawing.Point(120, 182);
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.Size = new System.Drawing.Size(320, 80);
            this.txtAdres.TabIndex = 11;

            // chkAktif
            this.chkAktif.Location = new System.Drawing.Point(120, 275);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Properties.Caption = "Aktif";
            this.chkAktif.Size = new System.Drawing.Size(75, 19);
            this.chkAktif.TabIndex = 12;

            // btnKaydet
            this.btnKaydet.Location = new System.Drawing.Point(316, 350);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 30);
            this.btnKaydet.TabIndex = 1;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);

            // btnIptal
            this.btnIptal.Location = new System.Drawing.Point(397, 350);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(75, 30);
            this.btnIptal.TabIndex = 2;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);

            // DlgTedarikci
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 392);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.grpBilgiler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgTedarikci";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tedarikçi";
            ((System.ComponentModel.ISupportInitialize)(this.grpBilgiler)).EndInit();
            this.grpBilgiler.ResumeLayout(false);
            this.grpBilgiler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdres.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYetkili.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTedarikciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTedarikciKod.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
