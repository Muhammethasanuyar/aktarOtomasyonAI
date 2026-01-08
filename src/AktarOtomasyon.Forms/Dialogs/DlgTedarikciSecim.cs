using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace AktarOtomasyon.Forms.Dialogs
{
    /// <summary>
    /// Tedarikçi seçimi için popup dialog
    /// </summary>
    public partial class DlgTedarikciSecim : Form
    {
        private GridControl _gridControl;
        private GridView _gridView;
        private List<TedarikciModel> _tedarikciler;
        private PanelControl _pnlBottom;
        private SimpleButton _btnSec;
        private SimpleButton _btnIptal;

        public int? SeciliTedarikciId { get; private set; }
        public TedarikciModel SeciliTedarikci { get; private set; }

        public DlgTedarikciSecim()
        {
            InitializeComponent();
            this.Text = "Tedarikçi Seçimi";
            this.Size = new System.Drawing.Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            // Grid setup
            _gridControl = new GridControl();
            _gridControl.Dock = DockStyle.Fill;
            _gridControl.Parent = this;

            _gridView = new GridView(_gridControl);
            _gridControl.MainView = _gridView;

            // Grid view settings
            _gridView.OptionsBehavior.Editable = false;
            _gridView.OptionsView.ShowGroupPanel = false;
            _gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            _gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            _gridView.DoubleClick += GridView_DoubleClick;

            // Columns
            var colKod = new GridColumn();
            colKod.FieldName = "TedarikciKod";
            colKod.Caption = "Kod";
            colKod.Visible = true;
            colKod.Width = 80;
            _gridView.Columns.Add(colKod);

            var colAdi = new GridColumn();
            colAdi.FieldName = "TedarikciAdi";
            colAdi.Caption = "Tedarikçi Adı";
            colAdi.Visible = true;
            colAdi.Width = 200;
            _gridView.Columns.Add(colAdi);

            var colYetkili = new GridColumn();
            colYetkili.FieldName = "Yetkili";
            colYetkili.Caption = "Yetkili";
            colYetkili.Visible = true;
            colYetkili.Width = 150;
            _gridView.Columns.Add(colYetkili);

            var colTelefon = new GridColumn();
            colTelefon.FieldName = "Telefon";
            colTelefon.Caption = "Telefon";
            colTelefon.Visible = true;
            colTelefon.Width = 120;
            _gridView.Columns.Add(colTelefon);

            var colEmail = new GridColumn();
            colEmail.FieldName = "Email";
            colEmail.Caption = "Email";
            colEmail.Visible = true;
            colEmail.Width = 180;
            _gridView.Columns.Add(colEmail);

            // Bottom panel with buttons
            _pnlBottom = new PanelControl();
            _pnlBottom.Dock = DockStyle.Bottom;
            _pnlBottom.Height = 50;
            _pnlBottom.Parent = this;

            _btnSec = new SimpleButton();
            _btnSec.Text = "Seç";
            _btnSec.Location = new System.Drawing.Point(10, 10);
            _btnSec.Size = new System.Drawing.Size(100, 30);
            _btnSec.Click += BtnSec_Click;
            _btnSec.Parent = _pnlBottom;
            ButtonHelper.ApplySuccessStyle(_btnSec);

            _btnIptal = new SimpleButton();
            _btnIptal.Text = "İptal";
            _btnIptal.Location = new System.Drawing.Point(120, 10);
            _btnIptal.Size = new System.Drawing.Size(100, 30);
            _btnIptal.Click += BtnIptal_Click;
            _btnIptal.Parent = _pnlBottom;
            ButtonHelper.ApplySecondaryStyle(_btnIptal);
        }

        private void LoadData()
        {
            try
            {
                _tedarikciler = InterfaceFactory.Common.TedarikciListele(aktif: true);

                // Fix encoding for Turkish characters
                if (_tedarikciler != null)
                {
                    foreach (var item in _tedarikciler)
                    {
                        item.TedarikciAdi = TextHelper.FixEncoding(item.TedarikciAdi);
                        item.Yetkili = TextHelper.FixEncoding(item.Yetkili ?? "");
                    }
                }

                _gridControl.DataSource = _tedarikciler;

                // Auto-select first row if any
                if (_gridView.RowCount > 0)
                {
                    _gridView.FocusedRowHandle = 0;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Tedarikçiler yüklenirken hata: " + ex.Message);
                ErrorManager.LogMessage("DlgTedarikciSecim LoadData error: " + ex.Message, "DIALOG");
            }
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            BtnSec_Click(sender, e);
        }

        private void BtnSec_Click(object sender, EventArgs e)
        {
            var selectedRow = _gridView.GetFocusedRow() as TedarikciModel;
            if (selectedRow != null)
            {
                SeciliTedarikciId = selectedRow.TedarikciId;
                SeciliTedarikci = selectedRow;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageHelper.ShowWarning("Lütfen bir tedarikçi seçin.");
            }
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
