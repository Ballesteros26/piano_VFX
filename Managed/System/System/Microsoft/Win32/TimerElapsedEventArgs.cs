using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.TimerElapsed" /> event.</summary>
	// Token: 0x020000D8 RID: 216
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class TimerElapsedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.TimerElapsedEventArgs" /> class.</summary>
		/// <param name="timerId">The ID number for the timer. </param>
		// Token: 0x060004DD RID: 1245 RVA: 0x0000ED4B File Offset: 0x0000CF4B
		public TimerElapsedEventArgs(IntPtr timerId)
		{
			this.mytimerId = timerId;
		}

		/// <summary>Gets the ID number for the timer.</summary>
		/// <returns>The ID number for the timer.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000ED5A File Offset: 0x0000CF5A
		public IntPtr TimerId
		{
			get
			{
				return this.mytimerId;
			}
		}

		// Token: 0x04000B9D RID: 2973
		private IntPtr mytimerId;
	}
}
