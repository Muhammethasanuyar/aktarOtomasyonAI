namespace AktarOtomasyon.Forms.Screens.Urun
{
    partial class UcUrunDetay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Ürün Bilgi Kontrolleri
        private DevExpress.XtraEditors.LabelControl lblUrunAdi;
        private DevExpress.XtraEditors.LabelControl lblUrunKod;
        private DevExpress.XtraEditors.LabelControl lblBarkod;
        private DevExpress.XtraEditors.LabelControl lblKategori;
        private DevExpress.XtraEditors.LabelControl lblBirim;
        private DevExpress.XtraEditors.LabelControl lblFiyat;
        private DevExpress.XtraEditors.LabelControl lblMevcutStok; // NEW
        private DevExpress.XtraEditors.MemoEdit txtAciklama;

        // Görsel Kontrolleri
        private DevExpress.XtraEditors.PictureEdit picGorsel;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.LabelControl lblGorselSayisi;

        // AI İçerik Kontrolleri
        private DevExpress.XtraTab.XtraTabControl tabAiIcerik;
        private DevExpress.XtraTab.XtraTabPage tabFayda;
        private DevExpress.XtraTab.XtraTabPage tabKullanim;
        private DevExpress.XtraTab.XtraTabPage tabUyari;
        private DevExpress.XtraTab.XtraTabPage tabKombinasyon;

        private DevExpress.XtraEditors.MemoEdit txtFayda;
        private DevExpress.XtraEditors.MemoEdit txtKullanim;
        private DevExpress.XtraEditors.MemoEdit txtUyari;
        private DevExpress.XtraEditors.MemoEdit txtKombinasyon;

        private DevExpress.XtraEditors.LabelControl lblAiDurum;

        // Action Butonları
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnKapat;

        // AI Üretim Butonları
        private DevExpress.XtraEditors.SimpleButton btnAiFayda;
        private DevExpress.XtraEditors.SimpleButton btnAiKullanim;
        private DevExpress.XtraEditors.SimpleButton btnAiUyari;
        private DevExpress.XtraEditors.SimpleButton btnAiKombinasyon;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Ürün Bilgi
            this.lblUrunAdi = new DevExpress.XtraEditors.LabelControl();
            this.lblUrunKod = new DevExpress.XtraEditors.LabelControl();
            this.lblBarkod = new DevExpress.XtraEditors.LabelControl();
            this.lblKategori = new DevExpress.XtraEditors.LabelControl();
            this.lblBirim = new DevExpress.XtraEditors.LabelControl();
            this.lblFiyat = new DevExpress.XtraEditors.LabelControl();
            this.lblMevcutStok = new DevExpress.XtraEditors.LabelControl(); // Added Stock Label
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();

            // Görsel
            this.picGorsel = new DevExpress.XtraEditors.PictureEdit();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.lblGorselSayisi = new DevExpress.XtraEditors.LabelControl();

            // AI İçerik
            this.tabAiIcerik = new DevExpress.XtraTab.XtraTabControl();
            this.tabFayda = new DevExpress.XtraTab.XtraTabPage();
            this.tabKullanim = new DevExpress.XtraTab.XtraTabPage();
            this.tabUyari = new DevExpress.XtraTab.XtraTabPage();
            this.tabKombinasyon = new DevExpress.XtraTab.XtraTabPage();

            this.txtFayda = new DevExpress.XtraEditors.MemoEdit();
            this.txtKullanim = new DevExpress.XtraEditors.MemoEdit();
            this.txtUyari = new DevExpress.XtraEditors.MemoEdit();
            this.txtKombinasyon = new DevExpress.XtraEditors.MemoEdit();

            this.lblAiDurum = new DevExpress.XtraEditors.LabelControl();

            // Butonlar
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();

            // AI Üretim Butonları
            this.btnAiFayda = new DevExpress.XtraEditors.SimpleButton();
            this.btnAiKullanim = new DevExpress.XtraEditors.SimpleButton();
            this.btnAiUyari = new DevExpress.XtraEditors.SimpleButton();
            this.btnAiKombinasyon = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGorsel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAiIcerik)).BeginInit();
            this.tabAiIcerik.SuspendLayout();
            this.tabFayda.SuspendLayout();
            this.tabKullanim.SuspendLayout();
            this.tabUyari.SuspendLayout();
            this.tabKombinasyon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFayda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullanim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUyari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKombinasyon.Properties)).BeginInit();
            this.SuspendLayout();

            //
            // lblUrunAdi
            //
            this.lblUrunAdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblUrunAdi.Appearance.Options.UseFont = true;
            this.lblUrunAdi.Location = new System.Drawing.Point(320, 15);
            this.lblUrunAdi.Name = "lblUrunAdi";
            this.lblUrunAdi.Size = new System.Drawing.Size(100, 32);
            this.lblUrunAdi.TabIndex = 0;
            this.lblUrunAdi.Text = "Ürün Adı";

            //
            // lblUrunKod
            //
            this.lblUrunKod.Location = new System.Drawing.Point(320, 55);
            this.lblUrunKod.Name = "lblUrunKod";
            this.lblUrunKod.Size = new System.Drawing.Size(80, 13);
            this.lblUrunKod.TabIndex = 1;
            this.lblUrunKod.Text = "Kod:";

            //
            // lblBarkod
            //
            this.lblBarkod.Location = new System.Drawing.Point(320, 75);
            this.lblBarkod.Name = "lblBarkod";
            this.lblBarkod.Size = new System.Drawing.Size(80, 13);
            this.lblBarkod.TabIndex = 2;
            this.lblBarkod.Text = "Barkod:";

            //
            // lblKategori
            //
            this.lblKategori.Location = new System.Drawing.Point(320, 95);
            this.lblKategori.Name = "lblKategori";
            this.lblKategori.Size = new System.Drawing.Size(80, 13);
            this.lblKategori.TabIndex = 3;
            this.lblKategori.Text = "Kategori:";

            //
            // lblBirim
            //
            this.lblBirim.Location = new System.Drawing.Point(320, 115);
            this.lblBirim.Name = "lblBirim";
            this.lblBirim.Size = new System.Drawing.Size(80, 13);
            this.lblBirim.TabIndex = 4;
            this.lblBirim.Text = "Birim:";

            //
            // lblFiyat
            //
            this.lblFiyat.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFiyat.Appearance.Options.UseFont = true;
            this.lblFiyat.Location = new System.Drawing.Point(320, 135);
            this.lblFiyat.Name = "lblFiyat";
            this.lblFiyat.Size = new System.Drawing.Size(80, 20);
            this.lblFiyat.TabIndex = 5;
            this.lblFiyat.Text = "Fiyat:";

            //
            // lblMevcutStok
            //
            this.lblMevcutStok.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.lblMevcutStok.Appearance.Options.UseFont = true;
            this.lblMevcutStok.Appearance.Options.UseForeColor = true;
            this.lblMevcutStok.Location = new System.Drawing.Point(320, 155);
            this.lblMevcutStok.Name = "lblMevcutStok";
            this.lblMevcutStok.Size = new System.Drawing.Size(80, 20);
            this.lblMevcutStok.TabIndex = 12;
            this.lblMevcutStok.Text = "Stok:";

            //
            // txtAciklama
            //
            this.txtAciklama.Location = new System.Drawing.Point(320, 185);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.ReadOnly = true;
            this.txtAciklama.Size = new System.Drawing.Size(410, 60);
            this.txtAciklama.TabIndex = 6;

            //
            // picGorsel
            //
            this.picGorsel.Location = new System.Drawing.Point(15, 15);
            this.picGorsel.Name = "picGorsel";
            this.picGorsel.Properties.ShowMenu = false;
            this.picGorsel.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picGorsel.Size = new System.Drawing.Size(290, 290);
            this.picGorsel.TabIndex = 7;

            //
            // btnPrev
            //
            this.btnPrev.Location = new System.Drawing.Point(15, 310);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 8;
            this.btnPrev.Text = "< Önceki";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);

            //
            // btnNext
            //
            this.btnNext.Location = new System.Drawing.Point(230, 310);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "Sonraki >";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            //
            // lblGorselSayisi
            //
            this.lblGorselSayisi.Location = new System.Drawing.Point(130, 315);
            this.lblGorselSayisi.Name = "lblGorselSayisi";
            this.lblGorselSayisi.Size = new System.Drawing.Size(50, 13);
            this.lblGorselSayisi.TabIndex = 10;
            this.lblGorselSayisi.Text = "1 / 1";

            //
            // tabAiIcerik
            //
            this.tabAiIcerik.Location = new System.Drawing.Point(15, 240);
            this.tabAiIcerik.Name = "tabAiIcerik";
            this.tabAiIcerik.SelectedTabPage = this.tabFayda;
            this.tabAiIcerik.Size = new System.Drawing.Size(715, 350);
            this.tabAiIcerik.TabIndex = 11;
            this.tabAiIcerik.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
                this.tabFayda,
                this.tabKullanim,
                this.tabUyari,
                this.tabKombinasyon});

            //
            // tabFayda
            //
            this.tabFayda.Controls.Add(this.txtFayda);
            this.tabFayda.Controls.Add(this.btnAiFayda);
            this.tabFayda.Name = "tabFayda";
            this.tabFayda.Size = new System.Drawing.Size(709, 322);
            this.tabFayda.Text = "Faydalar";

            //
            // txtFayda
            //
            this.txtFayda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFayda.Location = new System.Drawing.Point(0, 30);
            this.txtFayda.Name = "txtFayda";
            this.txtFayda.Properties.ReadOnly = true;
            this.txtFayda.Size = new System.Drawing.Size(709, 292);
            this.txtFayda.TabIndex = 1;
            //
            // btnAiFayda
            //
            this.btnAiFayda.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAiFayda.Location = new System.Drawing.Point(0, 0);
            this.btnAiFayda.Name = "btnAiFayda";
            this.btnAiFayda.Size = new System.Drawing.Size(709, 30);
            this.btnAiFayda.TabIndex = 0;
            this.btnAiFayda.Text = "🤖 AI ile Bilgi Al";
            this.btnAiFayda.Click += new System.EventHandler(this.btnAiFayda_Click);

            //
            // tabKullanim
            //
            this.tabKullanim.Controls.Add(this.txtKullanim);
            this.tabKullanim.Controls.Add(this.btnAiKullanim);
            this.tabKullanim.Name = "tabKullanim";
            this.tabKullanim.Size = new System.Drawing.Size(709, 322);
            this.tabKullanim.Text = "Kullanım";

            //
            // txtKullanim
            //
            this.txtKullanim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKullanim.Location = new System.Drawing.Point(0, 30);
            this.txtKullanim.Name = "txtKullanim";
            this.txtKullanim.Properties.ReadOnly = true;
            this.txtKullanim.Size = new System.Drawing.Size(709, 292);
            this.txtKullanim.TabIndex = 1;
            //
            // btnAiKullanim
            //
            this.btnAiKullanim.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAiKullanim.Location = new System.Drawing.Point(0, 0);
            this.btnAiKullanim.Name = "btnAiKullanim";
            this.btnAiKullanim.Size = new System.Drawing.Size(709, 30);
            this.btnAiKullanim.TabIndex = 0;
            this.btnAiKullanim.Text = "🤖 AI ile Bilgi Al";
            this.btnAiKullanim.Click += new System.EventHandler(this.btnAiKullanim_Click);

            //
            // tabUyari
            //
            this.tabUyari.Controls.Add(this.txtUyari);
            this.tabUyari.Controls.Add(this.btnAiUyari);
            this.tabUyari.Name = "tabUyari";
            this.tabUyari.Size = new System.Drawing.Size(709, 322);
            this.tabUyari.Text = "Uyarılar";

            //
            // txtUyari
            //
            this.txtUyari.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUyari.Location = new System.Drawing.Point(0, 30);
            this.txtUyari.Name = "txtUyari";
            this.txtUyari.Properties.ReadOnly = true;
            this.txtUyari.Size = new System.Drawing.Size(709, 292);
            this.txtUyari.TabIndex = 1;
            //
            // btnAiUyari
            //
            this.btnAiUyari.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAiUyari.Location = new System.Drawing.Point(0, 0);
            this.btnAiUyari.Name = "btnAiUyari";
            this.btnAiUyari.Size = new System.Drawing.Size(709, 30);
            this.btnAiUyari.TabIndex = 0;
            this.btnAiUyari.Text = "🤖 AI ile Bilgi Al";
            this.btnAiUyari.Click += new System.EventHandler(this.btnAiUyari_Click);

            //
            // tabKombinasyon
            //
            this.tabKombinasyon.Controls.Add(this.txtKombinasyon);
            this.tabKombinasyon.Controls.Add(this.btnAiKombinasyon);
            this.tabKombinasyon.Name = "tabKombinasyon";
            this.tabKombinasyon.Size = new System.Drawing.Size(709, 322);
            this.tabKombinasyon.Text = "Kombinasyonlar";

            //
            // txtKombinasyon
            //
            this.txtKombinasyon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKombinasyon.Location = new System.Drawing.Point(0, 30);
            this.txtKombinasyon.Name = "txtKombinasyon";
            this.txtKombinasyon.Properties.ReadOnly = true;
            this.txtKombinasyon.Size = new System.Drawing.Size(709, 292);
            this.txtKombinasyon.TabIndex = 1;
            //
            // btnAiKombinasyon
            //
            this.btnAiKombinasyon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAiKombinasyon.Location = new System.Drawing.Point(0, 0);
            this.btnAiKombinasyon.Name = "btnAiKombinasyon";
            this.btnAiKombinasyon.Size = new System.Drawing.Size(709, 30);
            this.btnAiKombinasyon.TabIndex = 0;
            this.btnAiKombinasyon.Text = "🤖 AI ile Bilgi Al";
            this.btnAiKombinasyon.Click += new System.EventHandler(this.btnAiKombinasyon_Click);

            //
            // lblAiDurum
            //
            this.lblAiDurum.Location = new System.Drawing.Point(15, 600);
            this.lblAiDurum.Name = "lblAiDurum";
            this.lblAiDurum.Size = new System.Drawing.Size(150, 13);
            this.lblAiDurum.TabIndex = 12;
            this.lblAiDurum.Text = "AI Durum: -";

            //
            // btnDuzenle
            //
            this.btnDuzenle.Location = new System.Drawing.Point(550, 595);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(85, 28);
            this.btnDuzenle.TabIndex = 13;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);

            //
            // btnKapat
            //
            this.btnKapat.Location = new System.Drawing.Point(645, 595);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(85, 28);
            this.btnKapat.TabIndex = 14;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            //
            // UcUrunDetay
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnDuzenle);
            this.Controls.Add(this.lblAiDurum);
            this.Controls.Add(this.tabAiIcerik);
            this.Controls.Add(this.lblGorselSayisi);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.picGorsel);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.lblMevcutStok); // NEW
            this.Controls.Add(this.lblFiyat);
            this.Controls.Add(this.lblBirim);
            this.Controls.Add(this.lblKategori);
            this.Controls.Add(this.lblBarkod);
            this.Controls.Add(this.lblUrunKod);
            this.Controls.Add(this.lblUrunAdi);
            this.Name = "UcUrunDetay";
            this.Size = new System.Drawing.Size(750, 640);
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGorsel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAiIcerik)).EndInit();
            this.tabAiIcerik.ResumeLayout(false);
            this.tabFayda.ResumeLayout(false);
            this.tabKullanim.ResumeLayout(false);
            this.tabUyari.ResumeLayout(false);
            this.tabKombinasyon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFayda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullanim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUyari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKombinasyon.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
