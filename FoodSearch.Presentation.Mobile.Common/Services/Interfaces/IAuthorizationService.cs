using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface IAuthorizationService
    {
        bool IsAuthorized {get;}
        ICommand AuthorizationCommand { get;}
        Task<bool> Authorize(string email, string password);
        Task<bool> Logout();
    }
}

