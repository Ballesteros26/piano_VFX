using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A7 RID: 423
	internal sealed class AnyAllSearchOperator<TInput> : UnaryQueryOperator<TInput, bool>
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x00025ED0 File Offset: 0x000240D0
		internal AnyAllSearchOperator(IEnumerable<TInput> child, bool qualification, Func<TInput, bool> predicate)
			: base(child)
		{
			this._qualification = qualification;
			this._predicate = predicate;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00025EE8 File Offset: 0x000240E8
		internal bool Aggregate()
		{
			using (IEnumerator<bool> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == this._qualification)
					{
						return this._qualification;
					}
				}
			}
			return !this._qualification;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00025F4C File Offset: 0x0002414C
		internal override QueryResults<bool> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, bool>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00025F64 File Offset: 0x00024164
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<bool> recipient, bool preferStriping, QuerySettings settings)
		{
			Shared<bool> shared = new Shared<bool>(false);
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<bool, int> partitionedStream = new PartitionedStream<bool, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new AnyAllSearchOperator<TInput>.AnyAllSearchOperatorEnumerator<TKey>(inputStream[i], this._qualification, this._predicate, i, shared, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<bool> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006CA RID: 1738
		private readonly Func<TInput, bool> _predicate;

		// Token: 0x040006CB RID: 1739
		private readonly bool _qualification;

		// Token: 0x020001A8 RID: 424
		private class AnyAllSearchOperatorEnumerator<TKey> : QueryOperatorEnumerator<bool, int>
		{
			// Token: 0x06000B7B RID: 2939 RVA: 0x00025FCC File Offset: 0x000241CC
			internal AnyAllSearchOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, bool qualification, Func<TInput, bool> predicate, int partitionIndex, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this._source = source;
				this._qualification = qualification;
				this._predicate = predicate;
				this._partitionIndex = partitionIndex;
				this._resultFoundFlag = resultFoundFlag;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x00026004 File Offset: 0x00024204
			internal override bool MoveNext(ref bool currentElement, ref int currentKey)
			{
				if (this._resultFoundFlag.Value)
				{
					return false;
				}
				TInput tinput = default(TInput);
				TKey tkey = default(TKey);
				if (this._source.MoveNext(ref tinput, ref tkey))
				{
					currentElement = !this._qualification;
					currentKey = this._partitionIndex;
					int num = 0;
					for (;;)
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._resultFoundFlag.Value)
						{
							break;
						}
						if (this._predicate(tinput) == this._qualification)
						{
							goto Block_5;
						}
						if (!this._source.MoveNext(ref tinput, ref tkey))
						{
							return true;
						}
					}
					return false;
					Block_5:
					this._resultFoundFlag.Value = true;
					currentElement = this._qualification;
					return true;
				}
				return false;
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x000260B8 File Offset: 0x000242B8
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006CC RID: 1740
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x040006CD RID: 1741
			private readonly Func<TInput, bool> _predicate;

			// Token: 0x040006CE RID: 1742
			private readonly bool _qualification;

			// Token: 0x040006CF RID: 1743
			private readonly int _partitionIndex;

			// Token: 0x040006D0 RID: 1744
			private readonly Shared<bool> _resultFoundFlag;

			// Token: 0x040006D1 RID: 1745
			private readonly CancellationToken _cancellationToken;
		}
	}
}
