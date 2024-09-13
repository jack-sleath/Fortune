using Fortune.Models.SaveObject;

namespace Fortune.Services.Interfaces
{
    public interface IAiService
    {
        FortuneModel GetFortuneData();
        string GetLongFortune();
        string GetShortFortune();
        string GetImageBlob();
        string GetLuckyNumbers();
    }
}
