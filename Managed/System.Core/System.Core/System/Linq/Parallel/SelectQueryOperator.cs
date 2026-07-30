using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D5 RID: 469
	internal sealed class SelectQueryOperator<TInput, TOutput> : UnaryQueryOperator<TInput, TOutput>
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0002865A File Offset: 0x0002685A
		internal SelectQueryOperator(IEnumerable<TInput> child, Func<TInput, TOutput> selector)
			: base(child)
		{
			this._selector = selector;
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002867C File Offset: 0x0002687C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TOutput, TKey> partitionedStream = new PartitionedStream<TOutput, TKey>(inputStream.PartitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorEnumerator<TKey>(inputStream[i], this._selector);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000286D2 File Offset: 0x000268D2
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorResults.NewResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000286E9 File Offset: 0x000268E9
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).Select(this._selector);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400075E RID: 1886
		private Func<TInput, TOutput> _selector;

		// Token: 0x020001D6 RID: 470
		private class SelectQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TOutput, TKey>
		{
			// Token: 0x06000C26 RID: 3110 RVA: 0x00028702 File Offset: 0x00026902
			internal SelectQueryOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, Func<TInput, TOutput> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000C27 RID: 3111 RVA: 0x00028718 File Offset: 0x00026918
			internal override bool MoveNext(ref TOutput currentElement, ref TKey currentKey)
			{
				TInput tinput = default(TInput);
				if (this._source.MoveNext(ref tinput, ref currentKey))
				{
					currentElement = this._selector(tinput);
					return true;
				}
				return false;
			}

			// Token: 0x06000C28 RID: 3112 RVA: 0x00028752 File Offset: 0x00026952
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400075F RID: 1887
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x04000760 RID: 1888
			private readonly Func<TInput, TOutput> _selector;
		}

		// Token: 0x020001D7 RID: 471
		private class SelectQueryOperatorResults : UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults
		{
			// Token: 0x06000C29 RID: 3113 RVA: 0x0002875F File Offset: 0x0002695F
			public static QueryResults<TOutput> NewResults(QueryResults<TInput> childQueryResults, SelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x0002877C File Offset: 0x0002697C
			private SelectQueryOperatorResults(QueryResults<TInput> childQueryResults, SelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
				: base(childQueryResults, op, settings, preferStriping)
			{
				this._selector = op._selector;
				this._childCount = this._childQueryResults.ElementsCount;
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0000AA13 File Offset: 0x00008C13
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000287A6 File Offset: 0x000269A6
			internal override int ElementsCount
			{
				get
				{
					return this._childCount;
				}
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x000287AE File Offset: 0x000269AE
			internal override TOutput GetElement(int index)
			{
				return this._selector(this._childQueryResults.GetElement(index));
			}

			// Token: 0x04000761 RID: 1889
			private Func<TInput, TOutput> _selector;

			// Token: 0x04000762 RID: 1890
			private int _childCount;
		}
	}
}
