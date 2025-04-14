using AuthService.Domain.Common;

namespace AuthService.Domain.Entities
{
    public class User : BaseSoftDeletableEntity
    {
        // Encapsulation?
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string PasswordHash { get; private set; }
        public string? Email { get; set; } = null;

        public bool IsActive { get; private set; } = true;

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password) // Single-Resposibility Principle isn't breaking? => PasswordService in App Layer?
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public void Deactivate() => IsActive = false;

        public void Activate() => IsActive = true;
    }
}
