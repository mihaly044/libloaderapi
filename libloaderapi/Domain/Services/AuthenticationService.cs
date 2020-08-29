using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using libloaderapi.Domain.Database;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace libloaderapi.Domain.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates an user and generates a JWT token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthResult> AuthenticateAsync(AuthRequest request);

        /// <summary>
        /// Authenticates a client and generates a JWT token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthResult> AuthenticateAsync(ClientAuthRequest request);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly byte[] _jwtSecretKey;
        private readonly IUsersService _usersService;

        public AuthenticationService(IConfiguration configuration, IUsersService usersService,
            AppDbContext context)
        {
            _usersService = usersService;
            _jwtSecretKey = Encoding.UTF8.GetBytes(configuration["Secret"]);
            _context = context;
        }

        public async Task<AuthResult> AuthenticateAsync(AuthRequest request)
        {
            var result = new AuthResult();
            var hashPass = string.Concat(new SHA256Managed()
                .ComputeHash(Encoding.UTF8.GetBytes(request.Password))
                .Select(x => x.ToString("x2")));

            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.Name == request.Username && hashPass == x.Password);
            if (user == null)
            {
                result.Success = false;
                result.Message = "Bad username or password";
                return result;
            }

            var claims = await GetClaimsAsync(user);
            result.Message = "OK";
            result.Success = true;
            result.Token = GenerateToken(claims, DateTime.UtcNow.AddMinutes(10));
            return result;
        }

        public async Task<AuthResult> AuthenticateAsync(ClientAuthRequest request)
        {
            var result = new AuthResult();
            var client = await _context.Clients
                .FirstOrDefaultAsync(x => x.Sha256 == request.CryptoId);

            if (client == null)
            {
                result.Success = false;
                result.Message = "401 Unauthorized (1)";
                return result;
            }

            var expectedDigest = string.Concat(new HMACSHA256(Encoding.UTF8.GetBytes(client.Key))
                .ComputeHash(Encoding.UTF8.GetBytes(request.CryptoId))
                .Select(x => x.ToString("x2")));

            if (expectedDigest == request.Digest)
            {
                result.Success = true;
                result.Message = "OK";
                result.Token = GenerateToken(new[]
                {
                    new Claim(ClaimTypes.Role, PredefinedRoles.Client),
                    new Claim(ClaimTypes.Name, client.Id.ToString())
                }, DateTime.UtcNow.AddMinutes(10));
            }
            else
            {
                result.Success = false;
                result.Message = "401 Unauthorized (2)";
            }

            return result;
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var roles = await _usersService.GetRolesAsync(user.Id);
            var claims = roles.
                Select(role => new Claim(ClaimTypes.Role, role.Name))
                .ToList();

            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            return claims;
        }

        private string GenerateToken(IEnumerable<Claim> claims, DateTime? expires = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Issuer = Constants.TokenIssuer,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtSecretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}