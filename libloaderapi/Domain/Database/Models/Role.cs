using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libloaderapi.Domain.Database.Models
{
    public class Role
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(64)]
        [Column(TypeName = "nvarchar(64)")]
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public IList<UserRole> UserRoles { get; set; }
    }
}
