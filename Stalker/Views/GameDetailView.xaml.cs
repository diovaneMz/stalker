using System.Windows.Controls;
using Stalker.ViewModels;

namespace Stalker.Views;

public partial class GameDetailView : UserControl
{
    public GameDetailView(GameItemViewModel game, Action goBack)
    {
        InitializeComponent();
        DataContext = new GameDetailViewModel(game, goBack);
    }
}
