﻿using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [Authorize(Roles = "LibClient")]
    [Route("[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public AssetsController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("get/{name}")]
        public ActionResult<string> GetResource(string name)
        {
            var uri = _blobService.GetBlobDownloadURI(name);
            return uri switch
            {
                "" => NotFound(),
                _ => Ok(uri),
            };
        }
    }
}