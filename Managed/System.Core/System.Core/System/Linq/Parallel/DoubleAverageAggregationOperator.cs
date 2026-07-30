using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200015A RID: 346
	internal sealed class DoubleAverageAggregationOperator : InlinedAggregationOperator<double, Pair<double, long>, double>
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x00022439 File Offset: 0x00020639
		internal DoubleAverageAggregationOperator(IEnumerable<double> child)
			: base(child)
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00022444 File Offset: 0x00020644
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double num;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0.0;
				}
				else
				{
					Pair<double, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						double first = pair.First;
						Pair<double, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					num = pair.First / (double)pair.Second;
				}
			}
			return num;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000224F8 File Offset: 0x000206F8
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleAverageAggregationOperator.DoubleAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200015B RID: 347
		private class DoubleAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06000A43 RID: 2627 RVA: 0x00022503 File Offset: 0x00020703
			internal DoubleAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A44 RID: 2628 RVA: 0x00022514 File Offset: 0x00020714
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<double, TKey> source = this._source;
				double num3 = 0.0;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref num3, ref tkey))
				{
					int num4 = 0;
					do
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num += num3;
						checked
						{
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<double, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A45 RID: 2629 RVA: 0x00022592 File Offset: 0x00020792
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000663 RID: 1635
			private QueryOperatorEnumerator<double, TKey> _source;
		}
	}
}
