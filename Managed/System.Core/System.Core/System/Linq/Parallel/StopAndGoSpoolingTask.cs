using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F8 RID: 504
	internal class StopAndGoSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002A467 File Offset: 0x00028667
		internal StopAndGoSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, SynchronousChannel<TInputOutput> destination)
			: base(taskIndex, groupState)
		{
			this._source = source;
			this._destination = destination;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002A480 File Offset: 0x00028680
		protected override void SpoolingWork()
		{
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source = this._source;
			SynchronousChannel<TInputOutput> destination = this._destination;
			CancellationToken mergedCancellationToken = this._groupState.CancellationState.MergedCancellationToken;
			destination.Init();
			while (source.MoveNext(ref tinputOutput, ref tignoreKey) && !mergedCancellationToken.IsCancellationRequested)
			{
				destination.Enqueue(tinputOutput);
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002A4E1 File Offset: 0x000286E1
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			if (this._destination != null)
			{
				this._destination.SetDone();
			}
			this._source.Dispose();
		}

		// Token: 0x040007DF RID: 2015
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;

		// Token: 0x040007E0 RID: 2016
		private SynchronousChannel<TInputOutput> _destination;
	}
}
