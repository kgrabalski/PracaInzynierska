using System;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace FoodSearch.Presentation.Mobile.Common
{
	public class TestClient
	{
		public async static Task<GetResponse> GetData(){
			return new GetResponse ();
		}
	}
}

