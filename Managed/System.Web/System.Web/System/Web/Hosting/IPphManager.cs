using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides stop and start control of listener channels.</summary>
	// Token: 0x0200076A RID: 1898
	[Guid("1cc9099d-0a8d-41cb-87d6-845e4f8c4e91")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IPphManager
	{
		/// <summary>Starts the specified process protocol listener channel.</summary>
		/// <param name="protocolId">The protocol ID.</param>
		/// <param name="listenerChannelCallback">The <see cref="T:System.Web.Hosting.IListenerChannelCallback" /> interface.</param>
		// Token: 0x06004D39 RID: 19769
		void StartProcessProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		/// <summary>Stops all protocol listener channels.</summary>
		/// <param name="protocolId">The protocol ID of the listener channel to stop.</param>
		/// <param name="immediate">true to notify the process protocol manager to stop all listener channels synchronously; false to stop all listener channels asynchronously.</param>
		// Token: 0x06004D3A RID: 19770
		void StopProcessProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);

		/// <summary>Stops the specified process protocol listener channel.</summary>
		/// <param name="protocolId">The protocol ID for the listener channel.</param>
		/// <param name="listenerChannelId">The listener channel ID.</param>
		/// <param name="immediate">true to notify the process protocol manager to stop all listener channels synchronously; false to stop all listener channels asynchronously.</param>
		// Token: 0x06004D3B RID: 19771
		void StopProcessProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);
	}
}
