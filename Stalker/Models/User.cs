namespace Stalker.Models;

public class User
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public string Provider { get; set; } = string.Empty; // "discord" or "google"
}
