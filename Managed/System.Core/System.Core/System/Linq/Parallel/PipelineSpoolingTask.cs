using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F9 RID: 505
	internal class PipelineSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002A507 File Offset: 0x00028707
		internal PipelineSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, AsynchronousChannel<TInputOutput> destination)
			: base(taskIndex, groupState)
		{
			this._source = source;
			this._destination = destination;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002A520 File Offset: 0x00028720
		protected override void SpoolingWork()
		{
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source = this._source;
			AsynchronousChannel<TInputOutput> destination = this._destination;
			CancellationToken mergedCancellationToken = this._groupState.CancellationState.MergedCancellationToken;
			while (source.MoveNext(ref tinputOutput, ref tignoreKey) && !mergedCancellationToken.IsCancellationRequested)
			{
				destination.Enqueue(tinputOutput);
			}
			destination.FlushBuffers();
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002A581 File Offset: 0x00028781
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			if (this._destination != null)
			{
				this._destination.SetDone();
			}
			this._source.Dispose();
		}

		// Token: 0x040007E1 RID: 2017
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;

		// Token: 0x040007E2 RID: 2018
		private AsynchronousChannel<TInputOutput> _destination;
	}
}
