using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnded" /> event.</summary>
	// Token: 0x020000D0 RID: 208
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionEndedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndedEventArgs" /> class.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values indicating how the session ended. </param>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000EBB1 File Offset: 0x0000CDB1
		public SessionEndedEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets an identifier that indicates how the session ended.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that indicates how the session ended.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		// Token: 0x04000B8D RID: 2957
		private SessionEndReasons myreason;
	}
}
