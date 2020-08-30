using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libloaderapi.Domain.Database.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(64)]
        [Column(TypeName = "varchar(64)")]
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Required]
        [MaxLength(64)]
        [Column(TypeName = "varchar(64)")]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public IList<UserRole> UserRoles { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public IList<Client> Clients { get; set; }
    }
}
