using DemoComSink.ComContracts;
using DemoComSink.ComContracts.Clients;
using DemoComSink.ComContracts.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/* IMPORTANT: Build with 32-bit */
namespace DemoComSink
{
    [ComVisible(true)]
    [Guid(ContractGuids.ServerClassId)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(ServerEvents))]
    public class Server : IServer, ServerEvents_Event
    {
        public string Message { get; set; } = ".NET 6 Server";

        public void Init()
        {
            Console.WriteLine($"Hello from Init ... {Message}");
        }

        public void RaiseClickEvent()
        {
            Console.WriteLine($"Raise Click {_click?.GetInvocationList().Length}");
            _click?.Invoke("Hello from C#!");
        }

        public void AddClick2(ServerEvents_ClickEventHandler click)
        {
            Console.WriteLine("Added ...");
        }

        private event ServerEvents_ClickEventHandler _click;
        public event ServerEvents_ClickEventHandler Click
        {
            add { Console.WriteLine("_click += value"); _click += value; }
            remove { Console.WriteLine("_click -= value"); _click -= value; }
        }
    }
}
