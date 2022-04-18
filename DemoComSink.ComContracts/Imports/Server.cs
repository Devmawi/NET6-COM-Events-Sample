using DemoComSink.ComContracts.Source;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoComSink.ComContracts.Sink
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
