using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x020000DB RID: 219
	internal sealed class TaskAsyncResult : IAsyncResult
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0001FB9E File Offset: 0x0001DD9E
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0001FBA6 File Offset: 0x0001DDA6
		public object AsyncState { get; private set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0001FBAF File Offset: 0x0001DDAF
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.task).AsyncWaitHandle;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		public bool CompletedSynchronously { get; private set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0001FBCD File Offset: 0x0001DDCD
		public bool IsCompleted
		{
			get
			{
				return this.task.IsCompleted;
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001FBDA File Offset: 0x0001DDDA
		private TaskAsyncResult(Task task, AsyncCallback callback, object state)
		{
			this.task = task;
			this.callback = callback;
			this.AsyncState = state;
			this.CompletedSynchronously = task.IsCompleted;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001FC04 File Offset: 0x0001DE04
		public static IAsyncResult GetAsyncResult(Task task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				return null;
			}
			TaskAsyncResult taskAsyncResult = new TaskAsyncResult(task, callback, state);
			if (callback != null)
			{
				if (taskAsyncResult.CompletedSynchronously)
				{
					callback(taskAsyncResult);
				}
				else
				{
					task.ContinueWith(TaskAsyncResult.invokeCallback, taskAsyncResult);
				}
			}
			return taskAsyncResult;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001FC44 File Offset: 0x0001DE44
		public static void Wait(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			TaskAsyncResult taskAsyncResult = result as TaskAsyncResult;
			if (taskAsyncResult == null)
			{
				throw new ArgumentException("The provided IAsyncResult is invalid.", "result");
			}
			taskAsyncResult.task.GetAwaiter().GetResult();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		private static void InvokeCallback(Task task, object state)
		{
			TaskAsyncResult taskAsyncResult = (TaskAsyncResult)state;
			taskAsyncResult.callback(taskAsyncResult);
		}

		// Token: 0x040010B5 RID: 4277
		private static readonly Action<Task, object> invokeCallback = new Action<Task, object>(TaskAsyncResult.InvokeCallback);

		// Token: 0x040010B6 RID: 4278
		private readonly Task task;

		// Token: 0x040010B7 RID: 4279
		private readonly AsyncCallback callback;
	}
}
