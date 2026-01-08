namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class UcAuditViewer
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.btnAra = new DevExpress.XtraEditors.SimpleButton();
            this.spnTop = new DevExpress.XtraEditors.SpinEdit();
            this.lblTop = new DevExpress.XtraEditors.LabelControl();
            this.dteBitis = new DevExpress.XtraEditors.DateEdit();
            this.lblBitis = new DevExpress.XtraEditors.LabelControl();
            this.dteBaslangic = new DevExpress.XtraEditors.DateEdit();
            this.lblBaslangic = new DevExpress.XtraEditors.LabelControl();
            this.lkpKullanici = new DevExpress.XtraEditors.LookUpEdit();
            this.lblKullanici = new DevExpress.XtraEditors.LabelControl();
            this.cmbAction = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblAction = new DevExpress.XtraEditors.LabelControl();
            this.cmbEntity = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblEntity = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grdAuditLog = new DevExpress.XtraGrid.GridControl();
            this.gvAuditLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblKayitSayisi = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnDetayKapat = new DevExpress.XtraEditors.SimpleButton();
            this.memoJsonDetay = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnTop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBitis.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBitis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpKullanici.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuditLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoJsonDetay.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // panelControl1
            //
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1200, 130);
            this.panelControl1.TabIndex = 0;
            //
            // groupControl1
            //
            this.groupControl1.Controls.Add(this.btnExport);
            this.groupControl1.Controls.Add(this.btnTemizle);
            this.groupControl1.Controls.Add(this.btnAra);
            this.groupControl1.Controls.Add(this.spnTop);
            this.groupControl1.Controls.Add(this.lblTop);
            this.groupControl1.Controls.Add(this.dteBitis);
            this.groupControl1.Controls.Add(this.lblBitis);
            this.groupControl1.Controls.Add(this.dteBaslangic);
            this.groupControl1.Controls.Add(this.lblBaslangic);
            this.groupControl1.Controls.Add(this.lkpKullanici);
            this.groupControl1.Controls.Add(this.lblKullanici);
            this.groupControl1.Controls.Add(this.cmbAction);
            this.groupControl1.Controls.Add(this.lblAction);
            this.groupControl1.Controls.Add(this.cmbEntity);
            this.groupControl1.Controls.Add(this.lblEntity);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1196, 126);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Filtreler";
            //
            // btnExport
            //
            this.btnExport.Location = new System.Drawing.Point(690, 85);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 25);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "Excel\'e Aktar";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            //
            // btnTemizle
            //
            this.btnTemizle.Location = new System.Drawing.Point(580, 85);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(100, 25);
            this.btnTemizle.TabIndex = 13;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            //
            // btnAra
            //
            this.btnAra.Location = new System.Drawing.Point(470, 85);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(100, 25);
            this.btnAra.TabIndex = 12;
            this.btnAra.Text = "Ara";
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            //
            // spnTop
            //
            this.spnTop.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnTop.Location = new System.Drawing.Point(690, 50);
            this.spnTop.Name = "spnTop";
            this.spnTop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnTop.Size = new System.Drawing.Size(100, 20);
            this.spnTop.TabIndex = 11;
            //
            // lblTop
            //
            this.lblTop.Location = new System.Drawing.Point(615, 53);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(68, 13);
            this.lblTop.TabIndex = 10;
            this.lblTop.Text = "Maksimum N:";
            //
            // dteBitis
            //
            this.dteBitis.EditValue = null;
            this.dteBitis.Location = new System.Drawing.Point(470, 50);
            this.dteBitis.Name = "dteBitis";
            this.dteBitis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBitis.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBitis.Size = new System.Drawing.Size(130, 20);
            this.dteBitis.TabIndex = 9;
            //
            // lblBitis
            //
            this.lblBitis.Location = new System.Drawing.Point(410, 53);
            this.lblBitis.Name = "lblBitis";
            this.lblBitis.Size = new System.Drawing.Size(54, 13);
            this.lblBitis.TabIndex = 8;
            this.lblBitis.Text = "Bitiş Tarihi:";
            //
            // dteBaslangic
            //
            this.dteBaslangic.EditValue = null;
            this.dteBaslangic.Location = new System.Drawing.Point(265, 50);
            this.dteBaslangic.Name = "dteBaslangic";
            this.dteBaslangic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBaslangic.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBaslangic.Size = new System.Drawing.Size(130, 20);
            this.dteBaslangic.TabIndex = 7;
            //
            // lblBaslangic
            //
            this.lblBaslangic.Location = new System.Drawing.Point(175, 53);
            this.lblBaslangic.Name = "lblBaslangic";
            this.lblBaslangic.Size = new System.Drawing.Size(84, 13);
            this.lblBaslangic.TabIndex = 6;
            this.lblBaslangic.Text = "Başlangıç Tarihi:";
            //
            // lkpKullanici
            //
            this.lkpKullanici.Location = new System.Drawing.Point(15, 50);
            this.lkpKullanici.Name = "lkpKullanici";
            this.lkpKullanici.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpKullanici.Properties.NullText = "";
            this.lkpKullanici.Size = new System.Drawing.Size(150, 20);
            this.lkpKullanici.TabIndex = 5;
            //
            // lblKullanici
            //
            this.lblKullanici.Location = new System.Drawing.Point(15, 30);
            this.lblKullanici.Name = "lblKullanici";
            this.lblKullanici.Size = new System.Drawing.Size(42, 13);
            this.lblKullanici.TabIndex = 4;
            this.lblKullanici.Text = "Kullanıcı:";
            //
            // cmbAction
            //
            this.cmbAction.Location = new System.Drawing.Point(615, 85);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAction.Size = new System.Drawing.Size(150, 20);
            this.cmbAction.TabIndex = 3;
            this.cmbAction.Visible = false;
            //
            // lblAction
            //
            this.lblAction.Location = new System.Drawing.Point(355, 30);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(33, 13);
            this.lblAction.TabIndex = 2;
            this.lblAction.Text = "İşlem:";
            //
            // cmbEntity
            //
            this.cmbEntity.Location = new System.Drawing.Point(200, 50);
            this.cmbEntity.Name = "cmbEntity";
            this.cmbEntity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEntity.Size = new System.Drawing.Size(150, 20);
            this.cmbEntity.TabIndex = 1;
            this.cmbEntity.Visible = false;
            //
            // lblEntity
            //
            this.lblEntity.Location = new System.Drawing.Point(200, 30);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(34, 13);
            this.lblEntity.TabIndex = 0;
            this.lblEntity.Text = "Varlık:";
            //
            // splitContainerControl1
            //
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 130);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl4);
            this.splitContainerControl1.Panel2.Visible = false;
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 570);
            this.splitContainerControl1.SplitterPosition = 400;
            this.splitContainerControl1.TabIndex = 1;
            //
            // panelControl2
            //
            this.panelControl2.Controls.Add(this.grdAuditLog);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1200, 370);
            this.panelControl2.TabIndex = 0;
            //
            // grdAuditLog
            //
            this.grdAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAuditLog.Location = new System.Drawing.Point(2, 2);
            this.grdAuditLog.MainView = this.gvAuditLog;
            this.grdAuditLog.Name = "grdAuditLog";
            this.grdAuditLog.Size = new System.Drawing.Size(1196, 366);
            this.grdAuditLog.TabIndex = 0;
            this.grdAuditLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAuditLog});
            //
            // gvAuditLog
            //
            this.gvAuditLog.GridControl = this.grdAuditLog;
            this.gvAuditLog.Name = "gvAuditLog";
            this.gvAuditLog.OptionsBehavior.Editable = false;
            this.gvAuditLog.OptionsView.ShowGroupPanel = false;
            this.gvAuditLog.DoubleClick += new System.EventHandler(this.gvAuditLog_DoubleClick);
            //
            // panelControl3
            //
            this.panelControl3.Controls.Add(this.lblKayitSayisi);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 370);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1200, 30);
            this.panelControl3.TabIndex = 1;
            //
            // lblKayitSayisi
            //
            this.lblKayitSayisi.Location = new System.Drawing.Point(10, 10);
            this.lblKayitSayisi.Name = "lblKayitSayisi";
            this.lblKayitSayisi.Size = new System.Drawing.Size(74, 13);
            this.lblKayitSayisi.TabIndex = 0;
            this.lblKayitSayisi.Text = "Toplam: 0 kayıt";
            //
            // panelControl4
            //
            this.panelControl4.Controls.Add(this.groupControl2);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1200, 165);
            this.panelControl4.TabIndex = 0;
            //
            // groupControl2
            //
            this.groupControl2.Controls.Add(this.btnDetayKapat);
            this.groupControl2.Controls.Add(this.memoJsonDetay);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1196, 161);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Detay";
            //
            // btnDetayKapat
            //
            this.btnDetayKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetayKapat.Location = new System.Drawing.Point(1100, 1);
            this.btnDetayKapat.Name = "btnDetayKapat";
            this.btnDetayKapat.Size = new System.Drawing.Size(75, 23);
            this.btnDetayKapat.TabIndex = 1;
            this.btnDetayKapat.Text = "Kapat";
            this.btnDetayKapat.Click += new System.EventHandler(this.btnDetayKapat_Click);
            //
            // memoJsonDetay
            //
            this.memoJsonDetay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoJsonDetay.Location = new System.Drawing.Point(2, 21);
            this.memoJsonDetay.Name = "memoJsonDetay";
            this.memoJsonDetay.Properties.ReadOnly = true;
            this.memoJsonDetay.Size = new System.Drawing.Size(1192, 138);
            this.memoJsonDetay.TabIndex = 0;
            //
            // UcAuditViewer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "UcAuditViewer";
            this.Size = new System.Drawing.Size(1200, 700);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnTop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBitis.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBitis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBaslangic.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBaslangic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpKullanici.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAuditLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoJsonDetay.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraEditors.SimpleButton btnAra;
        private DevExpress.XtraEditors.SpinEdit spnTop;
        private DevExpress.XtraEditors.LabelControl lblTop;
        private DevExpress.XtraEditors.DateEdit dteBitis;
        private DevExpress.XtraEditors.LabelControl lblBitis;
        private DevExpress.XtraEditors.DateEdit dteBaslangic;
        private DevExpress.XtraEditors.LabelControl lblBaslangic;
        private DevExpress.XtraEditors.LookUpEdit lkpKullanici;
        private DevExpress.XtraEditors.LabelControl lblKullanici;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAction;
        private DevExpress.XtraEditors.LabelControl lblAction;
        private DevExpress.XtraEditors.ComboBoxEdit cmbEntity;
        private DevExpress.XtraEditors.LabelControl lblEntity;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdAuditLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAuditLog;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblKayitSayisi;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnDetayKapat;
        private DevExpress.XtraEditors.MemoEdit memoJsonDetay;
    }
}
