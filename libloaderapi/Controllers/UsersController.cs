using System;
using System.Net.Mime;
using System.Threading.Tasks;
using libloaderapi.Domain.Attributes;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto.Auth;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersService _usersService;

        public UsersController(IAuthenticationService authenticationService, IUsersService usersService)
        {
            _authenticationService = authenticationService;
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(AuthResult))]
        public async Task<ActionResult<AuthResult>> Authenticate(AuthRequest request)
        {
            var result = await _authenticationService.AuthenticateAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("myself")]
        [Role(PredefinedRoles.User, PredefinedRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<User>> GetMyself()
        {
            var result = await _usersService.GetAsync(Guid.Parse(User.Identity.Name!));
            if (result != null)
                return Ok(result);
            return NotFound();
        }
    }
}
