using libloaderapi.Domain.Dto;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [Authorize(Roles = "LibClient")]
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IAnalyzerService _analyzerService;

        public ClientController(IAnalyzerService analyzerService)
        {
            _analyzerService = analyzerService;
        }

        [HttpPost("analyze")]
        public ActionResult<ulong> Analyze(ClientDataReq req)
        {
            if (req.Iter == 1)
            {
                if (_analyzerService.Iter0(req.Payload, out var result))
                    return Ok(result);
            }
            else if(req.Iter == 1)
            {
                if (_analyzerService.Iter1(req.Payload, out var result))
                    return Ok(result);
            }

            return BadRequest("400 Bad request");
        }
    }
}
