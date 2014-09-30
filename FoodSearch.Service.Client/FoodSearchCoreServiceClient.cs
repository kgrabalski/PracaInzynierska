using System;
using System.Net;

using FoodSearch.Service.Client.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Service.Client
{
	public class FoodSearchCoreServiceClient : ServiceClientBase, IFoodSearchCoreServiceClient
	{
		protected override string ServiceAddress { get { return _baseAddress + "Core/"; } }

		public async Task<IEnumerable<City>> GetCities ()
		{
			var response = await Get ("city");
			if (response.StatusCode == System.Net.HttpStatusCode.OK) {
				return Deserialize<List<City>> (response.Body);
			}
			return new List<City> ();
		}

		public async Task<IEnumerable<Street>> GetStreets(int cityId, string query)
		{
			var response = await Get(string.Format("street?cityId={0}&query={1}", cityId, query));
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Deserialize<List<Street>>(response.Body);
			}
			return new List<Street>();
		}

		public async Task<IEnumerable<StreetNumber>> GetStreetNumbers(int streetId)
		{
			var response = await Get(string.Format("street/" + streetId));
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Deserialize<List<StreetNumber>>(response.Body);
			}
			return new List<StreetNumber>();
		}
	}
}

