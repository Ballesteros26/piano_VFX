using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000156 RID: 342
	internal sealed class DecimalMinMaxAggregationOperator : InlinedAggregationOperator<decimal, decimal, decimal>
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x00022187 File Offset: 0x00020387
		internal DecimalMinMaxAggregationOperator(IEnumerable<decimal> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00022198 File Offset: 0x00020398
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal num;
			using (IEnumerator<decimal> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0m;
				}
				else
				{
					decimal num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							decimal num3 = enumerator.Current;
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
							decimal num4 = enumerator.Current;
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

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002223C File Offset: 0x0002043C
		protected override QueryOperatorEnumerator<decimal, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalMinMaxAggregationOperator.DecimalMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x0400065F RID: 1631
		private readonly int _sign;

		// Token: 0x02000157 RID: 343
		private class DecimalMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal>
		{
			// Token: 0x06000A37 RID: 2615 RVA: 0x0002224D File Offset: 0x0002044D
			internal DecimalMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x00022268 File Offset: 0x00020468
			protected override bool MoveNextCore(ref decimal currentElement)
			{
				QueryOperatorEnumerator<decimal, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						decimal num2 = 0m;
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
						decimal num3 = 0m;
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

			// Token: 0x06000A39 RID: 2617 RVA: 0x00022324 File Offset: 0x00020524
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000660 RID: 1632
			private QueryOperatorEnumerator<decimal, TKey> _source;

			// Token: 0x04000661 RID: 1633
			private int _sign;
		}
	}
}
