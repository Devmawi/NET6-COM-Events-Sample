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
    [Guid(ContractGuids.ServerId)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IServer
    {
        void Init();
        string Message { get; set; }

        public void RaiseClickEvent();
    }

    [ComVisible(true)]
    [Guid(ContractGuids.ServerEventsId)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IServerEvents
    {
        void Click(string message);
    }

    [ComVisible(true)]
    [Guid("2e833078-8cf8-4478-a0c0-349b0008656f")]
    public delegate void ClickEventHandler(string message);

    [ComVisible(true)]
    [Guid(ContractGuids.ServerClassId)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComSourceInterfaces(typeof(IServerEvents))]
    public class Server : IServer
    {
        public string Message { get; set; }

        public void Init()
        {
            Console.WriteLine($"Hello from Init ... {Message}");
        }

        public void RaiseClickEvent()
        {
            Console.WriteLine($"Raise Click {_click?.GetInvocationList().Length}");
            _click?.Invoke("Hello from C#!");
        }

        public void AddClick2(ClickEventHandler click)
        {
            Console.WriteLine("Added ...");
        }

        private event ClickEventHandler _click;
        public event ClickEventHandler Click
        {
            add { Console.WriteLine("_click += value"); _click += value; }
            remove { Console.WriteLine("_click += value"); _click += value; }
        }
    }
}
