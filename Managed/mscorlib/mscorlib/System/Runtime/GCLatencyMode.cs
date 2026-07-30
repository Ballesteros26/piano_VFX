using System;

namespace System.Runtime
{
	/// <summary>Adjusts the time that the garbage collector intrudes in your application.   </summary>
	// Token: 0x020006B6 RID: 1718
	[Serializable]
	public enum GCLatencyMode
	{
		/// <summary>Disables garbage collection concurrency and reclaims objects in a batch call. This is the most intrusive mode.</summary>
		// Token: 0x04002676 RID: 9846
		Batch,
		/// <summary>Enables garbage collection concurrency and reclaims objects while the application is running. This is the default mode for garbage collection on a workstation and is less intrusive than <see cref="F:System.Runtime.GCLatencyMode.Batch" />. It balances responsiveness with throughput.</summary>
		// Token: 0x04002677 RID: 9847
		Interactive,
		/// <summary>Enables garbage collection that is more conservative in reclaiming objects. Full collections occur only if the system is under memory pressure, whereas generation 0 and generation 1 collections might occur more frequently</summary>
		// Token: 0x04002678 RID: 9848
		LowLatency,
		/// <summary>Enables garbage collection that tries to minimize latency over an extended period. The collector tries to perform only generation 0, generation 1, and concurrent generation 2 collections. Full blocking collections may still occur if the system is under memory pressure.</summary>
		// Token: 0x04002679 RID: 9849
		SustainedLowLatency,
		// Token: 0x0400267A RID: 9850
		NoGCRegion
	}
}
