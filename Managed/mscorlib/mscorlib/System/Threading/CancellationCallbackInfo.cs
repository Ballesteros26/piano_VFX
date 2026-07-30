using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000452 RID: 1106
	internal class CancellationCallbackInfo
	{
		// Token: 0x06003501 RID: 13569 RVA: 0x000C4020 File Offset: 0x000C2220
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetSyncContext = targetSyncContext;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000C4050 File Offset: 0x000C2250
		[SecuritySafeCritical]
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000C4098 File Offset: 0x000C2298
		[SecurityCritical]
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x04001C3F RID: 7231
		internal readonly Action<object> Callback;

		// Token: 0x04001C40 RID: 7232
		internal readonly object StateForCallback;

		// Token: 0x04001C41 RID: 7233
		internal readonly SynchronizationContext TargetSyncContext;

		// Token: 0x04001C42 RID: 7234
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x04001C43 RID: 7235
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x04001C44 RID: 7236
		[SecurityCritical]
		private static ContextCallback s_executionContextCallback;
	}
}
