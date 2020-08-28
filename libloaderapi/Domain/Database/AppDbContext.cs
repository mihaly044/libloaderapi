using libloaderapi.Domain.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace libloaderapi.Domain.Database
{
    public class AppDbContext : DbContext
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly DbContextOptions<AppDbContext> _options;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<Client>()
                .HasOne(u => u.User)
                .WithMany(b => b.Clients);

            var roles = new[]
            {
                new Role { Name = "LibAdmin"},
                new Role { Name = "LibUser"},
                new Role { Name = "LibClient"}
            };
            modelBuilder.Entity<Role>()
                .HasData(roles);

            var users = new[]
            {
                new User { Name = "admin", Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"},
                new User { Name = "user", Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"}
            };
            modelBuilder.Entity<User>()
                .HasData(users);

            var userRoles = new List<UserRole>();
            for (short i = 0; i < users.Length; ++i)
                userRoles.Add(new UserRole { UserId = users[i].Id, RoleId = roles[i].Id });
            modelBuilder.Entity<UserRole>().HasData(userRoles);
        }
    }
}
