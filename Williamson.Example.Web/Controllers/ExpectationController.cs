using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Williamson.Example.Web.Controllers
{
    /// <summary>
    /// Storing and comparing images
    /// </summary>
    public class ExpectationController : ApiController
    {
        public string Get()
        {
            return "Hello there!";
        }

        public bool Post()
        {
            return false;
        }
    }
}
