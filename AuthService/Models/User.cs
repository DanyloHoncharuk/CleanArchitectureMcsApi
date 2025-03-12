using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } // hash by BCrypt

        [EmailAddress]
        [MaxLength (100)]
        public string? Email { get; set; } = null;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; } = null;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

    }
}
