using System.Windows;
using System.Windows.Input;
using Stalker.ViewModels;
using Stalker.Views;

namespace Stalker;

public partial class MainWindow : Window
{
    private DashboardView? _dashboardView;

    public MainWindow()
    {
        InitializeComponent();
        ShowDashboard();
    }

    private void ShowDashboard()
    {
        if (_dashboardView is null)
        {
            _dashboardView = new DashboardView();
            var vm = (DashboardViewModel)_dashboardView.DataContext;
            vm.RequestNavigateToDetail += game =>
            {
                var detail = new GameDetailView(game, ShowDashboard);
                var detailVm = (GameDetailViewModel)detail.DataContext;
                detailVm.GameDeleted += () =>
                {
                    vm.RemoveGame(game);
                    ShowDashboard();
                };
                NavigateTo(detail);
            };
            vm.RequestAddGame += ShowAddGameDialog;
        }

        NavigateTo(_dashboardView);
    }

    private async void ShowAddGameDialog()
    {
        var dialog = new AddGameDialog { Owner = this };
        if (dialog.ShowDialog() == true)
        {
            var vm = (DashboardViewModel)_dashboardView!.DataContext;
            await vm.AddNewGame(dialog.ViewModel.GameName, dialog.ViewModel.ExecutableName);
        }
    }

    private void NavigateTo(object view) => MainContent.Content = view;

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    private void CloseButton_Click(object sender, RoutedEventArgs e)
        => Close();

    private void NavDashboard_Click(object sender, RoutedEventArgs e)
        => ShowDashboard();

    private void NavSettings_Click(object sender, RoutedEventArgs e)
        => NavigateTo(new SettingsView());
}