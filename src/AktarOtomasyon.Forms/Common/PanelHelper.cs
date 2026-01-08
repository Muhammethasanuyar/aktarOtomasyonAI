using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Panel and GroupControl styling helper with animations
    /// Provides modern web-like appearance with smooth transitions
    /// </summary>
    public static class PanelHelper
    {
        /// <summary>
        /// Applies modern styling to PanelControl with hover effects
        /// </summary>
        public static void ApplyModernStyle(PanelControl panel)
        {
            if (panel == null) return;

            // Modern appearance
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.Options.UseBorderColor = true;

            // Border settings
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        }

        /// <summary>
        /// Applies modern card style to PanelControl (for product cards, etc.)
        /// </summary>
        public static void ApplyCardStyle(PanelControl panel)
        {
            if (panel == null) return;

            panel.Appearance.BackColor = Color.White;
            panel.Appearance.BorderColor = Color.FromArgb(220, 220, 220);
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.Options.UseBorderColor = true;

            // Card-like border
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            // Padding for card content
            panel.Padding = new Padding(10);
        }

        /// <summary>
        /// Applies modern styling to GroupControl with smooth expand/collapse
        /// </summary>
        public static void ApplyModernGroupStyle(GroupControl group)
        {
            if (group == null) return;

            // Modern header appearance
            group.AppearanceCaption.BackColor = Color.FromArgb(33, 150, 243); // Primary blue
            group.AppearanceCaption.ForeColor = Color.White;
            group.AppearanceCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            group.AppearanceCaption.Options.UseBackColor = true;
            group.AppearanceCaption.Options.UseForeColor = true;
            group.AppearanceCaption.Options.UseFont = true;

            // Content appearance
            group.Appearance.BackColor = Color.White;
            group.Appearance.Options.UseBackColor = true;

            // Border
            group.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        }

        /// <summary>
        /// Applies hover effect to panel (for card-like interactions)
        /// </summary>
        public static void EnableHoverEffect(PanelControl panel, Color hoverColor)
        {
            if (panel == null) return;

            var originalColor = panel.Appearance.BackColor;
            
            panel.MouseEnter += (s, e) =>
            {
                panel.Appearance.BackColor = hoverColor;
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.Appearance.BackColor = originalColor;
            };
        }
    }
}
