using System;
using System.Windows.Input;

namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface IAuthorizationService
    {
        bool IsAuthorized {get;}
        ICommand AuthorizationCommand { get;}
        bool Authorize(string userName, string password);
    }
}

