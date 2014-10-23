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

        protected async Task<HttpBodyResponse<string>> Get(string url)
		{
            try
            {
                HttpClient client = new HttpClient ();
                
                var response = await client.GetAsync (ServiceAddress + url);

                return new HttpBodyResponse<string> {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync ()
                };
            }
            catch (AggregateException ex)
            {
                throw;
            }
		}

        protected async Task<HttpResponse> Post(string url, object data)
        {
            HttpClient client = new HttpClient();
            string jsonString = string.Empty;
            if (data != null) 
            {
                jsonString = JsonConvert.SerializeObject(data);
            }
            var response = await client.PostAsync(ServiceAddress + url, new StringContent(jsonString));
            return new HttpResponse() { StatusCode = response.StatusCode };
        }

        protected async Task<HttpResponse> Delete(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ServiceAddress + url);
            return new HttpResponse() { StatusCode = response.StatusCode };
        }

		protected T Deserialize<T>(string jsonString)
		{
			return JsonConvert.DeserializeObject<T> (jsonString);
		}

        protected ObservableCollection<T> DeserializeList<T>(HttpBodyResponse<string> response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
                return Deserialize<ObservableCollection<T>>(response.Body);
			}
            return new ObservableCollection<T>();
		} 
	}
}

