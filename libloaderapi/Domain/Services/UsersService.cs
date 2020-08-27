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
        Task<IList<User>> GetAsync();

        Task<User> GetAsync(Guid id);

        Task<IList<Role>> GetRolesAsync(User user);
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

        public async Task<User> GetAsync(Guid id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IList<Role>> GetRolesAsync(User user)
        {
            return await _appDbContext.UserRoles
                .Where(u => u.UserId == user.Id)
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
