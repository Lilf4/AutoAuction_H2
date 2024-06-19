using AutoAuction.DAL;
using AutoAuction.Models;
using ReactiveUI;

namespace AutoAuction.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase currViewModel;
    public ViewModelBase CurrViewModel {
        get => currViewModel;
        set => this.RaiseAndSetIfChanged(ref currViewModel, value);
    }

    public IAuction _IAuction { get; set; }
    public IUser _IUser { get; set; }
    public IVehicle _IVehicle { get; set; }

    public User User { get; set; }
    public static MainWindowViewModel Instance { get; set; }

    public MainWindowViewModel(IAuction _IAuction, IUser _IUser, IVehicle _IVehicle) {
        if (Instance == null) {
            this._IAuction = _IAuction;
            this._IUser = _IUser;
            this._IVehicle = _IVehicle;
            Instance = this;
        }
        CurrViewModel = new LoginViewModel();
    }
}
