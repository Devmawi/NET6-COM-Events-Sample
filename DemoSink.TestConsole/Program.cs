using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSink.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.Click += Server_Click;
            server.Message = "Ulf";
            server.Init();
            server.RaiseClickEvent();
            
            
            //SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();
        }

        private static void Server_Click(string message)
        {
            Console.WriteLine("Clicked ...");
        }
    }
}
