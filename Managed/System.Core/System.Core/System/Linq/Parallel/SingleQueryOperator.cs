using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D8 RID: 472
	internal sealed class SingleQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x000287C7 File Offset: 0x000269C7
		internal SingleQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate)
			: base(child)
		{
			this._predicate = predicate;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000267C8 File Offset: 0x000249C8
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000287D8 File Offset: 0x000269D8
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			Shared<int> shared = new Shared<int>(0);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new SingleQueryOperator<TSource>.SingleQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, shared);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000763 RID: 1891
		private readonly Func<TSource, bool> _predicate;

		// Token: 0x020001D9 RID: 473
		private class SingleQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000C33 RID: 3123 RVA: 0x0002882D File Offset: 0x00026A2D
			internal SingleQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, Shared<int> totalElementCount)
			{
				this._source = source;
				this._predicate = predicate;
				this._totalElementCount = totalElementCount;
			}

			// Token: 0x06000C34 RID: 3124 RVA: 0x0002884C File Offset: 0x00026A4C
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (!this._alreadySearched)
				{
					bool flag = false;
					TSource tsource = default(TSource);
					TKey tkey = default(TKey);
					while (this._source.MoveNext(ref tsource, ref tkey))
					{
						if (this._predicate == null || this._predicate(tsource))
						{
							Interlocked.Increment(ref this._totalElementCount.Value);
							currentElement = tsource;
							currentKey = 0;
							if (flag)
							{
								this._yieldExtra = true;
								break;
							}
							flag = true;
						}
						if (Volatile.Read(ref this._totalElementCount.Value) > 1)
						{
							break;
						}
					}
					this._alreadySearched = true;
					return flag;
				}
				if (this._yieldExtra)
				{
					this._yieldExtra = false;
					currentElement = default(TSource);
					currentKey = 0;
					return true;
				}
				return false;
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x000288FD File Offset: 0x00026AFD
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000764 RID: 1892
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x04000765 RID: 1893
			private Func<TSource, bool> _predicate;

			// Token: 0x04000766 RID: 1894
			private bool _alreadySearched;

			// Token: 0x04000767 RID: 1895
			private bool _yieldExtra;

			// Token: 0x04000768 RID: 1896
			private Shared<int> _totalElementCount;
		}
	}
}
