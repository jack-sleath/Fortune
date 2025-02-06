using Fortune.Helpers;
using Fortune.Models.Enums;
using Fortune.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fortune.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase {

        private readonly IExternalImageAiService _imageAIService;
        private readonly IExternalTextAiService _textAiService;
        private readonly ITtsService _ttsService;
        private readonly IFortuneService _fortuneService;


        public AdminController(IExternalImageAiService imageAiService, IExternalTextAiService textAiService, ITtsService ttsService, IFortuneService fortuneService) {
            _imageAIService = imageAiService;
            _textAiService = textAiService;
            _ttsService = ttsService;
            _fortuneService = fortuneService;
        }

        [HttpGet("generateAudio", Name = "GenerateAudioFromText")]
        public IActionResult GenerateAudioFromText(string text) {
            
            var response = _ttsService.GetTTSBlob(text);
            return Ok(response);
        }

        [HttpGet("generateImage", Name = "GenerateImageFromText")]
        public IActionResult GenerateImageFromText(string text) {

            var response = _imageAIService.GenerateImageAsync(text);
            
            return Ok(response);
        }

        [HttpGet("generateLongFortune", Name = "GenerateLongFortune")]
        public IActionResult GenerateLongFortune()
        {
            var longRequest = FortuneHelper.LongFortuneRequest(EFortuneType.CurrentAffairs);

            var longResponse = _textAiService.GenerateTextResponseAsync(longRequest);

            return Ok(new { Request = longRequest, Response = longResponse });
        }

        [HttpGet("unreadAllFortunes", Name = "UnreadAllFortunes")]
        public IActionResult UnreadAllFortunes()
        {
            var response = _fortuneService.UnreadAllFortunes();

            return Ok(response);
        }
    }
}
