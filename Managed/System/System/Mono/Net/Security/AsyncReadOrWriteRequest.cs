using System;

namespace Mono.Net.Security
{
	// Token: 0x0200006B RID: 107
	internal abstract class AsyncReadOrWriteRequest : AsyncProtocolRequest
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000609E File Offset: 0x0000429E
		protected BufferOffsetSize UserBuffer { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000060A6 File Offset: 0x000042A6
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000060AE File Offset: 0x000042AE
		protected int CurrentSize { get; set; }

		// Token: 0x060001F3 RID: 499 RVA: 0x000060B7 File Offset: 0x000042B7
		public AsyncReadOrWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync)
		{
			this.UserBuffer = new BufferOffsetSize(buffer, offset, size);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000060D1 File Offset: 0x000042D1
		public override string ToString()
		{
			return string.Format("[{0}: {1}]", base.Name, this.UserBuffer);
		}
	}
}
