using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Stalker.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool _startWithWindows;

    [ObservableProperty]
    private bool _minimizeToTray = true;

    [ObservableProperty]
    private string _userDisplayName = string.Empty;

    [ObservableProperty]
    private string? _userAvatarUrl;

    public SettingsViewModel()
    {
        Title = "Configurações";
        UserDisplayName = "Diovane";
    }

    [RelayCommand]
    private void Logout()
    {
        // integração — você implementa
    }

    [RelayCommand]
    private void ToggleStartWithWindows(bool value)
    {
        // integração com registro do Windows — você implementa
    }
}
