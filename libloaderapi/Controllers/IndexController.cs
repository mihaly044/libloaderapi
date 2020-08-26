using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace libloaderapi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
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
