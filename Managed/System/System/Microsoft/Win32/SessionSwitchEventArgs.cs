using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionSwitch" /> event.</summary>
	// Token: 0x020000D4 RID: 212
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionSwitchEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionSwitchEventArgs" /> class using the specified session change event type identifer.</summary>
		/// <param name="reason">A <see cref="T:Microsoft.Win32.SessionSwitchReason" /> that indicates the type of session change event. </param>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public SessionSwitchEventArgs(SessionSwitchReason reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets an identifier that indicates the type of session change event.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SessionSwitchReason" /> indicating the type of the session change event.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		public SessionSwitchReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000B90 RID: 2960
		private SessionSwitchReason reason;
	}
}
