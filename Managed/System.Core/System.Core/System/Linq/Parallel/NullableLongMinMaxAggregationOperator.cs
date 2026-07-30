using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000190 RID: 400
	internal sealed class NullableLongMinMaxAggregationOperator : InlinedAggregationOperator<long?, long?, long?>
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x00024AED File Offset: 0x00022CED
		internal NullableLongMinMaxAggregationOperator(IEnumerable<long?> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00024B00 File Offset: 0x00022D00
		protected override long? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long? num;
			using (IEnumerator<long?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					long? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							long? num3 = enumerator.Current;
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
							long? num4 = enumerator.Current;
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

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00024BF4 File Offset: 0x00022DF4
		protected override QueryOperatorEnumerator<long?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongMinMaxAggregationOperator.NullableLongMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000690 RID: 1680
		private readonly int _sign;

		// Token: 0x02000191 RID: 401
		private class NullableLongMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long?>
		{
			// Token: 0x06000AEA RID: 2794 RVA: 0x00024C05 File Offset: 0x00022E05
			internal NullableLongMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000AEB RID: 2795 RVA: 0x00024C20 File Offset: 0x00022E20
			protected override bool MoveNextCore(ref long? currentElement)
			{
				QueryOperatorEnumerator<long?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						long? num2 = null;
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
						long? num3 = null;
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

			// Token: 0x06000AEC RID: 2796 RVA: 0x00024D2E File Offset: 0x00022F2E
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000691 RID: 1681
			private QueryOperatorEnumerator<long?, TKey> _source;

			// Token: 0x04000692 RID: 1682
			private int _sign;
		}
	}
}
