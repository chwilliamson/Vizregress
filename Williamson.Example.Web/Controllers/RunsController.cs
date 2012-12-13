using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SisoDb;
using SisoDb.SqlCe4;
using Williamson.Example.Web.Models;

namespace Williamson.Example.Web.Controllers
{
    /// <summary>
    /// A runs controller used for viewing runs
    /// </summary>
    public class RunsController : ApiController
    {
        ISisoDatabase db = "Data source=|DataDirectory|sisodb.sdf;".CreateSqlCe4Db();
       
        public Run[] Get()
        {
            return db.UseOnceTo().Query<Run>().Where(i=>!i.Deleted).ToArray();
        }

        [HttpPost]
        public int Start(Run input)
        {
            var run = new Run();
            run.RunTime = DateTime.UtcNow;
            run.Deleted = false;
            run.Name = input.Name;
            db.UseOnceTo().Insert<Run>(run);
            return run.Id;           
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var run = db.UseOnceTo().GetById<Run>(id);
            run.Deleted = true;
            db.UseOnceTo().Update(run);
        }        
    }
}
