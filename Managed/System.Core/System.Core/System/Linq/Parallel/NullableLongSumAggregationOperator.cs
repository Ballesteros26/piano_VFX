using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000192 RID: 402
	internal sealed class NullableLongSumAggregationOperator : InlinedAggregationOperator<long?, long?, long?>
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x00024D3B File Offset: 0x00022F3B
		internal NullableLongSumAggregationOperator(IEnumerable<long?> child)
			: base(child)
		{
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00024D44 File Offset: 0x00022F44
		protected override long? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long? num3;
			using (IEnumerator<long?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				long num = 0L;
				while (enumerator.MoveNext())
				{
					long num2 = num;
					num3 = enumerator.Current;
					num = checked(num2 + num3.GetValueOrDefault());
				}
				num3 = new long?(num);
			}
			return num3;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00024DA4 File Offset: 0x00022FA4
		protected override QueryOperatorEnumerator<long?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongSumAggregationOperator.NullableLongSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000193 RID: 403
		private class NullableLongSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long?>
		{
			// Token: 0x06000AF0 RID: 2800 RVA: 0x00024DAF File Offset: 0x00022FAF
			internal NullableLongSumAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x00024DC0 File Offset: 0x00022FC0
			protected override bool MoveNextCore(ref long? currentElement)
			{
				long? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<long?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					long num2 = 0L;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num2 += num.GetValueOrDefault();
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new long?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x00024E32 File Offset: 0x00023032
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000693 RID: 1683
			private readonly QueryOperatorEnumerator<long?, TKey> _source;
		}
	}
}
