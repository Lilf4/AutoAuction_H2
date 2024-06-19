using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.DAL {
    internal class AuctionR : DBUtil, IAuction {
        public void CreateAuction(Auction auction, string vehicleType) {
            SqlConnection conn = GetConnection(MasterUser);
            conn.Open();
            SqlCommand cmd = new SqlCommand("CreateAuction_sp", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SellerID", auction.Seller.Id);
            cmd.Parameters.AddWithValue("@VehicleType", vehicleType);
            cmd.Parameters.AddWithValue("@RegCode", auction.Vehicle.RegCode);
            cmd.Parameters.AddWithValue("@EnergyClass", auction.Vehicle.EnergyClass);
            cmd.Parameters.AddWithValue("@Name", auction.Vehicle.Name);
            cmd.Parameters.AddWithValue("@KmDriven", auction.Vehicle.KmDriven);
            cmd.Parameters.AddWithValue("@Year", auction.Vehicle.Year);
            cmd.Parameters.AddWithValue("@TowHook", auction.Vehicle.TowHook);
            cmd.Parameters.AddWithValue("@LicenseType", (byte)auction.Vehicle.LicenseType);
            cmd.Parameters.AddWithValue("@MotorSize", auction.Vehicle.MotorSize);
            cmd.Parameters.AddWithValue("@KmPerUnit", auction.Vehicle.KmPerUnit);
            cmd.Parameters.AddWithValue("@FuelType", (byte)auction.Vehicle.FuelType);
            cmd.Parameters.AddWithValue("@MinimumPrice", auction.MinimumPrice);
            switch (vehicleType) {
                case "Private":
                    PrivatePersonalCar privateCar = (PrivatePersonalCar)auction.Vehicle;
                    cmd.Parameters.AddWithValue("@IsoFix", privateCar.Isofix);
                    cmd.Parameters.AddWithValue("@BootSize", privateCar.BootSize);
                    cmd.Parameters.AddWithValue("@NumberOfSeats", privateCar.NumberOfSeats);
                    break;
                case "Professional":
                    ProfessionelPersonalCar professionalCar = (ProfessionelPersonalCar)auction.Vehicle;
                    cmd.Parameters.AddWithValue("@SafetyBar", professionalCar.SafetyBar);
                    cmd.Parameters.AddWithValue("@BootSize", professionalCar.BootSize);
                    break;
                case "Truck":
                    Truck truck = (Truck)auction.Vehicle;
                    cmd.Parameters.AddWithValue("@Height", truck.Height);
                    cmd.Parameters.AddWithValue("@Length", truck.Length);
                    cmd.Parameters.AddWithValue("@Weight", truck.Weight);
                    cmd.Parameters.AddWithValue("@LoadCapacity", truck.LoadCapacity);
                    break;
                case "Bus":
                    Bus bus = (Bus)auction.Vehicle;
                    cmd.Parameters.AddWithValue("@Height", bus.Height);
                    cmd.Parameters.AddWithValue("@Length", bus.Length);
                    cmd.Parameters.AddWithValue("@Weight", bus.Weight);
                    cmd.Parameters.AddWithValue("@NumberOfSeats", bus.Seats);
                    cmd.Parameters.AddWithValue("@NumberOfSleepingSpots", bus.StandingPlaces);
                    cmd.Parameters.AddWithValue("@Toilet", bus.Toilets);
                    break;
            }
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void GetAuction() {
            throw new NotImplementedException();
        }

        public void PlaceBid() {
            throw new NotImplementedException();
        }
        
        //Dumbed down to get all auctions with paging
        public Auction[] SearchAuction(IUser userInterface, IVehicle vehicleInterface) {
            SqlConnection conn = GetConnection(MasterUser);
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetActiveAuctions_sp", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            List<Auction> auctions = new List<Auction>();
            while (reader.Read()) {
                User seller = userInterface.GetUser(reader.GetString(reader.GetOrdinal("SellerUsername")));
                Vehicle vehicle = vehicleInterface.GetVehicle(reader.GetInt32(reader.GetOrdinal("VehicleID")));

                Auction auction = new Auction(
                    auctionID: reader.GetInt32(reader.GetOrdinal("AuctionID")),
                    seller: seller,
                    vehicle: vehicle,
                    minimumPrice: reader.GetDecimal(reader.GetOrdinal("MinimumPrice"))
                    );
                auctions.Add(auction);
            }
            return auctions.ToArray();
        }
    }
}
