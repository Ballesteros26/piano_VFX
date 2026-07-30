using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B0 RID: 432
	internal sealed class ElementAtQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x00026784 File Offset: 0x00024984
		internal ElementAtQueryOperator(IEnumerable<TSource> child, int index)
			: base(child)
		{
			this._index = index;
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this._prematureMerge = true;
				this._limitsParallelism = ordinalIndexState != OrdinalIndexState.Shuffled;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000267C8 File Offset: 0x000249C8
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000267E0 File Offset: 0x000249E0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, int> partitionedStream;
			if (this._prematureMerge)
			{
				partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TSource, int>)inputStream;
			}
			Shared<bool> shared = new Shared<bool>(false);
			PartitionedStream<TSource, int> partitionedStream2 = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new ElementAtQueryOperator<TSource>.ElementAtQueryOperatorEnumerator(partitionedStream[i], this._index, shared, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00026873 File Offset: 0x00024A73
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002687C File Offset: 0x00024A7C
		internal bool Aggregate(out TSource result, bool withDefaultValue)
		{
			if (this.LimitsParallelism && base.SpecifiedQuerySettings.WithDefaults().ExecutionMode.Value != ParallelExecutionMode.ForceParallelism)
			{
				CancellationState cancellationState = base.SpecifiedQuerySettings.CancellationState;
				if (withDefaultValue)
				{
					IEnumerable<TSource> enumerable = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(cancellationState.ExternalCancellationToken), cancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TSource>(enumerable, cancellationState).ElementAtOrDefault(this._index);
				}
				else
				{
					IEnumerable<TSource> enumerable2 = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(cancellationState.ExternalCancellationToken), cancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TSource>(enumerable2, cancellationState).ElementAt(this._index);
				}
				return true;
			}
			using (IEnumerator<TSource> enumerator = base.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)))
			{
				if (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					result = tsource;
					return true;
				}
			}
			result = default(TSource);
			return false;
		}

		// Token: 0x040006ED RID: 1773
		private readonly int _index;

		// Token: 0x040006EE RID: 1774
		private readonly bool _prematureMerge;

		// Token: 0x040006EF RID: 1775
		private readonly bool _limitsParallelism;

		// Token: 0x020001B1 RID: 433
		private class ElementAtQueryOperatorEnumerator : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000BA1 RID: 2977 RVA: 0x00026990 File Offset: 0x00024B90
			internal ElementAtQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, int> source, int index, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this._source = source;
				this._index = index;
				this._resultFoundFlag = resultFoundFlag;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x000269B8 File Offset: 0x00024BB8
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				int num = 0;
				while (this._source.MoveNext(ref currentElement, ref currentKey))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._resultFoundFlag.Value)
					{
						break;
					}
					if (currentKey == this._index)
					{
						this._resultFoundFlag.Value = true;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x00026A13 File Offset: 0x00024C13
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F0 RID: 1776
			private QueryOperatorEnumerator<TSource, int> _source;

			// Token: 0x040006F1 RID: 1777
			private int _index;

			// Token: 0x040006F2 RID: 1778
			private Shared<bool> _resultFoundFlag;

			// Token: 0x040006F3 RID: 1779
			private CancellationToken _cancellationToken;
		}
	}
}
