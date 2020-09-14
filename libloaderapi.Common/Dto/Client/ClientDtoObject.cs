using System;
using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Common.Dto.Client
{
    public class ClientDtoObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Digest { get; set; }

        public byte[] ApiKey { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUsed { get; set; }

        public string RegistrantIp { get; set; }

        public BucketType BucketType { get; set; }

        [MinLength(3)]
        public string Tag { get; set; }
    }
}
