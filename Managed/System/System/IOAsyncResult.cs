using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x0200011A RID: 282
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class IOAsyncResult : IAsyncResult
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x000020EB File Offset: 0x000002EB
		protected IOAsyncResult()
		{
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000264AB File Offset: 0x000246AB
		protected void Init(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
			this.completed = false;
			this.completed_synchronously = false;
			if (this.wait_handle != null)
			{
				this.wait_handle.Reset();
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000264DD File Offset: 0x000246DD
		protected IOAsyncResult(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000264F3 File Offset: 0x000246F3
		public AsyncCallback AsyncCallback
		{
			get
			{
				return this.async_callback;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000264FB File Offset: 0x000246FB
		public object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00026504 File Offset: 0x00024704
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.wait_handle == null)
					{
						this.wait_handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.wait_handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0002655C File Offset: 0x0002475C
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00026564 File Offset: 0x00024764
		public bool CompletedSynchronously
		{
			get
			{
				return this.completed_synchronously;
			}
			protected set
			{
				this.completed_synchronously = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0002656D File Offset: 0x0002476D
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00026578 File Offset: 0x00024778
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
			protected set
			{
				this.completed = value;
				lock (this)
				{
					if (value && this.wait_handle != null)
					{
						this.wait_handle.Set();
					}
				}
			}
		}

		// Token: 0x060007A6 RID: 1958
		internal abstract void CompleteDisposed();

		// Token: 0x04000D5D RID: 3421
		private AsyncCallback async_callback;

		// Token: 0x04000D5E RID: 3422
		private object async_state;

		// Token: 0x04000D5F RID: 3423
		private ManualResetEvent wait_handle;

		// Token: 0x04000D60 RID: 3424
		private bool completed_synchronously;

		// Token: 0x04000D61 RID: 3425
		private bool completed;
	}
}
