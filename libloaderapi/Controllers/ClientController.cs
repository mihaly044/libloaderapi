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
        public ActionResult<ulong> Analyze([FromBody] ClientDataReq req)
        {
            return req.Iter switch
            {
                0 => Ok(_analyzerService.Iter0(req.Payload).Result),
                1 => Ok(_analyzerService.Iter1(req.Payload).Result),
                _ => BadRequest("Invalid request"),
            };
        }
    }
}
