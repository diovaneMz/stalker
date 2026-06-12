namespace Stalker.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExecutableName { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }

    public TimeSpan TotalPlayTime { get; set; }
    public DateTime? LastPlayedAt { get; set; }
    public DateTime? FirstPlayedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? PlatinumAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Session> Sessions { get; set; } = [];
}
