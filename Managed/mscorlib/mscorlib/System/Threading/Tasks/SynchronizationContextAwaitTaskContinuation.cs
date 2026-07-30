using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000513 RID: 1299
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000D6BDC File Offset: 0x000D4DDC
		[SecurityCritical]
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
			: base(action, flowExecutionContext, ref stackMark)
		{
			this.m_syncContext = context;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000D6BEF File Offset: 0x000D4DEF
		[SecuritySafeCritical]
		internal sealed override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.CurrentNoFlow)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000D6C2C File Offset: 0x000D4E2C
		[SecurityCritical]
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000D6C58 File Offset: 0x000D4E58
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x04001EEF RID: 7919
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001EF0 RID: 7920
		[SecurityCritical]
		private static ContextCallback s_postActionCallback;

		// Token: 0x04001EF1 RID: 7921
		private readonly SynchronizationContext m_syncContext;
	}
}
