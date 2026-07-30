using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Manages an application domain protocol handler</summary>
	// Token: 0x02000767 RID: 1895
	public interface IAdphManager
	{
		/// <summary>Starts an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelCallback">The protocol listener channel callback.</param>
		// Token: 0x06004D32 RID: 19762
		void StartAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		/// <summary>Stops an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="immediate">The protocol listener channel callback.</param>
		// Token: 0x06004D33 RID: 19763
		void StopAppDomainProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);

		/// <summary>Stops an application domain protocol listener channel.</summary>
		/// <param name="appId">The application ID</param>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelId">The protocol listener channel callback.</param>
		/// <param name="immediate">Whether to stop the protocol listener channel immediately</param>
		// Token: 0x06004D34 RID: 19764
		void StopAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);
	}
}
