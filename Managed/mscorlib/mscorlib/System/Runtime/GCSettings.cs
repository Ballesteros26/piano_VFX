using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime
{
	/// <summary>Specifies the garbage collection settings for the current process. </summary>
	// Token: 0x020006B7 RID: 1719
	public static class GCSettings
	{
		/// <summary>Gets a value that indicates whether server garbage collection is enabled.</summary>
		/// <returns>true if server garbage collection is enabled; otherwise, false.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("Always returns false")]
		public static bool IsServerGC
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the current latency mode for garbage collection.</summary>
		/// <returns>One of the enumeration values that specifies the latency mode. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="T:System.Runtime.GCLatencyMode" /> is set to an invalid value.</exception>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x00003B29 File Offset: 0x00001D29
		// (set) Token: 0x06004965 RID: 18789 RVA: 0x00002194 File Offset: 0x00000394
		[MonoTODO("Always returns GCLatencyMode.Interactive and ignores set")]
		public static GCLatencyMode LatencyMode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return GCLatencyMode.Interactive;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether a full blocking garbage collection compacts the large object heap (LOH). </summary>
		/// <returns>One of the enumeration values that indicates whether a full blocking garbage collection compacts the LOH. </returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x001079CA File Offset: 0x00105BCA
		// (set) Token: 0x06004967 RID: 18791 RVA: 0x001079D1 File Offset: 0x00105BD1
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set;
		}
	}
}
