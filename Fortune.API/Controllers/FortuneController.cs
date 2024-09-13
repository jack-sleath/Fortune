using Fortune.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fortune.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FortuneController : ControllerBase
    {
        private readonly IExternalAiService _chatGptService;
        public FortuneController(IExternalAiService chatGptService)
        {
            _chatGptService = chatGptService;
        }

        [HttpGet(Name = "CreateFortune")]
        public IActionResult CreateFortune(Guid oldFortuneId)
        {
            return StatusCode(200);
        }

        [HttpGet("generate-text")]
        public async Task<IActionResult> GenerateText(string prompt)
        {
            var response = await _chatGptService.GetChatGptResponse(prompt);
            return Ok(response);
        }

        [HttpGet("generate-image")]
        public async Task<IActionResult> GenerateImage(string prompt)
        {
            var imageBlob = await _chatGptService.GenerateImageAsync(prompt);
            return File(imageBlob, "image/png");
        }
    }
}
