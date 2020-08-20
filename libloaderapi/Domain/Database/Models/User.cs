using System;
using System.Collections.Generic;

namespace libloaderapi.Domain.Database.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string Password { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public IList<UserRole> UserRoles { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public IList<Binary> Binaries { get; set; }
    }
}
