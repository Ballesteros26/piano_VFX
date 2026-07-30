using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnding" /> event.</summary>
	// Token: 0x020000D2 RID: 210
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionEndingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndingEventArgs" /> class using the specified value indicating the type of session close event that is occurring.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> that specifies how the session ends. </param>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		public SessionEndingEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets the reason the session is ending.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that specifies how the session is ending.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000EBD7 File Offset: 0x0000CDD7
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		/// <summary>Gets or sets a value indicating whether to cancel the user request to end the session.</summary>
		/// <returns>true to cancel the user request to end the session; otherwise, false.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000EBDF File Offset: 0x0000CDDF
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		public bool Cancel
		{
			get
			{
				return this.mycancel;
			}
			set
			{
				this.mycancel = value;
			}
		}

		// Token: 0x04000B8E RID: 2958
		private SessionEndReasons myreason;

		// Token: 0x04000B8F RID: 2959
		private bool mycancel;
	}
}
