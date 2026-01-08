using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Button styling and standardization helper
    /// Provides consistent button appearance across all screens
    /// </summary>
    public static class ButtonHelper
    {
        // Standard button colors (Blue theme)
        public static class StandardColors
        {
            public static Color Primary = Color.FromArgb(33, 150, 243);      // #2196F3
            public static Color Secondary = Color.FromArgb(100, 181, 246);   // #64B5F6
            public static Color Accent = Color.FromArgb(25, 118, 210);       // #1976D2
            public static Color Success = Color.FromArgb(76, 175, 80);        // #4CAF50
            public static Color Warning = Color.FromArgb(255, 171, 0);        // #FFAB00
            public static Color Danger = Color.FromArgb(255, 77, 79);         // #FF4D4F
            public static Color Gray = Color.FromArgb(158, 158, 158);         // #9E9E9E
        }

        // Standard button sizes
        public static class StandardSizes
        {
            public static Size Small = new Size(80, 30);
            public static Size Medium = new Size(120, 30);
            public static Size Large = new Size(160, 30);
            public static Size ExtraLarge = new Size(200, 40);
        }

        /// <summary>
        /// Applies standard blue primary button styling with hover effects (Modern Theme)
        /// </summary>
        public static void ApplyPrimaryStyle(SimpleButton button)
        {
            if (button == null) return;
            ModernTheme.ApplyModernButton(button, ModernTheme.Colors.Primary, isPrimary: true);
        }

        /// <summary>
        /// Applies standard success button styling with hover effects (Modern Theme)
        /// </summary>
        public static void ApplySuccessStyle(SimpleButton button)
        {
            if (button == null) return;
            ModernTheme.ApplyModernButton(button, ModernTheme.Colors.Success, isPrimary: false);
        }

        /// <summary>
        /// Applies standard warning button styling with hover effects (Modern Theme)
        /// </summary>
        public static void ApplyWarningStyle(SimpleButton button)
        {
            if (button == null) return;
            ModernTheme.ApplyModernButton(button, ModernTheme.Colors.Warning, isPrimary: false);
        }

        /// <summary>
        /// Applies standard danger button styling with hover effects (Modern Theme)
        /// </summary>
        public static void ApplyDangerStyle(SimpleButton button)
        {
            if (button == null) return;
            ModernTheme.ApplyModernButton(button, ModernTheme.Colors.Danger, isPrimary: false);
        }

        /// <summary>
        /// Applies standard secondary/gray button styling with hover effects (Modern Theme)
        /// </summary>
        public static void ApplySecondaryStyle(SimpleButton button)
        {
            if (button == null) return;
            ModernTheme.ApplyModernButton(button, ModernTheme.Colors.Secondary, isPrimary: false);
        }

        /// <summary>
        /// Applies standard size to button
        /// </summary>
        public static void ApplyStandardSize(SimpleButton button, Size size)
        {
            if (button == null) return;
            button.Size = size;
        }

        /// <summary>
        /// Applies modern gradient style to button (web-like appearance)
        /// </summary>
        public static void ApplyGradientStyle(SimpleButton button, Color startColor, Color endColor)
        {
            if (button == null) return;

            // DevExpress buttons support gradient through Appearance
            button.Appearance.BackColor = startColor;
            button.Appearance.ForeColor = Color.White;
            button.Appearance.Options.UseBackColor = true;
            button.Appearance.Options.UseForeColor = true;

            // Hover gradient (lighter)
            var hoverStart = LightenColor(startColor, 10);
            var hoverEnd = LightenColor(endColor, 10);
            button.AppearanceHovered.BackColor = hoverStart;
            button.AppearanceHovered.ForeColor = Color.White;
            button.AppearanceHovered.Options.UseBackColor = true;
            button.AppearanceHovered.Options.UseForeColor = true;

            // Pressed gradient (darker)
            var pressedStart = DarkenColor(startColor, 10);
            var pressedEnd = DarkenColor(endColor, 10);
            button.AppearancePressed.BackColor = pressedStart;
            button.AppearancePressed.ForeColor = Color.White;
            button.AppearancePressed.Options.UseBackColor = true;
            button.AppearancePressed.Options.UseForeColor = true;

            button.AllowFocus = true;
        }

        /// <summary>
        /// Lightens a color by a percentage
        /// </summary>
        private static Color LightenColor(Color color, int percent)
        {
            int r = Math.Min(255, color.R + (255 - color.R) * percent / 100);
            int g = Math.Min(255, color.G + (255 - color.G) * percent / 100);
            int b = Math.Min(255, color.B + (255 - color.B) * percent / 100);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Darkens a color by a percentage
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
