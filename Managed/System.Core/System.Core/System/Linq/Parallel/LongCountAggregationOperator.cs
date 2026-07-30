using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000170 RID: 368
	internal sealed class LongCountAggregationOperator<TSource> : InlinedAggregationOperator<TSource, long, long>
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x0002326D File Offset: 0x0002146D
		internal LongCountAggregationOperator(IEnumerable<TSource> child)
			: base(child)
		{
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00023278 File Offset: 0x00021478
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

		// Token: 0x06000A89 RID: 2697 RVA: 0x000232C8 File Offset: 0x000214C8
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongCountAggregationOperator<TSource>.LongCountAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000171 RID: 369
		private class LongCountAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x06000A8A RID: 2698 RVA: 0x000232D3 File Offset: 0x000214D3
			internal LongCountAggregationOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x000232E4 File Offset: 0x000214E4
			protected override bool MoveNextCore(ref long currentElement)
			{
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<TSource, TKey> source = this._source;
				if (source.MoveNext(ref tsource, ref tkey))
				{
					long num = 0L;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num += 1L;
						}
					}
					while (source.MoveNext(ref tsource, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x06000A8C RID: 2700 RVA: 0x00023348 File Offset: 0x00021548
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000676 RID: 1654
			private readonly QueryOperatorEnumerator<TSource, TKey> _source;
		}
	}
}
