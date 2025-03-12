using AuthService.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UserService(AuthServiceDbContext context)
    {
        private readonly AuthServiceDbContext _context = context;

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && !u.IsDeleted);
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && !u.IsDeleted);
        }
    }
}
