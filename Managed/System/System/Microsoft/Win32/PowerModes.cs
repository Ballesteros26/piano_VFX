using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers for power mode events reported by the operating system.</summary>
	// Token: 0x020000CE RID: 206
	public enum PowerModes
	{
		/// <summary>The operating system is about to resume from a suspended state.</summary>
		// Token: 0x04000B87 RID: 2951
		Resume = 1,
		/// <summary>A power mode status notification event has been raised by the operating system. This might indicate a weak or charging battery, a transition between AC power and battery, or another change in the status of the system power supply.</summary>
		// Token: 0x04000B88 RID: 2952
		StatusChange,
		/// <summary>The operating system is about to be suspended.</summary>
		// Token: 0x04000B89 RID: 2953
		Suspend
	}
}
