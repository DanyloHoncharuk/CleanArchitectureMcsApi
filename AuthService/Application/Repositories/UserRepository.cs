using AuthService.Infrastructure.Interfaces.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Application.Repositories
{
    public class UserRepository(AuthServiceDbContext context) : IUserRepository
    {
        private readonly AuthServiceDbContext _context = context;

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
