using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum MarkerFlags : ushort
	{
		// Token: 0x040000A6 RID: 166
		Default = 0,
		// Token: 0x040000A7 RID: 167
		Script = 2,
		// Token: 0x040000A8 RID: 168
		ScriptInvoke = 32,
		// Token: 0x040000A9 RID: 169
		ScriptDeepProfiler = 64,
		// Token: 0x040000AA RID: 170
		AvailabilityEditor = 4,
		// Token: 0x040000AB RID: 171
		Warning = 16,
		// Token: 0x040000AC RID: 172
		Counter = 128
	}
}
