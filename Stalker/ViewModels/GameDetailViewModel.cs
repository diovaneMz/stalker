using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Stalker.Data;

namespace Stalker.ViewModels;

public partial class GameDetailViewModel : BaseViewModel
{
    private readonly IGameRepository _gameRepository = App.Repository;
    private readonly Action _goBack;
    public event Action? GameDeleted;

    [ObservableProperty] private int _gameId ;
    [ObservableProperty] private string _gameName = string.Empty;
    [ObservableProperty] private string _totalPlayTime = string.Empty;
    [ObservableProperty] private string _lastPlayed = string.Empty;
    [ObservableProperty] private string? _firstPlayedAt;
    [ObservableProperty] private string? _completedAt;
    [ObservableProperty] private string? _platinumAt;
    [ObservableProperty] private bool _isCompleted;
    [ObservableProperty] private bool _isPlatinum;

    public ObservableCollection<SessionItemViewModel> Sessions { get; } = [];
    public ISeries[] WeeklySeries { get; private set; } = [];
    public Axis[] XAxes { get; private set; } = [];

    public GameDetailViewModel() : this(null, () => { }) { }

    public GameDetailViewModel(GameItemViewModel? game, Action goBack)
    {
        _goBack = goBack;
        Title = "Detalhes";

        if (game is not null)
            LoadFromGame(game);
        else
            LoadDesignData();

        BuildChart();
    }

    [RelayCommand]
    private void GoBack() => _goBack();

    [RelayCommand]
    private async Task DeleteGame(int id)
    {
        await _gameRepository.DeleteGameAsync(id);
        GameDeleted?.Invoke();
    } 
    
    [RelayCommand]
    private void MarkCompleted()
    {
        CompletedAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        IsCompleted = true;
    }

    [RelayCommand]
    private void MarkPlatinum()
    {
        PlatinumAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        IsPlatinum = true;
    }

    private void LoadFromGame(GameItemViewModel game)
    {
        GameId = game.Id;
        GameName = game.Name;
        TotalPlayTime = game.TotalPlayTimeFormatted;
        LastPlayed = game.LastPlayedFormatted;

        Sessions.Add(new SessionItemViewModel { Date = "Sessões disponíveis após integração", Duration = "" });
    }

    private void LoadDesignData()
    {
        GameName = "Elden Ring";
        TotalPlayTime = "87h 30m";
        LastPlayed = "20/05/2026";
        FirstPlayedAt = "01/01/2026";

        Sessions.Add(new SessionItemViewModel { Date = "22/05/2026", Duration = "2h 15m" });
        Sessions.Add(new SessionItemViewModel { Date = "20/05/2026", Duration = "4h 30m" });
        Sessions.Add(new SessionItemViewModel { Date = "18/05/2026", Duration = "1h 45m" });
    }

    private void BuildChart()
    {
        var days = new[] { "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom" };
        var hours = new double[] { 1.5, 0, 3, 2, 4.5, 6, 2 };

        WeeklySeries =
        [
            new ColumnSeries<double>
            {
                Values = hours,
                Fill = new SolidColorPaint(new SKColor(99, 102, 241)),
                Name = "Horas jogadas"
            }
        ];

        XAxes =
        [
            new Axis
            {
                Labels = days,
                LabelsPaint = new SolidColorPaint(SKColors.White)
            }
        ];
    }
}

public class SessionItemViewModel
{
    public string Date { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}

