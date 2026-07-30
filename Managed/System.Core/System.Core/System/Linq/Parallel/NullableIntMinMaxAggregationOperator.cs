using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200018A RID: 394
	internal sealed class NullableIntMinMaxAggregationOperator : InlinedAggregationOperator<int?, int?, int?>
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x00024646 File Offset: 0x00022846
		internal NullableIntMinMaxAggregationOperator(IEnumerable<int?> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00024658 File Offset: 0x00022858
		protected override int? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int? num;
			using (IEnumerator<int?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					int? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							int? num3 = enumerator.Current;
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
							int? num4 = enumerator.Current;
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

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002474C File Offset: 0x0002294C
		protected override QueryOperatorEnumerator<int?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntMinMaxAggregationOperator.NullableIntMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x0400068B RID: 1675
		private readonly int _sign;

		// Token: 0x0200018B RID: 395
		private class NullableIntMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int?>
		{
			// Token: 0x06000AD8 RID: 2776 RVA: 0x0002475D File Offset: 0x0002295D
			internal NullableIntMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x00024778 File Offset: 0x00022978
			protected override bool MoveNextCore(ref int? currentElement)
			{
				QueryOperatorEnumerator<int?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						int? num2 = null;
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
						int? num3 = null;
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

			// Token: 0x06000ADA RID: 2778 RVA: 0x00024886 File Offset: 0x00022A86
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400068C RID: 1676
			private QueryOperatorEnumerator<int?, TKey> _source;

			// Token: 0x0400068D RID: 1677
			private int _sign;
		}
	}
}
