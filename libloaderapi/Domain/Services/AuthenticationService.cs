using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using libloaderapi.Domain.Database;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto.Auth;
using libloaderapi.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace libloaderapi.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResult> AuthenticateAsync(AuthRequest request);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly byte[] _key;
        private readonly IUsersService _usersService;

        public AuthenticationService(IConfiguration configuration, IUsersService usersService,
            AppDbContext context)
        {
            _usersService = usersService;
            _key = Encoding.UTF8.GetBytes(configuration["Secret"]);
            _context = context;
        }

        public async Task<AuthResult> AuthenticateAsync(AuthRequest request)
        {
            var result = new AuthResult();

            var hashPass = PasswordUtils.GetSha1Hash(request.Password);
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.Name == request.Username && hashPass == x.Password);
            if (user == null)
            {
                result.Success = false;
                result.Message = "Invalid username or password";
                return result;
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Issuer = "api.libloader.net",
                Subject = new ClaimsIdentity(await GetClaimsAsync(user)),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Success = true;
            result.Token = tokenHandler.WriteToken(token);
            return result;
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            var roles = await _usersService.GetRolesAsync(user);
            var claims = roles.
                Select(role => new Claim(ClaimTypes.Role, role.Name))
                .ToList();

            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            return claims;
        }
    }
}