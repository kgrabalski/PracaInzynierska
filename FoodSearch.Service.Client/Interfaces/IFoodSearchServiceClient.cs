using System;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchServiceClient
	{
		IFoodSearchCoreServiceClient Core { get; }
        IFoodSearchUserServiceClient User { get; }
        IFoodSearchOrderServiceClient Order { get; }

        event EventHandler UnauthorizedAccess;
        void OnUnauthorizedAccess();
	}
}

