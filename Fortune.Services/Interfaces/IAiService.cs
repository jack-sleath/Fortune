using Fortune.Models.Enums;
using Fortune.Models.SaveObject;

namespace Fortune.Services.Interfaces
{
    public interface IAiService
    { 
        Task<string> GetLongFortune(EFortuneType eFortuneType);
        Task<string> GetShortFortune(EFortuneType eFortuneType, string longFortune);
        Task<byte[]> GetImageBlob(EFortuneType eFortuneType, string longFortune);
    }
}
