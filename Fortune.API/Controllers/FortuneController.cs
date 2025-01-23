using Fortune.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet("create", Name = "CreateFortune")]
        public async Task<IActionResult> CreateFortune()
        {
            var response = await _fortuneService.CreateNewFortune();
            return Ok(response);
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateFortune(List<Guid> usedFortunes)
        {
            var result = await _fortuneService.ClaimAndGenerateFortunes(usedFortunes);
            return Ok();
        }
    }
}
