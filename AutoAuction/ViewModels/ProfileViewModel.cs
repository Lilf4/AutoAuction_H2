using AutoAuction.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoAuction.Models.Vehicle;

namespace AutoAuction.ViewModels {
    public class ProfileViewModel : ViewModelBase {

        private User observableUser;
        public User ObservableUser
        {
            get => observableUser;
            set => this.RaiseAndSetIfChanged(ref observableUser, value);
        }
        public ProfileViewModel() {
            ObservableUser = new(1, "Test@TestUser.com", 1234, 0);
        }

        public void Back()
        {
            MainWindowViewModel.Instance.CurrViewModel = new HomeViewModel();
        }
    }
}
