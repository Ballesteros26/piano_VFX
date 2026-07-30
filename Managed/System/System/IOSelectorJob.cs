using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x0200011B RID: 283
	[StructLayout(LayoutKind.Sequential)]
	internal class IOSelectorJob : IThreadPoolWorkItem
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x000265CC File Offset: 0x000247CC
		public IOSelectorJob(IOOperation operation, IOAsyncCallback callback, IOAsyncResult state)
		{
			this.operation = operation;
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000265E9 File Offset: 0x000247E9
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.callback(this.state);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000027E8 File Offset: 0x000009E8
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000265FC File Offset: 0x000247FC
		public void MarkDisposed()
		{
			this.state.CompleteDisposed();
		}

		// Token: 0x04000D62 RID: 3426
		private IOOperation operation;

		// Token: 0x04000D63 RID: 3427
		private IOAsyncCallback callback;

		// Token: 0x04000D64 RID: 3428
		private IOAsyncResult state;
	}
}
