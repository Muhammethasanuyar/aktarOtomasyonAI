using System.Collections.Generic;
using AktarOtomasyon.Common.Interface.Models;

namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// System settings service interface
    /// </summary>
    public interface ISystemSettingService
    {
        List<SystemSettingDto> SettingListele();
        SystemSettingDto SettingGetir(string key);
        string SettingKaydet(SystemSettingDto dto);
    }
}
