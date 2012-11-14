using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Reflection;
using System.IO;
using Williamson.Example.Web.MessageHandlers;

namespace Williamson.Example.Web
{
    public class App
    {
        static void Main(string[] args) {
            var app = new App().Start();
            Console.WriteLine("Thanks for running my app.  Please visit {0} to start.",app.Uri);
            Console.ReadLine();
            app.Stop();
        }
        public Uri Uri { get; set; }
        HttpSelfHostServer server;
        public void StartAndAction(Action<Uri> doWithUri)
        {
            Start();
            try
            {
                doWithUri(Uri);
            }
            finally
            {
                Stop();
            }
        }
        public App Start()
        {
            Uri = new Uri("Http://localhost:8087");
            var config = new HttpSelfHostConfiguration(Uri);
            server = new HttpSelfHostServer(config);

            config.MessageHandlers.Add(new StaticContentResourceMessageHandler(Uri));
            server.OpenAsync().Wait();

            return this;
        }

        public void Stop()
        {
            server.CloseAsync().Wait();
        }
        
    }
}