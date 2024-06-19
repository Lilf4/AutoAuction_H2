using AutoAuction.Models;

namespace AutoAuction.DAL {
    public interface IAuction {
		void CreateAuction();
		void PlaceBid();
        Auction[] SearchAuction(IUser userInterface, IVehicle vehicleInterface);
    }
}
