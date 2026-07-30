using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000833 RID: 2099
	[StructLayout(LayoutKind.Auto)]
	public struct ConfiguredValueTaskAwaitable<TResult>
	{
		// Token: 0x06005397 RID: 21399 RVA: 0x00125AAA File Offset: 0x00123CAA
		internal ConfiguredValueTaskAwaitable(ValueTask<TResult> value, bool continueOnCapturedContext)
		{
			this._value = value;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x00125ABA File Offset: 0x00123CBA
		public ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter(this._value, this._continueOnCapturedContext);
		}

		// Token: 0x04002B7A RID: 11130
		private readonly ValueTask<TResult> _value;

		// Token: 0x04002B7B RID: 11131
		private readonly bool _continueOnCapturedContext;

		// Token: 0x02000834 RID: 2100
		[StructLayout(LayoutKind.Auto)]
		public struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06005399 RID: 21401 RVA: 0x00125ACD File Offset: 0x00123CCD
			internal ConfiguredValueTaskAwaiter(ValueTask<TResult> value, bool continueOnCapturedContext)
			{
				this._value = value;
				this._continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17000E9F RID: 3743
			// (get) Token: 0x0600539A RID: 21402 RVA: 0x00125AE0 File Offset: 0x00123CE0
			public bool IsCompleted
			{
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x0600539B RID: 21403 RVA: 0x00125AFC File Offset: 0x00123CFC
			public TResult GetResult()
			{
				if (this._value._task != null)
				{
					return this._value._task.GetAwaiter().GetResult();
				}
				return this._value._result;
			}

			// Token: 0x0600539C RID: 21404 RVA: 0x00125B3C File Offset: 0x00123D3C
			public void OnCompleted(Action continuation)
			{
				this._value.AsTask().ConfigureAwait(this._continueOnCapturedContext).GetAwaiter()
					.OnCompleted(continuation);
			}

			// Token: 0x0600539D RID: 21405 RVA: 0x00125B74 File Offset: 0x00123D74
			public void UnsafeOnCompleted(Action continuation)
			{
				this._value.AsTask().ConfigureAwait(this._continueOnCapturedContext).GetAwaiter()
					.UnsafeOnCompleted(continuation);
			}

			// Token: 0x04002B7C RID: 11132
			private readonly ValueTask<TResult> _value;

			// Token: 0x04002B7D RID: 11133
			private readonly bool _continueOnCapturedContext;
		}
	}
}
