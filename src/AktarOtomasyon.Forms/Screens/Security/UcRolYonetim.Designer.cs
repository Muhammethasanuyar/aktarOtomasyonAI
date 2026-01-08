namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class UcRolYonetim
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
            this.grdRoller = new DevExpress.XtraGrid.GridControl();
            this.gvRoller = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chklstYetkiler = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lblYetkiler = new DevExpress.XtraEditors.LabelControl();
            this.chkAktif = new DevExpress.XtraEditors.CheckEdit();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.lblAciklama = new DevExpress.XtraEditors.LabelControl();
            this.txtRolAdi = new DevExpress.XtraEditors.TextEdit();
            this.lblRolAdi = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnPasifle = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnYeni = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chklstYetkiler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRolAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainerControl1
            //
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grdRoller);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Size = new System.Drawing.Size(1000, 600);
            this.splitContainerControl1.SplitterPosition = 550;
            this.splitContainerControl1.TabIndex = 0;
            //
            // grdRoller
            //
            this.grdRoller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoller.Location = new System.Drawing.Point(0, 0);
            this.grdRoller.MainView = this.gvRoller;
            this.grdRoller.Name = "grdRoller";
            this.grdRoller.Size = new System.Drawing.Size(550, 600);
            this.grdRoller.TabIndex = 0;
            this.grdRoller.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRoller});
            //
            // gvRoller
            //
            this.gvRoller.GridControl = this.grdRoller;
            this.gvRoller.Name = "gvRoller";
            this.gvRoller.OptionsBehavior.Editable = false;
            this.gvRoller.OptionsView.ShowGroupPanel = false;
            this.gvRoller.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvRoller_FocusedRowChanged);
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
            this.groupControl1.Controls.Add(this.chklstYetkiler);
            this.groupControl1.Controls.Add(this.lblYetkiler);
            this.groupControl1.Controls.Add(this.chkAktif);
            this.groupControl1.Controls.Add(this.txtAciklama);
            this.groupControl1.Controls.Add(this.lblAciklama);
            this.groupControl1.Controls.Add(this.txtRolAdi);
            this.groupControl1.Controls.Add(this.lblRolAdi);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(441, 546);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Rol Bilgileri";
            //
            // chklstYetkiler
            //
            this.chklstYetkiler.Location = new System.Drawing.Point(15, 180);
            this.chklstYetkiler.Name = "chklstYetkiler";
            this.chklstYetkiler.Size = new System.Drawing.Size(410, 350);
            this.chklstYetkiler.TabIndex = 7;
            //
            // lblYetkiler
            //
            this.lblYetkiler.Location = new System.Drawing.Point(15, 160);
            this.lblYetkiler.Name = "lblYetkiler";
            this.lblYetkiler.Size = new System.Drawing.Size(42, 13);
            this.lblYetkiler.TabIndex = 6;
            this.lblYetkiler.Text = "Yetkiler:";
            //
            // chkAktif
            //
            this.chkAktif.Location = new System.Drawing.Point(120, 130);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Properties.Caption = "Aktif";
            this.chkAktif.Size = new System.Drawing.Size(75, 20);
            this.chkAktif.TabIndex = 5;
            //
            // txtAciklama
            //
            this.txtAciklama.Location = new System.Drawing.Point(120, 70);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(305, 50);
            this.txtAciklama.TabIndex = 4;
            //
            // lblAciklama
            //
            this.lblAciklama.Location = new System.Drawing.Point(15, 73);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(48, 13);
            this.lblAciklama.TabIndex = 3;
            this.lblAciklama.Text = "Açıklama:";
            //
            // txtRolAdi
            //
            this.txtRolAdi.Location = new System.Drawing.Point(120, 40);
            this.txtRolAdi.Name = "txtRolAdi";
            this.txtRolAdi.Size = new System.Drawing.Size(305, 20);
            this.txtRolAdi.TabIndex = 2;
            //
            // lblRolAdi
            //
            this.lblRolAdi.Location = new System.Drawing.Point(15, 43);
            this.lblRolAdi.Name = "lblRolAdi";
            this.lblRolAdi.Size = new System.Drawing.Size(40, 13);
            this.lblRolAdi.TabIndex = 1;
            this.lblRolAdi.Text = "Rol Adı:";
            //
            // panelControl2
            //
            this.panelControl2.Controls.Add(this.btnIptal);
            this.panelControl2.Controls.Add(this.btnKaydet);
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
            this.btnIptal.TabIndex = 4;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            //
            // btnKaydet
            //
            this.btnKaydet.Location = new System.Drawing.Point(270, 10);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 30);
            this.btnKaydet.TabIndex = 3;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            //
            // btnPasifle
            //
            this.btnPasifle.Location = new System.Drawing.Point(165, 10);
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
            // UcRolYonetim
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UcRolYonetim";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chklstYetkiler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRolAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdRoller;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRoller;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl chklstYetkiler;
        private DevExpress.XtraEditors.LabelControl lblYetkiler;
        private DevExpress.XtraEditors.CheckEdit chkAktif;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.TextEdit txtRolAdi;
        private DevExpress.XtraEditors.LabelControl lblRolAdi;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnPasifle;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnYeni;
    }
}
