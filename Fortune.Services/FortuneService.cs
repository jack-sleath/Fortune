using Fortune.Models.SaveObject;
using Fortune.Repositories.Interfaces;
using Fortune.Services.Interfaces;

namespace Fortune.Services
{
    public class FortuneService : IFortuneService
    {
        private readonly ITtsService _ttsService;
        private readonly IQrService _qrService;
        private readonly IAiService _aiService;
        private readonly IFortuneRepository _fortuneRepository;

        public FortuneService(IAiService aiService, IQrService qrService, ITtsService ttsService, IFortuneRepository fortuneRepository)
        {
            _aiService = aiService;
            _qrService = qrService;
            _ttsService = ttsService;
            _fortuneRepository = fortuneRepository;
        }

        public bool CreateNewFortune()
        {
            throw new NotImplementedException();
        }

        public List<FortuneModel> GetFortunes()
        {
            throw new NotImplementedException();
        }

        public bool SaveUsedFortune()
        {
            throw new NotImplementedException();
        }
    }
}
