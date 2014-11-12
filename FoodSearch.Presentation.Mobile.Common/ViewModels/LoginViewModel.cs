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
        public LoginViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {    
            if (Services.Settings.Contains(_rememberMeKey))
            {
                Email = Services.Settings.Get(_emailKey, string.Empty);
                Password = Services.Settings.Get(_passwordKey, string.Empty);
                RememberMe = true;
            }
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

        private bool _rememberMe = false;

        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }

        private readonly string _rememberMeKey = "rememberMe";
        private readonly string _passwordKey = "userPassword";
        private readonly string _emailKey = "userEmail";

        private Command _loginCommand;

        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(async () =>
                    {
                        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                        {
                            bool success = await Services.Authorization.Authorize(Email, Password);
                            if (success)
                            {
                                Services.Settings.Clear();
                                if (RememberMe)
                                {
                                    Services.Settings.Set(_rememberMeKey, "yes");
                                    Services.Settings.Set(_emailKey, Email);
                                    Services.Settings.Set(_passwordKey, Password);
                                }
                                Services.Dialog.Toast("Zalogowano pomyślnie");
                                await Services.Navigation.Navigate.PopAsync();
                            } else {
                                Services.Dialog.Toast("Błędny adres email lub hasło");
                                Password = string.Empty;
                                Email = string.Empty;
                            }
                        }
                    }));
            }
        }
    }
}
