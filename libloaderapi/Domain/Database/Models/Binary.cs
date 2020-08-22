using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libloaderapi.Domain.Database.Models
{
    public class Binary
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string Sha1 { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
