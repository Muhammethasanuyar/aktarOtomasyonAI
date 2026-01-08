namespace AktarOtomasyon.Forms.Common
{
    partial class EmptyStatePanel
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
            this.btnAction = new DevExpress.XtraEditors.SimpleButton();
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            this.picIcon = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // panelControl1
            //
            this.panelControl1.Controls.Add(this.btnAction);
            this.panelControl1.Controls.Add(this.lblMessage);
            this.panelControl1.Controls.Add(this.picIcon);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(400, 300);
            this.panelControl1.TabIndex = 0;
            //
            // btnAction
            //
            this.btnAction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAction.Location = new System.Drawing.Point(150, 180);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(100, 30);
            this.btnAction.TabIndex = 2;
            this.btnAction.Text = "Action";
            this.btnAction.Visible = false;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            //
            // lblMessage
            //
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMessage.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMessage.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Appearance.Options.UseForeColor = true;
            this.lblMessage.Appearance.Options.UseTextOptions = true;
            this.lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMessage.Location = new System.Drawing.Point(50, 130);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(300, 40);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Veri bulunamadı";
            //
            // picIcon
            //
            this.picIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picIcon.Location = new System.Drawing.Point(184, 80);
            this.picIcon.Name = "picIcon";
            this.picIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Properties.Appearance.Options.UseBackColor = true;
            this.picIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picIcon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picIcon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.TabIndex = 0;
            //
            // EmptyStatePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "EmptyStatePanel";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit picIcon;
        private DevExpress.XtraEditors.LabelControl lblMessage;
        private DevExpress.XtraEditors.SimpleButton btnAction;
    }
}
