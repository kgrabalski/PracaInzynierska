using System;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private bool _isAuthorized = false;

        public bool Authorize(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorized { get { return _isAuthorized; } }

        public System.Windows.Input.ICommand AuthorizationCommand
        {
            get
            {
                return new Command(async () =>
                    {
                        await NavigationService.Navigation.PushAsync(ViewLocator.Authorize);
                    });
            }
        }
    }
}

