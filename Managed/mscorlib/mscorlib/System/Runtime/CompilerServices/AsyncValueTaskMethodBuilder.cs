using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000832 RID: 2098
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder<TResult>
	{
		// Token: 0x0600538F RID: 21391 RVA: 0x001259E0 File Offset: 0x00123BE0
		public static AsyncValueTaskMethodBuilder<TResult> Create()
		{
			return new AsyncValueTaskMethodBuilder<TResult>
			{
				_methodBuilder = AsyncTaskMethodBuilder<TResult>.Create()
			};
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00125A02 File Offset: 0x00123C02
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00125A10 File Offset: 0x00123C10
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00125A1E File Offset: 0x00123C1E
		public void SetResult(TResult result)
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult(result);
				return;
			}
			this._result = result;
			this._haveResult = true;
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x00125A43 File Offset: 0x00123C43
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x00125A51 File Offset: 0x00123C51
		public ValueTask<TResult> Task
		{
			get
			{
				if (this._haveResult)
				{
					return new ValueTask<TResult>(this._result);
				}
				this._useBuilder = true;
				return new ValueTask<TResult>(this._methodBuilder.Task);
			}
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x00125A7E File Offset: 0x00123C7E
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00125A94 File Offset: 0x00123C94
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x04002B76 RID: 11126
		private AsyncTaskMethodBuilder<TResult> _methodBuilder;

		// Token: 0x04002B77 RID: 11127
		private TResult _result;

		// Token: 0x04002B78 RID: 11128
		private bool _haveResult;

		// Token: 0x04002B79 RID: 11129
		private bool _useBuilder;
	}
}
