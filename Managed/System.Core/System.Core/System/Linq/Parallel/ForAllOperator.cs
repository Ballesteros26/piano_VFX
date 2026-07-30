using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B5 RID: 437
	internal sealed class ForAllOperator<TInput> : UnaryQueryOperator<TInput, TInput>
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x00026CC8 File Offset: 0x00024EC8
		internal ForAllOperator(IEnumerable<TInput> child, Action<TInput> elementAction)
			: base(child)
		{
			this._elementAction = elementAction;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00026CD8 File Offset: 0x00024ED8
		internal void RunSynchronously()
		{
			Shared<bool> shared = new Shared<bool>(false);
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			QuerySettings querySettings = base.SpecifiedQuerySettings.WithPerExecutionSettings(cancellationTokenSource, shared).WithDefaults();
			QueryLifecycle.LogicalQueryExecutionBegin(querySettings.QueryId);
			base.GetOpenedEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true, true, querySettings);
			querySettings.CleanStateAtQueryEnd();
			QueryLifecycle.LogicalQueryExecutionEnd(querySettings.QueryId);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000262B7 File Offset: 0x000244B7
		internal override QueryResults<TInput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, TInput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00026D3C File Offset: 0x00024F3C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TInput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInput, int> partitionedStream = new PartitionedStream<TInput, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ForAllOperator<TInput>.ForAllEnumerator<TKey>(inputStream[i], this._elementAction, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E57E File Offset: 0x0001C77E
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TInput> AsSequentialQuery(CancellationToken token)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000700 RID: 1792
		private readonly Action<TInput> _elementAction;

		// Token: 0x020001B6 RID: 438
		private class ForAllEnumerator<TKey> : QueryOperatorEnumerator<TInput, int>
		{
			// Token: 0x06000BB4 RID: 2996 RVA: 0x00026D95 File Offset: 0x00024F95
			internal ForAllEnumerator(QueryOperatorEnumerator<TInput, TKey> source, Action<TInput> elementAction, CancellationToken cancellationToken)
			{
				this._source = source;
				this._elementAction = elementAction;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x00026DB4 File Offset: 0x00024FB4
			internal override bool MoveNext(ref TInput currentElement, ref int currentKey)
			{
				TInput tinput = default(TInput);
				TKey tkey = default(TKey);
				int num = 0;
				while (this._source.MoveNext(ref tinput, ref tkey))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					this._elementAction(tinput);
				}
				return false;
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x00026E08 File Offset: 0x00025008
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000701 RID: 1793
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x04000702 RID: 1794
			private readonly Action<TInput> _elementAction;

			// Token: 0x04000703 RID: 1795
			private CancellationToken _cancellationToken;
		}
	}
}
