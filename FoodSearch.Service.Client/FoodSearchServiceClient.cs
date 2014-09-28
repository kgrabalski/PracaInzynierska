using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client
{
	public class FoodSearchServiceClient : ServiceClientBase
	{
		public async Task<IEnumerable<City>> GetCities()
		{
			var response = await Get ("city");
			if (response.StatusCode == System.Net.HttpStatusCode.OK) {
				return Deserialize<List<City>> (response.Message);
			}
			return new List<City> ();
		}
	}
}

