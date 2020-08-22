using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
