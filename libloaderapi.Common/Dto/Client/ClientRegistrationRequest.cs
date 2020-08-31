using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Common.Dto.Client
{
    public enum OverridePolicy : ushort
    {
        Default = 0,
        DeleteOldestLastUsed,
        DeleteOldestCreated
    }

    public class ClientRegistrationRequest
    {
        [Required]
        public IFormFile File { get; set; }

        public string Tag { get; set; }

        public OverridePolicy OverridePolicy { get; set; }

        public BucketType Bucket { get; set; }
    }
}
