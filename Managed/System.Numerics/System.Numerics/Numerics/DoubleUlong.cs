using System;
using System.Runtime.InteropServices;

namespace System.Numerics
{
	// Token: 0x02000018 RID: 24
	[StructLayout(LayoutKind.Explicit)]
	internal struct DoubleUlong
	{
		// Token: 0x04000094 RID: 148
		[FieldOffset(0)]
		public double dbl;

		// Token: 0x04000095 RID: 149
		[FieldOffset(0)]
		public ulong uu;
	}
}
