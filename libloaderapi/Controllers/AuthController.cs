using System.Threading.Tasks;
using libloaderapi.Domain.Dto.Auth;
using libloaderapi.Domain.Services;
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

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthRequest>> Authenticate(AuthRequest request)
        {
            var result = await _authenticationService.AuthenticateAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
