using AuthService.Domain.Entities;
using AuthService.Application.Interfaces;
using AutoMapper;
using AuthService.Application.DTOs;
using AuthService.Application.Common;

namespace AuthService.Application.Services
{
    public class UserService(IUserRepository userRepository, IDbContextTransactionManager dbContextTransactionManager, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IDbContextTransactionManager _dbContextTransactionManager = dbContextTransactionManager;
        private readonly IMapper _mapper = mapper;

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username) != null;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email) != null;
        }

        public async Task<OperationResult> CreateUserAsync(CreateUserDto createUserDto)
        {
            if(await IsUsernameTakenAsync(createUserDto.Username))
            {
                return new OperationResult { Success = false, Message = $"Username {createUserDto.Username} is already taken." };
            }

            if (createUserDto.Email != null && await IsEmailTakenAsync(createUserDto.Email))
            {
                return new OperationResult { Success = false, Message = $"Email {createUserDto.Email} is already taken." };
            }

            var user = _mapper.Map<User>(createUserDto);
            userRepository.Add(user);
            await _dbContextTransactionManager.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "User created successfully." };
        }
    }
}
