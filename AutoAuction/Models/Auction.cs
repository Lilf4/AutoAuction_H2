using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public User Seller { get; set; }
        public Vehicle Vehicle { get; set; } 
        public decimal MinimumPrice { get; set; }
        public DateTime StartDate { get; set; }
        
        public Auction(int auctionID, User seller, Vehicle vehicle, decimal minimumPrice, DateTime startDate)
        {
            AuctionID = auctionID;
            Seller = seller;
            Vehicle = vehicle;
            MinimumPrice = minimumPrice;
            StartDate = startDate;
        }
    }


    


}
