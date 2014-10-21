using System;
using System.Collections.Generic;
using System.Net;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Response;
using System.Collections.ObjectModel;

namespace FoodSearch.Service.Client
{
    public abstract class ServiceClientBase
	{
        protected readonly string _baseAddress = "http://foodsearch.azurewebsites.net/MobileApi/";

		protected abstract string ServiceAddress { get ; }

        protected async Task<HttpResponse<string>> Get(string url)
		{
            try
            {
                HttpClient client = new HttpClient ();
                var response = await client.GetAsync (ServiceAddress + url);
                return new HttpResponse<string> {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync ()
                };
            }
            catch (AggregateException ex)
            {
                throw;
            }
			
		}

		protected T Deserialize<T>(string jsonString)
		{
			return JsonConvert.DeserializeObject<T> (jsonString);
		}

        protected ObservableCollection<T> DeserializeList<T>(HttpResponse<string> response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
                return Deserialize<ObservableCollection<T>>(response.Body);
			}
            return new ObservableCollection<T>();
		} 
	}
}

