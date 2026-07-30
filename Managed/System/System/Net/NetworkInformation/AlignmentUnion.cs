using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000679 RID: 1657
	[StructLayout(LayoutKind.Explicit)]
	internal struct AlignmentUnion
	{
		// Token: 0x0400299A RID: 10650
		[FieldOffset(0)]
		public ulong Alignment;

		// Token: 0x0400299B RID: 10651
		[FieldOffset(0)]
		public int Length;

		// Token: 0x0400299C RID: 10652
		[FieldOffset(4)]
		public int IfIndex;
	}
}
