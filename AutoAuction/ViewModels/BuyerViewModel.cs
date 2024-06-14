using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoAuction.Models.Vehicle;

namespace AutoAuction.ViewModels {
    public class BuyerViewModel : ViewModelBase {
        public ObservableCollection<Bus> busesObservable { get; set; }
        public BuyerViewModel() {
        }
    }
}
