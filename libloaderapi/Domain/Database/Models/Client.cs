using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using libloaderapi.Common;

namespace libloaderapi.Domain.Database.Models
{
    public class Client
    {
        [Required]
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(28)]
        [Column(TypeName = "varchar(28)")]
        public string Digest { get; set; }

        [Required]
        [Column(TypeName = "varchar(172)")]
        public byte[] ApiKey { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUsed { get; set; }

        [MinLength(3)]
        [MaxLength(32)]
        [Column(TypeName = "varchar(32)")]
        public string Tag { get; set; }

        [Required]
        [MaxLength(128)]
        [Column(TypeName = "varchar(128)")]
        public string RegistrantIp { get; set; }

        [Required]
        public BucketType BucketType { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User User { get; set; }
    }
}
