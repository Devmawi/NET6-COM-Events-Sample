using DemoComSink.ComContracts.Servers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoComSink.ComContracts.Clients
{
    [ClassInterface(ClassInterfaceType.None)]
    public class ServerEvents_SinkHelper : ServerEvents // Interface implementation is necessary for  m_ConnectionPointContainer.FindConnectionPoint(ref riid, out ppCP);
    {
        public ServerEvents_ClickEventHandler m_ClickDelegate;
        public int m_dwCookie;

        public void Click(string message)
        {
            if (m_ClickDelegate != null)
            {
                m_ClickDelegate(message);
            };
        }
    }
}
