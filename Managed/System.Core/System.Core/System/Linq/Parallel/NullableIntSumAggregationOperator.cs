using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200018C RID: 396
	internal sealed class NullableIntSumAggregationOperator : InlinedAggregationOperator<int?, int?, int?>
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x00024893 File Offset: 0x00022A93
		internal NullableIntSumAggregationOperator(IEnumerable<int?> child)
			: base(child)
		{
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002489C File Offset: 0x00022A9C
		protected override int? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int? num3;
			using (IEnumerator<int?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				int num = 0;
				while (enumerator.MoveNext())
				{
					int num2 = num;
					num3 = enumerator.Current;
					num = checked(num2 + num3.GetValueOrDefault());
				}
				num3 = new int?(num);
			}
			return num3;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000248F8 File Offset: 0x00022AF8
		protected override QueryOperatorEnumerator<int?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntSumAggregationOperator.NullableIntSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200018D RID: 397
		private class NullableIntSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int?>
		{
			// Token: 0x06000ADE RID: 2782 RVA: 0x00024903 File Offset: 0x00022B03
			internal NullableIntSumAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x00024914 File Offset: 0x00022B14
			protected override bool MoveNextCore(ref int? currentElement)
			{
				int? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<int?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					int num2 = 0;
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
					currentElement = new int?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x00024985 File Offset: 0x00022B85
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400068E RID: 1678
			private QueryOperatorEnumerator<int?, TKey> _source;
		}
	}
}
