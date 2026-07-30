using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A9 RID: 425
	internal sealed class ContainsSearchOperator<TInput> : UnaryQueryOperator<TInput, bool>
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x000260C5 File Offset: 0x000242C5
		internal ContainsSearchOperator(IEnumerable<TInput> child, TInput searchValue, IEqualityComparer<TInput> comparer)
			: base(child)
		{
			this._searchValue = searchValue;
			if (comparer == null)
			{
				this._comparer = EqualityComparer<TInput>.Default;
				return;
			}
			this._comparer = comparer;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000260EC File Offset: 0x000242EC
		internal bool Aggregate()
		{
			using (IEnumerator<bool> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00025F4C File Offset: 0x0002414C
		internal override QueryResults<bool> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, bool>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002613C File Offset: 0x0002433C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<bool> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<bool, int> partitionedStream = new PartitionedStream<bool, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			Shared<bool> shared = new Shared<bool>(false);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ContainsSearchOperator<TInput>.ContainsSearchOperatorEnumerator<TKey>(inputStream[i], this._searchValue, this._comparer, i, shared, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<bool> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006D2 RID: 1746
		private readonly TInput _searchValue;

		// Token: 0x040006D3 RID: 1747
		private readonly IEqualityComparer<TInput> _comparer;

		// Token: 0x020001AA RID: 426
		private class ContainsSearchOperatorEnumerator<TKey> : QueryOperatorEnumerator<bool, int>
		{
			// Token: 0x06000B84 RID: 2948 RVA: 0x000261A4 File Offset: 0x000243A4
			internal ContainsSearchOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, TInput searchValue, IEqualityComparer<TInput> comparer, int partitionIndex, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this._source = source;
				this._searchValue = searchValue;
				this._comparer = comparer;
				this._partitionIndex = partitionIndex;
				this._resultFoundFlag = resultFoundFlag;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x000261DC File Offset: 0x000243DC
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
					currentElement = false;
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
						if (this._comparer.Equals(tinput, this._searchValue))
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
					currentElement = true;
					return true;
				}
				return false;
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x00026283 File Offset: 0x00024483
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006D4 RID: 1748
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x040006D5 RID: 1749
			private readonly TInput _searchValue;

			// Token: 0x040006D6 RID: 1750
			private readonly IEqualityComparer<TInput> _comparer;

			// Token: 0x040006D7 RID: 1751
			private readonly int _partitionIndex;

			// Token: 0x040006D8 RID: 1752
			private readonly Shared<bool> _resultFoundFlag;

			// Token: 0x040006D9 RID: 1753
			private CancellationToken _cancellationToken;
		}
	}
}
