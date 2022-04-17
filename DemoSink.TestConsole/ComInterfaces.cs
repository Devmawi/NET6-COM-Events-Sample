using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/* IMPORTANT: Build with 32-bit */
namespace DemoSink.TestConsole
{
    [ComImport]
    [Guid(ContractGuids.ServerId)]
    [CoClass(typeof(ServerClass))]
    public interface Server : IServer, IServerEvents_Event
    {

    }

    [ComImport]
    [Guid(ContractGuids.ServerId)]
    public interface IServer
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        void Init();
        string Message { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
        [MethodImpl(MethodImplOptions.InternalCall)]
        void RaiseClickEvent();
    }

    [ComVisible(true)]
    [Guid(ContractGuids.ServerEventsId)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IServerEvents
    {
        [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
        void Click([In][MarshalAs(UnmanagedType.BStr)] string message);
    }

    [ComImport]
    [Guid(ContractGuids.ServerClassId)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces("DemoComSink.IServerEvents")]
    public class ServerClass: IServer
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public virtual extern void RaiseClickEvent();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public virtual extern void Init();  
        public virtual extern string Message { [MethodImpl(MethodImplOptions.InternalCall)] get ; [MethodImpl(MethodImplOptions.InternalCall)] set ; }
        
        //public extern event IServerEvents_ClickEventHandler Click;
    }

    [ComVisible(false)]
    public delegate void IServerEvents_ClickEventHandler([In][MarshalAs(UnmanagedType.BStr)]string message);

    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FHidden)]
    public class IServerEvents_SinkHelper: IServerEvents
    {
        public IServerEvents_ClickEventHandler m_ClickDelegate;
        internal int m_dwCookie;

        public void Click([In, MarshalAs(UnmanagedType.BStr)] string message)
        {
            if (m_ClickDelegate != null)
            {
                m_ClickDelegate(message);
            };
        }
    }

    public class IServerEvents_EventProvider : IServerEvents_Event
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
        public IServerEvents_EventProvider(object p)
        {
            m_ConnectionPointContainer = (IConnectionPointContainer)p;       
        }

        public event IServerEvents_ClickEventHandler Click
        {
            //[MethodImpl(MethodImplOptions.InternalCall)]
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
                    IServerEvents_SinkHelper serverEvents_SinkHelper = new IServerEvents_SinkHelper();
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
           // [MethodImpl(MethodImplOptions.InternalCall)]
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
                        IServerEvents_SinkHelper dWebBrowserEvents2_SinkHelper = (IServerEvents_SinkHelper)m_aEventSinkHelpers[num];
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

    [ComEventInterface(typeof(IServerEvents), typeof(IServerEvents_EventProvider))]
    [ComVisible(false)]
    public interface IServerEvents_Event
    {
        event IServerEvents_ClickEventHandler Click;
    }


}
