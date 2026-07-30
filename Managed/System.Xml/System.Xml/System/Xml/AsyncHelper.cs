using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200006E RID: 110
	internal static class AsyncHelper
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000D33E File Offset: 0x0000B53E
		public static bool IsSuccess(this Task task)
		{
			return task.IsCompleted && task.Exception == null;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000D353 File Offset: 0x0000B553
		public static Task CallVoidFuncWhenFinish(this Task task, Action func)
		{
			if (task.IsSuccess())
			{
				func();
				return AsyncHelper.DoneTask;
			}
			return task._CallVoidFuncWhenFinish(func);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000D370 File Offset: 0x0000B570
		private static async Task _CallVoidFuncWhenFinish(this Task task, Action func)
		{
			await task.ConfigureAwait(false);
			func();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000D3BD File Offset: 0x0000B5BD
		public static Task<bool> ReturnTaskBoolWhenFinish(this Task task, bool ret)
		{
			if (!task.IsSuccess())
			{
				return task._ReturnTaskBoolWhenFinish(ret);
			}
			if (ret)
			{
				return AsyncHelper.DoneTaskTrue;
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public static async Task<bool> _ReturnTaskBoolWhenFinish(this Task task, bool ret)
		{
			await task.ConfigureAwait(false);
			return ret;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D42D File Offset: 0x0000B62D
		public static Task CallTaskFuncWhenFinish(this Task task, Func<Task> func)
		{
			if (task.IsSuccess())
			{
				return func();
			}
			return AsyncHelper._CallTaskFuncWhenFinish(task, func);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D448 File Offset: 0x0000B648
		private static async Task _CallTaskFuncWhenFinish(Task task, Func<Task> func)
		{
			await task.ConfigureAwait(false);
			await func().ConfigureAwait(false);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D495 File Offset: 0x0000B695
		public static Task<bool> CallBoolTaskFuncWhenFinish(this Task task, Func<Task<bool>> func)
		{
			if (task.IsSuccess())
			{
				return func();
			}
			return task._CallBoolTaskFuncWhenFinish(func);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		private static async Task<bool> _CallBoolTaskFuncWhenFinish(this Task task, Func<Task<bool>> func)
		{
			await task.ConfigureAwait(false);
			return await func().ConfigureAwait(false);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D4FD File Offset: 0x0000B6FD
		public static Task<bool> ContinueBoolTaskFuncWhenFalse(this Task<bool> task, Func<Task<bool>> func)
		{
			if (!task.IsSuccess())
			{
				return AsyncHelper._ContinueBoolTaskFuncWhenFalse(task, func);
			}
			if (task.Result)
			{
				return AsyncHelper.DoneTaskTrue;
			}
			return func();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D524 File Offset: 0x0000B724
		private static async Task<bool> _ContinueBoolTaskFuncWhenFalse(Task<bool> task, Func<Task<bool>> func)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (configuredTaskAwaiter.GetResult())
			{
				flag = true;
			}
			else
			{
				flag = await func().ConfigureAwait(false);
			}
			return flag;
		}

		// Token: 0x040001F0 RID: 496
		public static readonly Task DoneTask = Task.FromResult<bool>(true);

		// Token: 0x040001F1 RID: 497
		public static readonly Task<bool> DoneTaskTrue = Task.FromResult<bool>(true);

		// Token: 0x040001F2 RID: 498
		public static readonly Task<bool> DoneTaskFalse = Task.FromResult<bool>(false);

		// Token: 0x040001F3 RID: 499
		public static readonly Task<int> DoneTaskZero = Task.FromResult<int>(0);
	}
}
