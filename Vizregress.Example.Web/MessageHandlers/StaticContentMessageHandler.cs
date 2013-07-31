using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vizregress.Example.Web.MessageHandlers
{
    /// <summary>
    /// Static Resource Handler
    /// </summary>
    public class StaticContentResourceMessageHandler: DelegatingHandler
    {
        private Uri baseUri;        

        public StaticContentResourceMessageHandler(Uri baseUri)
        {
            this.baseUri = baseUri;
        }
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var file = request.RequestUri.LocalPath.Substring(1).ToLowerInvariant();
            if (file.StartsWith("api/"))
            {
                //pass-through api requests
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                var tsc = new TaskCompletionSource<HttpResponseMessage>();            
                tsc.SetResult(this.Handle(file));
                return tsc.Task;
            }
            
        }
        private const string CONTENT = "Vizregress.Example.Web.Content";

        private HttpResponseMessage Handle(string file)
        {
            if (string.IsNullOrEmpty(file)) file = "start";
            if (string.Equals("css", file, StringComparison.OrdinalIgnoreCase))
            {
                return Create(this.Css(), null, "text/css");
            }
            if (string.Equals("js", file, StringComparison.OrdinalIgnoreCase))
            {
                return Create(this.Js(), null, "text/javascript");
            }
            if( file.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) {
                return File(CONTENT + "Images." + file, "image/png");
            }

            return Create(CombineStreams(CONTENT + ".Html", "html", file),"UTF-8","text/html");

        }

        private HttpResponseMessage Create(string contents, string charset, string mediaType)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                
                Content = new StringContent(contents)
            };

            response.Content.Headers.ContentType.CharSet = charset;
            response.Content.Headers.ContentType.MediaType = mediaType;
            return response;
        }

        /// <summary>
        /// Serve the css as one big file
        /// </summary>
        /// <returns></returns>
        protected string Css()
        {
            return CombineStreams(CONTENT + ".Css","css", "jquery-ui-1.9.1","bootstrap");
        }

        /// <summary>
        /// Serve the JavaScript as one big file
        /// </summary>
        /// <returns></returns>
        protected string Js()
        {
            return CombineStreams(CONTENT + ".JavaScript", "js", "jquery-1.8.2", "jquery-ui-1.9.1.custom", "knockout", "knockout.mapping", "application");
        }

        /// <summary>
        /// Server a file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        protected HttpResponseMessage File(string file, string mediaType)
        {            
            using (var s = Assembly
               .GetExecutingAssembly()
               .GetManifestResourceStream(file))
            using (var br = new BinaryReader(s))
            {
                if (s.Length > Int32.MaxValue) throw new ApplicationException("Too Big");
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(br.ReadBytes((int)s.Length))
                };

                response.Content.Headers.ContentType.MediaType = mediaType;
                return response;
            }            
           
        }

        /// <summary>
        /// Combine the streams
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <param name="fullnames"></param>
        /// <returns></returns>
        protected string CombineStreams(string prefix,string suffix, params string[] fullnames)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var name in fullnames)
            {
                var src = prefix + "." + name + "." + suffix;

                using (var s = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(src))
                {
                    if (s == null) 
                        throw new ApplicationException("Resource not found: " + src);
                    using (StreamReader sr = new StreamReader(s))
                    {
                        sb.AppendLine(sr.ReadToEnd());
                    }
                }
            }
            return sb.ToString();
        }       
    }
}
