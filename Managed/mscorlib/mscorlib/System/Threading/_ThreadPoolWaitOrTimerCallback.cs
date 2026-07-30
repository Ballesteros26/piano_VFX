using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000494 RID: 1172
	internal class _ThreadPoolWaitOrTimerCallback
	{
		// Token: 0x0600372E RID: 14126 RVA: 0x000C9AD1 File Offset: 0x000C7CD1
		[SecurityCritical]
		internal _ThreadPoolWaitOrTimerCallback(WaitOrTimerCallback waitOrTimerCallback, object state, bool compressStack, ref StackCrawlMark stackMark)
		{
			this._waitOrTimerCallback = waitOrTimerCallback;
			this._state = state;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000C9AFF File Offset: 0x000C7CFF
		[SecurityCritical]
		private static void WaitOrTimerCallback_Context_t(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, true);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000C9B08 File Offset: 0x000C7D08
		[SecurityCritical]
		private static void WaitOrTimerCallback_Context_f(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, false);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000C9B14 File Offset: 0x000C7D14
		private static void WaitOrTimerCallback_Context(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000C9B3C File Offset: 0x000C7D3C
		[SecurityCritical]
		internal static void PerformWaitOrTimerCallback(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			if (threadPoolWaitOrTimerCallback._executionContext == null)
			{
				threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
				return;
			}
			using (ExecutionContext executionContext = threadPoolWaitOrTimerCallback._executionContext.CreateCopy())
			{
				if (timedOut)
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbt, threadPoolWaitOrTimerCallback, true);
				}
				else
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbf, threadPoolWaitOrTimerCallback, true);
				}
			}
		}

		// Token: 0x04001D05 RID: 7429
		private WaitOrTimerCallback _waitOrTimerCallback;

		// Token: 0x04001D06 RID: 7430
		private ExecutionContext _executionContext;

		// Token: 0x04001D07 RID: 7431
		private object _state;

		// Token: 0x04001D08 RID: 7432
		[SecurityCritical]
		private static ContextCallback _ccbt = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_t);

		// Token: 0x04001D09 RID: 7433
		[SecurityCritical]
		private static ContextCallback _ccbf = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_f);
	}
}
