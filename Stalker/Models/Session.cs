namespace Stalker.Models;

public class Session
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    public TimeSpan Duration => EndedAt.HasValue
        ? EndedAt.Value - StartedAt
        : DateTime.UtcNow - StartedAt;
}
