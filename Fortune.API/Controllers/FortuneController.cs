using Fortune.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fortune.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FortuneController : ControllerBase
    {
        private readonly IFortuneService _fortuneService;
        public FortuneController(IFortuneService fortuneService)
        {
            _fortuneService = fortuneService;
        }

        [HttpGet(Name = "CreateFortune")]
        public IActionResult CreateFortune(Guid oldFortuneId)
        {
            return StatusCode(200);
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateFortune()
        {
            var response = await _fortuneService.GetFortunes();
            return Ok(response);
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateFortune()
        {
            var response = await _fortuneService.CreateNewFortune();
            return Ok(response);
        }
    }
}
