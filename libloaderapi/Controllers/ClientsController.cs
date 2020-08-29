using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto.Client;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = PredefinedRoles.User)]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return Ok(await _clientsService.GetByUserAsync(Guid.Parse(User.Identity.Name!)));
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ClientRegistrationResult))]
        public async Task<ActionResult<ClientRegistrationResult>> Register([FromForm] ClientRegistrationRequest request)
        {
            var result = await _clientsService.RegisterClient(request, Guid.Parse(User.Identity.Name!));
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
    }
}
