using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200017C RID: 380
	internal sealed class NullableDoubleAverageAggregationOperator : InlinedAggregationOperator<double?, Pair<double, long>, double?>
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x00023AA0 File Offset: 0x00021CA0
		internal NullableDoubleAverageAggregationOperator(IEnumerable<double?> child)
			: base(child)
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00023AAC File Offset: 0x00021CAC
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					Pair<double, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						double first = pair.First;
						Pair<double, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					num = new double?(pair.First / (double)pair.Second);
				}
			}
			return num;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00023B58 File Offset: 0x00021D58
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleAverageAggregationOperator.NullableDoubleAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200017D RID: 381
		private class NullableDoubleAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06000AAE RID: 2734 RVA: 0x00023B63 File Offset: 0x00021D63
			internal NullableDoubleAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000AAF RID: 2735 RVA: 0x00023B74 File Offset: 0x00021D74
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<double?, TKey> source = this._source;
				double? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if (num3 != null)
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num += num3.GetValueOrDefault();
						checked
						{
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<double, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000AB0 RID: 2736 RVA: 0x00023BF7 File Offset: 0x00021DF7
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000680 RID: 1664
			private QueryOperatorEnumerator<double?, TKey> _source;
		}
	}
}
