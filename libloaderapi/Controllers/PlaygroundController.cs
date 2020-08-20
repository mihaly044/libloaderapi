using Microsoft.AspNetCore.Mvc;
using libloaderapi.Domain.Dto;
using libloaderapi.Domain.Services;
using libloaderapi.Domain.Database;
using Microsoft.EntityFrameworkCore;
using libloaderapi.Domain.Database.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace libloaderapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaygroundController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public PlaygroundController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResult>> AuthenticateAsync(AuthenticationRequest request)
        {
            var token = await _userService.AuthenticateAsync(request);
            if (token == null)
                return BadRequest(new AuthenticationResult { Response = "400 Username or password incorrect" });

            return Ok(new AuthenticationResult { Response = "200 OK", Token = token });
        }
    }
}
