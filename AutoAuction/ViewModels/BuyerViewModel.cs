using AutoAuction.Models;
using ReactiveUI;

namespace AutoAuction.ViewModels {
    public class BuyerViewModel : ViewModelBase {
        
        private Auction auction;
        public Auction Auction {
            get {
                return auction;
            } 
            set { 
                this.RaiseAndSetIfChanged(ref auction, value, "Auction");
            } 
        }
        public BuyerViewModel(Auction auction) {
            Auction = auction;
        }
    }
}
