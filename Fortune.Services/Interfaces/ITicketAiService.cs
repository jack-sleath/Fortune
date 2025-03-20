using Fortune.Models.Enums;

namespace Fortune.Services.Interfaces
{
    public interface ITicketAiService : IBaseAiService
    {
        Task<byte[]> GetTicketImageBlob(EFortuneType eFortuneType, string imageTopics);
    }
}