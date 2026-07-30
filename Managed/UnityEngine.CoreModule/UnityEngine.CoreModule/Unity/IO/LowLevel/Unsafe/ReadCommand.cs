using System;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200004D RID: 77
	public struct ReadCommand
	{
		// Token: 0x040000FA RID: 250
		public unsafe void* Buffer;

		// Token: 0x040000FB RID: 251
		public long Offset;

		// Token: 0x040000FC RID: 252
		public long Size;
	}
}
