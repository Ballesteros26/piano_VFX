using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000538 RID: 1336
	internal class ListenerAsyncResult : IAsyncResult
	{
		// Token: 0x0600295B RID: 10587 RVA: 0x0009FA48 File Offset: 0x0009DC48
		public ListenerAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0009FA6C File Offset: 0x0009DC6C
		internal void Complete(Exception exc)
		{
			if (this.forward != null)
			{
				this.forward.Complete(exc);
				return;
			}
			this.exception = exc;
			if (this.InGet && exc is ObjectDisposedException)
			{
				this.exception = new HttpListenerException(500, "Listener closed");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
				if (this.cb != null)
				{
					ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
				}
			}
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0009FB18 File Offset: 0x0009DD18
		private static void InvokeCallback(object o)
		{
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)o;
			if (listenerAsyncResult.forward != null)
			{
				ListenerAsyncResult.InvokeCallback(listenerAsyncResult.forward);
				return;
			}
			try
			{
				listenerAsyncResult.cb(listenerAsyncResult);
			}
			catch
			{
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0009FB64 File Offset: 0x0009DD64
		internal void Complete(HttpListenerContext context)
		{
			this.Complete(context, false);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0009FB70 File Offset: 0x0009DD70
		internal void Complete(HttpListenerContext context, bool synch)
		{
			if (this.forward != null)
			{
				this.forward.Complete(context, synch);
				return;
			}
			this.synch = synch;
			this.context = context;
			object obj = this.locker;
			lock (obj)
			{
				AuthenticationSchemes authenticationSchemes = context.Listener.SelectAuthenticationScheme(context);
				if ((authenticationSchemes == AuthenticationSchemes.Basic || context.Listener.AuthenticationSchemes == AuthenticationSchemes.Negotiate) && context.Request.Headers["Authorization"] == null)
				{
					context.Response.StatusCode = 401;
					context.Response.Headers["WWW-Authenticate"] = string.Concat(new object[]
					{
						authenticationSchemes,
						" realm=\"",
						context.Listener.Realm,
						"\""
					});
					context.Response.OutputStream.Close();
					IAsyncResult asyncResult = context.Listener.BeginGetContext(this.cb, this.state);
					this.forward = (ListenerAsyncResult)asyncResult;
					object obj2 = this.forward.locker;
					lock (obj2)
					{
						if (this.handle != null)
						{
							this.forward.handle = this.handle;
						}
					}
					ListenerAsyncResult listenerAsyncResult = this.forward;
					int num = 0;
					while (listenerAsyncResult.forward != null)
					{
						if (num > 20)
						{
							this.Complete(new HttpListenerException(400, "Too many authentication errors"));
						}
						listenerAsyncResult = listenerAsyncResult.forward;
						num++;
					}
				}
				else
				{
					this.completed = true;
					this.synch = false;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.cb != null)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
					}
				}
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x0009FD74 File Offset: 0x0009DF74
		internal HttpListenerContext GetContext()
		{
			if (this.forward != null)
			{
				return this.forward.GetContext();
			}
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.context;
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x0009FD9F File Offset: 0x0009DF9F
		public object AsyncState
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncState;
				}
				return this.state;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x0009FDBC File Offset: 0x0009DFBC
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncWaitHandle;
				}
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

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x0009FE2C File Offset: 0x0009E02C
		public bool CompletedSynchronously
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.CompletedSynchronously;
				}
				return this.synch;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x0009FE48 File Offset: 0x0009E048
		public bool IsCompleted
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.IsCompleted;
				}
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x04002273 RID: 8819
		private ManualResetEvent handle;

		// Token: 0x04002274 RID: 8820
		private bool synch;

		// Token: 0x04002275 RID: 8821
		private bool completed;

		// Token: 0x04002276 RID: 8822
		private AsyncCallback cb;

		// Token: 0x04002277 RID: 8823
		private object state;

		// Token: 0x04002278 RID: 8824
		private Exception exception;

		// Token: 0x04002279 RID: 8825
		private HttpListenerContext context;

		// Token: 0x0400227A RID: 8826
		private object locker = new object();

		// Token: 0x0400227B RID: 8827
		private ListenerAsyncResult forward;

		// Token: 0x0400227C RID: 8828
		internal bool EndCalled;

		// Token: 0x0400227D RID: 8829
		internal bool InGet;

		// Token: 0x0400227E RID: 8830
		private static WaitCallback InvokeCB = new WaitCallback(ListenerAsyncResult.InvokeCallback);
	}
}
