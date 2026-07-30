using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the lifetime of a performance counter instance.</summary>
	// Token: 0x0200020B RID: 523
	public enum PerformanceCounterInstanceLifetime
	{
		/// <summary>Remove the performance counter instance when no counters are using the process category.</summary>
		// Token: 0x0400119B RID: 4507
		Global,
		/// <summary>Remove the performance counter instance when the process is closed.</summary>
		// Token: 0x0400119C RID: 4508
		Process
	}
}
