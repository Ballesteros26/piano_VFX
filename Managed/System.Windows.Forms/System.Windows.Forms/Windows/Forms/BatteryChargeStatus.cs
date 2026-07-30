using System;

namespace System.Windows.Forms
{
	/// <summary>Defines identifiers that indicate the current battery charge level or charging state information.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000057 RID: 87
	[Flags]
	public enum BatteryChargeStatus
	{
		/// <summary>Indicates a high level of battery charge.</summary>
		// Token: 0x04000607 RID: 1543
		High = 1,
		/// <summary>Indicates a low level of battery charge.</summary>
		// Token: 0x04000608 RID: 1544
		Low = 2,
		/// <summary>Indicates a critically low level of battery charge.</summary>
		// Token: 0x04000609 RID: 1545
		Critical = 4,
		/// <summary>Indicates a battery is charging.</summary>
		// Token: 0x0400060A RID: 1546
		Charging = 8,
		/// <summary>Indicates that no battery is present.</summary>
		// Token: 0x0400060B RID: 1547
		NoSystemBattery = 128,
		/// <summary>Indicates an unknown battery condition.</summary>
		// Token: 0x0400060C RID: 1548
		Unknown = 255
	}
}
