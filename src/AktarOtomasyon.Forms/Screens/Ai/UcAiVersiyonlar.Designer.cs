namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class UcAiVersiyonlar
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
            this.lblUrunBilgi = new DevExpress.XtraEditors.LabelControl();
            this.grpVersiyonListesi = new DevExpress.XtraEditors.GroupControl();
            this.gridVersions = new DevExpress.XtraGrid.GridControl();
            this.gridViewVersions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpSeciliVersiyon = new DevExpress.XtraEditors.GroupControl();
            this.memoVersiyonDetay = new DevExpress.XtraEditors.MemoEdit();
            this.grpIslemler = new DevExpress.XtraEditors.GroupControl();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnGeriYukle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpVersiyonListesi)).BeginInit();
            this.grpVersiyonListesi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSeciliVersiyon)).BeginInit();
            this.grpSeciliVersiyon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoVersiyonDetay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).BeginInit();
            this.grpIslemler.SuspendLayout();
            this.SuspendLayout();
            //
            // lblUrunBilgi
            //
            this.lblUrunBilgi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblUrunBilgi.Appearance.Options.UseFont = true;
            this.lblUrunBilgi.Location = new System.Drawing.Point(12, 12);
            this.lblUrunBilgi.Name = "lblUrunBilgi";
            this.lblUrunBilgi.Size = new System.Drawing.Size(0, 14);
            this.lblUrunBilgi.TabIndex = 0;
            //
            // grpVersiyonListesi
            //
            this.grpVersiyonListesi.Controls.Add(this.gridVersions);
            this.grpVersiyonListesi.Location = new System.Drawing.Point(0, 32);
            this.grpVersiyonListesi.Name = "grpVersiyonListesi";
            this.grpVersiyonListesi.Size = new System.Drawing.Size(900, 300);
            this.grpVersiyonListesi.TabIndex = 1;
            this.grpVersiyonListesi.Text = "Versiyon Listesi";
            //
            // gridVersions
            //
            this.gridVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVersions.Location = new System.Drawing.Point(2, 23);
            this.gridVersions.MainView = this.gridViewVersions;
            this.gridVersions.Name = "gridVersions";
            this.gridVersions.Size = new System.Drawing.Size(896, 275);
            this.gridVersions.TabIndex = 0;
            this.gridVersions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewVersions});
            //
            // gridViewVersions
            //
            this.gridViewVersions.GridControl = this.gridVersions;
            this.gridViewVersions.Name = "gridViewVersions";
            this.gridViewVersions.OptionsView.ShowGroupPanel = false;
            this.gridViewVersions.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewVersions_FocusedRowChanged);
            this.gridViewVersions.DoubleClick += new System.EventHandler(this.gridViewVersions_DoubleClick);
            //
            // grpSeciliVersiyon
            //
            this.grpSeciliVersiyon.Controls.Add(this.memoVersiyonDetay);
            this.grpSeciliVersiyon.Location = new System.Drawing.Point(0, 338);
            this.grpSeciliVersiyon.Name = "grpSeciliVersiyon";
            this.grpSeciliVersiyon.Size = new System.Drawing.Size(900, 200);
            this.grpSeciliVersiyon.TabIndex = 2;
            this.grpSeciliVersiyon.Text = "Seçili Versiyon Detayı";
            //
            // memoVersiyonDetay
            //
            this.memoVersiyonDetay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoVersiyonDetay.Location = new System.Drawing.Point(2, 23);
            this.memoVersiyonDetay.Name = "memoVersiyonDetay";
            this.memoVersiyonDetay.Properties.ReadOnly = true;
            this.memoVersiyonDetay.Size = new System.Drawing.Size(896, 175);
            this.memoVersiyonDetay.TabIndex = 0;
            //
            // grpIslemler
            //
            this.grpIslemler.Controls.Add(this.btnKapat);
            this.grpIslemler.Controls.Add(this.btnYenile);
            this.grpIslemler.Controls.Add(this.btnGeriYukle);
            this.grpIslemler.Location = new System.Drawing.Point(0, 544);
            this.grpIslemler.Name = "grpIslemler";
            this.grpIslemler.Size = new System.Drawing.Size(900, 56);
            this.grpIslemler.TabIndex = 3;
            this.grpIslemler.Text = "İşlemler";
            //
            // btnKapat
            //
            this.btnKapat.Location = new System.Drawing.Point(212, 26);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(95, 24);
            this.btnKapat.TabIndex = 2;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            //
            // btnYenile
            //
            this.btnYenile.Location = new System.Drawing.Point(111, 26);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(95, 24);
            this.btnYenile.TabIndex = 1;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            //
            // btnGeriYukle
            //
            this.btnGeriYukle.Enabled = false;
            this.btnGeriYukle.Location = new System.Drawing.Point(10, 26);
            this.btnGeriYukle.Name = "btnGeriYukle";
            this.btnGeriYukle.Size = new System.Drawing.Size(95, 24);
            this.btnGeriYukle.TabIndex = 0;
            this.btnGeriYukle.Text = "Geri Yükle";
            this.btnGeriYukle.Click += new System.EventHandler(this.btnGeriYukle_Click);
            //
            // UcAiVersiyonlar
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIslemler);
            this.Controls.Add(this.grpSeciliVersiyon);
            this.Controls.Add(this.grpVersiyonListesi);
            this.Controls.Add(this.lblUrunBilgi);
            this.Name = "UcAiVersiyonlar";
            this.Size = new System.Drawing.Size(900, 600);
            ((System.ComponentModel.ISupportInitialize)(this.grpVersiyonListesi)).EndInit();
            this.grpVersiyonListesi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSeciliVersiyon)).EndInit();
            this.grpSeciliVersiyon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoVersiyonDetay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).EndInit();
            this.grpIslemler.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblUrunBilgi;
        private DevExpress.XtraEditors.GroupControl grpVersiyonListesi;
        private DevExpress.XtraGrid.GridControl gridVersions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVersions;
        private DevExpress.XtraEditors.GroupControl grpSeciliVersiyon;
        private DevExpress.XtraEditors.MemoEdit memoVersiyonDetay;
        private DevExpress.XtraEditors.GroupControl grpIslemler;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.SimpleButton btnGeriYukle;
    }
}
