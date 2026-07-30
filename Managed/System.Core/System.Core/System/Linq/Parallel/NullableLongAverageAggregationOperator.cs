using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200018E RID: 398
	internal sealed class NullableLongAverageAggregationOperator : InlinedAggregationOperator<long?, Pair<long, long>, double?>
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00024992 File Offset: 0x00022B92
		internal NullableLongAverageAggregationOperator(IEnumerable<long?> child)
			: base(child)
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002499C File Offset: 0x00022B9C
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				double? num;
				using (IEnumerator<Pair<long, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					if (!enumerator.MoveNext())
					{
						num = null;
						num = num;
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
						num = new double?((double)pair.First / (double)pair.Second);
					}
				}
				return num;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00024A48 File Offset: 0x00022C48
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongAverageAggregationOperator.NullableLongAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200018F RID: 399
		private class NullableLongAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06000AE4 RID: 2788 RVA: 0x00024A53 File Offset: 0x00022C53
			internal NullableLongAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x00024A64 File Offset: 0x00022C64
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<long?, TKey> source = this._source;
				long? num3 = null;
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
				currentElement = new Pair<long, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x00024AE0 File Offset: 0x00022CE0
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400068F RID: 1679
			private QueryOperatorEnumerator<long?, TKey> _source;
		}
	}
}
