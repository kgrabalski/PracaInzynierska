using System;
using System.Collections.Generic;
using System.Net;

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

		protected async Task<HttpResponse> Get(string url)
		{
			HttpClient client = new HttpClient ();
			var response = await client.GetAsync (ServiceAddress + url);
			return new HttpResponse {
				StatusCode = response.StatusCode,
				Body = await response.Content.ReadAsStringAsync ()
			};
		}

		protected T Deserialize<T>(string jsonString)
		{
			return JsonConvert.DeserializeObject<T> (jsonString);
		}

		protected List<T> DeserializeList<T>(HttpResponse response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Deserialize<List<T>>(response.Body);
			}
			return new List<T>();
		} 
	}
}

