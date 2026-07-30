using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x0200010F RID: 271
	internal sealed class CancellationTokenHelper : IDisposable
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00025F5F File Offset: 0x0002415F
		public CancellationTokenHelper(bool canceled)
		{
			if (canceled)
			{
				this._cts.Cancel();
			}
			this._state = (canceled ? 2 : 0);
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00025F8D File Offset: 0x0002418D
		internal bool IsCancellationRequested
		{
			get
			{
				return this._cts.IsCancellationRequested;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00025F9A File Offset: 0x0002419A
		internal CancellationToken Token
		{
			get
			{
				return this._cts.Token;
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00025FA7 File Offset: 0x000241A7
		public void Cancel()
		{
			if (Interlocked.CompareExchange(ref this._state, 1, 0) == 0)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
				{
					try
					{
						this._cts.Cancel();
					}
					catch
					{
					}
					finally
					{
						if (Interlocked.CompareExchange(ref this._state, 2, 1) == 3)
						{
							this._cts.Dispose();
							Interlocked.Exchange(ref this._state, 4);
						}
					}
				}, null);
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00025FCC File Offset: 0x000241CC
		public void Dispose()
		{
			switch (Interlocked.Exchange(ref this._state, 3))
			{
			case 0:
			case 2:
				this._cts.Dispose();
				Interlocked.Exchange(ref this._state, 4);
				return;
			case 1:
			case 3:
				break;
			case 4:
				Interlocked.Exchange(ref this._state, 4);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00026027 File Offset: 0x00024227
		private static CancellationTokenHelper GetStaticDisposedHelper()
		{
			CancellationTokenHelper cancellationTokenHelper = new CancellationTokenHelper(false);
			cancellationTokenHelper.Dispose();
			return cancellationTokenHelper;
		}

		// Token: 0x04001198 RID: 4504
		private const int STATE_CREATED = 0;

		// Token: 0x04001199 RID: 4505
		private const int STATE_CANCELING = 1;

		// Token: 0x0400119A RID: 4506
		private const int STATE_CANCELED = 2;

		// Token: 0x0400119B RID: 4507
		private const int STATE_DISPOSING = 3;

		// Token: 0x0400119C RID: 4508
		private const int STATE_DISPOSED = 4;

		// Token: 0x0400119D RID: 4509
		internal static readonly CancellationTokenHelper StaticDisposed = CancellationTokenHelper.GetStaticDisposedHelper();

		// Token: 0x0400119E RID: 4510
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		// Token: 0x0400119F RID: 4511
		private int _state;
	}
}
