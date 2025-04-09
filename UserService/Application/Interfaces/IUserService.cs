using UserService.Application.DTOs;
using UserService.Application.Wrappers;

namespace UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<List<UserDto>>> GetUsersAsync(GetUsersDto getUsersQueryDto);
        Task<OperationResult<UserDto>> GetUserByIdAsync(string id);
        Task<OperationResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<OperationResult<UserDto>> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
        Task<OperationResult<object?>> DeleteUserAsync(string id);
    }
}
