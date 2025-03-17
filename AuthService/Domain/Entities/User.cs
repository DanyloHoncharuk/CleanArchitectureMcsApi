using System.ComponentModel.DataAnnotations;
using AuthService.Domain.Common;

namespace AuthService.Domain.Entities
{
    public class User : BaseSoftDeletableEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; } = null;

        public bool IsActive { get; set; } = true;
    }
}
