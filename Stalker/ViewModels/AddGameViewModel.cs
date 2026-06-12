using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Stalker.ViewModels;

public partial class AddGameViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    private string _gameName = string.Empty;

    [ObservableProperty]
    private string _executableName = string.Empty;

    public bool Confirmed { get; private set; }

    public event Action? RequestClose;

    [RelayCommand(CanExecute = nameof(CanConfirm))]
    private void Confirm()
    {
        Confirmed = true;
        RequestClose?.Invoke();
    }

    private bool CanConfirm() => !string.IsNullOrWhiteSpace(GameName);

    [RelayCommand]
    private void Cancel() => RequestClose?.Invoke();
}
