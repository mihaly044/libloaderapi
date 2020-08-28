using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libloaderapi.Domain.Database;
using libloaderapi.Domain.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace libloaderapi.Domain.Services
{
    public interface IUsersService
    {
        /// <summary>
        /// Get a list of all the users
        /// </summary>
        /// <returns></returns>
        Task<IList<User>> GetAsync();

        /// <summary>
        /// Get a specific user by <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetAsync(Guid userId);

        /// <summary>
        /// Get a list of roles associated with an <paramref name="userId"/>
        /// <param name="userId"></param>
        /// </summary>
        /// <returns></returns>
        Task<IList<Role>> GetRolesAsync(Guid userId);
    }

    public class UsersService : IUsersService
    {
        private readonly AppDbContext _appDbContext;

        public UsersService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<User>> GetAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<IList<Role>> GetRolesAsync(Guid userId)
        {
            return await _appDbContext.UserRoles
                .Where(u => u.UserId == userId)
                .Include(r => r.Role)
                .Select(o => new Role
                {
                    Id = o.Role.Id,
                    Name = o.Role.Name
                })
                .ToListAsync();
        }
    }
}
