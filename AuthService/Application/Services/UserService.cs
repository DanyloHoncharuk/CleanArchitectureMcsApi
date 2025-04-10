using AuthService.Domain.Entities;
using AuthService.Application.Interfaces;
using AutoMapper;
using AuthService.Application.DTOs;
using AuthService.Application.Wrappers;
using AuthService.Application.Common;
using AuthService.Application.Exceptions;

namespace AuthService.Application.Services
{
    public class UserService(IUserRepository userRepository, 
                             IDbContextTransactionManager dbContextTransactionManager, 
                             IMapper mapper, 
                             IJwtService jwtService) : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IDbContextTransactionManager _dbContextTransactionManager = dbContextTransactionManager;
        private readonly IMapper _mapper = mapper;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<OperationResult<object?>> CreateUserAsync(CreateUserDto createUserDto)
        {
            return await HandleRequestAsync(async () =>
            {
                await ValidateUserUniquenessAsync(createUserDto.Username, createUserDto.Email);

                var user = _mapper.Map<User>(createUserDto);
                userRepository.Add(user);
                await _dbContextTransactionManager.SaveChangesAsync();
            }, "User was created successfully.");
        }

        public async Task<OperationResult<object>> AuthenticateUserAsync(AuthenticateUserDto authenticateUserDto)
        {
            return await HandleRequestAsync<object>(async () =>
            {
                var user = await _userRepository.GetByUsernameAsync(authenticateUserDto.Username);
                if (user != null && user.VerifyPassword(authenticateUserDto.Password))
                {
                    var token = _jwtService.GenerateJwt(authenticateUserDto.Username);
                    return new { jwt = token };
                }

                throw new AuthenticationException("Invalid username or password");
            });            
        }

        private async Task ValidateUserUniquenessAsync(string? username, string? email, Guid? excludeUserId = null)
        {
            if(!string.IsNullOrEmpty(username))
            {
                var existing = await _userRepository.GetByUsernameAsync(username);
                if (existing != null && existing.Id != excludeUserId)
                    throw new ArgumentException($"User with username {username} already exists!");
            }

            if (!string.IsNullOrEmpty(email))
            {
                var existing = await _userRepository.GetByEmailAsync(email);
                if (existing != null && existing.Id != excludeUserId)
                    throw new ArgumentException($"User with email {email} already exists!");
            }
        }
    }
}
