using libloaderapi.Domain.Dto.Client;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [Authorize(Roles = PredefinedRoles.Client)]
    [Route("[controller]")]
    [ApiController]
    public class AnalyserController : ControllerBase
    {
        private readonly IAnalyserService _analyserService;

        public AnalyserController(IAnalyserService analyserService)
        {
            _analyserService = analyserService;
        }

        [HttpPost("analyze")]
        public ActionResult<ulong> Analyze(AnalyserContext req)
        {
            if (req.Iter == 1)
            {
                if (_analyserService.Iter0(req.Payload, out var result))
                    return Ok(result);
            }
            else if(req.Iter == 1)
            {
                if (_analyserService.Iter1(req.Payload, out var result))
                    return Ok(result);
            }

            return BadRequest("400 Bad request");
        }

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return Ok("test");
        }
    }
}
