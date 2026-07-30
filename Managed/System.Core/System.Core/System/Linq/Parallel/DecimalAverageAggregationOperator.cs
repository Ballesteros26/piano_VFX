using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000154 RID: 340
	internal sealed class DecimalAverageAggregationOperator : InlinedAggregationOperator<decimal, Pair<decimal, long>, decimal>
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x00022017 File Offset: 0x00020217
		internal DecimalAverageAggregationOperator(IEnumerable<decimal> child)
			: base(child)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00022020 File Offset: 0x00020220
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal num;
			using (IEnumerator<Pair<decimal, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0m;
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
					num = pair.First / pair.Second;
				}
			}
			return num;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000220DC File Offset: 0x000202DC
		protected override QueryOperatorEnumerator<Pair<decimal, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalAverageAggregationOperator.DecimalAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000155 RID: 341
		private class DecimalAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<decimal, long>>
		{
			// Token: 0x06000A31 RID: 2609 RVA: 0x000220E7 File Offset: 0x000202E7
			internal DecimalAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x000220F8 File Offset: 0x000202F8
			protected override bool MoveNextCore(ref Pair<decimal, long> currentElement)
			{
				decimal num = 0.0m;
				long num2 = 0L;
				QueryOperatorEnumerator<decimal, TKey> source = this._source;
				decimal num3 = 0m;
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
						num += num3;
						checked
						{
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<decimal, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x0002217A File Offset: 0x0002037A
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400065E RID: 1630
			private QueryOperatorEnumerator<decimal, TKey> _source;
		}
	}
}
