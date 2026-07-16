using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stalker.Data;
using Stalker.Models;

namespace Stalker.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private GameItemViewModel? _selectedGame;

    [ObservableProperty]
    private string _activeGameName = string.Empty;

    [ObservableProperty]
    private bool _hasActiveGame;

    private readonly IGameRepository _repo = App.Repository;
    
    public ObservableCollection<GameItemViewModel> Games { get; } = [];
    public ObservableCollection<GameItemViewModel> FilteredGames { get; } = [];

    public DashboardViewModel()
    {
        _ =  LoadGamesAsync();
        Title = "Stalker";
    }

    partial void OnSearchTextChanged(string value) => ApplyFilter();

    private void ApplyFilter()
    {
        FilteredGames.Clear();
        var query = SearchText.Trim().ToLower();
        foreach (var game in Games)
        {
            if (string.IsNullOrEmpty(query) || game.Name.ToLower().Contains(query))
                FilteredGames.Add(game);
        }
    }

    public event Action? RequestAddGame;
    public event Action<GameItemViewModel>? RequestNavigateToDetail;

    [RelayCommand]
    private void AddGame() => RequestAddGame?.Invoke();

    [RelayCommand]
    private void OpenGameDetail(GameItemViewModel? game)
    {
        if (game is null) return;
        SelectedGame = game;
        RequestNavigateToDetail?.Invoke(game);
    }
    
    public async Task AddNewGame(string name, string executableName)
    {
        var game = new Game
        {
            Name = name,
            ExecutableName = executableName
        };

        Game saved = await _repo.AddGameAsync(game);

        Games.Add(new GameItemViewModel(saved));
        ApplyFilter();
    }

    private async Task LoadGamesAsync()
    {
        List<Game> games = await _repo.GetAllGamesAsync();

        foreach (Game game in games)
            Games.Add(new GameItemViewModel(game));

        ApplyFilter();
    }

    public void RemoveGame(GameItemViewModel? game)
    {
        Games.Remove(game);
        ApplyFilter();
    }
}
