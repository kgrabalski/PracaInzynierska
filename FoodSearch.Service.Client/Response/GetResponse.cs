using System;
using System.Net;

namespace FoodSearch.Service.Client.Response
{
	public class GetResponse : HttpResponse
	{
		public string Body { get; set; }
	}
}

