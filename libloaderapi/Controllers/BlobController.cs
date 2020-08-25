using Microsoft.AspNetCore.Mvc;
using System;

namespace libloaderapi.Controllers
{
    [Route("[controller]")]
    public class BlobController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            throw new NotImplementedException();
        }
    }
}
