using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace libloaderapi.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            Response.ContentType = "text/plain";
            return new EmptyResult();
        }
    }
}
