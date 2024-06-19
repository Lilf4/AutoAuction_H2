using AutoAuction.Models;

namespace AutoAuction.DAL {
    public interface IAuction {
		void CreateAuction(Auction auction, string vehicleType);
		void PlaceBid();
        Auction[] SearchAuction(IUser userInterface, IVehicle vehicleInterface);
    }
}
