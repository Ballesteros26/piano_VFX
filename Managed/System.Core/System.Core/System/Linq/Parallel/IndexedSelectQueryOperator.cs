using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C5 RID: 453
	internal sealed class IndexedSelectQueryOperator<TInput, TOutput> : UnaryQueryOperator<TInput, TOutput>
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x000277CD File Offset: 0x000259CD
		internal IndexedSelectQueryOperator(IEnumerable<TInput> child, Func<TInput, int, TOutput> selector)
			: base(child)
		{
			this._selector = selector;
			this._outputOrdered = true;
			this.InitOrdinalIndexState();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000277EC File Offset: 0x000259EC
		private void InitOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			OrdinalIndexState ordinalIndexState2 = ordinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this._prematureMerge = true;
				this._limitsParallelism = ordinalIndexState != OrdinalIndexState.Shuffled;
				ordinalIndexState2 = OrdinalIndexState.Correct;
			}
			base.SetOrdinalIndexState(ordinalIndexState2);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002782D File Offset: 0x00025A2D
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorResults.NewResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00027844 File Offset: 0x00025A44
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInput, int> partitionedStream;
			if (this._prematureMerge)
			{
				partitionedStream = QueryOperator<TInput>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TInput, int>)inputStream;
			}
			PartitionedStream<TOutput, int> partitionedStream2 = new PartitionedStream<TOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorEnumerator(partitionedStream[i], this._selector);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000278C2 File Offset: 0x00025AC2
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000278CA File Offset: 0x00025ACA
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).Select(this._selector);
		}

		// Token: 0x04000726 RID: 1830
		private readonly Func<TInput, int, TOutput> _selector;

		// Token: 0x04000727 RID: 1831
		private bool _prematureMerge;

		// Token: 0x04000728 RID: 1832
		private bool _limitsParallelism;

		// Token: 0x020001C6 RID: 454
		private class IndexedSelectQueryOperatorEnumerator : QueryOperatorEnumerator<TOutput, int>
		{
			// Token: 0x06000BE9 RID: 3049 RVA: 0x000278E3 File Offset: 0x00025AE3
			internal IndexedSelectQueryOperatorEnumerator(QueryOperatorEnumerator<TInput, int> source, Func<TInput, int, TOutput> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x000278FC File Offset: 0x00025AFC
			internal override bool MoveNext(ref TOutput currentElement, ref int currentKey)
			{
				TInput tinput = default(TInput);
				if (this._source.MoveNext(ref tinput, ref currentKey))
				{
					currentElement = this._selector(tinput, currentKey);
					return true;
				}
				return false;
			}

			// Token: 0x06000BEB RID: 3051 RVA: 0x00027938 File Offset: 0x00025B38
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000729 RID: 1833
			private readonly QueryOperatorEnumerator<TInput, int> _source;

			// Token: 0x0400072A RID: 1834
			private readonly Func<TInput, int, TOutput> _selector;
		}

		// Token: 0x020001C7 RID: 455
		private class IndexedSelectQueryOperatorResults : UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults
		{
			// Token: 0x06000BEC RID: 3052 RVA: 0x00027945 File Offset: 0x00025B45
			public static QueryResults<TOutput> NewResults(QueryResults<TInput> childQueryResults, IndexedSelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000BED RID: 3053 RVA: 0x00027962 File Offset: 0x00025B62
			private IndexedSelectQueryOperatorResults(QueryResults<TInput> childQueryResults, IndexedSelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
				: base(childQueryResults, op, settings, preferStriping)
			{
				this._selectOp = op;
				this._childCount = this._childQueryResults.ElementsCount;
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00027987 File Offset: 0x00025B87
			internal override int ElementsCount
			{
				get
				{
					return this._childQueryResults.ElementsCount;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0000AA13 File Offset: 0x00008C13
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x00027994 File Offset: 0x00025B94
			internal override TOutput GetElement(int index)
			{
				return this._selectOp._selector(this._childQueryResults.GetElement(index), index);
			}

			// Token: 0x0400072B RID: 1835
			private IndexedSelectQueryOperator<TInput, TOutput> _selectOp;

			// Token: 0x0400072C RID: 1836
			private int _childCount;
		}
	}
}
