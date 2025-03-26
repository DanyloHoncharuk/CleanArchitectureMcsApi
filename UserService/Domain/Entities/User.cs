using UserService.Domain.Common;

namespace UserService.Domain.Entities
{
    public class User : BaseSoftDeletableEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public string? Email { get; private set; } = null;
        public string? PhoneNumber { get; private set; } = null;
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime DateOfBirth {  get; private set; }
        public bool IsActive { get; private set; } = true;

        private User() { } // for EF

        public User(string login, string password, string? email, string? phoneNumber, string name, string surname, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(login) || login.Length > 100)
                throw new ArgumentException("Login cannot be empty or exceed 100 characters");

            if (!string.IsNullOrEmpty(email) && email.Length > 100)
                throw new ArgumentException("Email cannot exceed 100 characters");

            if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length > 100)
                throw new ArgumentException("Phone number cannot exceed 100 characters");

            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                throw new ArgumentException("Name cannot be empty or exceed 100 characters");

            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                throw new ArgumentException("Surname cannot be empty or exceed 100 characters");


            Login = login;
            Email = email;
            PhoneNumber = phoneNumber;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
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
