using System.Configuration;
using System.Data;
using Stalker.Data;
using System.Windows;

namespace Stalker;

public partial class App : Application
{
    public static IGameRepository Repository { get; private set; } = null!;
    
    protected override void OnStartup(StartupEventArgs eventArgs)
    {
        base.OnStartup(eventArgs);
        var db = new AppDbContext();
        db.Database.EnsureCreated();
        Repository = new LocalRepository(db);
    }
}