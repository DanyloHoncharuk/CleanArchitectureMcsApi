using AuthService.Domain.Common;

namespace AuthService.Domain.Entities
{
    public class User : BaseSoftDeletableEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string? Email { get; private set; } = null;

        public bool IsActive { get; private set; } = true;

        private User() { } // for EF

        public User(string username, string password, string? email)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
                throw new ArgumentException("Username cannot be empty or exceed 100 characters");

            if (!string.IsNullOrEmpty(email) && email.Length > 100)
                throw new ArgumentException("Email cannot exceed 100 characters");

            Username = username;
            Email = email;
            SetPassword(password);
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

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
