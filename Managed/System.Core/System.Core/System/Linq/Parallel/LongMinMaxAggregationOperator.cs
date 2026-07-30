using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000172 RID: 370
	internal sealed class LongMinMaxAggregationOperator : InlinedAggregationOperator<long, long, long>
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00023355 File Offset: 0x00021555
		internal LongMinMaxAggregationOperator(IEnumerable<long> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00023368 File Offset: 0x00021568
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long num;
			using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0L;
				}
				else
				{
					long num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							long num3 = enumerator.Current;
							if (num3 < num2)
							{
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							long num4 = enumerator.Current;
							if (num4 > num2)
							{
								num2 = num4;
							}
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000233FC File Offset: 0x000215FC
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongMinMaxAggregationOperator.LongMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000677 RID: 1655
		private readonly int _sign;

		// Token: 0x02000173 RID: 371
		private class LongMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x06000A90 RID: 2704 RVA: 0x0002340D File Offset: 0x0002160D
			internal LongMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x00023428 File Offset: 0x00021628
			protected override bool MoveNextCore(ref long currentElement)
			{
				QueryOperatorEnumerator<long, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						long num2 = 0L;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 < currentElement)
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						long num3 = 0L;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 > currentElement)
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x000234BE File Offset: 0x000216BE
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000678 RID: 1656
			private QueryOperatorEnumerator<long, TKey> _source;

			// Token: 0x04000679 RID: 1657
			private int _sign;
		}
	}
}
