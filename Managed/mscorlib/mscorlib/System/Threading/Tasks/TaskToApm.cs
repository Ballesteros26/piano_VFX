using System;
using System.IO;

namespace System.Threading.Tasks
{
	// Token: 0x02000523 RID: 1315
	internal static class TaskToApm
	{
		// Token: 0x06003BFF RID: 15359 RVA: 0x000D8AD8 File Offset: 0x000D6CD8
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000D8B28 File Offset: 0x000D6D28
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000D8B68 File Offset: 0x000D6D68
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x000D8BAC File Offset: 0x000D6DAC
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000524 RID: 1316
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06003C03 RID: 15363 RVA: 0x000D8BF0 File Offset: 0x000D6DF0
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this.m_state = state;
				this.m_completedSynchronously = completedSynchronously;
			}

			// Token: 0x170009D0 RID: 2512
			// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000D8C0D File Offset: 0x000D6E0D
			object IAsyncResult.AsyncState
			{
				get
				{
					return this.m_state;
				}
			}

			// Token: 0x170009D1 RID: 2513
			// (get) Token: 0x06003C05 RID: 15365 RVA: 0x000D8C15 File Offset: 0x000D6E15
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this.m_completedSynchronously;
				}
			}

			// Token: 0x170009D2 RID: 2514
			// (get) Token: 0x06003C06 RID: 15366 RVA: 0x000D8C1D File Offset: 0x000D6E1D
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x06003C07 RID: 15367 RVA: 0x000D8C2A File Offset: 0x000D6E2A
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04001F17 RID: 7959
			internal readonly Task Task;

			// Token: 0x04001F18 RID: 7960
			private readonly object m_state;

			// Token: 0x04001F19 RID: 7961
			private readonly bool m_completedSynchronously;
		}
	}
}
