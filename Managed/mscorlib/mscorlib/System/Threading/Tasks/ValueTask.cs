using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x020004B5 RID: 1205
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder<>))]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTask<TResult> : IEquatable<ValueTask<TResult>>
	{
		// Token: 0x0600386B RID: 14443 RVA: 0x000CC583 File Offset: 0x000CA783
		public ValueTask(TResult result)
		{
			this._task = null;
			this._result = result;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000CC593 File Offset: 0x000CA793
		public ValueTask(Task<TResult> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			this._task = task;
			this._result = default(TResult);
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000CC5B8 File Offset: 0x000CA7B8
		public override int GetHashCode()
		{
			if (this._task != null)
			{
				return this._task.GetHashCode();
			}
			if (this._result == null)
			{
				return 0;
			}
			TResult result = this._result;
			return result.GetHashCode();
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000CC5FC File Offset: 0x000CA7FC
		public override bool Equals(object obj)
		{
			return obj is ValueTask<TResult> && this.Equals((ValueTask<TResult>)obj);
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000CC614 File Offset: 0x000CA814
		public bool Equals(ValueTask<TResult> other)
		{
			if (this._task == null && other._task == null)
			{
				return EqualityComparer<TResult>.Default.Equals(this._result, other._result);
			}
			return this._task == other._task;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000CC64B File Offset: 0x000CA84B
		public static bool operator ==(ValueTask<TResult> left, ValueTask<TResult> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000CC655 File Offset: 0x000CA855
		public static bool operator !=(ValueTask<TResult> left, ValueTask<TResult> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000CC662 File Offset: 0x000CA862
		public Task<TResult> AsTask()
		{
			return this._task ?? Task.FromResult<TResult>(this._result);
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000CC679 File Offset: 0x000CA879
		public bool IsCompleted
		{
			get
			{
				return this._task == null || this._task.IsCompleted;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000CC690 File Offset: 0x000CA890
		public bool IsCompletedSuccessfully
		{
			get
			{
				return this._task == null || this._task.Status == TaskStatus.RanToCompletion;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000CC6AA File Offset: 0x000CA8AA
		public bool IsFaulted
		{
			get
			{
				return this._task != null && this._task.IsFaulted;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000CC6C1 File Offset: 0x000CA8C1
		public bool IsCanceled
		{
			get
			{
				return this._task != null && this._task.IsCanceled;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000CC6D8 File Offset: 0x000CA8D8
		public TResult Result
		{
			get
			{
				if (this._task != null)
				{
					return this._task.GetAwaiter().GetResult();
				}
				return this._result;
			}
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000CC707 File Offset: 0x000CA907
		public ValueTaskAwaiter<TResult> GetAwaiter()
		{
			return new ValueTaskAwaiter<TResult>(this);
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000CC714 File Offset: 0x000CA914
		public ConfiguredValueTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable<TResult>(this, continueOnCapturedContext);
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000CC724 File Offset: 0x000CA924
		public override string ToString()
		{
			if (this._task != null)
			{
				if (this._task.Status != TaskStatus.RanToCompletion || this._task.Result == null)
				{
					return string.Empty;
				}
				TResult tresult = this._task.Result;
				return tresult.ToString();
			}
			else
			{
				if (this._result == null)
				{
					return string.Empty;
				}
				TResult tresult = this._result;
				return tresult.ToString();
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000CC7A0 File Offset: 0x000CA9A0
		public static AsyncValueTaskMethodBuilder<TResult> CreateAsyncMethodBuilder()
		{
			return AsyncValueTaskMethodBuilder<TResult>.Create();
		}

		// Token: 0x04001D8E RID: 7566
		internal readonly Task<TResult> _task;

		// Token: 0x04001D8F RID: 7567
		internal readonly TResult _result;
	}
}
