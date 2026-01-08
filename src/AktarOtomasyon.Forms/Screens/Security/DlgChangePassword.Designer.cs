namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class DlgChangePassword
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblKullaniciInfo = new DevExpress.XtraEditors.LabelControl();
            this.lblEskiParola = new DevExpress.XtraEditors.LabelControl();
            this.txtEskiParola = new DevExpress.XtraEditors.TextEdit();
            this.lblYeniParola = new DevExpress.XtraEditors.LabelControl();
            this.txtYeniParola = new DevExpress.XtraEditors.TextEdit();
            this.lblYeniParolaTekrar = new DevExpress.XtraEditors.LabelControl();
            this.txtYeniParolaTekrar = new DevExpress.XtraEditors.TextEdit();
            this.lblKurallar = new DevExpress.XtraEditors.LabelControl();
            this.btnTamam = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtEskiParola.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYeniParola.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYeniParolaTekrar.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // lblKullaniciInfo
            //
            this.lblKullaniciInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblKullaniciInfo.Appearance.Options.UseFont = true;
            this.lblKullaniciInfo.Location = new System.Drawing.Point(20, 20);
            this.lblKullaniciInfo.Name = "lblKullaniciInfo";
            this.lblKullaniciInfo.Size = new System.Drawing.Size(60, 14);
            this.lblKullaniciInfo.TabIndex = 0;
            this.lblKullaniciInfo.Text = "Kullanıcı:";
            //
            // lblEskiParola
            //
            this.lblEskiParola.Location = new System.Drawing.Point(20, 50);
            this.lblEskiParola.Name = "lblEskiParola";
            this.lblEskiParola.Size = new System.Drawing.Size(59, 13);
            this.lblEskiParola.TabIndex = 1;
            this.lblEskiParola.Text = "Eski Parola:";
            //
            // txtEskiParola
            //
            this.txtEskiParola.Location = new System.Drawing.Point(150, 47);
            this.txtEskiParola.Name = "txtEskiParola";
            this.txtEskiParola.Properties.PasswordChar = '*';
            this.txtEskiParola.Properties.UseSystemPasswordChar = true;
            this.txtEskiParola.Size = new System.Drawing.Size(250, 20);
            this.txtEskiParola.TabIndex = 0;
            //
            // lblYeniParola
            //
            this.lblYeniParola.Location = new System.Drawing.Point(20, 80);
            this.lblYeniParola.Name = "lblYeniParola";
            this.lblYeniParola.Size = new System.Drawing.Size(60, 13);
            this.lblYeniParola.TabIndex = 3;
            this.lblYeniParola.Text = "Yeni Parola:";
            //
            // txtYeniParola
            //
            this.txtYeniParola.Location = new System.Drawing.Point(150, 77);
            this.txtYeniParola.Name = "txtYeniParola";
            this.txtYeniParola.Properties.PasswordChar = '*';
            this.txtYeniParola.Properties.UseSystemPasswordChar = true;
            this.txtYeniParola.Size = new System.Drawing.Size(250, 20);
            this.txtYeniParola.TabIndex = 1;
            //
            // lblYeniParolaTekrar
            //
            this.lblYeniParolaTekrar.Location = new System.Drawing.Point(20, 110);
            this.lblYeniParolaTekrar.Name = "lblYeniParolaTekrar";
            this.lblYeniParolaTekrar.Size = new System.Drawing.Size(106, 13);
            this.lblYeniParolaTekrar.TabIndex = 5;
            this.lblYeniParolaTekrar.Text = "Yeni Parola (Tekrar):";
            //
            // txtYeniParolaTekrar
            //
            this.txtYeniParolaTekrar.Location = new System.Drawing.Point(150, 107);
            this.txtYeniParolaTekrar.Name = "txtYeniParolaTekrar";
            this.txtYeniParolaTekrar.Properties.PasswordChar = '*';
            this.txtYeniParolaTekrar.Properties.UseSystemPasswordChar = true;
            this.txtYeniParolaTekrar.Size = new System.Drawing.Size(250, 20);
            this.txtYeniParolaTekrar.TabIndex = 2;
            //
            // lblKurallar
            //
            this.lblKurallar.AllowHtmlString = true;
            this.lblKurallar.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblKurallar.Appearance.Options.UseForeColor = true;
            this.lblKurallar.Location = new System.Drawing.Point(20, 145);
            this.lblKurallar.Name = "lblKurallar";
            this.lblKurallar.Size = new System.Drawing.Size(380, 39);
            this.lblKurallar.TabIndex = 7;
            this.lblKurallar.Text = "<b>Parola Kuralları:</b><br/>" +
                                    "• En az 8 karakter<br/>" +
                                    "• En az bir büyük harf ve bir rakam içermeli";
            //
            // btnTamam
            //
            this.btnTamam.Location = new System.Drawing.Point(230, 200);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(85, 30);
            this.btnTamam.TabIndex = 3;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            //
            // btnIptal
            //
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.Location = new System.Drawing.Point(320, 200);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(85, 30);
            this.btnIptal.TabIndex = 4;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            //
            // DlgChangePassword
            //
            this.AcceptButton = this.btnTamam;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(420, 250);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnTamam);
            this.Controls.Add(this.lblKurallar);
            this.Controls.Add(this.txtYeniParolaTekrar);
            this.Controls.Add(this.lblYeniParolaTekrar);
            this.Controls.Add(this.txtYeniParola);
            this.Controls.Add(this.lblYeniParola);
            this.Controls.Add(this.txtEskiParola);
            this.Controls.Add(this.lblEskiParola);
            this.Controls.Add(this.lblKullaniciInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parola Değiştir";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgChangePassword_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtEskiParola.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYeniParola.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYeniParolaTekrar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblKullaniciInfo;
        private DevExpress.XtraEditors.LabelControl lblEskiParola;
        private DevExpress.XtraEditors.TextEdit txtEskiParola;
        private DevExpress.XtraEditors.LabelControl lblYeniParola;
        private DevExpress.XtraEditors.TextEdit txtYeniParola;
        private DevExpress.XtraEditors.LabelControl lblYeniParolaTekrar;
        private DevExpress.XtraEditors.TextEdit txtYeniParolaTekrar;
        private DevExpress.XtraEditors.LabelControl lblKurallar;
        private DevExpress.XtraEditors.SimpleButton btnTamam;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
    }
}
