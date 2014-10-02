using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Service.Client.Interfaces
{
	public interface IFoodSearchCoreServiceClient
	{
		Task<IEnumerable<City>> GetCities();
	    Task<IEnumerable<Street>> GetStreets(int cityId, string query);
	    Task<IEnumerable<StreetNumber>> GetStreetNumbers(int streetId);
	    Task<IEnumerable<Restaurant>> GetRestaurants(int addressId);
	    Task<IEnumerable<DishGroup>> GetDishes(Guid restaurantId);
	}
}

