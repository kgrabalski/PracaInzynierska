using FoodSearch.Service.Client.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchCoreServiceClient
	{
		Task<ObservableCollection<City>> GetCities();
	    Task<ObservableCollection<Street>> GetStreets(int cityId, string query);
	    Task<ObservableCollection<StreetNumber>> GetStreetNumbers(int streetId);
	    Task<ObservableCollection<Restaurant>> GetRestaurants(int addressId);
	    Task<ObservableCollection<DishGroup>> GetDishes(Guid restaurantId);
        Task<byte[]> GetLogo(int logoId);
        Task<ObservableCollection<Opinion>> GetOpinions(Guid restaurantId, int rating = 0, int page = 0);
	}
}

