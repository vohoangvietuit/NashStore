using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public UserService(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<bool> Register(UserDto userDto)
    {
        // If the role is missing or empty, default to "User"
        var role = string.IsNullOrWhiteSpace(userDto.Role) ? "User" : userDto.Role;

        var hashedPassword = _authService.HashPassword(userDto.Password);
        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = hashedPassword,
            Role = role
        };
        return await _userRepository.AddUser(user);
    }

    public async Task<string> Login(UserDto userDto)
    {
        var user = await _userRepository.GetUserByUsername(userDto.Username);
        if (user == null || !_authService.VerifyPassword(userDto.Password, user.PasswordHash))
            return null;

        return _authService.GenerateToken(user);
    }
}
