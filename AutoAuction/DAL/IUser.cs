using AutoAuction.Models;

namespace AutoAuction.DAL {
    public interface IUser {
		void Register(User user, bool isCorporate, string CPR, string CVR);
		User? Login(string username, string password);
		void Logout();
    }
}
