using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Data
{
    public class DbContextTransactionManager(UserServiceDbContext context) : IDbContextTransactionManager
    {
        private readonly UserServiceDbContext _context = context;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
