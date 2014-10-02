﻿using System;
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
			return DeserializeList<City>(response);
		}

		public async Task<IEnumerable<Street>> GetStreets(int cityId, string query)
		{
			var response = await Get(string.Format("street?cityId={0}&query={1}", cityId, query));
			return DeserializeList<Street>(response);
		}

		public async Task<IEnumerable<StreetNumber>> GetStreetNumbers(int streetId)
		{
			var response = await Get("street/" + streetId);
			return DeserializeList<StreetNumber>(response);
		}

		public async Task<IEnumerable<Restaurant>> GetRestaurants(int addressId)
		{
			var response = await Get("restaurant?addressId=" + addressId);
			return DeserializeList<Restaurant>(response);
		}

		public async Task<IEnumerable<DishGroup>> GetDishes(Guid restaurantId)
		{
			var response = await Get("dish?restaurantId=" + restaurantId);
			return DeserializeList<DishGroup>(response);
		}
	}
}
