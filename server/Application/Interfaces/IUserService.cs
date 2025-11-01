using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<bool> Register(UserDto userDto);
    Task<string> Login(UserDto userDto);
    Task<User?> GetUserByUsername(string username);
    Task<User?> GetUserById(int id);
    Task<List<User>> GetAllUsers();
    Task<bool> UpdateUser(int id, UserDto userDto);
    Task<bool> DeleteUser(int id);
    Task<bool> ChangePassword(string username, ChangePasswordDto changePasswordDto);
    Task<bool> UpdateProfile(string username, UpdateUserProfileDto updateDto);
    Task<bool> UpdateAvatar(string username, string avatarPath);
}
