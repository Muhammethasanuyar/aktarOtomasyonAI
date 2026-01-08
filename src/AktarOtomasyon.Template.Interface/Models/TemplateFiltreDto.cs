namespace AktarOtomasyon.Template.Interface.Models
{
    /// <summary>
    /// Template filter DTO for Listele method
    /// </summary>
    public class TemplateFiltreDto
    {
        public string Modul { get; set; }
        public bool? Aktif { get; set; }
        public string Arama { get; set; }

        public TemplateFiltreDto()
        {
            Aktif = true; // Default: show active only
        }
    }
}
