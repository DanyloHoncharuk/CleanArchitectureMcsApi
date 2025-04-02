using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Login must contain 100 or less characters")]
        public string Login { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must contain from 8 to 16 characters")]
        public string Password { get; set; }

        [EmailAddress]
        [MaxLength(100, ErrorMessage = "Email must contain 100 or less characters")]
        public string? Email { get; set; }

        [MaxLength(20, ErrorMessage = "PhoneNumber must containt 20 or less characters")]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Name must contain 255 or less characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Surname must contain 255 or less characters")]
        public string Surname { get; set; }

        [Required]
        [RegularExpression(@"^(19|20)\d\d-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Incorrect date format: YYYY-MM-DD")]
        public string DateOfBirth { get; set; }
    }
}
