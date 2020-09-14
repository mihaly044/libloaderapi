using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using libloaderapi.Common.Dto.Auth;
using libloaderapi.Common.Dto.Client;
using libloaderapi.Domain;
using libloaderapi.Domain.Attributes;
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
        public async Task<ActionResult<IEnumerable<ClientDtoObject>>> Get()
        {
            var clients = await _clientsService.GetByUserAsync(Guid.Parse(User.Identity.Name!));
            return Ok(clients.Select(c => new ClientDtoObject
            {
                Id = c.Id,
                BucketType = c.BucketType,
                CreatedAt = c.CreatedAt,
                ApiKey = null,
                LastUsed = c.LastUsed,
                RegistrantIp = c.RegistrantIp,
                Digest = null,
                Tag = c.Tag
            }));
        }

        [Authorize(Roles = PredefinedRoles.User)]
        [HttpDelete("id/{clientId}")]
        public async Task<ActionResult> DeleteClientById(Guid clientId)
        {
            var client = await _clientsService.GetByClientIdAsync(clientId);
            if (client == null)
                return NotFound();

            if (client.UserId != Guid.Parse(User.Identity.Name!))
                return Unauthorized();

            await _clientsService.DeleteByIdAsync(clientId);
            return Ok();
        }

        [Authorize(Roles = PredefinedRoles.User)]
        [HttpDelete("tag/{tag}")]
        public async Task<ActionResult> DeleteClientByTag(string tag)
        {
            var client = await _clientsService.GetByTagAsync(tag);
            if (client == null)
                return NotFound();

            if (client.UserId != Guid.Parse(User.Identity.Name!))
                return Unauthorized();

            await _clientsService.DeleteByTagAsync(tag);
            return Ok();
        }

        [Authorize(Roles = PredefinedRoles.User)]
        [HttpPost("tag")]
        public async Task<ActionResult> EditClientTag(ClientDtoObject dtoObject)
        {
            var client = await _clientsService.GetByClientIdAsync(dtoObject.Id);
            if (client == null)
                return NotFound();

            if (client.UserId != Guid.Parse(User.Identity.Name!))
                return Unauthorized();

            var clientsWithThisTag = await _clientsService.GetByTagAsync(dtoObject.Tag);
            if (clientsWithThisTag != null)
                return Conflict("A client with this tag already exists.");

            await _clientsService.UpdateTagAsync(dtoObject.Id, dtoObject.Tag);
            return Ok();
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
        
        [DevelClientRestricted]
        [Authorize(Roles = PredefinedRoles.Client)]
        [HttpPost("analyze")]
        public ActionResult<ulong> Analyze(AnalyserContext req)
        {
            if (req.Iter == 0)
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
