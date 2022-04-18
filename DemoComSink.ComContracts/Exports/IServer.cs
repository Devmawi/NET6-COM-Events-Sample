using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoComSink.ComContracts.Source
{
    [ComVisible(true)]
    [Guid(ContractGuids.ServerId)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IServer
    {
        void Init();
        string Message { get; set; }
        void RaiseClickEvent();
    }
}
