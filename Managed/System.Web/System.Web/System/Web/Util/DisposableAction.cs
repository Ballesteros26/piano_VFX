using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x02000114 RID: 276
	internal sealed class DisposableAction : IDisposable
	{
		// Token: 0x06000DFA RID: 3578 RVA: 0x000261EB File Offset: 0x000243EB
		public DisposableAction(Action disposeAction)
		{
			this._disposeAction = disposeAction;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000261FC File Offset: 0x000243FC
		public void Dispose()
		{
			Action action = Interlocked.Exchange<Action>(ref this._disposeAction, null);
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x040011AF RID: 4527
		public static readonly DisposableAction Empty = new DisposableAction(null);

		// Token: 0x040011B0 RID: 4528
		private Action _disposeAction;
	}
}
