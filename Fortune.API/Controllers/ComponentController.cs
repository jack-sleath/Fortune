using Fortune.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fortune.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ComponentController : ControllerBase {

        private readonly IExternalImageAiService _imageAIService;
        private readonly ITtsService _ttsService;


        public ComponentController(IExternalImageAiService imageAiService, ITtsService ttsService) {
            _imageAIService = imageAiService;
            _ttsService = ttsService;
        }

        [HttpGet("generateAudio", Name = "GenerateAudioFromText")]
        public IActionResult GenerateAudioFromText(string text) {
            
            var response = _ttsService.GetTTSBlob(text);
            return StatusCode(200);
        }

        [HttpGet("generateImage", Name = "GenerateImageFromText")]
        public IActionResult GenerateImageFromText(string text) {

            var response = _imageAIService.GenerateImageAsync(text);
            
            return Ok(response);
        }
    }
}
