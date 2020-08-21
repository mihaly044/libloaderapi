using Microsoft.AspNetCore.Mvc;
using libloaderapi.Domain.Dto;
using libloaderapi.Domain.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using libloaderapi.Domain.Database.Models;
using System;
using System.Linq;
using System.Security.Claims;

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
                return BadRequest();

            return Ok(new AuthenticationResult { Token = token });
        }

        [HttpGet]
        [Authorize(Roles = "LibAdmin")]
        public async Task<ActionResult<IList<User>>> GetAllUser()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("/uuid/{id}")]
        [Authorize(Roles = "LibUser,LibAdmin")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            if (!User.IsInRole("LibAdmin") && User.Identity.Name != id.ToString())
                return Forbid();

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("/roles/uuid/{id}")]
        [Authorize(Roles = "LibUser,LibAdmin")]
        public async Task<ActionResult<IList<Role>>> GetRoles(Guid id)
        {
            if (!User.IsInRole("LibAdmin") && User.Identity.Name != id.ToString())
                return Forbid();

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(await _userService.GetRolesAsync(user));
        }
    }
}
