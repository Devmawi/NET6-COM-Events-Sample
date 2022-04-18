using DemoComSink.ComContracts.Servers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoComSink.ComContracts.Clients
{
    [ComImport]
    [Guid(ContractGuids.ServerId)]
    [CoClass(typeof(ServerClass))]
    public interface Server : IServer, ServerEvents_Event
    {

    }

    [ComImport]
    [Guid(ContractGuids.ServerClassId)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(ServerEvents))]
    public class ServerClass
    {
    }
}
