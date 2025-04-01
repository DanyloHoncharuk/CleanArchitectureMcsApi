using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository(UserServiceDbContext context) : IUserRepository
    {
        private readonly UserServiceDbContext _context = context;

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>?> GetUsersAsync(Dictionary<string, string> parameters)
        {
            var query = _context.Users.AsQueryable();

            if (!parameters["search"].IsNullOrEmpty())
            {
                var serachValue = $"%{parameters["search"]}%";

                query = query.Where(u => 
                    EF.Functions.Like(u.Login, serachValue) ||
                    EF.Functions.Like(u.Name, serachValue) ||
                    EF.Functions.Like(u.Surname, serachValue)
                    );
            }

            var users = await query
                .Skip(Int32.Parse(parameters["skip"]))
                .Take(Int32.Parse(parameters["take"]))
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
