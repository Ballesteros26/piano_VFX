using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200016E RID: 366
	internal sealed class LongAverageAggregationOperator : InlinedAggregationOperator<long, Pair<long, long>, double>
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00023115 File Offset: 0x00021315
		internal LongAverageAggregationOperator(IEnumerable<long> child)
			: base(child)
		{
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00023120 File Offset: 0x00021320
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				double num;
				using (IEnumerator<Pair<long, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					if (!enumerator.MoveNext())
					{
						singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
						num = 0.0;
					}
					else
					{
						Pair<long, long> pair = enumerator.Current;
						while (enumerator.MoveNext())
						{
							long first = pair.First;
							Pair<long, long> pair2 = enumerator.Current;
							pair.First = first + pair2.First;
							long second = pair.Second;
							pair2 = enumerator.Current;
							pair.Second = second + pair2.Second;
						}
						num = (double)pair.First / (double)pair.Second;
					}
				}
				return num;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000231D4 File Offset: 0x000213D4
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongAverageAggregationOperator.LongAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200016F RID: 367
		private class LongAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06000A84 RID: 2692 RVA: 0x000231DF File Offset: 0x000213DF
			internal LongAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A85 RID: 2693 RVA: 0x000231F0 File Offset: 0x000213F0
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<long, TKey> source = this._source;
				long num3 = 0L;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref num3, ref tkey))
				{
					int num4 = 0;
					do
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num += num3;
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<long, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A86 RID: 2694 RVA: 0x00023260 File Offset: 0x00021460
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000675 RID: 1653
			private QueryOperatorEnumerator<long, TKey> _source;
		}
	}
}
