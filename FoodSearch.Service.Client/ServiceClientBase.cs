using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client
{
	public abstract class ServiceClientBase
	{
		protected readonly string _serviceBase = "http://foodsearch.azurewebsites.net/MobileApi/";

		protected async Task<Response> Get(string url)
		{
			HttpClient client = new HttpClient ();
			var response = await client.GetAsync (_serviceBase + url);
			return new Response {
				StatusCode = response.StatusCode,
				Message = await response.Content.ReadAsStringAsync ()
			};
		}

		protected T Deserialize<T>(string jsonString)
		{
			return JsonConvert.DeserializeObject<T> (jsonString);
		}
	}
}

