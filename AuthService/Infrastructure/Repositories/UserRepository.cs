using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuthService.Application.Interfaces;

namespace AuthService.Infrastructure.Repositories
{
    public class UserRepository(AuthServiceDbContext context) : IUserRepository
    {
        private readonly AuthServiceDbContext _context = context;

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
