using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libloaderapi.Common;
using libloaderapi.Common.Dto.Client;
using libloaderapi.Domain.Database;
using libloaderapi.Domain.Database.Models;
using libloaderapi.Utils;
using Microsoft.EntityFrameworkCore;

namespace libloaderapi.Domain.Services
{
    public interface IClientsService
    {
        Task<ClientRegistrationResult> RegisterClient(ClientRegistrationRequest request, Guid userId, string ipAddress);

        Task<IEnumerable<Client>> GetAsync();

        Task<IEnumerable<Client>> GetByUserAsync(Guid userId);

        Task<Client> GetByClientIdAsync(Guid clientId);

        Task<IEnumerable<Client>> GetBySha256Async(string sha256);

        Task DeleteByIdAsync(Guid clientId);

        Task DeleteByTagAsync(string tag);

        Task<Client> GetByTagAsync(string tag);
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

        public async Task<ClientRegistrationResult> RegisterClient(ClientRegistrationRequest request, Guid userId, string ipAddress)
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

            // Check if this is a valid exe
            var peStream = request.File.OpenReadStream();
            if (!PeUtils.IsValidExe(peStream))
            {
                result.Success = false;
                result.Message = "Invalid file";
                return result;
            }

            // Check if we have a matching tag
            if (!string.IsNullOrEmpty(request.Tag))
            {
                if ((await GetByTagAsync(request.Tag)) != null)
                {
                    result.Success = false;
                    result.Message = "A client with this tag already exists.";
                    return result;
                }
            }

            // Calculate the SHA256 hash of the client file
            peStream.Position = 0; // Rewind stream
            var sha256Task = CryptoUtils.CalcSha256Hash(peStream);
            var clientsTask = _context.Clients
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var clients = await clientsTask;
            var sha256 = await sha256Task;

            // Check if we already have a client with the same hash
            var matchingClient = clients.FirstOrDefault( x => x.Sha256 == sha256 && x.BucketType == request.Bucket && x.UserId == user.Id);
            if (matchingClient != null)
            {
                result.Success = true;
                result.Skipped = true;
                result.ApiKey = matchingClient.Key;
                result.Message = "Skipped - Client already exists";
                return result;
            }

            if (clients.Count >= (request.Bucket == BucketType.Development 
                ? Constants.MaxClientsInDevBucket : Constants.MaxClientsInProductionBucket))
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

            var key = await CryptoUtils.CreatePseudoRandomKey();
            await _context.Clients.AddAsync(new Client
            {
                UserId = userId,
                Sha256 = sha256,
                Key = key,
                BucketType = request.Bucket,
                RegistrantIp = ipAddress
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

        public async Task<Client> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(x => x.Id == clientId);
        }

        public async Task<IEnumerable<Client>> GetBySha256Async(string sha256)
        {
            return await _context.Clients
                .Where(x => x.Sha256 == sha256).ToListAsync();
        }

        public async Task DeleteByIdAsync(Guid clientId)
        {
            var clientToDelete = await _context.Clients
                .SingleAsync(x => x.Id == clientId);
            _context.Remove(clientToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByTagAsync(string tag)
        {
            var clientToDelete = await _context.Clients
                .SingleAsync(x => x.Tag == tag);
            _context.Remove(clientToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetByTagAsync(string tag)
        {
            return await _context.Clients
                .SingleOrDefaultAsync(x => x.Tag == tag);
        }
    }
}
