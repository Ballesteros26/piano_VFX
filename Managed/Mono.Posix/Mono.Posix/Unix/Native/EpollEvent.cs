using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200005E RID: 94
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 12)]
	public struct EpollEvent
	{
		// Token: 0x04000444 RID: 1092
		[FieldOffset(0)]
		public EpollEvents events;

		// Token: 0x04000445 RID: 1093
		[FieldOffset(4)]
		public int fd;

		// Token: 0x04000446 RID: 1094
		[FieldOffset(4)]
		public IntPtr ptr;

		// Token: 0x04000447 RID: 1095
		[FieldOffset(4)]
		public uint u32;

		// Token: 0x04000448 RID: 1096
		[FieldOffset(4)]
		public ulong u64;
	}
}
