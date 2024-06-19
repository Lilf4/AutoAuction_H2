using AutoAuction.Models;
using System.Data.SqlClient;

namespace AutoAuction.DAL {
    public class DBUtil {

        public DBUtil() {}

        public User MasterUser { get; } = new(0, "sa", "H2PD040124_Gruppe1", "0000", 0);
        public User NewUserCreator { get; } = new User(0, "NewUserCreator", "Creator123!", "0000", 0);

        public SqlConnection GetConnection(User user) {
            return new SqlConnection($"Data Source=docker.data.techcollege.dk,20001;Initial Catalog=AutoAuctionDB; User ID={user.UserName}; Password={user.Password}");
        }
    }
}
