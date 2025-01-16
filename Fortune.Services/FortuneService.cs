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
        private readonly IQrService _qrService;
        private readonly IAiService _aiService;
        private readonly IFortuneRepository _fortuneRepository;
        private readonly LuckyNumberConfig _luckyNumberConfig;

        public FortuneService(IAiService aiService, IOptions<LuckyNumberConfig> luckyNumberConfig, IQrService qrService, IFortuneRepository fortuneRepository)
        {
            _aiService = aiService;
            _luckyNumberConfig = luckyNumberConfig.Value;
            _qrService = qrService;
            //_ttsService = ttsService;
            _fortuneRepository = fortuneRepository;
        }

        public async Task<bool> CreateNewFortune()
        {
            var fortuneType = EFortuneType.Generic;
            var fortune = new FortuneModel();

            fortune.LongFortune = await _aiService.GetLongFortune(fortuneType);
            fortune.ShortFortune = await _aiService.GetShortFortune(fortuneType, fortune.LongFortune);
            fortune.ImageTopics = await _aiService.GetImageTopics(fortuneType, fortune.LongFortune);
            fortune.FortuneImage = await _aiService.GetImageBlob(fortuneType, fortune.ImageTopics);
            fortune.LuckyNumbers = _luckyNumberConfig.GetLuckyNumbers();
            //fortune.QrImage = await _qrService.GetQRCodeBlobForGuid(fortune.id);

            return await _fortuneRepository.SaveFortune(fortune);
            //return true;
        }

        public async Task<List<FortuneModel>> GetFortunes()
        {
            var list = new List<FortuneModel>();

            for (int i = 0; i < 15; i++)
            {
                var blah = await GetFortune();
                list.Add(blah);
            }

            return list;
        }

        private async Task<FortuneModel> GetFortune()
        {

            var fortuneType = EFortuneType.Generic;
            var fortune = new FortuneModel();

            fortune.LongFortune = await _aiService.GetLongFortune(fortuneType);
            fortune.ShortFortune = await _aiService.GetShortFortune(fortuneType, fortune.LongFortune);
            fortune.ImageTopics = await _aiService.GetImageTopics(fortuneType, fortune.LongFortune);
            fortune.FortuneImage = await _aiService.GetImageBlob(fortuneType, fortune.ImageTopics);
            fortune.LuckyNumbers = _luckyNumberConfig.GetLuckyNumbers();
            fortune.QrImage = await _qrService.GetQRCodeBlobForGuid(fortune.id);

            return fortune;
        }

        public bool SaveUsedFortune()
        {
            throw new NotImplementedException();
        }
    }
}
