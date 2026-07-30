using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides the context for waiting when asynchronously switching into a target environment.</summary>
	// Token: 0x0200085C RID: 2140
	public struct YieldAwaitable
	{
		/// <summary>Retrieves a <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> object  for this instance of the class.</summary>
		/// <returns>The object that is used to monitor the completion of an asynchronous operation.</returns>
		// Token: 0x0600542E RID: 21550 RVA: 0x001271C8 File Offset: 0x001253C8
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		/// <summary>Provides an awaiter for switching into a target environment.</summary>
		// Token: 0x0200085D RID: 2141
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			/// <summary>Gets a value that indicates whether a yield is not required.</summary>
			/// <returns>Always false, which indicates that a yield is always required for <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" />.</returns>
			// Token: 0x17000EB6 RID: 3766
			// (get) Token: 0x0600542F RID: 21551 RVA: 0x00015ED5 File Offset: 0x000140D5
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			/// <summary>Sets the continuation to invoke.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="continuation" /> is null.</exception>
			// Token: 0x06005430 RID: 21552 RVA: 0x001271DE File Offset: 0x001253DE
			[SecuritySafeCritical]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			/// <summary>Posts the <paramref name="continuation" /> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			// Token: 0x06005431 RID: 21553 RVA: 0x001271E7 File Offset: 0x001253E7
			[SecurityCritical]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x06005432 RID: 21554 RVA: 0x001271F0 File Offset: 0x001253F0
			[SecurityCritical]
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x06005433 RID: 21555 RVA: 0x000D6EF4 File Offset: 0x000D50F4
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			/// <summary>Ends the await operation.</summary>
			// Token: 0x06005434 RID: 21556 RVA: 0x00002194 File Offset: 0x00000394
			public void GetResult()
			{
			}

			// Token: 0x04002BB0 RID: 11184
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x04002BB1 RID: 11185
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
