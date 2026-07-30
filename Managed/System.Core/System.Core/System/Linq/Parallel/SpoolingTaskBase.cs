using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001FB RID: 507
	internal abstract class SpoolingTaskBase : QueryTask
	{
		// Token: 0x06000CAB RID: 3243 RVA: 0x0002A5F9 File Offset: 0x000287F9
		protected SpoolingTaskBase(int taskIndex, QueryTaskGroupState groupState)
			: base(taskIndex, groupState)
		{
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002A604 File Offset: 0x00028804
		protected override void Work()
		{
			try
			{
				this.SpoolingWork();
			}
			catch (Exception ex)
			{
				OperationCanceledException ex2 = ex as OperationCanceledException;
				if (ex2 == null || !(ex2.CancellationToken == this._groupState.CancellationState.MergedCancellationToken) || !this._groupState.CancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					this._groupState.CancellationState.InternalCancellationTokenSource.Cancel();
					throw;
				}
			}
			finally
			{
				this.SpoolingFinally();
			}
		}

		// Token: 0x06000CAD RID: 3245
		protected abstract void SpoolingWork();

		// Token: 0x06000CAE RID: 3246 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected virtual void SpoolingFinally()
		{
		}
	}
}
