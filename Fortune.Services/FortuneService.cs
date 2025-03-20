using Fortune.Models.Configs;
using Fortune.Models.Enums;
using Fortune.Models.SaveObject;
using Fortune.Repositories.Interfaces;
using Fortune.Services.Interfaces;
using Microsoft.Extensions.Options;
using Fortune.Helpers;
using Fortune.Shared.Services.Interfaces;

namespace Fortune.Services
{
    public class FortuneService : IFortuneService
    {
        private readonly ITtsService _ttsService;
        private readonly ITicketAiService _aiService;
        private readonly IFortuneRepository _fortuneRepository;
        private readonly ILoggingService _loggingService;
        private readonly LuckyNumberConfig _luckyNumberConfig;

        public FortuneService(ITicketAiService aiService, IOptions<LuckyNumberConfig> luckyNumberConfig, ITtsService ttsService, IFortuneRepository fortuneRepository, ILoggingService loggingService)
        {
            _aiService = aiService;
            _luckyNumberConfig = luckyNumberConfig.Value;
            _ttsService = ttsService;
            _fortuneRepository = fortuneRepository;
            _loggingService = loggingService;
        }

        public async Task<int> CreateNewFortunes(int fortunesToCreate = 1, EFortuneType eFortuneType = EFortuneType.Generic)
        {
            var fortunesCreated = new List<FortuneModel>();

            for (int i = 0; i < fortunesToCreate; i++)
            {
                var fortune = await GetFortune(eFortuneType);
                if (fortune != null)
                {
                    fortunesCreated.Add(fortune);
                }
            }

            if (fortunesCreated.Count < fortunesToCreate)
            {
                _loggingService.LogInfo("Less fortunes created than requested.");
            }

            var fortunesSaved = await _fortuneRepository.SaveFortunes(fortunesCreated);

            return fortunesSaved;
        }

        public async Task<int> ClaimAndGenerateFortunes(List<Guid> usedFortunes)
        {
            var fortunesClaimed = await _fortuneRepository.MarkFortunesRead(usedFortunes);

            if (fortunesClaimed < usedFortunes.Count)
            {
                _loggingService.LogInfo("Less fortunes claimed than used.");
            }

            var fortunesSaved = await CreateNewFortunes(usedFortunes.Count);

            return fortunesSaved;
        }

        public async Task<List<FortuneModel>> GetFortunes(int fortunesToGet = 1)
        {
            var fortunes = await _fortuneRepository.GetFortunes(fortunesToGet);

            return fortunes;
        }

        private async Task<FortuneModel> GetFortune(EFortuneType fortuneType = EFortuneType.Generic)
        {
            try
            {
                var fortune = new FortuneModel();

                fortune.LongFortune = await _aiService.GetLongFortune(fortuneType);
                fortune.ShortFortune = await _aiService.GetShortFortune(fortuneType, fortune.LongFortune);
                fortune.ImageTopics = await _aiService.GetImageTopics(fortuneType, fortune.LongFortune);
                fortune.FortuneImage = await _aiService.GetTicketImageBlob(fortuneType, fortune.ImageTopics);
                fortune.LuckyNumbers = _luckyNumberConfig.GetLuckyNumbers();
                fortune.Audio = await _ttsService.GetTTSBlob(fortune.ShortFortune);
                return fortune;
            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return null;
            }
        }

        public async Task<FortuneModel> GetRandomFortune()
        {
            var fortune = (await _fortuneRepository.GetFortunes(1)).First();

            var fortuneRead = await _fortuneRepository.MarkFortuneRead(fortune.id);

            if (fortuneRead)
            {
                //var fortuneSaved = await CreateNewFortunes(1);
            }
            else
            {
                _loggingService.LogInfo("Fortune not marked as read.");
            }

            return fortune;
        }

        public async Task<bool> UnreadAllFortunes()
        {
            return await _fortuneRepository.UnreadAllFortunes();
        }
    }
}
