using System;
using System.Net;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchServiceClient
	{
		IFoodSearchCoreServiceClient Core { get; }
        IFoodSearchUserServiceClient User { get; }
        IFoodSearchOrderServiceClient Order { get; }

        event EventHandler UnauthorizedAccess;
        void OnUnauthorizedAccess();
        CookieContainer Cookies { get; }
	}
}

