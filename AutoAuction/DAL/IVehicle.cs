using AutoAuction.Models;

namespace AutoAuction.DAL {
    public interface IVehicle {
        Vehicle? GetVehicle(int ID);
    }
}
