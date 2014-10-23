using System;

namespace FoodSearch.Service.Client.Response
{
    public class HttpBodyResponse<T> : HttpResponse
    {
        public T Body { get; set; }
    }
}

