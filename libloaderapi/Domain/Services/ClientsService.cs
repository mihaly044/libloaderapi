using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using libloaderapi.Domain.Database;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Domain.Dto.Client;
using libloaderapi.Utils;
using Microsoft.EntityFrameworkCore;

namespace libloaderapi.Domain.Services
{
    public interface IClientsService
    {
        Task<ClientRegistrationResult> RegisterClient(ClientRegistrationRequest request, Guid userId);

        Task<IEnumerable<Client>> GetAsync();

        Task<IEnumerable<Client>> GetByUserAsync(Guid userId);

        Task<IEnumerable<Client>> GetByClientIdAsync(Guid clientId);

        Task<Client> GetBySha256Async(string sha256);
    }

    public class ClientsService : IClientsService
    {
        private readonly AppDbContext _context;
        private readonly IUsersService _userUsersService;

        public ClientsService(AppDbContext context, IUsersService userUsersService)
        {
            _context = context;
            _userUsersService = userUsersService;
        }

        public async Task<ClientRegistrationResult> RegisterClient(ClientRegistrationRequest request, Guid userId)
        {
            var user = await _userUsersService.GetAsync(userId);
            var result = new ClientRegistrationResult();

            // Do we have a matching user for this request?
            if (user == null)
            {
                result.Success = false;
                result.Message = "This user does not exist";
                return result;
            }

            var clients = await _context.Clients
                .Where(x => x.UserId == userId)
                .ToListAsync();

            // Calculate the SHA256 hash of the client file
            var stream = new MemoryStream();
            await request.File.CopyToAsync(stream);
            stream.Position = 0; // Rewind stream

            var sha256 = string.Concat(new SHA256Managed()
                .ComputeHash(stream)
                .Select(x => x.ToString("x2")));

            // Check if we already have a client with the same hash
            var matchingClient = clients.FirstOrDefault(x => x.Sha256 == sha256 && x.BucketType == request.Bucket);
            if (matchingClient != null)
            {
                result.Success = true;
                result.Skipped = true;
                result.ApiKey = matchingClient.Key;
                result.Message = "Skipped - Client already exists";
                return result;
            }

            if (clients.Count >= Constants.MaxClientsPerUser)
            {
                if (request.OverridePolicy == OverridePolicy.Default)
                {
                    // No override policy has been set
                    result.Success = false;
                    result.Message = "Too many clients and overriding is disallowed";
                    return result;
                }

                var clientsToDelete = clients
                    .OrderBy(x => 
                        request.OverridePolicy == OverridePolicy.DeleteOldestLastUsed 
                            ? x.LastUsed : x.CreatedAt)
                    .First();

                _context.Clients.Remove(clientsToDelete);
            }

            var key = CryptoUtils.CreatePseudoRandomKey();
            await _context.Clients.AddAsync(new Client
            {
                UserId = userId,
                Sha256 = sha256,
                Key = key,
                BucketType = request.Bucket
            });

            await _context.SaveChangesAsync();

            result.ApiKey = key;
            result.Message = "OK";
            result.Success = true;
            return result;
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetByUserAsync(Guid userId)
        {
            return await _context.Clients
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Clients
                .Where(x => x.Id == clientId)
                .ToListAsync();
        }

        public async Task<Client> GetBySha256Async(string sha256)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(x => x.Sha256 == sha256);
        }
    }
}
