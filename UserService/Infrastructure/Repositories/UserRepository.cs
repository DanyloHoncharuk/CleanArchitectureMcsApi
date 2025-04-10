using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Interfaces;
using UserService.Common;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository(UserServiceDbContext context) : IUserRepository
    {
        private readonly UserServiceDbContext _context = context;

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<List<User>?> GetUsersAsync(Dictionary<string, string> parameters) // List or IEnumerable ???
        {
            var query = _context.Users.AsQueryable();

            if (!parameters[GetUsersQueryParameters.Search].IsNullOrEmpty())
            {
                var serachValue = $"%{parameters[GetUsersQueryParameters.Search]}%";

                query = query.Where(u => 
                    EF.Functions.Like(u.Login, serachValue) ||
                    EF.Functions.Like(u.Name, serachValue) ||
                    EF.Functions.Like(u.Surname, serachValue)
                    );
            }

            var users = await query
                .Where(u => !u.IsDeleted)
                .Skip(Int32.Parse(parameters[$"{GetUsersQueryParameters.Skip}"]))
                .Take(Int32.Parse(parameters[$"{GetUsersQueryParameters.Take}"]))
                .ToListAsync();

            return users;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
