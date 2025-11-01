namespace Domain.Entities
{
  public class User: BaseEntity
  {
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? Avatar { get; set; }
  }
}
