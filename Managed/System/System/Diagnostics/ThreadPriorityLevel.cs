using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the priority level of a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000218 RID: 536
	public enum ThreadPriorityLevel
	{
		/// <summary>Specifies one step above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E2 RID: 4578
		AboveNormal = 1,
		/// <summary>Specifies one step below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E3 RID: 4579
		BelowNormal = -1,
		/// <summary>Specifies highest priority. This is two steps above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E4 RID: 4580
		Highest = 2,
		/// <summary>Specifies idle priority. This is the lowest possible priority value of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E5 RID: 4581
		Idle = -15,
		/// <summary>Specifies lowest priority. This is two steps below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E6 RID: 4582
		Lowest = -2,
		/// <summary>Specifies normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E7 RID: 4583
		Normal = 0,
		/// <summary>Specifies time-critical priority. This is the highest priority of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040011E8 RID: 4584
		TimeCritical = 15
	}
}
