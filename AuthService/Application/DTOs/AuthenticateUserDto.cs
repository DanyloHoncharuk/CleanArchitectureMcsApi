using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.DTOs
{
    public class AuthenticateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
