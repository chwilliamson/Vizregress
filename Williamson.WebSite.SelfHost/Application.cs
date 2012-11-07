using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;

namespace Williamson.WebSite.SelfHost
{
    public class Application
    {
        HttpSelfHostServer server;
        public void Start()
        {
            Server
            var config = new HttpSelfHostConfiguration("http://localhost:8081");
            config.Routes.MapHttpRoute("default", "{controller}/{action}");
            server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }

        public void Stop()
        {
            server.CloseAsync();
        }

        public Uri Home
        {
            get { return new Uri("http://localhost:8081"); }
        }
    }
}
