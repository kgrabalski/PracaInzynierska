using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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

        protected readonly CookieContainer CookieContainer;

        protected ServiceClientBase(CookieContainer cookieContainer)
        {
            CookieContainer = cookieContainer;
        }
        
        protected async Task<HttpBodyResponse<string>> Get(string url)
		{
            using (var handler = new HttpClientHandler() {CookieContainer = CookieContainer})
            using (var client = new HttpClient(handler))
            {
                var response = await client.GetAsync(ServiceAddress + url);

                return new HttpBodyResponse<string>
                {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync()
                };
            }
		}

        protected async Task<HttpBodyResponse<string>> Post(string url, object data)
        {
            using (var handler = new HttpClientHandler() {CookieContainer = CookieContainer})
            using (var client = new HttpClient(handler))
            {
                string jsonString = string.Empty;
                if (data != null)
                {
                    jsonString = JsonConvert.SerializeObject(data);
                }
                var response = await client.PostAsync(ServiceAddress + url, new StringContent(jsonString, Encoding.UTF8, "application/json"));
                return new HttpBodyResponse<string>()
                {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync()
                };
            }
        }

        protected async Task<HttpResponse> Delete(string url)
        {
            using (var handler = new HttpClientHandler() {CookieContainer = CookieContainer})
            using (var client = new HttpClient(handler))
            {
                var response = await client.DeleteAsync(ServiceAddress + url);
                return new HttpResponse() {StatusCode = response.StatusCode};
            }
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

