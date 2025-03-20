using Fortune.Models.Enums;
using Fortune.Services.Interfaces;
using Fortune.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services
{
    public class ShortFormAiService : BaseAiService, IShortFormAiService
    {
        public ShortFormAiService(IExternalTextAiService externalTextAiService, IExternalImageAiService externalImageAiService, IFortuneTextService fortuneTextService) : base(externalTextAiService, externalImageAiService, fortuneTextService)
        {

        }

        public async Task<string> GenerateTitle(EFortuneType eFortuneType, string longFortune)
        {
            var titleFortuneRequest = _fortuneTextService.TitleFortuneRequest(eFortuneType, longFortune);

            var titleFortune = await _externalTextAiService.GenerateTextResponseAsync(titleFortuneRequest);

            return titleFortune;
        }

        public async Task<byte[]> GeneratePanningImage(EFortuneType eFortuneType, string imageTopics)
        {
            var imageFortuneRequest = _fortuneTextService.ImageFortuneRequest(eFortuneType, imageTopics);

            var imageFortune = await _externalImageAiService.GenerateImageAsync(imageFortuneRequest, EAspectRatio.Square, 1920);

            return imageFortune;
        }
    }
}
