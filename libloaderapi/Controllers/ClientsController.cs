using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto.Auth;
using libloaderapi.Domain.Dto.Client;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAnalyserService _analyserService;

        public ClientsController(IClientsService clientsService, IAuthenticationService authenticationService,
            IAnalyserService analyserService)
        {
            _clientsService = clientsService;
            _authenticationService = authenticationService;
            _analyserService = analyserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = PredefinedRoles.User)]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return Ok(await _clientsService.GetByUserAsync(Guid.Parse(User.Identity.Name!)));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(AuthResult))]
        public async Task<ActionResult<AuthResult>> Authenticate(ClientAuthRequest request)
        {
            var result = await _authenticationService.AuthenticateAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [Authorize(Roles = PredefinedRoles.User)]
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ClientRegistrationResult))]
        public async Task<ActionResult<ClientRegistrationResult>> Register([FromForm] ClientRegistrationRequest request)
        {
            var result = await _clientsService.RegisterClient(request, Guid.Parse(User.Identity.Name!), GetIpAddress());
            if (result.Success)
            {
                return result.Skipped switch
                {
                    true => Ok(result),
                    false => CreatedAtAction(nameof(Get), result)
                };
            }

            return BadRequest(result);
        }

        [Authorize(Roles = PredefinedRoles.Client)]
        [HttpPost("analyze")]
        public ActionResult<ulong> Analyze(AnalyserContext req)
        {
            if (req.Iter == 1)
            {
                if (_analyserService.Iter0(req.Payload, out var result))
                    return Ok(result);
            }
            else if (req.Iter == 1)
            {
                if (_analyserService.Iter1(req.Payload, out var result))
                    return Ok(result);
            }

            return BadRequest("400 Bad request");
        }

        private string GetIpAddress()
        {
            return HttpContext.Request.Headers.TryGetValue("X-Real-IP", out var ipAddress)
                ? ipAddress.ToString()
                : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
