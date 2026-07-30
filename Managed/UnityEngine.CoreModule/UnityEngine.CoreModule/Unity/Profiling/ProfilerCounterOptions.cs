using System;

namespace Unity.Profiling
{
	// Token: 0x0200002E RID: 46
	[Flags]
	public enum ProfilerCounterOptions : ushort
	{
		// Token: 0x040000A2 RID: 162
		None = 0,
		// Token: 0x040000A3 RID: 163
		FlushOnEndOfFrame = 2,
		// Token: 0x040000A4 RID: 164
		ResetToZeroOnFlush = 4
	}
}
