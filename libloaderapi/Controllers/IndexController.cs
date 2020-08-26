﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace libloaderapi.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public Task<string> Get()
        {
            // This is going to be funny
            Response.StatusCode = 418;
            return Task.FromResult("I'm a teapot (really!)");
        }
    }
}
