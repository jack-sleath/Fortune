using Fortune.Models.Enums;
using Fortune.Models.SaveObject;

namespace Fortune.Services.Interfaces
{
    public interface IBaseAiService
    { 
        Task<string> GetLongFortune(EFortuneType eFortuneType);
        Task<string> GetImageTopics(EFortuneType eFortuneType, string longFortune);
        Task<string> GetShortFortune(EFortuneType eFortuneType, string longFortune);
    }
}
