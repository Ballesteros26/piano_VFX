using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000160 RID: 352
	internal sealed class FloatAverageAggregationOperator : InlinedAggregationOperator<float, Pair<double, long>, float>
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x00022845 File Offset: 0x00020A45
		internal FloatAverageAggregationOperator(IEnumerable<float> child)
			: base(child)
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00022850 File Offset: 0x00020A50
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float num;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					num = 0f;
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
					num = (float)(pair.First / (double)pair.Second);
				}
			}
			return num;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00022900 File Offset: 0x00020B00
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatAverageAggregationOperator.FloatAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000161 RID: 353
		private class FloatAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06000A55 RID: 2645 RVA: 0x0002290B File Offset: 0x00020B0B
			internal FloatAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, CancellationToken cancellationToken)
				: base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A56 RID: 2646 RVA: 0x0002291C File Offset: 0x00020B1C
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<float, TKey> source = this._source;
				float num3 = 0f;
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
						num += (double)num3;
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

			// Token: 0x06000A57 RID: 2647 RVA: 0x00022997 File Offset: 0x00020B97
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000668 RID: 1640
			private QueryOperatorEnumerator<float, TKey> _source;
		}
	}
}
