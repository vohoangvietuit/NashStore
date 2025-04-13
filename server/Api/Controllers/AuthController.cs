using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, ILogger<AuthController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        _logger.LogInformation($"User {userDto.Username} is trying to register.");
        var result = await _userService.Register(userDto);
        if (result)
            return Ok(new { message = "User registered successfully" });
        return BadRequest(new { message = "User registration failed" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        var token = await _userService.Login(userDto);
        if (string.IsNullOrEmpty(token))
            return Unauthorized(new { message = "Invalid credentials" });
        return Ok(new { token });
    }
}
