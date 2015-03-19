using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace FoodSearch.Service.Client
{
	public class FoodSearchCoreServiceClient : ServiceClientBase, IFoodSearchCoreServiceClient
	{
	    public FoodSearchCoreServiceClient(CookieContainer cookieContainer) : base(cookieContainer)
	    {
	    }

	    protected override string ServiceAddress { get { return BaseAddress + "Core/"; } }

		public async Task<ObservableCollection<City>> GetCities ()
		{
			var response = await Get ("city");
			return DeserializeList<City>(response);
		}

        public async Task<ObservableCollection<Street>> GetStreets(int cityId, string query)
		{
			var response = await Get(string.Format("street?cityId={0}&query={1}", cityId, query));
			return DeserializeList<Street>(response);
		}

        public async Task<ObservableCollection<StreetNumber>> GetStreetNumbers(int streetId)
		{
			var response = await Get("street/" + streetId);
			return DeserializeList<StreetNumber>(response);
		}

        public async Task<ObservableCollection<Restaurant>> GetRestaurants(int addressId)
		{
			var response = await Get("restaurant?addressId=" + addressId);
			return DeserializeList<Restaurant>(response);
		}

        public async Task<ObservableCollection<DishGroup>> GetDishes(Guid restaurantId)
		{
			var response = await Get("dish?restaurantId=" + restaurantId);
			return DeserializeList<DishGroup>(response);
		}

        public Task<byte[]> GetLogo(int logoId)
        {
            var client = new HttpClient();
            return client.GetByteArrayAsync(ServiceAddress + "Logo/" + logoId.ToString());
        }

	    public async Task<ObservableCollection<Opinion>> GetOpinions(Guid restaurantId, int rating = 0, int page = 0)
	    {
	        string url = string.Format("/opinion?restaurantId={0}&rating={1}&page={2}", restaurantId, rating, page);
	        var response = await Get(url);
	        return DeserializeList<Opinion>(response);
	    }
	}
}

