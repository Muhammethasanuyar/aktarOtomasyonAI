namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class DlgSiparisSatir
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
            this.lblUrun = new DevExpress.XtraEditors.LabelControl();
            this.lkpUrun = new DevExpress.XtraEditors.LookUpEdit();
            this.lblMiktar = new DevExpress.XtraEditors.LabelControl();
            this.spnMiktar = new DevExpress.XtraEditors.SpinEdit();
            this.lblBirimFiyat = new DevExpress.XtraEditors.LabelControl();
            this.spnBirimFiyat = new DevExpress.XtraEditors.SpinEdit();
            this.lblTutarLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblTutar = new DevExpress.XtraEditors.LabelControl();
            this.btnTamam = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUrun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMiktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnBirimFiyat.Properties)).BeginInit();
            this.SuspendLayout();

            // lblUrun
            this.lblUrun.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblUrun.Appearance.Options.UseForeColor = true;
            this.lblUrun.Location = new System.Drawing.Point(20, 20);
            this.lblUrun.Name = "lblUrun";
            this.lblUrun.Size = new System.Drawing.Size(35, 13);
            this.lblUrun.TabIndex = 0;
            this.lblUrun.Text = "Ürün: *";

            // lkpUrun
            this.lkpUrun.Location = new System.Drawing.Point(120, 17);
            this.lkpUrun.Name = "lkpUrun";
            this.lkpUrun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpUrun.Properties.NullText = "Seçiniz...";
            this.lkpUrun.Size = new System.Drawing.Size(300, 20);
            this.lkpUrun.TabIndex = 1;

            // lblMiktar
            this.lblMiktar.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMiktar.Appearance.Options.UseForeColor = true;
            this.lblMiktar.Location = new System.Drawing.Point(20, 50);
            this.lblMiktar.Name = "lblMiktar";
            this.lblMiktar.Size = new System.Drawing.Size(43, 13);
            this.lblMiktar.TabIndex = 2;
            this.lblMiktar.Text = "Miktar: *";

            // spnMiktar
            this.spnMiktar.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.spnMiktar.Location = new System.Drawing.Point(120, 47);
            this.spnMiktar.Name = "spnMiktar";
            this.spnMiktar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnMiktar.Properties.DisplayFormat.FormatString = "n2";
            this.spnMiktar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnMiktar.Properties.EditFormat.FormatString = "n2";
            this.spnMiktar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnMiktar.Properties.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.spnMiktar.Properties.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.spnMiktar.Size = new System.Drawing.Size(150, 20);
            this.spnMiktar.TabIndex = 3;
            this.spnMiktar.ValueChanged += new System.EventHandler(this.spnMiktar_ValueChanged);

            // lblBirimFiyat
            this.lblBirimFiyat.Location = new System.Drawing.Point(20, 80);
            this.lblBirimFiyat.Name = "lblBirimFiyat";
            this.lblBirimFiyat.Size = new System.Drawing.Size(60, 13);
            this.lblBirimFiyat.TabIndex = 4;
            this.lblBirimFiyat.Text = "Birim Fiyat:";

            // spnBirimFiyat
            this.spnBirimFiyat.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.spnBirimFiyat.Location = new System.Drawing.Point(120, 77);
            this.spnBirimFiyat.Name = "spnBirimFiyat";
            this.spnBirimFiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnBirimFiyat.Properties.DisplayFormat.FormatString = "c2";
            this.spnBirimFiyat.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnBirimFiyat.Properties.EditFormat.FormatString = "n2";
            this.spnBirimFiyat.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnBirimFiyat.Properties.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.spnBirimFiyat.Properties.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.spnBirimFiyat.Size = new System.Drawing.Size(150, 20);
            this.spnBirimFiyat.TabIndex = 5;
            this.spnBirimFiyat.ValueChanged += new System.EventHandler(this.spnBirimFiyat_ValueChanged);

            // lblTutarLabel
            this.lblTutarLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTutarLabel.Appearance.Options.UseFont = true;
            this.lblTutarLabel.Location = new System.Drawing.Point(20, 110);
            this.lblTutarLabel.Name = "lblTutarLabel";
            this.lblTutarLabel.Size = new System.Drawing.Size(36, 13);
            this.lblTutarLabel.TabIndex = 6;
            this.lblTutarLabel.Text = "Tutar:";

            // lblTutar
            this.lblTutar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTutar.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTutar.Appearance.Options.UseFont = true;
            this.lblTutar.Appearance.Options.UseForeColor = true;
            this.lblTutar.Location = new System.Drawing.Point(120, 110);
            this.lblTutar.Name = "lblTutar";
            this.lblTutar.Size = new System.Drawing.Size(35, 13);
            this.lblTutar.TabIndex = 7;
            this.lblTutar.Text = "0,00 ₺";

            // btnTamam
            this.btnTamam.Location = new System.Drawing.Point(220, 150);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(90, 23);
            this.btnTamam.TabIndex = 8;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);

            // btnIptal
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.Location = new System.Drawing.Point(320, 150);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(90, 23);
            this.btnIptal.TabIndex = 9;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);

            // DlgSiparisSatir
            this.AcceptButton = this.btnTamam;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(440, 190);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnTamam);
            this.Controls.Add(this.lblTutar);
            this.Controls.Add(this.lblTutarLabel);
            this.Controls.Add(this.spnBirimFiyat);
            this.Controls.Add(this.lblBirimFiyat);
            this.Controls.Add(this.spnMiktar);
            this.Controls.Add(this.lblMiktar);
            this.Controls.Add(this.lkpUrun);
            this.Controls.Add(this.lblUrun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgSiparisSatir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sipariş Satırı";
            this.Load += new System.EventHandler(this.DlgSiparisSatir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lkpUrun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMiktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnBirimFiyat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DevExpress.XtraEditors.LabelControl lblUrun;
        private DevExpress.XtraEditors.LookUpEdit lkpUrun;
        private DevExpress.XtraEditors.LabelControl lblMiktar;
        private DevExpress.XtraEditors.SpinEdit spnMiktar;
        private DevExpress.XtraEditors.LabelControl lblBirimFiyat;
        private DevExpress.XtraEditors.SpinEdit spnBirimFiyat;
        private DevExpress.XtraEditors.LabelControl lblTutarLabel;
        private DevExpress.XtraEditors.LabelControl lblTutar;
        private DevExpress.XtraEditors.SimpleButton btnTamam;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
    }
}
