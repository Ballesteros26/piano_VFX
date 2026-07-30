using System;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000046 RID: 70
	internal class AsyncMethodResult : IAsyncResult
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public AsyncMethodResult()
		{
			this.handle = new ManualResetEvent(false);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					waitHandle = this.handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0001102C File Offset: 0x0000F22C
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00011034 File Offset: 0x0000F234
		public object AsyncState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00011040 File Offset: 0x0000F240
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00011044 File Offset: 0x0000F244
		public bool IsCompleted
		{
			get
			{
				bool flag;
				lock (this)
				{
					flag = this.completed;
				}
				return flag;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00011090 File Offset: 0x0000F290
		public object EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					if (this.exception == null)
					{
						return this.return_value;
					}
					throw this.exception;
				}
			}
			this.handle.WaitOne();
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.return_value;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001111C File Offset: 0x0000F31C
		public void Complete(object result)
		{
			lock (this)
			{
				this.completed = true;
				this.return_value = result;
				this.handle.Set();
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00011174 File Offset: 0x0000F374
		public void CompleteWithException(Exception ex)
		{
			lock (this)
			{
				this.completed = true;
				this.exception = ex;
				this.handle.Set();
			}
		}

		// Token: 0x040005DB RID: 1499
		private ManualResetEvent handle;

		// Token: 0x040005DC RID: 1500
		private object state;

		// Token: 0x040005DD RID: 1501
		private bool completed;

		// Token: 0x040005DE RID: 1502
		private object return_value;

		// Token: 0x040005DF RID: 1503
		private Exception exception;
	}
}
