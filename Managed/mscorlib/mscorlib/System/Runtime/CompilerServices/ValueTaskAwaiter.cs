using System;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000835 RID: 2101
	public struct ValueTaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600539E RID: 21406 RVA: 0x00125BAB File Offset: 0x00123DAB
		internal ValueTaskAwaiter(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x00125BB4 File Offset: 0x00123DB4
		public bool IsCompleted
		{
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00125BD0 File Offset: 0x00123DD0
		public TResult GetResult()
		{
			if (this._value._task != null)
			{
				return this._value._task.GetAwaiter().GetResult();
			}
			return this._value._result;
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x00125C10 File Offset: 0x00123E10
		public void OnCompleted(Action continuation)
		{
			this._value.AsTask().ConfigureAwait(true).GetAwaiter()
				.OnCompleted(continuation);
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00125C44 File Offset: 0x00123E44
		public void UnsafeOnCompleted(Action continuation)
		{
			this._value.AsTask().ConfigureAwait(true).GetAwaiter()
				.UnsafeOnCompleted(continuation);
		}

		// Token: 0x04002B7E RID: 11134
		private readonly ValueTask<TResult> _value;
	}
}
