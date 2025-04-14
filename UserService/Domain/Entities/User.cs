using UserService.Domain.Common;

namespace UserService.Domain.Entities
{
    public class User : BaseSoftDeletableEntity
    {
        // Encapsulation?

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string PasswordHash { get; private set; }
        public string? Email { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth {  get; set; }
        public bool IsActive { get; private set; } = true;

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public void Deactivate() => IsActive = false;

        public void Activate() => IsActive = true;
    }
}
