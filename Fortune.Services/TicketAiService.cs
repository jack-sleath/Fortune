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
    public class TicketAiService : BaseAiService, ITicketAiService
    {

        public TicketAiService(IExternalTextAiService externalTextAiService, IExternalImageAiService externalImageAiService, IFortuneTextService fortuneTextService)
            : base(externalTextAiService, externalImageAiService, fortuneTextService)
        {

        }

        public async Task<byte[]> GetTicketImageBlob(EFortuneType eFortuneType, string imageTopics)
        {
            var imageFortuneRequest = _fortuneTextService.ImageFortuneRequest(eFortuneType, imageTopics);

            var imageFortune = await _externalImageAiService.GenerateImageAsync(imageFortuneRequest, EAspectRatio.SixteenByNine, 180);

            return imageFortune;
        }
    }
}
