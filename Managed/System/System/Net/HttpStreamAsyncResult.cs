using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200052A RID: 1322
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x0009BF0A File Offset: 0x0009A10A
		public void Complete(Exception e)
		{
			this.Error = e;
			this.Complete();
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0009BF1C File Offset: 0x0009A11C
		public void Complete()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.Callback != null)
					{
						this.Callback.BeginInvoke(this, null, null);
					}
				}
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0009BF94 File Offset: 0x0009A194
		public object AsyncState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x0009BF9C File Offset: 0x0009A19C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
		public bool CompletedSynchronously
		{
			get
			{
				return this.SynchRead == this.Count;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0009C008 File Offset: 0x0009A208
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x040021E8 RID: 8680
		private object locker = new object();

		// Token: 0x040021E9 RID: 8681
		private ManualResetEvent handle;

		// Token: 0x040021EA RID: 8682
		private bool completed;

		// Token: 0x040021EB RID: 8683
		internal byte[] Buffer;

		// Token: 0x040021EC RID: 8684
		internal int Offset;

		// Token: 0x040021ED RID: 8685
		internal int Count;

		// Token: 0x040021EE RID: 8686
		internal AsyncCallback Callback;

		// Token: 0x040021EF RID: 8687
		internal object State;

		// Token: 0x040021F0 RID: 8688
		internal int SynchRead;

		// Token: 0x040021F1 RID: 8689
		internal Exception Error;
	}
}
