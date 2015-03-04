
using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(FoodSearch.Presentation.Web.Site.WebSocket.WebSocketStartup))]
namespace FoodSearch.Presentation.Web.Site.WebSocket
{
    public class WebSocketStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
