using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SlimWin.Core;
using SlimWin.Desktop.ViewModels;
using SlimWin.Desktop.Views;

namespace SlimWin.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var debloatService = new SimulatedDebloatService();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(debloatService),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
