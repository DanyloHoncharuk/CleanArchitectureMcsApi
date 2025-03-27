using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByLoginAsync(string login);
        Task<IEnumerable<User>?> GetUsersAsync(Dictionary<string, string> parameters);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}
