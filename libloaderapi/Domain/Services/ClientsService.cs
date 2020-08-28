using System;
using System.Collections.Generic;
using System.Linq;
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

            if (user == null)
            {
                result.Success = false;
                result.Message = "This user does not exist";
                return result;
            }

            var clients = await _context.Clients
                .Where(x => x.UserId == userId)
                .ToListAsync();

            if (clients.Count >= Constants.MaxClientsPerUser)
            {
                if (request.OverridePolicy == OverridePolicy.Default)
                {
                    result.Success = false;
                    result.Message = "Too many clients and overriding is disallowed";
                    return result;
                }

                var clientsToDelete = clients
                    .OrderBy(x => 
                        request.OverridePolicy == OverridePolicy.DeleteLastUsed 
                            ? x.LastUsed : x.CreatedAt)
                    .First();

                _context.Clients.Remove(clientsToDelete);
            }

            var key = CryptoUtils.CreatePseudoRandomKey();
            await _context.Clients.AddAsync(new Client
            {
                UserId = userId,
                Sha256 = CryptoUtils.Sha265HashAsString(request.Binary),
                Key = key
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
    }
}
