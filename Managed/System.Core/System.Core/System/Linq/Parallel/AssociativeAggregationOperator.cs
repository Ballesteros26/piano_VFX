using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000137 RID: 311
	internal sealed class AssociativeAggregationOperator<TInput, TIntermediate, TOutput> : UnaryQueryOperator<TInput, TIntermediate>
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0001FC0E File Offset: 0x0001DE0E
		internal AssociativeAggregationOperator(IEnumerable<TInput> child, TIntermediate seed, Func<TIntermediate> seedFactory, bool seedIsSpecified, Func<TIntermediate, TInput, TIntermediate> intermediateReduce, Func<TIntermediate, TIntermediate, TIntermediate> finalReduce, Func<TIntermediate, TOutput> resultSelector, bool throwIfEmpty, QueryAggregationOptions options)
			: base(child)
		{
			this._seed = seed;
			this._seedFactory = seedFactory;
			this._seedIsSpecified = seedIsSpecified;
			this._intermediateReduce = intermediateReduce;
			this._finalReduce = finalReduce;
			this._resultSelector = resultSelector;
			this._throwIfEmpty = throwIfEmpty;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001FC50 File Offset: 0x0001DE50
		internal TOutput Aggregate()
		{
			TIntermediate tintermediate = default(TIntermediate);
			bool flag = false;
			using (IEnumerator<TIntermediate> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (flag)
					{
						try
						{
							tintermediate = this._finalReduce(tintermediate, enumerator.Current);
							continue;
						}
						catch (Exception ex)
						{
							throw new AggregateException(new Exception[] { ex });
						}
					}
					tintermediate = enumerator.Current;
					flag = true;
				}
				if (!flag)
				{
					if (this._throwIfEmpty)
					{
						throw new InvalidOperationException("Sequence contains no elements");
					}
					tintermediate = ((this._seedFactory == null) ? this._seed : this._seedFactory());
				}
			}
			TOutput toutput;
			try
			{
				toutput = this._resultSelector(tintermediate);
			}
			catch (Exception ex2)
			{
				throw new AggregateException(new Exception[] { ex2 });
			}
			return toutput;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001FD40 File Offset: 0x0001DF40
		internal override QueryResults<TIntermediate> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, TIntermediate>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001FD58 File Offset: 0x0001DF58
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TIntermediate> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TIntermediate, int> partitionedStream = new PartitionedStream<TIntermediate, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new AssociativeAggregationOperator<TInput, TIntermediate, TOutput>.AssociativeAggregationOperatorEnumerator<TKey>(inputStream[i], this, i, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TIntermediate> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040005E2 RID: 1506
		private readonly TIntermediate _seed;

		// Token: 0x040005E3 RID: 1507
		private readonly bool _seedIsSpecified;

		// Token: 0x040005E4 RID: 1508
		private readonly bool _throwIfEmpty;

		// Token: 0x040005E5 RID: 1509
		private Func<TIntermediate, TInput, TIntermediate> _intermediateReduce;

		// Token: 0x040005E6 RID: 1510
		private Func<TIntermediate, TIntermediate, TIntermediate> _finalReduce;

		// Token: 0x040005E7 RID: 1511
		private Func<TIntermediate, TOutput> _resultSelector;

		// Token: 0x040005E8 RID: 1512
		private Func<TIntermediate> _seedFactory;

		// Token: 0x02000138 RID: 312
		private class AssociativeAggregationOperatorEnumerator<TKey> : QueryOperatorEnumerator<TIntermediate, int>
		{
			// Token: 0x060009B9 RID: 2489 RVA: 0x0001FDAD File Offset: 0x0001DFAD
			internal AssociativeAggregationOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, AssociativeAggregationOperator<TInput, TIntermediate, TOutput> reduceOperator, int partitionIndex, CancellationToken cancellationToken)
			{
				this._source = source;
				this._reduceOperator = reduceOperator;
				this._partitionIndex = partitionIndex;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
			internal override bool MoveNext(ref TIntermediate currentElement, ref int currentKey)
			{
				if (this._accumulated)
				{
					return false;
				}
				this._accumulated = true;
				bool flag = false;
				TIntermediate tintermediate = default(TIntermediate);
				if (this._reduceOperator._seedIsSpecified)
				{
					tintermediate = ((this._reduceOperator._seedFactory == null) ? this._reduceOperator._seed : this._reduceOperator._seedFactory());
				}
				else
				{
					TInput tinput = default(TInput);
					TKey tkey = default(TKey);
					if (!this._source.MoveNext(ref tinput, ref tkey))
					{
						return false;
					}
					flag = true;
					tintermediate = (TIntermediate)((object)tinput);
				}
				TInput tinput2 = default(TInput);
				TKey tkey2 = default(TKey);
				int num = 0;
				while (this._source.MoveNext(ref tinput2, ref tkey2))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					flag = true;
					tintermediate = this._reduceOperator._intermediateReduce(tintermediate, tinput2);
				}
				if (flag)
				{
					currentElement = tintermediate;
					currentKey = this._partitionIndex;
					return true;
				}
				return false;
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x0001FECF File Offset: 0x0001E0CF
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040005E9 RID: 1513
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x040005EA RID: 1514
			private readonly AssociativeAggregationOperator<TInput, TIntermediate, TOutput> _reduceOperator;

			// Token: 0x040005EB RID: 1515
			private readonly int _partitionIndex;

			// Token: 0x040005EC RID: 1516
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040005ED RID: 1517
			private bool _accumulated;
		}
	}
}
