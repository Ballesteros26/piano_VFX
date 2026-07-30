using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Encapsulates the results of an asynchronous operation on a delegate.</summary>
	// Token: 0x020007F9 RID: 2041
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncResult : IAsyncResult, IMessageSink, IThreadPoolWorkItem
	{
		// Token: 0x060051D4 RID: 20948 RVA: 0x00002111 File Offset: 0x00000311
		internal AsyncResult()
		{
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x001217D0 File Offset: 0x0011F9D0
		internal AsyncResult(WaitCallback cb, object state, bool capture_context)
		{
			this.orig_cb = cb;
			if (capture_context)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
				this.current = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
				cb = delegate
				{
					ExecutionContext.Run(this.current, AsyncResult.ccb, this, true);
				};
			}
			this.async_state = state;
			this.async_delegate = cb;
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x0012181C File Offset: 0x0011FA1C
		private static void WaitCallback_Context(object state)
		{
			AsyncResult asyncResult = (AsyncResult)state;
			asyncResult.orig_cb(asyncResult.async_state);
		}

		/// <summary>Gets the object provided as the last parameter of a BeginInvoke method call.</summary>
		/// <returns>The object provided as the last parameter of a BeginInvoke method call.</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x00121841 File Offset: 0x0011FA41
		public virtual object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x0012184C File Offset: 0x0011FA4C
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.handle;
				}
				return waitHandle;
			}
		}

		/// <summary>Gets a value indicating whether the BeginInvoke call completed synchronously.</summary>
		/// <returns>true if the BeginInvoke call completed synchronously; otherwise, false.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x001218A4 File Offset: 0x0011FAA4
		public virtual bool CompletedSynchronously
		{
			get
			{
				return this.sync_completed;
			}
		}

		/// <summary>Gets a value indicating whether the server has completed the call.</summary>
		/// <returns>true after the server has completed the call; otherwise, false.</returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x001218AC File Offset: 0x0011FAAC
		public virtual bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		/// <summary>Gets or sets a value indicating whether EndInvoke has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />.</summary>
		/// <returns>true if EndInvoke has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />; otherwise, false.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x001218B4 File Offset: 0x0011FAB4
		// (set) Token: 0x060051DC RID: 20956 RVA: 0x001218BC File Offset: 0x0011FABC
		public bool EndInvokeCalled
		{
			get
			{
				return this.endinvoke_called;
			}
			set
			{
				this.endinvoke_called = value;
			}
		}

		/// <summary>Gets the delegate object on which the asynchronous call was invoked.</summary>
		/// <returns>The delegate object on which the asynchronous call was invoked.</returns>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060051DD RID: 20957 RVA: 0x001218C5 File Offset: 0x0011FAC5
		public virtual object AsyncDelegate
		{
			get
			{
				return this.async_delegate;
			}
		}

		/// <summary>Gets the next message sink in the sink chain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface that represents the next message sink in the sink chain.</returns>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060051DE RID: 20958 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="msg">The request <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface. </param>
		/// <param name="replySink">The response <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface. </param>
		// Token: 0x060051DF RID: 20959 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the response message for the asynchronous call.</summary>
		/// <returns>A remoting message that should represent a response to a method call on a remote object.</returns>
		// Token: 0x060051E0 RID: 20960 RVA: 0x001218CD File Offset: 0x0011FACD
		public virtual IMessage GetReplyMessage()
		{
			return this.reply_message;
		}

		/// <summary>Sets an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call, which provides a way to control asynchronous messages after they have been dispatched.</summary>
		/// <param name="mc">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call. </param>
		// Token: 0x060051E1 RID: 20961 RVA: 0x001218D5 File Offset: 0x0011FAD5
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this.message_ctrl = mc;
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x001218DE File Offset: 0x0011FADE
		internal void SetCompletedSynchronously(bool completed)
		{
			this.sync_completed = completed;
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x001218E8 File Offset: 0x0011FAE8
		internal IMessage EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					return this.reply_message;
				}
			}
			this.AsyncWaitHandle.WaitOne();
			return this.reply_message;
		}

		/// <summary>Synchronously processes a response message returned by a method call on a remote object.</summary>
		/// <returns>Returns null.</returns>
		/// <param name="msg">A response message to a method call on a remote object.</param>
		// Token: 0x060051E4 RID: 20964 RVA: 0x00121944 File Offset: 0x0011FB44
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			this.reply_message = msg;
			lock (this)
			{
				this.completed = true;
				if (this.handle != null)
				{
					((ManualResetEvent)this.AsyncWaitHandle).Set();
				}
			}
			if (this.async_callback != null)
			{
				((AsyncCallback)this.async_callback)(this);
			}
			return null;
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x001219BC File Offset: 0x0011FBBC
		// (set) Token: 0x060051E6 RID: 20966 RVA: 0x001219C4 File Offset: 0x0011FBC4
		internal MonoMethodMessage CallMessage
		{
			get
			{
				return this.call_message;
			}
			set
			{
				this.call_message = value;
			}
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x001219CD File Offset: 0x0011FBCD
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.Invoke();
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x00002194 File Offset: 0x00000394
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x060051E9 RID: 20969
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object Invoke();

		// Token: 0x04002AD5 RID: 10965
		private object async_state;

		// Token: 0x04002AD6 RID: 10966
		private WaitHandle handle;

		// Token: 0x04002AD7 RID: 10967
		private object async_delegate;

		// Token: 0x04002AD8 RID: 10968
		private IntPtr data;

		// Token: 0x04002AD9 RID: 10969
		private object object_data;

		// Token: 0x04002ADA RID: 10970
		private bool sync_completed;

		// Token: 0x04002ADB RID: 10971
		private bool completed;

		// Token: 0x04002ADC RID: 10972
		private bool endinvoke_called;

		// Token: 0x04002ADD RID: 10973
		private object async_callback;

		// Token: 0x04002ADE RID: 10974
		private ExecutionContext current;

		// Token: 0x04002ADF RID: 10975
		private ExecutionContext original;

		// Token: 0x04002AE0 RID: 10976
		private long add_time;

		// Token: 0x04002AE1 RID: 10977
		private MonoMethodMessage call_message;

		// Token: 0x04002AE2 RID: 10978
		private IMessageCtrl message_ctrl;

		// Token: 0x04002AE3 RID: 10979
		private IMessage reply_message;

		// Token: 0x04002AE4 RID: 10980
		private WaitCallback orig_cb;

		// Token: 0x04002AE5 RID: 10981
		internal static ContextCallback ccb = new ContextCallback(AsyncResult.WaitCallback_Context);
	}
}
