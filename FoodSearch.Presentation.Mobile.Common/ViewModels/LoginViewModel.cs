using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {    
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private Command _loginCommand;

        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(async () =>
                    {
                        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                        {
                            bool success = await AuthorizationService.Authorize(Email, Password);
                            if (success)
                            {
                                DialogService.Toast("Zalogowano pomyślnie");
                                await NavigationService.Navigation.PopAsync();
                            } else {
                                DialogService.Toast("Błędny adres email lub hasło");
                                Password = string.Empty;
                                Email = string.Empty;
                            }
                        }
                    }));
            }
        }
    }
}
