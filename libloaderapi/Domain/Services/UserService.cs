using libloaderapi.Domain.Database;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace libloaderapi.Domain.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(AuthenticationRequest request);

        Task<IList<User>> GetUsersAsync();

        Task<User> GetByIdAsync(Guid id);

        IEnumerable<Role> GetRoles(User user);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> AuthenticateAsync(AuthenticationRequest request)
        {
            var hashPass = string.Concat(new SHA1Managed()
                .ComputeHash(Encoding.UTF8.GetBytes(request.Password))
                .Select(b => b.ToString("x2")));

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Password == hashPass);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Issuer = "api.libloader.net",
                Subject = new ClaimsIdentity(GetClaims(user)),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Role> GetRoles(User user)
        {
            return _context.UserRoles
                .Where(u => u.UserId == user.Id)
                .Include(r => r.Role)
                .Select(o => new Role
                {
                    Id = o.Role.Id,
                    Name = o.Role.Name
                });
        }

        public IEnumerable<Claim> GetClaims(User user)
        {
            var roles = GetRoles(user);
            IList<Claim> claims = new List<Claim>();
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Name));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            return claims;
        }
    }
}
