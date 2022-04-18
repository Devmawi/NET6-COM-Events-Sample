using DemoComSink.ComContracts.Servers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoComSink.ComContracts.Clients
{
    public delegate void ServerEvents_ClickEventHandler(string message);

    [ComEventInterface(typeof(ServerEvents), typeof(ServerEvents_EventProvider))]
    [ComVisible(false)]
    public interface ServerEvents_Event
    {
        event ServerEvents_ClickEventHandler Click;
    }
}
