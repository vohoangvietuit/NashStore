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
            Role = role,
            Name = userDto.Name,
            Phone = userDto.Phone,
            Location = userDto.Location,
            Avatar = userDto.Avatar
        };
        return await _userRepository.AddUser(user);
    }

    public async Task<string> Login(UserDto userDto)
    {
        var user = await _userRepository.GetUserByUsername(userDto.Username);
        if (user == null || !_authService.VerifyPassword(userDto.Password, user.PasswordHash))
            return string.Empty;

        return _authService.GenerateToken(user);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<bool> UpdateUser(int id, UserDto userDto)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null) return false;

        user.Username = userDto.Username;
        user.Role = userDto.Role ?? user.Role;
        user.Name = userDto.Name;
        user.Phone = userDto.Phone;
        user.Location = userDto.Location;
        user.Avatar = userDto.Avatar;

        // Only update password if provided
        if (!string.IsNullOrEmpty(userDto.Password))
        {
            user.PasswordHash = _authService.HashPassword(userDto.Password);
        }

        return await _userRepository.UpdateUser(user);
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await _userRepository.DeleteUser(id);
    }

    public async Task<bool> ChangePassword(string username, ChangePasswordDto changePasswordDto)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null) return false;

        // Verify current password
        if (!_authService.VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
            return false;

        // Update with new password
        user.PasswordHash = _authService.HashPassword(changePasswordDto.NewPassword);
        return await _userRepository.UpdateUser(user);
    }

    public async Task<bool> UpdateProfile(string username, UpdateUserProfileDto updateDto)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null) return false;

        // Update only the provided fields
        if (updateDto.Name != null) user.Name = updateDto.Name;
        if (updateDto.Phone != null) user.Phone = updateDto.Phone;
        if (updateDto.Location != null) user.Location = updateDto.Location;
        if (updateDto.Avatar != null) user.Avatar = updateDto.Avatar;

        return await _userRepository.UpdateUser(user);
    }

    public async Task<bool> UpdateAvatar(string username, string avatarPath)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null) return false;

        user.Avatar = avatarPath;
        return await _userRepository.UpdateUser(user);
    }
}
