using Fortune.Models.Enums;

namespace Fortune.Services.Interfaces
{
    public interface IShortFormAiService : IBaseAiService
    {
        Task<byte[]> GeneratePanningImage(EFortuneType eFortuneType, string imageTopics);
        Task<string> GenerateTitle(EFortuneType eFortuneType, string longFortune);
    }
}