using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000152 RID: 338
	internal sealed class CountAggregationOperator<TSource> : InlinedAggregationOperator<TSource, int, int>
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00021F31 File Offset: 0x00020131
		internal CountAggregationOperator(IEnumerable<TSource> child)
			: base(child)
		{
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00021F3C File Offset: 0x0002013C
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

		// Token: 0x06000A2A RID: 2602 RVA: 0x00021F8C File Offset: 0x0002018C
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new CountAggregationOperator<TSource>.CountAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000153 RID: 339
		private class CountAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x00021F97 File Offset: 0x00020197
			internal CountAggregationOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x00021FA8 File Offset: 0x000201A8
			protected override bool MoveNextCore(ref int currentElement)
			{
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<TSource, TKey> source = this._source;
				if (source.MoveNext(ref tsource, ref tkey))
				{
					int num = 0;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num++;
						}
					}
					while (source.MoveNext(ref tsource, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0002200A File Offset: 0x0002020A
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400065D RID: 1629
			private readonly QueryOperatorEnumerator<TSource, TKey> _source;
		}
	}
}
