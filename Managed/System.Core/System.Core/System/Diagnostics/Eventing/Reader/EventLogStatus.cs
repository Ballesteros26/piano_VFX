using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the status code or error code for a specific event log. This status can be used to determine if the event log is available for an operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039A RID: 922
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogStatus
	{
		// Token: 0x06001B3E RID: 6974 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventLogStatus()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the event log for which the status code is obtained.</summary>
		/// <returns>Returns a string that contains the name of the event log for which the status code is obtained.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000560B4 File Offset: 0x000542B4
		public string LogName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the status code or error code for the event log. This status or error is the result of a read or subscription operation on the event log.</summary>
		/// <returns>Returns an integer value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x00056888 File Offset: 0x00054A88
		public int StatusCode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
