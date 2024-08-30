using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fortune.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FortuneController : ControllerBase
    {

        public FortuneController()
        {
            
        }

        [HttpGet(Name = "CreateFortune")]
        public IActionResult CreateFortune(Guid oldFortuneId)
        {
            return StatusCode(200);
        }
    }
}
