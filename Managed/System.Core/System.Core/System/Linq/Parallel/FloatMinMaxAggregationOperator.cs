using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000162 RID: 354
	internal sealed class FloatMinMaxAggregationOperator : InlinedAggregationOperator<float, float, float>
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x000229A4 File Offset: 0x00020BA4
		internal FloatMinMaxAggregationOperator(IEnumerable<float> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000229B4 File Offset: 0x00020BB4
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float num;
			using (IEnumerator<float> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0f;
				}
				else
				{
					float num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							float num3 = enumerator.Current;
							if (num3 < num2 || float.IsNaN(num3))
							{
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							float num4 = enumerator.Current;
							if (num4 > num2 || float.IsNaN(num2))
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

		// Token: 0x06000A5A RID: 2650 RVA: 0x00022A5C File Offset: 0x00020C5C
		protected override QueryOperatorEnumerator<float, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatMinMaxAggregationOperator.FloatMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000669 RID: 1641
		private readonly int _sign;

		// Token: 0x02000163 RID: 355
		private class FloatMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<float>
		{
			// Token: 0x06000A5B RID: 2651 RVA: 0x00022A6D File Offset: 0x00020C6D
			internal FloatMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A5C RID: 2652 RVA: 0x00022A88 File Offset: 0x00020C88
			protected override bool MoveNextCore(ref float currentElement)
			{
				QueryOperatorEnumerator<float, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						float num2 = 0f;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 < currentElement || float.IsNaN(num2))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						float num3 = 0f;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 > currentElement || float.IsNaN(currentElement))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000A5D RID: 2653 RVA: 0x00022B38 File Offset: 0x00020D38
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400066A RID: 1642
			private QueryOperatorEnumerator<float, TKey> _source;

			// Token: 0x0400066B RID: 1643
			private int _sign;
		}
	}
}
