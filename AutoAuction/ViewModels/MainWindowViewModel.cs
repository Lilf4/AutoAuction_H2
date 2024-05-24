using ReactiveUI;

namespace AutoAuction.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase currViewModel;
    public ViewModelBase CurrViewModel {
        get => currViewModel;
        set => this.RaiseAndSetIfChanged(ref currViewModel, value);
    }

    public static MainWindowViewModel Instance { get; set; }
    public MainWindowViewModel() {
        if(Instance == null) {
            Instance = this;
        }
        CurrViewModel = new LoginViewModel();
    }
}
