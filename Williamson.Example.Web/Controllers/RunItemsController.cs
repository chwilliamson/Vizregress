using System;
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

        [HttpPost]
        public long Create(RunItem runItem)
        {
            //check image exists
            if (db.UseOnceTo().GetById<RunFile>(runItem.ImageId)==null) throw new ApplicationException("No record found");
            
            //check run id
            if (db.UseOnceTo().GetById<Run>(runItem.RunId) == null) throw new ApplicationException("No record found");

            db.UseOnceTo().Insert(runItem);
            return runItem.Id;
        }
    }       
}
