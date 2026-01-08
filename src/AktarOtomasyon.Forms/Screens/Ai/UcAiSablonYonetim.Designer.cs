namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class UcAiSablonYonetim
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
            this.grpSablonlar = new DevExpress.XtraEditors.GroupControl();
            this.gridSablonlar = new DevExpress.XtraGrid.GridControl();
            this.gridViewSablonlar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpIslemler = new DevExpress.XtraEditors.GroupControl();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnAktifPasif = new DevExpress.XtraEditors.SimpleButton();
            this.btnSil = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnEkle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpSablonlar)).BeginInit();
            this.grpSablonlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSablonlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSablonlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).BeginInit();
            this.grpIslemler.SuspendLayout();
            this.SuspendLayout();
            //
            // grpSablonlar
            //
            this.grpSablonlar.Controls.Add(this.gridSablonlar);
            this.grpSablonlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSablonlar.Location = new System.Drawing.Point(0, 0);
            this.grpSablonlar.Name = "grpSablonlar";
            this.grpSablonlar.Size = new System.Drawing.Size(770, 640);
            this.grpSablonlar.TabIndex = 0;
            this.grpSablonlar.Text = "Şablon Listesi";
            //
            // gridSablonlar
            //
            this.gridSablonlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSablonlar.Location = new System.Drawing.Point(2, 23);
            this.gridSablonlar.MainView = this.gridViewSablonlar;
            this.gridSablonlar.Name = "gridSablonlar";
            this.gridSablonlar.Size = new System.Drawing.Size(766, 615);
            this.gridSablonlar.TabIndex = 0;
            this.gridSablonlar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSablonlar});
            //
            // gridViewSablonlar
            //
            this.gridViewSablonlar.GridControl = this.gridSablonlar;
            this.gridViewSablonlar.Name = "gridViewSablonlar";
            this.gridViewSablonlar.OptionsView.ShowGroupPanel = false;
            this.gridViewSablonlar.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewSablonlar_FocusedRowChanged);
            this.gridViewSablonlar.DoubleClick += new System.EventHandler(this.gridViewSablonlar_DoubleClick);
            //
            // grpIslemler
            //
            this.grpIslemler.Controls.Add(this.btnYenile);
            this.grpIslemler.Controls.Add(this.btnAktifPasif);
            this.grpIslemler.Controls.Add(this.btnSil);
            this.grpIslemler.Controls.Add(this.btnDuzenle);
            this.grpIslemler.Controls.Add(this.btnEkle);
            this.grpIslemler.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpIslemler.Location = new System.Drawing.Point(0, 640);
            this.grpIslemler.Name = "grpIslemler";
            this.grpIslemler.Size = new System.Drawing.Size(770, 60);
            this.grpIslemler.TabIndex = 1;
            this.grpIslemler.Text = "İşlemler";
            //
            // btnYenile
            //
            this.btnYenile.Location = new System.Drawing.Point(410, 26);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(95, 28);
            this.btnYenile.TabIndex = 4;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            //
            // btnAktifPasif
            //
            this.btnAktifPasif.Enabled = false;
            this.btnAktifPasif.Location = new System.Drawing.Point(309, 26);
            this.btnAktifPasif.Name = "btnAktifPasif";
            this.btnAktifPasif.Size = new System.Drawing.Size(95, 28);
            this.btnAktifPasif.TabIndex = 3;
            this.btnAktifPasif.Text = "Aktif/Pasif";
            this.btnAktifPasif.Click += new System.EventHandler(this.btnAktifPasif_Click);
            //
            // btnSil
            //
            this.btnSil.Enabled = false;
            this.btnSil.Location = new System.Drawing.Point(208, 26);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(95, 28);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            //
            // btnDuzenle
            //
            this.btnDuzenle.Enabled = false;
            this.btnDuzenle.Location = new System.Drawing.Point(107, 26);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(95, 28);
            this.btnDuzenle.TabIndex = 1;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            //
            // btnEkle
            //
            this.btnEkle.Location = new System.Drawing.Point(6, 26);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(95, 28);
            this.btnEkle.TabIndex = 0;
            this.btnEkle.Text = "Yeni Şablon";
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            //
            // UcAiSablonYonetim
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSablonlar);
            this.Controls.Add(this.grpIslemler);
            this.Name = "UcAiSablonYonetim";
            this.Size = new System.Drawing.Size(770, 700);
            ((System.ComponentModel.ISupportInitialize)(this.grpSablonlar)).EndInit();
            this.grpSablonlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSablonlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSablonlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).EndInit();
            this.grpIslemler.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpSablonlar;
        private DevExpress.XtraGrid.GridControl gridSablonlar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSablonlar;
        private DevExpress.XtraEditors.GroupControl grpIslemler;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.SimpleButton btnAktifPasif;
        private DevExpress.XtraEditors.SimpleButton btnSil;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnEkle;
    }
}
