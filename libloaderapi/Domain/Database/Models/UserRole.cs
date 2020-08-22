using System;
using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Database.Models
{
    public class UserRole
    {
        [Required]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
