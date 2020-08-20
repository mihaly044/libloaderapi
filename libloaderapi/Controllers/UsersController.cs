using Microsoft.AspNetCore.Mvc;
using libloaderapi.Domain.Dto;
using libloaderapi.Domain.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using libloaderapi.Domain.Database.Models;
using System;

namespace libloaderapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResult>> AuthenticateAsync(AuthenticationRequest request)
        {
            var token = await _userService.AuthenticateAsync(request);
            if (token == null)
                return BadRequest(new AuthenticationResult { Response = "400 Username or password incorrect" });

            return Ok(new AuthenticationResult { Response = "200 OK", Token = token });
        }

        [HttpGet("user/all")]
        [Authorize(Roles = "LibAdmin")]
        public async Task<ActionResult<IList<User>>> GetAllUser()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("user/{id}")]
        [Authorize(Roles = "LibUser")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");
            return user;
        }
    }
}
