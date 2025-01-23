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
        private readonly ITtsService _ttsService;
        private readonly IQrService _qrService;
        private readonly IAiService _aiService;
        private readonly IFortuneRepository _fortuneRepository;
        private readonly LuckyNumberConfig _luckyNumberConfig;

        public FortuneService(IAiService aiService, IOptions<LuckyNumberConfig> luckyNumberConfig, IQrService qrService, ITtsService ttsService, IFortuneRepository fortuneRepository)
        {
            _aiService = aiService;
            _luckyNumberConfig = luckyNumberConfig.Value;
            _qrService = qrService;
            _ttsService = ttsService;
            _fortuneRepository = fortuneRepository;
        }



        public async Task<int> CreateNewFortunes(int fortunesToCreate = 1)
        {
            var fortunesCreated = new List<FortuneModel>();

            for (int i = 0; i < fortunesToCreate; i++)
            {
                var fortune = await GetFortune();
                if (fortune != null)
                {
                    fortunesCreated.Add(fortune);
                }
            }

            //log this fortunesCreated.Count if less than fortunesToCreate

            var fortunesSaved = await _fortuneRepository.SaveFortunes(fortunesCreated);

            return fortunesSaved;
        }

        public async Task<int> ClaimAndGenerateFortunes(List<Guid> usedFortunes)
        {
            var fortunesClaimed = await _fortuneRepository.MarkFortunesRead(usedFortunes);

            //log this fortunesClaimed if less than usedFortunes.Count

            var fortunesSaved = await CreateNewFortunes(usedFortunes.Count);

            return fortunesSaved;
        }

        public async Task<List<FortuneModel>> GetFortunes(int fortunesToGet = 1)
        {
            var fortunes = await _fortuneRepository.GetFortunes(fortunesToGet);

            return fortunes;
        }

        private async Task<FortuneModel> GetFortune()
        {
            try
            {
                var fortuneType = EFortuneType.Generic;
                var fortune = new FortuneModel();

                fortune.LongFortune = await _aiService.GetLongFortune(fortuneType);
                fortune.ShortFortune = await _aiService.GetShortFortune(fortuneType, fortune.LongFortune);
                fortune.ImageTopics = await _aiService.GetImageTopics(fortuneType, fortune.LongFortune);
                fortune.FortuneImage = await _aiService.GetImageBlob(fortuneType, fortune.ImageTopics);
                fortune.LuckyNumbers = _luckyNumberConfig.GetLuckyNumbers();
                fortune.Audio = await _ttsService.GetTTSBlob(fortune.ShortFortune);
                //fortune.QrImage = await _qrService.GetQRCodeBlobForGuid(fortune.id);
                return fortune;
            }
            catch (Exception ex)
            {
                //log this
                return null;
            }
        }

        public async Task<FortuneModel> GetRandomFortune()
        {
            var fortune = (await _fortuneRepository.GetFortunes(1)).First();

            var fortuneRead = await _fortuneRepository.MarkFortuneRead(fortune.id);

            //log this if fortune not read

            if (fortuneRead) 
            {
                var fortuneSaved = await CreateNewFortunes(1);
                //log if fortune saved does not equal 1
            }

            return fortune;
        }

        public async Task<bool> UnreadAllFortunes()
        {
           return await _fortuneRepository.UnreadAllFortunes();
        }
    }
}
