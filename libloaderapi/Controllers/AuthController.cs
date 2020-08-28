using System.Net.Mime;
using System.Threading.Tasks;
using libloaderapi.Domain.Dto.Auth;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResult>> Authenticate(AuthRequest request)
        {
            var result = await _authenticationService.AuthenticateAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("authenticate/client")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResult>> Authenticate(ClientAuthRequest request)
        {
            var result = await _authenticationService.AuthenticateAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
