using Microsoft.EntityFrameworkCore;
using AuthService.Domain.Models;

namespace AuthService.Infrastructure.Data
{
    public class AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
