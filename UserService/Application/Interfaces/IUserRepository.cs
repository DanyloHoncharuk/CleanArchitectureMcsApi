using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>?> GetUsersAsync(Dictionary<string, string> parameters);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}
