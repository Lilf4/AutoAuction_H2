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
        public ObservableCollection<Bus> busesObservable { get; set; }
        public HomeViewModel() {
            List<Bus> buses = new List<Bus> {
                new Bus(1, "Bus1", 1000, "AB12345", 2020, false, 0.5, FuelTypes.Diesel, 4.5f, 10.0f, 5000, 50, 20, true),
                new Bus(2, "Bus2", 2000, "CD67890", 2021, true, 0.6, FuelTypes.Electric, 5.0f, 12.0f, 6000, 60, 30, false)
            };
            busesObservable = new ObservableCollection<Bus>(buses);
        }

        public void Profile(){
            MainWindowViewModel.Instance.CurrViewModel = new ProfileViewModel();
        }
    }
}
