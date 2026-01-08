using System;
using System.Collections.Generic;
using System.Drawing;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Icon standardization helper
    /// Sprint 9: Provides consistent icons across the application
    /// Uses simple bitmap generation for common icons
    /// </summary>
    public static class IconHelper
    {
        private static Dictionary<string, Image> _iconCache = new Dictionary<string, Image>();

        /// <summary>
        /// Gets a standard icon by name
        /// </summary>
        /// <param name="iconName">Icon name (e.g., "kritik", "acil", "normal", "info")</param>
        /// <param name="size">Icon size (default: 16x16)</param>
        /// <returns>Icon image</returns>
        public static Image GetIcon(string iconName, Size? size = null)
        {
            var iconSize = size ?? new Size(16, 16);
            var cacheKey = string.Format("{0}_{1}x{2}", iconName, iconSize.Width, iconSize.Height);

            if (_iconCache.ContainsKey(cacheKey))
                return _iconCache[cacheKey];

            Image icon = null;

            switch (iconName.ToLower())
            {
                case "kritik":
                case "critical":
                case "error":
                    icon = CreateCircleIcon(iconSize, GridHelper.StandardColors.Kritik);
                    break;

                case "acil":
                case "warning":
                case "uyari":
                    icon = CreateTriangleIcon(iconSize, GridHelper.StandardColors.Acil);
                    break;

                case "normal":
                case "success":
                case "basarili":
                    icon = CreateCheckIcon(iconSize, GridHelper.StandardColors.Normal);
                    break;

                case "info":
                case "bilgi":
                    icon = CreateInfoIcon(iconSize, GridHelper.StandardColors.Info);
                    break;

                case "pasif":
                case "inactive":
                    icon = CreateCircleIcon(iconSize, GridHelper.StandardColors.Pasif);
                    break;

                case "stok":
                case "stock":
                    icon = CreateStockIcon(iconSize);
                    break;

                case "siparis":
                case "order":
                    icon = CreateOrderIcon(iconSize);
                    break;

                case "bildirim":
                case "notification":
                    icon = CreateNotificationIcon(iconSize);
                    break;

                case "urun":
                case "product":
                    icon = CreateProductIcon(iconSize);
                    break;

                default:
                    icon = CreateDefaultIcon(iconSize);
                    break;
            }

            _iconCache[cacheKey] = icon;
            return icon;
        }

        /// <summary>
        /// Gets status icon based on stock level
        /// </summary>
        public static Image GetStockStatusIcon(decimal mevcut, decimal kritik, decimal emniyet, Size? size = null)
        {
            if (mevcut <= kritik)
                return GetIcon("kritik", size);
            else if (mevcut <= emniyet)
                return GetIcon("acil", size);
            else
                return GetIcon("normal", size);
        }

        /// <summary>
        /// Gets order status icon
        /// </summary>
        public static Image GetOrderStatusIcon(string durum, Size? size = null)
        {
            string durumUpper = durum != null ? durum.ToUpper() : null;
            switch (durumUpper)
            {
                case "TASLAK":
                    return GetIcon("info", size);
                case "GONDERILDI":
                    return GetIcon("acil", size);
                case "KISMI":
                    return GetIcon("warning", size);
                case "TAMAMLANDI":
                    return GetIcon("success", size);
                case "IPTAL":
                    return GetIcon("error", size);
                default:
                    return GetIcon("info", size);
            }
        }

        /// <summary>
        /// Clears icon cache (useful for testing or theme changes)
        /// </summary>
        public static void ClearCache()
        {
            foreach (var icon in _iconCache.Values)
            {
                if (icon != null)
                {
                    icon.Dispose();
                }
            }
            _iconCache.Clear();
        }

        #region Icon Creation Methods

        private static Image CreateCircleIcon(Size size, Color color)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                var margin = size.Width / 4;
                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 2);
                }
            }
            return bmp;
        }

        private static Image CreateTriangleIcon(Size size, Color color)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                var margin = size.Width / 8;
                var points = new Point[]
                {
                    new Point(size.Width / 2, margin),
                    new Point(size.Width - margin, size.Height - margin),
                    new Point(margin, size.Height - margin)
                };

                using (var brush = new SolidBrush(color))
                {
                    g.FillPolygon(brush, points);
                }

                // Exclamation mark
                using (var pen = new Pen(Color.White, 2))
                {
                    var centerX = size.Width / 2;
                    g.DrawLine(pen, centerX, size.Height / 3, centerX, size.Height / 2);
                    g.FillEllipse(Brushes.White, centerX - 1, size.Height * 2 / 3, 2, 2);
                }
            }
            return bmp;
        }

        private static Image CreateCheckIcon(Size size, Color color)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Circle background
                var margin = size.Width / 8;
                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 2);
                }

                // Checkmark
                using (var pen = new Pen(Color.White, 2))
                {
                    var centerX = size.Width / 2;
                    var centerY = size.Height / 2;
                    g.DrawLine(pen, centerX - 3, centerY, centerX - 1, centerY + 3);
                    g.DrawLine(pen, centerX - 1, centerY + 3, centerX + 4, centerY - 3);
                }
            }
            return bmp;
        }

        private static Image CreateInfoIcon(Size size, Color color)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Circle background
                var margin = size.Width / 8;
                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 2);
                }

                // "i" letter
                using (var pen = new Pen(Color.White, 2))
                {
                    var centerX = size.Width / 2;
                    g.FillEllipse(Brushes.White, centerX - 1, size.Height / 3, 2, 2);
                    g.DrawLine(pen, centerX, size.Height / 2, centerX, size.Height * 2 / 3);
                }
            }
            return bmp;
        }

        private static Image CreateStockIcon(Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Simple box stack
                var margin = size.Width / 6;
                var boxHeight = size.Height / 4;

                using (var brush = new SolidBrush(Color.FromArgb(100, 149, 237))) // Cornflower blue
                using (var pen = new Pen(Color.FromArgb(70, 130, 180), 1))
                {
                    // Bottom box
                    g.FillRectangle(brush, margin, size.Height - boxHeight - margin,
                        size.Width - margin * 2, boxHeight);
                    g.DrawRectangle(pen, margin, size.Height - boxHeight - margin,
                        size.Width - margin * 2, boxHeight);

                    // Top box
                    g.FillRectangle(brush, margin, size.Height - boxHeight * 2 - margin,
                        size.Width - margin * 2, boxHeight);
                    g.DrawRectangle(pen, margin, size.Height - boxHeight * 2 - margin,
                        size.Width - margin * 2, boxHeight);
                }
            }
            return bmp;
        }

        private static Image CreateOrderIcon(Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Document with lines
                var margin = size.Width / 6;
                using (var brush = new SolidBrush(Color.White))
                using (var pen = new Pen(Color.FromArgb(128, 128, 128), 1))
                {
                    // Document background
                    g.FillRectangle(brush, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 2);
                    g.DrawRectangle(pen, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 2);

                    // Lines
                    var lineY = margin + size.Height / 4;
                    for (int i = 0; i < 3; i++)
                    {
                        g.DrawLine(pen, margin + 2, lineY, size.Width - margin - 2, lineY);
                        lineY += size.Height / 6;
                    }
                }
            }
            return bmp;
        }

        private static Image CreateNotificationIcon(Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Bell shape
                var margin = size.Width / 6;
                using (var brush = new SolidBrush(Color.FromArgb(255, 193, 7))) // Amber
                using (var pen = new Pen(Color.FromArgb(255, 160, 0), 1))
                {
                    // Bell body (simplified as circle)
                    g.FillEllipse(brush, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 3);
                    g.DrawEllipse(pen, margin, margin,
                        size.Width - margin * 2, size.Height - margin * 3);

                    // Bell bottom
                    g.DrawLine(pen, margin, size.Height - margin * 2,
                        size.Width - margin, size.Height - margin * 2);
                }
            }
            return bmp;
        }

        private static Image CreateProductIcon(Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Package/box icon
                var margin = size.Width / 6;
                using (var brush = new SolidBrush(Color.FromArgb(156, 39, 176))) // Purple
                using (var pen = new Pen(Color.FromArgb(123, 31, 162), 1))
                {
                    // Box
                    g.FillRectangle(brush, margin, margin + size.Height / 6,
                        size.Width - margin * 2, size.Height - margin * 2 - size.Height / 6);
                    g.DrawRectangle(pen, margin, margin + size.Height / 6,
                        size.Width - margin * 2, size.Height - margin * 2 - size.Height / 6);

                    // Top flap
                    var points = new Point[]
                    {
                        new Point(margin, margin + size.Height / 6),
                        new Point(size.Width / 2, margin),
                        new Point(size.Width - margin, margin + size.Height / 6)
                    };
                    g.FillPolygon(brush, points);
                    g.DrawPolygon(pen, points);
                }
            }
            return bmp;
        }

        private static Image CreateDefaultIcon(Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using (var pen = new Pen(Color.Gray, 1))
                {
                    g.DrawRectangle(pen, 2, 2, size.Width - 4, size.Height - 4);
                }
            }
            return bmp;
        }

        #endregion
    }
}
