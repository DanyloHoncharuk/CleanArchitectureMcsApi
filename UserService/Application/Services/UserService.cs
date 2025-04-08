using AutoMapper;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Application.Wrappers;
using UserService.Application.Common;
using System;

namespace UserService.Application.Services
{
    public class UserService(IUserRepository userRepository, 
                             IMapper mapper, 
                             IDbContextTransactionManager dbContextTransactionManager) : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IDbContextTransactionManager _dbContextTransactionManager = dbContextTransactionManager;

        public async Task<OperationResult<List<UserDto>>> GetUsersAsync(GetUsersDto getUsersDto)
        {
            return await HandleRequestAsync(async () =>
            {
                var parameters = _mapper.Map<Dictionary<string, string>>(getUsersDto);
                var users = await _userRepository.GetUsersAsync(parameters);
                return _mapper.Map<List<UserDto>>(users);
            });
        }

        public async Task<OperationResult<UserDto?>> GetUserByIdAsync(string id)
        {
            return await HandleRequestAsync(async () =>
            {
                if (!Guid.TryParse(id, out var guid))
                    throw new ArgumentException("Invalid ID format");

                var user = await _userRepository.GetUserByIdAsync(guid);
                return user is null ? null : _mapper.Map<UserDto>(user);
            });
        }

        public async Task<OperationResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        { 
            return await HandleRequestAsync(async () =>
            {
                await ValidateUserUniquenessAsync(createUserDto.Login, createUserDto.Email, createUserDto.PhoneNumber);

                var user = _mapper.Map<User>(createUserDto);
                user.SetPassword(createUserDto.Password);

                _userRepository.AddUser(user);
                await _dbContextTransactionManager.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }, "User was created successfully");
        }

        public async Task<OperationResult<UserDto>> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            return await HandleRequestAsync(async () =>
            {
                if (!Guid.TryParse(id, out var guid))
                    throw new ArgumentException("Invalid ID format");

                var user = await _userRepository.GetUserByIdAsync(guid);
                if (user is null)
                    throw new KeyNotFoundException($"User with ID {id} not found");

                await ValidateUserUniquenessAsync(null, updateUserDto.Email, updateUserDto.PhoneNumber, guid);

                _mapper.Map(updateUserDto, user);
                user.Update();

                _userRepository.UpdateUser(user);
                await _dbContextTransactionManager.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }, "User was updated successfully");
        }

        public async Task<OperationResult<object?>> DeleteUserAsync(string id)
        {
            return await HandleRequestAsync(async () =>
            {
                if (!Guid.TryParse(id, out var guid))
                    throw new ArgumentException("Invalid ID format");

                var user = await _userRepository.GetUserByIdAsync(guid);
                if (user is null)
                    throw new KeyNotFoundException($"User with ID {id} not found");

                user.MarkAsDeleted();
                user.Update();

                _userRepository.UpdateUser(user);
                await _dbContextTransactionManager.SaveChangesAsync();
            }, "User was deleted successfully");
        }

        private async Task ValidateUserUniquenessAsync(string? login, string? email, string? phoneNumber, Guid? excludeUserId = null)
        {
            if (!string.IsNullOrEmpty(login))
            {
                var existing = await _userRepository.GetUserByLoginAsync(login);
                if (existing != null && existing.Id != excludeUserId)
                    throw new ArgumentException($"User with login {login} already exists!");
            }

            if (!string.IsNullOrEmpty(email))
            {
                var existing = await _userRepository.GetUserByEmailAsync(email);
                if (existing != null && existing.Id != excludeUserId)
                    throw new ArgumentException($"User with email {email} already exists!");
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                var existing = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber);
                if (existing != null && existing.Id != excludeUserId)
                    throw new ArgumentException($"User with phone number {phoneNumber} already exists!");
            }
        }
    }
}
