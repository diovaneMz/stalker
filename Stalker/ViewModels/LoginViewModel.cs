using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Stalker.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public LoginViewModel()
    {
        Title = "Entrar no Stalker";
    }

    [RelayCommand]
    private void LoginWithDiscord()
    {
        // integração OAuth Discord — você implementa
    }

    [RelayCommand]
    private void LoginWithGoogle()
    {
        // integração OAuth Google — você implementa
    }
}
