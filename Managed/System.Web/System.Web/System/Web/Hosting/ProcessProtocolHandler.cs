using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides support for protocol handlers.</summary>
	// Token: 0x02000771 RID: 1905
	public abstract class ProcessProtocolHandler : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ProcessProtocolHandler" /> class.</summary>
		// Token: 0x06004D58 RID: 19800 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ProcessProtocolHandler()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Starts a protocol listener channel.</summary>
		/// <param name="listenerChannelCallback">The callback for the listener channel.</param>
		/// <param name="AdphManager">The application domain handler manager that is associated with the listener channel.</param>
		// Token: 0x06004D59 RID: 19801
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback, IAdphManager AdphManager);

		/// <summary>Stops the specified process protocol handler.</summary>
		/// <param name="listenerChannelId">The callback for the listener channel.</param>
		/// <param name="immediate">true to stop the protocol immediately; otherwise, false.</param>
		// Token: 0x06004D5A RID: 19802
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		/// <summary>Stops a process protocol handler.</summary>
		/// <param name="immediate">true to stop the protocol immediately; otherwise, false.</param>
		// Token: 0x06004D5B RID: 19803
		public abstract void StopProtocol(bool immediate);
	}
}
