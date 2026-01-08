using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Grid formatting and standardization helper
    /// Sprint 9: Consistent grid appearance and behavior across all screens
    /// </summary>
    public static class GridHelper
    {
        /// <summary>
        /// Applies standard formatting to a GridView with modern animations
        /// - Modern "Clean List" style (WXI compatible)
        /// - Increased row height for better readability
        /// - No vertical lines
        /// - Segoe UI Fonts
        /// - Smooth animations and hover effects
        /// </summary>
        /// <param name="gridView">The GridView to format</param>
        public static void ApplyStandardFormatting(GridView gridView)
        {
            if (gridView == null) return;

            // Use modern theme
            ModernTheme.ApplyModernGrid(gridView);

            // Additional behavior settings
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false; // Row selection only
            gridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            gridView.OptionsView.ShowAutoFilterRow = true; // Keep filtering
            gridView.OptionsView.ColumnAutoWidth = false; // Horizontal scrolling if needed
        }

        /// <summary>
        /// Formats a column as date with dd.MM.yyyy HH:mm format
        /// </summary>
        /// <param name="column">The column to format</param>
        public static void FormatDateColumn(GridColumn column)
        {
            if (column == null) return;

            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            column.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            column.Width = 130;
        }

        /// <summary>
        /// Formats a column as date (short) with dd.MM.yyyy format
        /// </summary>
        /// <param name="column">The column to format</param>
        public static void FormatDateOnlyColumn(GridColumn column)
        {
            if (column == null) return;

            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            column.DisplayFormat.FormatString = "dd.MM.yyyy";
            column.Width = 100;
        }

        /// <summary>
        /// Formats a column as money with N2 format (2 decimal places)
        /// </summary>
        /// <param name="column">The column to format</param>
        public static void FormatMoneyColumn(GridColumn column)
        {
            if (column == null) return;

            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.DisplayFormat.FormatString = "N2";
            column.Width = 100;
        }

        /// <summary>
        /// Formats a column as quantity with N2 format (2 decimal places)
        /// </summary>
        /// <param name="column">The column to format</param>
        public static void FormatQuantityColumn(GridColumn column)
        {
            if (column == null) return;

            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.DisplayFormat.FormatString = "N2";
            column.Width = 80;
        }

        /// <summary>
        /// Formats a column as integer (no decimal places)
        /// </summary>
        /// <param name="column">The column to format</param>
        public static void FormatIntegerColumn(GridColumn column)
        {
            if (column == null) return;

            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.DisplayFormat.FormatString = "N0";
            column.Width = 80;
        }

        /// <summary>
        /// Adds status badge coloring to a column based on status values
        /// </summary>
        /// <param name="gridView">The GridView to customize</param>
        /// <param name="columnName">The column field name containing status</param>
        /// <param name="statusColors">Dictionary mapping status values to colors</param>
        public static void AddStatusBadge(GridView gridView, string columnName,
            Dictionary<string, Color> statusColors)
        {
            if (gridView == null || string.IsNullOrEmpty(columnName) || statusColors == null)
                return;

            gridView.CustomDrawCell += (s, e) =>
            {
                if (e.Column.FieldName == columnName)
                {
                    var status = e.CellValue != null ? e.CellValue.ToString() : null;
                    if (status != null && statusColors.ContainsKey(status))
                    {
                        e.Appearance.ForeColor = statusColors[status];
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            };
        }

        /// <summary>
        /// Adds conditional row background coloring based on a condition function
        /// </summary>
        /// <param name="gridView">The GridView to customize</param>
        /// <param name="condition">Function that returns color based on row data, or null for no color</param>
        public static void AddConditionalRowColor(GridView gridView, Func<int, Color?> condition)
        {
            if (gridView == null || condition == null)
                return;

            gridView.RowStyle += (s, e) =>
            {
                if (e.RowHandle >= 0)
                {
                    var color = condition(e.RowHandle);
                    if (color.HasValue)
                    {
                        e.Appearance.BackColor = color.Value;
                    }
                }
            };
        }

        /// <summary>
        /// Enables best fit for all columns
        /// </summary>
        /// <param name="gridView">The GridView to format</param>
        public static void BestFitAllColumns(GridView gridView)
        {
            if (gridView == null) return;

            gridView.BestFitColumns();
        }

        /// <summary>
        /// Hides a column by field name
        /// </summary>
        /// <param name="gridView">The GridView</param>
        /// <param name="columnName">The column field name to hide</param>
        public static void HideColumn(GridView gridView, string columnName)
        {
            if (gridView == null || string.IsNullOrEmpty(columnName)) return;

            var column = gridView.Columns[columnName];
            if (column != null)
            {
                column.Visible = false;
            }
        }

        /// <summary>
        /// Sets a column as read-only ID column (hidden by default, shown if needed)
        /// </summary>
        /// <param name="column">The ID column</param>
        /// <param name="visible">Whether to show the column (default: false)</param>
        public static void FormatIdColumn(GridColumn column, bool visible = false)
        {
            if (column == null) return;

            column.Visible = visible;
            column.OptionsColumn.AllowEdit = false;
            column.Width = 60;
        }

        /// <summary>
        /// Standard colors for different states
        /// </summary>
        public static class StandardColors
        {
            public static Color Kritik = Color.FromArgb(255, 77, 79);      // Red
            public static Color Acil = Color.FromArgb(255, 171, 0);        // Orange
            public static Color Normal = Color.FromArgb(76, 175, 80);      // Green
            public static Color Info = Color.FromArgb(33, 150, 243);       // Blue
            public static Color Pasif = Color.FromArgb(158, 158, 158);     // Gray
        }
    }
}
