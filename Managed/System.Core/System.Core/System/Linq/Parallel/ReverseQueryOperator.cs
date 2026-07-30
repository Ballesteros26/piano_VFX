using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CD RID: 461
	internal sealed class ReverseQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x00027E04 File Offset: 0x00026004
		internal ReverseQueryOperator(IEnumerable<TSource> child)
			: base(child)
		{
			if (base.Child.OrdinalIndexState == OrdinalIndexState.Indexable)
			{
				base.SetOrdinalIndexState(OrdinalIndexState.Indexable);
				return;
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00027E2C File Offset: 0x0002602C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, TKey> partitionedStream = new PartitionedStream<TSource, TKey>(partitionCount, new ReverseComparer<TKey>(inputStream.KeyComparer), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ReverseQueryOperator<TSource>.ReverseQueryOperatorEnumerator<TKey>(inputStream[i], settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00027E85 File Offset: 0x00026085
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return ReverseQueryOperator<TSource>.ReverseQueryOperatorResults.NewResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00027E9C File Offset: 0x0002609C
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(token), token).Reverse<TSource>();
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x020001CE RID: 462
		private class ReverseQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, TKey>
		{
			// Token: 0x06000C09 RID: 3081 RVA: 0x00027EB5 File Offset: 0x000260B5
			internal ReverseQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, CancellationToken cancellationToken)
			{
				this._source = source;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x00027ECC File Offset: 0x000260CC
			internal override bool MoveNext(ref TSource currentElement, ref TKey currentKey)
			{
				if (this._buffer == null)
				{
					this._bufferIndex = new Shared<int>(0);
					this._buffer = new List<Pair<TSource, TKey>>();
					TSource tsource = default(TSource);
					TKey tkey = default(TKey);
					int num = 0;
					while (this._source.MoveNext(ref tsource, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						this._buffer.Add(new Pair<TSource, TKey>(tsource, tkey));
						this._bufferIndex.Value++;
					}
				}
				Shared<int> bufferIndex = this._bufferIndex;
				int num2 = bufferIndex.Value - 1;
				bufferIndex.Value = num2;
				if (num2 >= 0)
				{
					currentElement = this._buffer[this._bufferIndex.Value].First;
					currentKey = this._buffer[this._bufferIndex.Value].Second;
					return true;
				}
				return false;
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x00027FBD File Offset: 0x000261BD
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000740 RID: 1856
			private readonly QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x04000741 RID: 1857
			private readonly CancellationToken _cancellationToken;

			// Token: 0x04000742 RID: 1858
			private List<Pair<TSource, TKey>> _buffer;

			// Token: 0x04000743 RID: 1859
			private Shared<int> _bufferIndex;
		}

		// Token: 0x020001CF RID: 463
		private class ReverseQueryOperatorResults : UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults
		{
			// Token: 0x06000C0C RID: 3084 RVA: 0x00027FCA File Offset: 0x000261CA
			public static QueryResults<TSource> NewResults(QueryResults<TSource> childQueryResults, ReverseQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new ReverseQueryOperator<TSource>.ReverseQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x00027FE7 File Offset: 0x000261E7
			private ReverseQueryOperatorResults(QueryResults<TSource> childQueryResults, ReverseQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
				: base(childQueryResults, op, settings, preferStriping)
			{
				this._count = this._childQueryResults.ElementsCount;
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0000AA13 File Offset: 0x00008C13
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00028005 File Offset: 0x00026205
			internal override int ElementsCount
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x0002800D File Offset: 0x0002620D
			internal override TSource GetElement(int index)
			{
				return this._childQueryResults.GetElement(this._count - index - 1);
			}

			// Token: 0x04000744 RID: 1860
			private int _count;
		}
	}
}
