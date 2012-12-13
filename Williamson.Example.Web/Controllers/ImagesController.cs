using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SisoDb;
using SisoDb.SqlCe4;
using Williamson.Example.Web.Models;

namespace Williamson.Example.Web.Controllers
{
    public class ImagesController : ApiController
    {
        ISisoDatabase db = "Data source=|DataDirectory|sisodb.sdf;".CreateSqlCe4Db();
       
        /// <summary>
        /// Uploads an image file for storage and assigns an id
        /// </summary>
        /// <param name="name"></param>
        [HttpPost]
        public int UploadImage()
        {
            using (var bodyparts = Request.Content.ReadAsStreamAsync().Result)
            using (var ms = new MemoryStream())
            {
                bodyparts.CopyTo(ms);
                var f = new RunFile { Content = ms.ToArray(), Name = "test.png" };
                db.UseOnceTo().Insert(f);
                return f.Id;
            }
        }
    }
}
