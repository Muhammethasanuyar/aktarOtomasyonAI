using System;

namespace AktarOtomasyon.Common.Interface.Models
{
    /// <summary>
    /// System setting DTO
    /// </summary>
    public class SystemSettingDto
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string Aciklama { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Sprint 8: Configuration source tracking
        public string ConfigSource { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
