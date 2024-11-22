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
        private readonly IExternalTextAiService _externalTextAiService;
        private readonly IExternalImageAiService _externalImageAiService;
        private readonly LuckyNumberConfig _magicNumberConfig;
        public AiService(IExternalTextAiService externalTextAiService, IExternalImageAiService externalImageAiService, IOptions<LuckyNumberConfig> magicNumberConfig)
        {
            _externalTextAiService = externalTextAiService;
            _externalImageAiService = externalImageAiService;
            _magicNumberConfig = magicNumberConfig.Value;
        }

        public async Task<byte[]> GetImageBlob(EFortuneType eFortuneType, string imageTopics)
        {
            var imageFortuneRequest = eFortuneType.ImageFortuneRequest(imageTopics);

            var imageFortune = await _externalImageAiService.GenerateImageAsync(imageFortuneRequest);

            return imageFortune;
        }

        public async Task<string> GetLongFortune(EFortuneType eFortuneType)
        {
            var longFortuneRequest = eFortuneType.LongFortuneRequest();

            var longFortune = await _externalTextAiService.GenerateTextResponseAsync(longFortuneRequest);

            return longFortune;
        }

        public async Task<string> GetImageTopics(EFortuneType eFortuneType, string longFortune)
        {
            var imageTopicsRequest = eFortuneType.ImageTopicsRequest(longFortune);

            var imageTopics = await _externalTextAiService.GenerateTextResponseAsync(imageTopicsRequest);

            return imageTopics;
        }


        public async Task<string> GetShortFortune(EFortuneType eFortuneType, string longFortune)
        {
            var shortFortuneRequest = eFortuneType.ShortFortuneRequest(longFortune);

            var shortFortune = await _externalTextAiService.GenerateTextResponseAsync(shortFortuneRequest);

            return shortFortune;
        }
    }
}
