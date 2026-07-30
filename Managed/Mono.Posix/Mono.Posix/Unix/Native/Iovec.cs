using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005A RID: 90
	[Map("struct iovec")]
	public struct Iovec
	{
		// Token: 0x0400042D RID: 1069
		public IntPtr iov_base;

		// Token: 0x0400042E RID: 1070
		[CLSCompliant(false)]
		public ulong iov_len;
	}
}
