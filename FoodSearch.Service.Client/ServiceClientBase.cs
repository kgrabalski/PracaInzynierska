using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Response;

namespace FoodSearch.Service.Client
{
	public abstract class ServiceClientBase
	{
		protected readonly string _baseAddress = "http://foodsearch.azurewebsites.net/MobileApi/";

		protected abstract string ServiceAddress { get ; }

		protected async Task<GetResponse> Get(string url)
		{
			HttpClient client = new HttpClient ();
			var response = await client.GetAsync (ServiceAddress + url);
			return new GetResponse {
				StatusCode = response.StatusCode,
				Body = await response.Content.ReadAsStringAsync ()
			};
		}

		protected T Deserialize<T>(string jsonString)
		{
			return JsonConvert.DeserializeObject<T> (jsonString);
		}
	}
}

