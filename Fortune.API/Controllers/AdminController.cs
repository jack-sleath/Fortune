using Fortune.Helpers;
using Fortune.Models.Enums;
using Fortune.Services.Interfaces;
using Fortune.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fortune.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly IExternalImageAiService _imageAIService;
        private readonly IExternalTextAiService _textAiService;
        private readonly ITtsService _ttsService;
        private readonly IFortuneService _fortuneService;
        private readonly IQrService _qrService;
        private readonly ILoggingService _loggingService;
        private readonly IFortuneTextService _fortuneTextService;


        public AdminController(
            IExternalImageAiService imageAiService, 
            IExternalTextAiService textAiService, 
            ITtsService ttsService, 
            IFortuneService fortuneService, 
            IQrService qrService,
            ILoggingService loggingService,
            IFortuneTextService fortuneTextService)
        {
            _imageAIService = imageAiService;
            _textAiService = textAiService;
            _ttsService = ttsService;
            _fortuneService = fortuneService;
            _qrService = qrService;
            _loggingService = loggingService;
            _fortuneTextService = fortuneTextService;
        }

        [HttpGet("generateAudio", Name = "GenerateAudioFromText")]
        public IActionResult GenerateAudioFromText(string text)
        {

            var response = _ttsService.GetTTSBlob(text);
            return Ok(response);
        }

        [HttpGet("generateImage", Name = "GenerateImageFromText")]
        public IActionResult GenerateImageFromText(string text)
        {

            var response = _imageAIService.GenerateImageAsync(text);

            return Ok(response);
        }

        [HttpGet("generateLongFortune", Name = "GenerateLongFortune")]
        public IActionResult GenerateLongFortune()
        {
            var longRequest = _fortuneTextService.LongFortuneRequest(EFortuneType.CurrentAffairs);

            var longResponse = _textAiService.GenerateTextResponseAsync(longRequest);

            return Ok(new { Request = longRequest, Response = longResponse });
        }

        [HttpGet("unreadAllFortunes", Name = "UnreadAllFortunes")]
        public IActionResult UnreadAllFortunes()
        {
            var response = _fortuneService.UnreadAllFortunes();

            return Ok(response);
        }

        [HttpGet("generateQr", Name = "GenerateQr")]
        public IActionResult GenerateQr(Guid guid)
        {
            var response = _qrService.GetQRCodeBlobForGuid(guid);

            return Ok(response);
        }
       
        [HttpGet("generateLogs", Name = "GenerateLogs")]
        public IActionResult GenerateLogs(string message)
        {
            var exception = new Exception($"Exception - {message}");

            _loggingService.LogInfo(message);
            _loggingService.LogInfo(exception);
            _loggingService.LogWarning(message);
            _loggingService.LogWarning(exception);
            _loggingService.LogError(message);
            _loggingService.LogError(exception);
            _loggingService.LogCritical(message);
            _loggingService.LogCritical(exception);

            return Ok();
        }

    }
}
