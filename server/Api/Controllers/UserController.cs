using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using System.Security.Claims;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        var userResponse = users.Select(u => new
        {
            id = u.Id,
            username = u.Username,
            role = u.Role,
            name = u.Name,
            phone = u.Phone,
            location = u.Location,
            avatar = u.Avatar
        });
        return Ok(userResponse);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound();

        return Ok(new
        {
            id = user.Id,
            username = user.Username,
            role = user.Role,
            name = user.Name,
            phone = user.Phone,
            location = user.Location,
            avatar = user.Avatar
        });
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            return Unauthorized();

        var user = await _userService.GetUserByUsername(username);
        if (user == null)
            return NotFound();

        return Ok(new
        {
            id = user.Id,
            username = user.Username,
            role = user.Role,
            name = user.Name,
            phone = user.Phone,
            location = user.Location,
            avatar = user.Avatar,
            isAdmin = user.Role == "Admin"
        });
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        var result = await _userService.Register(userDto);
        if (result)
            return Ok(new { message = "User created successfully" });
        return BadRequest(new { message = "User creation failed" });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        // Convert UpdateUserDto to UserDto
        var userDto = new UserDto
        {
            Id = updateUserDto.Id,
            Username = updateUserDto.Username,
            Password = updateUserDto.Password ?? string.Empty, // Use empty string if null
            Role = updateUserDto.Role,
            Name = updateUserDto.Name,
            Phone = updateUserDto.Phone,
            Location = updateUserDto.Location,
            Avatar = updateUserDto.Avatar
        };

        var result = await _userService.UpdateUser(id, userDto);
        if (result)
            return Ok(new { message = "User updated successfully" });
        return BadRequest(new { message = "User update failed" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id);
        if (result)
            return Ok(new { message = "User deleted successfully" });
        return BadRequest(new { message = "User deletion failed" });
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            return Unauthorized();

        var result = await _userService.ChangePassword(username, changePasswordDto);
        if (result)
            return Ok(new { message = "Password changed successfully" });
        return BadRequest(new { message = "Password change failed" });
    }

    [HttpPost("update")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto updateDto)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            return Unauthorized();

        var result = await _userService.UpdateProfile(username, updateDto);
        if (result)
            return Ok(new { message = "Profile updated successfully" });
        return BadRequest(new { message = "Profile update failed" });
    }

    [HttpPost("update-avatar")]
    [Authorize]
    public async Task<IActionResult> UpdateAvatar([FromForm] IFormFile photo)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            return Unauthorized();

        if (photo == null || photo.Length == 0)
            return BadRequest(new { message = "No file uploaded" });

        // Save file logic here (simplified for now)
        var fileName = $"{Guid.NewGuid()}_{photo.FileName}";
        var folderName = Path.Combine("wwwroot", "uploads");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        if (!Directory.Exists(pathToSave))
            Directory.CreateDirectory(pathToSave);

        var fullPath = Path.Combine(pathToSave, fileName);
        var relativePath = Path.Combine("uploads", fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await photo.CopyToAsync(stream);

        var result = await _userService.UpdateAvatar(username, $"/{relativePath}");
        if (result)
            return Ok(new { message = "Avatar updated successfully", avatar = $"/{relativePath}" });
        return BadRequest(new { message = "Avatar update failed" });
    }
}
