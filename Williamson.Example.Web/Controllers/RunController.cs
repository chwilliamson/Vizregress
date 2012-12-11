using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Williamson.Example.Web.Model;

namespace Williamson.Example.Web.Controllers
{
    /// <summary>
    /// A reporting controller
    /// </summary>
    public class RunController : ApiController
    {
        public Run[] Get()
        {
            return new List<Run> { 
                new Run { Id=Guid.Empty,Name="Run One", RunTime=DateTime.UtcNow}
            }.ToArray();
        }
    }
}
