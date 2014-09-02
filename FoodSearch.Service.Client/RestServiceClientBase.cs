using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client
{
    public abstract class RestServiceClientBase
    {
        protected string apiPath = "http://foodsearch.azurewebsites.net/";
        private readonly HttpClient client = new HttpClient();

        protected Task<T> RestGet<T>(string url = "") where T : class
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(apiPath + url);
            Task<WebResponse> task = Task.Factory.FromAsync<WebResponse>(req.BeginGetResponse, req.EndGetResponse, null);
            return task.ContinueWith(t =>
            {
                using (Stream stream = t.Result.GetResponseStream())
                {
                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                    var result = js.ReadObject(stream) as T;
                    return result;
                }
            });
        }

        protected Task<TResponse> RestPost<TRequest, TResponse>(TRequest request, string url = "")
            where TRequest : class
            where TResponse : class
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiPath + url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            DataContractJsonSerializer jsRequest = new DataContractJsonSerializer(typeof(TRequest));
            byte[] reqData;
            using (MemoryStream memReqStream = new MemoryStream())
            {
                jsRequest.WriteObject(memReqStream, request);
                using (StreamReader sr = new StreamReader(memReqStream))
                {
                    reqData = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                }
            }

            var tStr = Task.Factory.FromAsync<Stream>(req.BeginGetRequestStream, req.EndGetRequestStream, null);
            tStr.Wait();
            using (Stream stream = tStr.Result)
            {
                stream.Write(reqData, 0, reqData.Length);
            }
            Task<WebResponse> task = Task.Factory.FromAsync<WebResponse>(req.BeginGetResponse, req.EndGetResponse, null);
            return task.ContinueWith(t =>
            {
                using (Stream stream = t.Result.GetResponseStream())
                {
                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(TResponse));
                    var result = js.ReadObject(stream) as TResponse;
                    return result;
                }
            });
        }
    }
}
