using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000178 RID: 376
	internal sealed class NullableDecimalMinMaxAggregationOperator : InlinedAggregationOperator<decimal?, decimal?, decimal?>
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x0002371E File Offset: 0x0002191E
		internal NullableDecimalMinMaxAggregationOperator(IEnumerable<decimal?> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00023730 File Offset: 0x00021930
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num;
			using (IEnumerator<decimal?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					decimal? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							decimal? num3 = enumerator.Current;
							if (num2 == null || num3 < num2)
							{
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							decimal? num4 = enumerator.Current;
							if (num2 == null || num4 > num2)
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

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00023830 File Offset: 0x00021A30
		protected override QueryOperatorEnumerator<decimal?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalMinMaxAggregationOperator.NullableDecimalMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x0400067C RID: 1660
		private readonly int _sign;

		// Token: 0x02000179 RID: 377
		private class NullableDecimalMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal?>
		{
			// Token: 0x06000AA2 RID: 2722 RVA: 0x00023841 File Offset: 0x00021A41
			internal NullableDecimalMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x0002385C File Offset: 0x00021A5C
			protected override bool MoveNextCore(ref decimal? currentElement)
			{
				QueryOperatorEnumerator<decimal?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						decimal? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement == null || num2 < currentElement)
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						decimal? num3 = null;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement == null || num3 > currentElement)
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000AA4 RID: 2724 RVA: 0x00023974 File Offset: 0x00021B74
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400067D RID: 1661
			private QueryOperatorEnumerator<decimal?, TKey> _source;

			// Token: 0x0400067E RID: 1662
			private int _sign;
		}
	}
}
