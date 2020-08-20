using System;
using System.Collections.Generic;

namespace libloaderapi.Domain.Database.Models
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public IList<UserRole> UserRoles { get; set; }
    }
}
