using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Common.Dto.Auth
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
