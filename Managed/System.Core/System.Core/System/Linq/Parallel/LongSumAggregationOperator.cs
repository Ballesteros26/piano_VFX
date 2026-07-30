using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000174 RID: 372
	internal sealed class LongSumAggregationOperator : InlinedAggregationOperator<long, long, long>
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x000234CB File Offset: 0x000216CB
		internal LongSumAggregationOperator(IEnumerable<long> child)
			: base(child)
		{
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000234D4 File Offset: 0x000216D4
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				long num3;
				using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					long num = 0L;
					while (enumerator.MoveNext())
					{
						long num2 = enumerator.Current;
						num += num2;
					}
					num3 = num;
				}
				return num3;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00023524 File Offset: 0x00021724
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongSumAggregationOperator.LongSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000175 RID: 373
		private class LongSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x06000A96 RID: 2710 RVA: 0x0002352F File Offset: 0x0002172F
			internal LongSumAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x00023540 File Offset: 0x00021740
			protected override bool MoveNextCore(ref long currentElement)
			{
				long num = 0L;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<long, TKey> source = this._source;
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
							num2 += num;
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x0002359E File Offset: 0x0002179E
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400067A RID: 1658
			private readonly QueryOperatorEnumerator<long, TKey> _source;
		}
	}
}
