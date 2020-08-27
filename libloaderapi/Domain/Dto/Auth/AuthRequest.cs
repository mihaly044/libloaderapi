using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto.Auth
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
