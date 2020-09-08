using System;
using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Common.Dto.Client
{
    public class ClientDtoObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Sha256 { get; set; }

        public byte[] Key { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUsed { get; set; }

        public string RegistrantIp { get; set; }

        public BucketType BucketType { get; set; }

        [MinLength(3)]
        public string Tag { get; set; }
    }
}
