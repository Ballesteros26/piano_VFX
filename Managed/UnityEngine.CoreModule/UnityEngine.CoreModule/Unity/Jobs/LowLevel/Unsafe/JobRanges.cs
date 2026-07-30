using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000044 RID: 68
	public struct JobRanges
	{
		// Token: 0x040000D4 RID: 212
		internal int BatchSize;

		// Token: 0x040000D5 RID: 213
		internal int NumJobs;

		// Token: 0x040000D6 RID: 214
		public int TotalIterationCount;

		// Token: 0x040000D7 RID: 215
		internal int NumPhases;

		// Token: 0x040000D8 RID: 216
		internal IntPtr StartEndIndex;

		// Token: 0x040000D9 RID: 217
		internal IntPtr PhaseData;
	}
}
