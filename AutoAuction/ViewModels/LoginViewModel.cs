using AutoAuction.DAL;
using AutoAuction.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels {
    public class LoginViewModel : ViewModelBase {

        private string username;
        public string Username {
            get { return username; }
            set { this.RaiseAndSetIfChanged(ref username, value, "Username"); }
        }

        private string password;
        public string Password {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value, "Password"); }
        }

        public LoginViewModel() {

        }

        public void Login() {
            User user = MainWindowViewModel.Instance._IUser.Login(Username, Password);


            if (user == null) {
                //return; Uncomment when in Production
            }

            MainWindowViewModel.Instance.User = user;
            
            MainWindowViewModel.Instance.CurrViewModel = new HomeViewModel();
        }

        public void CreateUser() {
            MainWindowViewModel.Instance.CurrViewModel = new UserCreationViewModel();
        }
    }

}
