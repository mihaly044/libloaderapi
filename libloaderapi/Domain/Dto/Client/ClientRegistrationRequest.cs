using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto.Client
{
    public enum OverridePolicy : ushort
    {
        Default = 0,
        DeleteOldestLastUsed,
        DeleteOldest
    }

    public class ClientRegistrationRequest
    {
        [Required]
        public IFormFile File { get; set; }

        public OverridePolicy OverridePolicy { get; set; }
    }
}
