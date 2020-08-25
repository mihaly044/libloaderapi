using libloaderapi.Domain.Dto;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;

namespace libloaderapi.Controllers
{
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
        [Authorize(Roles = "LibClient")]
        public ActionResult<ulong> Analyze([FromBody] ClientDataReq req)
        {
            return req.Iter switch
            {
                0 => Ok(_analyzerService.Iter0(req.Payload).Result),
                1 => Ok(_analyzerService.Iter1(req.Payload).Result),
                _ => BadRequest("Invalid request"),
            };
        }

        [HttpGet("test")]
        public FileStreamResult Test()
        {
            var data = Encoding.UTF8.GetBytes("Hello!");
            return File(new MemoryStream(data), "application/octet-stream", $"{Guid.NewGuid()}.txt");
        }
    }
}
