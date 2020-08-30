using libloaderapi.Domain;
using libloaderapi.Domain.Attributes;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = PredefinedRoles.Client)]
    [DevelClientRestricted]
    [Route("[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public AssetsController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status301MovedPermanently)]
        public ActionResult<string> GetResource(string name)
        {
            var uri = _blobService.GetBlobDownloadUrl(name);
            return uri switch
            {
                "" => NotFound("Resource not found"),
                _ => Redirect(uri),
            };
        }
    }
}
