using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Service.Client
{
	public class FoodSearchServiceClient : IFoodSearchServiceClient
	{
		private IFoodSearchCoreServiceClient _core;

		public IFoodSearchCoreServiceClient Core { get { return _core ?? (_core = new FoodSearchCoreServiceClient ()); } }
	}
}

