using AutoAuction.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoAuction.Models.Vehicle;

namespace AutoAuction.ViewModels {
    public class SellerViewModel : ViewModelBase {
        private Auction auction;
        public Auction Auction {
            get {
                return auction;
            }
            set {
                this.RaiseAndSetIfChanged(ref auction, value, "Auction");
            }
        }
        public SellerViewModel(Auction auction) {
            Auction = auction;
        }
    }
}
