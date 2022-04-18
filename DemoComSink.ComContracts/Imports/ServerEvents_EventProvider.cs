using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace DemoComSink.ComContracts.Sink
{
    public class ServerEvents_EventProvider : ServerEvents_Event
    {
        private IConnectionPointContainer m_ConnectionPointContainer;
        private ArrayList m_aEventSinkHelpers;
        private IConnectionPoint m_ConnectionPoint;

        private void Init()
        {
            IConnectionPoint ppCP = null;
            Guid riid = new Guid(ContractGuids.ServerEventsId);
            m_ConnectionPointContainer.FindConnectionPoint(ref riid, out ppCP);
            m_ConnectionPoint = ppCP;
            m_aEventSinkHelpers = new ArrayList();
        }
        public ServerEvents_EventProvider(object p)
        {
            m_ConnectionPointContainer = (IConnectionPointContainer)p;
        }

        public event ServerEvents_ClickEventHandler Click
        {
            add
            {

                bool lockTaken = default(bool);
                try
                {
                    Monitor.Enter(this, ref lockTaken);
                    if (m_ConnectionPoint == null)
                    {
                        Init();
                    }
                    ServerEvents_SinkHelper serverEvents_SinkHelper = new ServerEvents_SinkHelper();
                    int pdwCookie = 0;
                    m_ConnectionPoint.Advise(serverEvents_SinkHelper, out pdwCookie);
                    serverEvents_SinkHelper.m_dwCookie = pdwCookie;
                    serverEvents_SinkHelper.m_ClickDelegate = value;
                    m_aEventSinkHelpers.Add(serverEvents_SinkHelper);
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(this);
                    }
                }
            }
            remove
            {
                bool lockTaken = default(bool);
                try
                {
                    Monitor.Enter(this, ref lockTaken);
                    if (m_aEventSinkHelpers == null)
                    {
                        return;
                    }

                    int count = m_aEventSinkHelpers.Count;
                    int num = 0;
                    if (0 >= count)
                    {
                        return;
                    }

                    do
                    {
                        ServerEvents_SinkHelper dWebBrowserEvents2_SinkHelper = (ServerEvents_SinkHelper)m_aEventSinkHelpers[num];
                        if (dWebBrowserEvents2_SinkHelper.m_ClickDelegate != null && ((dWebBrowserEvents2_SinkHelper.m_ClickDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                        {
                            m_aEventSinkHelpers.RemoveAt(num);
                            m_ConnectionPoint.Unadvise(dWebBrowserEvents2_SinkHelper.m_dwCookie);
                            if (count <= 1)
                            {
                                Marshal.ReleaseComObject(m_ConnectionPoint);
                                m_ConnectionPoint = null;
                                m_aEventSinkHelpers = null;
                            }

                            break;
                        }

                        num++;
                    }
                    while (num < count);
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(this);
                    }
                }
            }
        }

    }
}
