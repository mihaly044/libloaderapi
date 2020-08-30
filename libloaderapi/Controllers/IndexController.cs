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
            /*Response.StatusCode = StatusCodes.Status501NotImplemented;
            Response.ContentType = "text/plain";
            return new EmptyResult();*/

            return Ok(HttpContext.Connection.RemoteIpAddress.ToString());
        }
    }
}
