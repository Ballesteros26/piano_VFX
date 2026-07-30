using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.PowerModeChanged" /> event.</summary>
	// Token: 0x020000CC RID: 204
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class PowerModeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.PowerModeChangedEventArgs" /> class using the specified power mode event type.</summary>
		/// <param name="mode">One of the <see cref="T:Microsoft.Win32.PowerModes" /> values that represents the type of power mode event. </param>
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000EB9A File Offset: 0x0000CD9A
		public PowerModeChangedEventArgs(PowerModes mode)
		{
			this.mymode = mode;
		}

		/// <summary>Gets an identifier that indicates the type of the power mode event that has occurred.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.PowerModes" /> values.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000EBA9 File Offset: 0x0000CDA9
		public PowerModes Mode
		{
			get
			{
				return this.mymode;
			}
		}

		// Token: 0x04000B85 RID: 2949
		private PowerModes mymode;
	}
}
