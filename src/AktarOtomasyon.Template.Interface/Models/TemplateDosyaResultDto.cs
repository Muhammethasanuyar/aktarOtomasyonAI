namespace AktarOtomasyon.Template.Interface.Models
{
    /// <summary>
    /// Template file result DTO (FileSystem mode)
    /// </summary>
    public class TemplateDosyaResultDto
    {
        public string DosyaYolu { get; set; }
        public string DosyaAdi { get; set; }
        public string MimeType { get; set; }
        public long? DosyaBoyut { get; set; }
    }
}
