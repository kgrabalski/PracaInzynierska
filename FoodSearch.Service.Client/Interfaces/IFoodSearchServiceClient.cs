using System;

namespace FoodSearch.Service.Client.Interfaces
{
	public interface IFoodSearchServiceClient
	{
		IFoodSearchCoreServiceClient Core { get; }
	}
}

