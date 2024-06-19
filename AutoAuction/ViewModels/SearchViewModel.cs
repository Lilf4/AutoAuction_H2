using AutoAuction.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AutoAuction.ViewModels {
    public class SearchViewModel : ViewModelBase {
        public ObservableCollection<Auction> Auctions { get; } = new ObservableCollection<Auction>();

        private Auction selectedAuction;
        public Auction SelectedAuction { get { return selectedAuction; } set { this.RaiseAndSetIfChanged(ref selectedAuction, value, "SelectedAuction"); } }
        public SearchViewModel() {
            Auction[] auctions = MainWindowViewModel.Instance._IAuction.SearchAuction(MainWindowViewModel.Instance._IUser, MainWindowViewModel.Instance._IVehicle);
            Auctions = new(auctions);
            Debug.WriteLine("Auctions: " + Auctions.Count);
        }

        public void Back() {
            MainWindowViewModel.Instance.CurrViewModel = new HomeViewModel();
        }

        public void OpenAuction() {
            if (SelectedAuction.Seller.Id == MainWindowViewModel.Instance.User.Id) {
                MainWindowViewModel.Instance.CurrViewModel = new SellerViewModel(SelectedAuction);
            }
            else {
                MainWindowViewModel.Instance.CurrViewModel = new BuyerViewModel(SelectedAuction);
            }
        }
    }
}
