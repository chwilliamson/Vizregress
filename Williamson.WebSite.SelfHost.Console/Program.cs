using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williamson.WebSite.SelfHost.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Williamson.WebSite.SelfHost.Application();
            app.Start();
            Console.WriteLine("Started: Press Enter to stop");

            Console.ReadLine();

            app.Stop();
        }
    }
}
