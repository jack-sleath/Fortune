using Fortune.Models.SaveObject;
using Fortune.Services.Interfaces;

namespace Fortune.Services
{
    public class AiService : IAiService
    {
        private readonly IExternalAiService _externalAiService;
        public AiService(IExternalAiService externalAiService)
        {
            _externalAiService = externalAiService;
        }

        public FortuneModel GetFortuneData()
        {
            throw new NotImplementedException();
        }

        public string GetImageBlob()
        {
            throw new NotImplementedException();
        }

        public string GetLongFortune()
        {
            throw new NotImplementedException();
        }

        public string GetLogicNumbers()
        {
            throw new NotImplementedException();
        }

        public string GetShortFortune()
        {
            throw new NotImplementedException();
        }
    }
}
