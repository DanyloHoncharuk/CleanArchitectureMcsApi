using AuthService.Application.DTOs;
using AuthService.Application.Models;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> IsUsernameTakenAsync(string username);
        Task<bool> IsEmailTakenAsync(string email);
        Task<OperationResult> CreateUserAsync(CreateUserDto createUserDto);
    }
}
