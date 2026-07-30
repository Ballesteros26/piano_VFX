using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200017E RID: 382
	internal sealed class NullableDoubleMinMaxAggregationOperator : InlinedAggregationOperator<double?, double?, double?>
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00023C04 File Offset: 0x00021E04
		internal NullableDoubleMinMaxAggregationOperator(IEnumerable<double?> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00023C14 File Offset: 0x00021E14
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					double? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							double? num3 = enumerator.Current;
							if (num3 != null && (num2 == null || num3 < num2 || double.IsNaN(num3.GetValueOrDefault())))
							{
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							double? num4 = enumerator.Current;
							if (num4 != null && (num2 == null || num4 > num2 || double.IsNaN(num2.GetValueOrDefault())))
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

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00023D38 File Offset: 0x00021F38
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleMinMaxAggregationOperator.NullableDoubleMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000681 RID: 1665
		private readonly int _sign;

		// Token: 0x0200017F RID: 383
		private class NullableDoubleMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06000AB4 RID: 2740 RVA: 0x00023D49 File Offset: 0x00021F49
			internal NullableDoubleMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000AB5 RID: 2741 RVA: 0x00023D64 File Offset: 0x00021F64
			protected override bool MoveNextCore(ref double? currentElement)
			{
				QueryOperatorEnumerator<double?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						double? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 != null && (currentElement == null || num2 < currentElement || double.IsNaN(num2.GetValueOrDefault())))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						double? num3 = null;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 != null && (currentElement == null || num3 > currentElement || double.IsNaN(currentElement.GetValueOrDefault())))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000AB6 RID: 2742 RVA: 0x00023EA5 File Offset: 0x000220A5
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000682 RID: 1666
			private QueryOperatorEnumerator<double?, TKey> _source;

			// Token: 0x04000683 RID: 1667
			private int _sign;
		}
	}
}
