using System;
using System.Runtime.InteropServices;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000031 RID: 49
	[StructLayout(2, Size = 16)]
	public struct ProfilerMarkerData
	{
		// Token: 0x040000B6 RID: 182
		[FieldOffset(0)]
		public byte Type;

		// Token: 0x040000B7 RID: 183
		[FieldOffset(1)]
		private readonly byte reserved0;

		// Token: 0x040000B8 RID: 184
		[FieldOffset(2)]
		private readonly ushort reserved1;

		// Token: 0x040000B9 RID: 185
		[FieldOffset(4)]
		public uint Size;

		// Token: 0x040000BA RID: 186
		[FieldOffset(8)]
		public unsafe void* Ptr;
	}
}
