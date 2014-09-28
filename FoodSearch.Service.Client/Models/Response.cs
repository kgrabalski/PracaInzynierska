using System;
using System.Net;

namespace FoodSearch.Service.Client
{
	public class Response
	{
		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }
	}
}

