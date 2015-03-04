using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Interfaces;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IFoodSearchServiceClient _client;
        private readonly INavigationService _navigation;

        private bool _isAuthorized = false;

        public AuthorizationService(IFoodSearchServiceClient client, INavigationService navigationService)
        {
            _client = client;
            _navigation = navigationService;
            _client.UnauthorizedAccess += (sender, e) => AuthorizationCommand.Execute(null);
        }

        public bool IsAuthorized { get { return _isAuthorized; } }

        public System.Windows.Input.ICommand AuthorizationCommand
        {
            get
            {
                return new Command(async () =>
                    {
                        if (IsAuthorized) return;
                        await _navigation.Navigate.PushAsync(ViewLocator.Authorize);
                    });
            }
        }
        
        public async Task<bool> Authorize(string email, string password)
        {
            _isAuthorized = await _client.User.Login(email, password);
            return IsAuthorized;
        }

        public async Task<bool> Logout()
        {
            //if (!IsAuthorized) return false;
            _isAuthorized = !await _client.User.Logout();
            return IsAuthorized;
        }
    }
}

