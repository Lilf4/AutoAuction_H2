using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AutoAuction.ViewModels;
using AutoAuction.Views;
using AutoAuction.DAL;

namespace AutoAuction;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    static IAuction auctionR = new AuctionR();
    static IUser userR = new UserR();
    static IVehicle vehicleR = new VehicleR();

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(auctionR, userR, vehicleR),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}