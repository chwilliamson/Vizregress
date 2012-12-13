using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SisoDb;
using SisoDb.SqlCe4;
using Williamson.Example.Web.Model;

namespace Williamson.Example.Web.Controllers
{
    /// <summary>
    /// A reporting controller
    /// </summary>
    public class RunController : ApiController
    {
        ISisoDatabase db = "Data source=|DataDirectory|sisodb.sdf;".CreateSqlCe4Db();
       
        public Run[] Get()
        {
            return db.UseOnceTo().Query<Run>().ToArray();
        }

        [HttpPost]
        public Guid Start(Run run)
        {
            run.Id = Guid.NewGuid();
            run.RunTime = DateTime.UtcNow;
            db.UseOnceTo().Insert<Run>(run);
            return run.Id;           
        }

        [HttpPost]
        public void Delete(Guid runId)
        {            
        }

        public void GetAllRunItems(Guid runId) {
        }
    }
}
