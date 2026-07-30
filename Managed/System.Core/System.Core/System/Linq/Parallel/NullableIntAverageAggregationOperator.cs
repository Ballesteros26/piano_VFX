using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000188 RID: 392
	internal sealed class NullableIntAverageAggregationOperator : InlinedAggregationOperator<int?, Pair<long, long>, double?>
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x000244EB File Offset: 0x000226EB
		internal NullableIntAverageAggregationOperator(IEnumerable<int?> child)
			: base(child)
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000244F4 File Offset: 0x000226F4
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

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000245A0 File Offset: 0x000227A0
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntAverageAggregationOperator.NullableIntAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000189 RID: 393
		private class NullableIntAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06000AD2 RID: 2770 RVA: 0x000245AB File Offset: 0x000227AB
			internal NullableIntAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000AD3 RID: 2771 RVA: 0x000245BC File Offset: 0x000227BC
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<int?, TKey> source = this._source;
				int? num3 = null;
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
							num += unchecked((long)num3.GetValueOrDefault());
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<long, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x00024639 File Offset: 0x00022839
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400068A RID: 1674
			private QueryOperatorEnumerator<int?, TKey> _source;
		}
	}
}
