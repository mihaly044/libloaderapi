using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto.Client
{
    public enum OverridePolicy : ushort
    {
        Default = 0,
        DeleteLastUsed,
        DeleteOldest
    }

    public class ClientRegistrationRequest
    {
        [Required]
        public byte[] Binary { get; set; }

        public OverridePolicy OverridePolicy { get; set; }
    }
}
