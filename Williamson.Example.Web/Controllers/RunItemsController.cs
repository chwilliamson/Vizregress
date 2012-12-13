using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SisoDb;
using SisoDb.SqlCe4;
using Williamson.Example.Web.Models;

namespace Williamson.Example.Web.Controllers
{
    public class RunItemsController : ApiController
    {
        ISisoDatabase db = "Data source=|DataDirectory|sisodb.sdf;".CreateSqlCe4Db();

        /// <summary>
        /// Gets all run items for a specified run
        /// </summary>
        /// <param name="runId"></param>
        /// <returns></returns>
        public RunItem[] GetRunItemsByRun(int runId)
        {
            return new RunItem[0];
        }

        public int Create(RunItem runItem)
        {
            return 0;
        }
    }

       
}
