using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000158 RID: 344
	internal sealed class DecimalSumAggregationOperator : InlinedAggregationOperator<decimal, decimal, decimal>
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00022331 File Offset: 0x00020531
		internal DecimalSumAggregationOperator(IEnumerable<decimal> child)
			: base(child)
		{
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002233C File Offset: 0x0002053C
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal num3;
			using (IEnumerator<decimal> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				decimal num = 0.0m;
				while (enumerator.MoveNext())
				{
					decimal num2 = enumerator.Current;
					num += num2;
				}
				num3 = num;
			}
			return num3;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002239C File Offset: 0x0002059C
		protected override QueryOperatorEnumerator<decimal, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalSumAggregationOperator.DecimalSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000159 RID: 345
		private class DecimalSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal>
		{
			// Token: 0x06000A3D RID: 2621 RVA: 0x000223A7 File Offset: 0x000205A7
			internal DecimalSumAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A3E RID: 2622 RVA: 0x000223B8 File Offset: 0x000205B8
			protected override bool MoveNextCore(ref decimal currentElement)
			{
				decimal num = 0m;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<decimal, TKey> source = this._source;
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
						num2 += num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000A3F RID: 2623 RVA: 0x0002242C File Offset: 0x0002062C
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000662 RID: 1634
			private QueryOperatorEnumerator<decimal, TKey> _source;
		}
	}
}
