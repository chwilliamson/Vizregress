using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.Example.Web.Models
{
    public class RunFile
    {
        public int Id
        {
            get;
            set;
        }

        public byte[] Content
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

    }
}
