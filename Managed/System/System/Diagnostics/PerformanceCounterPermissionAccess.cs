using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.PerformanceCounter" /> permission classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020E RID: 526
	[Flags]
	public enum PerformanceCounterPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> has no permissions.</summary>
		// Token: 0x0400119F RID: 4511
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read categories.</summary>
		// Token: 0x040011A0 RID: 4512
		[Obsolete]
		Browse = 1,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read categories.</summary>
		// Token: 0x040011A1 RID: 4513
		Read = 1,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can write categories.</summary>
		// Token: 0x040011A2 RID: 4514
		Write = 2,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read and write categories.</summary>
		// Token: 0x040011A3 RID: 4515
		[Obsolete]
		Instrument = 3,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read, write, and create categories.</summary>
		// Token: 0x040011A4 RID: 4516
		Administer = 7
	}
}
