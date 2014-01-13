using System;
using System.IO;
using System.Reflection;

namespace Vizregress.BDD.Examples
{
    /// <summary>
    /// Responsible for resolving resources
    /// </summary>
    public class ExpectedResourceResolver
    {
        /// <summary>
        /// Resolve an image stream based on <see cref="IInformationalWebDriver">driver information</see>
        /// </summary>
        /// <param name="name">Name of the resource</param>
        /// <param name="infoDriver"></param>
        /// <returns></returns>
        public Stream Resolve(string name, IInformationalWebDriver infoDriver)
        {
            //load base reference image
            var split = name.Split('/','.');
            var locale = "";
            if (infoDriver.Locale != null) locale = "." + infoDriver.Locale.ToLowerInvariant();
            var resource = "Vizregress.BDD.Examples.Images." + split[0] + "." + infoDriver.Browser + "." + split[1] + locale + ".png";                

            var r =  GetStreamForResource(resource);
            if (r == null) throw new ApplicationException("No expected image: " + resource);
            return r;
                    
        }

        public virtual Stream GetStreamForResource(string resource) 
        {
            var streamExpected = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            return streamExpected;
            
        }
    }
}
