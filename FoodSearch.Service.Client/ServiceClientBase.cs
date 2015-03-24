using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client
{
    public abstract class ServiceClientBase
	{
        protected readonly string BaseAddress = "http://foodsearch.azurewebsites.net/MobileApi/";
		protected abstract string ServiceAddress { get ; }
        protected readonly CookieContainer CookieContainer;
        public IFoodSearchServiceClient ClientHandler { get; set; }
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
                var result = new HttpBodyResponse<string>
                {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync()
                };
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                    ClientHandler.OnUnauthorizedAccess();
                return result;
            }
		}
        protected async Task<HttpBodyResponse<string>> Post(string url, object data)
        {
            using (var handler = new HttpClientHandler() {CookieContainer = CookieContainer})
            using (var client = new HttpClient(handler))
            {
                var content = prepareRequestContent(data);
                var response = await client.PostAsync(ServiceAddress + url, content);
                var result = new HttpBodyResponse<string>()
                {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync()
                };
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                    ClientHandler.OnUnauthorizedAccess();
                return result;
            }
        }
        protected async Task<HttpBodyResponse<string>> Put(string url, object data)
        {
            using (var handler = new HttpClientHandler() {CookieContainer = CookieContainer})
            using (var client = new HttpClient(handler))
            {
                var content = prepareRequestContent(data);
                var response = await client.PutAsync(ServiceAddress + url, content);
                var result = new HttpBodyResponse<string>()
                {
                    StatusCode = response.StatusCode,
                    Body = await response.Content.ReadAsStringAsync()
                };
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                    ClientHandler.OnUnauthorizedAccess();
                return result;
            }
        }
        private HttpContent prepareRequestContent(object data)
        {
            string jsonString = string.Empty;
            if (data != null)
            {
                jsonString = JsonConvert.SerializeObject(data);
            }
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
        protected async Task<HttpResponse> Delete(string url)
        {
            using (var handler = new HttpClientHandler() {CookieContainer = CookieContainer})
            using (var client = new HttpClient(handler))
            {
                var response = await client.DeleteAsync(ServiceAddress + url);
                var result = new HttpResponse() {StatusCode = response.StatusCode};
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                    ClientHandler.OnUnauthorizedAccess();
                return result;
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

