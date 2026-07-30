using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000493 RID: 1171
	internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
	{
		// Token: 0x06003728 RID: 14120 RVA: 0x000C9A06 File Offset: 0x000C7C06
		[SecurityCritical]
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000C9A34 File Offset: 0x000C7C34
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			this.context = ec;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000C9A51 File Offset: 0x000C7C51
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.context == null)
			{
				WaitCallback waitCallback = this.callback;
				this.callback = null;
				waitCallback(this.state);
				return;
			}
			ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, this, true);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x00002194 File Offset: 0x00000394
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000C9A88 File Offset: 0x000C7C88
		[SecurityCritical]
		private static void WaitCallback_Context(object state)
		{
			QueueUserWorkItemCallback queueUserWorkItemCallback = (QueueUserWorkItemCallback)state;
			queueUserWorkItemCallback.callback(queueUserWorkItemCallback.state);
		}

		// Token: 0x04001D01 RID: 7425
		private WaitCallback callback;

		// Token: 0x04001D02 RID: 7426
		private ExecutionContext context;

		// Token: 0x04001D03 RID: 7427
		private object state;

		// Token: 0x04001D04 RID: 7428
		[SecurityCritical]
		internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
	}
}
