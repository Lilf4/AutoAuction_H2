using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels {
    public class LoginViewModel : ViewModelBase {
        public LoginViewModel() {

        }

        public void Login() {
            MainWindowViewModel.Instance.CurrViewModel = new HomeViewModel();
        }

        public void CreateUser() {
            MainWindowViewModel.Instance.CurrViewModel = new UserCreationViewModel();
        }
    }

}
