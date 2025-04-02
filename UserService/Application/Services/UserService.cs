using AutoMapper;
using UserService.Application.Common;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Services
{
    public class UserService(IUserRepository userRepository, 
                             IMapper mapper, 
                             IDbContextTransactionManager dbContextTransactionManager) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IDbContextTransactionManager _dbContextTransactionManager = dbContextTransactionManager;

        public async Task<OperationResult> GetUsersAsync(GetUsersDto getUsersDto)
        {
            var parameters = _mapper.Map<Dictionary<string, string>>(getUsersDto);
            var users = await _userRepository.GetUsersAsync(parameters);

            return new OperationResult
            { 
                Success = true, 
                Message = null, 
                Data = _mapper.Map<List<UserDto>>(users) 
            };
        }

        public async Task<OperationResult> GetUserByIdAsync(string id)
        {
            return new OperationResult 
            { 
                Success = true, 
                Message = null, 
                Data = await _userRepository.GetUserByIdAsync(Guid.Parse(id)) 
            };
        }

        public async Task<OperationResult> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            user.SetPassword(createUserDto.Password);

            _userRepository.AddUser(user);
            await _dbContextTransactionManager.SaveChangesAsync();

            return new OperationResult 
            { 
                Success = true, 
                Message = "User was created succesfuly", 
                Data = _mapper.Map<UserDto>(user) 
            };
        }

        public async Task<OperationResult> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(Guid.Parse(id));
            
            if(user != null)
            {
                _mapper.Map(updateUserDto, user);
                user.Update();

                _userRepository.UpdateUser(user);
                await _dbContextTransactionManager.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "User was updated succesfuly",
                    Data = _mapper.Map<UserDto>(user)
                };
            }

            return new OperationResult
            {
                Success = false,
                Message = $"User {id} not found",
                Data = null
            };
        }

        public async Task<OperationResult> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(Guid.Parse(id));

            if(user != null)
            {
                user.MarkAsDeleted();
                user.Update();

                _userRepository.UpdateUser(user);
                await _dbContextTransactionManager.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "User was deleted succesfuly",
                    Data = null
                };
            }

            return new OperationResult
            {
                Success = false,
                Message = $"User {id} not found",
                Data = null
            };
        }
    }
}
