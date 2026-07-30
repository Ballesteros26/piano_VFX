using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C8 RID: 456
	internal sealed class IndexedWhereQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x000279B3 File Offset: 0x00025BB3
		internal IndexedWhereQueryOperator(IEnumerable<TInputOutput> child, Func<TInputOutput, int, bool> predicate)
			: base(child)
		{
			this._predicate = predicate;
			this._outputOrdered = true;
			this.InitOrdinalIndexState();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000279D0 File Offset: 0x00025BD0
		private void InitOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this._prematureMerge = true;
				this._limitsParallelism = ordinalIndexState != OrdinalIndexState.Shuffled;
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000262B7 File Offset: 0x000244B7
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00027A10 File Offset: 0x00025C10
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInputOutput, int> partitionedStream;
			if (this._prematureMerge)
			{
				partitionedStream = QueryOperator<TInputOutput>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TInputOutput, int>)inputStream;
			}
			PartitionedStream<TInputOutput, int> partitionedStream2 = new PartitionedStream<TInputOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new IndexedWhereQueryOperator<TInputOutput>.IndexedWhereQueryOperatorEnumerator(partitionedStream[i], this._predicate, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00027A9A File Offset: 0x00025C9A
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token).Where(this._predicate);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00027AB9 File Offset: 0x00025CB9
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x0400072D RID: 1837
		private Func<TInputOutput, int, bool> _predicate;

		// Token: 0x0400072E RID: 1838
		private bool _prematureMerge;

		// Token: 0x0400072F RID: 1839
		private bool _limitsParallelism;

		// Token: 0x020001C9 RID: 457
		private class IndexedWhereQueryOperatorEnumerator : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06000BF7 RID: 3063 RVA: 0x00027AC1 File Offset: 0x00025CC1
			internal IndexedWhereQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, int> source, Func<TInputOutput, int, bool> predicate, CancellationToken cancellationToken)
			{
				this._source = source;
				this._predicate = predicate;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x00027AE0 File Offset: 0x00025CE0
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this._outputLoopCount == null)
				{
					this._outputLoopCount = new Shared<int>(0);
				}
				while (this._source.MoveNext(ref currentElement, ref currentKey))
				{
					Shared<int> outputLoopCount = this._outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._predicate(currentElement, currentKey))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x00027B50 File Offset: 0x00025D50
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000730 RID: 1840
			private readonly QueryOperatorEnumerator<TInputOutput, int> _source;

			// Token: 0x04000731 RID: 1841
			private readonly Func<TInputOutput, int, bool> _predicate;

			// Token: 0x04000732 RID: 1842
			private CancellationToken _cancellationToken;

			// Token: 0x04000733 RID: 1843
			private Shared<int> _outputLoopCount;
		}
	}
}
