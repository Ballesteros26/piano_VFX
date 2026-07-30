using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200015C RID: 348
	internal sealed class DoubleMinMaxAggregationOperator : InlinedAggregationOperator<double, double, double>
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x0002259F File Offset: 0x0002079F
		internal DoubleMinMaxAggregationOperator(IEnumerable<double> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000225B0 File Offset: 0x000207B0
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double num;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0.0;
				}
				else
				{
					double num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							double num3 = enumerator.Current;
							if (num3 < num2 || double.IsNaN(num3))
							{
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							double num4 = enumerator.Current;
							if (num4 > num2 || double.IsNaN(num2))
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

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002265C File Offset: 0x0002085C
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleMinMaxAggregationOperator.DoubleMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000664 RID: 1636
		private readonly int _sign;

		// Token: 0x0200015D RID: 349
		private class DoubleMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06000A49 RID: 2633 RVA: 0x0002266D File Offset: 0x0002086D
			internal DoubleMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A4A RID: 2634 RVA: 0x00022688 File Offset: 0x00020888
			protected override bool MoveNextCore(ref double currentElement)
			{
				QueryOperatorEnumerator<double, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						double num2 = 0.0;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 < currentElement || double.IsNaN(num2))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						double num3 = 0.0;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 > currentElement || double.IsNaN(currentElement))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000A4B RID: 2635 RVA: 0x00022740 File Offset: 0x00020940
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000665 RID: 1637
			private QueryOperatorEnumerator<double, TKey> _source;

			// Token: 0x04000666 RID: 1638
			private int _sign;
		}
	}
}
