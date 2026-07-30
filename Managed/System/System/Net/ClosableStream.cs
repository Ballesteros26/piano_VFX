using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004C7 RID: 1223
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x06002447 RID: 9287 RVA: 0x0008DA9B File Offset: 0x0008BC9B
		internal ClosableStream(Stream stream, EventHandler onClose)
			: base(stream)
		{
			this.onClose = onClose;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0008DAAB File Offset: 0x0008BCAB
		public override void Close()
		{
			if (Interlocked.Increment(ref this.closed) == 1 && this.onClose != null)
			{
				this.onClose(this, new EventArgs());
			}
		}

		// Token: 0x04002021 RID: 8225
		private EventHandler onClose;

		// Token: 0x04002022 RID: 8226
		private int closed;
	}
}
