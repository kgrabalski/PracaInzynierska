using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace FoodSearch.Service.Client
{
    public abstract class RestServiceClientBase
    {
        protected string apiPath = "http://foodsearch.azurewebsites.net/";
        private readonly HttpClient client = new HttpClient();

        protected async Task<TResponse> RestGet<TResponse>(string url = "")
            where TResponse : class
        {
            return await ReturnResponse<TResponse>(await client.GetAsync(apiPath + url));
        }

        protected async Task<TResponse> RestPost<TResponse>(object request, string url = "")
            where TResponse : class
        {
            string json = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(json, Encoding.UTF8);
            return await ReturnResponse<TResponse>(await client.PostAsync(apiPath + url, content));
        }

        protected async Task<TResponse> RestPut<TResponse>(object request, string url = "")
            where TResponse : class
        {
            string json = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(json, Encoding.UTF8);
            return await ReturnResponse<TResponse>(await client.PutAsync(apiPath + url, content));
        }

        protected async Task<TResponse> RestDelete<TResponse>(string url = "")
            where TResponse : class
        {
            return await ReturnResponse<TResponse>(await client.DeleteAsync(apiPath + url));
        }

        private static async Task<T> ReturnResponse<T>(HttpResponseMessage resp)
            where T : class
        {
            using (Stream respStream = await resp.Content.ReadAsStreamAsync())
            using (StreamReader respReader = new StreamReader(respStream))
            {
                return JsonConvert.DeserializeObject<T>(await respReader.ReadToEndAsync());
            }
        }
    }
}
