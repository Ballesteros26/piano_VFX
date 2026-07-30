using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000037 RID: 55
	internal static class TaskToApm
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000D00C File Offset: 0x0000B20C
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

		// Token: 0x06000224 RID: 548 RVA: 0x0000D05C File Offset: 0x0000B25C
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
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000D09C File Offset: 0x0000B29C
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
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000038 RID: 56
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06000227 RID: 551 RVA: 0x0000D124 File Offset: 0x0000B324
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000228 RID: 552 RVA: 0x0000D141 File Offset: 0x0000B341
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000229 RID: 553 RVA: 0x0000D149 File Offset: 0x0000B349
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x0600022A RID: 554 RVA: 0x0000D151 File Offset: 0x0000B351
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x0600022B RID: 555 RVA: 0x0000D15E File Offset: 0x0000B35E
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04000447 RID: 1095
			internal readonly Task Task;

			// Token: 0x04000448 RID: 1096
			private readonly object _state;

			// Token: 0x04000449 RID: 1097
			private readonly bool _completedSynchronously;
		}
	}
}
