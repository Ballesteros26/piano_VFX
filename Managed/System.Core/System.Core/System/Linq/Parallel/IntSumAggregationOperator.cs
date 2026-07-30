using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200016C RID: 364
	internal sealed class IntSumAggregationOperator : InlinedAggregationOperator<int, int, int>
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x00023035 File Offset: 0x00021235
		internal IntSumAggregationOperator(IEnumerable<int> child)
			: base(child)
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00023040 File Offset: 0x00021240
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				int num3;
				using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						num += num2;
					}
					num3 = num;
				}
				return num3;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00023090 File Offset: 0x00021290
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntSumAggregationOperator.IntSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200016D RID: 365
		private class IntSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x06000A7E RID: 2686 RVA: 0x0002309B File Offset: 0x0002129B
			internal IntSumAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A7F RID: 2687 RVA: 0x000230AC File Offset: 0x000212AC
			protected override bool MoveNextCore(ref int currentElement)
			{
				int num = 0;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<int, TKey> source = this._source;
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
							num2 += num;
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000A80 RID: 2688 RVA: 0x00023108 File Offset: 0x00021308
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000674 RID: 1652
			private readonly QueryOperatorEnumerator<int, TKey> _source;
		}
	}
}
