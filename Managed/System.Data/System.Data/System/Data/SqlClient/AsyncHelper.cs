using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001EE RID: 494
	internal static class AsyncHelper
	{
		// Token: 0x060016CB RID: 5835 RVA: 0x00070DB8 File Offset: 0x0006EFB8
		internal static Task CreateContinuationTask(Task task, Action onSuccess, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null)
		{
			AsyncHelper.<>c__DisplayClass0_0 CS$<>8__locals1 = new AsyncHelper.<>c__DisplayClass0_0();
			CS$<>8__locals1.onSuccess = onSuccess;
			if (task == null)
			{
				CS$<>8__locals1.onSuccess();
				return null;
			}
			TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
			AsyncHelper.ContinueTask(task, completion, delegate
			{
				CS$<>8__locals1.onSuccess();
				completion.SetResult(null);
			}, connectionToDoom, onFailure, null, null, null);
			return completion.Task;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00070E24 File Offset: 0x0006F024
		internal static Task CreateContinuationTask<T1, T2>(Task task, Action<T1, T2> onSuccess, T1 arg1, T2 arg2, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null)
		{
			return AsyncHelper.CreateContinuationTask(task, delegate
			{
				onSuccess(arg1, arg2);
			}, connectionToDoom, onFailure);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00070E64 File Offset: 0x0006F064
		internal static void ContinueTask(Task task, TaskCompletionSource<object> completion, Action onSuccess, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null, Action onCancellation = null, Func<Exception, Exception> exceptionConverter = null, SqlConnection connectionToAbort = null)
		{
			task.ContinueWith(delegate(Task tsk)
			{
				if (tsk.Exception != null)
				{
					Exception ex = tsk.Exception.InnerException;
					if (exceptionConverter != null)
					{
						ex = exceptionConverter(ex);
					}
					try
					{
						if (onFailure != null)
						{
							onFailure(ex);
						}
						return;
					}
					finally
					{
						completion.TrySetException(ex);
					}
				}
				if (tsk.IsCanceled)
				{
					try
					{
						if (onCancellation != null)
						{
							onCancellation();
						}
						return;
					}
					finally
					{
						completion.TrySetCanceled();
					}
				}
				try
				{
					onSuccess();
				}
				catch (Exception ex2)
				{
					completion.SetException(ex2);
				}
			}, TaskScheduler.Default);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00070EB8 File Offset: 0x0006F0B8
		internal static void WaitForCompletion(Task task, int timeout, Action onTimeout = null, bool rethrowExceptions = true)
		{
			try
			{
				task.Wait((timeout > 0) ? (1000 * timeout) : (-1));
			}
			catch (AggregateException ex)
			{
				if (rethrowExceptions)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
			}
			if (!task.IsCompleted && onTimeout != null)
			{
				onTimeout();
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00070F14 File Offset: 0x0006F114
		internal static void SetTimeoutException(TaskCompletionSource<object> completion, int timeout, Func<Exception> exc, CancellationToken ctoken)
		{
			if (timeout > 0)
			{
				Task.Delay(timeout * 1000, ctoken).ContinueWith(delegate(Task tsk)
				{
					if (!tsk.IsCanceled && !completion.Task.IsCompleted)
					{
						completion.TrySetException(exc());
					}
				});
			}
		}
	}
}
