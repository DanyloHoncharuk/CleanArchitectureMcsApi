using AuthService.Application.Interfaces;

namespace AuthService.Infrastructure.Data
{
    public class DbContextTransactionManager(AuthServiceDbContext context) : IDbContextTransactionManager
    {
        private readonly AuthServiceDbContext _context = context;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
