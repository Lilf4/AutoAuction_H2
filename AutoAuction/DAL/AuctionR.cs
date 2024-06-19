using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.DAL {
    internal class AuctionR : DBUtil, IAuction {
        public void CreateAuction() {
            throw new NotImplementedException();
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
