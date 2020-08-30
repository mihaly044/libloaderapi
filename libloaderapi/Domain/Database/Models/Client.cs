using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libloaderapi.Domain.Database.Models
{
    public enum BucketType : ushort
    {
        Development = 0,
        Production
    }

    public class Client
    {
        [Required]
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(64)]
        [Column(TypeName = "varchar(64)")]
        public string Sha256 { get; set; }

        [Required]
        [MaxLength(128)]
        [Column(TypeName = "varchar(128)")]
        public string Key { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUsed { get; set; }

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
