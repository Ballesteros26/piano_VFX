using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000186 RID: 390
	internal sealed class NullableFloatSumAggregationOperator : InlinedAggregationOperator<float?, double?, float?>
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x000243D6 File Offset: 0x000225D6
		internal NullableFloatSumAggregationOperator(IEnumerable<float?> child)
			: base(child)
		{
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000243E0 File Offset: 0x000225E0
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? num4;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = num;
					double? num3 = enumerator.Current;
					num = num2 + num3.GetValueOrDefault();
				}
				num4 = new float?((float)num);
			}
			return num4;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00024448 File Offset: 0x00022648
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatSumAggregationOperator.NullableFloatSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000187 RID: 391
		private class NullableFloatSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06000ACC RID: 2764 RVA: 0x00024453 File Offset: 0x00022653
			internal NullableFloatSumAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000ACD RID: 2765 RVA: 0x00024464 File Offset: 0x00022664
			protected override bool MoveNextCore(ref double? currentElement)
			{
				float? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<float?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					double num2 = 0.0;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num2 += (double)num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new double?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x000244DE File Offset: 0x000226DE
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000689 RID: 1673
			private readonly QueryOperatorEnumerator<float?, TKey> _source;
		}
	}
}
