using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Standardized message display helper
    /// Sprint 9: Consistent error, success, warning, and confirmation messages
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// Shows a success message to the user
        /// </summary>
        /// <param name="message">Success message text</param>
        /// <param name="title">Dialog title (default: "Başarılı")</param>
        public static void ShowSuccess(string message, string title = "Başarılı")
        {
            XtraMessageBox.Show(message, title,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an error message to the user with log reference
        /// </summary>
        /// <param name="message">Error message text</param>
        /// <param name="title">Dialog title (default: "Hata")</param>
        public static void ShowError(string message, string title = "Hata")
        {
            var fullMessage = message + "\n\nDetaylar sistem loguna kaydedildi.";
            XtraMessageBox.Show(fullMessage, title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a warning message to the user
        /// </summary>
        /// <param name="message">Warning message text</param>
        /// <param name="title">Dialog title (default: "Uyarı")</param>
        public static void ShowWarning(string message, string title = "Uyarı")
        {
            XtraMessageBox.Show(message, title,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows a confirmation dialog (Yes/No)
        /// </summary>
        /// <param name="message">Confirmation question</param>
        /// <param name="title">Dialog title (default: "Onay")</param>
        /// <returns>True if user clicked Yes, false otherwise</returns>
        public static bool ShowConfirmation(string message, string title = "Onay")
        {
            var result = XtraMessageBox.Show(message, title,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        /// <summary>
        /// Shows an information message to the user
        /// </summary>
        /// <param name="message">Information message text</param>
        /// <param name="title">Dialog title (default: "Bilgi")</param>
        public static void ShowInfo(string message, string title = "Bilgi")
        {
            XtraMessageBox.Show(message, title,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
