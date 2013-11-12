﻿using System;
using System.Web.Http.SelfHost;
using System.Web.Http;
using Vizregress.Example.Web.MessageHandlers;

namespace Vizregress.Example.Web
{
    /// <summary>
    /// Self hosted web application
    /// </summary>
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
       
        /// <summary>
        /// Start the self host
        /// </summary>
        /// <returns></returns>
        public App Start()
        {
            Uri = new Uri("Http://localhost:8087");
            var config = new HttpSelfHostConfiguration(Uri);
            //mapping a default api
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            server = new HttpSelfHostServer(config);
            config.MessageHandlers.Add(new StaticContentResourceMessageHandler(Uri));
            server.OpenAsync().Wait();
            return this;
        }

        /// <summary>
        /// Stop the self host
        /// </summary>
        public void Stop()
        {
            server.CloseAsync().Wait();
        }
        
    }
}