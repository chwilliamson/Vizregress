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

namespace Williamson.Example.Web
{
    public class App
    {
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

            config.MessageHandlers.Add(new MessageHandler(Uri));
            server.OpenAsync().Wait();

            return this;
        }

        public void Stop()
        {
            server.CloseAsync().Wait();
        }

        public class MessageHandler : DelegatingHandler
        {
            private Uri baseUri;
            public MessageHandler(Uri baseUri)
            {
                this.baseUri = baseUri;
            }
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var file = request.RequestUri.LocalPath.Substring(1).ToLowerInvariant();
                // Create the response. 
                using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Williamson.Example.Web.Content." + file + ".html"))
                using(StreamReader sr = new StreamReader(s))
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        
                        Content = new StringContent(sr.ReadToEnd())
                    };

                    response.Content.Headers.ContentType.CharSet = "UTF-8";
                    response.Content.Headers.ContentType.MediaType = "text/html"; 

                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }                
            }
        }
    }
}