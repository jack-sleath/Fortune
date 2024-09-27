using Fortune.Models.Enums;
using Fortune.Models.SaveObject;
using Fortune.Services.Interfaces;
using Fortune.Helpers;
using Fortune.Models.Configs;
using Microsoft.Extensions.Options;


namespace Fortune.Services
{
    public class AiService : IAiService
    {
        private readonly IExternalAiService _externalAiService;
        private readonly LuckyNumberConfig _magicNumberConfig;
        public AiService(IExternalAiService externalAiService, IOptions<LuckyNumberConfig> magicNumberConfig)
        {
            _externalAiService = externalAiService;
            _magicNumberConfig = magicNumberConfig.Value;
        }

        public async Task<byte[]> GetImageBlob(EFortuneType eFortuneType, string longFortune)
        {
            var imageFortuneRequest = eFortuneType.ImageFortuneRequest(longFortune);

            var imageFortune = await _externalAiService.GenerateImageAsync(imageFortuneRequest);

            return imageFortune;
        }

        public async Task<string> GetLongFortune(EFortuneType eFortuneType)
        {
            var longFortuneRequest = eFortuneType.LongFortuneRequest();

            var longFortune = await _externalAiService.GenerateTextResponseAsync(longFortuneRequest);

            return longFortune;
        }

        public async Task<string> GetShortFortune(EFortuneType eFortuneType, string longFortune)
        {
            var shortFortuneRequest = eFortuneType.ShortFortuneRequest(longFortune);

            var shortFortune = await _externalAiService.GenerateTextResponseAsync(shortFortuneRequest);

            return shortFortune;
        }
    }
}
