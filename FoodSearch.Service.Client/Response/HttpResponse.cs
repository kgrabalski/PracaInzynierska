using System;
using System.Net;

namespace FoodSearch.Service.Client.Response
{
	public class HttpResponse<T>
	{
		public HttpStatusCode StatusCode { get; set; }
	    public T Body { get; set; }
	}
}

