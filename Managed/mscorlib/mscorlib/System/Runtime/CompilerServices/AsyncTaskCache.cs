using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200083F RID: 2111
	internal static class AsyncTaskCache
	{
		// Token: 0x060053D6 RID: 21462 RVA: 0x00126980 File Offset: 0x00124B80
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x001269B0 File Offset: 0x00124BB0
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x04002B8C RID: 11148
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x04002B8D RID: 11149
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x04002B8E RID: 11150
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x04002B8F RID: 11151
		internal const int INCLUSIVE_INT32_MIN = -1;

		// Token: 0x04002B90 RID: 11152
		internal const int EXCLUSIVE_INT32_MAX = 9;
	}
}
