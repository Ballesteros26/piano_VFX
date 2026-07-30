using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Notifies a waiting thread that an event has occurred. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200046E RID: 1134
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class AutoResetEvent : EventWaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AutoResetEvent" /> class with a Boolean value indicating whether to set the initial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to non-signaled. </param>
		// Token: 0x060035C4 RID: 13764 RVA: 0x000C6D1F File Offset: 0x000C4F1F
		public AutoResetEvent(bool initialState)
			: base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
