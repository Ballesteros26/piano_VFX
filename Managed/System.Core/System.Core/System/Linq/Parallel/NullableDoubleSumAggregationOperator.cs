using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000180 RID: 384
	internal sealed class NullableDoubleSumAggregationOperator : InlinedAggregationOperator<double?, double?, double?>
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x00023EB2 File Offset: 0x000220B2
		internal NullableDoubleSumAggregationOperator(IEnumerable<double?> child)
			: base(child)
		{
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00023EBC File Offset: 0x000220BC
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num3;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = num;
					num3 = enumerator.Current;
					num = num2 + num3.GetValueOrDefault();
				}
				num3 = new double?(num);
			}
			return num3;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00023F20 File Offset: 0x00022120
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleSumAggregationOperator.NullableDoubleSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000181 RID: 385
		private class NullableDoubleSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06000ABA RID: 2746 RVA: 0x00023F2B File Offset: 0x0002212B
			internal NullableDoubleSumAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000ABB RID: 2747 RVA: 0x00023F3C File Offset: 0x0002213C
			protected override bool MoveNextCore(ref double? currentElement)
			{
				double? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<double?, TKey> source = this._source;
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
						num2 += num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new double?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000ABC RID: 2748 RVA: 0x00023FB5 File Offset: 0x000221B5
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000684 RID: 1668
			private readonly QueryOperatorEnumerator<double?, TKey> _source;
		}
	}
}
