using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x020004BB RID: 1211
	internal sealed class BeginEndAwaitableAdapter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06003884 RID: 14468 RVA: 0x00002119 File Offset: 0x00000319
		public BeginEndAwaitableAdapter GetAwaiter()
		{
			return this;
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x000CC7BA File Offset: 0x000CA9BA
		public bool IsCompleted
		{
			get
			{
				return this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN;
			}
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000CC7CC File Offset: 0x000CA9CC
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			this.OnCompleted(continuation);
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000CC7D5 File Offset: 0x000CA9D5
		public void OnCompleted(Action continuation)
		{
			if (this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN || Interlocked.CompareExchange<Action>(ref this._continuation, continuation, null) == BeginEndAwaitableAdapter.CALLBACK_RAN)
			{
				Task.Run(continuation);
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000CC809 File Offset: 0x000CAA09
		public IAsyncResult GetResult()
		{
			IAsyncResult asyncResult = this._asyncResult;
			this._asyncResult = null;
			this._continuation = null;
			return asyncResult;
		}

		// Token: 0x04001DA3 RID: 7587
		private static readonly Action CALLBACK_RAN = delegate
		{
		};

		// Token: 0x04001DA4 RID: 7588
		private IAsyncResult _asyncResult;

		// Token: 0x04001DA5 RID: 7589
		private Action _continuation;

		// Token: 0x04001DA6 RID: 7590
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			BeginEndAwaitableAdapter beginEndAwaitableAdapter = (BeginEndAwaitableAdapter)asyncResult.AsyncState;
			beginEndAwaitableAdapter._asyncResult = asyncResult;
			Action action = Interlocked.Exchange<Action>(ref beginEndAwaitableAdapter._continuation, BeginEndAwaitableAdapter.CALLBACK_RAN);
			if (action != null)
			{
				action();
			}
		};
	}
}
