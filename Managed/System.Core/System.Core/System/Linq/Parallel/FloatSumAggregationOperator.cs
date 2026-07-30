using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000164 RID: 356
	internal sealed class FloatSumAggregationOperator : InlinedAggregationOperator<float, double, float>
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x00022B45 File Offset: 0x00020D45
		internal FloatSumAggregationOperator(IEnumerable<float> child)
			: base(child)
		{
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00022B50 File Offset: 0x00020D50
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float num3;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					num += num2;
				}
				num3 = (float)num;
			}
			return num3;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00022BA8 File Offset: 0x00020DA8
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatSumAggregationOperator.FloatSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000165 RID: 357
		private class FloatSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06000A61 RID: 2657 RVA: 0x00022BB3 File Offset: 0x00020DB3
			internal FloatSumAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x00022BC4 File Offset: 0x00020DC4
			protected override bool MoveNextCore(ref double currentElement)
			{
				float num = 0f;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<float, TKey> source = this._source;
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
						num2 += (double)num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000A63 RID: 2659 RVA: 0x00022C2D File Offset: 0x00020E2D
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400066C RID: 1644
			private readonly QueryOperatorEnumerator<float, TKey> _source;
		}
	}
}
