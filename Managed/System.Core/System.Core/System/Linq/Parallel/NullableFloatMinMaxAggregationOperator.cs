using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000184 RID: 388
	internal sealed class NullableFloatMinMaxAggregationOperator : InlinedAggregationOperator<float?, float?, float?>
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00024125 File Offset: 0x00022325
		internal NullableFloatMinMaxAggregationOperator(IEnumerable<float?> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00024138 File Offset: 0x00022338
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? num;
			using (IEnumerator<float?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					float? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							float? num3 = enumerator.Current;
							if (num3 != null && (num2 == null || num3 < num2 || float.IsNaN(num3.GetValueOrDefault())))
							{
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							float? num4 = enumerator.Current;
							if (num4 != null && (num2 == null || num4 > num2 || float.IsNaN(num2.GetValueOrDefault())))
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

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002425C File Offset: 0x0002245C
		protected override QueryOperatorEnumerator<float?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatMinMaxAggregationOperator.NullableFloatMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000686 RID: 1670
		private readonly int _sign;

		// Token: 0x02000185 RID: 389
		private class NullableFloatMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<float?>
		{
			// Token: 0x06000AC6 RID: 2758 RVA: 0x0002426D File Offset: 0x0002246D
			internal NullableFloatMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000AC7 RID: 2759 RVA: 0x00024288 File Offset: 0x00022488
			protected override bool MoveNextCore(ref float? currentElement)
			{
				QueryOperatorEnumerator<float?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						float? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 != null && (currentElement == null || num2 < currentElement || float.IsNaN(num2.GetValueOrDefault())))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						float? num3 = null;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 != null && (currentElement == null || num3 > currentElement || float.IsNaN(currentElement.GetValueOrDefault())))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000AC8 RID: 2760 RVA: 0x000243C9 File Offset: 0x000225C9
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000687 RID: 1671
			private QueryOperatorEnumerator<float?, TKey> _source;

			// Token: 0x04000688 RID: 1672
			private int _sign;
		}
	}
}
