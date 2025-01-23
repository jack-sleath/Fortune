using Fortune.Models.SaveObject;
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

        [HttpGet("create", Name = "CreateFortunes")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateFortunes(int fortunesToCreate)
        {
            var response = await _fortuneService.CreateNewFortunes(fortunesToCreate);
            return Ok(response);
        }

        [HttpGet("get", Name = "GetFortunes")]
        [ProducesResponseType(typeof(List<FortuneModel>), 200)]
        public async Task<IActionResult> GetFortunes(int fortunesToGet)
        {
            var response = await _fortuneService.GetFortunes(fortunesToGet);
            return Ok(response);
        }

        [HttpGet("getRandom", Name = "GetRandom")]
        [ProducesResponseType(typeof(FortuneModel), 200)]
        public async Task<IActionResult> GetRandom()
        {
            var response = await _fortuneService.GetRandomFortune();
            return Ok(response);
        }

        [HttpPost("generate", Name = "GenerateFortunes")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GenerateFortunes(List<Guid> usedFortunes)
        {
            var result = await _fortuneService.ClaimAndGenerateFortunes(usedFortunes);
            return Ok();
        }
    }
}
