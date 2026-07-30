using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Diagnostics;
using Unity;

namespace System.Web.Services.Protocols
{
	/// <summary>Provides an implementation of <see cref="T:System.IAsyncResult" /> for use by XML Web service proxies to implement the standard asynchronous method pattern.</summary>
	// Token: 0x0200001D RID: 29
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class WebClientAsyncResult : IAsyncResult
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000035FB File Offset: 0x000017FB
		internal WebClientAsyncResult(WebClientProtocol clientProtocol, object internalAsyncState, WebRequest request, AsyncCallback userCallback, object userAsyncState)
		{
			this.ClientProtocol = clientProtocol;
			this.InternalAsyncState = internalAsyncState;
			this.userAsyncState = userAsyncState;
			this.userCallback = userCallback;
			this.Request = request;
			this.completedSynchronously = true;
		}

		/// <summary>Gets the object provided in the last parameter to the Begin method asynchronous call.</summary>
		/// <returns>The <see cref="T:System.Object" /> provided in the last parameter to the Begin method call.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000362F File Offset: 0x0000182F
		public object AsyncState
		{
			get
			{
				return this.userAsyncState;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003638 File Offset: 0x00001838
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				bool flag = this.isCompleted;
				if (this.manualResetEvent == null)
				{
					lock (this)
					{
						if (this.manualResetEvent == null)
						{
							this.manualResetEvent = new ManualResetEvent(flag);
						}
					}
				}
				if (!flag && this.isCompleted)
				{
					this.manualResetEvent.Set();
				}
				return this.manualResetEvent;
			}
		}

		/// <summary>Gets a value indicating whether the Begin call completed synchronously.</summary>
		/// <returns>true if the Begin call completed synchronously; otherwise, false.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000036B8 File Offset: 0x000018B8
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		/// <summary>Gets a value indicating whether the asynchronous XML Web service request has completed.</summary>
		/// <returns>true if the asynchronous XML Web service request has completed; otherwise, false.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000036C0 File Offset: 0x000018C0
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		/// <summary>Cancels an asynchronous XML Web service request.</summary>
		// Token: 0x060000A2 RID: 162 RVA: 0x000036C8 File Offset: 0x000018C8
		public void Abort()
		{
			WebRequest request = this.Request;
			if (request != null)
			{
				request.Abort();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000036E8 File Offset: 0x000018E8
		internal void Complete()
		{
			try
			{
				if (this.ResponseStream != null)
				{
					this.ResponseStream.Close();
					this.ResponseStream = null;
				}
				if (this.ResponseBufferedStream != null)
				{
					this.ResponseBufferedStream.Position = 0L;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (this.Exception == null)
				{
					this.Exception = ex;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "Complete", ex);
				}
			}
			this.isCompleted = true;
			try
			{
				if (this.manualResetEvent != null)
				{
					this.manualResetEvent.Set();
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				if (this.Exception == null)
				{
					this.Exception = ex2;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "Complete", ex2);
				}
			}
			if (this.userCallback != null)
			{
				this.userCallback(this);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037FC File Offset: 0x000019FC
		internal void Complete(Exception e)
		{
			this.Exception = e;
			this.Complete();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000380B File Offset: 0x00001A0B
		internal WebResponse WaitForResponse()
		{
			if (!this.isCompleted)
			{
				this.AsyncWaitHandle.WaitOne();
			}
			if (this.Exception != null)
			{
				throw this.Exception;
			}
			return this.Response;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003836 File Offset: 0x00001A36
		internal void CombineCompletedSynchronously(bool innerCompletedSynchronously)
		{
			this.completedSynchronously = this.completedSynchronously && innerCompletedSynchronously;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003846 File Offset: 0x00001A46
		internal WebClientAsyncResult()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040001B6 RID: 438
		private object userAsyncState;

		// Token: 0x040001B7 RID: 439
		private bool completedSynchronously;

		// Token: 0x040001B8 RID: 440
		private bool isCompleted;

		// Token: 0x040001B9 RID: 441
		private volatile ManualResetEvent manualResetEvent;

		// Token: 0x040001BA RID: 442
		private AsyncCallback userCallback;

		// Token: 0x040001BB RID: 443
		internal WebClientProtocol ClientProtocol;

		// Token: 0x040001BC RID: 444
		internal object InternalAsyncState;

		// Token: 0x040001BD RID: 445
		internal Exception Exception;

		// Token: 0x040001BE RID: 446
		internal WebResponse Response;

		// Token: 0x040001BF RID: 447
		internal WebRequest Request;

		// Token: 0x040001C0 RID: 448
		internal Stream ResponseStream;

		// Token: 0x040001C1 RID: 449
		internal Stream ResponseBufferedStream;

		// Token: 0x040001C2 RID: 450
		internal byte[] Buffer;

		// Token: 0x040001C3 RID: 451
		internal bool EndSendCalled;
	}
}
