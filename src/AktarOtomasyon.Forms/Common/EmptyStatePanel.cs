using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Reusable empty state panel component
    /// Sprint 9: Shows friendly message when grids or lists are empty
    /// </summary>
    public partial class EmptyStatePanel : UserControl
    {
        /// <summary>
        /// The message to display
        /// </summary>
        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        /// <summary>
        /// The action button text (if any)
        /// </summary>
        public string ActionText
        {
            get { return btnAction.Text; }
            set
            {
                btnAction.Text = value;
                btnAction.Visible = !string.IsNullOrEmpty(value);
            }
        }

        /// <summary>
        /// Event raised when action button is clicked
        /// </summary>
        public event EventHandler ActionClick;

        public EmptyStatePanel()
        {
            InitializeComponent();
            SetupLayout();
        }

        private void SetupLayout()
        {
            // Set default icon (empty folder or search icon)
            SetDefaultIcon();

            // Center everything
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
        }

        private void SetDefaultIcon()
        {
            // Create a simple "no data" icon
            var bmp = new Bitmap(64, 64);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Draw a simple folder icon in gray
                using (var pen = new Pen(Color.LightGray, 3))
                using (var brush = new SolidBrush(Color.FromArgb(240, 240, 240)))
                {
                    // Folder shape
                    g.FillRectangle(brush, 8, 20, 48, 36);
                    g.DrawRectangle(pen, 8, 20, 48, 36);

                    // Folder tab
                    g.FillRectangle(brush, 8, 16, 20, 8);
                    g.DrawRectangle(pen, 8, 16, 20, 8);
                }

                // Draw "empty" indicator (small circle with line)
                using (var pen = new Pen(Color.Gray, 2))
                {
                    g.DrawEllipse(pen, 42, 42, 16, 16);
                    g.DrawLine(pen, 54, 54, 60, 60);
                }
            }

            picIcon.Image = bmp;
        }

        /// <summary>
        /// Sets a custom icon for the empty state
        /// </summary>
        /// <param name="icon">The icon to display</param>
        public void SetIcon(Image icon)
        {
            if (icon != null)
            {
                picIcon.Image = icon;
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (ActionClick != null)
            {
                ActionClick.Invoke(this, e);
            }
        }
    }
}
