using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Audit Viewer form - UC-Only pattern
    /// Thin wrapper for UcAuditViewer
    /// </summary>
    public partial class FrmAuditViewer : FrmBase
    {
        public FrmAuditViewer(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }
    }
}
