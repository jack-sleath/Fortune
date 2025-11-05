using Fortune.Models.Enums;

namespace Fortune.Services.Interfaces
{
    public interface IFortuneTextService
    {
        string ImageFortuneRequest(EFortuneType eFortuneType, string topics);
        string ImageTopicsRequest(EFortuneType eFortuneType, string longFortune);
        string LongFortuneRequest(EFortuneType eFortuneType);
        string ShortFortuneRequest(EFortuneType eFortuneType, string longFortune);
        string TitleFortuneRequest(EFortuneType eFortuneType, string longFortune);
    }
}