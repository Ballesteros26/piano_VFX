using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200016A RID: 362
	internal sealed class IntMinMaxAggregationOperator : InlinedAggregationOperator<int, int, int>
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x00022EC1 File Offset: 0x000210C1
		internal IntMinMaxAggregationOperator(IEnumerable<int> child, int sign)
			: base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00022ED4 File Offset: 0x000210D4
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int num;
			using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0;
				}
				else
				{
					int num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							int num3 = enumerator.Current;
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
							int num4 = enumerator.Current;
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

		// Token: 0x06000A77 RID: 2679 RVA: 0x00022F68 File Offset: 0x00021168
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntMinMaxAggregationOperator.IntMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000671 RID: 1649
		private readonly int _sign;

		// Token: 0x0200016B RID: 363
		private class IntMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x06000A78 RID: 2680 RVA: 0x00022F79 File Offset: 0x00021179
			internal IntMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x00022F94 File Offset: 0x00021194
			protected override bool MoveNextCore(ref int currentElement)
			{
				QueryOperatorEnumerator<int, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						int num2 = 0;
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
						int num3 = 0;
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

			// Token: 0x06000A7A RID: 2682 RVA: 0x00023028 File Offset: 0x00021228
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000672 RID: 1650
			private readonly QueryOperatorEnumerator<int, TKey> _source;

			// Token: 0x04000673 RID: 1651
			private readonly int _sign;
		}
	}
}
