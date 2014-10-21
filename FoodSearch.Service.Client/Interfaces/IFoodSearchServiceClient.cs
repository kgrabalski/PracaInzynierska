using System;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchServiceClient : IServiceClient
	{
		IFoodSearchCoreServiceClient Core { get; }
        IFoodSearchUserServiceClient User { get; }
        IFoodSearchOrderServiceClient Order { get; }
	}
}

