using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Modern tema sistemi - Material Design ve Fluent Design prensiplerine dayalı
    /// Tüm uygulama için tutarlı modern görünüm sağlar
    /// </summary>
    public static class ModernTheme
    {
        // Modern Renk Paleti (Material Design + Fluent Design)
        public static class Colors
        {
            // Primary Blue Palette
            public static Color Primary = Color.FromArgb(0, 120, 215);        // Windows Blue
            public static Color PrimaryLight = Color.FromArgb(96, 205, 255);  // Light Blue
            public static Color PrimaryDark = Color.FromArgb(0, 90, 158);     // Dark Blue
            public static Color PrimaryAccent = Color.FromArgb(0, 151, 251);  // Accent Blue

            // Secondary Colors
            public static Color Secondary = Color.FromArgb(124, 124, 124);    // Gray
            public static Color SecondaryLight = Color.FromArgb(200, 200, 200);
            public static Color SecondaryDark = Color.FromArgb(80, 80, 80);

            // Status Colors
            public static Color Success = Color.FromArgb(16, 124, 16);        // Green
            public static Color Warning = Color.FromArgb(255, 185, 0);        // Amber
            public static Color Danger = Color.FromArgb(232, 17, 35);         // Red
            public static Color Info = Color.FromArgb(0, 120, 215);           // Blue

            // Background Colors
            public static Color Background = Color.FromArgb(243, 243, 243);    // Light Gray
            public static Color Surface = Color.White;                         // White
            public static Color SurfaceHover = Color.FromArgb(249, 249, 249);  // Very Light Gray
            public static Color SurfacePressed = Color.FromArgb(240, 240, 240);

            // Text Colors
            public static Color TextPrimary = Color.FromArgb(32, 32, 32);      // Dark Gray
            public static Color TextSecondary = Color.FromArgb(96, 96, 96);    // Medium Gray
            public static Color TextDisabled = Color.FromArgb(160, 160, 160);  // Light Gray
            public static Color TextOnPrimary = Color.White;                   // White

            // Border Colors
            public static Color Border = Color.FromArgb(225, 225, 225);        // Light Border
            public static Color BorderHover = Color.FromArgb(200, 200, 200);  // Medium Border
            public static Color BorderFocus = Primary;                          // Primary Blue

            // Shadow Colors
            public static Color ShadowLight = Color.FromArgb(0, 0, 0, 10);     // Light Shadow
            public static Color ShadowMedium = Color.FromArgb(0, 0, 0, 20);    // Medium Shadow
            public static Color ShadowDark = Color.FromArgb(0, 0, 0, 30);      // Dark Shadow
        }

        // Modern Spacing System (8px grid)
        public static class Spacing
        {
            public static int XS = 4;   // 4px
            public static int SM = 8;   // 8px
            public static int MD = 16;  // 16px
            public static int LG = 24;  // 24px
            public static int XL = 32;  // 32px
            public static int XXL = 48; // 48px
        }

        // Modern Typography
        public static class Typography
        {
            public static Font H1 = new Font("Segoe UI", 32F, FontStyle.Bold);
            public static Font H2 = new Font("Segoe UI", 24F, FontStyle.Bold);
            public static Font H3 = new Font("Segoe UI", 20F, FontStyle.Bold);
            public static Font H4 = new Font("Segoe UI", 16F, FontStyle.Bold);
            public static Font H5 = new Font("Segoe UI", 14F, FontStyle.Bold);
            public static Font H6 = new Font("Segoe UI", 12F, FontStyle.Bold);
            public static Font Body = new Font("Segoe UI", 10F);
            public static Font BodyLarge = new Font("Segoe UI", 11F);
            public static Font Caption = new Font("Segoe UI", 9F);
            public static Font Button = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        /// <summary>
        /// Modern card stilini uygular (gölge, rounded corners, hover effects)
        /// </summary>
        public static void ApplyModernCard(PanelControl panel)
        {
            if (panel == null) return;

            panel.Appearance.BackColor = Colors.Surface;
            panel.Appearance.BorderColor = Colors.Border;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.Options.UseBorderColor = true;

            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Padding = new Padding(Spacing.MD);

            // Modern hover effect
            var originalColor = Colors.Surface;
            panel.MouseEnter += (s, e) =>
            {
                panel.Appearance.BackColor = Colors.SurfaceHover;
                panel.Appearance.BorderColor = Colors.BorderHover;
            };
            panel.MouseLeave += (s, e) =>
            {
                panel.Appearance.BackColor = originalColor;
                panel.Appearance.BorderColor = Colors.Border;
            };
        }

        /// <summary>
        /// Modern buton stilini uygular
        /// </summary>
        public static void ApplyModernButton(SimpleButton button, Color backgroundColor, bool isPrimary = false)
        {
            if (button == null) return;

            button.Appearance.BackColor = backgroundColor;
            button.Appearance.ForeColor = Colors.TextOnPrimary;
            button.Appearance.Font = Typography.Button;
            button.Appearance.Options.UseBackColor = true;
            button.Appearance.Options.UseForeColor = true;
            button.Appearance.Options.UseFont = true;

            // Hover effect
            var hoverColor = LightenColor(backgroundColor, 10);
            button.AppearanceHovered.BackColor = hoverColor;
            button.AppearanceHovered.ForeColor = Colors.TextOnPrimary;
            button.AppearanceHovered.Options.UseBackColor = true;
            button.AppearanceHovered.Options.UseForeColor = true;

            // Pressed effect
            var pressedColor = DarkenColor(backgroundColor, 10);
            button.AppearancePressed.BackColor = pressedColor;
            button.AppearancePressed.ForeColor = Colors.TextOnPrimary;
            button.AppearancePressed.Options.UseBackColor = true;
            button.AppearancePressed.Options.UseForeColor = true;

            // Size
            if (isPrimary)
            {
                button.Height = 40;
                button.Width = Math.Max(button.Width, 120);
            }
            else
            {
                button.Height = 36;
            }

            button.AllowFocus = true;
        }

        /// <summary>
        /// Modern Grid stilini uygular
        /// </summary>
        public static void ApplyModernGrid(GridView gridView)
        {
            if (gridView == null) return;

            // Header styling
            gridView.Appearance.HeaderPanel.BackColor = Colors.Background;
            gridView.Appearance.HeaderPanel.ForeColor = Colors.TextPrimary;
            gridView.Appearance.HeaderPanel.Font = Typography.H6;
            gridView.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridView.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridView.Appearance.HeaderPanel.Options.UseFont = true;
            gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView.Appearance.HeaderPanel.BorderColor = Colors.Border;
            gridView.Appearance.HeaderPanel.Options.UseBorderColor = true;

            // Row styling
            gridView.Appearance.Row.BackColor = Colors.Surface;
            gridView.Appearance.Row.ForeColor = Colors.TextPrimary;
            gridView.Appearance.Row.Font = Typography.Body;
            gridView.Appearance.Row.Options.UseBackColor = true;
            gridView.Appearance.Row.Options.UseForeColor = true;
            gridView.Appearance.Row.Options.UseFont = true;
            gridView.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            // Selected row
            gridView.Appearance.SelectedRow.BackColor = Colors.Primary;
            gridView.Appearance.SelectedRow.ForeColor = Colors.TextOnPrimary;
            gridView.Appearance.SelectedRow.Options.UseBackColor = true;
            gridView.Appearance.SelectedRow.Options.UseForeColor = true;

            // Focused row
            gridView.Appearance.FocusedRow.BackColor = Colors.PrimaryLight;
            gridView.Appearance.FocusedRow.ForeColor = Colors.TextPrimary;
            gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;

            // Even row (zebra striping)
            gridView.Appearance.EvenRow.BackColor = Color.FromArgb(250, 250, 250);
            gridView.Appearance.EvenRow.Options.UseBackColor = true;

            // Border
            gridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowIndicator = false;

            // Row height
            gridView.RowHeight = 40;
            gridView.ColumnPanelRowHeight = 45;
        }

        /// <summary>
        /// Modern GroupControl stilini uygular
        /// </summary>
        public static void ApplyModernGroup(GroupControl group)
        {
            if (group == null) return;

            // Header
            group.AppearanceCaption.BackColor = Colors.Primary;
            group.AppearanceCaption.ForeColor = Colors.TextOnPrimary;
            group.AppearanceCaption.Font = Typography.H5;
            group.AppearanceCaption.Options.UseBackColor = true;
            group.AppearanceCaption.Options.UseForeColor = true;
            group.AppearanceCaption.Options.UseFont = true;

            // Content
            group.Appearance.BackColor = Colors.Surface;
            group.Appearance.Options.UseBackColor = true;

            // Border
            group.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            // Padding
            group.Padding = new Padding(Spacing.MD);
        }

        /// <summary>
        /// Modern Sidebar stilini uygular
        /// </summary>
        public static void ApplyModernSidebar(DevExpress.XtraBars.Navigation.AccordionControl accordion)
        {
            if (accordion == null) return;

            // Group appearance
            accordion.Appearance.Group.Normal.BackColor = Colors.Primary;
            accordion.Appearance.Group.Normal.ForeColor = Colors.TextOnPrimary;
            accordion.Appearance.Group.Normal.Font = Typography.H6;
            accordion.Appearance.Group.Normal.Options.UseBackColor = true;
            accordion.Appearance.Group.Normal.Options.UseForeColor = true;
            accordion.Appearance.Group.Normal.Options.UseFont = true;

            accordion.Appearance.Group.Hovered.BackColor = Colors.PrimaryDark;
            accordion.Appearance.Group.Hovered.ForeColor = Colors.TextOnPrimary;
            accordion.Appearance.Group.Hovered.Options.UseBackColor = true;
            accordion.Appearance.Group.Hovered.Options.UseForeColor = true;

            accordion.Appearance.Group.Pressed.BackColor = Colors.PrimaryDark;
            accordion.Appearance.Group.Pressed.ForeColor = Colors.TextOnPrimary;
            accordion.Appearance.Group.Pressed.Options.UseBackColor = true;
            accordion.Appearance.Group.Pressed.Options.UseForeColor = true;

            // Item appearance
            accordion.Appearance.Item.Normal.BackColor = Colors.Surface;
            accordion.Appearance.Item.Normal.ForeColor = Colors.TextPrimary;
            accordion.Appearance.Item.Normal.Font = Typography.Body;
            accordion.Appearance.Item.Normal.Options.UseBackColor = true;
            accordion.Appearance.Item.Normal.Options.UseForeColor = true;
            accordion.Appearance.Item.Normal.Options.UseFont = true;

            accordion.Appearance.Item.Hovered.BackColor = Colors.SurfaceHover;
            accordion.Appearance.Item.Hovered.ForeColor = Colors.Primary;
            accordion.Appearance.Item.Hovered.Options.UseBackColor = true;
            accordion.Appearance.Item.Hovered.Options.UseForeColor = true;

            accordion.Appearance.Item.Pressed.BackColor = Colors.PrimaryLight;
            accordion.Appearance.Item.Pressed.ForeColor = Colors.TextOnPrimary;
            accordion.Appearance.Item.Pressed.Options.UseBackColor = true;
            accordion.Appearance.Item.Pressed.Options.UseForeColor = true;
        }

        /// <summary>
        /// Renk açıklığını artırır
        /// </summary>
        private static Color LightenColor(Color color, int percent)
        {
            int r = Math.Min(255, color.R + (255 - color.R) * percent / 100);
            int g = Math.Min(255, color.G + (255 - color.G) * percent / 100);
            int b = Math.Min(255, color.B + (255 - color.B) * percent / 100);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Renk koyuluğunu artırır
        /// </summary>
        private static Color DarkenColor(Color color, int percent)
        {
            int r = Math.Max(0, color.R - color.R * percent / 100);
            int g = Math.Max(0, color.G - color.G * percent / 100);
            int b = Math.Max(0, color.B - color.B * percent / 100);
            return Color.FromArgb(r, g, b);
        }
    }
}
