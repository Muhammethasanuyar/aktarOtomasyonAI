using System.Collections.Generic;
using AktarOtomasyon.Template.Interface.Models;

namespace AktarOtomasyon.Template.Interface
{
    /// <summary>
    /// Template service interface
    /// </summary>
    public interface ITemplateService
    {
        // Template operations
        List<TemplateListeItemDto> TemplateListele(TemplateFiltreDto filtre = null);
        TemplateModel TemplateGetirById(int templateId);
        TemplateModel TemplateGetirByKod(string templateKod);
        string TemplateKaydet(TemplateModel model);
        string TemplatePasifle(int templateId);

        // Version operations
        List<TemplateVersionListeItemDto> VersionListele(int templateId);
        TemplateVersionModel VersionGetir(int templateVersionId);
        string VersionEkle(int templateId, string dosyaAdi, string dosyaYolu, string mimeType, long dosyaBoyut, string sha256, string notlar = null);
        string VersionAktifEt(int templateId, int templateVersionId);
        string VersionArsivle(int templateVersionId);

        // File operations
        TemplateDosyaResultDto DosyaGetir(int templateVersionId);
    }
}
