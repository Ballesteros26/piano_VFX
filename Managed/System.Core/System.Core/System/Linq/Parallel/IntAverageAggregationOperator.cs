using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000168 RID: 360
	internal sealed class IntAverageAggregationOperator : InlinedAggregationOperator<int, Pair<long, long>, double>
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00022D6A File Offset: 0x00020F6A
		internal IntAverageAggregationOperator(IEnumerable<int> child)
			: base(child)
		{
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00022D74 File Offset: 0x00020F74
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

		// Token: 0x06000A71 RID: 2673 RVA: 0x00022E28 File Offset: 0x00021028
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntAverageAggregationOperator.IntAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000169 RID: 361
		private class IntAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06000A72 RID: 2674 RVA: 0x00022E33 File Offset: 0x00021033
			internal IntAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x00022E44 File Offset: 0x00021044
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<int, TKey> source = this._source;
				int num3 = 0;
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
							num += unchecked((long)num3);
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<long, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x00022EB4 File Offset: 0x000210B4
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000670 RID: 1648
			private QueryOperatorEnumerator<int, TKey> _source;
		}
	}
}
