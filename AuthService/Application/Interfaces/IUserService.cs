using AuthService.Application.DTOs;
using AuthService.Application.Common;
using AuthService.Domain.Entities;

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
