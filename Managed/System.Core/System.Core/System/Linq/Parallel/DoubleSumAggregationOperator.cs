using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200015E RID: 350
	internal sealed class DoubleSumAggregationOperator : InlinedAggregationOperator<double, double, double>
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0002274D File Offset: 0x0002094D
		internal DoubleSumAggregationOperator(IEnumerable<double> child)
			: base(child)
		{
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00022758 File Offset: 0x00020958
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double num3;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					num += num2;
				}
				num3 = num;
			}
			return num3;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000227B0 File Offset: 0x000209B0
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleSumAggregationOperator.DoubleSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200015F RID: 351
		private class DoubleSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06000A4F RID: 2639 RVA: 0x000227BB File Offset: 0x000209BB
			internal DoubleSumAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x000227CC File Offset: 0x000209CC
			protected override bool MoveNextCore(ref double currentElement)
			{
				double num = 0.0;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<double, TKey> source = this._source;
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
						num2 += num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000A51 RID: 2641 RVA: 0x00022838 File Offset: 0x00020A38
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000667 RID: 1639
			private readonly QueryOperatorEnumerator<double, TKey> _source;
		}
	}
}
