using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000176 RID: 374
	internal sealed class NullableDecimalAverageAggregationOperator : InlinedAggregationOperator<decimal?, Pair<decimal, long>, decimal?>
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x000235AB File Offset: 0x000217AB
		internal NullableDecimalAverageAggregationOperator(IEnumerable<decimal?> child)
			: base(child)
		{
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000235B4 File Offset: 0x000217B4
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num;
			using (IEnumerator<Pair<decimal, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					Pair<decimal, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						decimal first = pair.First;
						Pair<decimal, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					num = new decimal?(pair.First / pair.Second);
				}
			}
			return num;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002366C File Offset: 0x0002186C
		protected override QueryOperatorEnumerator<Pair<decimal, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalAverageAggregationOperator.NullableDecimalAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000177 RID: 375
		private class NullableDecimalAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<decimal, long>>
		{
			// Token: 0x06000A9C RID: 2716 RVA: 0x00023677 File Offset: 0x00021877
			internal NullableDecimalAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A9D RID: 2717 RVA: 0x00023688 File Offset: 0x00021888
			protected override bool MoveNextCore(ref Pair<decimal, long> currentElement)
			{
				decimal num = 0.0m;
				long num2 = 0L;
				QueryOperatorEnumerator<decimal?, TKey> source = this._source;
				decimal? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if ((num4++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					checked
					{
						if (num3 != null)
						{
							num += num3.GetValueOrDefault();
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<decimal, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000A9E RID: 2718 RVA: 0x00023711 File Offset: 0x00021911
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400067B RID: 1659
			private QueryOperatorEnumerator<decimal?, TKey> _source;
		}
	}
}
