using Fortune.Models.Enums;
using Fortune.Models.SaveObject;
using Fortune.Services.Interfaces;
using Fortune.Helpers;
using Fortune.Models.Configs;
using Microsoft.Extensions.Options;
using Fortune.Shared.Models.Enums;


namespace Fortune.Services
{
    public abstract class BaseAiService : IBaseAiService
    {
        protected readonly IExternalTextAiService _externalTextAiService;
        protected readonly IExternalImageAiService _externalImageAiService;
        protected readonly IFortuneTextService _fortuneTextService;
       
        public BaseAiService(IExternalTextAiService externalTextAiService, IExternalImageAiService externalImageAiService, IFortuneTextService fortuneTextService)
        {
            _externalTextAiService = externalTextAiService;
            _externalImageAiService = externalImageAiService;
            _fortuneTextService = fortuneTextService;

        }

        public async Task<string> GetLongFortune(EFortuneType eFortuneType)
        {
            var longFortuneRequest = _fortuneTextService.LongFortuneRequest(eFortuneType);

            var longFortune = await _externalTextAiService.GenerateTextResponseAsync(longFortuneRequest);

            return longFortune;
        }

        public async Task<string> GetImageTopics(EFortuneType eFortuneType, string longFortune)
        {
            var imageTopicsRequest = _fortuneTextService.ImageTopicsRequest(eFortuneType, longFortune);

            var imageTopics = await _externalTextAiService.GenerateTextResponseAsync(imageTopicsRequest);

            return imageTopics;
        }


        public async Task<string> GetShortFortune(EFortuneType eFortuneType, string longFortune)
        {
            var shortFortuneRequest = _fortuneTextService.ShortFortuneRequest(eFortuneType, longFortune);

            var shortFortune = await _externalTextAiService.GenerateTextResponseAsync(shortFortuneRequest);

            return shortFortune;
        }
    }
}
