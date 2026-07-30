using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001F2 RID: 498
	internal class QueryTaskGroupState
	{
		// Token: 0x06000C90 RID: 3216 RVA: 0x0002A052 File Offset: 0x00028252
		internal QueryTaskGroupState(CancellationState cancellationState, int queryId)
		{
			this._cancellationState = cancellationState;
			this._queryId = queryId;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002A068 File Offset: 0x00028268
		internal bool IsAlreadyEnded
		{
			get
			{
				return this._alreadyEnded == 1;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002A073 File Offset: 0x00028273
		internal CancellationState CancellationState
		{
			get
			{
				return this._cancellationState;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002A07B File Offset: 0x0002827B
		internal int QueryId
		{
			get
			{
				return this._queryId;
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002A083 File Offset: 0x00028283
		internal void QueryBegin(Task rootTask)
		{
			this._rootTask = rootTask;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002A08C File Offset: 0x0002828C
		internal void QueryEnd(bool userInitiatedDispose)
		{
			if (Interlocked.Exchange(ref this._alreadyEnded, 1) == 0)
			{
				try
				{
					this._rootTask.Wait();
				}
				catch (AggregateException ex)
				{
					AggregateException ex2 = ex.Flatten();
					bool flag = true;
					for (int i = 0; i < ex2.InnerExceptions.Count; i++)
					{
						OperationCanceledException ex3 = ex2.InnerExceptions[i] as OperationCanceledException;
						if (ex3 == null || !ex3.CancellationToken.IsCancellationRequested || ex3.CancellationToken != this._cancellationState.ExternalCancellationToken)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						throw ex2;
					}
				}
				finally
				{
					IDisposable rootTask = this._rootTask;
					if (rootTask != null)
					{
						rootTask.Dispose();
					}
				}
				if (this._cancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					if (!this._cancellationState.TopLevelDisposedFlag.Value)
					{
						CancellationState.ThrowWithStandardMessageIfCanceled(this._cancellationState.ExternalCancellationToken);
					}
					if (!userInitiatedDispose)
					{
						throw new ObjectDisposedException("enumerator", "The query enumerator has been disposed.");
					}
				}
			}
		}

		// Token: 0x040007CA RID: 1994
		private Task _rootTask;

		// Token: 0x040007CB RID: 1995
		private int _alreadyEnded;

		// Token: 0x040007CC RID: 1996
		private CancellationState _cancellationState;

		// Token: 0x040007CD RID: 1997
		private int _queryId;
	}
}
