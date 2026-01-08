namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class UcKullaniciYonetim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdKullanicilar = new DevExpress.XtraGrid.GridControl();
            this.gvKullanicilar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chklstRoller = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.chkAktif = new DevExpress.XtraEditors.CheckEdit();
            this.txtParola = new DevExpress.XtraEditors.TextEdit();
            this.lblParola = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtAdSoyad = new DevExpress.XtraEditors.TextEdit();
            this.lblAdSoyad = new DevExpress.XtraEditors.LabelControl();
            this.txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this.lblKullaniciAdi = new DevExpress.XtraEditors.LabelControl();
            this.lblRoller = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnParolaSifirla = new DevExpress.XtraEditors.SimpleButton();
            this.btnPasifle = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnYeni = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKullanicilar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKullanicilar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chklstRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParola.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainerControl1
            //
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grdKullanicilar);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Size = new System.Drawing.Size(1000, 600);
            this.splitContainerControl1.SplitterPosition = 550;
            this.splitContainerControl1.TabIndex = 0;
            //
            // grdKullanicilar
            //
            this.grdKullanicilar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKullanicilar.Location = new System.Drawing.Point(0, 0);
            this.grdKullanicilar.MainView = this.gvKullanicilar;
            this.grdKullanicilar.Name = "grdKullanicilar";
            this.grdKullanicilar.Size = new System.Drawing.Size(550, 600);
            this.grdKullanicilar.TabIndex = 0;
            this.grdKullanicilar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvKullanicilar});
            //
            // gvKullanicilar
            //
            this.gvKullanicilar.GridControl = this.grdKullanicilar;
            this.gvKullanicilar.Name = "gvKullanicilar";
            this.gvKullanicilar.OptionsBehavior.Editable = false;
            this.gvKullanicilar.OptionsView.ShowGroupPanel = false;
            this.gvKullanicilar.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvKullanicilar_FocusedRowChanged);
            //
            // panelControl1
            //
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(445, 550);
            this.panelControl1.TabIndex = 0;
            //
            // groupControl1
            //
            this.groupControl1.Controls.Add(this.chklstRoller);
            this.groupControl1.Controls.Add(this.lblRoller);
            this.groupControl1.Controls.Add(this.chkAktif);
            this.groupControl1.Controls.Add(this.txtParola);
            this.groupControl1.Controls.Add(this.lblParola);
            this.groupControl1.Controls.Add(this.txtEmail);
            this.groupControl1.Controls.Add(this.lblEmail);
            this.groupControl1.Controls.Add(this.txtAdSoyad);
            this.groupControl1.Controls.Add(this.lblAdSoyad);
            this.groupControl1.Controls.Add(this.txtKullaniciAdi);
            this.groupControl1.Controls.Add(this.lblKullaniciAdi);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(441, 546);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Kullanıcı Bilgileri";
            //
            // chklstRoller
            //
            this.chklstRoller.Location = new System.Drawing.Point(15, 230);
            this.chklstRoller.Name = "chklstRoller";
            this.chklstRoller.Size = new System.Drawing.Size(410, 300);
            this.chklstRoller.TabIndex = 11;
            //
            // chkAktif
            //
            this.chkAktif.Location = new System.Drawing.Point(120, 170);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Properties.Caption = "Aktif";
            this.chkAktif.Size = new System.Drawing.Size(75, 20);
            this.chkAktif.TabIndex = 9;
            //
            // txtParola
            //
            this.txtParola.Location = new System.Drawing.Point(120, 130);
            this.txtParola.Name = "txtParola";
            this.txtParola.Properties.PasswordChar = '*';
            this.txtParola.Properties.UseSystemPasswordChar = true;
            this.txtParola.Size = new System.Drawing.Size(305, 20);
            this.txtParola.TabIndex = 8;
            //
            // lblParola
            //
            this.lblParola.Location = new System.Drawing.Point(15, 133);
            this.lblParola.Name = "lblParola";
            this.lblParola.Size = new System.Drawing.Size(34, 13);
            this.lblParola.TabIndex = 7;
            this.lblParola.Text = "Parola:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(120, 100);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(305, 20);
            this.txtEmail.TabIndex = 6;
            //
            // lblEmail
            //
            this.lblEmail.Location = new System.Drawing.Point(15, 103);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(28, 13);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email:";
            //
            // txtAdSoyad
            //
            this.txtAdSoyad.Location = new System.Drawing.Point(120, 70);
            this.txtAdSoyad.Name = "txtAdSoyad";
            this.txtAdSoyad.Size = new System.Drawing.Size(305, 20);
            this.txtAdSoyad.TabIndex = 4;
            //
            // lblAdSoyad
            //
            this.lblAdSoyad.Location = new System.Drawing.Point(15, 73);
            this.lblAdSoyad.Name = "lblAdSoyad";
            this.lblAdSoyad.Size = new System.Drawing.Size(51, 13);
            this.lblAdSoyad.TabIndex = 3;
            this.lblAdSoyad.Text = "Ad Soyad:";
            //
            // txtKullaniciAdi
            //
            this.txtKullaniciAdi.Location = new System.Drawing.Point(120, 40);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(305, 20);
            this.txtKullaniciAdi.TabIndex = 2;
            //
            // lblKullaniciAdi
            //
            this.lblKullaniciAdi.Location = new System.Drawing.Point(15, 43);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(64, 13);
            this.lblKullaniciAdi.TabIndex = 1;
            this.lblKullaniciAdi.Text = "Kullanıcı Adı:";
            //
            // lblRoller
            //
            this.lblRoller.Location = new System.Drawing.Point(15, 210);
            this.lblRoller.Name = "lblRoller";
            this.lblRoller.Size = new System.Drawing.Size(33, 13);
            this.lblRoller.TabIndex = 10;
            this.lblRoller.Text = "Roller:";
            //
            // panelControl2
            //
            this.panelControl2.Controls.Add(this.btnIptal);
            this.panelControl2.Controls.Add(this.btnKaydet);
            this.panelControl2.Controls.Add(this.btnParolaSifirla);
            this.panelControl2.Controls.Add(this.btnPasifle);
            this.panelControl2.Controls.Add(this.btnDuzenle);
            this.panelControl2.Controls.Add(this.btnYeni);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 550);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(445, 50);
            this.panelControl2.TabIndex = 1;
            //
            // btnIptal
            //
            this.btnIptal.Location = new System.Drawing.Point(350, 10);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(75, 30);
            this.btnIptal.TabIndex = 5;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            //
            // btnKaydet
            //
            this.btnKaydet.Location = new System.Drawing.Point(270, 10);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 30);
            this.btnKaydet.TabIndex = 4;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnParolaSifirla
            //
            this.btnParolaSifirla.Location = new System.Drawing.Point(190, 10);
            this.btnParolaSifirla.Name = "btnParolaSifirla";
            this.btnParolaSifirla.Size = new System.Drawing.Size(75, 30);
            this.btnParolaSifirla.TabIndex = 3;
            this.btnParolaSifirla.Text = "Parola Sıfırla";
            this.btnParolaSifirla.Click += new System.EventHandler(this.btnParolaSifirla_Click);
            //
            // btnPasifle
            //
            this.btnPasifle.Location = new System.Drawing.Point(110, 10);
            this.btnPasifle.Name = "btnPasifle";
            this.btnPasifle.Size = new System.Drawing.Size(75, 30);
            this.btnPasifle.TabIndex = 2;
            this.btnPasifle.Text = "Pasifle";
            this.btnPasifle.Click += new System.EventHandler(this.btnPasifle_Click);
            //
            // btnDuzenle
            //
            this.btnDuzenle.Location = new System.Drawing.Point(85, 10);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(75, 30);
            this.btnDuzenle.TabIndex = 1;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            //
            // btnYeni
            //
            this.btnYeni.Location = new System.Drawing.Point(5, 10);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new System.Drawing.Size(75, 30);
            this.btnYeni.TabIndex = 0;
            this.btnYeni.Text = "Yeni";
            this.btnYeni.Click += new System.EventHandler(this.btnYeni_Click);
            //
            // UcKullaniciYonetim
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UcKullaniciYonetim";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKullanicilar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKullanicilar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chklstRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParola.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdKullanicilar;
        private DevExpress.XtraGrid.Views.Grid.GridView gvKullanicilar;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl chklstRoller;
        private DevExpress.XtraEditors.CheckEdit chkAktif;
        private DevExpress.XtraEditors.TextEdit txtParola;
        private DevExpress.XtraEditors.LabelControl lblParola;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl lblEmail;
        private DevExpress.XtraEditors.TextEdit txtAdSoyad;
        private DevExpress.XtraEditors.LabelControl lblAdSoyad;
        private DevExpress.XtraEditors.TextEdit txtKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl lblKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl lblRoller;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnParolaSifirla;
        private DevExpress.XtraEditors.SimpleButton btnPasifle;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnYeni;
    }
}
