using System.Windows;
using System.Windows.Input;
using Stalker.ViewModels;

namespace Stalker.Views;

public partial class AddGameDialog : Window
{
    public AddGameViewModel ViewModel { get; }

    public AddGameDialog()
    {
        InitializeComponent();
        ViewModel = new AddGameViewModel();
        DataContext = ViewModel;
        ViewModel.RequestClose += () => DialogResult = ViewModel.Confirmed;
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();
}
