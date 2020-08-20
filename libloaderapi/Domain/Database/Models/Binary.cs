using System;
using System.Collections.Generic;

namespace libloaderapi.Domain.Database.Models
{
    public class Binary
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Sha1 { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
