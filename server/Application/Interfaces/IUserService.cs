using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<bool> Register(UserDto userDto);
    Task<string> Login(UserDto userDto);
}
