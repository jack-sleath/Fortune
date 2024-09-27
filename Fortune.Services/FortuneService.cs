using Fortune.Models.Configs;
using Fortune.Models.Enums;
using Fortune.Models.SaveObject;
using Fortune.Repositories.Interfaces;
using Fortune.Services.Interfaces;
using Microsoft.Extensions.Options;
using Fortune.Helpers;

namespace Fortune.Services
{
    public class FortuneService : IFortuneService
    {
        //private readonly ITtsService _ttsService;
        //private readonly IQrService _qrService;
        private readonly IAiService _aiService;
        //private readonly IFortuneRepository _fortuneRepository;
        private readonly LuckyNumberConfig _luckyNumberConfig;

        public FortuneService(IAiService aiService, IOptions<LuckyNumberConfig> luckyNumberConfig/*, IQrService qrService, ITtsService ttsService, IFortuneRepository fortuneRepository*/)
        {
            _aiService = aiService;
            _luckyNumberConfig = luckyNumberConfig.Value;
            //_qrService = qrService;
            //_ttsService = ttsService;
            //_fortuneRepository = fortuneRepository;
        }

        public bool CreateNewFortune()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FortuneModel>> GetFortunes()
        {
            var fortuneType = EFortuneType.Generic;
            var fortune = new FortuneModel();

            fortune.LongFortune = await _aiService.GetLongFortune(fortuneType);
            fortune.ShortFortune = await _aiService.GetShortFortune(fortuneType, fortune.LongFortune);
            fortune.LuckyNumbers = _luckyNumberConfig.GetLuckyNumbers();


            return new List<FortuneModel> { fortune };
        }

        public bool SaveUsedFortune()
        {
            throw new NotImplementedException();
        }
    }
}
