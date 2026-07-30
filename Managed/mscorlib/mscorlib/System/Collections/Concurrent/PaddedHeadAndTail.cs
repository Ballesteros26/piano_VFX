using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.Concurrent
{
	// Token: 0x02000A01 RID: 2561
	[DebuggerDisplay("Head = {Head}, Tail = {Tail}")]
	[StructLayout(LayoutKind.Explicit, Size = 384)]
	internal struct PaddedHeadAndTail
	{
		// Token: 0x04003000 RID: 12288
		[FieldOffset(128)]
		public int Head;

		// Token: 0x04003001 RID: 12289
		[FieldOffset(256)]
		public int Tail;
	}
}
