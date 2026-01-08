using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Audit.Interface.Models;
using DevExpress.XtraGrid.Views.Grid;

namespace AktarOtomasyon.Forms.Screens.Security
{
    /// <summary>
    /// Audit log viewer user control
    /// View audit logs with filtering
    /// </summary>
    public partial class UcAuditViewer : UcBase
    {
        public UcAuditViewer()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Load += UcAuditViewer_Load;
        }

        private void UcAuditViewer_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeFilters();
                SetDefaultFilters();
                LoadAuditLogs();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("UcAuditViewer_Load error: {0}", ex.Message), "AUDIT");
                DMLManager.ShowError(string.Format("Yükleme hatası: {0}", ex.Message));
            }
        }

        private void InitializeFilters()
        {
            try
            {
                // Entity dropdown
                cmbEntity.Properties.Items.Clear();
                cmbEntity.Properties.Items.Add("(Tümü)");
                cmbEntity.Properties.Items.AddRange(new string[]
                {
                    "KULLANICI",
                    "ROL",
                    "URUN",
                    "STOK",
                    "SIPARIS",
                    "AI_ICERIK"
                });
                cmbEntity.SelectedIndex = 0;

                // Action dropdown
                cmbAction.Properties.Items.Clear();
                cmbAction.Properties.Items.Add("(Tümü)");
                cmbAction.Properties.Items.AddRange(new string[]
                {
                    "INSERT",
                    "UPDATE",
                    "DELETE"
                });
                cmbAction.SelectedIndex = 0;

                // User lookup - load all users
                LoadUsers();

                // Top N spinner
                spnTop.Properties.MinValue = 10;
                spnTop.Properties.MaxValue = 10000;
                spnTop.Properties.Increment = 100;
                spnTop.Value = 1000;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("InitializeFilters error: {0}", ex.Message), "AUDIT");
            }
        }

        private void LoadUsers()
        {
            try
            {
                var users = InterfaceFactory.Security.KullaniciListele(null);

                lkpKullanici.Properties.DataSource = users;
                lkpKullanici.Properties.DisplayMember = "AdSoyad";
                lkpKullanici.Properties.ValueMember = "KullaniciId";
                lkpKullanici.Properties.NullText = "(Tüm Kullanıcılar)";
                lkpKullanici.EditValue = null;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadUsers error: {0}", ex.Message), "AUDIT");
            }
        }

        private void SetDefaultFilters()
        {
            // Default: Last 7 days
            dteBaslangic.EditValue = DateTime.Now.AddDays(-7);
            dteBitis.EditValue = DateTime.Now;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAuditLogs();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnAra_Click error: {0}", ex.Message), "AUDIT");
                DMLManager.ShowError(string.Format("Arama hatası: {0}", ex.Message));
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            try
            {
                SetDefaultFilters();
                cmbEntity.SelectedIndex = 0;
                cmbAction.SelectedIndex = 0;
                lkpKullanici.EditValue = null;
                spnTop.Value = 1000;

                LoadAuditLogs();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnTemizle_Click error: {0}", ex.Message), "AUDIT");
            }
        }

        private void LoadAuditLogs()
        {
            try
            {
                var filter = new AuditFiltre
                {
                    Entity = cmbEntity.SelectedIndex > 0 ? cmbEntity.Text : null,
                    Action = cmbAction.SelectedIndex > 0 ? cmbAction.Text : null,
                    KullaniciId = lkpKullanici.EditValue != null ? (int?)lkpKullanici.EditValue : null,
                    BaslangicTarih = dteBaslangic.EditValue != null ? (DateTime?)dteBaslangic.EditValue : null,
                    BitisTarih = dteBitis.EditValue != null ? (DateTime?)dteBitis.EditValue : null,
                    Top = (int)spnTop.Value
                };

                var logs = InterfaceFactory.Audit.AuditListele(filter);

                grdAuditLog.DataSource = logs;
                gvAuditLog.BestFitColumns();

                // Show count in status
                lblKayitSayisi.Text = string.Format("Toplam: {0} kayıt", logs != null ? logs.Count : 0);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadAuditLogs error: {0}", ex.Message), "AUDIT");
                DMLManager.ShowError(string.Format("Kayıtlar yüklenemedi: {0}", ex.Message));
            }
        }

        private void gvAuditLog_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gvAuditLog.FocusedRowHandle < 0)
                    return;

                var row = gvAuditLog.GetFocusedRow() as AuditListeItemDto;
                if (row != null)
                {
                    ShowAuditDetail(row.AuditId);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("gvAuditLog_DoubleClick error: {0}", ex.Message), "AUDIT");
                DMLManager.ShowError(string.Format("Detay gösterilirken hata: {0}", ex.Message));
            }
        }

        private void ShowAuditDetail(int auditId)
        {
            try
            {
                var detail = InterfaceFactory.Audit.AuditGetir(auditId);
                if (detail == null)
                {
                    DMLManager.ShowWarning("Audit detayı bulunamadı.");
                    return;
                }

                // Show detail in memo
                memoJsonDetay.Text = string.Format(
                    "Audit ID: {0}\n" +
                    "Entity: {1}\n" +
                    "Entity ID: {2}\n" +
                    "Action: {3}\n" +
                    "Kullanıcı: {4} ({5})\n" +
                    "Tarih: {6}\n" +
                    "\nJSON Detay:\n" +
                    "{7}",
                    detail.AuditId,
                    detail.Entity,
                    detail.EntityId,
                    detail.Action,
                    detail.AdSoyad,
                    detail.KullaniciAdi,
                    detail.CreatedAt,
                    detail.JsonData ?? "(Boş)"
                );

                // Make detail panel visible
                splitContainerControl1.Panel2.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("ShowAuditDetail error: {0}", ex.Message), "AUDIT");
                throw;
            }
        }

        private void btnDetayKapat_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainerControl1.Panel2.Visible = false;
                memoJsonDetay.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnDetayKapat_Click error: {0}", ex.Message), "AUDIT");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
                    saveDialog.FileName = string.Format("AuditLog_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        grdAuditLog.ExportToXlsx(saveDialog.FileName);
                        DMLManager.ShowInfo("Dışa aktarma başarılı.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("btnExport_Click error: {0}", ex.Message), "AUDIT");
                DMLManager.ShowError(string.Format("Dışa aktarma hatası: {0}", ex.Message));
            }
        }
    }
}
