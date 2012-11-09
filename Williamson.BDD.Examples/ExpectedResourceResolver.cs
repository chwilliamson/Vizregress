using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.BDD.Examples
{
    /// <summary>
    /// Responsible for resolving resources
    /// </summary>
    public class ExpectedResourceResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name of the resource</param>
        /// <param name="infoDriver"></param>
        /// <returns></returns>
        public Stream Resolve(string name, IInformationalWebDriver infoDriver)
        {
            //load base reference image
            var split = name.Split('.');
            //e.g. Williamson.BDD.Images.GitHub.FireFox.Home.png
            var locale = "";
            if (infoDriver.Locale != null) locale = "." + infoDriver.Locale.ToLowerInvariant();
            var resource = "Williamson.BDD.Examples.Images." + split[0] + "." + infoDriver.Browser + "." + split[1] + locale + ".png";                

            var r =  this.GetStreamForResource(resource);
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
