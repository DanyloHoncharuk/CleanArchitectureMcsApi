using System.ComponentModel.DataAnnotations;

namespace AuthService.DTO
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(12, ErrorMessage = "Password requires a minimum of 12 characters.")]
        public string Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
