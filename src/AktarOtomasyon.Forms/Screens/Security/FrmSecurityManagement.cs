using System;
using AktarOtomasyon.Forms.Base;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Security Management form - UC-Only pattern
    /// Thin wrapper for UcSecurityManagement
    /// </summary>
    public partial class FrmSecurityManagement : FrmBase
    {
        public FrmSecurityManagement(string ekranKod) : base(ekranKod)
        {
            InitializeComponent();
        }
    }
}
