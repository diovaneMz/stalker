using CommunityToolkit.Mvvm.ComponentModel;
using Stalker.Models;

namespace Stalker.ViewModels;

public partial class GameItemViewModel : ObservableObject
{
    public int Id { get; }
    public string Name { get; }
    public string ExecutableName { get; }
    public string? CoverImageUrl { get; }

    [ObservableProperty]
    private TimeSpan _totalPlayTime;

    [ObservableProperty]
    private DateTime? _lastPlayedAt;

    [ObservableProperty]
    private bool _isRunning;

    public string TotalPlayTimeFormatted =>
        TotalPlayTime.TotalHours >= 1
            ? $"{(int)TotalPlayTime.TotalHours}h {TotalPlayTime.Minutes}m"
            : $"{TotalPlayTime.Minutes}m";

    public string LastPlayedFormatted =>
        LastPlayedAt.HasValue ? LastPlayedAt.Value.ToString("dd/MM/yyyy") : "Nunca";

    public GameItemViewModel(Game game)
    {
        Id = game.Id;
        Name = game.Name;
        ExecutableName = game.ExecutableName;
        CoverImageUrl = game.CoverImageUrl;
        _totalPlayTime = game.TotalPlayTime;
        _lastPlayedAt = game.LastPlayedAt;
    }
}
