using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000513 RID: 1299
	internal class FtpAsyncResult : IAsyncResult
	{
		// Token: 0x060026F6 RID: 9974 RVA: 0x00096A67 File Offset: 0x00094C67
		public FtpAsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x00096A88 File Offset: 0x00094C88
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00096A90 File Offset: 0x00094C90
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.waitHandle == null)
					{
						this.waitHandle = new ManualResetEvent(false);
					}
				}
				return this.waitHandle;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x00096AE4 File Offset: 0x00094CE4
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x00096AEC File Offset: 0x00094CEC
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x00096B30 File Offset: 0x00094D30
		internal bool GotException
		{
			get
			{
				return this.exception != null;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x00096B3B File Offset: 0x00094D3B
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00096B43 File Offset: 0x00094D43
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x00096B4B File Offset: 0x00094D4B
		internal FtpWebResponse Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00096B54 File Offset: 0x00094D54
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x00096B5C File Offset: 0x00094D5C
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00096B65 File Offset: 0x00094D65
		internal void WaitUntilComplete()
		{
			if (this.IsCompleted)
			{
				return;
			}
			this.AsyncWaitHandle.WaitOne();
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x00096B7C File Offset: 0x00094D7C
		internal bool WaitUntilComplete(int timeout, bool exitContext)
		{
			return this.IsCompleted || this.AsyncWaitHandle.WaitOne(timeout, exitContext);
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x00096B98 File Offset: 0x00094D98
		internal void SetCompleted(bool synch, Exception exc, FtpWebResponse response)
		{
			this.synch = synch;
			this.exception = exc;
			this.response = response;
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.waitHandle != null)
				{
					this.waitHandle.Set();
				}
			}
			this.DoCallback();
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x00096C08 File Offset: 0x00094E08
		internal void SetCompleted(bool synch, FtpWebResponse response)
		{
			this.SetCompleted(synch, null, response);
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x00096C13 File Offset: 0x00094E13
		internal void SetCompleted(bool synch, Exception exc)
		{
			this.SetCompleted(synch, exc, null);
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x00096C20 File Offset: 0x00094E20
		internal void DoCallback()
		{
			if (this.callback != null)
			{
				try
				{
					this.callback(this);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00096C58 File Offset: 0x00094E58
		internal void Reset()
		{
			this.exception = null;
			this.synch = false;
			this.response = null;
			this.state = null;
			object obj = this.locker;
			lock (obj)
			{
				this.completed = false;
				if (this.waitHandle != null)
				{
					this.waitHandle.Reset();
				}
			}
		}

		// Token: 0x0400212F RID: 8495
		private FtpWebResponse response;

		// Token: 0x04002130 RID: 8496
		private ManualResetEvent waitHandle;

		// Token: 0x04002131 RID: 8497
		private Exception exception;

		// Token: 0x04002132 RID: 8498
		private AsyncCallback callback;

		// Token: 0x04002133 RID: 8499
		private Stream stream;

		// Token: 0x04002134 RID: 8500
		private object state;

		// Token: 0x04002135 RID: 8501
		private bool completed;

		// Token: 0x04002136 RID: 8502
		private bool synch;

		// Token: 0x04002137 RID: 8503
		private object locker = new object();
	}
}
