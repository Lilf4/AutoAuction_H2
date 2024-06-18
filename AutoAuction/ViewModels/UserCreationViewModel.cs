using AutoAuction.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels {
    public class UserCreationViewModel : ViewModelBase {
        
        private User user = new User();
        public User User { 
            get { return user; } 
            set { this.RaiseAndSetIfChanged(ref user, value, "User"); }
        }

        private string rePassword;
        public string RePassword {
            get { return rePassword; }
            set { this.RaiseAndSetIfChanged(ref rePassword, value, "RePassword"); }
        }

        private bool isCorporate;
        public bool IsCorporate {
            get { return isCorporate; }
            set { this.RaiseAndSetIfChanged(ref isCorporate, value, "IsCorporate"); }
        }

        private bool isPrivate;
        public bool IsPrivate {
            get { return isPrivate; }
            set { this.RaiseAndSetIfChanged(ref isPrivate, value, "IsPrivate"); }
        }

        private string cvpr;
        public string CVPR {
            get { return cvpr; }
            set { this.RaiseAndSetIfChanged(ref cvpr, value, "CVPR"); }
        }

        public UserCreationViewModel() {
            
        }

        public void CreateUser() {
            if(User.Password != RePassword) {
                Debug.WriteLine("Passwords do not match");
                return;
            }

            if(!User.IsEmailValid(User.UserName, out string errMsg)) {
                Debug.WriteLine(errMsg);
                return;
            }

            if(!User.IsPasswordValid(User.Password, out errMsg)) {
                Debug.WriteLine(errMsg);
                return;
            }

            if(!int.TryParse(CVPR, out _)) {
                Debug.WriteLine("CPR/CVR Must be numeric");
                return;
            }

            if(IsCorporate && CVPR.Length != 8) {
                Debug.WriteLine("CVR must have a length of 8 digits");
                return;
            }

            if(IsPrivate && CVPR.Length != 10) {
                Debug.WriteLine("CPR must have a length of 10 digits");
                return;
            }

            if(isPrivate) {
                cvpr = cvpr.Insert(6, "-");
            }

            MainWindowViewModel.Instance._IUser.Register(User, IsCorporate, CVPR, CVPR);
            MainWindowViewModel.Instance.CurrViewModel = new LoginViewModel();
        }

        public void Cancel() {
            MainWindowViewModel.Instance.CurrViewModel = new LoginViewModel();
        }
    }
}
