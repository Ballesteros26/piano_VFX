using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200017A RID: 378
	internal sealed class NullableDecimalSumAggregationOperator : InlinedAggregationOperator<decimal?, decimal?, decimal?>
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x00023981 File Offset: 0x00021B81
		internal NullableDecimalSumAggregationOperator(IEnumerable<decimal?> child)
			: base(child)
		{
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002398C File Offset: 0x00021B8C
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num3;
			using (IEnumerator<decimal?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				decimal num = 0.0m;
				while (enumerator.MoveNext())
				{
					decimal num2 = num;
					num3 = enumerator.Current;
					num = num2 + num3.GetValueOrDefault();
				}
				num3 = new decimal?(num);
			}
			return num3;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000239F8 File Offset: 0x00021BF8
		protected override QueryOperatorEnumerator<decimal?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalSumAggregationOperator.NullableDecimalSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200017B RID: 379
		private class NullableDecimalSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal?>
		{
			// Token: 0x06000AA8 RID: 2728 RVA: 0x00023A03 File Offset: 0x00021C03
			internal NullableDecimalSumAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000AA9 RID: 2729 RVA: 0x00023A14 File Offset: 0x00021C14
			protected override bool MoveNextCore(ref decimal? currentElement)
			{
				decimal? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<decimal?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					decimal num2 = 0.0m;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num2 += num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new decimal?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000AAA RID: 2730 RVA: 0x00023A93 File Offset: 0x00021C93
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400067F RID: 1663
			private readonly QueryOperatorEnumerator<decimal?, TKey> _source;
		}
	}
}
