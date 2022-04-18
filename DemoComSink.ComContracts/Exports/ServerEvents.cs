using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoComSink.ComContracts.Source
{
    [ComVisible(true)]
    [Guid(ContractGuids.ServerEventsId)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ServerEvents
    {
        void Click(string message);
    }
}
