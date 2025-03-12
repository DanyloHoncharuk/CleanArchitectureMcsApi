using Microsoft.AspNetCore.Mvc;
using AuthService.Data;
using AuthService.DTO;
using AuthService.Services;
using AutoMapper;
using AuthService.Models;


namespace AuthService.Controllers
{
    public class AuthController(AuthServiceDbContext context,
                          UserService userService,
                          IMapper mapper) : BaseApiController(context)
    {
        private readonly UserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (await _userService.IsUsernameTakenAsync(createUserDto.Username)) 
                return BadRequest(new {message = $"User with username {createUserDto.Username} is already exist." });

            if(createUserDto.Email != null && await _userService.IsEmailTakenAsync(createUserDto.Email))
                return BadRequest(new { message = $"User with email {createUserDto.Email} is already exist." });

            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new {message = $"User {user.Username} was added successfully." });
        }
    }
}
