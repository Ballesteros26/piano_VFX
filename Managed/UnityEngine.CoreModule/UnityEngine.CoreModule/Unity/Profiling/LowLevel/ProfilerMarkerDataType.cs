using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x02000030 RID: 48
	public enum ProfilerMarkerDataType : byte
	{
		// Token: 0x040000AE RID: 174
		Int32 = 2,
		// Token: 0x040000AF RID: 175
		UInt32,
		// Token: 0x040000B0 RID: 176
		Int64,
		// Token: 0x040000B1 RID: 177
		UInt64,
		// Token: 0x040000B2 RID: 178
		Float,
		// Token: 0x040000B3 RID: 179
		Double,
		// Token: 0x040000B4 RID: 180
		String16 = 9,
		// Token: 0x040000B5 RID: 181
		Blob8 = 11
	}
}
