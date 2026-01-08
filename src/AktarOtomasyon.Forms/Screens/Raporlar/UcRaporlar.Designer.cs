namespace AktarOtomasyon.Forms.Screens.Raporlar
{
    partial class UcRaporlar
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
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.btnListele = new DevExpress.XtraEditors.SimpleButton();
            this.dtBitis = new DevExpress.XtraEditors.DateEdit();
            this.dtBaslangic = new DevExpress.XtraEditors.DateEdit();
            this.lblBitis = new DevExpress.XtraEditors.LabelControl();
            this.lblBaslangic = new DevExpress.XtraEditors.LabelControl();
            this.gcRapor = new DevExpress.XtraGrid.GridControl();
            this.gvRapor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToplamMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIslemSayisi = new DevExpress.XtraGrid.Columns.GridColumn();

            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtBitis.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBitis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRapor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRapor)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 60;
            this.pnlTop.Controls.Add(this.btnListele);
            this.pnlTop.Controls.Add(this.dtBitis);
            this.pnlTop.Controls.Add(this.dtBaslangic);
            this.pnlTop.Controls.Add(this.lblBitis);
            this.pnlTop.Controls.Add(this.lblBaslangic);
            
            // Filters
            this.lblBaslangic.Text = "Başlangıç:";
            this.lblBaslangic.Location = new System.Drawing.Point(20, 23);
            
            this.dtBaslangic.Location = new System.Drawing.Point(80, 20);
            this.dtBaslangic.Size = new System.Drawing.Size(120, 20);

            this.lblBitis.Text = "Bitiş:";
            this.lblBitis.Location = new System.Drawing.Point(220, 23);

            this.dtBitis.Location = new System.Drawing.Point(250, 20);
            this.dtBitis.Size = new System.Drawing.Size(120, 20);

            this.btnListele.Text = "Listele";
            this.btnListele.Location = new System.Drawing.Point(400, 18);
            this.btnListele.Size = new System.Drawing.Size(100, 24);
            this.btnListele.Click += new System.EventHandler(this.btnListele_Click);

            // Grid
            this.gcRapor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRapor.MainView = this.gvRapor;
            this.gcRapor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gvRapor });

            this.gvRapor.GridControl = this.gcRapor;
            this.gvRapor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                this.colUrunAdi,
                this.colToplamMiktar,
                this.colIslemSayisi
            });
            this.gvRapor.OptionsBehavior.Editable = false;
            this.gvRapor.OptionsView.ShowGroupPanel = false;

            this.colUrunAdi.Caption = "Ürün Adı";
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Visible = true;

            this.colToplamMiktar.Caption = "Satılan Miktar";
            this.colToplamMiktar.FieldName = "ToplamMiktar";
            this.colToplamMiktar.Visible = true;

            this.colIslemSayisi.Caption = "İşlem Sayısı";
            this.colIslemSayisi.FieldName = "IslemSayisi";
            this.colIslemSayisi.Visible = true;

            // Form
            this.Controls.Add(this.gcRapor);
            this.Controls.Add(this.pnlTop);
            this.Name = "UcRaporlar";
            this.Size = new System.Drawing.Size(800, 600);

            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtBitis.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBitis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBaslangic.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBaslangic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRapor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRapor)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.SimpleButton btnListele;
        private DevExpress.XtraEditors.DateEdit dtBitis;
        private DevExpress.XtraEditors.DateEdit dtBaslangic;
        private DevExpress.XtraEditors.LabelControl lblBitis;
        private DevExpress.XtraEditors.LabelControl lblBaslangic;
        private DevExpress.XtraGrid.GridControl gcRapor;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRapor;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colToplamMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colIslemSayisi;
    }
}
