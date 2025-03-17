using AuthService.Domain.Entities;

namespace AuthService.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);

        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);
    }
}
