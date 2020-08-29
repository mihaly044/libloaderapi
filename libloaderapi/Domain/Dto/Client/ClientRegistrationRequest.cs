using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using libloaderapi.Domain.Database.Models;

namespace libloaderapi.Domain.Dto.Client
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

        public OverridePolicy OverridePolicy { get; set; }

        public BucketType Bucket { get; set; }
    }
}
