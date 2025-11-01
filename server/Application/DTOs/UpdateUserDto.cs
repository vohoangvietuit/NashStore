using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UpdateUserDto
{
    public int? Id { get; set; }
    [Required]
    public string Username { get; set; } = string.Empty;
    public string? Password { get; set; }  // Optional for updates
    public string? Role { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? Avatar { get; set; }
}
