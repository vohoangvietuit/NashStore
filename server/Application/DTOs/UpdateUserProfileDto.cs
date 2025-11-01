using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UpdateUserProfileDto
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? Avatar { get; set; }
}
