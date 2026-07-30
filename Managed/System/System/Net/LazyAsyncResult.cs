using System;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200048C RID: 1164
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00085A9C File Offset: 0x00083C9C
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00085ABF File Offset: 0x00083CBF
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00085AE7 File Offset: 0x00083CE7
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack, object result)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = result;
			this.m_IntCompleted = 1;
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x00085B27 File Offset: 0x00083D27
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x00085B2F File Offset: 0x00083D2F
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x00085B37 File Offset: 0x00083D37
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x00085B3F File Offset: 0x00083D3F
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
			set
			{
				this.m_AsyncCallback = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x00085B48 File Offset: 0x00083D48
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00085B94 File Offset: 0x00083D94
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool flag;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					flag = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					flag = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		protected void DebugProtectState(bool protect)
		{
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x00085C0C File Offset: 0x00083E0C
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x00085C3C File Offset: 0x00083E3C
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x00085C6F File Offset: 0x00083E6F
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x00085C80 File Offset: 0x00083E80
		// (set) Token: 0x06002245 RID: 8773 RVA: 0x00085C97 File Offset: 0x00083E97
		internal object Result
		{
			get
			{
				if (this.m_Result != DBNull.Value)
				{
					return this.m_Result;
				}
				return null;
			}
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00085CA0 File Offset: 0x00083EA0
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x00085CA8 File Offset: 0x00083EA8
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00085CB1 File Offset: 0x00083EB1
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x00085CB9 File Offset: 0x00083EB9
		internal int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00085CC4 File Offset: 0x00083EC4
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00085D4C File Offset: 0x00083F4C
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00085D5A File Offset: 0x00083F5A
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00085D68 File Offset: 0x00083F68
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00085DEC File Offset: 0x00083FEC
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00085E20 File Offset: 0x00084020
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00085E2C File Offset: 0x0008402C
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_0073;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_0073;
			}
			catch (ObjectDisposedException)
			{
				goto IL_0073;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_006D:
			Thread.SpinWait(1);
			IL_0073:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_006D;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00085EDC File Offset: 0x000840DC
		internal void InternalCleanup()
		{
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				this.m_Result = null;
				this.Cleanup();
			}
		}

		// Token: 0x04001EEE RID: 7918
		private const int c_HighBit = -2147483648;

		// Token: 0x04001EEF RID: 7919
		private const int c_ForceAsyncCount = 50;

		// Token: 0x04001EF0 RID: 7920
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x04001EF1 RID: 7921
		private object m_AsyncObject;

		// Token: 0x04001EF2 RID: 7922
		private object m_AsyncState;

		// Token: 0x04001EF3 RID: 7923
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04001EF4 RID: 7924
		private object m_Result;

		// Token: 0x04001EF5 RID: 7925
		private int m_ErrorCode;

		// Token: 0x04001EF6 RID: 7926
		private int m_IntCompleted;

		// Token: 0x04001EF7 RID: 7927
		private bool m_EndCalled;

		// Token: 0x04001EF8 RID: 7928
		private bool m_UserEvent;

		// Token: 0x04001EF9 RID: 7929
		private object m_Event;

		// Token: 0x0200048D RID: 1165
		private class ThreadContext
		{
			// Token: 0x04001EFA RID: 7930
			internal int m_NestedIOCount;
		}
	}
}
