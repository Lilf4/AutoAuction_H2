using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoAuction.Models.Vehicle;

namespace AutoAuction.ViewModels {
    public class HomeViewModel : ViewModelBase {
        public ObservableCollection<Auction> ownAuctions { get; set; } = new ObservableCollection<Auction>();
        public HomeViewModel() {

            Auction[] auctions = MainWindowViewModel.Instance._IAuction.SearchAuction(MainWindowViewModel.Instance._IUser, MainWindowViewModel.Instance._IVehicle);
            ownAuctions = new(auctions.Where(a => a.Seller.Id == MainWindowViewModel.Instance.User.Id).ToArray());
            //ownAuctions = new(auctions);
        }

        public void Profile(){
            MainWindowViewModel.Instance.CurrViewModel = new ProfileViewModel();
        }

        public void Logout() {
            MainWindowViewModel.Instance.CurrViewModel = new LoginViewModel();
        }

        public void Auction()
        {
            MainWindowViewModel.Instance.CurrViewModel = new SetForSaleViewModel();
        }

        public void Search() {
            MainWindowViewModel.Instance.CurrViewModel = new SearchViewModel();
        }
    }
}
